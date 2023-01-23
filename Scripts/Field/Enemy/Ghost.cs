using UnityEngine;
using System.Collections;

public class Ghost : Enemy
{
    [Header("Component")]
    public GameObject enemy; //Enemy ������Ʈ
    public Transform enemyTransform; //Enemy�� Transform ������Ʈ
    public Rigidbody2D enemyRigidbody2D; //Enemy�� Rigidbody2D ������Ʈ
    public Collider2D enemyCollider2D; //Enemy�� Collider2D ������Ʈ
    public Transform spritesTransform; //Sprites�� Transform ������Ʈ
    public Animator spritesAnimator; //Sprites�� Animator ������Ʈ
    public Transform criticalParticlePosTransform; //Critical Particle Pos�� Transform ������Ʈ

    [Header("Bar")]
    public Transform hpBarTransform; //HPBar�� Transform ������Ʈ
    public Transform slowBarTransform; //SlowBar�� Transform ������Ʈ

    [Header("Variable")]
    public string nickname; //����
    public float moveSpeed; //�̵� �ӵ�
    public float slowRate; //�̵� �ӵ� ���� ����
    public float slowRestoreRate; //�̵� �ӵ� ���� ������
    public float approachDistance; //�ٰ����� �Ÿ�
    public float attackRange; //���� ��Ÿ�
    public float attackRangeAxisY; //���� ��Ÿ� Y��
    public float setPlayerAfterImageMinDelay; //Player �ܻ��� �����ϴ� �ּ� ���� �ð�
    public float setPlayerAfterImageMaxDelay; //Player �ܻ��� �����ϴ� �ִ� ���� �ð�
    private bool _enableMove = true;
    public bool enableMove
    {
        get
        {
            return _enableMove;
        }
        set
        {
            _enableMove = value;
        }
    } //������ �� �ִ� �� �Ǵ��ϴ� ����

    [Header("Animator")]
    public float runAnimationSpeedMultiplier; //Run �ִϸ��̼� �ӵ� ������
    public float attackAnimationSpeedMultiplier; //Attack �ִϸ��̼� �ӵ� ������

    [Header("Damaged")]
    public readonly Color32 damagedColor = Color.red; //Damaged ����
    public SpriteRenderer[] damagedSpriteRenderers; //Damaged �� ������ �޴� SpriteRenderer ������Ʈ��
    public float damagedColorDisplayTime; //Damaged ���� ǥ�� �ð�

    [Header("Layer Order")]
    public SpriteRenderer[] layerOrderSpriteRenderers; //Layer ������ Sprite Renderer ������Ʈ��

    [Header("Invincibility")]
    public float invincibilityDistance; //���� �Ÿ�
    public SpriteRenderer[] invincibilitySpriteRenderers; //������ Sprite Renderer ������Ʈ��
    private readonly float invincibilityColorAlpha = 0.25f; //���� ���� ���İ�
    public GameObject[] invincibilityObjects; //������ ������Ʈ��

    [Header("Cache")]
    private Vector2 playerAfterImagePos; //Player�� �ܻ� ��ġ
    private Vector2 defaultScale; //�⺻ ũ��
    private Vector2 prePosition; //���� ��ġ
    private int[] layerOrderSpriteRendererOffsets; //Layer ������ Sprite Renderer�� �����µ�
    private readonly int run_Hash = Animator.StringToHash("Run"); //Run �ִϸ����� �ؽ�
    private readonly int attack_Hash = Animator.StringToHash("Attack"); //Attack �ִϸ����� �ؽ�
    private readonly int damaged_Hash = Animator.StringToHash("Damaged"); //Damaged �ִϸ����� �ؽ�
    private readonly int die_Hash = Animator.StringToHash("Die"); //Die �ִϸ����� �ؽ�
    private readonly int run_Speed_Hash = Animator.StringToHash("Run_Speed"); //Run_Speed �ִϸ����� �ؽ�
    private readonly int attack_Speed_Hash = Animator.StringToHash("Attack_Speed"); //Attack_Speed �ִϸ����� �ؽ�

    private void Start()
    {
        enemy.name = nickname; //���� ����
        currentHP = maxHP; //���� ü�� ����
        defaultScale = enemyTransform.localScale; //�⺻ ũ�� �ʱ�ȭ
        prePosition = enemyTransform.position; //���� ��ġ �ʱ�ȭ

        /* Layer ������ Sprite Renderer�� �����µ� �ʱ�ȭ */
        layerOrderSpriteRendererOffsets = new int[layerOrderSpriteRenderers.Length];
        for (int i = 0; i < layerOrderSpriteRendererOffsets.Length; ++i) layerOrderSpriteRendererOffsets[i] = layerOrderSpriteRenderers[i].sortingOrder;

        StartCoroutine(SetPlayerAfterImage());
    }

    private void Update()
    {
        ApplyInvincibility();

        SyncHPBar();
        SyncSlowBar();

        SetScale();
        SetLayerOrder();
        RestoreSlowRate();

        ControlAnimatorParameter();
        FadeDamagedColor();
    }

    private void FixedUpdate()
    {
        ChasePlayerAfterImage();
        UsePrePosition();
    }

    /* ������ �����ϴ� �Լ� */
    private void ApplyInvincibility()
    {
        if (currentHP <= 0f || !Player.player) return; //ü���� �� �����ǰų� Player�� �������� ������ ����
        
        if(Vector2.Distance(Player.player.playerTransform.position, enemyTransform.position) > invincibilityDistance) //���� �Ÿ��� �Ǹ�
        {
            enemyCollider2D.enabled = false; //Collider2D ��Ȱ��ȭ
            
            for (int i = 0; i < invincibilitySpriteRenderers.Length; ++i) //���� ���� ���İ� ����
            {
                Color defaultColor = invincibilitySpriteRenderers[i].color;
                defaultColor.a = invincibilityColorAlpha;
                invincibilitySpriteRenderers[i].color = defaultColor; 
            }

            for (int i = 0; i < invincibilityObjects.Length; ++i) invincibilityObjects[i].SetActive(false); //���� ������Ʈ ��Ȱ��ȭ
        }
        else
        {
            enemyCollider2D.enabled = true; //Collider2D Ȱ��ȭ

            for (int i = 0; i < invincibilitySpriteRenderers.Length; ++i) //�⺻ ���� ���İ� ����
            {
                Color defaultColor = invincibilitySpriteRenderers[i].color;
                defaultColor.a = 1f;
                invincibilitySpriteRenderers[i].color = defaultColor;
            }

            for (int i = 0; i < invincibilityObjects.Length; ++i) invincibilityObjects[i].SetActive(true); //���� ������Ʈ Ȱ��ȭ
        }
    }

    #region Bar
    /* HPBar�� ����ȭ�ϴ� �Լ� */
    private void SyncHPBar()
    {
        hpBarTransform.localScale = new Vector3(currentHP / maxHP, 1f, 1f);
    }

    /* SlowBar�� ����ȭ�ϴ� �Լ� */
    private void SyncSlowBar()
    {
        slowBarTransform.localScale = new Vector3(slowRate, 1f, 1f);
    }
    #endregion

    #region Transform
    /* ũ�⸦ �����ϴ� �Լ� */
    private void SetScale()
    {
        float axisYLerp = (enemyTransform.position.y - Definition.moveYRange.x) / (Definition.moveYRange.y - Definition.moveYRange.x); //Y�� ���� 0 ~ 1 ������ ������ ����
        enemyTransform.localScale = defaultScale * Mathf.Lerp(Definition.scaleRange.y, Definition.scaleRange.x, axisYLerp); //Y�� ���� ������ ũ�� ��� ����
    }

    /* ���̾� ������ �����ϴ� �Լ� */
    private void SetLayerOrder()
    {
        float axisYLerp = (enemyTransform.position.y - Definition.moveYRange.x) / (Definition.moveYRange.y - Definition.moveYRange.x); //Y�� ���� 0 ~ 1 ������ ������ ����
        int resultLayerOrder = (int)Mathf.Lerp(Definition.layerOrderRange.y, Definition.layerOrderRange.x, axisYLerp); //Y�� ���� ������ ���̾� ���� ��� ����

        for (int i = 0; i < layerOrderSpriteRenderers.Length; ++i) layerOrderSpriteRenderers[i].sortingOrder = resultLayerOrder + layerOrderSpriteRendererOffsets[i]; //Layer ������ Sprite Renderer ������Ʈ���� ���̾� ���� ����
    }
    #endregion

    #region Chasing
    /* Player�� �ܻ��� �����ϴ� �ڷ�ƾ �Լ� */
    private IEnumerator SetPlayerAfterImage()
    {
        while (true)
        {
            while (!Player.player) yield return null; //Player�� �������� ������ ����

            Vector3 playerLeftPos = Player.player.playerTransform.position - new Vector3(approachDistance, 0f, 0f);
            Vector3 playerRightPos = Player.player.playerTransform.position + new Vector3(approachDistance, 0f, 0f);

            if (Vector3.Distance(playerLeftPos, enemyTransform.position) < Vector3.Distance(playerRightPos, enemyTransform.position)) //Player�� ������ �� ������
            {
                playerAfterImagePos = playerLeftPos;
            }
            else //Player�� �������� �� ������
            {
                playerAfterImagePos = playerRightPos;
            }

            playerAfterImagePos = new Vector2(Mathf.Clamp(playerAfterImagePos.x, Definition.moveXRange.x, Definition.moveXRange.y), Mathf.Clamp(playerAfterImagePos.y, Definition.moveYRange.x, Definition.moveYRange.y)); //���� ����

            yield return new WaitForSeconds(Random.Range(setPlayerAfterImageMinDelay, setPlayerAfterImageMaxDelay));
        }
    }

    /* Player�� �ܻ��� �Ѵ� �Լ� */
    private void ChasePlayerAfterImage()
    {
        if (!enableMove || !Player.player) return; //������ �� ���� ��Ȳ�̸� �Լ� ����

        if (Mathf.Abs(playerAfterImagePos.y - enemyTransform.position.y) >= attackRangeAxisY || Mathf.Abs(Player.player.playerTransform.position.x - enemyTransform.position.x) >= approachDistance) //Y���� �������� �ʰų� ���� ��Ÿ��� �ȵǸ�
        {
            float axisYLerp = (enemyTransform.position.y - Definition.moveYRange.x) / (Definition.moveYRange.y - Definition.moveYRange.x); //Y�� ���� 0 ~ 1 ������ ������ ����
            float resultMoveSpeed = moveSpeed * Mathf.Lerp(Definition.moveSpeedRange.y, Definition.moveSpeedRange.x, axisYLerp); //Y�� ���� ������ �̵� �ӵ� ����

            enemyRigidbody2D.MovePosition(Vector3.MoveTowards(enemyTransform.position, playerAfterImagePos, resultMoveSpeed * (1f - slowRate) * Time.deltaTime)); //�ܻ��� ���� �̵�
        }
    }

    /* �̵� �ӵ� �������� �����ϴ� �Լ� */
    private void RestoreSlowRate()
    {
        float result = slowRate - slowRestoreRate * Time.deltaTime;
        slowRate = result < 0f ? 0f : result;
    }

    /* ���� ��ġ�� ����ϴ� �Լ� */
    private void UsePrePosition()
    {
        if (!enableMove) return; //������ �� ������ ����

        if (enemyTransform.position.x - prePosition.x < 0f) //�������� �̵��ϸ�
            spritesTransform.rotation = Quaternion.Euler(0f, 0f, 0f); //ȸ��
        else if (enemyTransform.position.x - prePosition.x > 0f) //���������� �̵��ϸ�
            spritesTransform.rotation = Quaternion.Euler(0f, 180f, 0f); //ȸ��

        spritesAnimator.SetBool(run_Hash, enemyTransform.position != (Vector3)prePosition); //�ִϸ����� Run �Ķ���� ����

        prePosition = enemyTransform.position; //���� ��ġ ����
    }
    #endregion

    #region Animator & Damaged
    /* Animator�� Parameter�� �����ϴ� �Լ� */
    private void ControlAnimatorParameter()
    {
        spritesAnimator.SetBool(attack_Hash, Player.player && Mathf.Abs(Player.player.playerTransform.position.y - enemyTransform.position.y) <= attackRangeAxisY && Vector3.Distance(enemyTransform.position, Player.player.playerTransform.position) <= attackRange); //Attack �Ķ���� ����
        spritesAnimator.SetFloat(run_Speed_Hash, runAnimationSpeedMultiplier * (1f - slowRate)); //Run �ִϸ��̼� �ӵ� ����
        spritesAnimator.SetFloat(attack_Speed_Hash, attackAnimationSpeedMultiplier * (1f - slowRate)); //Attack �ִϸ��̼� �ӵ� ����
    }

    /* Player�� �ٶ󺸴� �Լ� */
    public void StarePlayer()
    {
        if (!Player.player) return; //Player�� �������� ������ ����

        if (enemyTransform.position.x - Player.player.playerTransform.position.x < 0f) //Player�� �����ʿ� ������
            spritesTransform.rotation = Quaternion.Euler(0f, 180f, 0f); //ȸ��
        else if (enemyTransform.position.x - Player.player.playerTransform.position.x > 0f) //Player�� ���ʿ� ������
            spritesTransform.rotation = Quaternion.Euler(0f, 0f, 0f); //ȸ��
    }

    /* ���ظ� �Դ� �Լ� */
    public override void GetDamage(float damage, float slow)
    {
        currentHP = currentHP - damage < 0f ? 0f : currentHP - damage; //HP ����
        slowRate = slowRate + slow > 1f ? 1f : slowRate + slow; //�̵� �ӵ� ������ ����

        if (slow <= 0f) spritesAnimator.SetTrigger(damaged_Hash); //Damaged Ʈ���� Ȱ��ȭ
        if (currentHP <= 0f) spritesAnimator.Play(die_Hash); //ü���� �� �����Ǹ� Die �ִϸ��̼� ����

        SetDamagedColor();
    }

    /* Damaged ������ �����ϴ� �Լ� */
    private void SetDamagedColor()
    {
        for (int i = 0; i < damagedSpriteRenderers.Length; ++i) damagedSpriteRenderers[i].color = damagedColor;
    }

    /* Damaged ������ ������� �ϴ� �Լ� */
    private void FadeDamagedColor()
    {
        for (int i = 0; i < damagedSpriteRenderers.Length; ++i) damagedSpriteRenderers[i].color += Color.white * Time.deltaTime / damagedColorDisplayTime;
    }

    /* Critical Particle�� �����ϴ� �Լ� */
    public override void InstanciateCriticalParticle(GameObject criticalParticle)
    {
        if (criticalParticle) Utility.InstantiateParticleSystemOnWorldSpace(criticalParticle, criticalParticlePosTransform.position, criticalParticlePosTransform.lossyScale);
    }

    /* �״� �Լ� */
    public void Die()
    {
        enableMove = false; //�̵� ����
        enemyCollider2D.enabled = false; //Collider2D ��Ȱ��ȭ
        ++FieldManager.fieldManager.currentKill; //���� ų �� ����
        if (Utility.GetChance(Player.player.moneyProbability)) FieldManager.fieldManager.currentMoney += Player.player.moneyAmount; //���� Money ����
        Destroy(enemy, Definition.enemyDestroyDelay); //Enemy ������Ʈ ����
    }
    #endregion
}
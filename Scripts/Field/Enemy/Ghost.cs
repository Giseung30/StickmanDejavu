using UnityEngine;
using System.Collections;

public class Ghost : Enemy
{
    [Header("Component")]
    public GameObject enemy; //Enemy 오브젝트
    public Transform enemyTransform; //Enemy의 Transform 컴포넌트
    public Rigidbody2D enemyRigidbody2D; //Enemy의 Rigidbody2D 컴포넌트
    public Collider2D enemyCollider2D; //Enemy의 Collider2D 컴포넌트
    public Transform spritesTransform; //Sprites의 Transform 컴포넌트
    public Animator spritesAnimator; //Sprites의 Animator 컴포넌트
    public Transform criticalParticlePosTransform; //Critical Particle Pos의 Transform 컴포넌트

    [Header("Bar")]
    public Transform hpBarTransform; //HPBar의 Transform 컴포넌트
    public Transform slowBarTransform; //SlowBar의 Transform 컴포넌트

    [Header("Variable")]
    public string nickname; //별명
    public float moveSpeed; //이동 속도
    public float slowRate; //이동 속도 감소 비율
    public float slowRestoreRate; //이동 속도 감소 복구율
    public float approachDistance; //다가서는 거리
    public float attackRange; //공격 사거리
    public float attackRangeAxisY; //공격 사거리 Y축
    public float setPlayerAfterImageMinDelay; //Player 잔상을 지정하는 최소 지연 시간
    public float setPlayerAfterImageMaxDelay; //Player 잔상을 지정하는 최대 지연 시간
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
    } //움직일 수 있는 지 판단하는 변수

    [Header("Animator")]
    public float runAnimationSpeedMultiplier; //Run 애니메이션 속도 곱셈기
    public float attackAnimationSpeedMultiplier; //Attack 애니메이션 속도 곱셈기

    [Header("Damaged")]
    public readonly Color32 damagedColor = Color.red; //Damaged 색상
    public SpriteRenderer[] damagedSpriteRenderers; //Damaged 시 영향을 받는 SpriteRenderer 컴포넌트들
    public float damagedColorDisplayTime; //Damaged 색상 표시 시간

    [Header("Layer Order")]
    public SpriteRenderer[] layerOrderSpriteRenderers; //Layer 순서용 Sprite Renderer 컴포넌트들

    [Header("Invincibility")]
    public float invincibilityDistance; //무적 거리
    public SpriteRenderer[] invincibilitySpriteRenderers; //무적용 Sprite Renderer 컴포넌트들
    private readonly float invincibilityColorAlpha = 0.25f; //무적 색상 알파값
    public GameObject[] invincibilityObjects; //무적용 오브젝트들

    [Header("Cache")]
    private Vector2 playerAfterImagePos; //Player의 잔상 위치
    private Vector2 defaultScale; //기본 크기
    private Vector2 prePosition; //이전 위치
    private int[] layerOrderSpriteRendererOffsets; //Layer 순서용 Sprite Renderer의 오프셋들
    private readonly int run_Hash = Animator.StringToHash("Run"); //Run 애니메이터 해쉬
    private readonly int attack_Hash = Animator.StringToHash("Attack"); //Attack 애니메이터 해쉬
    private readonly int damaged_Hash = Animator.StringToHash("Damaged"); //Damaged 애니메이터 해쉬
    private readonly int die_Hash = Animator.StringToHash("Die"); //Die 애니메이터 해쉬
    private readonly int run_Speed_Hash = Animator.StringToHash("Run_Speed"); //Run_Speed 애니메이터 해쉬
    private readonly int attack_Speed_Hash = Animator.StringToHash("Attack_Speed"); //Attack_Speed 애니메이터 해쉬

    private void Start()
    {
        enemy.name = nickname; //별명 지정
        currentHP = maxHP; //현재 체력 지정
        defaultScale = enemyTransform.localScale; //기본 크기 초기화
        prePosition = enemyTransform.position; //이전 위치 초기화

        /* Layer 순서용 Sprite Renderer의 오프셋들 초기화 */
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

    /* 무적을 적용하는 함수 */
    private void ApplyInvincibility()
    {
        if (currentHP <= 0f || !Player.player) return; //체력이 다 소진되거나 Player가 존재하지 않으면 종료
        
        if(Vector2.Distance(Player.player.playerTransform.position, enemyTransform.position) > invincibilityDistance) //무적 거리가 되면
        {
            enemyCollider2D.enabled = false; //Collider2D 비활성화
            
            for (int i = 0; i < invincibilitySpriteRenderers.Length; ++i) //무적 색상 알파값 지정
            {
                Color defaultColor = invincibilitySpriteRenderers[i].color;
                defaultColor.a = invincibilityColorAlpha;
                invincibilitySpriteRenderers[i].color = defaultColor; 
            }

            for (int i = 0; i < invincibilityObjects.Length; ++i) invincibilityObjects[i].SetActive(false); //무적 오브젝트 비활성화
        }
        else
        {
            enemyCollider2D.enabled = true; //Collider2D 활성화

            for (int i = 0; i < invincibilitySpriteRenderers.Length; ++i) //기본 색상 알파값 지정
            {
                Color defaultColor = invincibilitySpriteRenderers[i].color;
                defaultColor.a = 1f;
                invincibilitySpriteRenderers[i].color = defaultColor;
            }

            for (int i = 0; i < invincibilityObjects.Length; ++i) invincibilityObjects[i].SetActive(true); //무적 오브젝트 활성화
        }
    }

    #region Bar
    /* HPBar를 동기화하는 함수 */
    private void SyncHPBar()
    {
        hpBarTransform.localScale = new Vector3(currentHP / maxHP, 1f, 1f);
    }

    /* SlowBar를 동기화하는 함수 */
    private void SyncSlowBar()
    {
        slowBarTransform.localScale = new Vector3(slowRate, 1f, 1f);
    }
    #endregion

    #region Transform
    /* 크기를 지정하는 함수 */
    private void SetScale()
    {
        float axisYLerp = (enemyTransform.position.y - Definition.moveYRange.x) / (Definition.moveYRange.y - Definition.moveYRange.x); //Y축 값을 0 ~ 1 사이의 값으로 보간
        enemyTransform.localScale = defaultScale * Mathf.Lerp(Definition.scaleRange.y, Definition.scaleRange.x, axisYLerp); //Y축 보간 값으로 크기 결과 저장
    }

    /* 레이어 순서를 지정하는 함수 */
    private void SetLayerOrder()
    {
        float axisYLerp = (enemyTransform.position.y - Definition.moveYRange.x) / (Definition.moveYRange.y - Definition.moveYRange.x); //Y축 값을 0 ~ 1 사이의 값으로 보간
        int resultLayerOrder = (int)Mathf.Lerp(Definition.layerOrderRange.y, Definition.layerOrderRange.x, axisYLerp); //Y축 보간 값으로 레이어 순서 결과 저장

        for (int i = 0; i < layerOrderSpriteRenderers.Length; ++i) layerOrderSpriteRenderers[i].sortingOrder = resultLayerOrder + layerOrderSpriteRendererOffsets[i]; //Layer 순서용 Sprite Renderer 컴포넌트들의 레이어 순서 지정
    }
    #endregion

    #region Chasing
    /* Player의 잔상을 지정하는 코루틴 함수 */
    private IEnumerator SetPlayerAfterImage()
    {
        while (true)
        {
            while (!Player.player) yield return null; //Player가 존재하지 않으면 정지

            Vector3 playerLeftPos = Player.player.playerTransform.position - new Vector3(approachDistance, 0f, 0f);
            Vector3 playerRightPos = Player.player.playerTransform.position + new Vector3(approachDistance, 0f, 0f);

            if (Vector3.Distance(playerLeftPos, enemyTransform.position) < Vector3.Distance(playerRightPos, enemyTransform.position)) //Player의 왼쪽이 더 가까우면
            {
                playerAfterImagePos = playerLeftPos;
            }
            else //Player의 오른쪽이 더 가까우면
            {
                playerAfterImagePos = playerRightPos;
            }

            playerAfterImagePos = new Vector2(Mathf.Clamp(playerAfterImagePos.x, Definition.moveXRange.x, Definition.moveXRange.y), Mathf.Clamp(playerAfterImagePos.y, Definition.moveYRange.x, Definition.moveYRange.y)); //범위 제한

            yield return new WaitForSeconds(Random.Range(setPlayerAfterImageMinDelay, setPlayerAfterImageMaxDelay));
        }
    }

    /* Player의 잔상을 쫓는 함수 */
    private void ChasePlayerAfterImage()
    {
        if (!enableMove || !Player.player) return; //움직일 수 없는 상황이면 함수 종료

        if (Mathf.Abs(playerAfterImagePos.y - enemyTransform.position.y) >= attackRangeAxisY || Mathf.Abs(Player.player.playerTransform.position.x - enemyTransform.position.x) >= approachDistance) //Y축이 도달하지 않거나 접근 사거리가 안되면
        {
            float axisYLerp = (enemyTransform.position.y - Definition.moveYRange.x) / (Definition.moveYRange.y - Definition.moveYRange.x); //Y축 값을 0 ~ 1 사이의 값으로 보간
            float resultMoveSpeed = moveSpeed * Mathf.Lerp(Definition.moveSpeedRange.y, Definition.moveSpeedRange.x, axisYLerp); //Y축 보간 값으로 이동 속도 결정

            enemyRigidbody2D.MovePosition(Vector3.MoveTowards(enemyTransform.position, playerAfterImagePos, resultMoveSpeed * (1f - slowRate) * Time.deltaTime)); //잔상을 향해 이동
        }
    }

    /* 이동 속도 감소율을 복구하는 함수 */
    private void RestoreSlowRate()
    {
        float result = slowRate - slowRestoreRate * Time.deltaTime;
        slowRate = result < 0f ? 0f : result;
    }

    /* 이전 위치를 사용하는 함수 */
    private void UsePrePosition()
    {
        if (!enableMove) return; //움직일 수 없으면 종료

        if (enemyTransform.position.x - prePosition.x < 0f) //왼쪽으로 이동하면
            spritesTransform.rotation = Quaternion.Euler(0f, 0f, 0f); //회전
        else if (enemyTransform.position.x - prePosition.x > 0f) //오른쪽으로 이동하면
            spritesTransform.rotation = Quaternion.Euler(0f, 180f, 0f); //회전

        spritesAnimator.SetBool(run_Hash, enemyTransform.position != (Vector3)prePosition); //애니메이터 Run 파라미터 지정

        prePosition = enemyTransform.position; //이전 위치 갱신
    }
    #endregion

    #region Animator & Damaged
    /* Animator의 Parameter를 조절하는 함수 */
    private void ControlAnimatorParameter()
    {
        spritesAnimator.SetBool(attack_Hash, Player.player && Mathf.Abs(Player.player.playerTransform.position.y - enemyTransform.position.y) <= attackRangeAxisY && Vector3.Distance(enemyTransform.position, Player.player.playerTransform.position) <= attackRange); //Attack 파라미터 지정
        spritesAnimator.SetFloat(run_Speed_Hash, runAnimationSpeedMultiplier * (1f - slowRate)); //Run 애니메이션 속도 지정
        spritesAnimator.SetFloat(attack_Speed_Hash, attackAnimationSpeedMultiplier * (1f - slowRate)); //Attack 애니메이션 속도 지정
    }

    /* Player를 바라보는 함수 */
    public void StarePlayer()
    {
        if (!Player.player) return; //Player가 존재하지 않으면 종료

        if (enemyTransform.position.x - Player.player.playerTransform.position.x < 0f) //Player가 오른쪽에 있으면
            spritesTransform.rotation = Quaternion.Euler(0f, 180f, 0f); //회전
        else if (enemyTransform.position.x - Player.player.playerTransform.position.x > 0f) //Player가 왼쪽에 있으면
            spritesTransform.rotation = Quaternion.Euler(0f, 0f, 0f); //회전
    }

    /* 피해를 입는 함수 */
    public override void GetDamage(float damage, float slow)
    {
        currentHP = currentHP - damage < 0f ? 0f : currentHP - damage; //HP 변경
        slowRate = slowRate + slow > 1f ? 1f : slowRate + slow; //이동 속도 감소율 변경

        if (slow <= 0f) spritesAnimator.SetTrigger(damaged_Hash); //Damaged 트리거 활성화
        if (currentHP <= 0f) spritesAnimator.Play(die_Hash); //체력이 다 소진되면 Die 애니메이션 실행

        SetDamagedColor();
    }

    /* Damaged 색상을 지정하는 함수 */
    private void SetDamagedColor()
    {
        for (int i = 0; i < damagedSpriteRenderers.Length; ++i) damagedSpriteRenderers[i].color = damagedColor;
    }

    /* Damaged 색상을 사라지게 하는 함수 */
    private void FadeDamagedColor()
    {
        for (int i = 0; i < damagedSpriteRenderers.Length; ++i) damagedSpriteRenderers[i].color += Color.white * Time.deltaTime / damagedColorDisplayTime;
    }

    /* Critical Particle을 생성하는 함수 */
    public override void InstanciateCriticalParticle(GameObject criticalParticle)
    {
        if (criticalParticle) Utility.InstantiateParticleSystemOnWorldSpace(criticalParticle, criticalParticlePosTransform.position, criticalParticlePosTransform.lossyScale);
    }

    /* 죽는 함수 */
    public void Die()
    {
        enableMove = false; //이동 금지
        enemyCollider2D.enabled = false; //Collider2D 비활성화
        ++FieldManager.fieldManager.currentKill; //현재 킬 수 증가
        if (Utility.GetChance(Player.player.moneyProbability)) FieldManager.fieldManager.currentMoney += Player.player.moneyAmount; //현재 Money 증가
        Destroy(enemy, Definition.enemyDestroyDelay); //Enemy 오브젝트 제거
    }
    #endregion
}
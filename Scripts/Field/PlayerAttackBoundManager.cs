using UnityEngine;
using System.Collections;

public class PlayerAttackBoundManager : MonoBehaviour
{
    public enum AttackBoundType
    {
        Once,
        Waiting,
        Activation
    }

    [Header("Type")]
    public AttackBoundType attackBoundType; //Attack Bound ����
    public bool ultGaugeIncreasing; //�ñر� ���� ����
    public bool playOnStart; //Start �� ���� ����

    [Header("Component")]
    public Collider2D playerAttackBoundCollider2D; //Collider2D ������Ʈ
    public AudioSource onTriggerEnterAudioSource; //OnTriggerEnter �� ����Ǵ� Audio Source ������Ʈ
    public GameObject criticalParticleObject; //Critical Particle ������Ʈ

    [Header("Variable")]
    public float damage; //���ط�
    public float damageMultiplier; //���ط� ������
    public float slow; //�̵� �ӵ� ���ҷ�
    public float slowMultiplier; //�̵� �ӵ� ���ҷ� ������
    public float waitingTime; //��� �ð�

    [Header("Cache")]
    private Coroutine coroutine; //�ڷ�ƾ �Լ�
    private bool onTrigger; //Trigger ���θ� �Ǵ��ϴ� ����
    private readonly WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private void Start()
    {
        if (playOnStart) SetEnableAttackBound(true);
    }

    /* Attack Bound�� Ȱ��ȭ�ϴ� �Լ� */
    public void SetEnableAttackBound(bool value)
    {
        if (value) //Ȱ��ȭ�̸�
        {
            switch (attackBoundType)
            {
                case AttackBoundType.Once: //Once
                    coroutine = StartCoroutine(ActivateOnceAttackBound());
                    break;
                case AttackBoundType.Waiting: //Waiting
                    if (coroutine != null) //�ڷ�ƾ �Լ� ���� ��
                    {
                        StopCoroutine(coroutine); //�ڷ�ƾ �Լ� ��� ����
                        playerAttackBoundCollider2D.enabled = false; //Collider2D ������Ʈ ��Ȱ��ȭ
                        onTrigger = false; //Trigger ����
                    }
                    coroutine = StartCoroutine(ActivateWaitingAttackBound());
                    break;
                case AttackBoundType.Activation: //Activation
                    playerAttackBoundCollider2D.enabled = true; //Collider2D ������Ʈ Ȱ��ȭ
                    break;
            }
        }
        else //��Ȱ��ȭ�̸�
        {
            switch (attackBoundType)
            {
                case AttackBoundType.Once: //Once
                case AttackBoundType.Waiting: //Waiting
                    if (coroutine != null) //�ڷ�ƾ �Լ� ���� ��
                    {
                        StopCoroutine(coroutine); //�ڷ�ƾ �Լ� ��� ����
                        playerAttackBoundCollider2D.enabled = false; //Collider2D ������Ʈ ��Ȱ��ȭ
                        onTrigger = false; //Trigger ����
                    }
                    break;
                case AttackBoundType.Activation: //Activation
                    playerAttackBoundCollider2D.enabled = false; //Collider2D ������Ʈ ��Ȱ��ȭ
                    onTrigger = false; //Trigger ����
                    break;
            }
        }
    }

    /* Once Attack Bound�� Ȱ��ȭ�ϴ� �ڷ�ƾ �Լ� */
    private IEnumerator ActivateOnceAttackBound()
    {
        playerAttackBoundCollider2D.enabled = true; //Collider2D ������Ʈ Ȱ��ȭ

        yield return waitForFixedUpdate; //������ ���

        playerAttackBoundCollider2D.enabled = false; //Collider2D ������Ʈ ��Ȱ��ȭ
        onTrigger = false; //Trigger ����
    }

    /* Waiting Attack Bound�� Ȱ��ȭ�ϴ� �ڷ�ƾ �Լ� */
    private IEnumerator ActivateWaitingAttackBound()
    {
        playerAttackBoundCollider2D.enabled = true; //Collider2D ������Ʈ Ȱ��ȭ

        yield return new WaitForSeconds(waitingTime); //��� �ð����� Ȱ��ȭ

        playerAttackBoundCollider2D.enabled = false; //Collider2D ������Ʈ ��Ȱ��ȭ
        onTrigger = false; //Trigger ����
    }

    /* Trigger�� �浹���� �� ����Ǵ� �Լ� */
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (attackBoundType == AttackBoundType.Once)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (Utility.GetChance(Player.player.criticalProbability)) //ġ��Ÿ�� �߻��ϸ�
            { 
                enemy.GetDamage(damage * damageMultiplier * Player.player.criticalDamage, slow * slowMultiplier); //���� ����
                enemy.InstanciateCriticalParticle(criticalParticleObject); //ġ��Ÿ ȿ�� ����
            }
            else //ġ��Ÿ�� �߻����� ������
                enemy.GetDamage(damage * damageMultiplier, slow * slowMultiplier); //���� ����

            if (ultGaugeIncreasing) UltButton.ultButton.IncreaseUltGauge(1f); //�ñر� ������ ����
            if (onTriggerEnterAudioSource && !onTrigger) onTriggerEnterAudioSource.PlayOneShot(onTriggerEnterAudioSource.clip); //OnTriggerEnter AudioSource ������Ʈ ���

            onTrigger = true; //Trigger ����
        }
    }

    /* Trigger�� �浹 �� �϶� ����Ǵ� �Լ� */
    private void OnTriggerStay2D(Collider2D col)
    {
        if (attackBoundType == AttackBoundType.Waiting || attackBoundType == AttackBoundType.Activation)
        {
            col.GetComponent<Enemy>().GetDamage(damage * damageMultiplier * Time.deltaTime, slow * slowMultiplier * Time.deltaTime); //���� ����
            if (ultGaugeIncreasing) UltButton.ultButton.IncreaseUltGauge(Time.deltaTime); //�ñر� ������ ����
        }
    }
}
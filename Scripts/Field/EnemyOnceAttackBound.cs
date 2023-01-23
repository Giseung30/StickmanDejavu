using UnityEngine;
using System.Collections;

public class EnemyOnceAttackBound : MonoBehaviour
{
    [Header("Component")]
    public Collider2D enemyAttackBoundCollider2D; //Collider2D ������Ʈ
    public AudioSource onTriggerEnterAudioSource; //OnTriggerEnter �� ����Ǵ� Audio Source ������Ʈ

    [Header("Variable")]
    public float damage; //���ط�

    [Header("Cache")]
    private Coroutine coroutine; //�ڷ�ƾ �Լ�
    private readonly WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    /* Bound�� Ȱ��ȭ�ϴ� �Լ� */
    public void SetEnableBound()
    {
        if (coroutine != null) //�ڷ�ƾ �Լ� ���� ��
        {
            StopCoroutine(coroutine); //�ڷ�ƾ �Լ� ��� ����
            enemyAttackBoundCollider2D.enabled = false; //Collider2D ������Ʈ ��Ȱ��ȭ
        }
        coroutine = StartCoroutine(TriggerBound());
    }

    /* Bound�� Ʈ�����ϴ� �ڷ�ƾ �Լ� */
    private IEnumerator TriggerBound()
    {
        enemyAttackBoundCollider2D.enabled = true; //Collider2D ������Ʈ Ȱ��ȭ

        yield return waitForFixedUpdate; //������ ���

        enemyAttackBoundCollider2D.enabled = false; //Collider2D ������Ʈ ��Ȱ��ȭ
    }

    /* Trigger�� �浹���� �� ����Ǵ� �Լ� */
    private void OnTriggerEnter2D(Collider2D col)
    {
        Player.player.GetDamage(damage); //���� ����
        if (onTriggerEnterAudioSource) onTriggerEnterAudioSource.PlayOneShot(onTriggerEnterAudioSource.clip); //OnTriggerEnter AudioSource ������Ʈ ���
    }
}
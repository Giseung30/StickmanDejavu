using UnityEngine;
using System.Collections;

public class EnemyOnceAttackBound : MonoBehaviour
{
    [Header("Component")]
    public Collider2D enemyAttackBoundCollider2D; //Collider2D 컴포넌트
    public AudioSource onTriggerEnterAudioSource; //OnTriggerEnter 시 재생되는 Audio Source 컴포넌트

    [Header("Variable")]
    public float damage; //피해량

    [Header("Cache")]
    private Coroutine coroutine; //코루틴 함수
    private readonly WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    /* Bound를 활성화하는 함수 */
    public void SetEnableBound()
    {
        if (coroutine != null) //코루틴 함수 실행 시
        {
            StopCoroutine(coroutine); //코루틴 함수 즉시 종료
            enemyAttackBoundCollider2D.enabled = false; //Collider2D 컴포넌트 비활성화
        }
        coroutine = StartCoroutine(TriggerBound());
    }

    /* Bound를 트리거하는 코루틴 함수 */
    private IEnumerator TriggerBound()
    {
        enemyAttackBoundCollider2D.enabled = true; //Collider2D 컴포넌트 활성화

        yield return waitForFixedUpdate; //프레임 대기

        enemyAttackBoundCollider2D.enabled = false; //Collider2D 컴포넌트 비활성화
    }

    /* Trigger와 충돌했을 때 실행되는 함수 */
    private void OnTriggerEnter2D(Collider2D col)
    {
        Player.player.GetDamage(damage); //피해 전달
        if (onTriggerEnterAudioSource) onTriggerEnterAudioSource.PlayOneShot(onTriggerEnterAudioSource.clip); //OnTriggerEnter AudioSource 컴포넌트 재생
    }
}
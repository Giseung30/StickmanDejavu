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
    public AttackBoundType attackBoundType; //Attack Bound 종류
    public bool ultGaugeIncreasing; //궁극기 증가 여부
    public bool playOnStart; //Start 시 실행 여부

    [Header("Component")]
    public Collider2D playerAttackBoundCollider2D; //Collider2D 컴포넌트
    public AudioSource onTriggerEnterAudioSource; //OnTriggerEnter 시 재생되는 Audio Source 컴포넌트
    public GameObject criticalParticleObject; //Critical Particle 오브젝트

    [Header("Variable")]
    public float damage; //피해량
    public float damageMultiplier; //피해량 곱셈기
    public float slow; //이동 속도 감소량
    public float slowMultiplier; //이동 속도 감소량 곱셈기
    public float waitingTime; //대기 시간

    [Header("Cache")]
    private Coroutine coroutine; //코루틴 함수
    private bool onTrigger; //Trigger 여부를 판단하는 변수
    private readonly WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private void Start()
    {
        if (playOnStart) SetEnableAttackBound(true);
    }

    /* Attack Bound를 활성화하는 함수 */
    public void SetEnableAttackBound(bool value)
    {
        if (value) //활성화이면
        {
            switch (attackBoundType)
            {
                case AttackBoundType.Once: //Once
                    coroutine = StartCoroutine(ActivateOnceAttackBound());
                    break;
                case AttackBoundType.Waiting: //Waiting
                    if (coroutine != null) //코루틴 함수 실행 시
                    {
                        StopCoroutine(coroutine); //코루틴 함수 즉시 종료
                        playerAttackBoundCollider2D.enabled = false; //Collider2D 컴포넌트 비활성화
                        onTrigger = false; //Trigger 해제
                    }
                    coroutine = StartCoroutine(ActivateWaitingAttackBound());
                    break;
                case AttackBoundType.Activation: //Activation
                    playerAttackBoundCollider2D.enabled = true; //Collider2D 컴포넌트 활성화
                    break;
            }
        }
        else //비활성화이면
        {
            switch (attackBoundType)
            {
                case AttackBoundType.Once: //Once
                case AttackBoundType.Waiting: //Waiting
                    if (coroutine != null) //코루틴 함수 실행 시
                    {
                        StopCoroutine(coroutine); //코루틴 함수 즉시 종료
                        playerAttackBoundCollider2D.enabled = false; //Collider2D 컴포넌트 비활성화
                        onTrigger = false; //Trigger 해제
                    }
                    break;
                case AttackBoundType.Activation: //Activation
                    playerAttackBoundCollider2D.enabled = false; //Collider2D 컴포넌트 비활성화
                    onTrigger = false; //Trigger 해제
                    break;
            }
        }
    }

    /* Once Attack Bound를 활성화하는 코루틴 함수 */
    private IEnumerator ActivateOnceAttackBound()
    {
        playerAttackBoundCollider2D.enabled = true; //Collider2D 컴포넌트 활성화

        yield return waitForFixedUpdate; //프레임 대기

        playerAttackBoundCollider2D.enabled = false; //Collider2D 컴포넌트 비활성화
        onTrigger = false; //Trigger 해제
    }

    /* Waiting Attack Bound를 활성화하는 코루틴 함수 */
    private IEnumerator ActivateWaitingAttackBound()
    {
        playerAttackBoundCollider2D.enabled = true; //Collider2D 컴포넌트 활성화

        yield return new WaitForSeconds(waitingTime); //대기 시간동안 활성화

        playerAttackBoundCollider2D.enabled = false; //Collider2D 컴포넌트 비활성화
        onTrigger = false; //Trigger 해제
    }

    /* Trigger와 충돌했을 때 실행되는 함수 */
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (attackBoundType == AttackBoundType.Once)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (Utility.GetChance(Player.player.criticalProbability)) //치명타가 발생하면
            { 
                enemy.GetDamage(damage * damageMultiplier * Player.player.criticalDamage, slow * slowMultiplier); //피해 전달
                enemy.InstanciateCriticalParticle(criticalParticleObject); //치명타 효과 생성
            }
            else //치명타가 발생하지 않으면
                enemy.GetDamage(damage * damageMultiplier, slow * slowMultiplier); //피해 전달

            if (ultGaugeIncreasing) UltButton.ultButton.IncreaseUltGauge(1f); //궁극기 게이지 증가
            if (onTriggerEnterAudioSource && !onTrigger) onTriggerEnterAudioSource.PlayOneShot(onTriggerEnterAudioSource.clip); //OnTriggerEnter AudioSource 컴포넌트 재생

            onTrigger = true; //Trigger 실행
        }
    }

    /* Trigger와 충돌 중 일때 실행되는 함수 */
    private void OnTriggerStay2D(Collider2D col)
    {
        if (attackBoundType == AttackBoundType.Waiting || attackBoundType == AttackBoundType.Activation)
        {
            col.GetComponent<Enemy>().GetDamage(damage * damageMultiplier * Time.deltaTime, slow * slowMultiplier * Time.deltaTime); //피해 전달
            if (ultGaugeIncreasing) UltButton.ultButton.IncreaseUltGauge(Time.deltaTime); //궁극기 게이지 증가
        }
    }
}
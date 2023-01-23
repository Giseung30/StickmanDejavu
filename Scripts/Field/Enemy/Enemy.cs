using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Parent")]
    public float currentHP; //현재 체력
    public float maxHP; //최대 체력

    /* 피해를 입는 함수 */
    public virtual void GetDamage(float damage, float slow) { }

    /* Critical Particle을 생성하는 함수 */
    public virtual void InstanciateCriticalParticle(GameObject criticalParticle) { }
}
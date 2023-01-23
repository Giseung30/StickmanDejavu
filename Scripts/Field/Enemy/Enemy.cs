using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Parent")]
    public float currentHP; //���� ü��
    public float maxHP; //�ִ� ü��

    /* ���ظ� �Դ� �Լ� */
    public virtual void GetDamage(float damage, float slow) { }

    /* Critical Particle�� �����ϴ� �Լ� */
    public virtual void InstanciateCriticalParticle(GameObject criticalParticle) { }
}
using UnityEngine;

public class EnemyProjectileAttackBound : MonoBehaviour
{
    [Header("Component")]
    public GameObject enemyAttackBound; //Enemy�� Attack Bound ������Ʈ
    public Rigidbody2D enemyAttackBoundRigidbody2D; //Enemy Attack Bound�� Rigidbody2D ������Ʈ
    public Transform enemyAttackBoundTransform; //Enemy Attack Bound�� Transform ������Ʈ
    public AudioSource onTriggerEnterAudioSource; //OnTriggerEnter �� ����Ǵ� Audio Source ������Ʈ

    [Header("Variable")]
    public float damage; //���ط�
    public float speed; //�ӵ�
    public Vector2 direction; //����
    public float lifeTime; //���� �ð�

    private void Start()
    {
        Destroy(enemyAttackBound, lifeTime);
    }

    private void FixedUpdate()
    {
        enemyAttackBoundRigidbody2D.MovePosition(enemyAttackBoundTransform.position + speed * Time.deltaTime * enemyAttackBoundTransform.TransformDirection(direction));
    }

    /* Trigger�� �浹���� �� ����Ǵ� �Լ� */
    private void OnTriggerEnter2D(Collider2D col)
    {
        Player.player.GetDamage(damage); //���� ����
        if (onTriggerEnterAudioSource) onTriggerEnterAudioSource.PlayOneShot(onTriggerEnterAudioSource.clip); //OnTriggerEnter AudioSource ������Ʈ ���
        Destroy(enemyAttackBound);
    }
}
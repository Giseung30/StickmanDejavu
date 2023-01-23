using UnityEngine;

public class BazookaBullet : MonoBehaviour
{
    [Header("Component")]
    public GameObject bazookaBullet; //Bazooka Bullet ������Ʈ
    public Transform bazookaBulletTransform; //Bazooka Bullet�� Transform ������Ʈ
    public Rigidbody2D bazookaBulletRigidbody2D; //Bazooka Bullet�� Rigidbody2D ������Ʈ
    public Collider2D bazookaBulletCollider2D; //Bazooka Bullet�� Collider2D ������Ʈ
    public SpriteRenderer bazookaBulletSpriteRenderer; //Bazooka Bullet�� SpriteRenderer ������Ʈ
    public ParticleSystem bazookaSmokeParticleSystem; //Bazooka Smoke�� ParticleSystem ������Ʈ
    public GameObject bazookaAttackBound; //Bazooka Attack Bound ������Ʈ

    [Header("Const")]
    private readonly float lifeTime = 3f; //���� �ð�
    private readonly float startSpeed = 2f; //���� �ӵ�
    private readonly float acceleration = 100f; //���ӵ�

    [Header("Cache")]
    private float speed; //�ӵ�
    private bool onTrigger; //Trigger ���θ� �Ǵ��ϴ� ����

    private void Start()
    {
        Destroy(gameObject, lifeTime); //���� �ð� ����

        speed = startSpeed; //���� �ӵ� ����
    }

    private void FixedUpdate()
    {
        SetAcceleration();
    }

    /* ���ӵ��� �����ϴ� �Լ� */
    private void SetAcceleration()
    {
        bazookaBulletRigidbody2D.MovePosition(bazookaBulletRigidbody2D.position + speed * Time.deltaTime * (Vector2)bazookaBulletTransform.right); //��ġ �̵�
        speed += acceleration * Time.deltaTime; //���ӵ� ����
    }

    /* Trigger�� �߻����� �� ����Ǵ� �Լ� */
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (onTrigger) return; //�̹� Ʈ���Ű� �߻������� ����

        bazookaSmokeParticleSystem.Stop(); //Bazooka Smoke ParticleSystem ����
        bazookaBulletSpriteRenderer.enabled = false; //Bazooka Bullet�� SpriteRenderer ��Ȱ��ȭ
        bazookaBulletCollider2D.enabled = false; //Bazooka Bullet�� Collider2D ��Ȱ��ȭ

        GameObject clone = Instantiate(bazookaAttackBound); //Bazooka Attack�� Bound ����
        clone.transform.position = bazookaBulletTransform.position; //Bazooka Attack Bound�� ��ġ ����
        clone.transform.localScale = Vector3.one; //Bazooka Attack Bound�� ũ�� ����
        clone.SetActive(true); //Bazooka Attack Bound Ȱ��ȭ

        onTrigger = true; //Ʈ���� �߻�
    }
}
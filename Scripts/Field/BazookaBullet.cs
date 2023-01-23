using UnityEngine;

public class BazookaBullet : MonoBehaviour
{
    [Header("Component")]
    public GameObject bazookaBullet; //Bazooka Bullet 오브젝트
    public Transform bazookaBulletTransform; //Bazooka Bullet의 Transform 컴포넌트
    public Rigidbody2D bazookaBulletRigidbody2D; //Bazooka Bullet의 Rigidbody2D 컴포넌트
    public Collider2D bazookaBulletCollider2D; //Bazooka Bullet의 Collider2D 컴포넌트
    public SpriteRenderer bazookaBulletSpriteRenderer; //Bazooka Bullet의 SpriteRenderer 컴포넌트
    public ParticleSystem bazookaSmokeParticleSystem; //Bazooka Smoke의 ParticleSystem 컴포넌트
    public GameObject bazookaAttackBound; //Bazooka Attack Bound 오브젝트

    [Header("Const")]
    private readonly float lifeTime = 3f; //생존 시간
    private readonly float startSpeed = 2f; //시작 속도
    private readonly float acceleration = 100f; //가속도

    [Header("Cache")]
    private float speed; //속도
    private bool onTrigger; //Trigger 여부를 판단하는 변수

    private void Start()
    {
        Destroy(gameObject, lifeTime); //생존 시간 설정

        speed = startSpeed; //시작 속도 설정
    }

    private void FixedUpdate()
    {
        SetAcceleration();
    }

    /* 가속도를 설정하는 함수 */
    private void SetAcceleration()
    {
        bazookaBulletRigidbody2D.MovePosition(bazookaBulletRigidbody2D.position + speed * Time.deltaTime * (Vector2)bazookaBulletTransform.right); //위치 이동
        speed += acceleration * Time.deltaTime; //가속도 설정
    }

    /* Trigger가 발생했을 때 실행되는 함수 */
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (onTrigger) return; //이미 트리거가 발생했으면 종료

        bazookaSmokeParticleSystem.Stop(); //Bazooka Smoke ParticleSystem 정지
        bazookaBulletSpriteRenderer.enabled = false; //Bazooka Bullet의 SpriteRenderer 비활성화
        bazookaBulletCollider2D.enabled = false; //Bazooka Bullet의 Collider2D 비활성화

        GameObject clone = Instantiate(bazookaAttackBound); //Bazooka Attack의 Bound 복제
        clone.transform.position = bazookaBulletTransform.position; //Bazooka Attack Bound의 위치 지정
        clone.transform.localScale = Vector3.one; //Bazooka Attack Bound의 크기 지정
        clone.SetActive(true); //Bazooka Attack Bound 활성화

        onTrigger = true; //트리거 발생
    }
}
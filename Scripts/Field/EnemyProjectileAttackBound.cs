using UnityEngine;

public class EnemyProjectileAttackBound : MonoBehaviour
{
    [Header("Component")]
    public GameObject enemyAttackBound; //Enemy의 Attack Bound 오브젝트
    public Rigidbody2D enemyAttackBoundRigidbody2D; //Enemy Attack Bound의 Rigidbody2D 컴포넌트
    public Transform enemyAttackBoundTransform; //Enemy Attack Bound의 Transform 컴포넌트
    public AudioSource onTriggerEnterAudioSource; //OnTriggerEnter 시 재생되는 Audio Source 컴포넌트

    [Header("Variable")]
    public float damage; //피해량
    public float speed; //속도
    public Vector2 direction; //방향
    public float lifeTime; //생존 시간

    private void Start()
    {
        Destroy(enemyAttackBound, lifeTime);
    }

    private void FixedUpdate()
    {
        enemyAttackBoundRigidbody2D.MovePosition(enemyAttackBoundTransform.position + speed * Time.deltaTime * enemyAttackBoundTransform.TransformDirection(direction));
    }

    /* Trigger와 충돌했을 때 실행되는 함수 */
    private void OnTriggerEnter2D(Collider2D col)
    {
        Player.player.GetDamage(damage); //피해 전달
        if (onTriggerEnterAudioSource) onTriggerEnterAudioSource.PlayOneShot(onTriggerEnterAudioSource.clip); //OnTriggerEnter AudioSource 컴포넌트 재생
        Destroy(enemyAttackBound);
    }
}
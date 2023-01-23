using UnityEngine;
using System.Collections;

/* 쌍 구조체 */
public struct Pair<T1, T2>
{
    public T1 first;
    public T2 second;
    public Pair(T1 first, T2 second)
    {
        this.first = first;
        this.second = second;
    }
}

public class Utility : MonoBehaviour
{
    public static Utility utility; //전역 참조 변수

    private void Awake()
    {
        utility = this;
    }

    /* StartCoroutine 함수를 불러오는 함수 */
    public void LoadStartCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    /* ParticleSystem을 World Space에 복사하는 함수 */
    public static void InstantiateParticleSystemOnWorldSpace(GameObject particleSystem)
    {
        GameObject particleClone = Instantiate(particleSystem); //파티클 복사
        Transform effectCloneTransform = particleClone.GetComponent<Transform>(); //파티클의 Transform 컴포넌트
        effectCloneTransform.localScale = particleSystem.transform.lossyScale; //파티클 크기 정의
        effectCloneTransform.SetPositionAndRotation(particleSystem.transform.position, particleSystem.transform.rotation); //파티클 위치 및 회전 정의
        particleClone.SetActive(true); //파티클 활성화
    }

    /* ParticleSystem을 World Space에 복사하는 함수 */
    public static void InstantiateParticleSystemOnWorldSpace(GameObject particleSystem, Vector3 position, Vector3 lossyScale)
    {
        GameObject particleClone = Instantiate(particleSystem); //파티클 복사
        Transform effectCloneTransform = particleClone.GetComponent<Transform>(); //파티클의 Transform 컴포넌트
        effectCloneTransform.localScale = lossyScale; //파티클 크기 정의
        effectCloneTransform.SetPositionAndRotation(position, particleSystem.transform.rotation); //파티클 위치 및 회전 정의
        particleClone.SetActive(true); //파티클 활성화
    }

    /* 기회를 얻는 함수 */
    public static bool GetChance(float percent)
    {
        if (percent >= 100f) return true;
        return (Random.value * 100f) < percent;
    }
}
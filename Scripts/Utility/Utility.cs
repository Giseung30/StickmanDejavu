using UnityEngine;
using System.Collections;

/* �� ����ü */
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
    public static Utility utility; //���� ���� ����

    private void Awake()
    {
        utility = this;
    }

    /* StartCoroutine �Լ��� �ҷ����� �Լ� */
    public void LoadStartCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    /* ParticleSystem�� World Space�� �����ϴ� �Լ� */
    public static void InstantiateParticleSystemOnWorldSpace(GameObject particleSystem)
    {
        GameObject particleClone = Instantiate(particleSystem); //��ƼŬ ����
        Transform effectCloneTransform = particleClone.GetComponent<Transform>(); //��ƼŬ�� Transform ������Ʈ
        effectCloneTransform.localScale = particleSystem.transform.lossyScale; //��ƼŬ ũ�� ����
        effectCloneTransform.SetPositionAndRotation(particleSystem.transform.position, particleSystem.transform.rotation); //��ƼŬ ��ġ �� ȸ�� ����
        particleClone.SetActive(true); //��ƼŬ Ȱ��ȭ
    }

    /* ParticleSystem�� World Space�� �����ϴ� �Լ� */
    public static void InstantiateParticleSystemOnWorldSpace(GameObject particleSystem, Vector3 position, Vector3 lossyScale)
    {
        GameObject particleClone = Instantiate(particleSystem); //��ƼŬ ����
        Transform effectCloneTransform = particleClone.GetComponent<Transform>(); //��ƼŬ�� Transform ������Ʈ
        effectCloneTransform.localScale = lossyScale; //��ƼŬ ũ�� ����
        effectCloneTransform.SetPositionAndRotation(position, particleSystem.transform.rotation); //��ƼŬ ��ġ �� ȸ�� ����
        particleClone.SetActive(true); //��ƼŬ Ȱ��ȭ
    }

    /* ��ȸ�� ��� �Լ� */
    public static bool GetChance(float percent)
    {
        if (percent >= 100f) return true;
        return (Random.value * 100f) < percent;
    }
}
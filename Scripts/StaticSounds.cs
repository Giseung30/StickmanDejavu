using UnityEngine;

public class StaticSounds : MonoBehaviour
{
    [Header("Static")]
    public static StaticSounds staticSounds; //���� ���� ����

    [Header("Audio Source")]
    public AudioSource[] audioSources; //AudioSource ������Ʈ��

    private void Awake()
    {
        if (!staticSounds) staticSounds = this;
    }

    /* Audio Source�� ����ϴ� �Լ� */
    public void PlayAudioSource(int index)
    {
        audioSources[index].Play();
    }
}
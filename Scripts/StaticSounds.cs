using UnityEngine;

public class StaticSounds : MonoBehaviour
{
    [Header("Static")]
    public static StaticSounds staticSounds; //전역 참조 변수

    [Header("Audio Source")]
    public AudioSource[] audioSources; //AudioSource 컴포넌트들

    private void Awake()
    {
        if (!staticSounds) staticSounds = this;
    }

    /* Audio Source를 재생하는 함수 */
    public void PlayAudioSource(int index)
    {
        audioSources[index].Play();
    }
}
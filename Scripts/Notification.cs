using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Notification : MonoBehaviour
{
    [Header("Static")]
    public static Notification notification; //전역 참조 변수

    [Header("Component")]
    public RectTransform notificationRectTransform; //Notification의 RectTransform 컴포넌트
    public TextMeshProUGUI contentTMP; //Content의 TMP 컴포넌트

    [Header("Cache")]
    private Queue<Pair<string, float>> contentQueue = new Queue<Pair<string, float>>(); //표시할 내용을 담는 Queue

    private void Awake()
    {
        if (!notification) notification = this;
    }

    private void Start()
    {
        StartCoroutine(ControlNotification());
    }

    /* Notification을 설정하는 함수 */
    public void SetNotification(string content, float displayTime)
    {
        contentQueue.Enqueue(new Pair<string, float>(content, displayTime));
    }

    /* Notification을 관리하는 코루틴 함수 */
    private IEnumerator ControlNotification()
    {
        while (true)
        {
            while (contentQueue.Count == 0) yield return null; //알림이 존재할 때 까지 대기
            yield return StartCoroutine(DisplayNotification(contentQueue.Dequeue())); //표시 코루틴 종료까지 대기
        }
    }

    /* Notification을 표시하는 코루틴 함수 */
    private IEnumerator DisplayNotification(Pair<string, float> content)
    {
        float displaySpeed = 2f; //나타나는 속도
        float displayTime = content.second; //표시 시간

        contentTMP.text = content.first; //알림 내용 지정

        notificationRectTransform.anchoredPosition = new Vector2(notificationRectTransform.anchoredPosition.x, notificationRectTransform.sizeDelta.y * 0.5f); //위치 초기화

        while (notificationRectTransform.anchoredPosition.y - notificationRectTransform.sizeDelta.y * Time.smoothDeltaTime * displaySpeed > -notificationRectTransform.sizeDelta.y * 0.5f) //높이가 낮아지지 않으면
        {
            notificationRectTransform.anchoredPosition -= new Vector2(0f, notificationRectTransform.sizeDelta.y * Time.smoothDeltaTime * displaySpeed); //y축 감소
            yield return null;
        }

        notificationRectTransform.anchoredPosition = new Vector2(notificationRectTransform.anchoredPosition.x, -notificationRectTransform.sizeDelta.y * 0.5f); //위치 보간

        yield return new WaitForSeconds(displayTime); //대기

        while (notificationRectTransform.anchoredPosition.y + notificationRectTransform.sizeDelta.y * Time.smoothDeltaTime * displaySpeed < notificationRectTransform.sizeDelta.y * 0.5f) //높이가 높아지지 않으면
        {
            notificationRectTransform.anchoredPosition += new Vector2(0f, notificationRectTransform.sizeDelta.y * Time.smoothDeltaTime * displaySpeed); //y축 증가
            yield return null;
        }

        notificationRectTransform.anchoredPosition = new Vector2(notificationRectTransform.anchoredPosition.x, notificationRectTransform.sizeDelta.y * 0.5f); //위치 보간
    }
}
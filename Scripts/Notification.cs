using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Notification : MonoBehaviour
{
    [Header("Static")]
    public static Notification notification; //���� ���� ����

    [Header("Component")]
    public RectTransform notificationRectTransform; //Notification�� RectTransform ������Ʈ
    public TextMeshProUGUI contentTMP; //Content�� TMP ������Ʈ

    [Header("Cache")]
    private Queue<Pair<string, float>> contentQueue = new Queue<Pair<string, float>>(); //ǥ���� ������ ��� Queue

    private void Awake()
    {
        if (!notification) notification = this;
    }

    private void Start()
    {
        StartCoroutine(ControlNotification());
    }

    /* Notification�� �����ϴ� �Լ� */
    public void SetNotification(string content, float displayTime)
    {
        contentQueue.Enqueue(new Pair<string, float>(content, displayTime));
    }

    /* Notification�� �����ϴ� �ڷ�ƾ �Լ� */
    private IEnumerator ControlNotification()
    {
        while (true)
        {
            while (contentQueue.Count == 0) yield return null; //�˸��� ������ �� ���� ���
            yield return StartCoroutine(DisplayNotification(contentQueue.Dequeue())); //ǥ�� �ڷ�ƾ ������� ���
        }
    }

    /* Notification�� ǥ���ϴ� �ڷ�ƾ �Լ� */
    private IEnumerator DisplayNotification(Pair<string, float> content)
    {
        float displaySpeed = 2f; //��Ÿ���� �ӵ�
        float displayTime = content.second; //ǥ�� �ð�

        contentTMP.text = content.first; //�˸� ���� ����

        notificationRectTransform.anchoredPosition = new Vector2(notificationRectTransform.anchoredPosition.x, notificationRectTransform.sizeDelta.y * 0.5f); //��ġ �ʱ�ȭ

        while (notificationRectTransform.anchoredPosition.y - notificationRectTransform.sizeDelta.y * Time.smoothDeltaTime * displaySpeed > -notificationRectTransform.sizeDelta.y * 0.5f) //���̰� �������� ������
        {
            notificationRectTransform.anchoredPosition -= new Vector2(0f, notificationRectTransform.sizeDelta.y * Time.smoothDeltaTime * displaySpeed); //y�� ����
            yield return null;
        }

        notificationRectTransform.anchoredPosition = new Vector2(notificationRectTransform.anchoredPosition.x, -notificationRectTransform.sizeDelta.y * 0.5f); //��ġ ����

        yield return new WaitForSeconds(displayTime); //���

        while (notificationRectTransform.anchoredPosition.y + notificationRectTransform.sizeDelta.y * Time.smoothDeltaTime * displaySpeed < notificationRectTransform.sizeDelta.y * 0.5f) //���̰� �������� ������
        {
            notificationRectTransform.anchoredPosition += new Vector2(0f, notificationRectTransform.sizeDelta.y * Time.smoothDeltaTime * displaySpeed); //y�� ����
            yield return null;
        }

        notificationRectTransform.anchoredPosition = new Vector2(notificationRectTransform.anchoredPosition.x, notificationRectTransform.sizeDelta.y * 0.5f); //��ġ ����
    }
}
using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{
    [Header("Static")]
    public static MainCamera mainCamera; //���� ���� ����

    [Header("Component")]
    public Camera mainCameraCamera; //MainCamera�� Camera ������Ʈ
    public Transform mainCameraTransform; //MainCamera�� Transform ������Ʈ
    public Transform pushLeftTransform; //PushLeft�� Transform ������Ʈ
    public Transform pushRightTransform; //PushRight�� Transform ������Ʈ
    public Transform edgeLeftTransform; //EdgeLeft�� Transform ������Ʈ
    public Transform edgeRightTransform; //EdgeRight�� Transform ������Ʈ

    private void Awake()
    {
        mainCamera = this;

        edgeLeftTransform.position = new Vector3(mainCameraTransform.position.x - mainCameraCamera.orthographicSize * Definition.deviceWidth / Definition.deviceHeight + 1f, edgeLeftTransform.position.y, edgeLeftTransform.position.z); //EdgeLeft ��ġ �ʱ�ȭ
        edgeRightTransform.position = new Vector3(mainCameraTransform.position.x + mainCameraCamera.orthographicSize * Definition.deviceWidth / Definition.deviceHeight - 1f, edgeRightTransform.position.y, edgeRightTransform.position.z); //EdgeRight ��ġ �ʱ�ȭ
    }

    private void Update()
    {
        PushMainCamera();
    }

    /* MainCamera�� �о�� �Լ� */
    private void PushMainCamera()
    {
        if (Definition.moveXRange.y - Definition.moveXRange.x <= edgeRightTransform.position.x - edgeLeftTransform.position.x || !Player.player) return; //���� ��ü�� ���̰ų� Player�� �������� ������ ����

        Vector3 resultPosition = mainCameraTransform.position; //���� ��ġ

        if (Player.player.playerTransform.position.x < pushLeftTransform.position.x) //Player�� PushLeft���� ���ʿ� ������
            resultPosition += new Vector3(Player.player.playerTransform.position.x - pushLeftTransform.position.x, 0f, 0f); //ī�޶� ��ġ �������� �̵�
        else if (pushRightTransform.position.x < Player.player.playerTransform.position.x) //Player�� PushRight���� �����ʿ� ������
            resultPosition += new Vector3(Player.player.playerTransform.position.x - pushRightTransform.position.x, 0f, 0f); //ī�޶� ��ġ ���������� �̵�

        if ((edgeLeftTransform.position.x - mainCameraTransform.position.x) + resultPosition.x < Definition.moveXRange.x) //EdgeLeft�� ���� �����
            resultPosition = new Vector3(Definition.moveXRange.x + (mainCameraTransform.position.x - edgeLeftTransform.position.x), resultPosition.y, resultPosition.z); //X�� ���� ����
        else if (Definition.moveXRange.y < (edgeRightTransform.position.x - mainCameraTransform.position.x) + resultPosition.x) //EdgeRight�� ���� �����
            resultPosition = new Vector3(Definition.moveXRange.y + (mainCameraTransform.position.x - edgeRightTransform.position.x), resultPosition.y, resultPosition.z); //X�� ���� ����

        mainCameraTransform.position = resultPosition; //���� ��ġ ����
    }
}
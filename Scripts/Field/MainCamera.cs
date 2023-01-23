using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{
    [Header("Static")]
    public static MainCamera mainCamera; //전역 참조 변수

    [Header("Component")]
    public Camera mainCameraCamera; //MainCamera의 Camera 컴포넌트
    public Transform mainCameraTransform; //MainCamera의 Transform 컴포넌트
    public Transform pushLeftTransform; //PushLeft의 Transform 컴포넌트
    public Transform pushRightTransform; //PushRight의 Transform 컴포넌트
    public Transform edgeLeftTransform; //EdgeLeft의 Transform 컴포넌트
    public Transform edgeRightTransform; //EdgeRight의 Transform 컴포넌트

    private void Awake()
    {
        mainCamera = this;

        edgeLeftTransform.position = new Vector3(mainCameraTransform.position.x - mainCameraCamera.orthographicSize * Definition.deviceWidth / Definition.deviceHeight + 1f, edgeLeftTransform.position.y, edgeLeftTransform.position.z); //EdgeLeft 위치 초기화
        edgeRightTransform.position = new Vector3(mainCameraTransform.position.x + mainCameraCamera.orthographicSize * Definition.deviceWidth / Definition.deviceHeight - 1f, edgeRightTransform.position.y, edgeRightTransform.position.z); //EdgeRight 위치 초기화
    }

    private void Update()
    {
        PushMainCamera();
    }

    /* MainCamera를 밀어내는 함수 */
    private void PushMainCamera()
    {
        if (Definition.moveXRange.y - Definition.moveXRange.x <= edgeRightTransform.position.x - edgeLeftTransform.position.x || !Player.player) return; //맵의 전체가 보이거나 Player가 존재하지 않으면 종료

        Vector3 resultPosition = mainCameraTransform.position; //최종 위치

        if (Player.player.playerTransform.position.x < pushLeftTransform.position.x) //Player가 PushLeft보다 왼쪽에 있으면
            resultPosition += new Vector3(Player.player.playerTransform.position.x - pushLeftTransform.position.x, 0f, 0f); //카메라 위치 왼쪽으로 이동
        else if (pushRightTransform.position.x < Player.player.playerTransform.position.x) //Player가 PushRight보다 오른쪽에 있으면
            resultPosition += new Vector3(Player.player.playerTransform.position.x - pushRightTransform.position.x, 0f, 0f); //카메라 위치 오른쪽으로 이동

        if ((edgeLeftTransform.position.x - mainCameraTransform.position.x) + resultPosition.x < Definition.moveXRange.x) //EdgeLeft가 맵을 벗어나면
            resultPosition = new Vector3(Definition.moveXRange.x + (mainCameraTransform.position.x - edgeLeftTransform.position.x), resultPosition.y, resultPosition.z); //X축 범위 제한
        else if (Definition.moveXRange.y < (edgeRightTransform.position.x - mainCameraTransform.position.x) + resultPosition.x) //EdgeRight가 맵을 벗어나면
            resultPosition = new Vector3(Definition.moveXRange.y + (mainCameraTransform.position.x - edgeRightTransform.position.x), resultPosition.y, resultPosition.z); //X축 범위 제한

        mainCameraTransform.position = resultPosition; //최종 위치 적용
    }
}
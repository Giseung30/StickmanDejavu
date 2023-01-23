using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;

public class SplashManager : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60; //프레임 초기화
        SetResolution(1280, 720);

        PlayGamesPlatform.DebugLogEnabled = true; //구글 플레이 디버깅 활성화
        PlayGamesPlatform.Activate(); //구글 플레이 활성화
    }

    /* 해상도 설정하는 함수 */
    private void SetResolution(int setWidth, int setHeight)
    {
        int pixelCount = setWidth * setHeight; //픽셀의 개수
        Screen.SetResolution((int)Mathf.Sqrt((float)pixelCount * Definition.deviceWidth / Definition.deviceHeight), (int)Mathf.Sqrt((float)pixelCount * Definition.deviceHeight / Definition.deviceWidth), true); //해상도 설정
    }

    /* Scene을 변경하는 함수 */
    public void ChangeScene()
    {
        SceneManager.LoadScene((int)Scene.Verification);
    }
}
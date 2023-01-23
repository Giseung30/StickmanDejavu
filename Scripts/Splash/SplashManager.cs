using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;

public class SplashManager : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60; //������ �ʱ�ȭ
        SetResolution(1280, 720);

        PlayGamesPlatform.DebugLogEnabled = true; //���� �÷��� ����� Ȱ��ȭ
        PlayGamesPlatform.Activate(); //���� �÷��� Ȱ��ȭ
    }

    /* �ػ� �����ϴ� �Լ� */
    private void SetResolution(int setWidth, int setHeight)
    {
        int pixelCount = setWidth * setHeight; //�ȼ��� ����
        Screen.SetResolution((int)Mathf.Sqrt((float)pixelCount * Definition.deviceWidth / Definition.deviceHeight), (int)Mathf.Sqrt((float)pixelCount * Definition.deviceHeight / Definition.deviceWidth), true); //�ػ� ����
    }

    /* Scene�� �����ϴ� �Լ� */
    public void ChangeScene()
    {
        SceneManager.LoadScene((int)Scene.Verification);
    }
}
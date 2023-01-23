using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoginManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit(); //�ڷ� ���� ��ư�� ������ ����
    }

    /* ���� �α��� ��ư�� Ŭ���ϸ� ����Ǵ� �Լ� */
    public void OnClickGoogleLoginButton()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            StartCoroutine(ProcessAuthenticate(success));
        });
    }

    /* ������ ó���ϴ� �ڷ�ƾ �Լ� */
    private IEnumerator ProcessAuthenticate(bool success)
    {
        if (success) //�α��ο� �����ϸ�
        {
            LoginInfo.userID = Social.localUser.id; //UserID ����
            LoginInfo.userName = Social.localUser.userName; //UserName ����
            Notification.notification.SetNotification(Social.localUser.userName + "�� ȯ���մϴ�", 2f);

            yield return StartCoroutine(NetworkManager.networkManager.GetAchievementInfo(Social.localUser.id)); //���� �ε�
            if (NetworkManager.getAchievementInfoResult == NetworkResult.Error) Notification.notification.SetNotification("������ �ҷ����� ���� ������ �߻��߽��ϴ�", 3f);

            
            SceneManager.LoadScene((int)Scene.Main); //Scene ��ȯ
        }
        else //�α��ο� �����ϸ�
        {
            Notification.notification.SetNotification("���� ���� �α��ο� �����߽��ϴ�", 3f);
        }
    }

    /* �Խ�Ʈ �α��� ��ư�� Ŭ���ϸ� ����Ǵ� �Լ� */
    public void OnClickGuestLoginButton()
    {
        LoginInfo.userID = string.Empty; //UserID ����
        AchievementManager.ResetAchievementInfo(); //���� �ʱ�ȭ
        Notification.notification.SetNotification("�Խ�Ʈ �α����� ���� ������ ������� �ʽ��ϴ�", 2f);
        SceneManager.LoadScene((int)Scene.Main); //Scene ��ȯ
    }

    /* ���� Sound�� ����ϴ� �Լ� */
    public void PlayStaticSounds(int index)
    {
        StaticSounds.staticSounds.PlayAudioSource(index);
    }
}
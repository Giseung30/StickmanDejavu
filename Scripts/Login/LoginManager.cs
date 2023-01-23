using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoginManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit(); //뒤로 가기 버튼을 누르면 종료
    }

    /* 구글 로그인 버튼을 클릭하면 실행되는 함수 */
    public void OnClickGoogleLoginButton()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            StartCoroutine(ProcessAuthenticate(success));
        });
    }

    /* 증명을 처리하는 코루틴 함수 */
    private IEnumerator ProcessAuthenticate(bool success)
    {
        if (success) //로그인에 성공하면
        {
            LoginInfo.userID = Social.localUser.id; //UserID 지정
            LoginInfo.userName = Social.localUser.userName; //UserName 지정
            Notification.notification.SetNotification(Social.localUser.userName + "님 환영합니다", 2f);

            yield return StartCoroutine(NetworkManager.networkManager.GetAchievementInfo(Social.localUser.id)); //업적 로드
            if (NetworkManager.getAchievementInfoResult == NetworkResult.Error) Notification.notification.SetNotification("업적을 불러오는 도중 문제가 발생했습니다", 3f);

            
            SceneManager.LoadScene((int)Scene.Main); //Scene 전환
        }
        else //로그인에 실패하면
        {
            Notification.notification.SetNotification("구글 계정 로그인에 실패했습니다", 3f);
        }
    }

    /* 게스트 로그인 버튼을 클릭하면 실행되는 함수 */
    public void OnClickGuestLoginButton()
    {
        LoginInfo.userID = string.Empty; //UserID 지정
        AchievementManager.ResetAchievementInfo(); //업적 초기화
        Notification.notification.SetNotification("게스트 로그인은 진행 사항이 저장되지 않습니다", 2f);
        SceneManager.LoadScene((int)Scene.Main); //Scene 전환
    }

    /* 전역 Sound를 재생하는 함수 */
    public void PlayStaticSounds(int index)
    {
        StaticSounds.staticSounds.PlayAudioSource(index);
    }
}
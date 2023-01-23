using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class VerificationManager : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI versionTMP; //Version의 TMP 컴포넌트
    public Canvas mainCanvasCanvas; //MainCanvas의 Canvas 컴포넌트
    public Canvas exitCanvasCanvas; //ExitCanvas의 Canvas 컴포넌트
    public TextMeshProUGUI mainLineTextTMP; //MainCanvas > Line > Text의 TMP 컴포넌트
    public TextMeshProUGUI exitCanvasTextTMP; //ExitCanvas > Background > Text의 TMP 컴포넌트

    private void Start()
    {
        versionTMP.text = Application.version;
        StartCoroutine(Verify());
    }

    /* 증명하는 코루틴 함수 */
    private IEnumerator Verify()
    {
        mainLineTextTMP.text = "버전 확인 중..";
        yield return StartCoroutine(NetworkManager.networkManager.GetPostVerision()); //게시 버전 로드
        if (NetworkManager.getPostVersionResult == NetworkResult.Error) { //오류가 발생하면
            EnableExitCanvas("버전을 확인할 수 없습니다");
            yield break;
        }
        else //오류가 발생하지 않으면
        {
            if(!VerifyVersion(VerificationInfo.postVersion, Application.version)) //버전이 증명되지 않으면
            {
                EnableExitCanvas("현재 버전은 최신 버전이 아닙니다\n현재 버전: " + Application.version + "\n최신 버전: " + VerificationInfo.postVersion);
                yield break;
            }
        }

        SceneManager.LoadScene((int)Scene.Login); //Scene 전환
    }


    /* 버전을 증명하는 함수 */
    private bool VerifyVersion(string postVersion, string version)
    {
        string[] splitedPostVersion = postVersion.Split('.'); //게시 버전 분리
        string[] splitedVersion = version.Split('.'); //버전 분리

        if (int.Parse(splitedPostVersion[0]) > int.Parse(splitedVersion[0])) return false; //첫 번째 문자 비교 (게시 버전이 더 높으면)
        else if (int.Parse(splitedPostVersion[0]) < int.Parse(splitedVersion[0])) return true; //첫 번째 문자 비교 (게시 버전이 더 낮으면)
        else //첫 번째 문자 비교 (동일하면)
        {
            if (int.Parse(splitedPostVersion[1]) > int.Parse(splitedVersion[1])) return false; //두 번째 문자 비교 (게시 버전이 더 높으면)
            else if (int.Parse(splitedPostVersion[1]) < int.Parse(splitedVersion[1])) return true; //두 번째 문자 비교 (게시 버전이 더 낮으면)
            else //두 번째 문자 비교 (동일하면)
            {
                if (int.Parse(splitedPostVersion[2]) > int.Parse(splitedVersion[2])) return false; //세 번째 문자 비교 (게시 버전이 더 높으면)
                else return true; //세 번째 문자 비교 (게시 버전이 더 낮거나 동일하면)
            }
        }
    }

    #region Exit Canvas
    /* Exit Canvas를 활성화하는 함수 */
    private void EnableExitCanvas(string context)
    {
        exitCanvasTextTMP.text = context; //내용 삽입

        mainCanvasCanvas.enabled = false; //Main Canvas 비활성화
        exitCanvasCanvas.enabled = true; //Exit Canvas 활성화
    }

    /* Exit Button을 클릭하면 실행되는 함수 */
    public void OnClickExitButton()
    {
        Application.Quit();
    }
    #endregion

    /* 전역 Sound를 재생하는 함수 */
    public void PlayStaticSounds(int index)
    {
        StaticSounds.staticSounds.PlayAudioSource(index);
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class VerificationManager : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI versionTMP; //Version�� TMP ������Ʈ
    public Canvas mainCanvasCanvas; //MainCanvas�� Canvas ������Ʈ
    public Canvas exitCanvasCanvas; //ExitCanvas�� Canvas ������Ʈ
    public TextMeshProUGUI mainLineTextTMP; //MainCanvas > Line > Text�� TMP ������Ʈ
    public TextMeshProUGUI exitCanvasTextTMP; //ExitCanvas > Background > Text�� TMP ������Ʈ

    private void Start()
    {
        versionTMP.text = Application.version;
        StartCoroutine(Verify());
    }

    /* �����ϴ� �ڷ�ƾ �Լ� */
    private IEnumerator Verify()
    {
        mainLineTextTMP.text = "���� Ȯ�� ��..";
        yield return StartCoroutine(NetworkManager.networkManager.GetPostVerision()); //�Խ� ���� �ε�
        if (NetworkManager.getPostVersionResult == NetworkResult.Error) { //������ �߻��ϸ�
            EnableExitCanvas("������ Ȯ���� �� �����ϴ�");
            yield break;
        }
        else //������ �߻����� ������
        {
            if(!VerifyVersion(VerificationInfo.postVersion, Application.version)) //������ ������� ������
            {
                EnableExitCanvas("���� ������ �ֽ� ������ �ƴմϴ�\n���� ����: " + Application.version + "\n�ֽ� ����: " + VerificationInfo.postVersion);
                yield break;
            }
        }

        SceneManager.LoadScene((int)Scene.Login); //Scene ��ȯ
    }


    /* ������ �����ϴ� �Լ� */
    private bool VerifyVersion(string postVersion, string version)
    {
        string[] splitedPostVersion = postVersion.Split('.'); //�Խ� ���� �и�
        string[] splitedVersion = version.Split('.'); //���� �и�

        if (int.Parse(splitedPostVersion[0]) > int.Parse(splitedVersion[0])) return false; //ù ��° ���� �� (�Խ� ������ �� ������)
        else if (int.Parse(splitedPostVersion[0]) < int.Parse(splitedVersion[0])) return true; //ù ��° ���� �� (�Խ� ������ �� ������)
        else //ù ��° ���� �� (�����ϸ�)
        {
            if (int.Parse(splitedPostVersion[1]) > int.Parse(splitedVersion[1])) return false; //�� ��° ���� �� (�Խ� ������ �� ������)
            else if (int.Parse(splitedPostVersion[1]) < int.Parse(splitedVersion[1])) return true; //�� ��° ���� �� (�Խ� ������ �� ������)
            else //�� ��° ���� �� (�����ϸ�)
            {
                if (int.Parse(splitedPostVersion[2]) > int.Parse(splitedVersion[2])) return false; //�� ��° ���� �� (�Խ� ������ �� ������)
                else return true; //�� ��° ���� �� (�Խ� ������ �� ���ų� �����ϸ�)
            }
        }
    }

    #region Exit Canvas
    /* Exit Canvas�� Ȱ��ȭ�ϴ� �Լ� */
    private void EnableExitCanvas(string context)
    {
        exitCanvasTextTMP.text = context; //���� ����

        mainCanvasCanvas.enabled = false; //Main Canvas ��Ȱ��ȭ
        exitCanvasCanvas.enabled = true; //Exit Canvas Ȱ��ȭ
    }

    /* Exit Button�� Ŭ���ϸ� ����Ǵ� �Լ� */
    public void OnClickExitButton()
    {
        Application.Quit();
    }
    #endregion

    /* ���� Sound�� ����ϴ� �Լ� */
    public void PlayStaticSounds(int index)
    {
        StaticSounds.staticSounds.PlayAudioSource(index);
    }
}
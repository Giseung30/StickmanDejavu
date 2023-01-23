using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class WaitingManager : MonoBehaviour
{
    [Header("Background")]
    public Image backgroundImage; //Background�� Image ������Ʈ
    public Sprite[] backgroundSprite; //��� Sprite��

    [Header("Waiting")]
    public TextMeshProUGUI stageTMP; //Stage�� TMP ������Ʈ

    [Header("Selected Weapons")]
    public GameObject[] firstWeapons; //ù ��° ���� ������Ʈ��
    public GameObject[] secondWeapons; //�� ��° ���� ������Ʈ��

    [Header("Infos")]
    public TextMeshProUGUI moneyTMP; //Money�� TMP ������Ʈ
    public TextMeshProUGUI profitTMP; //Profit�� TMP ������Ʈ
    public TextMeshProUGUI killTMP; //kill�� TMP ������Ʈ

    [Header("Cache")]
    private Weapon currentFirstSelectedWeapon; //���� ù ��° ���õ� ����
    private Weapon currentSecondSelectedWeapon; //���� �� ��° ���õ� ����
    private Weapon preFirstSelectedWeapon = Weapon.None; //���� ù ��° ���õ� ����
    private Weapon preSecondSelectedWeapon = Weapon.None; //���� �� ��° ���õ� ����
    private int preMoney = -1; //���� Money
    private int preProfit = -1; //���� Profit
    private int preKill = -1; //���� Kill

    private void Start()
    {
        backgroundImage.sprite = backgroundSprite[((WaitingInfo.stage - 1) - (WaitingInfo.stage - 1) % 10) / 10 % 3]; //��� ����
        stageTMP.text = string.Format("{0:000}", WaitingInfo.stage); //�ܰ� ����
    }

    private void Update()
    {
        SyncSelectedWeapons();
        SyncInfos();
        SaveAuto();
    }

    /* WaitingInfo�� �ʱ�ȭ�ϴ� �Լ� */
    public static void ResetWaitingInfo()
    {
        WaitingInfo.stage = 1;
    }

    #region Sync
    /* ���õ� ������� ����ȭ�ϴ� �Լ� */
    private void SyncSelectedWeapons()
    {
        currentFirstSelectedWeapon = ShopInfo.firstSelectedWeapon; //���� ù ��° ���õ� ���� ����
        currentSecondSelectedWeapon = ShopInfo.secondSelectedWeapon;  //���� �� ��° ���õ� ���� ����

        if (preFirstSelectedWeapon != currentFirstSelectedWeapon || preSecondSelectedWeapon != currentSecondSelectedWeapon) //���� ���õ� ����� ���� ���õ� ���Ⱑ �ٸ���
        {
            for (int i = 0; i < firstWeapons.Length; ++i) firstWeapons[i].SetActive(false); //��� Weapon ��Ȱ��ȭ
            for (int i = 0; i < secondWeapons.Length; ++i) secondWeapons[i].SetActive(false);

            firstWeapons[(int)currentFirstSelectedWeapon].SetActive(true); //���õ� Weapon Ȱ��ȭ
            secondWeapons[(int)currentSecondSelectedWeapon].SetActive(true);

            preFirstSelectedWeapon = currentFirstSelectedWeapon; //���� ù ��° ���õ� ���� ����
            preSecondSelectedWeapon = currentSecondSelectedWeapon; //���� �� ��° ���õ� ���� ����
        }
    }

    /* Infos ���� ��ġ�� ����ȭ�ϴ� �Լ� */
    private void SyncInfos()
    {
        if (preMoney != ShopInfo.money)
        {
            moneyTMP.text = string.Format("{0:#,0}", ShopInfo.money);
            preMoney = ShopInfo.money;
        }
        if (preProfit != ShopInfo.profit)
        {
            profitTMP.text = string.Format("{0:#,0}", ShopInfo.profit);
            preProfit = ShopInfo.profit;
        }
        if (preKill != ShopInfo.kill)
        {
            killTMP.text = string.Format("{0:#,0}", ShopInfo.kill);
            preKill = ShopInfo.kill;
        }
    }
    #endregion

    #region Event Buttons
    /* Save Button�� Ŭ������ �� ����Ǵ� �Լ� */
    public void OnClickSaveButton()
    {
        StartCoroutine(SaveGame());
    }

    /* ������ �����ϴ� �ڷ�ƾ �Լ� */
    private IEnumerator SaveGame()
    {
        if (string.IsNullOrEmpty(LoginInfo.userID)) //�Խ�Ʈ �α����̸�
        {
            Notification.notification.SetNotification("�Խ�Ʈ �α����� ���� ����� �������� �ʽ��ϴ�", 2f);
        }
        else //���� ���� �α����̸�
        {
            yield return StartCoroutine(NetworkManager.networkManager.SetGameInfo(LoginInfo.userID, LoginInfo.userName)); //���� ���� ����
            yield return StartCoroutine(NetworkManager.networkManager.SetAchievementInfo(LoginInfo.userID, LoginInfo.userName)); //���� ���� ����

            if (NetworkManager.setGameInfoResult == NetworkResult.NoError) //������ �߻����� ������
            {
                Notification.notification.SetNotification("���� ������ ���������� <color=#73AF87>����</color>�Ǿ����ϴ�", 2f);
            }
            else //������ �߻��ϸ�
            {
                Notification.notification.SetNotification("���� ������ �����ϴ� ���� ������ �߻��߽��ϴ�", 3f);
            }
        }
    }

    /* Battle Button�� Ŭ������ �� ����Ǵ� �Լ� */
    public void OnClickBattleButton()
    {
        SceneManager.LoadScene((int)Scene.Field);
    }

    /* Main Button�� Ŭ������ �� ����Ǵ� �Լ� */
    public void OnClickMainButton()
    {
        SceneManager.LoadScene((int)Scene.Main);
    }
    #endregion

    /* �ڵ����� �����ϴ� �Լ� */
    private void SaveAuto()
    {
        if (WaitingInfo.autoSave) //�ڵ� ���� �䱸��
        {
            StartCoroutine(SaveGameAuto());
            WaitingInfo.autoSave = false;
        }
    }

    /* ������ �ڵ� �����ϴ� �ڷ�ƾ �Լ� */
    private IEnumerator SaveGameAuto()
    {
        if (string.IsNullOrEmpty(LoginInfo.userID)) //�Խ�Ʈ �α����̸�
        {
            Notification.notification.SetNotification("�Խ�Ʈ �α����� �ڵ� ���� ����� �������� �ʽ��ϴ�", 2f);
        }
        else //���� ���� �α����̸�
        {
            yield return StartCoroutine(NetworkManager.networkManager.SetGameInfo(LoginInfo.userID, LoginInfo.userName)); //���� ���� ����
            yield return StartCoroutine(NetworkManager.networkManager.SetAchievementInfo(LoginInfo.userID, LoginInfo.userName)); //���� ���� ����

            if (NetworkManager.setGameInfoResult == NetworkResult.NoError) //������ �߻����� ������
            {
                Notification.notification.SetNotification("���� ������ <color=#73AF87>�ڵ� ����</color>�Ǿ����ϴ�", 2f);
            }
            else //������ �߻��ϸ�
            {
                Notification.notification.SetNotification("���� ������ �ڵ� �����ϴ� ���� ������ �߻��߽��ϴ�", 3f);
            }
        }
    }

    /* ���� Sound�� ����ϴ� �Լ� */
    public void PlayStaticSounds(int index)
    {
        StaticSounds.staticSounds.PlayAudioSource(index);
    }
}
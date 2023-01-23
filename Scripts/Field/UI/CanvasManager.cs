using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [Header("Static")]
    public static CanvasManager canvasManager; //���� ���� ����

    [Header("HP Bar")]
    public Image hpBarImage; //HP Bar�� Image ������Ʈ
    public Image hpAfterImageBarImage; //HP AfterImage Bar�� Image ������Ʈ
    public float hpAfterImageSpeed; //HP �ܻ� �ӵ�
    public float hpAfterImageLerpSpeed; //HP �ܻ� ���� �ӵ�
    public float hpAfterImageChangeDistance; //HP �ܻ� ���� �Ÿ�

    [Header("Kill")]
    public TextMeshProUGUI killTMP; //Kill�� TMP ������Ʈ

    [Header("Money")]
    public TextMeshProUGUI moneyTMP; //Money�� TMP ������Ʈ
    public Animator diamondImageAnimator; //Diamond Image�� Animator ������Ʈ

    [Header("Pause")]
    public SpriteRenderer backgroundSpriteRenderer; //Background�� SpriteRenderer ������Ʈ
    public Canvas mainCanvasCanvas; //Main Canvas�� Canvas ������Ʈ
    public Canvas pauseCanvasCanvas; //Pause Canvas�� Canvas ������Ʈ

    [Header("Result")]
    public Canvas resultCanvasCanvas; //Result Canvas�� Canvas ������Ʈ
    public TextMeshProUGUI stageTextTMP; //Stage Text�� TMP ������Ʈ
    public TextMeshProUGUI currentKillTextTMP; //Current Kill Text�� TMP ������Ʈ
    public TextMeshProUGUI currentMoneyTextTMP; //Current Money Text�� TMP ������Ʈ
    public TextMeshProUGUI clearRewardTextTMP; //Clear Reward Text�� TMP ������Ʈ
    public TextMeshProUGUI totalProfitTextTMP; //Total Profit Text�� TMP ������Ʈ
    public GameObject adButton; //AD Button ������Ʈ

    [Header("Cache")]
    private int preMoney = -1; //���� Money
    private int preKill = -1; //���� ų ��
    private readonly int get_Hash = Animator.StringToHash("Get"); //Get �ִϸ����� �ؽ�
    private readonly Color32 totalProfitADRewardColor = new Color32(255, 235, 0, 255); //Total Profit�� ���� ���� ����

    private void Awake()
    {
        canvasManager = this;
    }

    private void Update()
    {
        SyncHPBar();
        SyncKill();
        SyncMoney();
    }

    /* Money�� ȹ���ϴ� �ִϸ��̼��� �����ϴ� �Լ� */
    public void AnimateGetMoney()
    {
        diamondImageAnimator.SetTrigger(get_Hash);
    }

    #region Sync
    /* HP Bar�� ����ȭ�ϴ� �Լ� */
    private void SyncHPBar()
    {
        hpBarImage.fillAmount = Player.player.currentHP / Player.player.maxHp; //HPBar ����

        if (Mathf.Abs(hpBarImage.fillAmount - hpAfterImageBarImage.fillAmount) < hpAfterImageChangeDistance) //HP �ܻ� ���� �Ÿ��� �Ǹ�
            hpAfterImageBarImage.fillAmount = Mathf.MoveTowards(hpAfterImageBarImage.fillAmount, hpBarImage.fillAmount, hpAfterImageSpeed * Time.deltaTime); //��� �̵�
        else //HP �ܻ� ���� �Ÿ��� �ȵǸ�
            hpAfterImageBarImage.fillAmount = Mathf.Lerp(hpAfterImageBarImage.fillAmount, hpBarImage.fillAmount, hpAfterImageLerpSpeed * Time.deltaTime); //���� �̵�
    }

    /* ų ���� ����ȭ�ϴ� �Լ� */
    private void SyncKill()
    {
        if (preKill != ShopInfo.kill) //���� ų ���� �ٸ���
        {
            killTMP.text = string.Format("{0:#,0}", ShopInfo.kill); //ų �� ����
            preKill = ShopInfo.kill; //���� ų �� ����
        }
    }

    /* Money�� ����ȭ�ϴ� �Լ� */
    private void SyncMoney()
    {
        if (preMoney != ShopInfo.money) //���� Money�� �ٸ���
        {
            moneyTMP.text = string.Format("{0:#,0}", ShopInfo.money); //Money ����
            preMoney = ShopInfo.money; //���� Money ����
        }
    }
    #endregion

    #region Pause
    /* Pause Canvas�� Ȱ��ȭ�ϴ� �Լ� */
    public void EnablePauseCanvas(bool value)
    {
        Time.timeScale = value ? 0f : 1f; //TimeScale ����
        pauseCanvasCanvas.enabled = value; //Pause Canvas Ȱ��ȭ/��Ȱ��ȭ
        mainCanvasCanvas.enabled = !value; //Main Canvas Ȱ��ȭ/��Ȱ��ȭ
        backgroundSpriteRenderer.sortingOrder = value ? (int)Definition.layerOrderRange.y + (int)Definition.layerOrderRange.y : (int)Definition.layerOrderRange.x - (int)Definition.layerOrderRange.y; //Background�� ���̾� ���� ����
    }

    /* ExitButton�� Ŭ������ �� ����Ǵ� �Լ� */
    public void OnClickExitButton()
    {
        Time.timeScale = 1f; //TimeScale ����
        SceneManager.LoadScene((int)Scene.Waiting);
    }
    #endregion

    #region Result
    /* Result Canvas�� Ȱ��ȭ�ϴ� �Լ� */
    public void ActivateResultCanvas()
    {
        mainCanvasCanvas.enabled = false; //MainCanvas ��Ȱ��ȭ
        pauseCanvasCanvas.enabled = false; //PauseCanvas ��Ȱ��ȭ
        resultCanvasCanvas.enabled = true; //ResultCanvas Ȱ��ȭ

        stageTextTMP.text = WaitingInfo.stage.ToString(); //�ܰ� ����
        currentKillTextTMP.text = string.Format("{0:#,0}", FieldManager.fieldManager.currentKill); //óġ �� ����

        int clearReward = FieldManager.fieldManager.isClear ? FieldManager.fieldManager.GetClearReward(WaitingInfo.stage) : 0; //Ŭ���� ����
        int totalProfit = (FieldManager.fieldManager.currentMoney + clearReward) + (FieldManager.fieldManager.isADReward ? (FieldManager.fieldManager.currentMoney + clearReward) / 2 : 0); //�� ����

        currentMoneyTextTMP.text = string.Format("{0:#,0}", FieldManager.fieldManager.currentMoney); //�� ���� ����
        clearRewardTextTMP.text = string.Format("{0:#,0}", clearReward); //Ŭ���� ���� ����
        if (FieldManager.fieldManager.isADReward) totalProfitTextTMP.color = totalProfitADRewardColor; //���� ���� �� �� ���� ���� ����
        totalProfitTextTMP.text = string.Format("{0:#,0}", totalProfit); //�� ���� ����

        adButton.SetActive(ADMobManager.adMobManager.rewardedAD.IsLoaded() && !FieldManager.fieldManager.isADReward); //AD Button Ȱ��ȭ/��Ȱ��ȭ
    }

    /* Result Canvas�� OKButton�� Ŭ������ �� ����Ǵ� �Լ� */
    public void OnClickResultCanvasOKButton()
    {
        int clearReward = FieldManager.fieldManager.isClear ? FieldManager.fieldManager.GetClearReward(WaitingInfo.stage) : 0; //Ŭ���� ����
        int totalProfit = (FieldManager.fieldManager.currentMoney + clearReward) + (FieldManager.fieldManager.isADReward ? (FieldManager.fieldManager.currentMoney + clearReward) / 2 : 0); //�� ����

        if(!string.IsNullOrEmpty(LoginInfo.userID)) Utility.utility.LoadStartCoroutine(NetworkManager.networkManager.SetResultInfo(LoginInfo.userID, LoginInfo.userName, WaitingInfo.stage, FieldManager.fieldManager.currentKill, FieldManager.fieldManager.currentMoney, clearReward, totalProfit, FieldManager.fieldManager.isADReward)); //��� ����

        ShopInfo.money += totalProfit - FieldManager.fieldManager.currentMoney; //�� ����
        ShopInfo.profit += totalProfit - FieldManager.fieldManager.currentMoney; //���� ����

        WaitingInfo.stage = FieldManager.fieldManager.isClear ? WaitingInfo.stage + 1 : WaitingInfo.stage; //�ܰ� ����

        WaitingInfo.autoSave = true; //�ڵ� ����
        SceneManager.LoadScene((int)Scene.Waiting); //Scene ��ȯ
    }

    /* Result Canvas�� AD Button�� Ŭ������ �� ����Ǵ� �Լ� */
    public void OnClickADButton()
    {
        ADMobManager.adMobManager.onUserEarnedRewardAction = () =>
        {
            FieldManager.fieldManager.isADReward = true; //���� ���� Ȱ��ȭ
            canvasManager.ActivateResultCanvas(); //Result Canvas Ȱ��ȭ
        }; //������ ���� ȹ�� �� ���� �Լ� ����

        ADMobManager.adMobManager.ShowRewardAD(); //������ ���� ����
    }
    #endregion
}
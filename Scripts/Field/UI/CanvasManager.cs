using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [Header("Static")]
    public static CanvasManager canvasManager; //전역 참조 변수

    [Header("HP Bar")]
    public Image hpBarImage; //HP Bar의 Image 컴포넌트
    public Image hpAfterImageBarImage; //HP AfterImage Bar의 Image 컴포넌트
    public float hpAfterImageSpeed; //HP 잔상 속도
    public float hpAfterImageLerpSpeed; //HP 잔상 보간 속도
    public float hpAfterImageChangeDistance; //HP 잔상 변경 거리

    [Header("Kill")]
    public TextMeshProUGUI killTMP; //Kill의 TMP 컴포넌트

    [Header("Money")]
    public TextMeshProUGUI moneyTMP; //Money의 TMP 컴포넌트
    public Animator diamondImageAnimator; //Diamond Image의 Animator 컴포넌트

    [Header("Pause")]
    public SpriteRenderer backgroundSpriteRenderer; //Background의 SpriteRenderer 컴포넌트
    public Canvas mainCanvasCanvas; //Main Canvas의 Canvas 컴포넌트
    public Canvas pauseCanvasCanvas; //Pause Canvas의 Canvas 컴포넌트

    [Header("Result")]
    public Canvas resultCanvasCanvas; //Result Canvas의 Canvas 컴포넌트
    public TextMeshProUGUI stageTextTMP; //Stage Text의 TMP 컴포넌트
    public TextMeshProUGUI currentKillTextTMP; //Current Kill Text의 TMP 컴포넌트
    public TextMeshProUGUI currentMoneyTextTMP; //Current Money Text의 TMP 컴포넌트
    public TextMeshProUGUI clearRewardTextTMP; //Clear Reward Text의 TMP 컴포넌트
    public TextMeshProUGUI totalProfitTextTMP; //Total Profit Text의 TMP 컴포넌트
    public GameObject adButton; //AD Button 오브젝트

    [Header("Cache")]
    private int preMoney = -1; //이전 Money
    private int preKill = -1; //이전 킬 수
    private readonly int get_Hash = Animator.StringToHash("Get"); //Get 애니메이터 해쉬
    private readonly Color32 totalProfitADRewardColor = new Color32(255, 235, 0, 255); //Total Profit의 광고 보상 색상

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

    /* Money를 획득하는 애니메이션을 실행하는 함수 */
    public void AnimateGetMoney()
    {
        diamondImageAnimator.SetTrigger(get_Hash);
    }

    #region Sync
    /* HP Bar를 동기화하는 함수 */
    private void SyncHPBar()
    {
        hpBarImage.fillAmount = Player.player.currentHP / Player.player.maxHp; //HPBar 갱신

        if (Mathf.Abs(hpBarImage.fillAmount - hpAfterImageBarImage.fillAmount) < hpAfterImageChangeDistance) //HP 잔상 변경 거리가 되면
            hpAfterImageBarImage.fillAmount = Mathf.MoveTowards(hpAfterImageBarImage.fillAmount, hpBarImage.fillAmount, hpAfterImageSpeed * Time.deltaTime); //등속 이동
        else //HP 잔상 변경 거리가 안되면
            hpAfterImageBarImage.fillAmount = Mathf.Lerp(hpAfterImageBarImage.fillAmount, hpBarImage.fillAmount, hpAfterImageLerpSpeed * Time.deltaTime); //보간 이동
    }

    /* 킬 수를 동기화하는 함수 */
    private void SyncKill()
    {
        if (preKill != ShopInfo.kill) //이전 킬 수와 다르면
        {
            killTMP.text = string.Format("{0:#,0}", ShopInfo.kill); //킬 수 갱신
            preKill = ShopInfo.kill; //이전 킬 수 갱신
        }
    }

    /* Money를 동기화하는 함수 */
    private void SyncMoney()
    {
        if (preMoney != ShopInfo.money) //이전 Money와 다르면
        {
            moneyTMP.text = string.Format("{0:#,0}", ShopInfo.money); //Money 갱신
            preMoney = ShopInfo.money; //이전 Money 갱신
        }
    }
    #endregion

    #region Pause
    /* Pause Canvas를 활성화하는 함수 */
    public void EnablePauseCanvas(bool value)
    {
        Time.timeScale = value ? 0f : 1f; //TimeScale 조절
        pauseCanvasCanvas.enabled = value; //Pause Canvas 활성화/비활성화
        mainCanvasCanvas.enabled = !value; //Main Canvas 활성화/비활성화
        backgroundSpriteRenderer.sortingOrder = value ? (int)Definition.layerOrderRange.y + (int)Definition.layerOrderRange.y : (int)Definition.layerOrderRange.x - (int)Definition.layerOrderRange.y; //Background의 레이어 순서 조절
    }

    /* ExitButton을 클릭했을 때 실행되는 함수 */
    public void OnClickExitButton()
    {
        Time.timeScale = 1f; //TimeScale 조절
        SceneManager.LoadScene((int)Scene.Waiting);
    }
    #endregion

    #region Result
    /* Result Canvas를 활성화하는 함수 */
    public void ActivateResultCanvas()
    {
        mainCanvasCanvas.enabled = false; //MainCanvas 비활성화
        pauseCanvasCanvas.enabled = false; //PauseCanvas 비활성화
        resultCanvasCanvas.enabled = true; //ResultCanvas 활성화

        stageTextTMP.text = WaitingInfo.stage.ToString(); //단계 지정
        currentKillTextTMP.text = string.Format("{0:#,0}", FieldManager.fieldManager.currentKill); //처치 수 지정

        int clearReward = FieldManager.fieldManager.isClear ? FieldManager.fieldManager.GetClearReward(WaitingInfo.stage) : 0; //클리어 보상
        int totalProfit = (FieldManager.fieldManager.currentMoney + clearReward) + (FieldManager.fieldManager.isADReward ? (FieldManager.fieldManager.currentMoney + clearReward) / 2 : 0); //총 수익

        currentMoneyTextTMP.text = string.Format("{0:#,0}", FieldManager.fieldManager.currentMoney); //번 수익 지정
        clearRewardTextTMP.text = string.Format("{0:#,0}", clearReward); //클리어 보상 지정
        if (FieldManager.fieldManager.isADReward) totalProfitTextTMP.color = totalProfitADRewardColor; //광고 보상 시 총 수익 색상 변경
        totalProfitTextTMP.text = string.Format("{0:#,0}", totalProfit); //총 수익 지정

        adButton.SetActive(ADMobManager.adMobManager.rewardedAD.IsLoaded() && !FieldManager.fieldManager.isADReward); //AD Button 활성화/비활성화
    }

    /* Result Canvas의 OKButton을 클릭했을 때 실행되는 함수 */
    public void OnClickResultCanvasOKButton()
    {
        int clearReward = FieldManager.fieldManager.isClear ? FieldManager.fieldManager.GetClearReward(WaitingInfo.stage) : 0; //클리어 보상
        int totalProfit = (FieldManager.fieldManager.currentMoney + clearReward) + (FieldManager.fieldManager.isADReward ? (FieldManager.fieldManager.currentMoney + clearReward) / 2 : 0); //총 수익

        if(!string.IsNullOrEmpty(LoginInfo.userID)) Utility.utility.LoadStartCoroutine(NetworkManager.networkManager.SetResultInfo(LoginInfo.userID, LoginInfo.userName, WaitingInfo.stage, FieldManager.fieldManager.currentKill, FieldManager.fieldManager.currentMoney, clearReward, totalProfit, FieldManager.fieldManager.isADReward)); //결과 저장

        ShopInfo.money += totalProfit - FieldManager.fieldManager.currentMoney; //돈 증가
        ShopInfo.profit += totalProfit - FieldManager.fieldManager.currentMoney; //수익 증가

        WaitingInfo.stage = FieldManager.fieldManager.isClear ? WaitingInfo.stage + 1 : WaitingInfo.stage; //단계 증가

        WaitingInfo.autoSave = true; //자동 저장
        SceneManager.LoadScene((int)Scene.Waiting); //Scene 전환
    }

    /* Result Canvas의 AD Button을 클릭했을 때 실행되는 함수 */
    public void OnClickADButton()
    {
        ADMobManager.adMobManager.onUserEarnedRewardAction = () =>
        {
            FieldManager.fieldManager.isADReward = true; //광고 보상 활성화
            canvasManager.ActivateResultCanvas(); //Result Canvas 활성화
        }; //보상형 광고 획득 시 실행 함수 지정

        ADMobManager.adMobManager.ShowRewardAD(); //보상형 광고 실행
    }
    #endregion
}
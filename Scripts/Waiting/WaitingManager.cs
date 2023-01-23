using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class WaitingManager : MonoBehaviour
{
    [Header("Background")]
    public Image backgroundImage; //Background의 Image 컴포넌트
    public Sprite[] backgroundSprite; //배경 Sprite들

    [Header("Waiting")]
    public TextMeshProUGUI stageTMP; //Stage의 TMP 컴포넌트

    [Header("Selected Weapons")]
    public GameObject[] firstWeapons; //첫 번째 무기 오브젝트들
    public GameObject[] secondWeapons; //두 번째 무기 오브젝트들

    [Header("Infos")]
    public TextMeshProUGUI moneyTMP; //Money의 TMP 컴포넌트
    public TextMeshProUGUI profitTMP; //Profit의 TMP 컴포넌트
    public TextMeshProUGUI killTMP; //kill의 TMP 컴포넌트

    [Header("Cache")]
    private Weapon currentFirstSelectedWeapon; //현재 첫 번째 선택된 무기
    private Weapon currentSecondSelectedWeapon; //현재 두 번째 선택된 무기
    private Weapon preFirstSelectedWeapon = Weapon.None; //이전 첫 번째 선택된 무기
    private Weapon preSecondSelectedWeapon = Weapon.None; //이전 두 번째 선택된 무기
    private int preMoney = -1; //이전 Money
    private int preProfit = -1; //이전 Profit
    private int preKill = -1; //이전 Kill

    private void Start()
    {
        backgroundImage.sprite = backgroundSprite[((WaitingInfo.stage - 1) - (WaitingInfo.stage - 1) % 10) / 10 % 3]; //배경 지정
        stageTMP.text = string.Format("{0:000}", WaitingInfo.stage); //단계 지정
    }

    private void Update()
    {
        SyncSelectedWeapons();
        SyncInfos();
        SaveAuto();
    }

    /* WaitingInfo를 초기화하는 함수 */
    public static void ResetWaitingInfo()
    {
        WaitingInfo.stage = 1;
    }

    #region Sync
    /* 선택된 무기들을 동기화하는 함수 */
    private void SyncSelectedWeapons()
    {
        currentFirstSelectedWeapon = ShopInfo.firstSelectedWeapon; //현재 첫 번째 선택된 무기 저장
        currentSecondSelectedWeapon = ShopInfo.secondSelectedWeapon;  //현재 두 번째 선택된 무기 저장

        if (preFirstSelectedWeapon != currentFirstSelectedWeapon || preSecondSelectedWeapon != currentSecondSelectedWeapon) //이전 선택된 무기와 현재 선택된 무기가 다르면
        {
            for (int i = 0; i < firstWeapons.Length; ++i) firstWeapons[i].SetActive(false); //모든 Weapon 비활성화
            for (int i = 0; i < secondWeapons.Length; ++i) secondWeapons[i].SetActive(false);

            firstWeapons[(int)currentFirstSelectedWeapon].SetActive(true); //선택된 Weapon 활성화
            secondWeapons[(int)currentSecondSelectedWeapon].SetActive(true);

            preFirstSelectedWeapon = currentFirstSelectedWeapon; //이전 첫 번째 선택된 무기 갱신
            preSecondSelectedWeapon = currentSecondSelectedWeapon; //이전 두 번째 선택된 무기 갱신
        }
    }

    /* Infos 내의 수치를 동기화하는 함수 */
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
    /* Save Button을 클릭했을 때 실행되는 함수 */
    public void OnClickSaveButton()
    {
        StartCoroutine(SaveGame());
    }

    /* 게임을 저장하는 코루틴 함수 */
    private IEnumerator SaveGame()
    {
        if (string.IsNullOrEmpty(LoginInfo.userID)) //게스트 로그인이면
        {
            Notification.notification.SetNotification("게스트 로그인은 저장 기능이 제공되지 않습니다", 2f);
        }
        else //구글 계정 로그인이면
        {
            yield return StartCoroutine(NetworkManager.networkManager.SetGameInfo(LoginInfo.userID, LoginInfo.userName)); //게임 정보 저장
            yield return StartCoroutine(NetworkManager.networkManager.SetAchievementInfo(LoginInfo.userID, LoginInfo.userName)); //업적 정보 저장

            if (NetworkManager.setGameInfoResult == NetworkResult.NoError) //오류가 발생하지 않으면
            {
                Notification.notification.SetNotification("진행 사항이 성공적으로 <color=#73AF87>저장</color>되었습니다", 2f);
            }
            else //오류가 발생하면
            {
                Notification.notification.SetNotification("진행 사항을 저장하는 도중 오류가 발생했습니다", 3f);
            }
        }
    }

    /* Battle Button을 클릭했을 때 실행되는 함수 */
    public void OnClickBattleButton()
    {
        SceneManager.LoadScene((int)Scene.Field);
    }

    /* Main Button을 클릭했을 때 실행되는 함수 */
    public void OnClickMainButton()
    {
        SceneManager.LoadScene((int)Scene.Main);
    }
    #endregion

    /* 자동으로 저장하는 함수 */
    private void SaveAuto()
    {
        if (WaitingInfo.autoSave) //자동 저장 요구면
        {
            StartCoroutine(SaveGameAuto());
            WaitingInfo.autoSave = false;
        }
    }

    /* 게임을 자동 저장하는 코루틴 함수 */
    private IEnumerator SaveGameAuto()
    {
        if (string.IsNullOrEmpty(LoginInfo.userID)) //게스트 로그인이면
        {
            Notification.notification.SetNotification("게스트 로그인은 자동 저장 기능이 제공되지 않습니다", 2f);
        }
        else //구글 계정 로그인이면
        {
            yield return StartCoroutine(NetworkManager.networkManager.SetGameInfo(LoginInfo.userID, LoginInfo.userName)); //게임 정보 저장
            yield return StartCoroutine(NetworkManager.networkManager.SetAchievementInfo(LoginInfo.userID, LoginInfo.userName)); //업적 정보 저장

            if (NetworkManager.setGameInfoResult == NetworkResult.NoError) //오류가 발생하지 않으면
            {
                Notification.notification.SetNotification("진행 사항이 <color=#73AF87>자동 저장</color>되었습니다", 2f);
            }
            else //오류가 발생하면
            {
                Notification.notification.SetNotification("진행 사항을 자동 저장하는 도중 오류가 발생했습니다", 3f);
            }
        }
    }

    /* 전역 Sound를 재생하는 함수 */
    public void PlayStaticSounds(int index)
    {
        StaticSounds.staticSounds.PlayAudioSource(index);
    }
}
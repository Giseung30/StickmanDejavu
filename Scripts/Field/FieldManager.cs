using UnityEngine;
using System.Collections;

public class FieldManager : MonoBehaviour
{
    [Header("Static")]
    public static FieldManager fieldManager; //전역 참조 변수

    [Header("Background")]
    public SpriteRenderer backgroundSpriteRenderer; //Background의 SpriteRenderer 컴포넌트
    public Sprite[] backgroundSprite; //배경 Sprite들

    [Header("BGM")]
    public AudioSource bgmAudioSource; //BGM의 AudioSource 컴포넌트
    public AudioClip[] backgroundMusics; //배경 Music의 AudioClip 컴포넌트들

    [Header("Variable")]
    public bool isClear; //단계를 클리어했는지 판단하는 변수
    public bool isADReward; //광고 보상인지 판단하는 변수
    public int currentKill
    {
        get
        {
            return AntiCheatManager.currentKillSecured;
        }
        set
        {
            ShopInfo.kill += value - AntiCheatManager.currentKillSecured; //킬 수 증가
            AntiCheatManager.currentKillSecured = value; //현재 킬 수 지정
        }
    } //현재 킬 수
    public int currentMoney
    {
        get
        {
            return AntiCheatManager.currentMoneySecured;
        }
        set
        {
            CanvasManager.canvasManager.AnimateGetMoney(); //GetMoney 애니메이션 실행
            ShopInfo.money += value - AntiCheatManager.currentMoneySecured; //Money 증가
            ShopInfo.profit += value - AntiCheatManager.currentMoneySecured; //수익 증가
            AntiCheatManager.currentMoneySecured = value; //현재 Money 지정
        }
    }//현재 Money

    private void Awake()
    {
        fieldManager = this;
    }

    private void Start()
    {
        AntiCheatManager.currentKillSecured = 0; //현재 킬 수 초기화
        AntiCheatManager.currentMoneySecured = 0; //현재 Money 초기화

        int fieldType = ((WaitingInfo.stage - 1) - (WaitingInfo.stage - 1) % 10) / 10 % 3; //배경 종류

        backgroundSpriteRenderer.sprite = backgroundSprite[fieldType]; //배경 지정

        bgmAudioSource.clip = backgroundMusics[fieldType]; //BGM 지정
        bgmAudioSource.mute = !MainInfo.enableBGM; //BGM 음소거 지정
        bgmAudioSource.Play(); //BGM 재생
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) CanvasManager.canvasManager.EnablePauseCanvas(true); //뒤로 가기 버튼을 누르면 Pause
    }

    #region Result
    /* 단계를 종료하는 함수 */
    public IEnumerator FinishStage(bool isClear)
    {
        this.isClear = isClear; //클리어 여부 지정

        if (isClear && WaitingInfo.stage % Definition.maxStage == 0) //모든 단계 클리어 시 알림 출력
        {
            Notification.notification.SetNotification("축하합니다! 모든 단계를 클리어하셨습니다!", 10f);
        }

        yield return new WaitForSeconds(1f);

        CanvasManager.canvasManager.ActivateResultCanvas(); //Result Canvas 활성화
    }

    /* 클리어 보상을 반환하는 함수 */
    public int GetClearReward(int stage)
    {
        return stage > Definition.maxStage ? 0 : 100 + 10 * (stage - 1);
    }
    #endregion

    /* 전역 Sound를 재생하는 함수 */
    public void PlayStaticSounds(int index)
    {
        StaticSounds.staticSounds.PlayAudioSource(index);
    }
}
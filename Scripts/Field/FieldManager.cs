using UnityEngine;
using System.Collections;

public class FieldManager : MonoBehaviour
{
    [Header("Static")]
    public static FieldManager fieldManager; //���� ���� ����

    [Header("Background")]
    public SpriteRenderer backgroundSpriteRenderer; //Background�� SpriteRenderer ������Ʈ
    public Sprite[] backgroundSprite; //��� Sprite��

    [Header("BGM")]
    public AudioSource bgmAudioSource; //BGM�� AudioSource ������Ʈ
    public AudioClip[] backgroundMusics; //��� Music�� AudioClip ������Ʈ��

    [Header("Variable")]
    public bool isClear; //�ܰ踦 Ŭ�����ߴ��� �Ǵ��ϴ� ����
    public bool isADReward; //���� �������� �Ǵ��ϴ� ����
    public int currentKill
    {
        get
        {
            return AntiCheatManager.currentKillSecured;
        }
        set
        {
            ShopInfo.kill += value - AntiCheatManager.currentKillSecured; //ų �� ����
            AntiCheatManager.currentKillSecured = value; //���� ų �� ����
        }
    } //���� ų ��
    public int currentMoney
    {
        get
        {
            return AntiCheatManager.currentMoneySecured;
        }
        set
        {
            CanvasManager.canvasManager.AnimateGetMoney(); //GetMoney �ִϸ��̼� ����
            ShopInfo.money += value - AntiCheatManager.currentMoneySecured; //Money ����
            ShopInfo.profit += value - AntiCheatManager.currentMoneySecured; //���� ����
            AntiCheatManager.currentMoneySecured = value; //���� Money ����
        }
    }//���� Money

    private void Awake()
    {
        fieldManager = this;
    }

    private void Start()
    {
        AntiCheatManager.currentKillSecured = 0; //���� ų �� �ʱ�ȭ
        AntiCheatManager.currentMoneySecured = 0; //���� Money �ʱ�ȭ

        int fieldType = ((WaitingInfo.stage - 1) - (WaitingInfo.stage - 1) % 10) / 10 % 3; //��� ����

        backgroundSpriteRenderer.sprite = backgroundSprite[fieldType]; //��� ����

        bgmAudioSource.clip = backgroundMusics[fieldType]; //BGM ����
        bgmAudioSource.mute = !MainInfo.enableBGM; //BGM ���Ұ� ����
        bgmAudioSource.Play(); //BGM ���
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) CanvasManager.canvasManager.EnablePauseCanvas(true); //�ڷ� ���� ��ư�� ������ Pause
    }

    #region Result
    /* �ܰ踦 �����ϴ� �Լ� */
    public IEnumerator FinishStage(bool isClear)
    {
        this.isClear = isClear; //Ŭ���� ���� ����

        if (isClear && WaitingInfo.stage % Definition.maxStage == 0) //��� �ܰ� Ŭ���� �� �˸� ���
        {
            Notification.notification.SetNotification("�����մϴ�! ��� �ܰ踦 Ŭ�����ϼ̽��ϴ�!", 10f);
        }

        yield return new WaitForSeconds(1f);

        CanvasManager.canvasManager.ActivateResultCanvas(); //Result Canvas Ȱ��ȭ
    }

    /* Ŭ���� ������ ��ȯ�ϴ� �Լ� */
    public int GetClearReward(int stage)
    {
        return stage > Definition.maxStage ? 0 : 100 + 10 * (stage - 1);
    }
    #endregion

    /* ���� Sound�� ����ϴ� �Լ� */
    public void PlayStaticSounds(int index)
    {
        StaticSounds.staticSounds.PlayAudioSource(index);
    }
}
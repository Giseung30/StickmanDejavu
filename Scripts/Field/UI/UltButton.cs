using UnityEngine;
using UnityEngine.UI;

public class UltButton : MonoBehaviour
{
    [Header("Static")]
    public static UltButton ultButton; //���� ���� ����

    [Header("Component")]
    public GameObject[] ultButtons; //UltButton ������Ʈ��
    public Image gaugeImage; //Gauge�� Image ������Ʈ

    [Header("State")]
    private bool _enableUlt = true;
    public bool enableUlt
    {
        get
        {
            return _enableUlt;
        }
        set
        {
            _enableUlt = value;
        }
    } //�ñر� ���� ����
    private Weapon _currentUlt;
    public Weapon currentUlt
    {
        get
        {
            return _currentUlt;
        }
        set
        {
            for (int i = 0; i < ultButtons.Length; ++i) ultButtons[i].SetActive(false); //��� UltButton ��Ȱ��ȭ
            ultButtons[(int)value].SetActive(true); //���õ� UltButton Ȱ��ȭ
            _currentUlt = value; //�� ����
        }
    } //���� �ñر�

    public float fistUltGauge; //Fist�� �ñر� ������
    public float swordUltGauge; //Sword�� �ñر� ������
    public float gunUltGauge; //Gun�� �ñر� ������
    public float sniperUltGauge; //Sniper�� �ñر� ������
    public float bazookaUltGauge; //Bazooka�� �ñر� ������
    public float wizardUltGauge; //Wizard�� �ñر� ������

    [Header("Cache")]
    private readonly int fist_Ult_AnimationHash = Animator.StringToHash("Fist_Ult"); //Fist_Ult �ִϸ��̼� �ؽ�
    private readonly int sword_Ult_AnimationHash = Animator.StringToHash("Sword_Ult"); //Sword_Ult �ִϸ��̼� �ؽ�
    private readonly int gun_Ult_AnimationHash = Animator.StringToHash("Gun_Ult"); //Gun_Ult �ִϸ��̼� �ؽ�
    private readonly int sniper_Ult_Ready_AnimationHash = Animator.StringToHash("Sniper_Ult_Ready"); //Sniper_Ult_Ready �ִϸ��̼� �ؽ�
    private readonly int bazooka_Ult_AnimationHash = Animator.StringToHash("Bazooka_Ult"); //Bazooka_Ult �ִϸ��̼� �ؽ�
    private readonly int wizard_Ult_AnimationHash = Animator.StringToHash("Wizard_Ult"); //Wizard_Ult �ִϸ��̼� �ؽ�

    private readonly float fistUltGaugeMax = Definition.fistUltGaugeMax[ShopInfo.fistUltLevel / 3]; //Fist Ult Gauge �ִ�
    private readonly float swordUltGaugeMax = Definition.swordUltGaugeMax[ShopInfo.swordUltLevel / 3]; //Sword Ult Gauge �ִ�
    private readonly float gunUltGaugeMax = Definition.gunUltGaugeMax[ShopInfo.gunUltLevel / 3]; //Gun Ult Gauge �ִ�
    private readonly float sniperUltGaugeMax = Definition.sniperUltGaugeMax[ShopInfo.sniperUltLevel / 3]; //Sniper Ult Gauge �ִ�
    private readonly float bazookaUltGaugeMax = Definition.bazookaUltGaugeMax[ShopInfo.bazookaUltLevel / 3]; //Bazooka Ult Gauge �ִ�
    private readonly float wizardUltGaugeMax = Definition.wizardUltGaugeMax[ShopInfo.wizardUltLevel / 3]; //Wizard Ult Gauge �ִ�

    private readonly Color32 ultGaugeBasicColor = Color.white; //Ult Gauge�� �⺻ ����
    private readonly Color32 ultGaugeFullColor = new Color32(50, 255, 50, 255); //Ult Gauge�� ���� �� ����

    private void Awake()
    {
        ultButton = this; //���� ���� ���� �ʱ�ȭ
    }

    private void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor) StartCoroutine(OnClickUltButtonForPC());
    }

    private void Update()
    {
        SyncUltGauge();
    }

    /* UltButton�� Ŭ���Ǿ��� �� ����Ǵ� �ڷ�ƾ �Լ� - PC */
    private System.Collections.IEnumerator OnClickUltButtonForPC()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Q)) OnClickUltButton((int)Player.player.currentWeapon);

            yield return null;
        }
    }

    /* UltButton�� Ŭ���Ǿ��� �� ����Ǵ� �Լ� */
    public void OnClickUltButton(int index)
    {
        if (!enableUlt) return; //�ñر� ���� �����̸� ����

        if ((Weapon)index == Weapon.Fist) //Fist
        {
            if (fistUltGauge >= fistUltGaugeMax) //�ñر� �������� ��������
            {
                fistUltGauge = 0f; //�ñر� ������ �ʱ�ȭ
                Player.player.playerAnimator.Play(fist_Ult_AnimationHash); //�ִϸ��̼� ����
            }
        }
        else if ((Weapon)index == Weapon.Sword) //Sword
        {
            if (swordUltGauge >= swordUltGaugeMax) //�ñر� �������� ��������
            {
                swordUltGauge = 0f; //�ñر� ������ �ʱ�ȭ
                Player.player.playerAnimator.Play(sword_Ult_AnimationHash); //�ִϸ��̼� ����
            }
        }
        else if ((Weapon)index == Weapon.Gun) //Gun
        {
            if (gunUltGauge >= gunUltGaugeMax) //�ñر� �������� ��������
            {
                gunUltGauge = 0f; //�ñر� ������ �ʱ�ȭ
                Player.player.playerAnimator.Play(gun_Ult_AnimationHash); //�ִϸ��̼� ����
            }
        }
        else if ((Weapon)index == Weapon.Sniper) //Sniper
        {
            if (sniperUltGauge >= sniperUltGaugeMax) //�ñر� �������� ��������
            {
                sniperUltGauge = 0f; //�ñر� ������ �ʱ�ȭ
                Player.player.playerAnimator.Play(sniper_Ult_Ready_AnimationHash); //�ִϸ��̼� ����
            }
        }
        else if ((Weapon)index == Weapon.Bazooka) //Bazooka
        {
            if (bazookaUltGauge >= bazookaUltGaugeMax) //�ñر� �������� ��������
            {
                bazookaUltGauge = 0f; //�ñر� ������ �ʱ�ȭ
                Player.player.playerAnimator.Play(bazooka_Ult_AnimationHash); //�ִϸ��̼� ����
            }
        }
        else if ((Weapon)index == Weapon.Wizard) //Wizard
        {
            if (wizardUltGauge >= wizardUltGaugeMax) //�ñر� �������� ��������
            {
                wizardUltGauge = 0f; //�ñر� ������ �ʱ�ȭ
                Player.player.playerAnimator.Play(wizard_Ult_AnimationHash); //�ִϸ��̼� ����
            }
        }
    }

    /* Ult Gauge�� ����ȭ�ϴ� �Լ� */
    private void SyncUltGauge()
    {
        if (currentUlt == Weapon.Fist) //Fist
        {
            gaugeImage.fillAmount = fistUltGauge / fistUltGaugeMax; //FillAmount ����
            gaugeImage.color = fistUltGauge < fistUltGaugeMax ? ultGaugeBasicColor : ultGaugeFullColor; //���� ����
        }
        else if (currentUlt == Weapon.Sword) //Sword
        {
            gaugeImage.fillAmount = swordUltGauge / swordUltGaugeMax; //FillAmount ����
            gaugeImage.color = swordUltGauge < swordUltGaugeMax ? ultGaugeBasicColor : ultGaugeFullColor; //���� ����
        }
        else if (currentUlt == Weapon.Gun) //Gun
        {
            gaugeImage.fillAmount = gunUltGauge / gunUltGaugeMax; //FillAmount ����
            gaugeImage.color = gunUltGauge < gunUltGaugeMax ? ultGaugeBasicColor : ultGaugeFullColor; //���� ����
        }
        else if (currentUlt == Weapon.Sniper) //Sniper
        {
            gaugeImage.fillAmount = sniperUltGauge / sniperUltGaugeMax; //FillAmount ����
            gaugeImage.color = sniperUltGauge < sniperUltGaugeMax ? ultGaugeBasicColor : ultGaugeFullColor; //���� ����
        }
        else if (currentUlt == Weapon.Bazooka) //Bazooka
        {
            gaugeImage.fillAmount = bazookaUltGauge / bazookaUltGaugeMax; //FillAmount ����
            gaugeImage.color = bazookaUltGauge < bazookaUltGaugeMax ? ultGaugeBasicColor : ultGaugeFullColor; //���� ����
        }
        else if (currentUlt == Weapon.Wizard) //Wizard
        {
            gaugeImage.fillAmount = wizardUltGauge / wizardUltGaugeMax; //FillAmount ����
            gaugeImage.color = wizardUltGauge < wizardUltGaugeMax ? ultGaugeBasicColor : ultGaugeFullColor; //���� ����
        }
    }

    /* Ult Gauge�� ������Ű�� �Լ� */
    public void IncreaseUltGauge(float value)
    {
        if (currentUlt == Weapon.Fist) //Fist
        {
            fistUltGauge += value;
        }
        else if (currentUlt == Weapon.Sword) //Sword
        {
            swordUltGauge += value;
        }
        else if (currentUlt == Weapon.Gun) //Gun
        {
            gunUltGauge += value;
        }
        else if (currentUlt == Weapon.Sniper) //Sniper
        {
            sniperUltGauge += value;
        }
        else if (currentUlt == Weapon.Bazooka) //Bazooka
        {
            bazookaUltGauge += value;
        }
        else if (currentUlt == Weapon.Wizard) //Wizard
        {
            wizardUltGauge += value;
        }
    }
}
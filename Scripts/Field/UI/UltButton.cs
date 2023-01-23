using UnityEngine;
using UnityEngine.UI;

public class UltButton : MonoBehaviour
{
    [Header("Static")]
    public static UltButton ultButton; //전역 참조 변수

    [Header("Component")]
    public GameObject[] ultButtons; //UltButton 오브젝트들
    public Image gaugeImage; //Gauge의 Image 컴포넌트

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
    } //궁극기 제한 변수
    private Weapon _currentUlt;
    public Weapon currentUlt
    {
        get
        {
            return _currentUlt;
        }
        set
        {
            for (int i = 0; i < ultButtons.Length; ++i) ultButtons[i].SetActive(false); //모든 UltButton 비활성화
            ultButtons[(int)value].SetActive(true); //선택된 UltButton 활성화
            _currentUlt = value; //값 지정
        }
    } //현재 궁극기

    public float fistUltGauge; //Fist의 궁극기 게이지
    public float swordUltGauge; //Sword의 궁극기 게이지
    public float gunUltGauge; //Gun의 궁극기 게이지
    public float sniperUltGauge; //Sniper의 궁극기 게이지
    public float bazookaUltGauge; //Bazooka의 궁극기 게이지
    public float wizardUltGauge; //Wizard의 궁극기 게이지

    [Header("Cache")]
    private readonly int fist_Ult_AnimationHash = Animator.StringToHash("Fist_Ult"); //Fist_Ult 애니메이션 해쉬
    private readonly int sword_Ult_AnimationHash = Animator.StringToHash("Sword_Ult"); //Sword_Ult 애니메이션 해쉬
    private readonly int gun_Ult_AnimationHash = Animator.StringToHash("Gun_Ult"); //Gun_Ult 애니메이션 해쉬
    private readonly int sniper_Ult_Ready_AnimationHash = Animator.StringToHash("Sniper_Ult_Ready"); //Sniper_Ult_Ready 애니메이션 해쉬
    private readonly int bazooka_Ult_AnimationHash = Animator.StringToHash("Bazooka_Ult"); //Bazooka_Ult 애니메이션 해쉬
    private readonly int wizard_Ult_AnimationHash = Animator.StringToHash("Wizard_Ult"); //Wizard_Ult 애니메이션 해쉬

    private readonly float fistUltGaugeMax = Definition.fistUltGaugeMax[ShopInfo.fistUltLevel / 3]; //Fist Ult Gauge 최대
    private readonly float swordUltGaugeMax = Definition.swordUltGaugeMax[ShopInfo.swordUltLevel / 3]; //Sword Ult Gauge 최대
    private readonly float gunUltGaugeMax = Definition.gunUltGaugeMax[ShopInfo.gunUltLevel / 3]; //Gun Ult Gauge 최대
    private readonly float sniperUltGaugeMax = Definition.sniperUltGaugeMax[ShopInfo.sniperUltLevel / 3]; //Sniper Ult Gauge 최대
    private readonly float bazookaUltGaugeMax = Definition.bazookaUltGaugeMax[ShopInfo.bazookaUltLevel / 3]; //Bazooka Ult Gauge 최대
    private readonly float wizardUltGaugeMax = Definition.wizardUltGaugeMax[ShopInfo.wizardUltLevel / 3]; //Wizard Ult Gauge 최대

    private readonly Color32 ultGaugeBasicColor = Color.white; //Ult Gauge의 기본 색상
    private readonly Color32 ultGaugeFullColor = new Color32(50, 255, 50, 255); //Ult Gauge의 가득 찬 색상

    private void Awake()
    {
        ultButton = this; //전역 참조 변수 초기화
    }

    private void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor) StartCoroutine(OnClickUltButtonForPC());
    }

    private void Update()
    {
        SyncUltGauge();
    }

    /* UltButton이 클릭되었을 때 실행되는 코루틴 함수 - PC */
    private System.Collections.IEnumerator OnClickUltButtonForPC()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Q)) OnClickUltButton((int)Player.player.currentWeapon);

            yield return null;
        }
    }

    /* UltButton이 클릭되었을 때 실행되는 함수 */
    public void OnClickUltButton(int index)
    {
        if (!enableUlt) return; //궁극기 금지 상태이면 종료

        if ((Weapon)index == Weapon.Fist) //Fist
        {
            if (fistUltGauge >= fistUltGaugeMax) //궁극기 게이지가 가득차면
            {
                fistUltGauge = 0f; //궁극기 게이지 초기화
                Player.player.playerAnimator.Play(fist_Ult_AnimationHash); //애니메이션 실행
            }
        }
        else if ((Weapon)index == Weapon.Sword) //Sword
        {
            if (swordUltGauge >= swordUltGaugeMax) //궁극기 게이지가 가득차면
            {
                swordUltGauge = 0f; //궁극기 게이지 초기화
                Player.player.playerAnimator.Play(sword_Ult_AnimationHash); //애니메이션 실행
            }
        }
        else if ((Weapon)index == Weapon.Gun) //Gun
        {
            if (gunUltGauge >= gunUltGaugeMax) //궁극기 게이지가 가득차면
            {
                gunUltGauge = 0f; //궁극기 게이지 초기화
                Player.player.playerAnimator.Play(gun_Ult_AnimationHash); //애니메이션 실행
            }
        }
        else if ((Weapon)index == Weapon.Sniper) //Sniper
        {
            if (sniperUltGauge >= sniperUltGaugeMax) //궁극기 게이지가 가득차면
            {
                sniperUltGauge = 0f; //궁극기 게이지 초기화
                Player.player.playerAnimator.Play(sniper_Ult_Ready_AnimationHash); //애니메이션 실행
            }
        }
        else if ((Weapon)index == Weapon.Bazooka) //Bazooka
        {
            if (bazookaUltGauge >= bazookaUltGaugeMax) //궁극기 게이지가 가득차면
            {
                bazookaUltGauge = 0f; //궁극기 게이지 초기화
                Player.player.playerAnimator.Play(bazooka_Ult_AnimationHash); //애니메이션 실행
            }
        }
        else if ((Weapon)index == Weapon.Wizard) //Wizard
        {
            if (wizardUltGauge >= wizardUltGaugeMax) //궁극기 게이지가 가득차면
            {
                wizardUltGauge = 0f; //궁극기 게이지 초기화
                Player.player.playerAnimator.Play(wizard_Ult_AnimationHash); //애니메이션 실행
            }
        }
    }

    /* Ult Gauge를 동기화하는 함수 */
    private void SyncUltGauge()
    {
        if (currentUlt == Weapon.Fist) //Fist
        {
            gaugeImage.fillAmount = fistUltGauge / fistUltGaugeMax; //FillAmount 지정
            gaugeImage.color = fistUltGauge < fistUltGaugeMax ? ultGaugeBasicColor : ultGaugeFullColor; //색상 지정
        }
        else if (currentUlt == Weapon.Sword) //Sword
        {
            gaugeImage.fillAmount = swordUltGauge / swordUltGaugeMax; //FillAmount 지정
            gaugeImage.color = swordUltGauge < swordUltGaugeMax ? ultGaugeBasicColor : ultGaugeFullColor; //색상 지정
        }
        else if (currentUlt == Weapon.Gun) //Gun
        {
            gaugeImage.fillAmount = gunUltGauge / gunUltGaugeMax; //FillAmount 지정
            gaugeImage.color = gunUltGauge < gunUltGaugeMax ? ultGaugeBasicColor : ultGaugeFullColor; //색상 지정
        }
        else if (currentUlt == Weapon.Sniper) //Sniper
        {
            gaugeImage.fillAmount = sniperUltGauge / sniperUltGaugeMax; //FillAmount 지정
            gaugeImage.color = sniperUltGauge < sniperUltGaugeMax ? ultGaugeBasicColor : ultGaugeFullColor; //색상 지정
        }
        else if (currentUlt == Weapon.Bazooka) //Bazooka
        {
            gaugeImage.fillAmount = bazookaUltGauge / bazookaUltGaugeMax; //FillAmount 지정
            gaugeImage.color = bazookaUltGauge < bazookaUltGaugeMax ? ultGaugeBasicColor : ultGaugeFullColor; //색상 지정
        }
        else if (currentUlt == Weapon.Wizard) //Wizard
        {
            gaugeImage.fillAmount = wizardUltGauge / wizardUltGaugeMax; //FillAmount 지정
            gaugeImage.color = wizardUltGauge < wizardUltGaugeMax ? ultGaugeBasicColor : ultGaugeFullColor; //색상 지정
        }
    }

    /* Ult Gauge를 증가시키는 함수 */
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
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("Money")]
    public TextMeshProUGUI moneyTMP; //Money의 TMP 컴포넌트

    [Header("Panel")]
    public GameObject weaponPanel; //Weapon Panel 오브젝트
    public TextMeshProUGUI weaponButtonTextTMP; //Weapon Button Text의 TMP 컴포넌트
    public GameObject statPanel; //Stat Panel 오브젝트
    public TextMeshProUGUI statButtonTextTMP; //Stat Button Text의 TMP 컴포넌트
    private readonly Color32 panelButtonTextActivationColor = new Color32(255, 255, 255, 255); //Panel Button Text의 활성화 색상
    private readonly Color32 panelButtonTextDeactivationColor = new Color32(75, 75, 75, 255); //Panel Button Text의 비활성화 색상

    [Header("Weapon Select Buttons")]
    public Sprite selectedButtonSprite; //SelectedButton의 Sprite
    public Sprite unselectedButtonSprite; //UnselectedButton의 Sprite

    public Image fistSelectButtonImage; //Fist Select Button의 Image 컴포넌트
    public Image swordSelectButtonImage; //Sword Select Button의 Image 컴포넌트
    public Image gunSelectButtonImage; //Gun Select Button의 Image 컴포넌트
    public Image sniperSelectButtonImage; //Sniper Select Button의 Image 컴포넌트
    public Image bazookaSelectButtonImage; //Bazooka Select Button의 Image 컴포넌트
    public Image wizardSelectButtonImage; //Wizard Select Button의 Image 컴포넌트

    private int selectedWeaponCount
    {
        get
        {
            int count = 0;
            count = ShopInfo.fistSelected ? count + 1 : count;
            count = ShopInfo.swordSelected ? count + 1 : count;
            count = ShopInfo.gunSelected ? count + 1 : count;
            count = ShopInfo.sniperSelected ? count + 1 : count;
            count = ShopInfo.bazookaSelected ? count + 1 : count;
            count = ShopInfo.wizardSelected ? count + 1 : count;

            return count;
        }
    } //선택된 무기의 개수

    [Header("Weapon Buttons")]
    private int _selectedWeapon;
    public int selectedWeapon
    {
        get
        {
            return _selectedWeapon;
        }
        set
        {
            for (int i = 0; i < weaponIndicators.Length; ++i) weaponIndicators[i].SetActive(false); //모든 Indicator 비활성화
            weaponIndicators[value].SetActive(true); //선택된 Indicator 활성화

            switch (value)
            {
                case (int)Weapon.Fist:
                    weaponPriceTMP.text = string.Format("{0:#,0}", Definition.fistBuyPrice); //Weapon 가격 초기화

                    weaponBuying.SetActive(ShopInfo.fistBuying); //Weapon Buying 활성화/비활성화
                    weaponNoneBuying.SetActive(!ShopInfo.fistBuying); //Weapon NoneBuying 활성화/비활성화

                    SetWeaponAttackUpgradeStep(ShopInfo.fistAttackLevel); //Weapon Attack Upgrade Step 지정
                    SetWeaponUltUpgradeStep(ShopInfo.fistUltLevel); //Weapon Ult Upgrade Step 지정

                    weaponAttackUpgradePriceTMP.text = ShopInfo.fistAttackLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.fistAttackUpgradePrice[ShopInfo.fistAttackLevel]) : "MAX"; //Weapon Attack Upgrade 가격 동기화
                    weaponUltUpgradePriceTMP.text = ShopInfo.fistUltLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.fistUltUpgradePrice[ShopInfo.fistUltLevel]) : "MAX"; //Weapon Ult Upgrade 가격 동기화

                    break;
                case (int)Weapon.Sword:
                    weaponPriceTMP.text = string.Format("{0:#,0}", Definition.swordBuyPrice); //Weapon 가격 초기화

                    weaponBuying.SetActive(ShopInfo.swordBuying); //Weapon Buying 활성화/비활성화
                    weaponNoneBuying.SetActive(!ShopInfo.swordBuying); //Weapon NoneBuying 활성화/비활성화

                    SetWeaponAttackUpgradeStep(ShopInfo.swordAttackLevel); //Weapon Attack Upgrade Step 지정
                    SetWeaponUltUpgradeStep(ShopInfo.swordUltLevel); //Weapon Ult Upgrade Step 지정

                    weaponAttackUpgradePriceTMP.text = ShopInfo.swordAttackLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.swordAttackUpgradePrice[ShopInfo.swordAttackLevel]) : "MAX"; //Weapon Attack Upgrade 가격 동기화
                    weaponUltUpgradePriceTMP.text = ShopInfo.swordUltLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.swordUltUpgradePrice[ShopInfo.swordUltLevel]) : "MAX"; //Weapon Ult Upgrade 가격 동기화

                    break;
                case (int)Weapon.Gun:
                    weaponPriceTMP.text = string.Format("{0:#,0}", Definition.gunBuyPrice); //Weapon 가격 초기화

                    weaponBuying.SetActive(ShopInfo.gunBuying); //Weapon Buying 활성화/비활성화
                    weaponNoneBuying.SetActive(!ShopInfo.gunBuying); //Weapon NoneBuying 활성화/비활성화

                    SetWeaponAttackUpgradeStep(ShopInfo.gunAttackLevel); //Weapon Attack Upgrade Step 지정
                    SetWeaponUltUpgradeStep(ShopInfo.gunUltLevel); //Weapon Ult Upgrade Step 지정

                    weaponAttackUpgradePriceTMP.text = ShopInfo.gunAttackLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.gunAttackUpgradePrice[ShopInfo.gunAttackLevel]) : "MAX"; //Weapon Attack Upgrade 가격 동기화
                    weaponUltUpgradePriceTMP.text = ShopInfo.gunUltLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.gunUltUpgradePrice[ShopInfo.gunUltLevel]) : "MAX"; //Weapon Ult Upgrade 가격 동기화

                    break;
                case (int)Weapon.Sniper:
                    weaponPriceTMP.text = string.Format("{0:#,0}", Definition.sniperBuyPrice); //Weapon 가격 초기화

                    weaponBuying.SetActive(ShopInfo.sniperBuying); //Weapon Buying 활성화/비활성화
                    weaponNoneBuying.SetActive(!ShopInfo.sniperBuying); //Weapon NoneBuying 활성화/비활성화

                    SetWeaponAttackUpgradeStep(ShopInfo.sniperAttackLevel); //Weapon Attack Upgrade Step 지정
                    SetWeaponUltUpgradeStep(ShopInfo.sniperUltLevel); //Weapon Ult Upgrade Step 지정

                    weaponAttackUpgradePriceTMP.text = ShopInfo.sniperAttackLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.sniperAttackUpgradePrice[ShopInfo.sniperAttackLevel]) : "MAX"; //Weapon Attack Upgrade 가격 동기화
                    weaponUltUpgradePriceTMP.text = ShopInfo.sniperUltLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.sniperUltUpgradePrice[ShopInfo.sniperUltLevel]) : "MAX"; //Weapon Ult Upgrade 가격 동기화

                    break;
                case (int)Weapon.Bazooka:
                    weaponPriceTMP.text = string.Format("{0:#,0}", Definition.bazookaBuyPrice); //Weapon 가격 초기화

                    weaponBuying.SetActive(ShopInfo.bazookaBuying); //Weapon Buying 활성화/비활성화
                    weaponNoneBuying.SetActive(!ShopInfo.bazookaBuying); //Weapon NoneBuying 활성화/비활성화

                    SetWeaponAttackUpgradeStep(ShopInfo.bazookaAttackLevel); //Weapon Attack Upgrade Step 지정
                    SetWeaponUltUpgradeStep(ShopInfo.bazookaUltLevel); //Weapon Ult Upgrade Step 지정

                    weaponAttackUpgradePriceTMP.text = ShopInfo.bazookaAttackLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.bazookaAttackUpgradePrice[ShopInfo.bazookaAttackLevel]) : "MAX"; //Weapon Attack Upgrade 가격 동기화
                    weaponUltUpgradePriceTMP.text = ShopInfo.bazookaUltLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.bazookaUltUpgradePrice[ShopInfo.bazookaUltLevel]) : "MAX"; //Weapon Ult Upgrade 가격 동기화

                    break;
                case (int)Weapon.Wizard:
                    weaponPriceTMP.text = string.Format("{0:#,0}", Definition.wizardBuyPrice); //Weapon 가격 초기화

                    weaponBuying.SetActive(ShopInfo.wizardBuying); //Weapon Buying 활성화/비활성화
                    weaponNoneBuying.SetActive(!ShopInfo.wizardBuying); //Weapon NoneBuying 활성화/비활성화

                    SetWeaponAttackUpgradeStep(ShopInfo.wizardAttackLevel); //Weapon Attack Upgrade Step 지정
                    SetWeaponUltUpgradeStep(ShopInfo.wizardUltLevel); //Weapon Ult Upgrade Step 지정

                    weaponAttackUpgradePriceTMP.text = ShopInfo.wizardAttackLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.wizardAttackUpgradePrice[ShopInfo.wizardAttackLevel]) : "MAX"; //Weapon Attack Upgrade 가격 동기화
                    weaponUltUpgradePriceTMP.text = ShopInfo.wizardUltLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.wizardUltUpgradePrice[ShopInfo.wizardUltLevel]) : "MAX"; //Weapon Ult Upgrade 가격 동기화

                    break;
            }

            _selectedWeapon = value; //값 지정
        }
    } //선택된 Weapon
    public GameObject[] weaponIndicators; //Weapon Indicator 오브젝트들

    [Header("Weapon Buying")]
    public GameObject weaponBuying; //Weapon Buying 오브젝트
    public GameObject weaponNoneBuying; //Weapon None Buying 오브젝트
    public TextMeshProUGUI weaponPriceTMP; //Weapon Price의 TMP 컴포넌트

    [Header("Weapon Upgrade")]
    public GameObject[] weaponAttackStepActivations; //Weapon Attack의 Step Activation 오브젝트들
    public GameObject[] weaponAttackStepDeactivations; //Weapon Attack의 Step Deactivation 오브젝트들
    public TextMeshProUGUI weaponAttackUpgradePriceTMP; //Weapon Attack의 Upgrade Price의 TMP 컴포넌트

    public GameObject[] weaponUltStepActivations; //Weapon Ult의 Step Activation 오브젝트들
    public GameObject[] weaponUltStepDeactivations; //Weapon Ult의 Step Deactivation 오브젝트들
    public TextMeshProUGUI weaponUltUpgradePriceTMP; //Weapon Ult의 Upgrade Price의 TMP 컴포넌트

    [Header("Stat Buttons")]
    private int _selectedStat; 
    public int selectedStat
    {
        get
        {
            return _selectedStat;
        }
        set
        {
            for (int i = 0; i < statButtonSelectedImages.Length; ++i) statButtonSelectedImages[i].SetActive(false); //모든 Selected Image 비활성화
            statButtonSelectedImages[value].SetActive(true); //선택된 Selected Image 활성화

            for (int i = 0; i < statUpgradeLabelTexts.Length; ++i) statUpgradeLabelTexts[i].SetActive(false); //모든 Upgrade Label 비활성화
            statUpgradeLabelTexts[value].SetActive(true); //선택된 Upgrade Label 활성화

            switch (value)
            {
                case (int)Stat.maxHP:
                    SetStatUpgradeStep(ShopInfo.maxHPLevel);
                    statUpgradeButtonPriceTMP.text = ShopInfo.maxHPLevel < Definition.statUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.maxHPUpgradePrice[ShopInfo.maxHPLevel]) : "MAX";
                    break;
                case (int)Stat.recoveryHP:
                    SetStatUpgradeStep(ShopInfo.recoveryHPLevel);
                    statUpgradeButtonPriceTMP.text = ShopInfo.recoveryHPLevel < Definition.statUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.recoveryHPUpgradePrice[ShopInfo.recoveryHPLevel]) : "MAX";
                    break;
                case (int)Stat.defense:
                    SetStatUpgradeStep(ShopInfo.defenseLevel);
                    statUpgradeButtonPriceTMP.text = ShopInfo.defenseLevel < Definition.statUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.defenseUpgradePrice[ShopInfo.defenseLevel]) : "MAX";
                    break;
                case (int)Stat.moveSpeed:
                    SetStatUpgradeStep(ShopInfo.moveSpeedLevel);
                    statUpgradeButtonPriceTMP.text = ShopInfo.moveSpeedLevel < Definition.statUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.speedUpgradePrice[ShopInfo.moveSpeedLevel]) : "MAX";
                    break;
                case (int)Stat.moneyAmount:
                    SetStatUpgradeStep(ShopInfo.moneyAmountLevel);
                    statUpgradeButtonPriceTMP.text = ShopInfo.moneyAmountLevel < Definition.statUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.moneyAmountUpgradePrice[ShopInfo.moneyAmountLevel]) : "MAX";
                    break;
                case (int)Stat.moneyProbability:
                    SetStatUpgradeStep(ShopInfo.moneyProbabilityLevel);
                    statUpgradeButtonPriceTMP.text = ShopInfo.moneyProbabilityLevel < Definition.statUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.moneyProbabilityUpgradePrice[ShopInfo.moneyProbabilityLevel]) : "MAX";
                    break;
                case (int)Stat.criticalDamage:
                    SetStatUpgradeStep(ShopInfo.criticalDamageLevel);
                    statUpgradeButtonPriceTMP.text = ShopInfo.criticalDamageLevel < Definition.statUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.criticalDamageUpgradePrice[ShopInfo.criticalDamageLevel]) : "MAX";
                    break;
                case (int)Stat.criticalProbability:
                    SetStatUpgradeStep(ShopInfo.criticalProbabilityLevel);
                    statUpgradeButtonPriceTMP.text = ShopInfo.criticalProbabilityLevel < Definition.statUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.criticalProbabilityUpgradePrice[ShopInfo.criticalProbabilityLevel]) : "MAX";
                    break;
            } //Upgrade Step 및 Upgrade Price 동기화

            _selectedStat = value; //값 지정
        }
    } //선택된 Stat

    public Image[] statButtonPercentImages; //Stat Button의 PercentImage의 Image 컴포넌트들
    private readonly Color32 statButtonLowLevelColor = new Color32(255, 255, 0, 65); //Stat Button의 Low Level 색상
    private readonly Color32 statButtonHighLevelColor = new Color32(255, 0, 0, 65); //Stat Button의 High Level 색상

    public GameObject[] statButtonSelectedImages; //Stat Button의 SelectedImage 오브젝트들
    public TextMeshProUGUI[] statButtonLevelTextTMPs; //Stat Button의 LevelText의 TMP 컴포넌트들

    [Header("Stat Upgrade")]
    public GameObject[] statUpgradeLabelTexts; //Stat Upgrade의 LabelText 오브젝트들
    public GameObject[] statUpgradeStepActivations; //Stat Upgrade의 Step Activation 오브젝트들
    public GameObject[] statUpgradeStepDeactivations; //Stat Upgrade의 Step Deactivation 오브젝트들
    public TextMeshProUGUI statUpgradeButtonPriceTMP; //Stat Upgrade Button의 Price의 TMP 컴포넌트

    [Header("Cache")]
    private float preMoney = -1; //이전 Money

    private void Start()
    {
        OnClickPanelButton(true);

        SyncWeaponSelectButtons();
        selectedWeapon = (int)ShopInfo.clickedWeaponButton;

        SyncStatButtonLevel();
        selectedStat = (int)ShopInfo.clickedStatButton;
    }

    private void Update()
    {
        SyncMoney();
    }

    /* ShopInfo를 초기화하는 함수 */
    public static void ResetShopInfo()
    {
        ShopInfo.money = 0;
        ShopInfo.profit = 0;
        ShopInfo.kill = 0;

        ShopInfo.fistSelected = true;
        ShopInfo.fistBuying = false;
        ShopInfo.fistAttackLevel = 0;
        ShopInfo.fistUltLevel = 0;

        ShopInfo.swordSelected = false;
        ShopInfo.swordBuying = false;
        ShopInfo.swordAttackLevel = 0;
        ShopInfo.swordUltLevel = 0;

        ShopInfo.gunSelected = false;
        ShopInfo.gunBuying = false;
        ShopInfo.gunAttackLevel = 0;
        ShopInfo.gunUltLevel = 0;

        ShopInfo.sniperSelected = false;
        ShopInfo.sniperBuying = false;
        ShopInfo.sniperAttackLevel = 0;
        ShopInfo.sniperUltLevel = 0;

        ShopInfo.bazookaSelected = false;
        ShopInfo.bazookaBuying = false;
        ShopInfo.bazookaAttackLevel = 0;
        ShopInfo.bazookaUltLevel = 0;

        ShopInfo.wizardSelected = false;
        ShopInfo.wizardBuying = false;
        ShopInfo.wizardAttackLevel = 0;
        ShopInfo.wizardUltLevel = 0;

        ShopInfo.maxHPLevel = 0;
        ShopInfo.recoveryHPLevel = 0;
        ShopInfo.defenseLevel = 0;
        ShopInfo.moveSpeedLevel = 0;
        ShopInfo.moneyAmountLevel = 0;
        ShopInfo.moneyProbabilityLevel = 0;
        ShopInfo.criticalDamageLevel = 0;
        ShopInfo.criticalProbabilityLevel = 0;
    }

    #region Money
    /* Money를 동기화하는 함수 */
    private void SyncMoney()
    {
        if (preMoney != ShopInfo.money) //이전 Money와 현재 Money가 다르면
        {
            moneyTMP.text = string.Format("{0:#,0}", ShopInfo.money); //Money Text 갱신
            preMoney = ShopInfo.money; //이전 Money 저장
        }
    }
    #endregion

    /* Panel 버튼을 클릭했을 때 실행되는 함수 */
    public void OnClickPanelButton(bool isWeaponButton)
    {
        weaponPanel.SetActive(isWeaponButton);
        weaponButtonTextTMP.color = isWeaponButton ? panelButtonTextActivationColor : panelButtonTextDeactivationColor;
        statPanel.SetActive(!isWeaponButton);
        statButtonTextTMP.color = !isWeaponButton ? panelButtonTextActivationColor : panelButtonTextDeactivationColor;
    }

    #region Weapon Select Buttons
    /* Weapon Select Button들을 동기화하는 함수 */
    private void SyncWeaponSelectButtons()
    {
        fistSelectButtonImage.sprite = ShopInfo.fistSelected ? selectedButtonSprite : unselectedButtonSprite;
        swordSelectButtonImage.sprite = ShopInfo.swordSelected ? selectedButtonSprite : unselectedButtonSprite;
        gunSelectButtonImage.sprite = ShopInfo.gunSelected ? selectedButtonSprite : unselectedButtonSprite;
        sniperSelectButtonImage.sprite = ShopInfo.sniperSelected ? selectedButtonSprite : unselectedButtonSprite;
        bazookaSelectButtonImage.sprite = ShopInfo.bazookaSelected ? selectedButtonSprite : unselectedButtonSprite;
        wizardSelectButtonImage.sprite = ShopInfo.wizardSelected ? selectedButtonSprite : unselectedButtonSprite;
    }

    /* Weapon Select Button을 클릭했을 때 실행되는 함수 */
    public void OnClickWeaponSelectButton(int index)
    {
        if(index == (int)Weapon.Fist) //Fist Select Button이면
        {
            if (selectedWeaponCount < 2 && !ShopInfo.fistSelected && ShopInfo.fistBuying) //불가능한 상태가 아니면
            {
                ShopInfo.fistSelected = true; //선택 변경
            }
            else if (selectedWeaponCount > 1 && ShopInfo.fistSelected) //불가능한 상태가 아니면
            {
                ShopInfo.fistSelected = false; //선택 변경
            }
        }
        if (index == (int)Weapon.Sword) //Sword Select Button이면
        {
            if (selectedWeaponCount < 2 && !ShopInfo.swordSelected && ShopInfo.swordBuying) //불가능한 상태가 아니면
            {
                ShopInfo.swordSelected = true; //선택 변경
            }
            else if (selectedWeaponCount > 1 && ShopInfo.swordSelected) //불가능한 상태가 아니면
            {
                ShopInfo.swordSelected = false; //선택 변경
            }
        }
        if (index == (int)Weapon.Gun) //Gun Select Button이면
        {
            if (selectedWeaponCount < 2 && !ShopInfo.gunSelected && ShopInfo.gunBuying) //불가능한 상태가 아니면
            {
                ShopInfo.gunSelected = true; //선택 변경
            }
            else if (selectedWeaponCount > 1 && ShopInfo.gunSelected) //불가능한 상태가 아니면
            {
                ShopInfo.gunSelected = false; //선택 변경
            }
        }
        if (index == (int)Weapon.Sniper) //Sniper Select Button이면
        {
            if (selectedWeaponCount < 2 && !ShopInfo.sniperSelected && ShopInfo.sniperBuying) //불가능한 상태가 아니면
            {
                ShopInfo.sniperSelected = true; //선택 변경
            }
            else if (selectedWeaponCount > 1 && ShopInfo.sniperSelected) //불가능한 상태가 아니면
            {
                ShopInfo.sniperSelected = false; //선택 변경
            }
        }
        if (index == (int)Weapon.Bazooka) //Bazooka Select Button이면
        {
            if (selectedWeaponCount < 2 && !ShopInfo.bazookaSelected && ShopInfo.bazookaBuying) //불가능한 상태가 아니면
            {
                ShopInfo.bazookaSelected = true; //선택 변경
            }
            else if (selectedWeaponCount > 1 && ShopInfo.bazookaSelected) //불가능한 상태가 아니면
            {
                ShopInfo.bazookaSelected = false; //선택 변경
            }
        }
        if (index == (int)Weapon.Wizard) //Wizard Select Button이면
        {
            if (selectedWeaponCount < 2 && !ShopInfo.wizardSelected && ShopInfo.wizardBuying) //불가능한 상태가 아니면
            {
                ShopInfo.wizardSelected = true; //선택 변경
            }
            else if (selectedWeaponCount > 1 && ShopInfo.wizardSelected) //불가능한 상태가 아니면
            {
                ShopInfo.wizardSelected = false; //선택 변경
            }
        }

        SyncWeaponSelectButtons(); //Selected Button들 동기화
    }
    #endregion

    #region Weapon

    /* Weapon Buy Button을 클릭했을 때 실행되는 함수 */
    public void OnClickWeaponBuyButton()
    {
        if(selectedWeapon == (int)Weapon.Fist)
        {
            if (ShopInfo.money >= Definition.fistBuyPrice) //구매가 가능하면
            {
                ShopInfo.money -= Definition.fistBuyPrice; //돈 감소
                ShopInfo.fistBuying = true; //구매 처리

                selectedWeapon = (int)Weapon.Fist; //선택된 Weapon 변경
            }
        }
        else if (selectedWeapon == (int)Weapon.Sword)
        {
            if (ShopInfo.money >= Definition.swordBuyPrice) //구매가 가능하면
            {
                ShopInfo.money -= Definition.swordBuyPrice; //돈 감소
                ShopInfo.swordBuying = true; //구매 처리

                selectedWeapon = (int)Weapon.Sword; //선택된 Weapon 변경
            }
        }
        else if (selectedWeapon == (int)Weapon.Gun)
        {
            if (ShopInfo.money >= Definition.gunBuyPrice) //구매가 가능하면
            {
                ShopInfo.money -= Definition.gunBuyPrice; //돈 감소
                ShopInfo.gunBuying = true; //구매 처리

                selectedWeapon = (int)Weapon.Gun; //선택된 Weapon 변경
            }
        }
        else if (selectedWeapon == (int)Weapon.Sniper)
        {
            if (ShopInfo.money >= Definition.sniperBuyPrice) //구매가 가능하면
            {
                ShopInfo.money -= Definition.sniperBuyPrice; //돈 감소
                ShopInfo.sniperBuying = true; //구매 처리

                selectedWeapon = (int)Weapon.Sniper; //선택된 Weapon 변경
            }
        }
        else if (selectedWeapon == (int)Weapon.Bazooka)
        {
            if (ShopInfo.money >= Definition.bazookaBuyPrice) //구매가 가능하면
            {
                ShopInfo.money -= Definition.bazookaBuyPrice; //돈 감소
                ShopInfo.bazookaBuying = true; //구매 처리

                selectedWeapon = (int)Weapon.Bazooka; //선택된 Weapon 변경
            }
        }
        else if (selectedWeapon == (int)Weapon.Wizard)
        {
            if (ShopInfo.money >= Definition.wizardBuyPrice) //구매가 가능하면
            {
                ShopInfo.money -= Definition.wizardBuyPrice; //돈 감소
                ShopInfo.wizardBuying = true; //구매 처리

                selectedWeapon = (int)Weapon.Wizard; //선택된 Weapon 변경
            }
        }
    }

    /* Weapon Attack Upgrade Step을 지정하는 함수 */
    private void SetWeaponAttackUpgradeStep(int step)
    {
        /* 모든 Step 비활성화 */
        for (int i = 0; i < Definition.weaponUpgradeMaxLevel; ++i)
        {
            weaponAttackStepActivations[i].SetActive(false);
            weaponAttackStepDeactivations[i].SetActive(false);
        }

        /* Step 만큼 Activation 활성화 */
        for (int i = 0; i < Definition.weaponUpgradeMaxLevel; ++i)
        {
            if (step > 0) weaponAttackStepActivations[i].SetActive(true);
            else weaponAttackStepDeactivations[i].SetActive(true);
            --step;
        }
    }

    /* Weapon Ult Upgrade Step을 지정하는 함수 */
    private void SetWeaponUltUpgradeStep(int step)
    {
        /* 모든 Step 비활성화 */
        for (int i = 0; i < Definition.weaponUpgradeMaxLevel; ++i)
        {
            weaponUltStepActivations[i].SetActive(false);
            weaponUltStepDeactivations[i].SetActive(false);
        }

        /* Step 만큼 Activation 활성화 */
        for (int i = 0; i < Definition.weaponUpgradeMaxLevel; ++i)
        {
            if (step > 0) weaponUltStepActivations[i].SetActive(true);
            else weaponUltStepDeactivations[i].SetActive(true);
            --step;
        }
    }

    /* Weapon Attack의 Upgrade Button을 클릭했을 때 실행되는 함수 */
    public void OnClickWeaponAttackUpgradeButton()
    {
        if (selectedWeapon == (int)Weapon.Fist)
        {
            if (ShopInfo.fistAttackLevel < Definition.weaponUpgradeMaxLevel && Definition.fistAttackUpgradePrice[ShopInfo.fistAttackLevel] <= ShopInfo.money) //Upgrade가 가능하면
            {
                ShopInfo.money -= Definition.fistAttackUpgradePrice[ShopInfo.fistAttackLevel]; //Money 감소
                ++ShopInfo.fistAttackLevel; //레벨 증가
                selectedWeapon = (int)Weapon.Fist; //선택된 Weapon 변경
            }
        }
        else if (selectedWeapon == (int)Weapon.Sword)
        {
            if (ShopInfo.swordAttackLevel < Definition.weaponUpgradeMaxLevel && Definition.swordAttackUpgradePrice[ShopInfo.swordAttackLevel] <= ShopInfo.money) //Upgrade가 가능하면
            {
                ShopInfo.money -= Definition.swordAttackUpgradePrice[ShopInfo.swordAttackLevel]; //Money 감소
                ++ShopInfo.swordAttackLevel; //레벨 증가
                selectedWeapon = (int)Weapon.Sword; //선택된 Weapon 변경
            }
        }
        else if (selectedWeapon == (int)Weapon.Gun)
        {
            if (ShopInfo.gunAttackLevel < Definition.weaponUpgradeMaxLevel && Definition.gunAttackUpgradePrice[ShopInfo.gunAttackLevel] <= ShopInfo.money) //Upgrade가 가능하면
            {
                ShopInfo.money -= Definition.gunAttackUpgradePrice[ShopInfo.gunAttackLevel]; //Money 감소
                ++ShopInfo.gunAttackLevel; //레벨 증가
                selectedWeapon = (int)Weapon.Gun; //선택된 Weapon 변경
            }
        }
        else if (selectedWeapon == (int)Weapon.Sniper)
        {
            if (ShopInfo.sniperAttackLevel < Definition.weaponUpgradeMaxLevel && Definition.sniperAttackUpgradePrice[ShopInfo.sniperAttackLevel] <= ShopInfo.money) //Upgrade가 가능하면
            {
                ShopInfo.money -= Definition.sniperAttackUpgradePrice[ShopInfo.sniperAttackLevel]; //Money 감소
                ++ShopInfo.sniperAttackLevel; //레벨 증가
                selectedWeapon = (int)Weapon.Sniper; //선택된 Weapon 변경
            }
        }
        else if (selectedWeapon == (int)Weapon.Bazooka)
        {
            if (ShopInfo.bazookaAttackLevel < Definition.weaponUpgradeMaxLevel && Definition.bazookaAttackUpgradePrice[ShopInfo.bazookaAttackLevel] <= ShopInfo.money) //Upgrade가 가능하면
            {
                ShopInfo.money -= Definition.bazookaAttackUpgradePrice[ShopInfo.bazookaAttackLevel]; //Money 감소
                ++ShopInfo.bazookaAttackLevel; //레벨 증가
                selectedWeapon = (int)Weapon.Bazooka; //선택된 Weapon 변경
            }
        }
        else if (selectedWeapon == (int)Weapon.Wizard)
        {
            if (ShopInfo.wizardAttackLevel < Definition.weaponUpgradeMaxLevel && Definition.wizardAttackUpgradePrice[ShopInfo.wizardAttackLevel] <= ShopInfo.money) //Upgrade가 가능하면
            {
                ShopInfo.money -= Definition.wizardAttackUpgradePrice[ShopInfo.wizardAttackLevel]; //Money 감소
                ++ShopInfo.wizardAttackLevel; //레벨 증가
                selectedWeapon = (int)Weapon.Wizard; //선택된 Weapon 변경
            }
        }
    }

    /* Weapon Ult의 Upgrade Button을 클릭했을 때 실행되는 함수 */
    public void OnClickWeaponUltUpgradeButton()
    {
        if (selectedWeapon == (int)Weapon.Fist)
        {
            if (ShopInfo.fistUltLevel < Definition.weaponUpgradeMaxLevel && Definition.fistUltUpgradePrice[ShopInfo.fistUltLevel] <= ShopInfo.money) //Upgrade가 가능하면
            {
                ShopInfo.money -= Definition.fistUltUpgradePrice[ShopInfo.fistUltLevel]; //Money 감소
                ++ShopInfo.fistUltLevel; //레벨 증가
                selectedWeapon = (int)Weapon.Fist; //선택된 Weapon 변경
            }
        }
        else if (selectedWeapon == (int)Weapon.Sword)
        {
            if (ShopInfo.swordUltLevel < Definition.weaponUpgradeMaxLevel && Definition.swordUltUpgradePrice[ShopInfo.swordUltLevel] <= ShopInfo.money) //Upgrade가 가능하면
            {
                ShopInfo.money -= Definition.swordUltUpgradePrice[ShopInfo.swordUltLevel]; //Money 감소
                ++ShopInfo.swordUltLevel; //레벨 증가
                selectedWeapon = (int)Weapon.Sword; //선택된 Weapon 변경
            }
        }
        else if (selectedWeapon == (int)Weapon.Gun)
        {
            if (ShopInfo.gunUltLevel < Definition.weaponUpgradeMaxLevel && Definition.gunUltUpgradePrice[ShopInfo.gunUltLevel] <= ShopInfo.money) //Upgrade가 가능하면
            {
                ShopInfo.money -= Definition.gunUltUpgradePrice[ShopInfo.gunUltLevel]; //Money 감소
                ++ShopInfo.gunUltLevel; //레벨 증가
                selectedWeapon = (int)Weapon.Gun; //선택된 Weapon 변경
            }
        }
        else if (selectedWeapon == (int)Weapon.Sniper)
        {
            if (ShopInfo.sniperUltLevel < Definition.weaponUpgradeMaxLevel && Definition.sniperUltUpgradePrice[ShopInfo.sniperUltLevel] <= ShopInfo.money) //Upgrade가 가능하면
            {
                ShopInfo.money -= Definition.sniperUltUpgradePrice[ShopInfo.sniperUltLevel]; //Money 감소
                ++ShopInfo.sniperUltLevel; //레벨 증가
                selectedWeapon = (int)Weapon.Sniper; //선택된 Weapon 변경
            }
        }
        else if (selectedWeapon == (int)Weapon.Bazooka)
        {
            if (ShopInfo.bazookaUltLevel < Definition.weaponUpgradeMaxLevel && Definition.bazookaUltUpgradePrice[ShopInfo.bazookaUltLevel] <= ShopInfo.money) //Upgrade가 가능하면
            {
                ShopInfo.money -= Definition.bazookaUltUpgradePrice[ShopInfo.bazookaUltLevel]; //Money 감소
                ++ShopInfo.bazookaUltLevel; //레벨 증가
                selectedWeapon = (int)Weapon.Bazooka; //선택된 Weapon 변경
            }
        }
        else if (selectedWeapon == (int)Weapon.Wizard)
        {
            if (ShopInfo.wizardUltLevel < Definition.weaponUpgradeMaxLevel && Definition.wizardUltUpgradePrice[ShopInfo.wizardUltLevel] <= ShopInfo.money) //Upgrade가 가능하면
            {
                ShopInfo.money -= Definition.wizardUltUpgradePrice[ShopInfo.wizardUltLevel]; //Money 감소
                ++ShopInfo.wizardUltLevel; //레벨 증가
                selectedWeapon = (int)Weapon.Wizard; //선택된 Weapon 변경
            }
        }
    }
    #endregion

    #region Stat
    /* Stat Button의 Level을 동기화하는 함수 */
    private void SyncStatButtonLevel()
    {
        statButtonPercentImages[(int)Stat.maxHP].color = Color.Lerp(statButtonLowLevelColor, statButtonHighLevelColor, (float)ShopInfo.maxHPLevel / Definition.statUpgradeMaxLevel);
        statButtonPercentImages[(int)Stat.maxHP].fillAmount = (float)ShopInfo.maxHPLevel / Definition.statUpgradeMaxLevel; //PercentImage의 FillAmount 변경
        statButtonLevelTextTMPs[(int)Stat.maxHP].text = ShopInfo.maxHPLevel < Definition.statUpgradeMaxLevel ? "Lv " + ShopInfo.maxHPLevel : "Max Lv"; //LevelText의 Text 변경

        statButtonPercentImages[(int)Stat.recoveryHP].color = Color.Lerp(statButtonLowLevelColor, statButtonHighLevelColor, (float)ShopInfo.recoveryHPLevel / Definition.statUpgradeMaxLevel);
        statButtonPercentImages[(int)Stat.recoveryHP].fillAmount = (float)ShopInfo.recoveryHPLevel / Definition.statUpgradeMaxLevel; //PercentImage의 FillAmount 변경
        statButtonLevelTextTMPs[(int)Stat.recoveryHP].text = ShopInfo.recoveryHPLevel < Definition.statUpgradeMaxLevel ? "Lv " + ShopInfo.recoveryHPLevel : "Max Lv"; //LevelText의 Text 변경

        statButtonPercentImages[(int)Stat.defense].color = Color.Lerp(statButtonLowLevelColor, statButtonHighLevelColor, (float)ShopInfo.defenseLevel / Definition.statUpgradeMaxLevel);
        statButtonPercentImages[(int)Stat.defense].fillAmount = (float)ShopInfo.defenseLevel / Definition.statUpgradeMaxLevel; //PercentImage의 FillAmount 변경
        statButtonLevelTextTMPs[(int)Stat.defense].text = ShopInfo.defenseLevel < Definition.statUpgradeMaxLevel ? "Lv " + ShopInfo.defenseLevel : "Max Lv"; //LevelText의 Text 변경

        statButtonPercentImages[(int)Stat.moveSpeed].color = Color.Lerp(statButtonLowLevelColor, statButtonHighLevelColor, (float)ShopInfo.moveSpeedLevel / Definition.statUpgradeMaxLevel);
        statButtonPercentImages[(int)Stat.moveSpeed].fillAmount = (float)ShopInfo.moveSpeedLevel / Definition.statUpgradeMaxLevel; //PercentImage의 FillAmount 변경
        statButtonLevelTextTMPs[(int)Stat.moveSpeed].text = ShopInfo.moveSpeedLevel < Definition.statUpgradeMaxLevel ? "Lv " + ShopInfo.moveSpeedLevel : "Max Lv"; //LevelText의 Text 변경

        statButtonPercentImages[(int)Stat.moneyAmount].color = Color.Lerp(statButtonLowLevelColor, statButtonHighLevelColor, (float)ShopInfo.moneyAmountLevel / Definition.statUpgradeMaxLevel);
        statButtonPercentImages[(int)Stat.moneyAmount].fillAmount = (float)ShopInfo.moneyAmountLevel / Definition.statUpgradeMaxLevel; //PercentImage의 FillAmount 변경
        statButtonLevelTextTMPs[(int)Stat.moneyAmount].text = ShopInfo.moneyAmountLevel < Definition.statUpgradeMaxLevel ? "Lv " + ShopInfo.moneyAmountLevel : "Max Lv"; //LevelText의 Text 변경

        statButtonPercentImages[(int)Stat.moneyProbability].color = Color.Lerp(statButtonLowLevelColor, statButtonHighLevelColor, (float)ShopInfo.moneyProbabilityLevel / Definition.statUpgradeMaxLevel);
        statButtonPercentImages[(int)Stat.moneyProbability].fillAmount = (float)ShopInfo.moneyProbabilityLevel / Definition.statUpgradeMaxLevel; //PercentImage의 FillAmount 변경
        statButtonLevelTextTMPs[(int)Stat.moneyProbability].text = ShopInfo.moneyProbabilityLevel < Definition.statUpgradeMaxLevel ? "Lv " + ShopInfo.moneyProbabilityLevel : "Max Lv"; //LevelText의 Text 변경

        statButtonPercentImages[(int)Stat.criticalDamage].color = Color.Lerp(statButtonLowLevelColor, statButtonHighLevelColor, (float)ShopInfo.criticalDamageLevel / Definition.statUpgradeMaxLevel);
        statButtonPercentImages[(int)Stat.criticalDamage].fillAmount = (float)ShopInfo.criticalDamageLevel / Definition.statUpgradeMaxLevel; //PercentImage의 FillAmount 변경
        statButtonLevelTextTMPs[(int)Stat.criticalDamage].text = ShopInfo.criticalDamageLevel < Definition.statUpgradeMaxLevel ? "Lv " + ShopInfo.criticalDamageLevel : "Max Lv"; //LevelText의 Text 변경

        statButtonPercentImages[(int)Stat.criticalProbability].color = Color.Lerp(statButtonLowLevelColor, statButtonHighLevelColor, (float)ShopInfo.criticalProbabilityLevel / Definition.statUpgradeMaxLevel);
        statButtonPercentImages[(int)Stat.criticalProbability].fillAmount = (float)ShopInfo.criticalProbabilityLevel / Definition.statUpgradeMaxLevel; //PercentImage의 FillAmount 변경
        statButtonLevelTextTMPs[(int)Stat.criticalProbability].text = ShopInfo.criticalProbabilityLevel < Definition.statUpgradeMaxLevel ? "Lv " + ShopInfo.criticalProbabilityLevel : "Max Lv"; //LevelText의 Text 변경
    }

    /* Stat Upgrade Step을 지정하는 함수 */
    private void SetStatUpgradeStep(int step)
    {
        /* 모든 Step 비활성화 */
        for (int i = 0; i < Definition.statUpgradeMaxLevel; ++i)
        {
            statUpgradeStepActivations[i].SetActive(false);
            statUpgradeStepDeactivations[i].SetActive(false);
        }

        /* Step 만큼 Activation 활성화 */
        for (int i = 0; i < Definition.statUpgradeMaxLevel; ++i)
        {
            if (step > 0) statUpgradeStepActivations[i].SetActive(true);
            else statUpgradeStepDeactivations[i].SetActive(true);
            --step;
        }
    }

    /* Stat Upgrade Button을 클릭했을 때 실행되는 함수 */
    public void OnClickStatUpgradeButton()
    {
        if(selectedStat == (int)Stat.maxHP)
        {
            if(ShopInfo.maxHPLevel < Definition.statUpgradeMaxLevel && ShopInfo.money >= Definition.maxHPUpgradePrice[ShopInfo.maxHPLevel]) //업그레이드가 가능하면
            {
                ShopInfo.money -= Definition.maxHPUpgradePrice[ShopInfo.maxHPLevel]; //돈 감소

                ++ShopInfo.maxHPLevel; //레벨 증가
                SyncStatButtonLevel(); //Stat Button의 Level 동기화
                selectedStat = (int)Stat.maxHP; //선택된 Stat 변경
            }
        }
        else if (selectedStat == (int)Stat.recoveryHP)
        {
            if (ShopInfo.recoveryHPLevel < Definition.statUpgradeMaxLevel && ShopInfo.money >= Definition.recoveryHPUpgradePrice[ShopInfo.recoveryHPLevel]) //업그레이드가 가능하면
            {
                ShopInfo.money -= Definition.recoveryHPUpgradePrice[ShopInfo.recoveryHPLevel]; //돈 감소

                ++ShopInfo.recoveryHPLevel; //레벨 증가
                SyncStatButtonLevel(); //Stat Button의 Level 동기화
                selectedStat = (int)Stat.recoveryHP; //선택된 Stat 변경
            }
        }
        else if (selectedStat == (int)Stat.defense)
        {
            if (ShopInfo.defenseLevel < Definition.statUpgradeMaxLevel && ShopInfo.money >= Definition.defenseUpgradePrice[ShopInfo.defenseLevel]) //업그레이드가 가능하면
            {
                ShopInfo.money -= Definition.defenseUpgradePrice[ShopInfo.defenseLevel]; //돈 감소

                ++ShopInfo.defenseLevel; //레벨 증가
                SyncStatButtonLevel(); //Stat Button의 Level 동기화
                selectedStat = (int)Stat.defense; //선택된 Stat 변경
            }
        }
        else if (selectedStat == (int)Stat.moveSpeed)
        {
            if (ShopInfo.moveSpeedLevel < Definition.statUpgradeMaxLevel && ShopInfo.money >= Definition.speedUpgradePrice[ShopInfo.moveSpeedLevel]) //업그레이드가 가능하면
            {
                ShopInfo.money -= Definition.speedUpgradePrice[ShopInfo.moveSpeedLevel]; //돈 감소

                ++ShopInfo.moveSpeedLevel; //레벨 증가
                SyncStatButtonLevel(); //Stat Button의 Level 동기화
                selectedStat = (int)Stat.moveSpeed; //선택된 Stat 변경
            }
        }
        else if (selectedStat == (int)Stat.moneyAmount)
        {
            if (ShopInfo.moneyAmountLevel < Definition.statUpgradeMaxLevel && ShopInfo.money >= Definition.moneyAmountUpgradePrice[ShopInfo.moneyAmountLevel]) //업그레이드가 가능하면
            {
                ShopInfo.money -= Definition.moneyAmountUpgradePrice[ShopInfo.moneyAmountLevel]; //돈 감소

                ++ShopInfo.moneyAmountLevel; //레벨 증가
                SyncStatButtonLevel(); //Stat Button의 Level 동기화
                selectedStat = (int)Stat.moneyAmount; //선택된 Stat 변경
            }
        }
        else if (selectedStat == (int)Stat.moneyProbability)
        {
            if (ShopInfo.moneyProbabilityLevel < Definition.statUpgradeMaxLevel && ShopInfo.money >= Definition.moneyProbabilityUpgradePrice[ShopInfo.moneyProbabilityLevel]) //업그레이드가 가능하면
            {
                ShopInfo.money -= Definition.moneyProbabilityUpgradePrice[ShopInfo.moneyProbabilityLevel]; //돈 감소

                ++ShopInfo.moneyProbabilityLevel; //레벨 증가
                SyncStatButtonLevel(); //Stat Button의 Level 동기화
                selectedStat = (int)Stat.moneyProbability; //선택된 Stat 변경
            }
        }
        else if (selectedStat == (int)Stat.criticalDamage)
        {
            if (ShopInfo.criticalDamageLevel < Definition.statUpgradeMaxLevel && ShopInfo.money >= Definition.criticalDamageUpgradePrice[ShopInfo.criticalDamageLevel]) //업그레이드가 가능하면
            {
                ShopInfo.money -= Definition.criticalDamageUpgradePrice[ShopInfo.criticalDamageLevel]; //돈 감소

                ++ShopInfo.criticalDamageLevel; //레벨 증가
                SyncStatButtonLevel(); //Stat Button의 Level 동기화
                selectedStat = (int)Stat.criticalDamage; //선택된 Stat 변경
            }
        }
        else if (selectedStat == (int)Stat.criticalProbability)
        {
            if (ShopInfo.criticalProbabilityLevel < Definition.statUpgradeMaxLevel && ShopInfo.money >= Definition.criticalProbabilityUpgradePrice[ShopInfo.criticalProbabilityLevel]) //업그레이드가 가능하면
            {
                ShopInfo.money -= Definition.criticalProbabilityUpgradePrice[ShopInfo.criticalProbabilityLevel]; //돈 감소

                ++ShopInfo.criticalProbabilityLevel; //레벨 증가
                SyncStatButtonLevel(); //Stat Button의 Level 동기화
                selectedStat = (int)Stat.criticalProbability; //선택된 Stat 변경
            }
        }
    }
    #endregion
}
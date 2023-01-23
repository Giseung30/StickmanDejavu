using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("Money")]
    public TextMeshProUGUI moneyTMP; //Money�� TMP ������Ʈ

    [Header("Panel")]
    public GameObject weaponPanel; //Weapon Panel ������Ʈ
    public TextMeshProUGUI weaponButtonTextTMP; //Weapon Button Text�� TMP ������Ʈ
    public GameObject statPanel; //Stat Panel ������Ʈ
    public TextMeshProUGUI statButtonTextTMP; //Stat Button Text�� TMP ������Ʈ
    private readonly Color32 panelButtonTextActivationColor = new Color32(255, 255, 255, 255); //Panel Button Text�� Ȱ��ȭ ����
    private readonly Color32 panelButtonTextDeactivationColor = new Color32(75, 75, 75, 255); //Panel Button Text�� ��Ȱ��ȭ ����

    [Header("Weapon Select Buttons")]
    public Sprite selectedButtonSprite; //SelectedButton�� Sprite
    public Sprite unselectedButtonSprite; //UnselectedButton�� Sprite

    public Image fistSelectButtonImage; //Fist Select Button�� Image ������Ʈ
    public Image swordSelectButtonImage; //Sword Select Button�� Image ������Ʈ
    public Image gunSelectButtonImage; //Gun Select Button�� Image ������Ʈ
    public Image sniperSelectButtonImage; //Sniper Select Button�� Image ������Ʈ
    public Image bazookaSelectButtonImage; //Bazooka Select Button�� Image ������Ʈ
    public Image wizardSelectButtonImage; //Wizard Select Button�� Image ������Ʈ

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
    } //���õ� ������ ����

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
            for (int i = 0; i < weaponIndicators.Length; ++i) weaponIndicators[i].SetActive(false); //��� Indicator ��Ȱ��ȭ
            weaponIndicators[value].SetActive(true); //���õ� Indicator Ȱ��ȭ

            switch (value)
            {
                case (int)Weapon.Fist:
                    weaponPriceTMP.text = string.Format("{0:#,0}", Definition.fistBuyPrice); //Weapon ���� �ʱ�ȭ

                    weaponBuying.SetActive(ShopInfo.fistBuying); //Weapon Buying Ȱ��ȭ/��Ȱ��ȭ
                    weaponNoneBuying.SetActive(!ShopInfo.fistBuying); //Weapon NoneBuying Ȱ��ȭ/��Ȱ��ȭ

                    SetWeaponAttackUpgradeStep(ShopInfo.fistAttackLevel); //Weapon Attack Upgrade Step ����
                    SetWeaponUltUpgradeStep(ShopInfo.fistUltLevel); //Weapon Ult Upgrade Step ����

                    weaponAttackUpgradePriceTMP.text = ShopInfo.fistAttackLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.fistAttackUpgradePrice[ShopInfo.fistAttackLevel]) : "MAX"; //Weapon Attack Upgrade ���� ����ȭ
                    weaponUltUpgradePriceTMP.text = ShopInfo.fistUltLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.fistUltUpgradePrice[ShopInfo.fistUltLevel]) : "MAX"; //Weapon Ult Upgrade ���� ����ȭ

                    break;
                case (int)Weapon.Sword:
                    weaponPriceTMP.text = string.Format("{0:#,0}", Definition.swordBuyPrice); //Weapon ���� �ʱ�ȭ

                    weaponBuying.SetActive(ShopInfo.swordBuying); //Weapon Buying Ȱ��ȭ/��Ȱ��ȭ
                    weaponNoneBuying.SetActive(!ShopInfo.swordBuying); //Weapon NoneBuying Ȱ��ȭ/��Ȱ��ȭ

                    SetWeaponAttackUpgradeStep(ShopInfo.swordAttackLevel); //Weapon Attack Upgrade Step ����
                    SetWeaponUltUpgradeStep(ShopInfo.swordUltLevel); //Weapon Ult Upgrade Step ����

                    weaponAttackUpgradePriceTMP.text = ShopInfo.swordAttackLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.swordAttackUpgradePrice[ShopInfo.swordAttackLevel]) : "MAX"; //Weapon Attack Upgrade ���� ����ȭ
                    weaponUltUpgradePriceTMP.text = ShopInfo.swordUltLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.swordUltUpgradePrice[ShopInfo.swordUltLevel]) : "MAX"; //Weapon Ult Upgrade ���� ����ȭ

                    break;
                case (int)Weapon.Gun:
                    weaponPriceTMP.text = string.Format("{0:#,0}", Definition.gunBuyPrice); //Weapon ���� �ʱ�ȭ

                    weaponBuying.SetActive(ShopInfo.gunBuying); //Weapon Buying Ȱ��ȭ/��Ȱ��ȭ
                    weaponNoneBuying.SetActive(!ShopInfo.gunBuying); //Weapon NoneBuying Ȱ��ȭ/��Ȱ��ȭ

                    SetWeaponAttackUpgradeStep(ShopInfo.gunAttackLevel); //Weapon Attack Upgrade Step ����
                    SetWeaponUltUpgradeStep(ShopInfo.gunUltLevel); //Weapon Ult Upgrade Step ����

                    weaponAttackUpgradePriceTMP.text = ShopInfo.gunAttackLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.gunAttackUpgradePrice[ShopInfo.gunAttackLevel]) : "MAX"; //Weapon Attack Upgrade ���� ����ȭ
                    weaponUltUpgradePriceTMP.text = ShopInfo.gunUltLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.gunUltUpgradePrice[ShopInfo.gunUltLevel]) : "MAX"; //Weapon Ult Upgrade ���� ����ȭ

                    break;
                case (int)Weapon.Sniper:
                    weaponPriceTMP.text = string.Format("{0:#,0}", Definition.sniperBuyPrice); //Weapon ���� �ʱ�ȭ

                    weaponBuying.SetActive(ShopInfo.sniperBuying); //Weapon Buying Ȱ��ȭ/��Ȱ��ȭ
                    weaponNoneBuying.SetActive(!ShopInfo.sniperBuying); //Weapon NoneBuying Ȱ��ȭ/��Ȱ��ȭ

                    SetWeaponAttackUpgradeStep(ShopInfo.sniperAttackLevel); //Weapon Attack Upgrade Step ����
                    SetWeaponUltUpgradeStep(ShopInfo.sniperUltLevel); //Weapon Ult Upgrade Step ����

                    weaponAttackUpgradePriceTMP.text = ShopInfo.sniperAttackLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.sniperAttackUpgradePrice[ShopInfo.sniperAttackLevel]) : "MAX"; //Weapon Attack Upgrade ���� ����ȭ
                    weaponUltUpgradePriceTMP.text = ShopInfo.sniperUltLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.sniperUltUpgradePrice[ShopInfo.sniperUltLevel]) : "MAX"; //Weapon Ult Upgrade ���� ����ȭ

                    break;
                case (int)Weapon.Bazooka:
                    weaponPriceTMP.text = string.Format("{0:#,0}", Definition.bazookaBuyPrice); //Weapon ���� �ʱ�ȭ

                    weaponBuying.SetActive(ShopInfo.bazookaBuying); //Weapon Buying Ȱ��ȭ/��Ȱ��ȭ
                    weaponNoneBuying.SetActive(!ShopInfo.bazookaBuying); //Weapon NoneBuying Ȱ��ȭ/��Ȱ��ȭ

                    SetWeaponAttackUpgradeStep(ShopInfo.bazookaAttackLevel); //Weapon Attack Upgrade Step ����
                    SetWeaponUltUpgradeStep(ShopInfo.bazookaUltLevel); //Weapon Ult Upgrade Step ����

                    weaponAttackUpgradePriceTMP.text = ShopInfo.bazookaAttackLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.bazookaAttackUpgradePrice[ShopInfo.bazookaAttackLevel]) : "MAX"; //Weapon Attack Upgrade ���� ����ȭ
                    weaponUltUpgradePriceTMP.text = ShopInfo.bazookaUltLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.bazookaUltUpgradePrice[ShopInfo.bazookaUltLevel]) : "MAX"; //Weapon Ult Upgrade ���� ����ȭ

                    break;
                case (int)Weapon.Wizard:
                    weaponPriceTMP.text = string.Format("{0:#,0}", Definition.wizardBuyPrice); //Weapon ���� �ʱ�ȭ

                    weaponBuying.SetActive(ShopInfo.wizardBuying); //Weapon Buying Ȱ��ȭ/��Ȱ��ȭ
                    weaponNoneBuying.SetActive(!ShopInfo.wizardBuying); //Weapon NoneBuying Ȱ��ȭ/��Ȱ��ȭ

                    SetWeaponAttackUpgradeStep(ShopInfo.wizardAttackLevel); //Weapon Attack Upgrade Step ����
                    SetWeaponUltUpgradeStep(ShopInfo.wizardUltLevel); //Weapon Ult Upgrade Step ����

                    weaponAttackUpgradePriceTMP.text = ShopInfo.wizardAttackLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.wizardAttackUpgradePrice[ShopInfo.wizardAttackLevel]) : "MAX"; //Weapon Attack Upgrade ���� ����ȭ
                    weaponUltUpgradePriceTMP.text = ShopInfo.wizardUltLevel < Definition.weaponUpgradeMaxLevel ? string.Format("{0:#,0}", Definition.wizardUltUpgradePrice[ShopInfo.wizardUltLevel]) : "MAX"; //Weapon Ult Upgrade ���� ����ȭ

                    break;
            }

            _selectedWeapon = value; //�� ����
        }
    } //���õ� Weapon
    public GameObject[] weaponIndicators; //Weapon Indicator ������Ʈ��

    [Header("Weapon Buying")]
    public GameObject weaponBuying; //Weapon Buying ������Ʈ
    public GameObject weaponNoneBuying; //Weapon None Buying ������Ʈ
    public TextMeshProUGUI weaponPriceTMP; //Weapon Price�� TMP ������Ʈ

    [Header("Weapon Upgrade")]
    public GameObject[] weaponAttackStepActivations; //Weapon Attack�� Step Activation ������Ʈ��
    public GameObject[] weaponAttackStepDeactivations; //Weapon Attack�� Step Deactivation ������Ʈ��
    public TextMeshProUGUI weaponAttackUpgradePriceTMP; //Weapon Attack�� Upgrade Price�� TMP ������Ʈ

    public GameObject[] weaponUltStepActivations; //Weapon Ult�� Step Activation ������Ʈ��
    public GameObject[] weaponUltStepDeactivations; //Weapon Ult�� Step Deactivation ������Ʈ��
    public TextMeshProUGUI weaponUltUpgradePriceTMP; //Weapon Ult�� Upgrade Price�� TMP ������Ʈ

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
            for (int i = 0; i < statButtonSelectedImages.Length; ++i) statButtonSelectedImages[i].SetActive(false); //��� Selected Image ��Ȱ��ȭ
            statButtonSelectedImages[value].SetActive(true); //���õ� Selected Image Ȱ��ȭ

            for (int i = 0; i < statUpgradeLabelTexts.Length; ++i) statUpgradeLabelTexts[i].SetActive(false); //��� Upgrade Label ��Ȱ��ȭ
            statUpgradeLabelTexts[value].SetActive(true); //���õ� Upgrade Label Ȱ��ȭ

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
            } //Upgrade Step �� Upgrade Price ����ȭ

            _selectedStat = value; //�� ����
        }
    } //���õ� Stat

    public Image[] statButtonPercentImages; //Stat Button�� PercentImage�� Image ������Ʈ��
    private readonly Color32 statButtonLowLevelColor = new Color32(255, 255, 0, 65); //Stat Button�� Low Level ����
    private readonly Color32 statButtonHighLevelColor = new Color32(255, 0, 0, 65); //Stat Button�� High Level ����

    public GameObject[] statButtonSelectedImages; //Stat Button�� SelectedImage ������Ʈ��
    public TextMeshProUGUI[] statButtonLevelTextTMPs; //Stat Button�� LevelText�� TMP ������Ʈ��

    [Header("Stat Upgrade")]
    public GameObject[] statUpgradeLabelTexts; //Stat Upgrade�� LabelText ������Ʈ��
    public GameObject[] statUpgradeStepActivations; //Stat Upgrade�� Step Activation ������Ʈ��
    public GameObject[] statUpgradeStepDeactivations; //Stat Upgrade�� Step Deactivation ������Ʈ��
    public TextMeshProUGUI statUpgradeButtonPriceTMP; //Stat Upgrade Button�� Price�� TMP ������Ʈ

    [Header("Cache")]
    private float preMoney = -1; //���� Money

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

    /* ShopInfo�� �ʱ�ȭ�ϴ� �Լ� */
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
    /* Money�� ����ȭ�ϴ� �Լ� */
    private void SyncMoney()
    {
        if (preMoney != ShopInfo.money) //���� Money�� ���� Money�� �ٸ���
        {
            moneyTMP.text = string.Format("{0:#,0}", ShopInfo.money); //Money Text ����
            preMoney = ShopInfo.money; //���� Money ����
        }
    }
    #endregion

    /* Panel ��ư�� Ŭ������ �� ����Ǵ� �Լ� */
    public void OnClickPanelButton(bool isWeaponButton)
    {
        weaponPanel.SetActive(isWeaponButton);
        weaponButtonTextTMP.color = isWeaponButton ? panelButtonTextActivationColor : panelButtonTextDeactivationColor;
        statPanel.SetActive(!isWeaponButton);
        statButtonTextTMP.color = !isWeaponButton ? panelButtonTextActivationColor : panelButtonTextDeactivationColor;
    }

    #region Weapon Select Buttons
    /* Weapon Select Button���� ����ȭ�ϴ� �Լ� */
    private void SyncWeaponSelectButtons()
    {
        fistSelectButtonImage.sprite = ShopInfo.fistSelected ? selectedButtonSprite : unselectedButtonSprite;
        swordSelectButtonImage.sprite = ShopInfo.swordSelected ? selectedButtonSprite : unselectedButtonSprite;
        gunSelectButtonImage.sprite = ShopInfo.gunSelected ? selectedButtonSprite : unselectedButtonSprite;
        sniperSelectButtonImage.sprite = ShopInfo.sniperSelected ? selectedButtonSprite : unselectedButtonSprite;
        bazookaSelectButtonImage.sprite = ShopInfo.bazookaSelected ? selectedButtonSprite : unselectedButtonSprite;
        wizardSelectButtonImage.sprite = ShopInfo.wizardSelected ? selectedButtonSprite : unselectedButtonSprite;
    }

    /* Weapon Select Button�� Ŭ������ �� ����Ǵ� �Լ� */
    public void OnClickWeaponSelectButton(int index)
    {
        if(index == (int)Weapon.Fist) //Fist Select Button�̸�
        {
            if (selectedWeaponCount < 2 && !ShopInfo.fistSelected && ShopInfo.fistBuying) //�Ұ����� ���°� �ƴϸ�
            {
                ShopInfo.fistSelected = true; //���� ����
            }
            else if (selectedWeaponCount > 1 && ShopInfo.fistSelected) //�Ұ����� ���°� �ƴϸ�
            {
                ShopInfo.fistSelected = false; //���� ����
            }
        }
        if (index == (int)Weapon.Sword) //Sword Select Button�̸�
        {
            if (selectedWeaponCount < 2 && !ShopInfo.swordSelected && ShopInfo.swordBuying) //�Ұ����� ���°� �ƴϸ�
            {
                ShopInfo.swordSelected = true; //���� ����
            }
            else if (selectedWeaponCount > 1 && ShopInfo.swordSelected) //�Ұ����� ���°� �ƴϸ�
            {
                ShopInfo.swordSelected = false; //���� ����
            }
        }
        if (index == (int)Weapon.Gun) //Gun Select Button�̸�
        {
            if (selectedWeaponCount < 2 && !ShopInfo.gunSelected && ShopInfo.gunBuying) //�Ұ����� ���°� �ƴϸ�
            {
                ShopInfo.gunSelected = true; //���� ����
            }
            else if (selectedWeaponCount > 1 && ShopInfo.gunSelected) //�Ұ����� ���°� �ƴϸ�
            {
                ShopInfo.gunSelected = false; //���� ����
            }
        }
        if (index == (int)Weapon.Sniper) //Sniper Select Button�̸�
        {
            if (selectedWeaponCount < 2 && !ShopInfo.sniperSelected && ShopInfo.sniperBuying) //�Ұ����� ���°� �ƴϸ�
            {
                ShopInfo.sniperSelected = true; //���� ����
            }
            else if (selectedWeaponCount > 1 && ShopInfo.sniperSelected) //�Ұ����� ���°� �ƴϸ�
            {
                ShopInfo.sniperSelected = false; //���� ����
            }
        }
        if (index == (int)Weapon.Bazooka) //Bazooka Select Button�̸�
        {
            if (selectedWeaponCount < 2 && !ShopInfo.bazookaSelected && ShopInfo.bazookaBuying) //�Ұ����� ���°� �ƴϸ�
            {
                ShopInfo.bazookaSelected = true; //���� ����
            }
            else if (selectedWeaponCount > 1 && ShopInfo.bazookaSelected) //�Ұ����� ���°� �ƴϸ�
            {
                ShopInfo.bazookaSelected = false; //���� ����
            }
        }
        if (index == (int)Weapon.Wizard) //Wizard Select Button�̸�
        {
            if (selectedWeaponCount < 2 && !ShopInfo.wizardSelected && ShopInfo.wizardBuying) //�Ұ����� ���°� �ƴϸ�
            {
                ShopInfo.wizardSelected = true; //���� ����
            }
            else if (selectedWeaponCount > 1 && ShopInfo.wizardSelected) //�Ұ����� ���°� �ƴϸ�
            {
                ShopInfo.wizardSelected = false; //���� ����
            }
        }

        SyncWeaponSelectButtons(); //Selected Button�� ����ȭ
    }
    #endregion

    #region Weapon

    /* Weapon Buy Button�� Ŭ������ �� ����Ǵ� �Լ� */
    public void OnClickWeaponBuyButton()
    {
        if(selectedWeapon == (int)Weapon.Fist)
        {
            if (ShopInfo.money >= Definition.fistBuyPrice) //���Ű� �����ϸ�
            {
                ShopInfo.money -= Definition.fistBuyPrice; //�� ����
                ShopInfo.fistBuying = true; //���� ó��

                selectedWeapon = (int)Weapon.Fist; //���õ� Weapon ����
            }
        }
        else if (selectedWeapon == (int)Weapon.Sword)
        {
            if (ShopInfo.money >= Definition.swordBuyPrice) //���Ű� �����ϸ�
            {
                ShopInfo.money -= Definition.swordBuyPrice; //�� ����
                ShopInfo.swordBuying = true; //���� ó��

                selectedWeapon = (int)Weapon.Sword; //���õ� Weapon ����
            }
        }
        else if (selectedWeapon == (int)Weapon.Gun)
        {
            if (ShopInfo.money >= Definition.gunBuyPrice) //���Ű� �����ϸ�
            {
                ShopInfo.money -= Definition.gunBuyPrice; //�� ����
                ShopInfo.gunBuying = true; //���� ó��

                selectedWeapon = (int)Weapon.Gun; //���õ� Weapon ����
            }
        }
        else if (selectedWeapon == (int)Weapon.Sniper)
        {
            if (ShopInfo.money >= Definition.sniperBuyPrice) //���Ű� �����ϸ�
            {
                ShopInfo.money -= Definition.sniperBuyPrice; //�� ����
                ShopInfo.sniperBuying = true; //���� ó��

                selectedWeapon = (int)Weapon.Sniper; //���õ� Weapon ����
            }
        }
        else if (selectedWeapon == (int)Weapon.Bazooka)
        {
            if (ShopInfo.money >= Definition.bazookaBuyPrice) //���Ű� �����ϸ�
            {
                ShopInfo.money -= Definition.bazookaBuyPrice; //�� ����
                ShopInfo.bazookaBuying = true; //���� ó��

                selectedWeapon = (int)Weapon.Bazooka; //���õ� Weapon ����
            }
        }
        else if (selectedWeapon == (int)Weapon.Wizard)
        {
            if (ShopInfo.money >= Definition.wizardBuyPrice) //���Ű� �����ϸ�
            {
                ShopInfo.money -= Definition.wizardBuyPrice; //�� ����
                ShopInfo.wizardBuying = true; //���� ó��

                selectedWeapon = (int)Weapon.Wizard; //���õ� Weapon ����
            }
        }
    }

    /* Weapon Attack Upgrade Step�� �����ϴ� �Լ� */
    private void SetWeaponAttackUpgradeStep(int step)
    {
        /* ��� Step ��Ȱ��ȭ */
        for (int i = 0; i < Definition.weaponUpgradeMaxLevel; ++i)
        {
            weaponAttackStepActivations[i].SetActive(false);
            weaponAttackStepDeactivations[i].SetActive(false);
        }

        /* Step ��ŭ Activation Ȱ��ȭ */
        for (int i = 0; i < Definition.weaponUpgradeMaxLevel; ++i)
        {
            if (step > 0) weaponAttackStepActivations[i].SetActive(true);
            else weaponAttackStepDeactivations[i].SetActive(true);
            --step;
        }
    }

    /* Weapon Ult Upgrade Step�� �����ϴ� �Լ� */
    private void SetWeaponUltUpgradeStep(int step)
    {
        /* ��� Step ��Ȱ��ȭ */
        for (int i = 0; i < Definition.weaponUpgradeMaxLevel; ++i)
        {
            weaponUltStepActivations[i].SetActive(false);
            weaponUltStepDeactivations[i].SetActive(false);
        }

        /* Step ��ŭ Activation Ȱ��ȭ */
        for (int i = 0; i < Definition.weaponUpgradeMaxLevel; ++i)
        {
            if (step > 0) weaponUltStepActivations[i].SetActive(true);
            else weaponUltStepDeactivations[i].SetActive(true);
            --step;
        }
    }

    /* Weapon Attack�� Upgrade Button�� Ŭ������ �� ����Ǵ� �Լ� */
    public void OnClickWeaponAttackUpgradeButton()
    {
        if (selectedWeapon == (int)Weapon.Fist)
        {
            if (ShopInfo.fistAttackLevel < Definition.weaponUpgradeMaxLevel && Definition.fistAttackUpgradePrice[ShopInfo.fistAttackLevel] <= ShopInfo.money) //Upgrade�� �����ϸ�
            {
                ShopInfo.money -= Definition.fistAttackUpgradePrice[ShopInfo.fistAttackLevel]; //Money ����
                ++ShopInfo.fistAttackLevel; //���� ����
                selectedWeapon = (int)Weapon.Fist; //���õ� Weapon ����
            }
        }
        else if (selectedWeapon == (int)Weapon.Sword)
        {
            if (ShopInfo.swordAttackLevel < Definition.weaponUpgradeMaxLevel && Definition.swordAttackUpgradePrice[ShopInfo.swordAttackLevel] <= ShopInfo.money) //Upgrade�� �����ϸ�
            {
                ShopInfo.money -= Definition.swordAttackUpgradePrice[ShopInfo.swordAttackLevel]; //Money ����
                ++ShopInfo.swordAttackLevel; //���� ����
                selectedWeapon = (int)Weapon.Sword; //���õ� Weapon ����
            }
        }
        else if (selectedWeapon == (int)Weapon.Gun)
        {
            if (ShopInfo.gunAttackLevel < Definition.weaponUpgradeMaxLevel && Definition.gunAttackUpgradePrice[ShopInfo.gunAttackLevel] <= ShopInfo.money) //Upgrade�� �����ϸ�
            {
                ShopInfo.money -= Definition.gunAttackUpgradePrice[ShopInfo.gunAttackLevel]; //Money ����
                ++ShopInfo.gunAttackLevel; //���� ����
                selectedWeapon = (int)Weapon.Gun; //���õ� Weapon ����
            }
        }
        else if (selectedWeapon == (int)Weapon.Sniper)
        {
            if (ShopInfo.sniperAttackLevel < Definition.weaponUpgradeMaxLevel && Definition.sniperAttackUpgradePrice[ShopInfo.sniperAttackLevel] <= ShopInfo.money) //Upgrade�� �����ϸ�
            {
                ShopInfo.money -= Definition.sniperAttackUpgradePrice[ShopInfo.sniperAttackLevel]; //Money ����
                ++ShopInfo.sniperAttackLevel; //���� ����
                selectedWeapon = (int)Weapon.Sniper; //���õ� Weapon ����
            }
        }
        else if (selectedWeapon == (int)Weapon.Bazooka)
        {
            if (ShopInfo.bazookaAttackLevel < Definition.weaponUpgradeMaxLevel && Definition.bazookaAttackUpgradePrice[ShopInfo.bazookaAttackLevel] <= ShopInfo.money) //Upgrade�� �����ϸ�
            {
                ShopInfo.money -= Definition.bazookaAttackUpgradePrice[ShopInfo.bazookaAttackLevel]; //Money ����
                ++ShopInfo.bazookaAttackLevel; //���� ����
                selectedWeapon = (int)Weapon.Bazooka; //���õ� Weapon ����
            }
        }
        else if (selectedWeapon == (int)Weapon.Wizard)
        {
            if (ShopInfo.wizardAttackLevel < Definition.weaponUpgradeMaxLevel && Definition.wizardAttackUpgradePrice[ShopInfo.wizardAttackLevel] <= ShopInfo.money) //Upgrade�� �����ϸ�
            {
                ShopInfo.money -= Definition.wizardAttackUpgradePrice[ShopInfo.wizardAttackLevel]; //Money ����
                ++ShopInfo.wizardAttackLevel; //���� ����
                selectedWeapon = (int)Weapon.Wizard; //���õ� Weapon ����
            }
        }
    }

    /* Weapon Ult�� Upgrade Button�� Ŭ������ �� ����Ǵ� �Լ� */
    public void OnClickWeaponUltUpgradeButton()
    {
        if (selectedWeapon == (int)Weapon.Fist)
        {
            if (ShopInfo.fistUltLevel < Definition.weaponUpgradeMaxLevel && Definition.fistUltUpgradePrice[ShopInfo.fistUltLevel] <= ShopInfo.money) //Upgrade�� �����ϸ�
            {
                ShopInfo.money -= Definition.fistUltUpgradePrice[ShopInfo.fistUltLevel]; //Money ����
                ++ShopInfo.fistUltLevel; //���� ����
                selectedWeapon = (int)Weapon.Fist; //���õ� Weapon ����
            }
        }
        else if (selectedWeapon == (int)Weapon.Sword)
        {
            if (ShopInfo.swordUltLevel < Definition.weaponUpgradeMaxLevel && Definition.swordUltUpgradePrice[ShopInfo.swordUltLevel] <= ShopInfo.money) //Upgrade�� �����ϸ�
            {
                ShopInfo.money -= Definition.swordUltUpgradePrice[ShopInfo.swordUltLevel]; //Money ����
                ++ShopInfo.swordUltLevel; //���� ����
                selectedWeapon = (int)Weapon.Sword; //���õ� Weapon ����
            }
        }
        else if (selectedWeapon == (int)Weapon.Gun)
        {
            if (ShopInfo.gunUltLevel < Definition.weaponUpgradeMaxLevel && Definition.gunUltUpgradePrice[ShopInfo.gunUltLevel] <= ShopInfo.money) //Upgrade�� �����ϸ�
            {
                ShopInfo.money -= Definition.gunUltUpgradePrice[ShopInfo.gunUltLevel]; //Money ����
                ++ShopInfo.gunUltLevel; //���� ����
                selectedWeapon = (int)Weapon.Gun; //���õ� Weapon ����
            }
        }
        else if (selectedWeapon == (int)Weapon.Sniper)
        {
            if (ShopInfo.sniperUltLevel < Definition.weaponUpgradeMaxLevel && Definition.sniperUltUpgradePrice[ShopInfo.sniperUltLevel] <= ShopInfo.money) //Upgrade�� �����ϸ�
            {
                ShopInfo.money -= Definition.sniperUltUpgradePrice[ShopInfo.sniperUltLevel]; //Money ����
                ++ShopInfo.sniperUltLevel; //���� ����
                selectedWeapon = (int)Weapon.Sniper; //���õ� Weapon ����
            }
        }
        else if (selectedWeapon == (int)Weapon.Bazooka)
        {
            if (ShopInfo.bazookaUltLevel < Definition.weaponUpgradeMaxLevel && Definition.bazookaUltUpgradePrice[ShopInfo.bazookaUltLevel] <= ShopInfo.money) //Upgrade�� �����ϸ�
            {
                ShopInfo.money -= Definition.bazookaUltUpgradePrice[ShopInfo.bazookaUltLevel]; //Money ����
                ++ShopInfo.bazookaUltLevel; //���� ����
                selectedWeapon = (int)Weapon.Bazooka; //���õ� Weapon ����
            }
        }
        else if (selectedWeapon == (int)Weapon.Wizard)
        {
            if (ShopInfo.wizardUltLevel < Definition.weaponUpgradeMaxLevel && Definition.wizardUltUpgradePrice[ShopInfo.wizardUltLevel] <= ShopInfo.money) //Upgrade�� �����ϸ�
            {
                ShopInfo.money -= Definition.wizardUltUpgradePrice[ShopInfo.wizardUltLevel]; //Money ����
                ++ShopInfo.wizardUltLevel; //���� ����
                selectedWeapon = (int)Weapon.Wizard; //���õ� Weapon ����
            }
        }
    }
    #endregion

    #region Stat
    /* Stat Button�� Level�� ����ȭ�ϴ� �Լ� */
    private void SyncStatButtonLevel()
    {
        statButtonPercentImages[(int)Stat.maxHP].color = Color.Lerp(statButtonLowLevelColor, statButtonHighLevelColor, (float)ShopInfo.maxHPLevel / Definition.statUpgradeMaxLevel);
        statButtonPercentImages[(int)Stat.maxHP].fillAmount = (float)ShopInfo.maxHPLevel / Definition.statUpgradeMaxLevel; //PercentImage�� FillAmount ����
        statButtonLevelTextTMPs[(int)Stat.maxHP].text = ShopInfo.maxHPLevel < Definition.statUpgradeMaxLevel ? "Lv " + ShopInfo.maxHPLevel : "Max Lv"; //LevelText�� Text ����

        statButtonPercentImages[(int)Stat.recoveryHP].color = Color.Lerp(statButtonLowLevelColor, statButtonHighLevelColor, (float)ShopInfo.recoveryHPLevel / Definition.statUpgradeMaxLevel);
        statButtonPercentImages[(int)Stat.recoveryHP].fillAmount = (float)ShopInfo.recoveryHPLevel / Definition.statUpgradeMaxLevel; //PercentImage�� FillAmount ����
        statButtonLevelTextTMPs[(int)Stat.recoveryHP].text = ShopInfo.recoveryHPLevel < Definition.statUpgradeMaxLevel ? "Lv " + ShopInfo.recoveryHPLevel : "Max Lv"; //LevelText�� Text ����

        statButtonPercentImages[(int)Stat.defense].color = Color.Lerp(statButtonLowLevelColor, statButtonHighLevelColor, (float)ShopInfo.defenseLevel / Definition.statUpgradeMaxLevel);
        statButtonPercentImages[(int)Stat.defense].fillAmount = (float)ShopInfo.defenseLevel / Definition.statUpgradeMaxLevel; //PercentImage�� FillAmount ����
        statButtonLevelTextTMPs[(int)Stat.defense].text = ShopInfo.defenseLevel < Definition.statUpgradeMaxLevel ? "Lv " + ShopInfo.defenseLevel : "Max Lv"; //LevelText�� Text ����

        statButtonPercentImages[(int)Stat.moveSpeed].color = Color.Lerp(statButtonLowLevelColor, statButtonHighLevelColor, (float)ShopInfo.moveSpeedLevel / Definition.statUpgradeMaxLevel);
        statButtonPercentImages[(int)Stat.moveSpeed].fillAmount = (float)ShopInfo.moveSpeedLevel / Definition.statUpgradeMaxLevel; //PercentImage�� FillAmount ����
        statButtonLevelTextTMPs[(int)Stat.moveSpeed].text = ShopInfo.moveSpeedLevel < Definition.statUpgradeMaxLevel ? "Lv " + ShopInfo.moveSpeedLevel : "Max Lv"; //LevelText�� Text ����

        statButtonPercentImages[(int)Stat.moneyAmount].color = Color.Lerp(statButtonLowLevelColor, statButtonHighLevelColor, (float)ShopInfo.moneyAmountLevel / Definition.statUpgradeMaxLevel);
        statButtonPercentImages[(int)Stat.moneyAmount].fillAmount = (float)ShopInfo.moneyAmountLevel / Definition.statUpgradeMaxLevel; //PercentImage�� FillAmount ����
        statButtonLevelTextTMPs[(int)Stat.moneyAmount].text = ShopInfo.moneyAmountLevel < Definition.statUpgradeMaxLevel ? "Lv " + ShopInfo.moneyAmountLevel : "Max Lv"; //LevelText�� Text ����

        statButtonPercentImages[(int)Stat.moneyProbability].color = Color.Lerp(statButtonLowLevelColor, statButtonHighLevelColor, (float)ShopInfo.moneyProbabilityLevel / Definition.statUpgradeMaxLevel);
        statButtonPercentImages[(int)Stat.moneyProbability].fillAmount = (float)ShopInfo.moneyProbabilityLevel / Definition.statUpgradeMaxLevel; //PercentImage�� FillAmount ����
        statButtonLevelTextTMPs[(int)Stat.moneyProbability].text = ShopInfo.moneyProbabilityLevel < Definition.statUpgradeMaxLevel ? "Lv " + ShopInfo.moneyProbabilityLevel : "Max Lv"; //LevelText�� Text ����

        statButtonPercentImages[(int)Stat.criticalDamage].color = Color.Lerp(statButtonLowLevelColor, statButtonHighLevelColor, (float)ShopInfo.criticalDamageLevel / Definition.statUpgradeMaxLevel);
        statButtonPercentImages[(int)Stat.criticalDamage].fillAmount = (float)ShopInfo.criticalDamageLevel / Definition.statUpgradeMaxLevel; //PercentImage�� FillAmount ����
        statButtonLevelTextTMPs[(int)Stat.criticalDamage].text = ShopInfo.criticalDamageLevel < Definition.statUpgradeMaxLevel ? "Lv " + ShopInfo.criticalDamageLevel : "Max Lv"; //LevelText�� Text ����

        statButtonPercentImages[(int)Stat.criticalProbability].color = Color.Lerp(statButtonLowLevelColor, statButtonHighLevelColor, (float)ShopInfo.criticalProbabilityLevel / Definition.statUpgradeMaxLevel);
        statButtonPercentImages[(int)Stat.criticalProbability].fillAmount = (float)ShopInfo.criticalProbabilityLevel / Definition.statUpgradeMaxLevel; //PercentImage�� FillAmount ����
        statButtonLevelTextTMPs[(int)Stat.criticalProbability].text = ShopInfo.criticalProbabilityLevel < Definition.statUpgradeMaxLevel ? "Lv " + ShopInfo.criticalProbabilityLevel : "Max Lv"; //LevelText�� Text ����
    }

    /* Stat Upgrade Step�� �����ϴ� �Լ� */
    private void SetStatUpgradeStep(int step)
    {
        /* ��� Step ��Ȱ��ȭ */
        for (int i = 0; i < Definition.statUpgradeMaxLevel; ++i)
        {
            statUpgradeStepActivations[i].SetActive(false);
            statUpgradeStepDeactivations[i].SetActive(false);
        }

        /* Step ��ŭ Activation Ȱ��ȭ */
        for (int i = 0; i < Definition.statUpgradeMaxLevel; ++i)
        {
            if (step > 0) statUpgradeStepActivations[i].SetActive(true);
            else statUpgradeStepDeactivations[i].SetActive(true);
            --step;
        }
    }

    /* Stat Upgrade Button�� Ŭ������ �� ����Ǵ� �Լ� */
    public void OnClickStatUpgradeButton()
    {
        if(selectedStat == (int)Stat.maxHP)
        {
            if(ShopInfo.maxHPLevel < Definition.statUpgradeMaxLevel && ShopInfo.money >= Definition.maxHPUpgradePrice[ShopInfo.maxHPLevel]) //���׷��̵尡 �����ϸ�
            {
                ShopInfo.money -= Definition.maxHPUpgradePrice[ShopInfo.maxHPLevel]; //�� ����

                ++ShopInfo.maxHPLevel; //���� ����
                SyncStatButtonLevel(); //Stat Button�� Level ����ȭ
                selectedStat = (int)Stat.maxHP; //���õ� Stat ����
            }
        }
        else if (selectedStat == (int)Stat.recoveryHP)
        {
            if (ShopInfo.recoveryHPLevel < Definition.statUpgradeMaxLevel && ShopInfo.money >= Definition.recoveryHPUpgradePrice[ShopInfo.recoveryHPLevel]) //���׷��̵尡 �����ϸ�
            {
                ShopInfo.money -= Definition.recoveryHPUpgradePrice[ShopInfo.recoveryHPLevel]; //�� ����

                ++ShopInfo.recoveryHPLevel; //���� ����
                SyncStatButtonLevel(); //Stat Button�� Level ����ȭ
                selectedStat = (int)Stat.recoveryHP; //���õ� Stat ����
            }
        }
        else if (selectedStat == (int)Stat.defense)
        {
            if (ShopInfo.defenseLevel < Definition.statUpgradeMaxLevel && ShopInfo.money >= Definition.defenseUpgradePrice[ShopInfo.defenseLevel]) //���׷��̵尡 �����ϸ�
            {
                ShopInfo.money -= Definition.defenseUpgradePrice[ShopInfo.defenseLevel]; //�� ����

                ++ShopInfo.defenseLevel; //���� ����
                SyncStatButtonLevel(); //Stat Button�� Level ����ȭ
                selectedStat = (int)Stat.defense; //���õ� Stat ����
            }
        }
        else if (selectedStat == (int)Stat.moveSpeed)
        {
            if (ShopInfo.moveSpeedLevel < Definition.statUpgradeMaxLevel && ShopInfo.money >= Definition.speedUpgradePrice[ShopInfo.moveSpeedLevel]) //���׷��̵尡 �����ϸ�
            {
                ShopInfo.money -= Definition.speedUpgradePrice[ShopInfo.moveSpeedLevel]; //�� ����

                ++ShopInfo.moveSpeedLevel; //���� ����
                SyncStatButtonLevel(); //Stat Button�� Level ����ȭ
                selectedStat = (int)Stat.moveSpeed; //���õ� Stat ����
            }
        }
        else if (selectedStat == (int)Stat.moneyAmount)
        {
            if (ShopInfo.moneyAmountLevel < Definition.statUpgradeMaxLevel && ShopInfo.money >= Definition.moneyAmountUpgradePrice[ShopInfo.moneyAmountLevel]) //���׷��̵尡 �����ϸ�
            {
                ShopInfo.money -= Definition.moneyAmountUpgradePrice[ShopInfo.moneyAmountLevel]; //�� ����

                ++ShopInfo.moneyAmountLevel; //���� ����
                SyncStatButtonLevel(); //Stat Button�� Level ����ȭ
                selectedStat = (int)Stat.moneyAmount; //���õ� Stat ����
            }
        }
        else if (selectedStat == (int)Stat.moneyProbability)
        {
            if (ShopInfo.moneyProbabilityLevel < Definition.statUpgradeMaxLevel && ShopInfo.money >= Definition.moneyProbabilityUpgradePrice[ShopInfo.moneyProbabilityLevel]) //���׷��̵尡 �����ϸ�
            {
                ShopInfo.money -= Definition.moneyProbabilityUpgradePrice[ShopInfo.moneyProbabilityLevel]; //�� ����

                ++ShopInfo.moneyProbabilityLevel; //���� ����
                SyncStatButtonLevel(); //Stat Button�� Level ����ȭ
                selectedStat = (int)Stat.moneyProbability; //���õ� Stat ����
            }
        }
        else if (selectedStat == (int)Stat.criticalDamage)
        {
            if (ShopInfo.criticalDamageLevel < Definition.statUpgradeMaxLevel && ShopInfo.money >= Definition.criticalDamageUpgradePrice[ShopInfo.criticalDamageLevel]) //���׷��̵尡 �����ϸ�
            {
                ShopInfo.money -= Definition.criticalDamageUpgradePrice[ShopInfo.criticalDamageLevel]; //�� ����

                ++ShopInfo.criticalDamageLevel; //���� ����
                SyncStatButtonLevel(); //Stat Button�� Level ����ȭ
                selectedStat = (int)Stat.criticalDamage; //���õ� Stat ����
            }
        }
        else if (selectedStat == (int)Stat.criticalProbability)
        {
            if (ShopInfo.criticalProbabilityLevel < Definition.statUpgradeMaxLevel && ShopInfo.money >= Definition.criticalProbabilityUpgradePrice[ShopInfo.criticalProbabilityLevel]) //���׷��̵尡 �����ϸ�
            {
                ShopInfo.money -= Definition.criticalProbabilityUpgradePrice[ShopInfo.criticalProbabilityLevel]; //�� ����

                ++ShopInfo.criticalProbabilityLevel; //���� ����
                SyncStatButtonLevel(); //Stat Button�� Level ����ȭ
                selectedStat = (int)Stat.criticalProbability; //���õ� Stat ����
            }
        }
    }
    #endregion
}
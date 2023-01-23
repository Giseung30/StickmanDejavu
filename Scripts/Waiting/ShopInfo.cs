using UnityEngine;

public static class ShopInfo
{
    public static int money
    {
        get
        {
            return AntiCheatManager.moneySecured;
        }
        set
        {
            AntiCheatManager.moneySecured = Mathf.Clamp(value, 0, 2000000000);
        }
    } //��
    public static int profit
    {
        get
        {
            return AntiCheatManager.profitSecured;
        }
        set
        {
            AntiCheatManager.profitSecured = Mathf.Clamp(value, 0, 2000000000);
        }
    } //����
    public static int kill
    {
        get
        {
            return AntiCheatManager.killSecured;
        }
        set
        {
            AntiCheatManager.killSecured = Mathf.Clamp(value, 0, 2000000000);
        }
    } //óġ

    public static Weapon firstSelectedWeapon
    {
        get
        {
            if (fistSelected == true) return Weapon.Fist;
            else if (swordSelected == true) return Weapon.Sword;
            else if (gunSelected == true) return Weapon.Gun;
            else if (sniperSelected == true) return Weapon.Sniper;
            else if (bazookaSelected == true) return Weapon.Bazooka;
            else if (wizardSelected == true) return Weapon.Wizard;

            return Weapon.None;
        }
    } //ù ��° ���õ� ����
    public static Weapon secondSelectedWeapon
    {
        get
        {
            bool firstWeaponSelected = false; //ù ��° ���� ���� ����

            if (fistSelected) firstWeaponSelected = true;
            if (firstWeaponSelected && swordSelected) return Weapon.Sword;

            if (swordSelected) firstWeaponSelected = true;
            if (firstWeaponSelected && gunSelected) return Weapon.Gun;

            if (gunSelected) firstWeaponSelected = true;
            if (firstWeaponSelected && sniperSelected) return Weapon.Sniper;

            if (sniperSelected) firstWeaponSelected = true;
            if (firstWeaponSelected && bazookaSelected) return Weapon.Bazooka;

            if (bazookaSelected) firstWeaponSelected = true;
            if (firstWeaponSelected && wizardSelected) return Weapon.Wizard;

            return Weapon.None;
        }
    } //�� ��° ���õ� ����

    public static Weapon clickedWeaponButton; //Ŭ���� Weapon Button

    #region Weapon
    public static bool fistSelected = true; //Fist ���� ����
    public static bool fistBuying = false; //Fist ���� ����
    public static int fistAttackLevel = 0; //Fist Attack ����
    public static int fistUltLevel = 0; //Fist Ult ����

    public static bool swordSelected = false; //Sword ���� ����
    public static bool swordBuying = false; //Sword ���� ����
    public static int swordAttackLevel = 0; //Sword Attack ����
    public static int swordUltLevel = 0; //Sword Ult ����

    public static bool gunSelected = false; //Gun ���� ����
    public static bool gunBuying = false; //Gun ���� ����
    public static int gunAttackLevel = 0; //Gun Attack ����
    public static int gunUltLevel = 0; //Gun Ult ����

    public static bool sniperSelected = false; //Sniper ���� ����
    public static bool sniperBuying = false; //Sniper ���� ����
    public static int sniperAttackLevel = 0; //Sniper Attack ����
    public static int sniperUltLevel = 0; //Sniper Ult ����

    public static bool bazookaSelected = false; //Bazooka ���� ����
    public static bool bazookaBuying = false; //Bazooka ���� ����
    public static int bazookaAttackLevel = 0; //Bazooka Attack ����
    public static int bazookaUltLevel = 0; //Bazooka Ult ����

    public static bool wizardSelected = false; //Wizard ���� ����
    public static bool wizardBuying = false; //Wizard ���� ����
    public static int wizardAttackLevel = 0; //Wizard Attack ����
    public static int wizardUltLevel = 0; //Wizard Ult ����
    #endregion

    public static Stat clickedStatButton; //Ŭ���� Stat Button

    #region Stat
    public static int maxHPLevel = 0; //�ִ� ü�� ����
    public static int recoveryHPLevel = 0; //ü�� ȸ���� ����
    public static int defenseLevel = 0; //���� ����
    public static int moveSpeedLevel = 0; //�̵� �ӵ� ����
    public static int moneyAmountLevel = 0; //�� ȹ�淮 ����
    public static int moneyProbabilityLevel = 0; //�� ȹ�� Ȯ�� ����
    public static int criticalDamageLevel = 0; //ġ��Ÿ ���ط� ����
    public static int criticalProbabilityLevel = 0; //ġ��Ÿ Ȯ�� ����
    #endregion
}
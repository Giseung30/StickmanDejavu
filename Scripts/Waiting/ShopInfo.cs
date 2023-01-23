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
    } //돈
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
    } //수익
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
    } //처치

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
    } //첫 번째 선택된 무기
    public static Weapon secondSelectedWeapon
    {
        get
        {
            bool firstWeaponSelected = false; //첫 번째 무기 선택 여부

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
    } //두 번째 선택된 무기

    public static Weapon clickedWeaponButton; //클릭된 Weapon Button

    #region Weapon
    public static bool fistSelected = true; //Fist 선택 여부
    public static bool fistBuying = false; //Fist 구매 여부
    public static int fistAttackLevel = 0; //Fist Attack 레벨
    public static int fistUltLevel = 0; //Fist Ult 레벨

    public static bool swordSelected = false; //Sword 선택 여부
    public static bool swordBuying = false; //Sword 구매 여부
    public static int swordAttackLevel = 0; //Sword Attack 레벨
    public static int swordUltLevel = 0; //Sword Ult 레벨

    public static bool gunSelected = false; //Gun 선택 여부
    public static bool gunBuying = false; //Gun 구매 여부
    public static int gunAttackLevel = 0; //Gun Attack 레벨
    public static int gunUltLevel = 0; //Gun Ult 레벨

    public static bool sniperSelected = false; //Sniper 선택 여부
    public static bool sniperBuying = false; //Sniper 구매 여부
    public static int sniperAttackLevel = 0; //Sniper Attack 레벨
    public static int sniperUltLevel = 0; //Sniper Ult 레벨

    public static bool bazookaSelected = false; //Bazooka 선택 여부
    public static bool bazookaBuying = false; //Bazooka 구매 여부
    public static int bazookaAttackLevel = 0; //Bazooka Attack 레벨
    public static int bazookaUltLevel = 0; //Bazooka Ult 레벨

    public static bool wizardSelected = false; //Wizard 선택 여부
    public static bool wizardBuying = false; //Wizard 구매 여부
    public static int wizardAttackLevel = 0; //Wizard Attack 레벨
    public static int wizardUltLevel = 0; //Wizard Ult 레벨
    #endregion

    public static Stat clickedStatButton; //클릭된 Stat Button

    #region Stat
    public static int maxHPLevel = 0; //최대 체력 레벨
    public static int recoveryHPLevel = 0; //체력 회복률 레벨
    public static int defenseLevel = 0; //방어력 레벨
    public static int moveSpeedLevel = 0; //이동 속도 레벨
    public static int moneyAmountLevel = 0; //돈 획득량 레벨
    public static int moneyProbabilityLevel = 0; //돈 획득 확률 레벨
    public static int criticalDamageLevel = 0; //치명타 피해량 레벨
    public static int criticalProbabilityLevel = 0; //치명타 확률 레벨
    #endregion
}
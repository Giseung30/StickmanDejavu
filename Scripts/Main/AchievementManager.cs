using UnityEngine;
using System.Collections;

public class AchievementManager : MonoBehaviour
{
    [Header("Cache")]
    private bool isAccomplishAchievement; //업적을 달성했는지 확인하는 변수

    private void Update()
    {
        CheckAchievement();
    }

    /* 업적을 확인하는 함수 */
    private void CheckAchievement()
    {
        isAccomplishAchievement = false;

        if (!AchievementInfo.fistBuying && ShopInfo.fistBuying) //FistBuying
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'싸운꾼'</color> 달성!", 5f);
            AchievementInfo.fistBuying = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.fistAttackUpgrade && ShopInfo.fistAttackLevel >= Definition.weaponUpgradeMaxLevel) //FistAttackUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'연타'</color> 달성!", 5f);
            AchievementInfo.fistAttackUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.fistUltUpgrade && ShopInfo.fistUltLevel >= Definition.weaponUpgradeMaxLevel) //FistUltUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'격투'</color> 달성!", 5f);
            AchievementInfo.fistUltUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.fistUpgrade && ShopInfo.fistAttackLevel >= Definition.weaponUpgradeMaxLevel && ShopInfo.fistUltLevel >= Definition.weaponUpgradeMaxLevel) //FistUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FF5A00>'무도인'</color> 달성!", 5f);
            AchievementInfo.fistUpgrade = true;
            isAccomplishAchievement = true;
        }

        if (!AchievementInfo.swordBuying && ShopInfo.swordBuying) //SwordBuying
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'검객'</color> 달성!", 5f);
            AchievementInfo.swordBuying = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.swordAttackUpgrade && ShopInfo.swordAttackLevel >= Definition.weaponUpgradeMaxLevel) //SwordAttackUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'질풍'</color> 달성!", 5f);
            AchievementInfo.swordAttackUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.swordUltUpgrade && ShopInfo.swordUltLevel >= Definition.weaponUpgradeMaxLevel) //SwordUltUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'일격'</color> 달성!", 5f);
            AchievementInfo.swordUltUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.swordUpgrade && ShopInfo.swordAttackLevel >= Definition.weaponUpgradeMaxLevel && ShopInfo.swordUltLevel >= Definition.weaponUpgradeMaxLevel) //SwordUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FF5A00>'암살자'</color> 달성!", 5f);
            AchievementInfo.swordUpgrade = true;
            isAccomplishAchievement = true;
        }

        if (!AchievementInfo.gunBuying && ShopInfo.gunBuying) //GunBuying
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'사수'</color> 달성!", 5f);
            AchievementInfo.gunBuying = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.gunAttackUpgrade && ShopInfo.gunAttackLevel >= Definition.weaponUpgradeMaxLevel) //GunAttackUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'연사'</color> 달성!", 5f);
            AchievementInfo.gunAttackUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.gunUltUpgrade && ShopInfo.gunUltLevel >= Definition.weaponUpgradeMaxLevel) //GunUltUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'난사'</color> 달성!", 5f);
            AchievementInfo.gunUltUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.gunUpgrade && ShopInfo.gunAttackLevel >= Definition.weaponUpgradeMaxLevel && ShopInfo.gunUltLevel >= Definition.weaponUpgradeMaxLevel) //GunUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FF5A00>'총잡이'</color> 달성!", 5f);
            AchievementInfo.gunUpgrade = true;
            isAccomplishAchievement = true;
        }

        if (!AchievementInfo.sniperBuying && ShopInfo.sniperBuying) //SniperBuying
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'지원가'</color> 달성!", 5f);
            AchievementInfo.sniperBuying = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.sniperAttackUpgrade && ShopInfo.sniperAttackLevel >= Definition.weaponUpgradeMaxLevel) //SniperAttackUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'관통'</color> 달성!", 5f);
            AchievementInfo.sniperAttackUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.sniperUltUpgrade && ShopInfo.sniperUltLevel >= Definition.weaponUpgradeMaxLevel) //SniperUltUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'조준'</color> 달성!", 5f);
            AchievementInfo.sniperUltUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.sniperUpgrade && ShopInfo.sniperAttackLevel >= Definition.weaponUpgradeMaxLevel && ShopInfo.sniperUltLevel >= Definition.weaponUpgradeMaxLevel) //SniperUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FF5A00>'저격수'</color> 달성!", 5f);
            AchievementInfo.sniperUpgrade = true;
            isAccomplishAchievement = true;
        }

        if (!AchievementInfo.bazookaBuying && ShopInfo.bazookaBuying) //BazookaBuying
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'포병'</color> 달성!", 5f);
            AchievementInfo.bazookaBuying = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.bazookaAttackUpgrade && ShopInfo.bazookaAttackLevel >= Definition.weaponUpgradeMaxLevel) //BazookaAttackUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'발포'</color> 달성!", 5f);
            AchievementInfo.bazookaAttackUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.bazookaUltUpgrade && ShopInfo.bazookaUltLevel >= Definition.weaponUpgradeMaxLevel) //BazookaUltUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'폭발'</color> 달성!", 5f);
            AchievementInfo.bazookaUltUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.bazookaUpgrade && ShopInfo.bazookaAttackLevel >= Definition.weaponUpgradeMaxLevel && ShopInfo.bazookaUltLevel >= Definition.weaponUpgradeMaxLevel) //BazookaUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FF5A00>'기갑'</color> 달성!", 5f);
            AchievementInfo.bazookaUpgrade = true;
            isAccomplishAchievement = true;
        }

        if (!AchievementInfo.wizardBuying && ShopInfo.wizardBuying) //WizardBuying
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'마법사'</color> 달성!", 5f);
            AchievementInfo.wizardBuying = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.wizardAttackUpgrade && ShopInfo.wizardAttackLevel >= Definition.weaponUpgradeMaxLevel) //WizardAttackUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'냉각'</color> 달성!", 5f);
            AchievementInfo.wizardAttackUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.wizardUltUpgrade && ShopInfo.wizardUltLevel >= Definition.weaponUpgradeMaxLevel) //WizardUltUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'빙결'</color> 달성!", 5f);
            AchievementInfo.wizardUltUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.wizardUpgrade && ShopInfo.wizardAttackLevel >= Definition.weaponUpgradeMaxLevel && ShopInfo.wizardUltLevel >= Definition.weaponUpgradeMaxLevel) //WizardUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FF5A00>'대마법사'</color> 달성!", 5f);
            AchievementInfo.wizardUpgrade = true;
            isAccomplishAchievement = true;
        }

        if (!AchievementInfo.allWeaponUpgrade && AchievementInfo.fistUpgrade && AchievementInfo.swordUpgrade && AchievementInfo.gunUpgrade & AchievementInfo.sniperUpgrade && AchievementInfo.bazookaUpgrade && AchievementInfo.wizardUpgrade) //AllWeaponUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FF1E00>'1인 군단'</color> 달성!", 5f);
            AchievementInfo.allWeaponUpgrade = true;
            isAccomplishAchievement = true;
        }

        if (!AchievementInfo.maxHPUpgrade && ShopInfo.maxHPLevel >= Definition.statUpgradeMaxLevel) //MaxHPUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'맷집'</color> 달성!", 5f);
            AchievementInfo.maxHPUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.recoveryHPUpgrade && ShopInfo.recoveryHPLevel >= Definition.statUpgradeMaxLevel) //RecoveryHPUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'치유'</color> 달성!", 5f);
            AchievementInfo.recoveryHPUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.defenseUpgrade && ShopInfo.defenseLevel >= Definition.statUpgradeMaxLevel) //DefenseUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'보호'</color> 달성!", 5f);
            AchievementInfo.defenseUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.moveSpeedUpgrade && ShopInfo.moveSpeedLevel >= Definition.statUpgradeMaxLevel) //MoveSpeedUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'질주'</color> 달성!", 5f);
            AchievementInfo.moveSpeedUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.moneyAmountUpgrade && ShopInfo.moneyAmountLevel >= Definition.statUpgradeMaxLevel) //MoneyAmountUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'일확'</color> 달성!", 5f);
            AchievementInfo.moneyAmountUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.moneyProbabilityUpgrade && ShopInfo.moneyProbabilityLevel >= Definition.statUpgradeMaxLevel) //MoneyProbabilityUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'도벽'</color> 달성!", 5f);
            AchievementInfo.moneyProbabilityUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.criticalDamageUpgrade && ShopInfo.criticalDamageLevel >= Definition.statUpgradeMaxLevel) //CriticalDamageUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'살기'</color> 달성!", 5f);
            AchievementInfo.criticalDamageUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.criticalProbabilityUpgrade && ShopInfo.criticalProbabilityLevel >= Definition.statUpgradeMaxLevel) //CriticalProbabilityUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'급소'</color> 달성!", 5f);
            AchievementInfo.criticalProbabilityUpgrade = true;
            isAccomplishAchievement = true;
        }

        if (!AchievementInfo.allStatUpgrade && AchievementInfo.maxHPUpgrade && AchievementInfo.recoveryHPUpgrade && AchievementInfo.defenseUpgrade & AchievementInfo.moveSpeedUpgrade && AchievementInfo.moneyAmountUpgrade && AchievementInfo.moneyProbabilityUpgrade && AchievementInfo.criticalDamageUpgrade && AchievementInfo.criticalProbabilityUpgrade) //AllStatUpgrade
        {
            Notification.notification.SetNotification("업적 <color=#FF1E00>'생체병기'</color> 달성!", 5f);
            AchievementInfo.allStatUpgrade = true;
            isAccomplishAchievement = true;
        }

        if (!AchievementInfo.kill_1 && ShopInfo.kill >= 1) //Kill_1
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'선취점'</color> 달성!", 5f);
            AchievementInfo.kill_1 = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.kill_100 && ShopInfo.kill >= 100) //Kill_100
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'사냥꾼'</color> 달성!", 5f);
            AchievementInfo.kill_100 = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.kill_1000 && ShopInfo.kill >= 1000) //Kill_1000
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'학살자'</color> 달성!", 5f);
            AchievementInfo.kill_1000 = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.kill_10000 && ShopInfo.kill >= 10000) //Kill_10000
        {
            Notification.notification.SetNotification("업적 <color=#FF5A00>'베테랑'</color> 달성!", 5f);
            AchievementInfo.kill_10000 = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.kill_100000 && ShopInfo.kill >= 100000) //Kill_100000
        {
            Notification.notification.SetNotification("업적 <color=#FF1E00>'장의사'</color> 달성!", 5f);
            AchievementInfo.kill_100000 = true;
            isAccomplishAchievement = true;
        }

        if (!AchievementInfo.profit_1 && ShopInfo.profit >= 1) //Profit_1
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'티끌 모아 태산'</color> 달성!", 5f);
            AchievementInfo.profit_1 = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.profit_10000 && ShopInfo.profit >= 10000) //Profit_10000
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'만원의 행복'</color> 달성!", 5f);
            AchievementInfo.profit_10000 = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.profit_100000 && ShopInfo.profit >= 100000) //Profit_100000
        {
            Notification.notification.SetNotification("업적 <color=#FFB400>'돈 벌 시간'</color> 달성!", 5f);
            AchievementInfo.profit_100000 = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.profit_1000000 && ShopInfo.profit >= 1000000) //Profit_1000000
        {
            Notification.notification.SetNotification("업적 <color=#FF5A00>'백만장자'</color> 달성!", 5f);
            AchievementInfo.profit_1000000 = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.profit_10000000 && ShopInfo.profit >= 10000000) //Profit_10000000
        {
            Notification.notification.SetNotification("업적 <color=#FF1E00>'노력의 결과'</color> 달성!", 5f);
            AchievementInfo.profit_10000000 = true;
            isAccomplishAchievement = true;
        }

        if (isAccomplishAchievement) StartCoroutine(SaveAchievement()); //업적을 하나라도 달성하면 업적 저장
    }

    /* 업적을 저장하는 코루틴 함수 */
    private IEnumerator SaveAchievement()
    {
        if (!string.IsNullOrEmpty(LoginInfo.userID)) //구글 계정 로그인이면
        {
            yield return StartCoroutine(NetworkManager.networkManager.SetAchievementInfo(LoginInfo.userID, LoginInfo.userName)); //업적 저장
            if(NetworkManager.setAchievementInfoResult == NetworkResult.Error) Notification.notification.SetNotification("업적을 저장하는 도중 오류가 발생했습니다", 3f);
        }
    }

    /* 업적을 초기화하는 함수 */
    public static void ResetAchievementInfo()
    {
        AchievementInfo.fistBuying = false;
        AchievementInfo.fistAttackUpgrade = false;
        AchievementInfo.fistUltUpgrade = false;
        AchievementInfo.fistUpgrade = false;

        AchievementInfo.swordBuying = false;
        AchievementInfo.swordAttackUpgrade = false;
        AchievementInfo.swordUltUpgrade = false;
        AchievementInfo.swordUpgrade = false;

        AchievementInfo.gunBuying = false;
        AchievementInfo.gunAttackUpgrade = false;
        AchievementInfo.gunUltUpgrade = false;
        AchievementInfo.gunUpgrade = false;

        AchievementInfo.sniperBuying = false;
        AchievementInfo.sniperAttackUpgrade = false;
        AchievementInfo.sniperUltUpgrade = false;
        AchievementInfo.sniperUpgrade = false;

        AchievementInfo.bazookaBuying = false;
        AchievementInfo.bazookaAttackUpgrade = false;
        AchievementInfo.bazookaUltUpgrade = false;
        AchievementInfo.bazookaUpgrade = false;

        AchievementInfo.wizardBuying = false;
        AchievementInfo.wizardAttackUpgrade = false;
        AchievementInfo.wizardUltUpgrade = false;
        AchievementInfo.wizardUpgrade = false;

        AchievementInfo.allWeaponUpgrade = false;

        AchievementInfo.maxHPUpgrade = false;
        AchievementInfo.recoveryHPUpgrade = false;
        AchievementInfo.defenseUpgrade = false;
        AchievementInfo.moveSpeedUpgrade = false;
        AchievementInfo.moneyAmountUpgrade = false;
        AchievementInfo.moneyProbabilityUpgrade = false;
        AchievementInfo.criticalDamageUpgrade = false;
        AchievementInfo.criticalProbabilityUpgrade = false;

        AchievementInfo.allStatUpgrade = false;

        AchievementInfo.kill_1 = false;
        AchievementInfo.kill_100 = false;
        AchievementInfo.kill_1000 = false;
        AchievementInfo.kill_10000 = false;
        AchievementInfo.kill_100000 = false;
        AchievementInfo.profit_1 = false;
        AchievementInfo.profit_10000 = false;
        AchievementInfo.profit_100000 = false;
        AchievementInfo.profit_1000000 = false;
        AchievementInfo.profit_10000000 = false;
    }
}
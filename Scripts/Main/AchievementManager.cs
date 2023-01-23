using UnityEngine;
using System.Collections;

public class AchievementManager : MonoBehaviour
{
    [Header("Cache")]
    private bool isAccomplishAchievement; //������ �޼��ߴ��� Ȯ���ϴ� ����

    private void Update()
    {
        CheckAchievement();
    }

    /* ������ Ȯ���ϴ� �Լ� */
    private void CheckAchievement()
    {
        isAccomplishAchievement = false;

        if (!AchievementInfo.fistBuying && ShopInfo.fistBuying) //FistBuying
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'�ο��'</color> �޼�!", 5f);
            AchievementInfo.fistBuying = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.fistAttackUpgrade && ShopInfo.fistAttackLevel >= Definition.weaponUpgradeMaxLevel) //FistAttackUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'��Ÿ'</color> �޼�!", 5f);
            AchievementInfo.fistAttackUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.fistUltUpgrade && ShopInfo.fistUltLevel >= Definition.weaponUpgradeMaxLevel) //FistUltUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'����'</color> �޼�!", 5f);
            AchievementInfo.fistUltUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.fistUpgrade && ShopInfo.fistAttackLevel >= Definition.weaponUpgradeMaxLevel && ShopInfo.fistUltLevel >= Definition.weaponUpgradeMaxLevel) //FistUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FF5A00>'������'</color> �޼�!", 5f);
            AchievementInfo.fistUpgrade = true;
            isAccomplishAchievement = true;
        }

        if (!AchievementInfo.swordBuying && ShopInfo.swordBuying) //SwordBuying
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'�˰�'</color> �޼�!", 5f);
            AchievementInfo.swordBuying = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.swordAttackUpgrade && ShopInfo.swordAttackLevel >= Definition.weaponUpgradeMaxLevel) //SwordAttackUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'��ǳ'</color> �޼�!", 5f);
            AchievementInfo.swordAttackUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.swordUltUpgrade && ShopInfo.swordUltLevel >= Definition.weaponUpgradeMaxLevel) //SwordUltUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'�ϰ�'</color> �޼�!", 5f);
            AchievementInfo.swordUltUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.swordUpgrade && ShopInfo.swordAttackLevel >= Definition.weaponUpgradeMaxLevel && ShopInfo.swordUltLevel >= Definition.weaponUpgradeMaxLevel) //SwordUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FF5A00>'�ϻ���'</color> �޼�!", 5f);
            AchievementInfo.swordUpgrade = true;
            isAccomplishAchievement = true;
        }

        if (!AchievementInfo.gunBuying && ShopInfo.gunBuying) //GunBuying
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'���'</color> �޼�!", 5f);
            AchievementInfo.gunBuying = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.gunAttackUpgrade && ShopInfo.gunAttackLevel >= Definition.weaponUpgradeMaxLevel) //GunAttackUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'����'</color> �޼�!", 5f);
            AchievementInfo.gunAttackUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.gunUltUpgrade && ShopInfo.gunUltLevel >= Definition.weaponUpgradeMaxLevel) //GunUltUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'����'</color> �޼�!", 5f);
            AchievementInfo.gunUltUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.gunUpgrade && ShopInfo.gunAttackLevel >= Definition.weaponUpgradeMaxLevel && ShopInfo.gunUltLevel >= Definition.weaponUpgradeMaxLevel) //GunUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FF5A00>'������'</color> �޼�!", 5f);
            AchievementInfo.gunUpgrade = true;
            isAccomplishAchievement = true;
        }

        if (!AchievementInfo.sniperBuying && ShopInfo.sniperBuying) //SniperBuying
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'������'</color> �޼�!", 5f);
            AchievementInfo.sniperBuying = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.sniperAttackUpgrade && ShopInfo.sniperAttackLevel >= Definition.weaponUpgradeMaxLevel) //SniperAttackUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'����'</color> �޼�!", 5f);
            AchievementInfo.sniperAttackUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.sniperUltUpgrade && ShopInfo.sniperUltLevel >= Definition.weaponUpgradeMaxLevel) //SniperUltUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'����'</color> �޼�!", 5f);
            AchievementInfo.sniperUltUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.sniperUpgrade && ShopInfo.sniperAttackLevel >= Definition.weaponUpgradeMaxLevel && ShopInfo.sniperUltLevel >= Definition.weaponUpgradeMaxLevel) //SniperUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FF5A00>'���ݼ�'</color> �޼�!", 5f);
            AchievementInfo.sniperUpgrade = true;
            isAccomplishAchievement = true;
        }

        if (!AchievementInfo.bazookaBuying && ShopInfo.bazookaBuying) //BazookaBuying
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'����'</color> �޼�!", 5f);
            AchievementInfo.bazookaBuying = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.bazookaAttackUpgrade && ShopInfo.bazookaAttackLevel >= Definition.weaponUpgradeMaxLevel) //BazookaAttackUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'����'</color> �޼�!", 5f);
            AchievementInfo.bazookaAttackUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.bazookaUltUpgrade && ShopInfo.bazookaUltLevel >= Definition.weaponUpgradeMaxLevel) //BazookaUltUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'����'</color> �޼�!", 5f);
            AchievementInfo.bazookaUltUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.bazookaUpgrade && ShopInfo.bazookaAttackLevel >= Definition.weaponUpgradeMaxLevel && ShopInfo.bazookaUltLevel >= Definition.weaponUpgradeMaxLevel) //BazookaUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FF5A00>'�Ⱙ'</color> �޼�!", 5f);
            AchievementInfo.bazookaUpgrade = true;
            isAccomplishAchievement = true;
        }

        if (!AchievementInfo.wizardBuying && ShopInfo.wizardBuying) //WizardBuying
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'������'</color> �޼�!", 5f);
            AchievementInfo.wizardBuying = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.wizardAttackUpgrade && ShopInfo.wizardAttackLevel >= Definition.weaponUpgradeMaxLevel) //WizardAttackUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'�ð�'</color> �޼�!", 5f);
            AchievementInfo.wizardAttackUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.wizardUltUpgrade && ShopInfo.wizardUltLevel >= Definition.weaponUpgradeMaxLevel) //WizardUltUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'����'</color> �޼�!", 5f);
            AchievementInfo.wizardUltUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.wizardUpgrade && ShopInfo.wizardAttackLevel >= Definition.weaponUpgradeMaxLevel && ShopInfo.wizardUltLevel >= Definition.weaponUpgradeMaxLevel) //WizardUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FF5A00>'�븶����'</color> �޼�!", 5f);
            AchievementInfo.wizardUpgrade = true;
            isAccomplishAchievement = true;
        }

        if (!AchievementInfo.allWeaponUpgrade && AchievementInfo.fistUpgrade && AchievementInfo.swordUpgrade && AchievementInfo.gunUpgrade & AchievementInfo.sniperUpgrade && AchievementInfo.bazookaUpgrade && AchievementInfo.wizardUpgrade) //AllWeaponUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FF1E00>'1�� ����'</color> �޼�!", 5f);
            AchievementInfo.allWeaponUpgrade = true;
            isAccomplishAchievement = true;
        }

        if (!AchievementInfo.maxHPUpgrade && ShopInfo.maxHPLevel >= Definition.statUpgradeMaxLevel) //MaxHPUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'����'</color> �޼�!", 5f);
            AchievementInfo.maxHPUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.recoveryHPUpgrade && ShopInfo.recoveryHPLevel >= Definition.statUpgradeMaxLevel) //RecoveryHPUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'ġ��'</color> �޼�!", 5f);
            AchievementInfo.recoveryHPUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.defenseUpgrade && ShopInfo.defenseLevel >= Definition.statUpgradeMaxLevel) //DefenseUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'��ȣ'</color> �޼�!", 5f);
            AchievementInfo.defenseUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.moveSpeedUpgrade && ShopInfo.moveSpeedLevel >= Definition.statUpgradeMaxLevel) //MoveSpeedUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'����'</color> �޼�!", 5f);
            AchievementInfo.moveSpeedUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.moneyAmountUpgrade && ShopInfo.moneyAmountLevel >= Definition.statUpgradeMaxLevel) //MoneyAmountUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'��Ȯ'</color> �޼�!", 5f);
            AchievementInfo.moneyAmountUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.moneyProbabilityUpgrade && ShopInfo.moneyProbabilityLevel >= Definition.statUpgradeMaxLevel) //MoneyProbabilityUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'����'</color> �޼�!", 5f);
            AchievementInfo.moneyProbabilityUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.criticalDamageUpgrade && ShopInfo.criticalDamageLevel >= Definition.statUpgradeMaxLevel) //CriticalDamageUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'���'</color> �޼�!", 5f);
            AchievementInfo.criticalDamageUpgrade = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.criticalProbabilityUpgrade && ShopInfo.criticalProbabilityLevel >= Definition.statUpgradeMaxLevel) //CriticalProbabilityUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'�޼�'</color> �޼�!", 5f);
            AchievementInfo.criticalProbabilityUpgrade = true;
            isAccomplishAchievement = true;
        }

        if (!AchievementInfo.allStatUpgrade && AchievementInfo.maxHPUpgrade && AchievementInfo.recoveryHPUpgrade && AchievementInfo.defenseUpgrade & AchievementInfo.moveSpeedUpgrade && AchievementInfo.moneyAmountUpgrade && AchievementInfo.moneyProbabilityUpgrade && AchievementInfo.criticalDamageUpgrade && AchievementInfo.criticalProbabilityUpgrade) //AllStatUpgrade
        {
            Notification.notification.SetNotification("���� <color=#FF1E00>'��ü����'</color> �޼�!", 5f);
            AchievementInfo.allStatUpgrade = true;
            isAccomplishAchievement = true;
        }

        if (!AchievementInfo.kill_1 && ShopInfo.kill >= 1) //Kill_1
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'������'</color> �޼�!", 5f);
            AchievementInfo.kill_1 = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.kill_100 && ShopInfo.kill >= 100) //Kill_100
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'��ɲ�'</color> �޼�!", 5f);
            AchievementInfo.kill_100 = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.kill_1000 && ShopInfo.kill >= 1000) //Kill_1000
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'�л���'</color> �޼�!", 5f);
            AchievementInfo.kill_1000 = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.kill_10000 && ShopInfo.kill >= 10000) //Kill_10000
        {
            Notification.notification.SetNotification("���� <color=#FF5A00>'���׶�'</color> �޼�!", 5f);
            AchievementInfo.kill_10000 = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.kill_100000 && ShopInfo.kill >= 100000) //Kill_100000
        {
            Notification.notification.SetNotification("���� <color=#FF1E00>'���ǻ�'</color> �޼�!", 5f);
            AchievementInfo.kill_100000 = true;
            isAccomplishAchievement = true;
        }

        if (!AchievementInfo.profit_1 && ShopInfo.profit >= 1) //Profit_1
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'Ƽ�� ��� �»�'</color> �޼�!", 5f);
            AchievementInfo.profit_1 = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.profit_10000 && ShopInfo.profit >= 10000) //Profit_10000
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'������ �ູ'</color> �޼�!", 5f);
            AchievementInfo.profit_10000 = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.profit_100000 && ShopInfo.profit >= 100000) //Profit_100000
        {
            Notification.notification.SetNotification("���� <color=#FFB400>'�� �� �ð�'</color> �޼�!", 5f);
            AchievementInfo.profit_100000 = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.profit_1000000 && ShopInfo.profit >= 1000000) //Profit_1000000
        {
            Notification.notification.SetNotification("���� <color=#FF5A00>'�鸸����'</color> �޼�!", 5f);
            AchievementInfo.profit_1000000 = true;
            isAccomplishAchievement = true;
        }
        if (!AchievementInfo.profit_10000000 && ShopInfo.profit >= 10000000) //Profit_10000000
        {
            Notification.notification.SetNotification("���� <color=#FF1E00>'����� ���'</color> �޼�!", 5f);
            AchievementInfo.profit_10000000 = true;
            isAccomplishAchievement = true;
        }

        if (isAccomplishAchievement) StartCoroutine(SaveAchievement()); //������ �ϳ��� �޼��ϸ� ���� ����
    }

    /* ������ �����ϴ� �ڷ�ƾ �Լ� */
    private IEnumerator SaveAchievement()
    {
        if (!string.IsNullOrEmpty(LoginInfo.userID)) //���� ���� �α����̸�
        {
            yield return StartCoroutine(NetworkManager.networkManager.SetAchievementInfo(LoginInfo.userID, LoginInfo.userName)); //���� ����
            if(NetworkManager.setAchievementInfoResult == NetworkResult.Error) Notification.notification.SetNotification("������ �����ϴ� ���� ������ �߻��߽��ϴ�", 3f);
        }
    }

    /* ������ �ʱ�ȭ�ϴ� �Լ� */
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
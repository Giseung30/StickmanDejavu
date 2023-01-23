using UnityEngine;
using TMPro;

public class CashShopManager : MonoBehaviour
{
    [Header("Money")]
    public TextMeshProUGUI moneyTMP; //Money�� TMP ������Ʈ

    [Header("Cache")]
    private float preMoney = -1; //���� Money

    private void Update()
    {
        SyncMoney();
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

    /* Money Button�� Ŭ���ϸ� ����Ǵ� �Լ� */
    public void OnClickMoneyButton(string productID)
    {
        if (string.IsNullOrEmpty(LoginInfo.userID)) Notification.notification.SetNotification("�Խ�Ʈ �α����� ������ �� �����ϴ�", 2f);
        else IAPManager.iapManager.Purchase(productID);
    }
}
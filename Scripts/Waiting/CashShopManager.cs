using UnityEngine;
using TMPro;

public class CashShopManager : MonoBehaviour
{
    [Header("Money")]
    public TextMeshProUGUI moneyTMP; //Money의 TMP 컴포넌트

    [Header("Cache")]
    private float preMoney = -1; //이전 Money

    private void Update()
    {
        SyncMoney();
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

    /* Money Button을 클릭하면 실행되는 함수 */
    public void OnClickMoneyButton(string productID)
    {
        if (string.IsNullOrEmpty(LoginInfo.userID)) Notification.notification.SetNotification("게스트 로그인은 구매할 수 없습니다", 2f);
        else IAPManager.iapManager.Purchase(productID);
    }
}
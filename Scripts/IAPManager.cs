using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    [Header("Static")]
    public static IAPManager iapManager; //전역 참조 변수

    [Header("Product ID")]
    public readonly string product_ID_10000Money = "10000money";
    public readonly string product_ID_20000Money = "20000money";
    public readonly string product_ID_50000Money = "50000money";
    public readonly string product_ID_100000Money = "100000money";
    public readonly string product_ID_200000Money = "200000money";
    public readonly string product_ID_500000Money = "500000money";

    [Header("Cache")]
    private IStoreController storeController; //구매 과정을 제어하는 함수 제공자
    private IExtensionProvider storeExtensionProvider; //여러 플랫폼을 위한 확장 처리 제공자

    private void Start()
    {
        if (!iapManager)
        {
            iapManager = this;
            InitUnityIAP(); //Start 문에서 초기화 필수
        }
    }

    /* Unity IAP를 초기화하는 함수 */
    private void InitUnityIAP()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(product_ID_10000Money, ProductType.Consumable, new IDs() { { product_ID_10000Money, GooglePlay.Name } });
        builder.AddProduct(product_ID_20000Money, ProductType.Consumable, new IDs() { { product_ID_20000Money, GooglePlay.Name } });
        builder.AddProduct(product_ID_50000Money, ProductType.Consumable, new IDs() { { product_ID_50000Money, GooglePlay.Name } });
        builder.AddProduct(product_ID_100000Money, ProductType.Consumable, new IDs() { { product_ID_100000Money, GooglePlay.Name } });
        builder.AddProduct(product_ID_200000Money, ProductType.Consumable, new IDs() { { product_ID_200000Money, GooglePlay.Name } });
        builder.AddProduct(product_ID_500000Money, ProductType.Consumable, new IDs() { { product_ID_500000Money, GooglePlay.Name } });

        UnityPurchasing.Initialize(this, builder);
    }

    /* 구매하는 함수 */
    public void Purchase(string productId)
    {
        Product product = storeController.products.WithID(productId); //상품 정의
        if (product != null && product.availableToPurchase) storeController.InitiatePurchase(product); //구매가 가능하면 구매 실행
        else Notification.notification.SetNotification("현재 상품을 구매할 수 없습니다", 3f);
    }

    #region Interface
    /* 초기화 성공 시 실행되는 함수 */
    public void OnInitialized(IStoreController controller, IExtensionProvider extension)
    {
        storeController = controller;
        storeExtensionProvider = extension;
    }

    /* 초기화 실패 시 실행되는 함수 */
    public void OnInitializeFailed(InitializationFailureReason error){}

    /* 구매에 실패했을 때 실행되는 함수 */
    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Notification.notification.SetNotification("결제를 완료하지 못했습니다", 3f);
    }

    /* 구매를 처리하는 함수 */
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (args.purchasedProduct.definition.id == product_ID_10000Money)
        {
            ShopInfo.money += 10000; //돈 증가
            ShopInfo.profit += 10000; //수익 증가
            Notification.notification.SetNotification("<color=#B4D7E1>10,000</color> 다이아를 획득했습니다", 2f);
        }
        else if (args.purchasedProduct.definition.id == product_ID_20000Money)
        {
            ShopInfo.money += 20000; //돈 증가
            ShopInfo.profit += 20000; //수익 증가
            Notification.notification.SetNotification("<color=#B4D7E1>20,000</color> 다이아를 획득했습니다", 2f);
        }
        else if (args.purchasedProduct.definition.id == product_ID_50000Money)
        {
            ShopInfo.money += 50000; //돈 증가
            ShopInfo.profit += 50000; //수익 증가
            Notification.notification.SetNotification("<color=#B4D7E1>50,000</color> 다이아를 획득했습니다", 2f);
        }
        else if (args.purchasedProduct.definition.id == product_ID_100000Money)
        {
            ShopInfo.money += 100000; //돈 증가
            ShopInfo.profit += 100000; //수익 증가
            Notification.notification.SetNotification("<color=#B4D7E1>100,000</color> 다이아를 획득했습니다", 2f);
        }
        else if (args.purchasedProduct.definition.id == product_ID_200000Money)
        {
            ShopInfo.money += 200000; //돈 증가
            ShopInfo.profit += 200000; //수익 증가
            Notification.notification.SetNotification("<color=#B4D7E1>200,000</color> 다이아를 획득했습니다", 2f);
        }
        else if (args.purchasedProduct.definition.id == product_ID_500000Money)
        {
            ShopInfo.money += 500000; //돈 증가
            ShopInfo.profit += 500000; //수익 증가
            Notification.notification.SetNotification("<color=#B4D7E1>500,000</color> 다이아를 획득했습니다", 2f);
        }

        StartCoroutine(NetworkManager.networkManager.SetPurchaseInfo(LoginInfo.userID, LoginInfo.userName, args.purchasedProduct.definition.id)); //구매 정보 저장
        WaitingInfo.autoSave = true; //자동 저장

        return PurchaseProcessingResult.Complete;
    }
    #endregion
}
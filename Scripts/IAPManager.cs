using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    [Header("Static")]
    public static IAPManager iapManager; //���� ���� ����

    [Header("Product ID")]
    public readonly string product_ID_10000Money = "10000money";
    public readonly string product_ID_20000Money = "20000money";
    public readonly string product_ID_50000Money = "50000money";
    public readonly string product_ID_100000Money = "100000money";
    public readonly string product_ID_200000Money = "200000money";
    public readonly string product_ID_500000Money = "500000money";

    [Header("Cache")]
    private IStoreController storeController; //���� ������ �����ϴ� �Լ� ������
    private IExtensionProvider storeExtensionProvider; //���� �÷����� ���� Ȯ�� ó�� ������

    private void Start()
    {
        if (!iapManager)
        {
            iapManager = this;
            InitUnityIAP(); //Start ������ �ʱ�ȭ �ʼ�
        }
    }

    /* Unity IAP�� �ʱ�ȭ�ϴ� �Լ� */
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

    /* �����ϴ� �Լ� */
    public void Purchase(string productId)
    {
        Product product = storeController.products.WithID(productId); //��ǰ ����
        if (product != null && product.availableToPurchase) storeController.InitiatePurchase(product); //���Ű� �����ϸ� ���� ����
        else Notification.notification.SetNotification("���� ��ǰ�� ������ �� �����ϴ�", 3f);
    }

    #region Interface
    /* �ʱ�ȭ ���� �� ����Ǵ� �Լ� */
    public void OnInitialized(IStoreController controller, IExtensionProvider extension)
    {
        storeController = controller;
        storeExtensionProvider = extension;
    }

    /* �ʱ�ȭ ���� �� ����Ǵ� �Լ� */
    public void OnInitializeFailed(InitializationFailureReason error){}

    /* ���ſ� �������� �� ����Ǵ� �Լ� */
    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Notification.notification.SetNotification("������ �Ϸ����� ���߽��ϴ�", 3f);
    }

    /* ���Ÿ� ó���ϴ� �Լ� */
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (args.purchasedProduct.definition.id == product_ID_10000Money)
        {
            ShopInfo.money += 10000; //�� ����
            ShopInfo.profit += 10000; //���� ����
            Notification.notification.SetNotification("<color=#B4D7E1>10,000</color> ���̾Ƹ� ȹ���߽��ϴ�", 2f);
        }
        else if (args.purchasedProduct.definition.id == product_ID_20000Money)
        {
            ShopInfo.money += 20000; //�� ����
            ShopInfo.profit += 20000; //���� ����
            Notification.notification.SetNotification("<color=#B4D7E1>20,000</color> ���̾Ƹ� ȹ���߽��ϴ�", 2f);
        }
        else if (args.purchasedProduct.definition.id == product_ID_50000Money)
        {
            ShopInfo.money += 50000; //�� ����
            ShopInfo.profit += 50000; //���� ����
            Notification.notification.SetNotification("<color=#B4D7E1>50,000</color> ���̾Ƹ� ȹ���߽��ϴ�", 2f);
        }
        else if (args.purchasedProduct.definition.id == product_ID_100000Money)
        {
            ShopInfo.money += 100000; //�� ����
            ShopInfo.profit += 100000; //���� ����
            Notification.notification.SetNotification("<color=#B4D7E1>100,000</color> ���̾Ƹ� ȹ���߽��ϴ�", 2f);
        }
        else if (args.purchasedProduct.definition.id == product_ID_200000Money)
        {
            ShopInfo.money += 200000; //�� ����
            ShopInfo.profit += 200000; //���� ����
            Notification.notification.SetNotification("<color=#B4D7E1>200,000</color> ���̾Ƹ� ȹ���߽��ϴ�", 2f);
        }
        else if (args.purchasedProduct.definition.id == product_ID_500000Money)
        {
            ShopInfo.money += 500000; //�� ����
            ShopInfo.profit += 500000; //���� ����
            Notification.notification.SetNotification("<color=#B4D7E1>500,000</color> ���̾Ƹ� ȹ���߽��ϴ�", 2f);
        }

        StartCoroutine(NetworkManager.networkManager.SetPurchaseInfo(LoginInfo.userID, LoginInfo.userName, args.purchasedProduct.definition.id)); //���� ���� ����
        WaitingInfo.autoSave = true; //�ڵ� ����

        return PurchaseProcessingResult.Complete;
    }
    #endregion
}
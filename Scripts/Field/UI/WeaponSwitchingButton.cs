using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitchingButton : MonoBehaviour
{
    [Header("Static")]
    public static WeaponSwitchingButton weaponSwitchingButton; //���� ���� ����

    [Header("Component")]
    public Transform weaponSwitchingButtonTransform; //WeaponSwitchingButton�� Transform ������Ʈ
    public Animator weaponSwitchingButtonAnimator; //Weapon Switching Button�� Animator ������Ʈ
    public Image firstWeaponImage; //First Weapon�� Image ������Ʈ
    public Image secondWeaponImage; //Second Weapon�� Image ������Ʈ
    public Sprite[] weaponIconSprites; //Weapon Icon�� Sprites��

    [Header("Cache")]
    private Weapon firstWeapon; //ù ��° ����
    private Weapon secondWeapon; //�� ��° ����
    private readonly int switch_Hash = Animator.StringToHash("Switch"); //Switch �ִϸ����� �ؽ�
    private readonly Color32 weaponSwitchingButtonNormalColor = new Color32(255, 255, 255, 255); //WeaponSwitchingButton�� �⺻ ����
    private readonly Color32 weaponSwitchingButtonTransparentColor = new Color32(255, 255, 255, 50); //WeaponSwitchingButton�� ���� ����

    private void Awake()
    {
        weaponSwitchingButton = this;
    }

    private void Start()
    {
        firstWeapon = ShopInfo.firstSelectedWeapon; //ù ��° ���� ����
        secondWeapon = ShopInfo.secondSelectedWeapon == Weapon.None ? firstWeapon : ShopInfo.secondSelectedWeapon; //�� ��° ���� ����

        firstWeaponImage.sprite = weaponIconSprites[(int)firstWeapon]; //ù ��° ���� Sprite ����
        secondWeaponImage.sprite = weaponIconSprites[(int)secondWeapon]; //�� ��° ���� Sprite ����

        Player.player.currentWeapon = firstWeapon; //���� �ʱ�ȭ

        if (Application.platform == RuntimePlatform.WindowsEditor) StartCoroutine(OnClickWeaponSwitchingButtonForPC());
    }

    private void Update()
    {
        CheckCoveredEnemy();
    }

    /* Weapon Switching Button�� Ŭ������ �� ����Ǵ� �Լ� - PC�� */
    private System.Collections.IEnumerator OnClickWeaponSwitchingButtonForPC()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.E)) OnClickWeaponSwitchingButton();

            yield return null;
        }
    }

    /* Weapon Switching Button�� Ŭ������ �� ����Ǵ� �Լ� */
    public void OnClickWeaponSwitchingButton()
    {
        if (Player.player.enableMove && AttackJoystick.attackJoystick.axis == 0f) //Player�� �ƹ��� ���۵� ���� �ʴ� ���¸�
            weaponSwitchingButtonAnimator.SetTrigger(switch_Hash); //Change Trigger �۵�
    }

    /* ���⸦ ��ü�ϴ� �Լ� */
    public void SwitchWeapon(bool isFirstWeapon)
    {
        Player.player.currentWeapon = isFirstWeapon ? firstWeapon : secondWeapon;
    }

    /* ������ Enemy�� Ȯ���ϴ� �Լ� */
    private void CheckCoveredEnemy()
    {
        Vector2 worldPosition = MainCamera.mainCamera.mainCameraCamera.ScreenToWorldPoint(weaponSwitchingButtonTransform.position); //Weapon Switching Button�� ���� ���� ��ǥ

        bool isCovered = Physics2D.OverlapCircle(worldPosition, 1f, 1 << LayerMask.NameToLayer("Enemy")); //�������� �Ǵ�

        firstWeaponImage.color = isCovered ? weaponSwitchingButtonTransparentColor : weaponSwitchingButtonNormalColor;
        secondWeaponImage.color = isCovered ? weaponSwitchingButtonTransparentColor : weaponSwitchingButtonNormalColor;
    }
}
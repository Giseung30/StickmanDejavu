using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitchingButton : MonoBehaviour
{
    [Header("Static")]
    public static WeaponSwitchingButton weaponSwitchingButton; //전역 참조 변수

    [Header("Component")]
    public Transform weaponSwitchingButtonTransform; //WeaponSwitchingButton의 Transform 컴포넌트
    public Animator weaponSwitchingButtonAnimator; //Weapon Switching Button의 Animator 컴포넌트
    public Image firstWeaponImage; //First Weapon의 Image 컴포넌트
    public Image secondWeaponImage; //Second Weapon의 Image 컴포넌트
    public Sprite[] weaponIconSprites; //Weapon Icon의 Sprites들

    [Header("Cache")]
    private Weapon firstWeapon; //첫 번째 무기
    private Weapon secondWeapon; //두 번째 무기
    private readonly int switch_Hash = Animator.StringToHash("Switch"); //Switch 애니메이터 해쉬
    private readonly Color32 weaponSwitchingButtonNormalColor = new Color32(255, 255, 255, 255); //WeaponSwitchingButton의 기본 색상
    private readonly Color32 weaponSwitchingButtonTransparentColor = new Color32(255, 255, 255, 50); //WeaponSwitchingButton의 투명 색상

    private void Awake()
    {
        weaponSwitchingButton = this;
    }

    private void Start()
    {
        firstWeapon = ShopInfo.firstSelectedWeapon; //첫 번째 무기 지정
        secondWeapon = ShopInfo.secondSelectedWeapon == Weapon.None ? firstWeapon : ShopInfo.secondSelectedWeapon; //두 번째 무기 지정

        firstWeaponImage.sprite = weaponIconSprites[(int)firstWeapon]; //첫 번째 무기 Sprite 지정
        secondWeaponImage.sprite = weaponIconSprites[(int)secondWeapon]; //두 번째 무기 Sprite 지정

        Player.player.currentWeapon = firstWeapon; //무기 초기화

        if (Application.platform == RuntimePlatform.WindowsEditor) StartCoroutine(OnClickWeaponSwitchingButtonForPC());
    }

    private void Update()
    {
        CheckCoveredEnemy();
    }

    /* Weapon Switching Button을 클릭했을 때 실행되는 함수 - PC용 */
    private System.Collections.IEnumerator OnClickWeaponSwitchingButtonForPC()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.E)) OnClickWeaponSwitchingButton();

            yield return null;
        }
    }

    /* Weapon Switching Button을 클릭했을 때 실행되는 함수 */
    public void OnClickWeaponSwitchingButton()
    {
        if (Player.player.enableMove && AttackJoystick.attackJoystick.axis == 0f) //Player가 아무런 동작도 하지 않는 상태면
            weaponSwitchingButtonAnimator.SetTrigger(switch_Hash); //Change Trigger 작동
    }

    /* 무기를 교체하는 함수 */
    public void SwitchWeapon(bool isFirstWeapon)
    {
        Player.player.currentWeapon = isFirstWeapon ? firstWeapon : secondWeapon;
    }

    /* 가려진 Enemy를 확인하는 함수 */
    private void CheckCoveredEnemy()
    {
        Vector2 worldPosition = MainCamera.mainCamera.mainCameraCamera.ScreenToWorldPoint(weaponSwitchingButtonTransform.position); //Weapon Switching Button의 월드 상의 좌표

        bool isCovered = Physics2D.OverlapCircle(worldPosition, 1f, 1 << LayerMask.NameToLayer("Enemy")); //가리는지 판단

        firstWeaponImage.color = isCovered ? weaponSwitchingButtonTransparentColor : weaponSwitchingButtonNormalColor;
        secondWeaponImage.color = isCovered ? weaponSwitchingButtonTransparentColor : weaponSwitchingButtonNormalColor;
    }
}
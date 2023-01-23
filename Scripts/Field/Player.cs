using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Static")]
    public static Player player; //전역 참조 변수

    [Header("Variable")]
    public float currentHP; //현재 체력

    private bool _enableImmortal;
    public bool enableImmortal
    {
        get 
        { 
            return _enableImmortal; 
        }
        set 
        { 
            _enableImmortal = value; 
        }
    } //무적 변수

    private bool _enableMove = true;
    public bool enableMove
    {
        get
        {
            return _enableMove;
        }
        set
        {
            if (!value) upTransform.localRotation = Quaternion.Euler(Vector3.zero); //움직이지 못하면 상체 원래대로 복구
            _enableMove = value;
        }
    } //이동 제한 변수

    private bool _enableAttack = true;
    public bool enableAttack
    {
        get
        {
            return _enableAttack;
        }
        set
        {
            _enableAttack = value;
        }
    } //공격 제한 변수

    private Weapon _currentWeapon;
    public Weapon currentWeapon
    {
        set
        {
            if (Weapon.Fist == value) //Fist
            {
                playerAnimator.Play(MoveJoystick.moveJoystick.axis.magnitude == 0f ? fist_Idle_Hash : fist_Run_Hash);
                downAnimator.Play(MoveJoystick.moveJoystick.axis.magnitude == 0f ? fist_Idle_Hash : fist_Run_Hash);
            }
            else if (Weapon.Sword == value) //Sword
            {
                playerAnimator.Play(MoveJoystick.moveJoystick.axis.magnitude == 0f ? sword_Idle_Hash : sword_Run_Hash);
                downAnimator.Play(MoveJoystick.moveJoystick.axis.magnitude == 0f ? sword_Idle_Hash : sword_Run_Hash);
            }
            else if (Weapon.Gun == value) //Gun
            {
                playerAnimator.Play(MoveJoystick.moveJoystick.axis.magnitude == 0f ? gun_Idle_Hash : gun_Run_Hash);
                downAnimator.Play(MoveJoystick.moveJoystick.axis.magnitude == 0f ? gun_Idle_Hash : gun_Run_Hash);
            }
            else if (Weapon.Sniper == value) //Sniper
            {
                playerAnimator.Play(MoveJoystick.moveJoystick.axis.magnitude == 0f ? sniper_Idle_Hash : sniper_Run_Hash);
                downAnimator.Play(MoveJoystick.moveJoystick.axis.magnitude == 0f ? sniper_Idle_Hash : sniper_Run_Hash);
            }
            else if (Weapon.Bazooka == value) //Bazooka
            {
                playerAnimator.Play(MoveJoystick.moveJoystick.axis.magnitude == 0f ? bazooka_Idle_Hash : bazooka_Run_Hash);
                downAnimator.Play(MoveJoystick.moveJoystick.axis.magnitude == 0f ? bazooka_Idle_Hash : bazooka_Run_Hash);
            }
            else if (Weapon.Wizard == value) //Wizard
            {
                playerAnimator.Play(MoveJoystick.moveJoystick.axis.magnitude == 0f ? wizard_Idle_Hash : wizard_Run_Hash);
                downAnimator.Play(MoveJoystick.moveJoystick.axis.magnitude == 0f ? wizard_Idle_Hash : wizard_Run_Hash);
            }

            UltButton.ultButton.currentUlt = value; //궁극기 버튼 변경

            _currentWeapon = value; //값 지정
        }
        get
        {
            return _currentWeapon;
        }
    } //현재 무기

    private int _sniperUltBulletCount;
    public int sniperUltBulletCount
    {
        get
        {
            return _sniperUltBulletCount;
        }
        set
        {
            _sniperUltBulletCount = value;
        }
    } //Sniper의 Ult 총알 개수

    [Header("Component")]
    public Transform playerTransform; //Player의 Transform 컴포넌트
    public Transform upTransform; //Up의 Transform 컴포넌트
    public Rigidbody2D playerRigidbody2D; //Player의 Rigidbody2D 컴포넌트
    public Animator playerAnimator; //Player의 Animator 컴포넌트
    public Animator downAnimator; //Down의 Animator 컴포넌트

    [Header("Layer Order")]
    public SpriteRenderer upSpriteRenderer; //Up의 SpriteRenderer 컴포넌트
    public SpriteRenderer downSpriteRenderer; //Down의 SpriteRenderer 컴포넌트
    public SpriteRenderer shadowSpriteRenderer; //Shadow의 SpriteRenderer 컴포넌트
    public SpriteRenderer[] layerOrderSpriteRenderers; //Layer 순서용 Sprite Renderer 컴포넌트들
    public ParticleSystemRenderer[] layerOrderParticleSystemRenderers; //Layer 순서용 Particle System Renderer 컴포넌트들

    [Header("Damaged")]
    public Material playerMaterial; //Player의 Material 컴포넌트
    public float damagedColorDisplayTime; //Damaged 색상 표시 시간
    private float playerMaterialAlpha; //Player Material의 Alpha 값

    #region Stat
    public float maxHp
    {
        get
        {
            return Definition.defaultMaxHP + Definition.maxHPAdder * ShopInfo.maxHPLevel;
        }
    } //최대 체력
    private float recoveryHP
    {
        get
        {
            return Definition.defaultRecoveryHP + Definition.recoveryHPAdder * ShopInfo.recoveryHPLevel;
        }
    } //체력 회복률
    private float defense
    {
        get
        {
            float defense = currentWeapon == Weapon.Fist ? Definition.fistDefense :
                currentWeapon == Weapon.Sword ? Definition.swordDefense :
                currentWeapon == Weapon.Gun ? Definition.gunDefense :
                currentWeapon == Weapon.Sniper ? Definition.sniperDefense :
                currentWeapon == Weapon.Bazooka ? Definition.bazookaDefense :
                Definition.wizardDefense;
            return defense + Definition.defenseAdder * ShopInfo.defenseLevel;
        }
    } //방어력
    private float moveSpeed
    {
        get
        {
            float moveSpeed = currentWeapon == Weapon.Fist ? Definition.fistMoveSpeed :
                currentWeapon == Weapon.Sword ? Definition.swordMoveSpeed :
                currentWeapon == Weapon.Gun ? Definition.gunMoveSpeed :
                currentWeapon == Weapon.Sniper ? Definition.sniperMoveSpeed :
                currentWeapon == Weapon.Bazooka ? Definition.bazookaMoveSpeed :
                Definition.wizardMoveSpeed;
            return moveSpeed + Definition.moveSpeedAdder * ShopInfo.moveSpeedLevel;
        }
    } //이동 속도
    public int moneyAmount
    {
        get
        {
            return Definition.defaultMoneyAmount + Definition.moneyAmountAdder * ShopInfo.moneyAmountLevel;
        }
    } //돈 획득량
    public float moneyProbability
    {
        get
        {
            return Definition.defaultMoneyProbability + Definition.moneyProbabilityAdder * ShopInfo.moneyProbabilityLevel;
        }
    } //돈 획득 확률
    public float criticalDamage
    {
        get
        {
            return Definition.defaultCriticalDamage + Definition.criticalDamageAdder * ShopInfo.criticalDamageLevel;
        }
    } //치명타 피해량
    public float criticalProbability
    {
        get
        {
            return Definition.defaultCriticalProbability + Definition.criticalProbabilityAdder * ShopInfo.criticalProbabilityLevel;
        }
    } //치명타 확률
    #endregion

    #region Weapon Detail
    [Header("Fist Detail")]
    public PlayerAttackBoundManager[] fistAttackDamageMultiplierPlayerBoundManagers; //Fist Attack의 DamageMultiplier 용 PlayerBoundManager 컴포넌트들
    public ParticleSystem[] fistAttackSpeedMultiplierParticleSystems; //Fist Attack의 SpeedMultiplier 용 Particle System 컴포넌트들
    public PlayerAttackBoundManager[] fistUltDamageMultiplierPlayerBoundManagers; //Fist Ult의 DamageMultiplier 용 PlayerBoundManager 컴포넌트들
    public ParticleSystem[] fistUltSpeedMultiplierParticleSystems; //Fist Ult의 SpeedMultiplier 용 Particle System 컴포넌트들

    [Header("Sword Detail")]
    public PlayerAttackBoundManager[] swordAttackDamageMultiplierPlayerBoundManagers; //Sword Attack의 DamageMultiplier 용 PlayerBoundManager 컴포넌트들
    public ParticleSystem[] swordAttackSpeedMultiplierParticleSystems; //Sword Attack의 SpeedMultiplier 용 Particle System 컴포넌트들
    public PlayerAttackBoundManager[] swordUltDamageMultiplierPlayerBoundManagers; //Sword Ult의 DamageMultiplier 용 PlayerBoundManager 컴포넌트들
    public ParticleSystem[] swordUltSpeedMultiplierParticleSystems; //Sword Ult의 SpeedMultiplier 용 Particle System 컴포넌트들

    [Header("Gun Detail")]
    public PlayerAttackBoundManager[] gunAttackDamageMultiplierPlayerBoundManagers; //Gun Attack의 DamageMultiplier 용 PlayerBoundManager 컴포넌트들
    public ParticleSystem[] gunAttackSpeedMultiplierParticleSystems; //Gun Attack의 SpeedMultiplier 용 Particle System 컴포넌트들
    public PlayerAttackBoundManager[] gunUltDamageMultiplierPlayerBoundManagers; //Gun Ult의 DamageMultiplier 용 PlayerBoundManager 컴포넌트들
    public ParticleSystem[] gunUltSpeedMultiplierParticleSystems; //Gun Ult의 SpeedMultiplier 용 Particle System 컴포넌트들

    [Header("Sniper Detail")]
    public PlayerAttackBoundManager[] sniperAttackDamageMultiplierPlayerBoundManagers; //Sniper Attack의 DamageMultiplier 용 PlayerBoundManager 컴포넌트들
    public ParticleSystem[] sniperAttackSpeedMultiplierParticleSystems; //Sniper Attack의 SpeedMultiplier 용 Particle System 컴포넌트들
    public ParticleSystem[] sniperUltSpeedMultiplierParticleSystems; //Sniper Ult의 SpeedMultiplier 용 Particle System 컴포넌트들

    [Header("Bazooka Detail")]
    public PlayerAttackBoundManager[] bazookaAttackDamageMultiplierPlayerBoundManagers; //Bazooka Attack의 DamageMultiplier 용 PlayerBoundManager 컴포넌트들
    public PlayerAttackBoundManager[] bazookaUltDamageMultiplierPlayerBoundManagers; //Bazooka Ult의 DamageMultiplier 용 PlayerBoundManager 컴포넌트들
    public ParticleSystem[] bazookaUltSpeedMultiplierParticleSystems; //Bazooka Ult의 SpeedMultiplier 용 Particle System 컴포넌트들

    [Header("Wizard Detail")]
    public PlayerAttackBoundManager[] wizardAttackMultiplierPlayerBoundManagers; //Wizard Attack의 Multiplier 용 PlayerBoundManager 컴포넌트들
    public PlayerAttackBoundManager[] wizardUltMultiplierPlayerBoundManagers; //Wizard Ult의 Multiplier 용 PlayerBoundManager 컴포넌트들
    #endregion

    [Header("AnimationHash")]
    private readonly int fist_Idle_Hash = Animator.StringToHash("Fist_Idle"); //Fist_Idle 해쉬
    private readonly int sword_Idle_Hash = Animator.StringToHash("Sword_Idle"); //Sword_Idle 해쉬
    private readonly int gun_Idle_Hash = Animator.StringToHash("Gun_Idle"); //Gun_Idle 해쉬
    private readonly int sniper_Idle_Hash = Animator.StringToHash("Sniper_Idle"); //Sniper_Idle 해쉬
    private readonly int bazooka_Idle_Hash = Animator.StringToHash("Bazooka_Idle"); //Bazooka_Idle 해쉬
    private readonly int wizard_Idle_Hash = Animator.StringToHash("Wizard_Idle"); //Wizard_Idle 해쉬

    private readonly int fist_Run_Hash = Animator.StringToHash("Fist_Run"); //Fist_Run 해쉬
    private readonly int sword_Run_Hash = Animator.StringToHash("Sword_Run"); //Sword_Run 해쉬
    private readonly int gun_Run_Hash = Animator.StringToHash("Gun_Run"); //Gun_Run 해쉬
    private readonly int sniper_Run_Hash = Animator.StringToHash("Sniper_Run"); //Sniper_Run 해쉬
    private readonly int bazooka_Run_Hash = Animator.StringToHash("Bazooka_Run"); //Bazooka_Run 해쉬
    private readonly int wizard_Run_Hash = Animator.StringToHash("Wizard_Run"); //Wizard_Run 해쉬

    private readonly int run_Hash = Animator.StringToHash("Run"); //Run 해쉬
    private readonly int attack_Hash = Animator.StringToHash("Attack"); //Attack 해쉬
    private readonly int sniperUltBulletCount_Hash = Animator.StringToHash("SniperUltBulletCount"); //SniperUltBulletCount 해쉬
    private readonly int run_Speed_Hash = Animator.StringToHash("Run_Speed"); //Run_Speed 해쉬
    private readonly int sniperUltShot_Hash = Animator.StringToHash("SniperUltShot"); //SniperUltShot 해쉬

    private void Awake()
    {
        player = this;
    }

    private void Start()
    {
        currentHP = maxHp; //현재 체력 지정
        playerMaterialAlpha = 0f;
        SetWeaponLevel();
    }

    private void Update()
    {
        FadeDamagedColor();

        ControlAnimator();

        RecoverHP();

        RotatePlayer();
        SetLayerOrder();
        SetScale();

        SetSniperUltShotTrigger();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    /* 무기 레벨을 적용하는 함수 */
    private void SetWeaponLevel()
    {
        ParticleSystem.MainModule main;

        /* Fist Attack */
        for (int i = 0; i < fistAttackDamageMultiplierPlayerBoundManagers.Length; ++i)
        {
            fistAttackDamageMultiplierPlayerBoundManagers[i].damageMultiplier = Definition.fistAttackDamageMultiplier[ShopInfo.fistAttackLevel]; //Damage Multiplier 지정
        }
        playerAnimator.SetFloat("Fist_Attack_Speed", Definition.fistAttackSpeedMultiplier[ShopInfo.fistAttackLevel / 3]); //Animator 파라미터 지정
        for (int i = 0; i < fistAttackSpeedMultiplierParticleSystems.Length; ++i) //ParticleSystem 속도 지정
        {
            main = fistAttackSpeedMultiplierParticleSystems[i].main;
            main.simulationSpeed = Definition.fistAttackSpeedMultiplier[ShopInfo.fistAttackLevel / 3];
        }

        /* Fist Ult */
        for (int i = 0; i < fistUltDamageMultiplierPlayerBoundManagers.Length; ++i)
        {
            fistUltDamageMultiplierPlayerBoundManagers[i].damageMultiplier = Definition.fistUltDamageMultiplier[ShopInfo.fistUltLevel]; //Damage Multiplier 지정
        }
        playerAnimator.SetFloat("Fist_Ult_Speed", Definition.fistUltSpeedMultiplier[ShopInfo.fistUltLevel / 3]); //Animator 파라미터 지정
        for (int i = 0; i < fistUltSpeedMultiplierParticleSystems.Length; ++i) //ParticleSystem 속도 지정
        {
            main = fistUltSpeedMultiplierParticleSystems[i].main;
            main.simulationSpeed = Definition.fistUltSpeedMultiplier[ShopInfo.fistUltLevel / 3];
        }

        /* Sword Attack */
        for (int i = 0; i < swordAttackDamageMultiplierPlayerBoundManagers.Length; ++i)
        {
            swordAttackDamageMultiplierPlayerBoundManagers[i].damageMultiplier = Definition.swordAttackDamageMultiplier[ShopInfo.swordAttackLevel]; //Damage Multiplier 지정
        }
        playerAnimator.SetFloat("Sword_Attack_Speed", Definition.swordAttackSpeedMultiplier[ShopInfo.swordAttackLevel / 3]); //Animator 파라미터 지정
        for (int i = 0; i < swordAttackSpeedMultiplierParticleSystems.Length; ++i) //ParticleSystem 속도 지정
        {
            main = swordAttackSpeedMultiplierParticleSystems[i].main;
            main.simulationSpeed = Definition.swordAttackSpeedMultiplier[ShopInfo.swordAttackLevel / 3];
        }

        /* Sword Ult */
        for (int i = 0; i < swordUltDamageMultiplierPlayerBoundManagers.Length; ++i)
        {
            swordUltDamageMultiplierPlayerBoundManagers[i].damageMultiplier = Definition.swordUltDamageMultiplier[ShopInfo.swordUltLevel]; //Damage Multiplier 지정
        }
        playerAnimator.SetFloat("Sword_Ult_Speed", Definition.swordUltSpeedMultiplier[ShopInfo.swordUltLevel / 3]); //Animator 파라미터 지정
        for (int i = 0; i < swordUltSpeedMultiplierParticleSystems.Length; ++i) //ParticleSystem 속도 지정
        {
            main = swordUltSpeedMultiplierParticleSystems[i].main;
            main.simulationSpeed = Definition.swordUltSpeedMultiplier[ShopInfo.swordUltLevel / 3];
        }

        /* Gun Attack */
        for (int i = 0; i < gunAttackDamageMultiplierPlayerBoundManagers.Length; ++i)
        {
            gunAttackDamageMultiplierPlayerBoundManagers[i].damageMultiplier = Definition.gunAttackDamageMultiplier[ShopInfo.gunAttackLevel]; //Damage Multiplier 지정
        }
        playerAnimator.SetFloat("Gun_Attack_Speed", Definition.gunAttackSpeedMultiplier[ShopInfo.gunAttackLevel / 3]); //Animator 파라미터 지정
        for (int i = 0; i < gunAttackSpeedMultiplierParticleSystems.Length; ++i) //ParticleSystem 속도 지정
        {
            main = gunAttackSpeedMultiplierParticleSystems[i].main;
            main.simulationSpeed = Definition.gunAttackSpeedMultiplier[ShopInfo.gunAttackLevel / 3];
        }

        /* Gun Ult */
        for (int i = 0; i < gunUltDamageMultiplierPlayerBoundManagers.Length; ++i)
        {
            gunUltDamageMultiplierPlayerBoundManagers[i].damageMultiplier = Definition.gunUltDamageMultiplier[ShopInfo.gunUltLevel]; //Damage Multiplier 지정
        }
        playerAnimator.SetFloat("Gun_Ult_Speed", Definition.gunUltSpeedMultiplier[ShopInfo.gunUltLevel / 3]); //Animator 파라미터 지정
        for (int i = 0; i < gunUltSpeedMultiplierParticleSystems.Length; ++i) //ParticleSystem 속도 지정
        {
            main = gunUltSpeedMultiplierParticleSystems[i].main;
            main.simulationSpeed = Definition.gunUltSpeedMultiplier[ShopInfo.gunUltLevel / 3];
        }

        /* Sniper Attack */
        for (int i = 0; i < sniperAttackDamageMultiplierPlayerBoundManagers.Length; ++i)
        {
            sniperAttackDamageMultiplierPlayerBoundManagers[i].damageMultiplier = Definition.sniperAttackDamageMultiplier[ShopInfo.sniperAttackLevel]; //Damage Multiplier 지정
        }
        playerAnimator.SetFloat("Sniper_Attack_Speed", Definition.sniperAttackSpeedMultiplier[ShopInfo.sniperAttackLevel / 3]); //Animator 파라미터 지정
        for (int i = 0; i < sniperAttackSpeedMultiplierParticleSystems.Length; ++i) //ParticleSystem 속도 지정
        {
            main = sniperAttackSpeedMultiplierParticleSystems[i].main;
            main.simulationSpeed = Definition.sniperAttackSpeedMultiplier[ShopInfo.sniperAttackLevel / 3];
        }

        /* Sniper Ult */
        playerAnimator.SetFloat("Sniper_Ult_Speed", Definition.sniperUltSpeedMultiplier[ShopInfo.sniperUltLevel / 3]); //Animator 파라미터 지정
        for (int i = 0; i < sniperUltSpeedMultiplierParticleSystems.Length; ++i) //ParticleSystem 속도 지정
        {
            main = sniperUltSpeedMultiplierParticleSystems[i].main;
            main.simulationSpeed = Definition.sniperUltSpeedMultiplier[ShopInfo.sniperUltLevel / 3];
        }

        /* Bazooka Attack */
        for (int i = 0; i < bazookaAttackDamageMultiplierPlayerBoundManagers.Length; ++i)
        {
            bazookaAttackDamageMultiplierPlayerBoundManagers[i].damageMultiplier = Definition.bazookaAttackDamageMultiplier[ShopInfo.bazookaAttackLevel]; //Damage Multiplier 지정
        }
        playerAnimator.SetFloat("Bazooka_Attack_Speed", Definition.bazookaAttackSpeedMultiplier[ShopInfo.bazookaAttackLevel / 3]); //Animator 파라미터 지정

        /* Bazooka Ult */
        for (int i = 0; i < bazookaUltDamageMultiplierPlayerBoundManagers.Length; ++i)
        {
            bazookaUltDamageMultiplierPlayerBoundManagers[i].damageMultiplier = Definition.bazookaUltDamageMultiplier[ShopInfo.bazookaUltLevel]; //Damage Multiplier 지정
        }
        playerAnimator.SetFloat("Bazooka_Ult_Speed", Definition.bazookaUltSpeedMultiplier[ShopInfo.bazookaUltLevel / 3]); //Animator 파라미터 지정
        for (int i = 0; i < bazookaUltSpeedMultiplierParticleSystems.Length; ++i) //ParticleSystem 속도 지정
        {
            main = bazookaUltSpeedMultiplierParticleSystems[i].main;
            main.simulationSpeed = Definition.bazookaUltSpeedMultiplier[ShopInfo.bazookaUltLevel / 3];
        }

        /* Wizard Attack */
        for (int i = 0; i < wizardAttackMultiplierPlayerBoundManagers.Length; ++i)
        {
            wizardAttackMultiplierPlayerBoundManagers[i].damageMultiplier = Definition.wizardAttackDamageMultiplier[ShopInfo.wizardAttackLevel]; //Damage Multiplier 지정
            wizardAttackMultiplierPlayerBoundManagers[i].slowMultiplier = Definition.wizardAttackSpeedMultiplier[ShopInfo.wizardAttackLevel / 3]; //Speed Multiplier 지정
        }

        /* Wizard Ult */
        for (int i = 0; i < wizardUltMultiplierPlayerBoundManagers.Length; ++i)
        {
            wizardUltMultiplierPlayerBoundManagers[i].damageMultiplier = Definition.wizardUltDamageMultiplier[ShopInfo.wizardUltLevel]; //Damage Multiplier 지정
            wizardUltMultiplierPlayerBoundManagers[i].slowMultiplier = Definition.wizardUltSpeedMultiplier[ShopInfo.wizardUltLevel / 3]; //Speed Multiplier 지정
        }
    }

    /* Animator를 조작하는 함수 */
    private void ControlAnimator()
    {
        /* Player */
        playerAnimator.SetBool(run_Hash, MoveJoystick.moveJoystick.axis.magnitude != 0f); //Run 애니메이션 State
        playerAnimator.SetBool(attack_Hash, AttackJoystick.attackJoystick.axis != 0f && enableAttack); //Attack 애니메이션 State
        playerAnimator.SetInteger(sniperUltBulletCount_Hash, sniperUltBulletCount); //SniperUltBulletCount 개수 갱신
        playerAnimator.SetFloat(run_Speed_Hash, moveSpeed / Definition.defaultMoveSpeed); //Run_Speed 파라미터 갱신

        /* Down */
        downAnimator.SetBool(run_Hash, MoveJoystick.moveJoystick.axis.magnitude != 0f); //Run 애니메이션 State
        downAnimator.SetFloat(run_Speed_Hash, moveSpeed / Definition.defaultMoveSpeed); //Run_Speed 파라미터 갱신
    }

    /* HP를 회복하는 함수 */
    private void RecoverHP()
    {
        if (currentHP <= 0f || sniperUltBulletCount > 0) return; //특정 상황이면 종료

        float resultHP = currentHP + recoveryHP * Time.deltaTime; //결과 HP
        currentHP = resultHP > maxHp ? maxHp : resultHP; //HP 회복
    }

    #region Damaged
    /* 피해를 입는 함수 */
    public void GetDamage(float damage)
    {
        if (enableImmortal) return; //무적 상태이면 종료

        damage *= (1f - defense); //방어력 적용
        currentHP = currentHP - damage < 0f ? 0f : currentHP - damage; //HP 변경
        playerMaterialAlpha = 1f;

        if (currentHP <= 0f) //체력이 모두 소진되면
        {
            Utility.utility.LoadStartCoroutine(FieldManager.fieldManager.FinishStage(false)); //단계 종료
            Destroy(gameObject); //Player 파괴
        }
    }

    /* Damaged 색상을 사라지게 하는 함수 */
    private void FadeDamagedColor()
    {
        playerMaterialAlpha = Mathf.Clamp(playerMaterialAlpha - Time.deltaTime / damagedColorDisplayTime, 0f, 1f); //알파 값 감소
        playerMaterial.SetFloat("_Alpha", playerMaterialAlpha); //알파 값 적용
    }
    #endregion

    #region Transform
    /* Player를 움직이는 함수 */
    private void MovePlayer()
    {
        if (enableMove) //이동할 수 있으면
        {
            float axisYLerp = (playerTransform.position.y - Definition.moveYRange.x) / (Definition.moveYRange.y - Definition.moveYRange.x); //Y축 값을 0 ~ 1 사이의 값으로 보간
            float resultMoveSpeed = moveSpeed * Mathf.Lerp(Definition.moveSpeedRange.y, Definition.moveSpeedRange.x, axisYLerp); //Y축 보간 값으로 이동 속도 결정

            Vector2 resultPosition = (Vector2)playerTransform.position + resultMoveSpeed * Time.deltaTime * MoveJoystick.moveJoystick.axis; //결과 위치 저장
            resultPosition = new Vector2(Mathf.Clamp(resultPosition.x, Definition.moveXRange.x, Definition.moveXRange.y), Mathf.Clamp(resultPosition.y, Definition.moveYRange.x, Definition.moveYRange.y)); //범위 제한
            playerRigidbody2D.position = resultPosition; //이동 적용
        }
    }

    /* Player를 회전하는 함수 */
    private void RotatePlayer()
    {
        if (enableMove) //움직일 수 있으면
        {
            playerTransform.rotation = Quaternion.Euler(playerTransform.eulerAngles.x, MoveJoystick.moveJoystick.horizontalMoveDirection == -1 ? 180f : MoveJoystick.moveJoystick.horizontalMoveDirection == 1 ? 0f : playerTransform.eulerAngles.y, playerTransform.eulerAngles.z);

            if (AttackJoystick.attackJoystick.axis == 0f) // AttackJoystick의 축 변화가 없으면
                upTransform.localRotation = Quaternion.Euler(Vector3.zero);
            else //AttackJoystick의 축 변화가 있으면
                upTransform.rotation = Quaternion.Euler(upTransform.eulerAngles.x, AttackJoystick.attackJoystick.axis == -1 ? 180f : AttackJoystick.attackJoystick.axis == 1 ? 0f : upTransform.eulerAngles.y, upTransform.eulerAngles.z);
        }
    }

    /* 레이어 순서를 지정하는 함수 */
    private void SetLayerOrder()
    {
        float axisYLerp = (playerTransform.position.y - Definition.moveYRange.x) / (Definition.moveYRange.y - Definition.moveYRange.x); //Y축 값을 0 ~ 1 사이의 값으로 보간

        int resultLayerOrder = (int)Mathf.Lerp(Definition.layerOrderRange.y, Definition.layerOrderRange.x, axisYLerp); //Y축 보간 값으로 레이어 순서 결과 저장
        upSpriteRenderer.sortingOrder = resultLayerOrder + 2; //Up 레이어 순서 보정
        downSpriteRenderer.sortingOrder = resultLayerOrder + 1; //Down 레이어 순서 보정
        shadowSpriteRenderer.sortingOrder = resultLayerOrder; //Shadow 레이어 순서 보정

        for (int i = 0; i < layerOrderSpriteRenderers.Length; ++i) layerOrderSpriteRenderers[i].sortingOrder = resultLayerOrder + 3; //Layer 순서용 Sprite Renderer 컴포넌트들의 레이어 순서 지정
        for (int i = 0; i < layerOrderParticleSystemRenderers.Length; ++i) layerOrderParticleSystemRenderers[i].sortingOrder = resultLayerOrder + 3; //Layer 순서용 Particle System Renderer 컴포넌트들의 레이어 순서 지정
    }

    /* Player의 크기를 지정하는 함수 */
    private void SetScale()
    {
        float axisYLerp = (playerTransform.position.y - Definition.moveYRange.x) / (Definition.moveYRange.y - Definition.moveYRange.x); //Y축 값을 0 ~ 1 사이의 값으로 보간

        float resultScale = Mathf.Lerp(Definition.scaleRange.y, Definition.scaleRange.x, axisYLerp); //Y축 보간 값으로 크기 결과 저장
        playerTransform.localScale = new Vector3(resultScale, resultScale, resultScale); //크기 보정
    }
    #endregion

    #region Sniper
    /* SniperUltShot 트리거를 발생시키는 함수 */
    private void SetSniperUltShotTrigger()
    {
        if (sniperUltBulletCount > 0) //총알이 남아있으면
        {
            if (Input.GetMouseButtonDown(0)) //클릭이 발생하면
            {
                playerAnimator.SetTrigger(sniperUltShot_Hash); //애니메이션 트리거 발생
            }
        }
    }

    public GameObject sniperParticleUlt3; //Sniper Ult의 파티클 3 오브젝트
    /* Sniper의 Ult 총알을 발사하는 함수 */
    public void ShotSniperUltBullet()
    {
        sniperUltBulletCount--; //총알 개수 감소

        Vector2 mousePosition = MainCamera.mainCamera.mainCameraCamera.ScreenToWorldPoint(Input.mousePosition); //마우스 위치 저장

        playerTransform.rotation = Quaternion.Euler(playerTransform.eulerAngles.x, mousePosition.x - playerTransform.position.x > 0f ? 0f : -180f, playerTransform.eulerAngles.z); //마우스 위치를 향해 Player 회전

        Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePosition, 0.5f, 1 << LayerMask.NameToLayer("Enemy")); //클릭 지점으로부터 모든 적 탐색
        for (int i = 0; i < colliders.Length; ++i)
        {
            Enemy enemy = colliders[i].GetComponent<Enemy>();
            enemy.GetDamage(enemy.maxHP * 0.2f * Definition.sniperUltDamageMultiplier[ShopInfo.sniperUltLevel], 0f); //최대 체력에 비례한 피해
        }

        GameObject sniperUltEffect = Instantiate(sniperParticleUlt3); //Sniper Ult 이펙트 복사
        sniperUltEffect.transform.position = mousePosition; //마우스 위치로 이동
        sniperUltEffect.transform.localScale = Vector3.one; //크기 일정하게 변경
        sniperUltEffect.SetActive(true); //활성화
    }
    #endregion
}
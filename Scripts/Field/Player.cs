using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Static")]
    public static Player player; //���� ���� ����

    [Header("Variable")]
    public float currentHP; //���� ü��

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
    } //���� ����

    private bool _enableMove = true;
    public bool enableMove
    {
        get
        {
            return _enableMove;
        }
        set
        {
            if (!value) upTransform.localRotation = Quaternion.Euler(Vector3.zero); //�������� ���ϸ� ��ü ������� ����
            _enableMove = value;
        }
    } //�̵� ���� ����

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
    } //���� ���� ����

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

            UltButton.ultButton.currentUlt = value; //�ñر� ��ư ����

            _currentWeapon = value; //�� ����
        }
        get
        {
            return _currentWeapon;
        }
    } //���� ����

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
    } //Sniper�� Ult �Ѿ� ����

    [Header("Component")]
    public Transform playerTransform; //Player�� Transform ������Ʈ
    public Transform upTransform; //Up�� Transform ������Ʈ
    public Rigidbody2D playerRigidbody2D; //Player�� Rigidbody2D ������Ʈ
    public Animator playerAnimator; //Player�� Animator ������Ʈ
    public Animator downAnimator; //Down�� Animator ������Ʈ

    [Header("Layer Order")]
    public SpriteRenderer upSpriteRenderer; //Up�� SpriteRenderer ������Ʈ
    public SpriteRenderer downSpriteRenderer; //Down�� SpriteRenderer ������Ʈ
    public SpriteRenderer shadowSpriteRenderer; //Shadow�� SpriteRenderer ������Ʈ
    public SpriteRenderer[] layerOrderSpriteRenderers; //Layer ������ Sprite Renderer ������Ʈ��
    public ParticleSystemRenderer[] layerOrderParticleSystemRenderers; //Layer ������ Particle System Renderer ������Ʈ��

    [Header("Damaged")]
    public Material playerMaterial; //Player�� Material ������Ʈ
    public float damagedColorDisplayTime; //Damaged ���� ǥ�� �ð�
    private float playerMaterialAlpha; //Player Material�� Alpha ��

    #region Stat
    public float maxHp
    {
        get
        {
            return Definition.defaultMaxHP + Definition.maxHPAdder * ShopInfo.maxHPLevel;
        }
    } //�ִ� ü��
    private float recoveryHP
    {
        get
        {
            return Definition.defaultRecoveryHP + Definition.recoveryHPAdder * ShopInfo.recoveryHPLevel;
        }
    } //ü�� ȸ����
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
    } //����
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
    } //�̵� �ӵ�
    public int moneyAmount
    {
        get
        {
            return Definition.defaultMoneyAmount + Definition.moneyAmountAdder * ShopInfo.moneyAmountLevel;
        }
    } //�� ȹ�淮
    public float moneyProbability
    {
        get
        {
            return Definition.defaultMoneyProbability + Definition.moneyProbabilityAdder * ShopInfo.moneyProbabilityLevel;
        }
    } //�� ȹ�� Ȯ��
    public float criticalDamage
    {
        get
        {
            return Definition.defaultCriticalDamage + Definition.criticalDamageAdder * ShopInfo.criticalDamageLevel;
        }
    } //ġ��Ÿ ���ط�
    public float criticalProbability
    {
        get
        {
            return Definition.defaultCriticalProbability + Definition.criticalProbabilityAdder * ShopInfo.criticalProbabilityLevel;
        }
    } //ġ��Ÿ Ȯ��
    #endregion

    #region Weapon Detail
    [Header("Fist Detail")]
    public PlayerAttackBoundManager[] fistAttackDamageMultiplierPlayerBoundManagers; //Fist Attack�� DamageMultiplier �� PlayerBoundManager ������Ʈ��
    public ParticleSystem[] fistAttackSpeedMultiplierParticleSystems; //Fist Attack�� SpeedMultiplier �� Particle System ������Ʈ��
    public PlayerAttackBoundManager[] fistUltDamageMultiplierPlayerBoundManagers; //Fist Ult�� DamageMultiplier �� PlayerBoundManager ������Ʈ��
    public ParticleSystem[] fistUltSpeedMultiplierParticleSystems; //Fist Ult�� SpeedMultiplier �� Particle System ������Ʈ��

    [Header("Sword Detail")]
    public PlayerAttackBoundManager[] swordAttackDamageMultiplierPlayerBoundManagers; //Sword Attack�� DamageMultiplier �� PlayerBoundManager ������Ʈ��
    public ParticleSystem[] swordAttackSpeedMultiplierParticleSystems; //Sword Attack�� SpeedMultiplier �� Particle System ������Ʈ��
    public PlayerAttackBoundManager[] swordUltDamageMultiplierPlayerBoundManagers; //Sword Ult�� DamageMultiplier �� PlayerBoundManager ������Ʈ��
    public ParticleSystem[] swordUltSpeedMultiplierParticleSystems; //Sword Ult�� SpeedMultiplier �� Particle System ������Ʈ��

    [Header("Gun Detail")]
    public PlayerAttackBoundManager[] gunAttackDamageMultiplierPlayerBoundManagers; //Gun Attack�� DamageMultiplier �� PlayerBoundManager ������Ʈ��
    public ParticleSystem[] gunAttackSpeedMultiplierParticleSystems; //Gun Attack�� SpeedMultiplier �� Particle System ������Ʈ��
    public PlayerAttackBoundManager[] gunUltDamageMultiplierPlayerBoundManagers; //Gun Ult�� DamageMultiplier �� PlayerBoundManager ������Ʈ��
    public ParticleSystem[] gunUltSpeedMultiplierParticleSystems; //Gun Ult�� SpeedMultiplier �� Particle System ������Ʈ��

    [Header("Sniper Detail")]
    public PlayerAttackBoundManager[] sniperAttackDamageMultiplierPlayerBoundManagers; //Sniper Attack�� DamageMultiplier �� PlayerBoundManager ������Ʈ��
    public ParticleSystem[] sniperAttackSpeedMultiplierParticleSystems; //Sniper Attack�� SpeedMultiplier �� Particle System ������Ʈ��
    public ParticleSystem[] sniperUltSpeedMultiplierParticleSystems; //Sniper Ult�� SpeedMultiplier �� Particle System ������Ʈ��

    [Header("Bazooka Detail")]
    public PlayerAttackBoundManager[] bazookaAttackDamageMultiplierPlayerBoundManagers; //Bazooka Attack�� DamageMultiplier �� PlayerBoundManager ������Ʈ��
    public PlayerAttackBoundManager[] bazookaUltDamageMultiplierPlayerBoundManagers; //Bazooka Ult�� DamageMultiplier �� PlayerBoundManager ������Ʈ��
    public ParticleSystem[] bazookaUltSpeedMultiplierParticleSystems; //Bazooka Ult�� SpeedMultiplier �� Particle System ������Ʈ��

    [Header("Wizard Detail")]
    public PlayerAttackBoundManager[] wizardAttackMultiplierPlayerBoundManagers; //Wizard Attack�� Multiplier �� PlayerBoundManager ������Ʈ��
    public PlayerAttackBoundManager[] wizardUltMultiplierPlayerBoundManagers; //Wizard Ult�� Multiplier �� PlayerBoundManager ������Ʈ��
    #endregion

    [Header("AnimationHash")]
    private readonly int fist_Idle_Hash = Animator.StringToHash("Fist_Idle"); //Fist_Idle �ؽ�
    private readonly int sword_Idle_Hash = Animator.StringToHash("Sword_Idle"); //Sword_Idle �ؽ�
    private readonly int gun_Idle_Hash = Animator.StringToHash("Gun_Idle"); //Gun_Idle �ؽ�
    private readonly int sniper_Idle_Hash = Animator.StringToHash("Sniper_Idle"); //Sniper_Idle �ؽ�
    private readonly int bazooka_Idle_Hash = Animator.StringToHash("Bazooka_Idle"); //Bazooka_Idle �ؽ�
    private readonly int wizard_Idle_Hash = Animator.StringToHash("Wizard_Idle"); //Wizard_Idle �ؽ�

    private readonly int fist_Run_Hash = Animator.StringToHash("Fist_Run"); //Fist_Run �ؽ�
    private readonly int sword_Run_Hash = Animator.StringToHash("Sword_Run"); //Sword_Run �ؽ�
    private readonly int gun_Run_Hash = Animator.StringToHash("Gun_Run"); //Gun_Run �ؽ�
    private readonly int sniper_Run_Hash = Animator.StringToHash("Sniper_Run"); //Sniper_Run �ؽ�
    private readonly int bazooka_Run_Hash = Animator.StringToHash("Bazooka_Run"); //Bazooka_Run �ؽ�
    private readonly int wizard_Run_Hash = Animator.StringToHash("Wizard_Run"); //Wizard_Run �ؽ�

    private readonly int run_Hash = Animator.StringToHash("Run"); //Run �ؽ�
    private readonly int attack_Hash = Animator.StringToHash("Attack"); //Attack �ؽ�
    private readonly int sniperUltBulletCount_Hash = Animator.StringToHash("SniperUltBulletCount"); //SniperUltBulletCount �ؽ�
    private readonly int run_Speed_Hash = Animator.StringToHash("Run_Speed"); //Run_Speed �ؽ�
    private readonly int sniperUltShot_Hash = Animator.StringToHash("SniperUltShot"); //SniperUltShot �ؽ�

    private void Awake()
    {
        player = this;
    }

    private void Start()
    {
        currentHP = maxHp; //���� ü�� ����
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

    /* ���� ������ �����ϴ� �Լ� */
    private void SetWeaponLevel()
    {
        ParticleSystem.MainModule main;

        /* Fist Attack */
        for (int i = 0; i < fistAttackDamageMultiplierPlayerBoundManagers.Length; ++i)
        {
            fistAttackDamageMultiplierPlayerBoundManagers[i].damageMultiplier = Definition.fistAttackDamageMultiplier[ShopInfo.fistAttackLevel]; //Damage Multiplier ����
        }
        playerAnimator.SetFloat("Fist_Attack_Speed", Definition.fistAttackSpeedMultiplier[ShopInfo.fistAttackLevel / 3]); //Animator �Ķ���� ����
        for (int i = 0; i < fistAttackSpeedMultiplierParticleSystems.Length; ++i) //ParticleSystem �ӵ� ����
        {
            main = fistAttackSpeedMultiplierParticleSystems[i].main;
            main.simulationSpeed = Definition.fistAttackSpeedMultiplier[ShopInfo.fistAttackLevel / 3];
        }

        /* Fist Ult */
        for (int i = 0; i < fistUltDamageMultiplierPlayerBoundManagers.Length; ++i)
        {
            fistUltDamageMultiplierPlayerBoundManagers[i].damageMultiplier = Definition.fistUltDamageMultiplier[ShopInfo.fistUltLevel]; //Damage Multiplier ����
        }
        playerAnimator.SetFloat("Fist_Ult_Speed", Definition.fistUltSpeedMultiplier[ShopInfo.fistUltLevel / 3]); //Animator �Ķ���� ����
        for (int i = 0; i < fistUltSpeedMultiplierParticleSystems.Length; ++i) //ParticleSystem �ӵ� ����
        {
            main = fistUltSpeedMultiplierParticleSystems[i].main;
            main.simulationSpeed = Definition.fistUltSpeedMultiplier[ShopInfo.fistUltLevel / 3];
        }

        /* Sword Attack */
        for (int i = 0; i < swordAttackDamageMultiplierPlayerBoundManagers.Length; ++i)
        {
            swordAttackDamageMultiplierPlayerBoundManagers[i].damageMultiplier = Definition.swordAttackDamageMultiplier[ShopInfo.swordAttackLevel]; //Damage Multiplier ����
        }
        playerAnimator.SetFloat("Sword_Attack_Speed", Definition.swordAttackSpeedMultiplier[ShopInfo.swordAttackLevel / 3]); //Animator �Ķ���� ����
        for (int i = 0; i < swordAttackSpeedMultiplierParticleSystems.Length; ++i) //ParticleSystem �ӵ� ����
        {
            main = swordAttackSpeedMultiplierParticleSystems[i].main;
            main.simulationSpeed = Definition.swordAttackSpeedMultiplier[ShopInfo.swordAttackLevel / 3];
        }

        /* Sword Ult */
        for (int i = 0; i < swordUltDamageMultiplierPlayerBoundManagers.Length; ++i)
        {
            swordUltDamageMultiplierPlayerBoundManagers[i].damageMultiplier = Definition.swordUltDamageMultiplier[ShopInfo.swordUltLevel]; //Damage Multiplier ����
        }
        playerAnimator.SetFloat("Sword_Ult_Speed", Definition.swordUltSpeedMultiplier[ShopInfo.swordUltLevel / 3]); //Animator �Ķ���� ����
        for (int i = 0; i < swordUltSpeedMultiplierParticleSystems.Length; ++i) //ParticleSystem �ӵ� ����
        {
            main = swordUltSpeedMultiplierParticleSystems[i].main;
            main.simulationSpeed = Definition.swordUltSpeedMultiplier[ShopInfo.swordUltLevel / 3];
        }

        /* Gun Attack */
        for (int i = 0; i < gunAttackDamageMultiplierPlayerBoundManagers.Length; ++i)
        {
            gunAttackDamageMultiplierPlayerBoundManagers[i].damageMultiplier = Definition.gunAttackDamageMultiplier[ShopInfo.gunAttackLevel]; //Damage Multiplier ����
        }
        playerAnimator.SetFloat("Gun_Attack_Speed", Definition.gunAttackSpeedMultiplier[ShopInfo.gunAttackLevel / 3]); //Animator �Ķ���� ����
        for (int i = 0; i < gunAttackSpeedMultiplierParticleSystems.Length; ++i) //ParticleSystem �ӵ� ����
        {
            main = gunAttackSpeedMultiplierParticleSystems[i].main;
            main.simulationSpeed = Definition.gunAttackSpeedMultiplier[ShopInfo.gunAttackLevel / 3];
        }

        /* Gun Ult */
        for (int i = 0; i < gunUltDamageMultiplierPlayerBoundManagers.Length; ++i)
        {
            gunUltDamageMultiplierPlayerBoundManagers[i].damageMultiplier = Definition.gunUltDamageMultiplier[ShopInfo.gunUltLevel]; //Damage Multiplier ����
        }
        playerAnimator.SetFloat("Gun_Ult_Speed", Definition.gunUltSpeedMultiplier[ShopInfo.gunUltLevel / 3]); //Animator �Ķ���� ����
        for (int i = 0; i < gunUltSpeedMultiplierParticleSystems.Length; ++i) //ParticleSystem �ӵ� ����
        {
            main = gunUltSpeedMultiplierParticleSystems[i].main;
            main.simulationSpeed = Definition.gunUltSpeedMultiplier[ShopInfo.gunUltLevel / 3];
        }

        /* Sniper Attack */
        for (int i = 0; i < sniperAttackDamageMultiplierPlayerBoundManagers.Length; ++i)
        {
            sniperAttackDamageMultiplierPlayerBoundManagers[i].damageMultiplier = Definition.sniperAttackDamageMultiplier[ShopInfo.sniperAttackLevel]; //Damage Multiplier ����
        }
        playerAnimator.SetFloat("Sniper_Attack_Speed", Definition.sniperAttackSpeedMultiplier[ShopInfo.sniperAttackLevel / 3]); //Animator �Ķ���� ����
        for (int i = 0; i < sniperAttackSpeedMultiplierParticleSystems.Length; ++i) //ParticleSystem �ӵ� ����
        {
            main = sniperAttackSpeedMultiplierParticleSystems[i].main;
            main.simulationSpeed = Definition.sniperAttackSpeedMultiplier[ShopInfo.sniperAttackLevel / 3];
        }

        /* Sniper Ult */
        playerAnimator.SetFloat("Sniper_Ult_Speed", Definition.sniperUltSpeedMultiplier[ShopInfo.sniperUltLevel / 3]); //Animator �Ķ���� ����
        for (int i = 0; i < sniperUltSpeedMultiplierParticleSystems.Length; ++i) //ParticleSystem �ӵ� ����
        {
            main = sniperUltSpeedMultiplierParticleSystems[i].main;
            main.simulationSpeed = Definition.sniperUltSpeedMultiplier[ShopInfo.sniperUltLevel / 3];
        }

        /* Bazooka Attack */
        for (int i = 0; i < bazookaAttackDamageMultiplierPlayerBoundManagers.Length; ++i)
        {
            bazookaAttackDamageMultiplierPlayerBoundManagers[i].damageMultiplier = Definition.bazookaAttackDamageMultiplier[ShopInfo.bazookaAttackLevel]; //Damage Multiplier ����
        }
        playerAnimator.SetFloat("Bazooka_Attack_Speed", Definition.bazookaAttackSpeedMultiplier[ShopInfo.bazookaAttackLevel / 3]); //Animator �Ķ���� ����

        /* Bazooka Ult */
        for (int i = 0; i < bazookaUltDamageMultiplierPlayerBoundManagers.Length; ++i)
        {
            bazookaUltDamageMultiplierPlayerBoundManagers[i].damageMultiplier = Definition.bazookaUltDamageMultiplier[ShopInfo.bazookaUltLevel]; //Damage Multiplier ����
        }
        playerAnimator.SetFloat("Bazooka_Ult_Speed", Definition.bazookaUltSpeedMultiplier[ShopInfo.bazookaUltLevel / 3]); //Animator �Ķ���� ����
        for (int i = 0; i < bazookaUltSpeedMultiplierParticleSystems.Length; ++i) //ParticleSystem �ӵ� ����
        {
            main = bazookaUltSpeedMultiplierParticleSystems[i].main;
            main.simulationSpeed = Definition.bazookaUltSpeedMultiplier[ShopInfo.bazookaUltLevel / 3];
        }

        /* Wizard Attack */
        for (int i = 0; i < wizardAttackMultiplierPlayerBoundManagers.Length; ++i)
        {
            wizardAttackMultiplierPlayerBoundManagers[i].damageMultiplier = Definition.wizardAttackDamageMultiplier[ShopInfo.wizardAttackLevel]; //Damage Multiplier ����
            wizardAttackMultiplierPlayerBoundManagers[i].slowMultiplier = Definition.wizardAttackSpeedMultiplier[ShopInfo.wizardAttackLevel / 3]; //Speed Multiplier ����
        }

        /* Wizard Ult */
        for (int i = 0; i < wizardUltMultiplierPlayerBoundManagers.Length; ++i)
        {
            wizardUltMultiplierPlayerBoundManagers[i].damageMultiplier = Definition.wizardUltDamageMultiplier[ShopInfo.wizardUltLevel]; //Damage Multiplier ����
            wizardUltMultiplierPlayerBoundManagers[i].slowMultiplier = Definition.wizardUltSpeedMultiplier[ShopInfo.wizardUltLevel / 3]; //Speed Multiplier ����
        }
    }

    /* Animator�� �����ϴ� �Լ� */
    private void ControlAnimator()
    {
        /* Player */
        playerAnimator.SetBool(run_Hash, MoveJoystick.moveJoystick.axis.magnitude != 0f); //Run �ִϸ��̼� State
        playerAnimator.SetBool(attack_Hash, AttackJoystick.attackJoystick.axis != 0f && enableAttack); //Attack �ִϸ��̼� State
        playerAnimator.SetInteger(sniperUltBulletCount_Hash, sniperUltBulletCount); //SniperUltBulletCount ���� ����
        playerAnimator.SetFloat(run_Speed_Hash, moveSpeed / Definition.defaultMoveSpeed); //Run_Speed �Ķ���� ����

        /* Down */
        downAnimator.SetBool(run_Hash, MoveJoystick.moveJoystick.axis.magnitude != 0f); //Run �ִϸ��̼� State
        downAnimator.SetFloat(run_Speed_Hash, moveSpeed / Definition.defaultMoveSpeed); //Run_Speed �Ķ���� ����
    }

    /* HP�� ȸ���ϴ� �Լ� */
    private void RecoverHP()
    {
        if (currentHP <= 0f || sniperUltBulletCount > 0) return; //Ư�� ��Ȳ�̸� ����

        float resultHP = currentHP + recoveryHP * Time.deltaTime; //��� HP
        currentHP = resultHP > maxHp ? maxHp : resultHP; //HP ȸ��
    }

    #region Damaged
    /* ���ظ� �Դ� �Լ� */
    public void GetDamage(float damage)
    {
        if (enableImmortal) return; //���� �����̸� ����

        damage *= (1f - defense); //���� ����
        currentHP = currentHP - damage < 0f ? 0f : currentHP - damage; //HP ����
        playerMaterialAlpha = 1f;

        if (currentHP <= 0f) //ü���� ��� �����Ǹ�
        {
            Utility.utility.LoadStartCoroutine(FieldManager.fieldManager.FinishStage(false)); //�ܰ� ����
            Destroy(gameObject); //Player �ı�
        }
    }

    /* Damaged ������ ������� �ϴ� �Լ� */
    private void FadeDamagedColor()
    {
        playerMaterialAlpha = Mathf.Clamp(playerMaterialAlpha - Time.deltaTime / damagedColorDisplayTime, 0f, 1f); //���� �� ����
        playerMaterial.SetFloat("_Alpha", playerMaterialAlpha); //���� �� ����
    }
    #endregion

    #region Transform
    /* Player�� �����̴� �Լ� */
    private void MovePlayer()
    {
        if (enableMove) //�̵��� �� ������
        {
            float axisYLerp = (playerTransform.position.y - Definition.moveYRange.x) / (Definition.moveYRange.y - Definition.moveYRange.x); //Y�� ���� 0 ~ 1 ������ ������ ����
            float resultMoveSpeed = moveSpeed * Mathf.Lerp(Definition.moveSpeedRange.y, Definition.moveSpeedRange.x, axisYLerp); //Y�� ���� ������ �̵� �ӵ� ����

            Vector2 resultPosition = (Vector2)playerTransform.position + resultMoveSpeed * Time.deltaTime * MoveJoystick.moveJoystick.axis; //��� ��ġ ����
            resultPosition = new Vector2(Mathf.Clamp(resultPosition.x, Definition.moveXRange.x, Definition.moveXRange.y), Mathf.Clamp(resultPosition.y, Definition.moveYRange.x, Definition.moveYRange.y)); //���� ����
            playerRigidbody2D.position = resultPosition; //�̵� ����
        }
    }

    /* Player�� ȸ���ϴ� �Լ� */
    private void RotatePlayer()
    {
        if (enableMove) //������ �� ������
        {
            playerTransform.rotation = Quaternion.Euler(playerTransform.eulerAngles.x, MoveJoystick.moveJoystick.horizontalMoveDirection == -1 ? 180f : MoveJoystick.moveJoystick.horizontalMoveDirection == 1 ? 0f : playerTransform.eulerAngles.y, playerTransform.eulerAngles.z);

            if (AttackJoystick.attackJoystick.axis == 0f) // AttackJoystick�� �� ��ȭ�� ������
                upTransform.localRotation = Quaternion.Euler(Vector3.zero);
            else //AttackJoystick�� �� ��ȭ�� ������
                upTransform.rotation = Quaternion.Euler(upTransform.eulerAngles.x, AttackJoystick.attackJoystick.axis == -1 ? 180f : AttackJoystick.attackJoystick.axis == 1 ? 0f : upTransform.eulerAngles.y, upTransform.eulerAngles.z);
        }
    }

    /* ���̾� ������ �����ϴ� �Լ� */
    private void SetLayerOrder()
    {
        float axisYLerp = (playerTransform.position.y - Definition.moveYRange.x) / (Definition.moveYRange.y - Definition.moveYRange.x); //Y�� ���� 0 ~ 1 ������ ������ ����

        int resultLayerOrder = (int)Mathf.Lerp(Definition.layerOrderRange.y, Definition.layerOrderRange.x, axisYLerp); //Y�� ���� ������ ���̾� ���� ��� ����
        upSpriteRenderer.sortingOrder = resultLayerOrder + 2; //Up ���̾� ���� ����
        downSpriteRenderer.sortingOrder = resultLayerOrder + 1; //Down ���̾� ���� ����
        shadowSpriteRenderer.sortingOrder = resultLayerOrder; //Shadow ���̾� ���� ����

        for (int i = 0; i < layerOrderSpriteRenderers.Length; ++i) layerOrderSpriteRenderers[i].sortingOrder = resultLayerOrder + 3; //Layer ������ Sprite Renderer ������Ʈ���� ���̾� ���� ����
        for (int i = 0; i < layerOrderParticleSystemRenderers.Length; ++i) layerOrderParticleSystemRenderers[i].sortingOrder = resultLayerOrder + 3; //Layer ������ Particle System Renderer ������Ʈ���� ���̾� ���� ����
    }

    /* Player�� ũ�⸦ �����ϴ� �Լ� */
    private void SetScale()
    {
        float axisYLerp = (playerTransform.position.y - Definition.moveYRange.x) / (Definition.moveYRange.y - Definition.moveYRange.x); //Y�� ���� 0 ~ 1 ������ ������ ����

        float resultScale = Mathf.Lerp(Definition.scaleRange.y, Definition.scaleRange.x, axisYLerp); //Y�� ���� ������ ũ�� ��� ����
        playerTransform.localScale = new Vector3(resultScale, resultScale, resultScale); //ũ�� ����
    }
    #endregion

    #region Sniper
    /* SniperUltShot Ʈ���Ÿ� �߻���Ű�� �Լ� */
    private void SetSniperUltShotTrigger()
    {
        if (sniperUltBulletCount > 0) //�Ѿ��� ����������
        {
            if (Input.GetMouseButtonDown(0)) //Ŭ���� �߻��ϸ�
            {
                playerAnimator.SetTrigger(sniperUltShot_Hash); //�ִϸ��̼� Ʈ���� �߻�
            }
        }
    }

    public GameObject sniperParticleUlt3; //Sniper Ult�� ��ƼŬ 3 ������Ʈ
    /* Sniper�� Ult �Ѿ��� �߻��ϴ� �Լ� */
    public void ShotSniperUltBullet()
    {
        sniperUltBulletCount--; //�Ѿ� ���� ����

        Vector2 mousePosition = MainCamera.mainCamera.mainCameraCamera.ScreenToWorldPoint(Input.mousePosition); //���콺 ��ġ ����

        playerTransform.rotation = Quaternion.Euler(playerTransform.eulerAngles.x, mousePosition.x - playerTransform.position.x > 0f ? 0f : -180f, playerTransform.eulerAngles.z); //���콺 ��ġ�� ���� Player ȸ��

        Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePosition, 0.5f, 1 << LayerMask.NameToLayer("Enemy")); //Ŭ�� �������κ��� ��� �� Ž��
        for (int i = 0; i < colliders.Length; ++i)
        {
            Enemy enemy = colliders[i].GetComponent<Enemy>();
            enemy.GetDamage(enemy.maxHP * 0.2f * Definition.sniperUltDamageMultiplier[ShopInfo.sniperUltLevel], 0f); //�ִ� ü�¿� ����� ����
        }

        GameObject sniperUltEffect = Instantiate(sniperParticleUlt3); //Sniper Ult ����Ʈ ����
        sniperUltEffect.transform.position = mousePosition; //���콺 ��ġ�� �̵�
        sniperUltEffect.transform.localScale = Vector3.one; //ũ�� �����ϰ� ����
        sniperUltEffect.SetActive(true); //Ȱ��ȭ
    }
    #endregion
}
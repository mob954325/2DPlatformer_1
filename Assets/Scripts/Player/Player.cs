using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어 인풋 관련 행동을 다루는 클래스
/// </summary>
public class Player : MonoBehaviour
{
    // Action
    /// <summary>
    /// 플레이어가 공격할 때 실행하는 델리게이트
    /// </summary>
    Action OnAttack;
    /// <summary>
    /// 플레이어가 방어할 때 실행하는 델리게이트
    /// </summary>
    Action OnDefence;

    /// <summary>
    /// 플레이어 인터렉션 델리게이트
    /// </summary>
    public Action OnInteractionAction;

    // components
    PlayerInputActions actions;
    Animator animator;
    Rigidbody rigid;
    EnemyBase enemy;
    SoundControl soundControl;
    
    WeaponControl weapon;
    ShieldControl shield;

    // 플레이어 인풋값을 받는 변수
    [Header("Input Value")]
    public Vector3 playerInput;
    public Vector2 mouseInput;

    // player Stats
    [Header("Input Stats")]
    public float moveSpeed = 5.0f; // 플레이어의 현재 스피드 변수
    float baseSpeed; // 플레이어 스피드 저장 변수

    [Range(0f,1f)]
    public float rotationPower = 5.0f; // 플레이어 회전력
    public float rotSpeed = 15f; // 플레이어 회전 속도
    public float jumpPower = 5.0f; // 점프력

    [Space(12f)]
    [Range(1.5f,3f)]
    public float attackDelayTime = 3.0f; // 공격 딜레이 시간
    [SerializeField] float attackDelayTimer; // 공격 딜레이 타이머
    /// <summary>
    /// attackDelayTimer 프로퍼티
    /// </summary>
    float AttackDelayTimer
    {
        get => attackDelayTimer;
        set
        {
            attackDelayTimer = value;

            if(attackDelayTimer < 0f)
            {
                attackDelayTimer = 0f;
            }
        }
    }

    public float defenceDelayTime = 3.0f; // 방어 딜레이 시간
    [SerializeField] float defenceDelayTimer; // 방어 딜레이 타이머
    /// <summary>
    /// defenceDelayTimer 프로퍼티
    /// </summary>
    float DefenceDelayTimer
    {
        get => defenceDelayTimer;
        set
        {
            defenceDelayTimer = value;

            if (defenceDelayTimer < 0f)
            {
                defenceDelayTimer = 0f;
            }
        }
    }
    

    [Space(12f)]
    // Hp
    public int maxhp = 5;
    int hp;
    public int HP // 플레이어 체력 프로퍼티
    {
        get => hp;
        set
        {
            hp = value;
            //Debug.Log($"플레이어의 체력이 [{hp}]만큼 남았습니다");

            if (hp <= 0)
            {
                hp = 0;
                isDie = true;
                Die();
            }
        }
    }

    [Header("Objects")]
    // player's Transform objects
    public Transform cameraFollowTransform;
    public Transform playerModel;

    // player movement
    [Header("Movement")]
    Vector3 moveDirection; // 플레이어 움직임 방향
    float inputVertical = 0f; // 플레이어의 상 하 인풋값
    float inputHorizontal = 0f; // 플레이어의 좌 우 인풋값

    // player animator
    readonly int inputVerticalToHash = Animator.StringToHash("Vertical"); // input.z
    readonly int inputHorizontalToHash = Animator.StringToHash("Horizontal"); // input.x
    readonly int jumpToHash = Animator.StringToHash("Jump");
    readonly int attackToHash = Animator.StringToHash("Attack");
    readonly int damagedToHash = Animator.StringToHash("Damaged");
    readonly int defenceToHash = Animator.StringToHash("isDefence"); // 방패 밀치기를 하는 animator parameter ( true : 방패 들기, false : 방패로 치기)
    readonly int ActiveDefenceToHash = Animator.StringToHash("ActiveDefence");
    readonly int DieToHash = Animator.StringToHash("Die");

    // player flag
    bool isJump = false;
    bool isAttack = false;
    [SerializeField]bool isDamaged = false;
    public bool isDefence = false; // 플레이어가 방어를 하는지 확인하는 bool
    bool isLockOn = false; // 플레이어가 락온을 활성화 했는지 확인하는 bool
    public bool isDefenceAttack = false; // 플레이어가 방패밀치기를 했는지 확인하는 bool
    bool isDie = false;
    //float checkEnemyAngle = 0f;

    void Awake()
    {
        // 컴포넌트
        actions = new PlayerInputActions();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        enemy = FindAnyObjectByType<EnemyBase>();
        weapon = GetComponentInChildren<WeaponControl>();
        shield = GetComponentInChildren<ShieldControl>();
        cameraFollowTransform = FindAnyObjectByType<FollowCamera>().transform;
        soundControl = GetComponentInChildren<SoundControl>();
        playerModel = transform.GetChild(0);

        // 변수 초기화
        HP = maxhp;
        baseSpeed = moveSpeed;

        // 델리게이트
        OnAttack += weapon.ChangeColliderEnableState; 
        OnDefence += shield.ChangeColliderEnableState;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 커서 고정
        Cursor.visible = false; // 커서 가리기
        rigid.freezeRotation = true;
    }

    void OnEnable()
    {
        actions.Player.Enable();
        actions.Player.Move.performed += OnMoveInput;
        actions.Player.Move.canceled += OnMoveInput;
        actions.Player.Jump.performed += OnJumpInput;
        actions.Player.Jump.canceled += OnJumpInput;
        actions.Player.Look.performed += OnLookInput;
        actions.Player.Look.canceled += OnLookInput;
        actions.Player.Attack.performed += OnAttackInput;
        actions.Player.Attack.canceled += OnAttackInput;
        actions.Player.Defence.performed += OnDefenceInput;
        actions.Player.Defence.canceled += OnDefenceInput;
        actions.Player.LockOn.performed += OnLockCameraInput;
        actions.Player.LockOn.canceled += OnLockCameraInput;
        actions.Player.Interaction.performed += OnInteraction;
        actions.Player.Interaction.canceled += OnInteraction;
    }

    private void OnInteraction(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnInteractionAction?.Invoke();
        }
    }

    void OnDisable()
    {
        actions.Player.Interaction.canceled -= OnInteraction;
        actions.Player.Interaction.performed -= OnInteraction;
        actions.Player.LockOn.canceled -= OnLockCameraInput;
        actions.Player.LockOn.performed -= OnLockCameraInput;
        actions.Player.Defence.canceled -= OnDefenceInput;
        actions.Player.Defence.performed -= OnDefenceInput;
        actions.Player.Attack.canceled -= OnAttackInput;
        actions.Player.Attack.performed -= OnAttackInput;
        actions.Player.Look.canceled -= OnLookInput;
        actions.Player.Look.performed -= OnLookInput;
        actions.Player.Jump.canceled -= OnJumpInput;
        actions.Player.Jump.performed -= OnJumpInput;
        actions.Player.Move.canceled -= OnMoveInput;
        actions.Player.Move.performed -= OnMoveInput;
        actions.Player.Disable();
    }

    void Update()
    {
        // Timer
        DefenceDelayTimer -= Time.deltaTime;
        AttackDelayTimer -= Time.deltaTime;

    }

    void FixedUpdate()
    {
        playerMove();
        GetPlayerMoveInput();
        PlayerRotate();
        RotateCamera();
        PlayAnimMove();

        // 카메라 락온
        if(isLockOn)
            cameraFollowTransform.LookAt(enemy.transform);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            rigid.position = transform.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 적 공격 감지
        if (other.CompareTag("EnemyAttack") && !isDamaged && !isDie)
        {
            if (!isDefence) // 피격당할 시, 방패를 안들었을 때, 방패 밀치기를 실행하지 않았을 때
            {
                animator.SetTrigger(damagedToHash);
                HP--;
            }
            else if (other.CompareTag("EnemyAttack") && isDefence && !isDie)
            {
                isDamaged = true;
            }
            rigid.AddForce(enemy.transform.forward * 70f, ForceMode.Impulse); // 적 방향으로 넉백
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnemyAttack") && isDefence && !isDie)
        {
            rigid.AddForce(enemy.transform.forward * 45f, ForceMode.Impulse); // 적 방향으로 넉백
        }
    }

    private void OnLookInput(InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        playerInput = context.ReadValue<Vector3>();
    }

    void GetPlayerMoveInput()
    {
        // CALL AFTER playerMove()
        inputVertical = playerInput.z;
        inputHorizontal = playerInput.x;
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        if(!isJump)
        {
            animator.SetTrigger(jumpToHash);
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJump = true;
        }
    }

    void RotateCamera()
    {
        if(isLockOn)
            return;

        #region Vertical Rotation

        // Rotate the follow target transfrom based on input

        // leftright
        cameraFollowTransform.transform.rotation *= Quaternion.AngleAxis(mouseInput.x * rotationPower, Vector3.up);

        // updown
        cameraFollowTransform.transform.rotation *= Quaternion.AngleAxis(mouseInput.y * -rotationPower, Vector3.right);

        Vector3 angles = cameraFollowTransform.transform.localEulerAngles;
        angles.z = 0;

        float angle = cameraFollowTransform.transform.localEulerAngles.x;

        // Clamp the up/down rotation
        if(angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if(angle < 180 && angle > 40)
        {
            angles.x = 40;
        }

        cameraFollowTransform.transform.localEulerAngles = angles;

        #endregion

        // Reset the y rotation of the look transform
        cameraFollowTransform.transform.localEulerAngles = new Vector3(angles.x, angles.y, 0);

    }

    /// <summary>
    /// player model rotate
    /// </summary>
    void PlayerRotate()
    {
        if (isDefence || isAttack)
            return;

        Vector3 rotDirection = Vector3.zero;
        rotDirection.x = inputHorizontal;
        rotDirection.z = inputVertical;
        rotDirection.Normalize(); // 회전 방향 백터

        // 입력키 기준 모델 회전값 + 카메라 회전값 = 실제 플레이어 모델이 회전할 y값
        if(!isLockOn)
        {
            if(rotDirection.magnitude > 0.01f)
            {
                float lookAngle = Mathf.Atan2(rotDirection.x, rotDirection.z) * Mathf.Rad2Deg; // 회전할 방향
                float lerpLookAngle = Mathf.LerpAngle(playerModel.localRotation.eulerAngles.y, 
                                                      lookAngle + cameraFollowTransform.rotation.eulerAngles.y, 
                                                      rotSpeed * Time.fixedDeltaTime);

                playerModel.localRotation = Quaternion.Euler(0, lerpLookAngle, 0); // rotate Player model
            }

        }
        else if(isLockOn)
        {
            // 카메라 방향 기준 모델 회전
            float angle = Mathf.LerpAngle(playerModel.localRotation.eulerAngles.y, cameraFollowTransform.rotation.eulerAngles.y, rotSpeed * Time.fixedDeltaTime);
            playerModel.localRotation = Quaternion.Euler(0, angle, 0); // rotate Player model
        }
    }

    void playerMove()
    {
        if (isDefence)
            return;

        // calculate movement direction
        moveDirection = cameraFollowTransform.forward * inputVertical + cameraFollowTransform.right * inputHorizontal; // Player Move Direction
        moveDirection.y = 0f;

        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * moveDirection.normalized * moveSpeed); // player Move position
    }

    void PlayAnimMove()
    {
        // check input value
        animator.SetFloat(inputVerticalToHash, inputVertical);
        animator.SetFloat(inputHorizontalToHash, inputHorizontal);
    }

    private void OnAttackInput(InputAction.CallbackContext context)
    {
        if(context.performed && !isAttack
        && !isDefence && !isDefenceAttack)// 방어 중이 아닐 때, 방패 밀치기를 하지 않을 때
        {
            if (AttackDelayTimer > 0f)
            {
                Debug.Log($"공격 쿨타임이 [{AttackDelayTimer}] 남았습니다 !!!!");
                return;
            }

            moveSpeed = 0f; // 움직일 수 없다.
            animator.SetTrigger(attackToHash); // 애니메이션 시작
            StartCoroutine(AttackDelay()); 
        }
    }

    IEnumerator AttackDelay()
    {
        AttackDelayTimer = attackDelayTime; // 공격 쿨타임
        isAttack = true;
        OnAttack?.Invoke();

        float time = GetAnimClipLength("Player_Attack"); // 애니메이션 시간 받기
        yield return new WaitForSeconds(time);

        OnAttack?.Invoke();
        isAttack = false;

        moveSpeed = baseSpeed;
    }

    // Defence
    private void OnDefenceInput(InputAction.CallbackContext context)
    {

        if (context.performed && !isDefence
            && !isAttack) // 공격하는 중이 아닐때
        {
            if (DefenceDelayTimer > 0f) // 쿨타임
                return;

            DefenceDelayTimer = defenceDelayTime; // 쿨타임

            // Set animator paramaters
            animator.SetTrigger(ActiveDefenceToHash); // 방패 들기 트리거
            animator.SetBool(defenceToHash, true); // 방패 밀치기 준비

            OnDefence?.Invoke(); // 방패 콜라이더 활성화

            isDefence = true; // 방어활성화

        }
        if (context.canceled && isDefence)
        {
            animator.SetBool(defenceToHash, false); // 방패 밀치기 준비
            isDamaged = true; // 무적 활성화 
            StartCoroutine(AfterDefence());
        }
    }

    /// <summary>
    /// 방어 무적 판정 시간 코루틴(Defencing attack + 0.5f)
    /// </summary>
    /// <returns></returns>
    IEnumerator AfterDefence()
    {
        animator.SetBool(defenceToHash, false);

        isDefence = false; // 방패 밀치기 애니메이션 시작
        float defenceAnimTime = GetAnimClipLength("Player_Defence_Attack"); // 0.9s

        isDefenceAttack = true;
        yield return new WaitForSeconds(defenceAnimTime);

        OnDefence?.Invoke(); // 방패 콜라이더 비활성화
        isDefenceAttack = false;
        isDamaged = false; // 무적 판정 비활성화
    }

    private void OnLockCameraInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isLockOn = !isLockOn;
        }
    }

    IEnumerator HitDelay()
    {
        isDamaged = true;
        yield return new WaitForSeconds(2f);
        isDamaged = false;
    }

    /// <summary>
    /// 플레이어 사망 함수
    /// </summary>
    void Die()
    {
        DisablePlayerAction();
        animator.SetTrigger(DieToHash);
        GameUIManager.Instance.ShowResult(true);
        GameManager.Instance.BattleEnd();
    }

    public void DisablePlayerAction()
    {
        actions.Player.Disable();
    }

    /// <summary>
    /// 특정 애니메이션 시간을 찾는 함수
    /// </summary>
    /// <param name="clipName">찾는 애니메이션 이름</param>
    /// <returns>clipName 애니메이션 시간</returns>
    float GetAnimClipLength(string clipName)
    {
        float time = 0;
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;

        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == clipName)
            {
                time = ac.animationClips[i].length;
            }
        }
        return time;
    }

    /// <summary>
    /// 소리를 실행하는 함수
    /// </summary>
    public void PlayAttackSound()
    {
        int rand = UnityEngine.Random.Range(0, soundControl.audioSources.Length);
        soundControl.audioSources[rand].Play();
    }
}

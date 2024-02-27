using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSEnemy : MonoBehaviour
{
    // Delegate
    /// <summary>
    /// 공격시 실행하는 델리게이트
    /// </summary>
    Action onAttack;

    // Components
    Player player;
    WeaponControl weapon;
    Rigidbody rigid;
    Animator animator;

    // Values
    Vector3 direction = Vector3.zero;
    float lookAngle;
    float attackAnimTime;

    // Enemy stats
    float baseSpeed;
    [Header ("Enemy Stats")]
    public float speed = 3.0f;
    public float rotateSpeed = 5.0f;
    [Space (10f)]
    public float attackRange = 2.0f;
    public float attackDelay = 2.5f;
    public float ToughnessDelay = 2f;  // 강인성 감소 딜레이 시간
    [SerializeField] float StepBackTime = 0f;

    int hp;
    public int maxHp = 10;
    public int HP
    {
        get => hp;
        set
        {
            hp = value;
            //Debug.Log($"적의 체력이 [{hp}]만큼 남았습니다");

            if (hp <= 0)
            {
                hp = 0;
                Die();
            }
        }
    }

    int toughness = 0; // 강인성 (0이되면 기절)

    /// <summary>
    /// 강인성(toughness)를 참조하는 파라미터 (0이되면 기절 애니메이션을 실행한다)
    /// </summary>
    public int Toughness
    {
        get => toughness;
        set
        {
            toughness = value;
            //Debug.Log($"남은 강인성 : [{toughness}]");
            animator.SetBool(isFaintToHash, isFaint);

            if(toughness <= 0)
            {
                toughness = 0;
                speed = 0;

                // 애니메이션 실행
                animator.SetTrigger(faintToHash);

                Invoke("AfterFaint", 3f);
                //StartCoroutine(AfterFaint());
            }
        }
    }
    public int maxToughness = 100;

    // Hashes
    readonly int SpeedToHash = Animator.StringToHash("Speed");
    readonly int AttackToHash = Animator.StringToHash("Attack");
    readonly int DamagedToHash = Animator.StringToHash("Damaged");
    readonly int DieToHash = Animator.StringToHash("Die");
    readonly int faintToHash = Animator.StringToHash("Faint"); // 기절 trigger 
    readonly int isFaintToHash = Animator.StringToHash("isFaint"); // 기절이 끝나기 전까지 대기하게 하는 animator bool값

    // Flags
    [Header("Enemy Flag")]
    bool isAttack = false;
    /// <summary>
    /// 공격 했는지 확인하는 파라미터
    /// </summary>
    bool IsAttack
    {
        get => isAttack;
        set
        {
            isAttack = value;

            if (!isAttack)
            {
                speed = baseSpeed;
            }
        }
    }
    bool isDamaged = false; // 피격 여부
    bool isDead => HP <= 0; // 사망 여부
    bool isFaint => Toughness <= 0; // 기절 여부 
    bool isAttackBlocked => weapon.CheckIsDefenced(); // 공격이 막혔는지 확인하는 변수
    bool canToughnessChange = true; // 강인성이 감소 될 수 있는지 확인하는 bool 값 (true : 강인성 감소 가능 , false : 강인성 감소 불가능)

    void Awake()
    {
        player = FindAnyObjectByType<Player>();
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        weapon = GetComponentInChildren<WeaponControl>();

        // setting values
        baseSpeed = speed; // speed 값 저장
        HP = maxHp;
        Toughness = maxToughness;

        // delegate
        onAttack += weapon.ChangeColliderEnableState; // 콜라이더 반전
    }

    void FixedUpdate()
    {
        direction = player.transform.position - transform.position; // 플레이어 방향 백터
        animator.SetFloat(SpeedToHash, speed);

        // 적 행동 함수들
        if (!isDead && !isFaint)
        {
            MoveToPlayer();
            RotateToPlayer();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isDead)
            return;

        if(other.CompareTag("PlayerAttack") && !isDamaged)
        {
            if (isFaint) return;

            HP--;
            isDamaged = true;
            animator.SetTrigger(DamagedToHash);
            StartCoroutine(HitDelay());

        }
    }

    /// <summary>
    /// 플레이어한테 이동하는 함수 ( AttackRange 내에 도달하면 공격 )
    /// </summary>
    void MoveToPlayer()
    {
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * direction.normalized * speed);

        if(direction.magnitude <= attackRange) // 플레이어 근처에 도달
        {
            if (!IsAttack)
            {
                StopCoroutine(Attack());
                StartCoroutine(Attack()); // 공격 코루틴 시작
            }
        }
    }

    /// <summary>
    /// 플레이어를 향해 회전하는 함수
    /// </summary>
    void RotateToPlayer()
    {
        Vector3 rotDirection = Vector3.zero;
        rotDirection.x = direction.x;
        rotDirection.z = direction.z;
        rotDirection.Normalize();


        if (rotDirection.magnitude > 0.01f)
        {
            lookAngle = Mathf.Atan2(rotDirection.x, rotDirection.z) * Mathf.Rad2Deg; // 회전할 방향
        }
        float angle = Mathf.LerpAngle(transform.localRotation.eulerAngles.y, lookAngle, rotateSpeed * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(0, angle, 0); // rotate Player model
    }

    /// <summary>
    /// 플레이어를 공격하는 코루틴 (AttackDelay시간마다 계속 함수를 불러옴)
    /// </summary>
    /// <returns></returns>
    IEnumerator Attack()
    {
        speed = 0f;
        // 공격 애니메이션 실행
        animator.SetTrigger(AttackToHash); // 공격 애니메이션 플레이

        IsAttack = true;
        onAttack?.Invoke(); // 무기 콜라이더 활성화

        attackAnimTime = GetAnimClipLength("1HSEnemy_Attack"); // 공격 애니메이션 시작

        yield return new WaitForSeconds(attackAnimTime - 0.1f);

        onAttack?.Invoke(); // 무기 콜라이더 비활성화  
       
        // 뒤로 물러나기
        StepBackTime = UnityEngine.Random.Range(1, attackDelay - attackAnimTime); // 뒤로 물러가는 랜덤 시간
        yield return new WaitForSeconds(1f);

        speed = baseSpeed * -1 / 2f;
        yield return new WaitForSeconds(StepBackTime);

        // 정지
        speed = 0f;
        yield return new WaitForSeconds(attackDelay - attackAnimTime - StepBackTime);

        // 공격 딜레이 끝
        IsAttack = false;
    }

    /// <summary>
    /// 피격 무적 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(0.7f);
        isDamaged = false;
    }

    /// <summary>
    /// 죽으면 실행되는 함수
    /// </summary>
    void Die()
    {
        animator.SetTrigger(DieToHash); // 애니메이션 실행
        GameManager.Instance.BattleEnd();
        GameUIManager.Instance.ShowResult();
    }

    /// <summary>
    /// 플레이어한테 방패 밀치기를 당하면 실항하는 함수
    /// </summary>
    public void CheckDefenced()
    {
        if (!isAttackBlocked)
            return;

        //// 무기 콜라이더 비활성화
        //onAttack?.Invoke();
        //onAttack?.Invoke();

        animator.SetTrigger(DamagedToHash); // 피격 모션 실행

        if (canToughnessChange)
        {

            Toughness -= 20; // 강인성 감소
            StartCoroutine(BlockedDelay());
        }
    }

    IEnumerator BlockedDelay()
    {
        canToughnessChange = false;

        yield return new WaitForSeconds(ToughnessDelay);

        Debug.Log("플레이어가 다시 방어를 할 수 있습니다. !!!");
        canToughnessChange = true;
    }

    /// <summary>
    /// 기절 후 수행할 함수
    /// </summary>
    void AfterFaint()
    {
        animator.SetBool(isFaintToHash, isFaint);
        Toughness = maxToughness;
        speed = baseSpeed;
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
}

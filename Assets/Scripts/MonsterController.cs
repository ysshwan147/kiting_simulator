using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public GameObject playerObject;
    public float detectionRange = 10f; // 몬스터의 감지 범위
    public float attackRange = 2f; // 몬스터의 공격 사거리
    public float attackDelay = 2f; // 공격 딜레이 시간
    public float attackAnimationSpeed = 0.3f;
    public float movingAnimationSpeed = 1.0f;

    private Transform player;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private float nextAttackTime;

    private float delayTime;

    private MonsterAttack monsterAttack;

    private enum MonsterState
    {
        Idle,
        Moving,
        AttackReady,
        Attacking
    }

    private MonsterState currentState;

    private void Start()
    {
        player = playerObject.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();

        currentState = MonsterState.Idle;

        animator = GetComponentInChildren<Animator>();
        if (animator == null) {
            Debug.LogError("ERROR: There is no Animator component for character.");
            Debug.Break();
        } 

        animator.SetFloat("Animation Speed", movingAnimationSpeed);
        animator.SetInteger("Action", 1);
        animator.SetInteger("Trigger Number", 2);

        monsterAttack = GetComponent<MonsterAttack>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        float deltaTime = Time.deltaTime;

        switch (currentState)
        {
            case MonsterState.Idle:
                // 플레이어가 감지 범위 안에 들어오면 이동 상태로 전환
                if (distanceToPlayer <= detectionRange)
                {
                    currentState = MonsterState.Moving;
                    navMeshAgent.SetDestination(player.position);
                    animator.SetFloat("Animation Speed", movingAnimationSpeed);
                    movingAnimation(true, 10);
                }
                break;

            case MonsterState.Moving:
                // 플레이어가 공격 사거리 안에 들어오면 공격 준비 상태로 전환
                if (distanceToPlayer <= attackRange)
                {
                    currentState = MonsterState.AttackReady;
                    navMeshAgent.isStopped = true; // 이동 중지
                    movingAnimation(false, 0);

                    animator.SetFloat("Animation Speed", attackAnimationSpeed);
                    animator.SetTrigger("Trigger");
                    
                }
                else if (distanceToPlayer <= detectionRange)
                {
                    navMeshAgent.SetDestination(player.position);
                }
                else if (distanceToPlayer > detectionRange)
                {
                    // 플레이어가 감지 범위를 벗어나면 다시 Idle 상태로 전환
                    currentState = MonsterState.Idle;
                    movingAnimation(false, 0);
                }
                break;

            case MonsterState.AttackReady:
                // 일정 시간 동안 공격을 준비한 후에 공격
                if (delayTime > attackDelay)
                {
                    currentState = MonsterState.Attacking;
                    
                    delayTime = 0.0f;
                }

                delayTime += deltaTime;
                break;

            case MonsterState.Attacking:
                // 여기에서 공격 애니메이션을 재생하고 실제 공격 동작을 구현
                // 이 부분은 몬스터의 공격 로직에 따라 구현되어야 합니다.
                if (distanceToPlayer > attackRange) {
                    Debug.Log("Miss!");
                } else {
                    Debug.Log("Attack!");

                    monsterAttack.attack();
                }

                // 공격이 끝나면 이동 상태로 전환
                currentState = MonsterState.Moving;
                navMeshAgent.SetDestination(player.position);
                navMeshAgent.isStopped = false;
                animator.SetFloat("Animation Speed", movingAnimationSpeed);
                movingAnimation(true, 10);
                break;
        }

        
    }

    private void movingAnimation(bool flag, float velocity) {
        animator.SetBool("Moving", flag);
        animator.SetFloat("Velocity", velocity);
    }
}

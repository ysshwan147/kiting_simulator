using UnityEngine;
using UnityEngine.AI;

public class MonsterController : Controller
{
    public GameObject playerObject;
    public float detectionRange = 10f; // 몬스터의 감지 범위
    public float attackRange = 2f; // 몬스터의 공격 사거리
    public float attackAnimationSpeed = 0.3f;
    public float movingAnimationSpeed = 1.0f;

    private Transform player;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    private MonsterAttack monsterAttack;
    private MonsterState currentState;

    private enum MonsterState
    {
        Idle,
        Moving,
        AttackReady,
        Attacking
    }


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

        monsterAttack = GetComponent<MonsterAttack>();
    }

    private void Update()
    {
        Vector3 targetPosition = player.position;
        targetPosition.y = transform.position.y;
        float distanceToPlayer = Vector3.Distance(transform.position, targetPosition);
        float deltaTime = Time.deltaTime;

        if (!animating)
        {
            if (distanceToPlayer > detectionRange)
            {
                currentState = MonsterState.Idle;
            }
            else if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange)
            {
                currentState = MonsterState.Moving;
            }
            else if (distanceToPlayer <= attackRange)
            {
                currentState = MonsterState.AttackReady;
            }
        }

        switch (currentState)
        {
            case MonsterState.Idle:
            {
                setIdleAnimation();
                break;
            }
            case MonsterState.Moving:
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetPosition);
                setMovingAnimation();
                break;
            }
            case MonsterState.AttackReady:
            {
                navMeshAgent.isStopped = true; // 이동 중지
                setIdleAnimation();
                currentState = MonsterState.Attacking;
                animating = true;
                break;
            }
            case MonsterState.Attacking:
            {
                // 여기에서 공격 애니메이션을 재생하고 실제 공격 동작을 구현
                // 이 부분은 몬스터의 공격 로직에 따라 구현되어야 합니다.
                if (hitting)
                {
                    Debug.Log("Attack!");
                    monsterAttack.attack();
                }
                transform.LookAt(targetPosition);
                setAttackAnimation();

                
                break;
            }
        }
        
    }

    private void setMovingAnimation() {
        animator.SetFloat("Animation Speed", movingAnimationSpeed);
        animator.SetTrigger("Moving");
        animator.SetFloat("Velocity", 10);
    }


    private void setIdleAnimation()
    {
        animator.SetFloat("Animation Speed", movingAnimationSpeed);
        animator.SetTrigger("Idle");
        animator.SetFloat("Velocity", 0);
    }


    private void setAttackAnimation()
    {
        animator.SetFloat("Animation Speed", attackAnimationSpeed);
        animator.SetTrigger("Attack");
    }
}

using UnityEngine;
using UnityEngine.AI;

public class PlayerController : Controller
{
    public float attackAnimationSpeed = 0.3f;
    public float movingAnimationSpeed = 1.0f;
    public Vector3 startingPosition;

    private NavMeshAgent navMeshAgent;

    [HideInInspector] public Animator animator;
    [HideInInspector] public Attack attackScript;

    private enum PlayerState
    {
        Idle,
        Attacking,
        Moving
    }

    private PlayerState currentState;

    private void Start()
    {
        // NavMeshAgent 컴포넌트를 가져옵니다.
        navMeshAgent = GetComponent<NavMeshAgent>();
        // 회전을 NavMeshAgent에서 제어하도록 설정합니다.
        navMeshAgent.updateRotation = false;


        // Setup Animator, add AnimationEvents script.
        animator = GetComponentInChildren<Animator>();
        if (animator == null) {
            Debug.LogError("ERROR: There is no Animator component for character.");
            Debug.Break();
        } 

        attackScript = GetComponent<Attack>();

        currentState = PlayerState.Idle;

        animating = true;

        transform.position = startingPosition;
    }

    private void Update()
    {

        // 좌클릭을 감지하면 몬스터를 공격 상태로 전환
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Monster"))
                {
                    currentState = PlayerState.Attacking;

                    transform.LookAt(hit.point);
                }
                else if (hit.collider.CompareTag("Ground"))
                {
                    currentState = PlayerState.Moving;

                    if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 1.0f, NavMesh.AllAreas))
                    {
                        // navMeshAgent.destination = navHit.position;

                        navMeshAgent.SetDestination(navHit.position);
                        navMeshAgent.isStopped = false;

                        // 플레이어의 방향을 즉시 변경합니다.
                        transform.LookAt(navHit.position);
                    }
                }
            }
        }

        if (!animating)
        {
            currentState = PlayerState.Idle;
            animating = true;
        }

        if (navMeshAgent.isStopped)
        {
            currentState = PlayerState.Idle;
        }

        // 상태에 따라 애니메이션 및 동작을 처리
        switch (currentState)
        {
            case PlayerState.Idle:
                // Debug.Log("idle");
                // Idle 상태 처리
                setIdleAnimation();
                break;

            case PlayerState.Attacking:
                // Debug.Log("attack");
                // 공격 상태 처리 (공격 애니메이션 재생 등)

                setAttackAnimation();
                
                if (hitting)
                {
                    hitting = false;
                    // Debug.Log("Player Attack!");
                    attackScript.attack();
                }
                
                break;

            case PlayerState.Moving:
                // Debug.Log("move");
                // 이동 상태 처리 (이동 명령 실행 등)
                
                setMovingAnimation();
                break;
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

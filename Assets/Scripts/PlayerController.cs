using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    
    // [HideInInspector] public SuperCharacterController superCharacterController;
    // [HideInInspector] public PlayerMovementController playerMovementController;
    [HideInInspector] public Animator animator;


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
        // else {
        //     animator.gameObject.AddComponent<WarriorCharacterAnimatorEvents>();
        //     animator.GetComponent<WarriorCharacterAnimatorEvents>().warriorController = this;
        //     animator.gameObject.AddComponent<AnimatorParentMove>();
        //     animator.GetComponent<AnimatorParentMove>().animator = animator;
        //     animator.GetComponent<AnimatorParentMove>().warriorController = this;
        //     animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
        //     animator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
        // }


        animator.SetFloat("Animation Speed", 1);
    }

    private void Update()
    {
        // 마우스 우클릭을 감지합니다.
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Ray를 쏘아 맞은 지점을 확인합니다.
            if (Physics.Raycast(ray, out hit))
            {
                // NavMesh에서 유효한 위치를 찾아 플레이어의 목표 위치로 설정합니다.
                if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 1.0f, NavMesh.AllAreas))
                {
                    navMeshAgent.destination = navHit.position;

                    // 플레이어의 방향을 즉시 변경합니다.
                    transform.LookAt(navHit.position);

                    animator.SetBool("Moving", true);
                    animator.SetFloat("Velocity", 100);
                }
            }
        }
        else if (!navMeshAgent.isStopped)
        {
            // 이동 중인 경우 (NavMeshAgent가 동작 중)
            // Idle 상태로 전환할 필요 없음

            // 이동 중 애니메이션 활성화
            animator.SetBool("Moving", true);
            animator.SetFloat("Velocity", 100);
        }
        else //if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            // 이동 중이 아닌 경우 정지 애니메이션 활성화
            animator.SetBool("Moving", false);
            animator.SetFloat("Velocity", 0);
        }
        // else
        // {
        //     animator.SetBool("Moving", true);
        //     animator.SetFloat("Velocity", 100);
        // }
    }
}

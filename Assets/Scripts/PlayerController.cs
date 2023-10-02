using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 targetPosition;
    private bool isMoving = false;

    [SerializeField] private float moveSpeed = 1.0f;

    private void Update()
    {
        // 마우스 왼쪽 버튼을 클릭하면 플레이어를 움직일 목표 위치를 설정합니다.
        if (Input.GetMouseButtonDown(1))
        {
            // 마우스 클릭 위치를 월드 좌표로 변환합니다.
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.y = 0f;

            // 플레이어가 움직이도록 플래그를 설정합니다.
            isMoving = true;
        }

        // 플레이어를 목표 위치로 움직입니다.
        if (isMoving)
        {
            // 플레이어를 목표 위치로 부드럽게 이동시킵니다.
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);

            // 목표 위치에 도달하면 움직임을 멈춥니다.
            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }
}

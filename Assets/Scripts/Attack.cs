using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject ballPrefab; // 공 프리팹을 할당할 변수
    public GameObject Shoot;
    public float ballSpeed = 10f;
    public float ballDuration = 5f;
    public string targetTag;
    private Vector3 ballDirection;

    public void attack()
    {
        // 공 오브젝트 생성
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);

        ball.transform.position = Shoot.transform.position;
        // Ball 스크립트를 가져옴
        Ball ballScript = ball.GetComponent<Ball>();

        if (ballScript != null)
        {
            ballDirection = transform.forward;

            ballScript.Initialize(ballSpeed, ballDuration, ballDirection, targetTag);
        }
    }
}

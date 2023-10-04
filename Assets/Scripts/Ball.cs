using UnityEngine;

public class Ball : MonoBehaviour
{
    private float speed;
    private float duration;
    private Vector3 direction;
    private string targetTag;

    private void Update() {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void Initialize(float _speed, float _duration, Vector3 _direction, string _targetTag)
    {
        speed = _speed;
        duration = _duration;
        direction = _direction;
        targetTag = _targetTag;

        

        // 일정 시간이 지난 후에 공을 파괴합니다.
        Destroy(gameObject, duration);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트의 태그를 확인합니다.
        if (other.CompareTag(targetTag))
        {
            // 상대 오브젝트의 BallHit 스크립트를 가져옵니다.
            BallHit ballHitScript = other.GetComponent<BallHit>();

            if (ballHitScript != null)
            {
                // BallHit 스크립트의 ballHit() 메서드를 호출합니다.
                ballHitScript.ballHit();
            }

            // 충돌한 오브젝트를 파괴합니다.
            Destroy(gameObject);
        }
    }
}

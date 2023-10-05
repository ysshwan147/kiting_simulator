
using UnityEngine;

public class BallHit : MonoBehaviour
{

    public void ballHit()
    {
        Debug.Log("ball hit");

        GameManager.endGame();
    }
}

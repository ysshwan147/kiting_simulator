using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static Timer timer;

    void Start()
    {
        
    }

    public static void startGame() {
        SceneManager.LoadScene("Game");
    }

    public static void endGame() {
        string elapsedTime = timer.getTime();
        PlayerPrefs.SetString("LastTime", elapsedTime);
        PlayerPrefs.Save();

        SceneManager.LoadScene("GameOver");
    }

    public static void setTimerObject(GameObject o) {
        timer = o.GetComponent<Timer>();
    }
}

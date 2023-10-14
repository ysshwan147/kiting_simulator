
using UnityEngine;

public class BallHit : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip gameOverSound;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    public void ballHit()
    {
        audioSource.clip = gameOverSound;
        audioSource.Play();

        GameManager.endGame();
    }
}

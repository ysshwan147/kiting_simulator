using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clickSound;
    private void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clickSound;
    }

    public void click() {
        audioSource.Play();
    }
}

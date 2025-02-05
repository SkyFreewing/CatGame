using UnityEngine;

public class SoundSourceRoutine : MonoBehaviour
{
    public bool Played;

    AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Update()
    {
        if (audioSource.isPlaying && !Played)
            Played = true;

        if (!audioSource.isPlaying && Played)
            GameObject.Destroy(gameObject);
    }
}

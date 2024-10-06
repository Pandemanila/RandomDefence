using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioClip bgmClip; 
    private AudioSource audioSource;
    [SerializeField]
    private GameObject canvas;

    void Start()
    {   
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }


        audioSource.clip = bgmClip;
        audioSource.loop = true;  
        audioSource.playOnAwake = false;
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
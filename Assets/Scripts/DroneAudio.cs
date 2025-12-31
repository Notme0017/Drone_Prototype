using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DroneAudio : MonoBehaviour
{
    public AudioClip idlehum;
    public AudioClip throttlehum;
    public AudioClip gotHit;

    [SerializeField]
    private float pitchSmooth = 5f;

    AudioSource audioSource;

    float targetPitch = 1f;
    float targetVolume = 0.6f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        PlayIdle();
    }

    private void Update()
    {
        audioSource.pitch = Mathf.Lerp(audioSource.pitch, targetPitch, pitchSmooth * Time.deltaTime);
        audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, pitchSmooth * Time.deltaTime);
    }

    public void PlayIdle()
    {
        if (audioSource.clip == idlehum) return;

        audioSource.clip = idlehum;
        audioSource.loop = true;
        audioSource.Play();

        targetPitch = 1f;
        targetVolume = 0.4f;
    }

    public void PlayThrottle(float intensity)
    {
        if (audioSource.clip == throttlehum) return;

        audioSource.clip = throttlehum;
        audioSource.loop = true;
        audioSource.Play();

        targetPitch = Mathf.Lerp(1.0f, 1.3f, intensity);
        targetVolume = Mathf.Lerp(0.6f, 1.0f, intensity);
    }

    public void PlayHit()
    {
        audioSource.PlayOneShot(gotHit, 1f);
    }
}

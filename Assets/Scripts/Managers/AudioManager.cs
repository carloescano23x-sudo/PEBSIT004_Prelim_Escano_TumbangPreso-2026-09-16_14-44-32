using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // =========================================================
    // INSTANCE
    // =========================================================

    public static AudioManager Instance;

    // =========================================================
    // AUDIO SOURCES
    // =========================================================

    [Header("Audio Sources")]
    public AudioSource soundEffectSource;
    public AudioSource musicSource;

    // =========================================================
    // SOUND EFFECTS
    // =========================================================

    [Header("Sound Effects")]
    public AudioClip throwSound;
    public AudioClip canHitSound;
    public AudioClip missSound;
    public AudioClip gameOverSound;

    // =========================================================
    // BACKGROUND MUSIC
    // =========================================================

    [Header("Background Music")]
    public AudioClip backgroundMusic;

    // =========================================================
    // VOLUME
    // =========================================================

    [Header("Volume Settings")]

    [Range(0f, 1f)]
    public float soundEffectVolume = 1f;

    [Range(0f, 1f)]
    public float musicVolume = 0.35f;

    // =========================================================
    // AWAKE
    // =========================================================

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // =========================================================
    // START
    // =========================================================

    private void Start()
    {
        UpdateVolumes();

        PlayBackgroundMusic();
    }

    // =========================================================
    // UPDATE VOLUMES
    // =========================================================

    public void UpdateVolumes()
    {
        if (soundEffectSource != null)
        {
            soundEffectSource.volume =
                soundEffectVolume;
        }

        if (musicSource != null)
        {
            musicSource.volume =
                musicVolume;
        }
    }

    // =========================================================
    // THROW SOUND
    // =========================================================

    public void PlayThrowSound()
    {
        PlaySoundEffect(
            throwSound
        );
    }

    // =========================================================
    // CAN HIT SOUND
    // =========================================================

    public void PlayCanHitSound()
    {
        PlaySoundEffect(
            canHitSound
        );
    }

    // =========================================================
    // MISS SOUND
    // =========================================================

    public void PlayMissSound()
    {
        PlaySoundEffect(
            missSound
        );
    }

    // =========================================================
    // GAME OVER SOUND
    // =========================================================

    public void PlayGameOverSound()
    {
        PlaySoundEffect(
            gameOverSound
        );
    }

    // =========================================================
    // GENERIC SOUND EFFECT PLAYER
    // =========================================================

    private void PlaySoundEffect(
        AudioClip clip
    )
    {
        if (clip == null)
        {
            return;
        }

        if (soundEffectSource == null)
        {
            Debug.LogWarning(
                "Sound Effect AudioSource is missing!"
            );

            return;
        }

        soundEffectSource.PlayOneShot(
            clip
        );
    }

    // =========================================================
    // BACKGROUND MUSIC
    // =========================================================

    private void PlayBackgroundMusic()
    {
        if (backgroundMusic == null)
        {
            return;
        }

        if (musicSource == null)
        {
            Debug.LogWarning(
                "Music AudioSource is missing!"
            );

            return;
        }

        musicSource.clip =
            backgroundMusic;

        musicSource.loop = true;

        musicSource.Play();
    }
}
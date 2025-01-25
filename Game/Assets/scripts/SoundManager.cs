using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // SoundManager'� her yerden kolayca eri�mek i�in Singleton yap�s�
    public static SoundManager Instance;

   
    public AudioSource musicSource; // Arkaplan m�zi�i 
    public AudioSource sfxSource;   // Ses efektleri 

  
    public AudioClip backgroundMusic;  // Arka plan m�zi�i (loop)
    public AudioClip healthSound;          // Can toplama sesi
    public AudioClip fireSound;         // Ate� sesi
    public AudioClip ultiSound;//ulti sesi
    public AudioClip explosionSound;  // pat sesi

   
    public bool isMuted = false; // T�m sesleri kapatmak/a�mak i�in kontrol

    private void Awake()
    {
        // E�er hen�z bir SoundManager yoksa bunu kullan, yoksa yok et
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne ge�i�lerinde kaybolmas�n
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // Arkaplan m�zi�ini ba�lat
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true; // S�rekli d�ng�
            musicSource.Play();
        }
    }

    // Ses efekti �almak i�in �a��raca��m�z fonksiyon
    public void PlaySFX(AudioClip clip)
    {
        // E�er global mute aktif de�ilse ve clip ge�erliyse ses �al
        if (sfxSource != null && clip != null && !isMuted)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    // Ana men�deki butondan �a��rarak
    // t�m sesleri kapat�p a�mak i�in bir fonksiyon
    public void ToggleMuteAll()
    {
        isMuted = !isMuted;

        // Arkaplan m�zi�i ve efektleri direkt "mute" �zelli�iyle kapat�yoruz
        musicSource.mute = isMuted;
        sfxSource.mute = isMuted;
    }
    
}

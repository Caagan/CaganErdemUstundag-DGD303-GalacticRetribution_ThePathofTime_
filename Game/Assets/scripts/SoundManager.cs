using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // SoundManager'ý her yerden kolayca eriþmek için Singleton yapýsý
    public static SoundManager Instance;

   
    public AudioSource musicSource; // Arkaplan müziði 
    public AudioSource sfxSource;   // Ses efektleri 

  
    public AudioClip backgroundMusic;  // Arka plan müziði (loop)
    public AudioClip healthSound;          // Can toplama sesi
    public AudioClip fireSound;         // Ateþ sesi
    public AudioClip ultiSound;//ulti sesi
    public AudioClip explosionSound;  // pat sesi

   
    public bool isMuted = false; // Tüm sesleri kapatmak/açmak için kontrol

    private void Awake()
    {
        // Eðer henüz bir SoundManager yoksa bunu kullan, yoksa yok et
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne geçiþlerinde kaybolmasýn
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // Arkaplan müziðini baþlat
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true; // Sürekli döngü
            musicSource.Play();
        }
    }

    // Ses efekti çalmak için çaðýracaðýmýz fonksiyon
    public void PlaySFX(AudioClip clip)
    {
        // Eðer global mute aktif deðilse ve clip geçerliyse ses çal
        if (sfxSource != null && clip != null && !isMuted)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    // Ana menüdeki butondan çaðýrarak
    // tüm sesleri kapatýp açmak için bir fonksiyon
    public void ToggleMuteAll()
    {
        isMuted = !isMuted;

        // Arkaplan müziði ve efektleri direkt "mute" özelliðiyle kapatýyoruz
        musicSource.mute = isMuted;
        sfxSource.mute = isMuted;
    }
    
}

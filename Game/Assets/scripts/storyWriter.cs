using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class storyWriter : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Yazdýrma Ayarlarý")]
     private float typingSpeed = 0.03f;  // Harfler arasý bekleme süresi
    [SerializeField, TextArea(3, 10)] private string fullText; // Yazdýrýlacak tam metin
     private float skipTypingSpeed = 0.0005f; //

   
    private string nextSceneName = "Level1"; // Geçeceðimiz sahnenin adý

    
    [SerializeField] private TextMeshProUGUI textMeshPro; // TextMeshPro bileþeni
    private bool isSkipping = false;

    

    private IEnumerator Start()
    {
        // Metni sýfýrla
        textMeshPro.text = "";

        // Harf harf yazma döngüsü
        for (int i = 0; i < fullText.Length; i++)
        {
            // Eðer ekrana dokunulmuþ veya fare týklanmýþsa hýzlandýr
            if (Input.GetMouseButton(0) || Input.touchCount > 0)
            {
                isSkipping = true;
            }

            // Þu ana kadarki metni ekrana yaz
            textMeshPro.text = fullText.Substring(0, i + 1);

            // Hýzlandýrýlmýþ mý, yoksa normal hýzda mý?
            float currentSpeed = isSkipping ? skipTypingSpeed : typingSpeed;

            // Harfler arasýndaki bekleme
            yield return new WaitForSeconds(currentSpeed);
        }

        // Metin tamamlandýktan sonra uyarý ekle
        textMeshPro.text += "\n\n<color=#FFFF00>PRESS ANY BUTTON TO START...</color>";

        // Herhangi bir tuþa basýlmasýný bekle
        yield return new WaitUntil(() => Input.anyKeyDown);

        // Sahneyi yükle
        SceneManager.LoadScene(nextSceneName);
    }
}

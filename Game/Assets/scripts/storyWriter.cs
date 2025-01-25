using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class storyWriter : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Yazd�rma Ayarlar�")]
     private float typingSpeed = 0.03f;  // Harfler aras� bekleme s�resi
    [SerializeField, TextArea(3, 10)] private string fullText; // Yazd�r�lacak tam metin
     private float skipTypingSpeed = 0.0005f; //

   
    private string nextSceneName = "Level1"; // Ge�ece�imiz sahnenin ad�

    
    [SerializeField] private TextMeshProUGUI textMeshPro; // TextMeshPro bile�eni
    private bool isSkipping = false;

    

    private IEnumerator Start()
    {
        // Metni s�f�rla
        textMeshPro.text = "";

        // Harf harf yazma d�ng�s�
        for (int i = 0; i < fullText.Length; i++)
        {
            // E�er ekrana dokunulmu� veya fare t�klanm��sa h�zland�r
            if (Input.GetMouseButton(0) || Input.touchCount > 0)
            {
                isSkipping = true;
            }

            // �u ana kadarki metni ekrana yaz
            textMeshPro.text = fullText.Substring(0, i + 1);

            // H�zland�r�lm�� m�, yoksa normal h�zda m�?
            float currentSpeed = isSkipping ? skipTypingSpeed : typingSpeed;

            // Harfler aras�ndaki bekleme
            yield return new WaitForSeconds(currentSpeed);
        }

        // Metin tamamland�ktan sonra uyar� ekle
        textMeshPro.text += "\n\n<color=#FFFF00>PRESS ANY BUTTON TO START...</color>";

        // Herhangi bir tu�a bas�lmas�n� bekle
        yield return new WaitUntil(() => Input.anyKeyDown);

        // Sahneyi y�kle
        SceneManager.LoadScene(nextSceneName);
    }
}

using UnityEngine;

public class AutoResizeScroll : MonoBehaviour
{
    [Header("Boyu Değişecek Scroll View")]
    public RectTransform scrollViewRect;

    [Header("Height (Yükseklik) Değerleri")]
    public float normalHeight = 673f;      // Resmindeki tam boy
    public float kuculmusHeight = 400f;    // Alt bar varken (Örnek değer)

    void OnEnable()
    {
        // Menü açıldığında panelin yüksekliğini (Height) azalt
        if (scrollViewRect != null)
            scrollViewRect.sizeDelta = new Vector2(scrollViewRect.sizeDelta.x, kuculmusHeight);
    }

    void OnDisable()
    {
        // Menü kapandığında panelin yüksekliğini eski uzun haline getir
        if (scrollViewRect != null)
            scrollViewRect.sizeDelta = new Vector2(scrollViewRect.sizeDelta.x, normalHeight);
    }
}
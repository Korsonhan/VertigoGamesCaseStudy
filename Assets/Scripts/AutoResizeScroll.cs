using UnityEngine;

public class AutoResizeScroll : MonoBehaviour
{
    [Header("Boyu Değişecek Scroll View")]
    public RectTransform scrollViewRect;

    [Header("Height (Yükseklik) Değerleri")]
    public float normalHeight = 673f;      
    public float kuculmusHeight = 400f;    

    // SİHİRLİ SAYAÇ (Tüm menülerin ortak hafızası - "static" kelimesi bunu sağlar)
    private static int acikMenuSayisi = 0;

    void OnEnable()
    {
        acikMenuSayisi++; // Bir menü açıldığında sayacı 1 artır
        
        // Menü açıldığı için paneli kesinlikle küçült
        if (scrollViewRect != null)
            scrollViewRect.sizeDelta = new Vector2(scrollViewRect.sizeDelta.x, kuculmusHeight);
    }

    void OnDisable()
    {
        acikMenuSayisi--; // Bir menü kapandığında sayacı 1 azalt
        
        // SADECE EĞER AÇIKTA HİÇBİR MENÜ KALMADIYSA paneli eski uzun haline getir!
        if (acikMenuSayisi <= 0)
        {
            acikMenuSayisi = 0; // Eksiye düşmesini engellemek için güvenlik kilidi
            if (scrollViewRect != null)
                scrollViewRect.sizeDelta = new Vector2(scrollViewRect.sizeDelta.x, normalHeight);
        }
    }
}
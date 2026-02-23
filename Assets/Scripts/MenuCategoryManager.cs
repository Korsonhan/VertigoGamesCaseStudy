using UnityEngine;

public class MenuCategoryManager : MonoBehaviour
{
    [Header("Tüm Kategori Yazıları (Sırasıyla)")]
    public GameObject[] categoryTexts;

    [Header("Tüm Sarı Çizgiler (Sırasıyla)")]
    public GameObject[] yellowHighlights;

    private bool isMenuExpanded = false;

    // Bu fonksiyon butona tıklandığında çalışacak (0=Sight, 1=Mag...)
    public void OnCategoryClicked(int index)
    {
        // 1. Eğer ilk kez tıklanıyorsa, tüm yazıları görünür yap
        if (!isMenuExpanded)
        {
            foreach (GameObject txt in categoryTexts)
            {
                if (txt != null) txt.SetActive(true);
            }
            isMenuExpanded = true;
        }

        // 2. Sadece tıklanan kategorinin sarı çizgisini aç, diğerlerini kapat
        for (int i = 0; i < yellowHighlights.Length; i++)
        {
            if (yellowHighlights[i] != null)
            {
                // Eğer döngüdeki numara, tıklanan numaraya eşitse (true) yap, değilse (false) yap
                yellowHighlights[i].SetActive(i == index);
            }
        }
    }
}
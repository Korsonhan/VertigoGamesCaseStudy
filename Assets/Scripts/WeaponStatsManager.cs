using UnityEngine;
using TMPro;
using UnityEngine.UI; // Kutuların rengini değiştirmek için bu şart!

public class WeaponStatsManager : MonoBehaviour
{
    [Header("Silahın Çıplak (Eklentisiz) Gücü")]
    public float baseDamage = 100f;
    public float baseRange = 50f;
    public float baseAccuracy = 80f;

    [Header("UI Yazıları - Toplam Değer (Beyaz)")]
    public TextMeshProUGUI damageTotalText;
    public TextMeshProUGUI rangeTotalText;
    public TextMeshProUGUI accuracyTotalText;

    [Header("UI Yazıları - Fark (Renkli)")]
    public TextMeshProUGUI damageDiffText;
    public TextMeshProUGUI rangeDiffText;
    public TextMeshProUGUI accuracyDiffText;

    [Header("UI Kutuları (Arka Plan Renkleri İçin)")]
    public Image damageBarImage;
    public Image rangeBarImage;
    public Image accuracyBarImage;

    [Header("Kutu Arka Plan Renkleri")]
    public Color defaultColor = new Color(0.2f, 0.3f, 0.4f, 0.5f); // Normal Mavi/Gri (Saydam)
    public Color buffColor = new Color(0.1f, 0.6f, 0.1f, 0.5f);    // Artış varsa Yeşil (Saydam)
    public Color nerfColor = new Color(0.6f, 0.1f, 0.1f, 0.5f);    // Düşüş varsa Kırmızı (Saydam)

    public void CalculateAndLogStats(AttachmentStats equippedStats, AttachmentStats previewStats)
    {
        float eqDamage = equippedStats != null ? equippedStats.damageBonus : 0;
        float eqRange = equippedStats != null ? equippedStats.rangeBonus : 0;
        float eqAccuracy = equippedStats != null ? equippedStats.accuracyBonus : 0;

        float prevDamage = previewStats != null ? previewStats.damageBonus : 0;
        float prevRange = previewStats != null ? previewStats.rangeBonus : 0;
        float prevAccuracy = previewStats != null ? previewStats.accuracyBonus : 0;

        // Beyaz yazıları güncelle
        if (damageTotalText != null) damageTotalText.text = (baseDamage + eqDamage).ToString("F1");
        if (rangeTotalText != null) rangeTotalText.text = (baseRange + eqRange).ToString("F1");
        if (accuracyTotalText != null) accuracyTotalText.text = (baseAccuracy + eqAccuracy).ToString("F0");

        // Farkları hesapla ve hem yazıyı hem kutu rengini güncelle
        UpdateDiffUI(damageDiffText, damageBarImage, prevDamage - eqDamage);
        UpdateDiffUI(rangeDiffText, rangeBarImage, prevRange - eqRange);
        UpdateDiffUI(accuracyDiffText, accuracyBarImage, prevAccuracy - eqAccuracy);
    }

    private void UpdateDiffUI(TextMeshProUGUI diffText, Image barImage, float difference)
    {
        if (diffText == null) return;

        if (Mathf.Abs(difference) < 0.01f) // Fark yoksa veya EQUIP edildiyse
        {
            diffText.text = ""; 
            if (barImage != null) barImage.color = defaultColor; // Rengi normale döndür
        }
        else if (difference > 0)
        {
            diffText.text = "+" + difference.ToString("F1");
            diffText.color = Color.green;
            if (barImage != null) barImage.color = buffColor; // Kutuyu yeşilimsi yap
        }
        else
        {
            diffText.text = difference.ToString("F1");
            diffText.color = Color.red;
            if (barImage != null) barImage.color = nerfColor; // Kutuyu kırmızımsı yap
        }
    }
}
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponStatsManager : MonoBehaviour
{
    [Header("Silahın Temel (Çıplak) Değerleri")]
    public float basePower = 275f;
    public float baseDamage = 549.6f;
    public float baseFireRate = 600f;
    public float baseAccuracy = 96f;
    public float baseSpeed = 96f;
    public float baseRange = 26.4f;

    [Header("UI - Toplam Değerler (Beyaz Yazılar)")]
    public TextMeshProUGUI powerTotalText;
    public TextMeshProUGUI damageTotalText;
    public TextMeshProUGUI fireRateTotalText;
    public TextMeshProUGUI accuracyTotalText;
    public TextMeshProUGUI speedTotalText;
    public TextMeshProUGUI rangeTotalText;

    [Header("UI - Fark Değerleri (Renkli Yazılar)")]
    public TextMeshProUGUI powerDiffText;
    public TextMeshProUGUI damageDiffText;
    public TextMeshProUGUI fireRateDiffText;
    public TextMeshProUGUI accuracyDiffText;
    public TextMeshProUGUI speedDiffText;
    public TextMeshProUGUI rangeDiffText;

    [Header("UI - Kutu Arka Planları (Renklenecek)")]
    public Image powerBarImage;
    public Image damageBarImage;
    public Image fireRateBarImage;
    public Image accuracyBarImage;
    public Image speedBarImage;
    public Image rangeBarImage;

    [Header("Renk Ayarları")]
    public Color defaultColor = new Color(0.2f, 0.3f, 0.4f, 0.5f); // Normal
    public Color buffColor = new Color(0.1f, 0.6f, 0.1f, 0.5f);    // Yeşil (Artış)
    public Color nerfColor = new Color(0.6f, 0.1f, 0.1f, 0.5f);    // Kırmızı (Düşüş)

    void Start()
    {
        // Oyun başlar başlamaz sistemdeki tüm fark yazılarını ve renklerini SIFIRLA!
        UpdateDiffUI(powerDiffText, powerBarImage, 0);
        UpdateDiffUI(damageDiffText, damageBarImage, 0);
        UpdateDiffUI(fireRateDiffText, fireRateBarImage, 0);
        UpdateDiffUI(accuracyDiffText, accuracyBarImage, 0);
        UpdateDiffUI(speedDiffText, speedBarImage, 0);
        UpdateDiffUI(rangeDiffText, rangeBarImage, 0);
    }

    public void CalculateAndLogStats(AttachmentStats equippedStats, AttachmentStats previewStats)
    {
        // 1. ŞU AN TAKILI OLANIN DEĞERLERİ
        float eqPower = equippedStats != null ? equippedStats.powerBonus : 0;
        float eqDamage = equippedStats != null ? equippedStats.damageBonus : 0;
        float eqFireRate = equippedStats != null ? equippedStats.fireRateBonus : 0;
        float eqAccuracy = equippedStats != null ? equippedStats.accuracyBonus : 0;
        float eqSpeed = equippedStats != null ? equippedStats.speedBonus : 0;
        float eqRange = equippedStats != null ? equippedStats.rangeBonus : 0;

        // 2. ÖNİZLENEN (PREVIEW) DEĞERLERİ
        float prevPower = previewStats != null ? previewStats.powerBonus : 0;
        float prevDamage = previewStats != null ? previewStats.damageBonus : 0;
        float prevFireRate = previewStats != null ? previewStats.fireRateBonus : 0;
        float prevAccuracy = previewStats != null ? previewStats.accuracyBonus : 0;
        float prevSpeed = previewStats != null ? previewStats.speedBonus : 0;
        float prevRange = previewStats != null ? previewStats.rangeBonus : 0;

        // 3. BEYAZ YAZILARI GÜNCELLE (Base + Takılı Olan)
        if (powerTotalText != null) powerTotalText.text = (basePower + eqPower).ToString("F0");
        if (damageTotalText != null) damageTotalText.text = (baseDamage + eqDamage).ToString("F1");
        if (fireRateTotalText != null) fireRateTotalText.text = (baseFireRate + eqFireRate).ToString("F0");
        if (accuracyTotalText != null) accuracyTotalText.text = (baseAccuracy + eqAccuracy).ToString("F0");
        if (speedTotalText != null) speedTotalText.text = (baseSpeed + eqSpeed).ToString("F0");
        if (rangeTotalText != null) rangeTotalText.text = (baseRange + eqRange).ToString("F1");

        // 4. FARKLARI HESAPLA VE RENKLENDİR
        UpdateDiffUI(powerDiffText, powerBarImage, prevPower - eqPower);
        UpdateDiffUI(damageDiffText, damageBarImage, prevDamage - eqDamage);
        UpdateDiffUI(fireRateDiffText, fireRateBarImage, prevFireRate - eqFireRate);
        UpdateDiffUI(accuracyDiffText, accuracyBarImage, prevAccuracy - eqAccuracy);
        UpdateDiffUI(speedDiffText, speedBarImage, prevSpeed - eqSpeed);
        UpdateDiffUI(rangeDiffText, rangeBarImage, prevRange - eqRange);
    }

    private void UpdateDiffUI(TextMeshProUGUI diffText, Image barImage, float difference)
    {
        if (diffText == null) return;

        if (Mathf.Abs(difference) < 0.01f) // Fark Yoksa
        {
            diffText.text = "";
            if (barImage != null) barImage.color = defaultColor;
        }
        else if (difference > 0) // Artış Varsa
        {
            diffText.text = "+" + difference.ToString("F1");
            diffText.color = Color.green;
            if (barImage != null) barImage.color = buffColor;
        }
        else // Düşüş Varsa
        {
            diffText.text = difference.ToString("F1");
            diffText.color = Color.red;
            if (barImage != null) barImage.color = nerfColor;
        }
    }
}
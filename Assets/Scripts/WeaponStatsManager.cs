using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic; // DİKKAT: Sözlük (Hafıza) yapısı için bu kütüphane ŞARTTIR!

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
    public Color defaultColor = new Color(0.2f, 0.3f, 0.4f, 0.5f); 
    public Color buffColor = new Color(0.1f, 0.6f, 0.1f, 0.5f);    
    public Color nerfColor = new Color(0.6f, 0.1f, 0.1f, 0.5f);    

    // İŞTE SİHİRLİ HAFIZA! (Hangi kategoride hangi eşya takılı aklında tutacak)
    private Dictionary<string, AttachmentStats> takiliEsyalarHafizasi = new Dictionary<string, AttachmentStats>();

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

    // YENİ: Artık kod, değerleri gönderenin "Kim" (categoryName) olduğunu da soruyor!
    public void CalculateAndLogStats(string categoryName, AttachmentStats equippedStats, AttachmentStats previewStats)
    {
        // 1. HAFIZAYI GÜNCELLE: Sana bu veriyi gönderen kategorinin (Örn: Group_Sight) takılı eşyasını hafızaya yaz.
        takiliEsyalarHafizasi[categoryName] = equippedStats;

        // 2. TÜM MENÜLERDEKİ TOPLAM GÜCÜ HESAPLA (Sadece bu menü değil, hafızadaki her şey!)
        float totalEqPower = 0, totalEqDamage = 0, totalEqFireRate = 0, totalEqAccuracy = 0, totalEqSpeed = 0, totalEqRange = 0;

        foreach (var eklenti in takiliEsyalarHafizasi.Values)
        {
            if (eklenti != null) // Eğer o kategoride bir şey takılıysa gücünü toplama ekle
            {
                totalEqPower += eklenti.powerBonus;
                totalEqDamage += eklenti.damageBonus;
                totalEqFireRate += eklenti.fireRateBonus;
                totalEqAccuracy += eklenti.accuracyBonus;
                totalEqSpeed += eklenti.speedBonus;
                totalEqRange += eklenti.rangeBonus;
            }
        }

        // 3. BEYAZ YAZILARI GÜNCELLE (Silahın Ana Gücü + TÜM Takılı Eşyaların Toplamı)
        if (powerTotalText != null) powerTotalText.text = (basePower + totalEqPower).ToString("F0");
        if (damageTotalText != null) damageTotalText.text = (baseDamage + totalEqDamage).ToString("F1");
        if (fireRateTotalText != null) fireRateTotalText.text = (baseFireRate + totalEqFireRate).ToString("F0");
        if (accuracyTotalText != null) accuracyTotalText.text = (baseAccuracy + totalEqAccuracy).ToString("F0");
        if (speedTotalText != null) speedTotalText.text = (baseSpeed + totalEqSpeed).ToString("F0");
        if (rangeTotalText != null) rangeTotalText.text = (baseRange + totalEqRange).ToString("F1");

        // 4. ŞU ANKİ KATEGORİ İÇİN FARK HESAPLA (Yeşil/Kırmızı yazılar sadece baktığın eklentiyi gösterir)
        float eqPower = equippedStats != null ? equippedStats.powerBonus : 0;
        float eqDamage = equippedStats != null ? equippedStats.damageBonus : 0;
        float eqFireRate = equippedStats != null ? equippedStats.fireRateBonus : 0;
        float eqAccuracy = equippedStats != null ? equippedStats.accuracyBonus : 0;
        float eqSpeed = equippedStats != null ? equippedStats.speedBonus : 0;
        float eqRange = equippedStats != null ? equippedStats.rangeBonus : 0;

        float prevPower = previewStats != null ? previewStats.powerBonus : 0;
        float prevDamage = previewStats != null ? previewStats.damageBonus : 0;
        float prevFireRate = previewStats != null ? previewStats.fireRateBonus : 0;
        float prevAccuracy = previewStats != null ? previewStats.accuracyBonus : 0;
        float prevSpeed = previewStats != null ? previewStats.speedBonus : 0;
        float prevRange = previewStats != null ? previewStats.rangeBonus : 0;

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
        if (Mathf.Abs(difference) < 0.01f) { diffText.text = ""; if (barImage != null) barImage.color = defaultColor; }
        else if (difference > 0) { diffText.text = "+" + difference.ToString("F1"); diffText.color = Color.green; if (barImage != null) barImage.color = buffColor; }
        else { diffText.text = difference.ToString("F1"); diffText.color = Color.red; if (barImage != null) barImage.color = nerfColor; }
    }
}
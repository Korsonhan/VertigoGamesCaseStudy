using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttachmentCategory : MonoBehaviour
{
    [Header("Şu An Silaha Takılı Olan (Kalıcı) Parça")]
    public GameObject equippedItem;

    [Header("Equip Butonu Referansları")]
    public Button equipButton;
    public TextMeshProUGUI equipButtonText;

    [Header("Seçim Çerçeveleri (Sırasıyla)")]
    public GameObject[] attachmentItems; 
    public GameObject[] highlightImages; 

    [Header("Sol Menü İkon Güncellemesi (YENİ)")]
    public Image mainCategoryIcon; // Sol paneldeki SIGHT ikonu
    public Sprite[] attachmentIcons; // Alt paneldeki butonların 2D ikonları (Sırasıyla)

    private GameObject previewItem;
    public WeaponStatsManager statsManager;

    void Start()
    {
        previewItem = equippedItem;
        UpdateHighlights();
        UpdateMainCategoryIcon(); // Oyun başlarken sol ikonu doğru ayarla
    }

    void OnEnable()
    {
        UpdateEquipButtonState();
        UpdateHighlights();
    }

    public void PreviewItem(GameObject itemToPreview)
    {
        if (previewItem != null) previewItem.SetActive(false);
        if (equippedItem != null) equippedItem.SetActive(false);

        previewItem = itemToPreview;
        if (previewItem != null) previewItem.SetActive(true);

        // İstatistik Hesaplayıcıyı Çalıştır
        if (statsManager != null)
        {
            AttachmentStats eqStats = equippedItem != null ? equippedItem.GetComponent<AttachmentStats>() : null;
            AttachmentStats prevStats = previewItem != null ? previewItem.GetComponent<AttachmentStats>() : null;
            statsManager.CalculateAndLogStats(eqStats, prevStats);
        }

        UpdateEquipButtonState();
        UpdateHighlights();
    }

    public void ConfirmEquip()
    {
        if (gameObject.activeInHierarchy) 
        {
            equippedItem = previewItem; 
            UpdateEquipButtonState();
            UpdateMainCategoryIcon(); // Onaylayınca sol ikonu da değiştir!
        }
    }

    void OnDisable()
    {
        if (previewItem != equippedItem)
        {
            if (previewItem != null) previewItem.SetActive(false);
            if (equippedItem != null) equippedItem.SetActive(true);
            previewItem = equippedItem;
        }
    }

    private void UpdateEquipButtonState()
    {
        if (equipButton == null || equipButtonText == null) return;

        if (previewItem == equippedItem)
        {
            equipButton.interactable = false;
            equipButtonText.text = "EQUIPPED";
        }
        else
        {
            equipButton.interactable = true;
            equipButtonText.text = "EQUIP";
        }
    }

    private void UpdateHighlights()
    {
        if (attachmentItems.Length != highlightImages.Length) return;

        for (int i = 0; i < attachmentItems.Length; i++)
        {
            if (highlightImages[i] != null && attachmentItems[i] != null)
            {
                highlightImages[i].SetActive(attachmentItems[i] == previewItem);
            }
        }
    }

    // YENİ SİHİRLİ FONKSİYON: Sol paneli günceller
    private void UpdateMainCategoryIcon()
    {
        // Eğer ikonları atamayı unuttuysak kod çökmesin
        if (mainCategoryIcon == null || attachmentIcons.Length == 0) return;

        for (int i = 0; i < attachmentItems.Length; i++)
        {
            // Şu anki takılı 3D parçayı listemizde bulursak, onun sırasındaki ikonu sol panele bas
            if (attachmentItems[i] == equippedItem)
            {
                mainCategoryIcon.sprite = attachmentIcons[i];
                break; // Bulduk, aramayı bırak
            }
        }
    }
}
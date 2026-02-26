using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttachmentCategory : MonoBehaviour
{
    [Header("Currently Attached to the Weapon (Permanent) Piece")]
    public GameObject equippedItem;

    [Header("Equip Button References")]
    public Button equipButton;
    public TextMeshProUGUI equipButtonText;

    [Header("Election Frames (In order)")]
    public GameObject[] attachmentItems; 
    public GameObject[] highlightImages; 

    [Header("Left Menu Icon Update (NEW)")]
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
        
        // Menü açıldığında veya oyun başladığında mevcut eşyayı sisteme bildir
        if (equippedItem != null)
        {
            PreviewItem(equippedItem);
        }
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
            
            // GÜNCELLEME BURADA YAPILDI:
            // Artık 'gameObject.name' (yani kategori ismi) de gönderiliyor.
            statsManager.CalculateAndLogStats(gameObject.name, eqStats, prevStats);
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

            if (statsManager != null)
            {
                AttachmentStats eqStats = equippedItem != null ? equippedItem.GetComponent<AttachmentStats>() : null;
                
                // GÜNCELLEME BURADA YAPILDI:
                // Kuşanma sırasında da kategori ismini gönderiyoruz ki hafızaya kaydetsin.
                statsManager.CalculateAndLogStats(gameObject.name, eqStats, eqStats); 
            }
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

    private void UpdateMainCategoryIcon()
    {
        if (mainCategoryIcon == null || attachmentIcons.Length == 0) return;

        for (int i = 0; i < attachmentItems.Length; i++)
        {
            if (attachmentItems[i] == equippedItem)
            {
                mainCategoryIcon.sprite = attachmentIcons[i];
                break; 
            }
        }
    }
}
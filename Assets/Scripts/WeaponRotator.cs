using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem; // Yeni sistemi buraya ekledik

public class WeaponRotator : MonoBehaviour
{
    [Header("Döndürülecek Silah")]
    public Transform weaponTarget; 
    
    [Header("Dönüş Hızı")]
    public float rotationSpeed = 0.5f; // Yeni sistem daha hassas olduğu için hızı düşürdük

    void Update()
    {
        // Farenin sol tuşuna basılı tutuluyorsa (YENİ SİSTEM KODU)
        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            // Fare imleci UI üzerinde değilse
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                // Farenin ekrandaki kayma miktarını (delta) al
                Vector2 mouseDelta = Mouse.current.delta.ReadValue();
                
                float rotX = mouseDelta.x * rotationSpeed;
                float rotY = mouseDelta.y * rotationSpeed; 

                // Silahı döndür
                weaponTarget.Rotate(Vector3.down, rotX, Space.World);
                weaponTarget.Rotate(Vector3.right, rotY, Space.World);
            }
        }
    }
}
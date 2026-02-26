using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem; 

public class WeaponRotator : MonoBehaviour
{
    [Header("The weapon to be rotated")]
    public Transform weaponTarget; 
    
    [Header("Rotation Speed")]
    public float rotationSpeed = 0.5f; 

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Vector2 mouseDelta = Mouse.current.delta.ReadValue();
                
                float rotX = mouseDelta.x * rotationSpeed;
                //float rotY = mouseDelta.y * rotationSpeed; 

                // Silahı döndür
                weaponTarget.Rotate(Vector3.down, rotX, Space.World);
                //weaponTarget.Rotate(Vector3.right, rotY, Space.World);
            }
        }
    }
}
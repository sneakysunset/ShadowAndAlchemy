using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponRotation : MonoBehaviour
{
    private Camera mainCamera;
    
    void Start()
    {
        mainCamera = Camera.main;
    }

    
    void Update()
    {
        LookAtMouse();
    }

    private void LookAtMouse()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        Vector3 lookDirection = mousePosition - transform.position;

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}

using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveVector;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        PlayerMovementVector();
        //LookAtMouse();
    }

    public void InputPlayer(InputAction.CallbackContext _context)
    {
        moveVector = _context.ReadValue<Vector2>();
    }

    private void PlayerMovementVector()
    {
        Vector2 movement = new Vector2(moveVector.x, moveVector.y);
        movement.Normalize();
        transform.Translate(moveSpeed * movement * Time.deltaTime);
    }

    private void LookAtMouse()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        Vector3 lookDirection = mousePosition - transform.position;
    }
}

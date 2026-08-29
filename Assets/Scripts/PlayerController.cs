using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    Rigidbody rb;
    Vector3 movementDirection;
    [SerializeField] Transform orientation;
    [SerializeField] float moveSpeed;
    Vector2 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movementDirection = orientation.forward * movementInput.y + orientation.right * movementInput.x;
        rb.AddForce(movementDirection * moveSpeed, ForceMode.Force);
    }

    public void move(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
}

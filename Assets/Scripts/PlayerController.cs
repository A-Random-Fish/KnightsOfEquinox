using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    
    
    [Header("Movement")]
    [SerializeField] Transform orientation;
    Vector2 movementInput;
    Vector3 movementDirection;

    [Header("Movement Variable")]
    [SerializeField] float moveSpeed;
    [SerializeField] float dodgeSpeed; 

    [Header("Cooldowns")]
    [SerializeField] float dodgeCooldown;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movementDirection = orientation.forward * movementInput.y + orientation.right * movementInput.x;
        rb.AddForce(movementDirection * moveSpeed, ForceMode.Force);
        dodgeCooldown -= Time.deltaTime;
    }

    public void Move(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void Dodge(InputAction.CallbackContext context)
    {
        if (context.performed && dodgeCooldown <= 0f)
        {
            rb.AddForce(movementDirection * dodgeSpeed, ForceMode.Impulse);
            dodgeCooldown = 1.5f;
        }
    }
}

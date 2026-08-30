using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    
    [Header("Movement")]
    [SerializeField] Transform orientation;
    Vector2 movementInput;
    Vector3 movementDirection;
    Vector2 dodgeInput = new Vector2(0f,1f);

    [Header("Movement Variable")]
    [SerializeField] float moveSpeed;
    [SerializeField] float dodgeSpeed; 
    [SerializeField] float dodgeLength;
    bool isDodging = false;

    [Header("Cooldowns")]
    [SerializeField] float dodgeCooldownLength;
    float dodgeCooldown;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        dodgeCooldown -= Time.deltaTime;

        if (movementInput != Vector2.zero)
        {
            dodgeInput = movementInput;
        }

        if (!isDodging)
        {
            rb.linearVelocity = new Vector3(movementInput.x * moveSpeed, rb.linearVelocity.y, movementInput.y * moveSpeed);
        }
        else 
        {
            rb.linearVelocity = new Vector3(dodgeInput.x * dodgeSpeed, rb.linearVelocity.y, dodgeInput.y * dodgeSpeed);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void Dodge(InputAction.CallbackContext context)
    {
        if (context.performed && dodgeCooldown <= 0f)
        {
            StartCoroutine("DodgeDuration");
            dodgeCooldown = dodgeCooldownLength;
        }
    }

    private IEnumerator DodgeDuration()
    {
        isDodging = true;
        yield return new WaitForSeconds(dodgeLength);
        isDodging = false;
    }
}

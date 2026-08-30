using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;

    [Header("Animation Variables")]
    [SerializeField] GameObject playerDisplay;
    [SerializeField] float playerRotSmoothing;
    Vector3 playerLookDirection;
    
    [Header("Movement")]
    [SerializeField] Transform orientation;
    Vector2 movementInput;
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
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        dodgeCooldown -= Time.deltaTime;

        if (movementInput != Vector2.zero && !isDodging)
        {
            dodgeInput = movementInput;
            playerLookDirection = new Vector3(movementInput.x, 0f, movementInput.y);
        }

        if (!isDodging)
        {
            rb.linearVelocity = new Vector3(movementInput.x * moveSpeed, rb.linearVelocity.y, movementInput.y * moveSpeed);
        }
        else 
        {
            rb.linearVelocity = new Vector3(dodgeInput.x * dodgeSpeed, rb.linearVelocity.y, dodgeInput.y * dodgeSpeed);
        }

        
        Quaternion targetRot = Quaternion.LookRotation(playerLookDirection);
        playerDisplay.transform.rotation = Quaternion.Lerp(playerDisplay.transform.rotation, targetRot, playerRotSmoothing * Time.deltaTime);

        //Animator Conditions
        anim.SetBool("Moving", movementInput != Vector2.zero);
    }

    public void Move(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void Dodge(InputAction.CallbackContext context)
    {
        if (context.performed && dodgeCooldown <= 0f)
        {
            anim.SetTrigger("Dodge");
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

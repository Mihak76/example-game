using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    Vector3 velocity;  
    bool isGrounded;

    // nastavitve za crouch
    public float crouchHeight = 1.0f;      
    public float standingHeight = 2.0f;    
    public float crouchSpeed = 2f;         
    public KeyCode crouchKey = KeyCode.LeftControl; 
    public float crouchTransitionSpeed = 6f; 
    bool isCrouching = false;
    float targetHeight;

    // sprint
    public float sprintSpeed = 8f;  // hitrost za sprint
    public KeyCode sprintKey = KeyCode.LeftShift;

    void Start()
    {
        targetHeight = standingHeight;
        controller.height = standingHeight;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // if za crouch
        if (Input.GetKeyDown(crouchKey))
        {
            isCrouching = !isCrouching;
            targetHeight = isCrouching ? crouchHeight : standingHeight;
        }

        controller.height = Mathf.Lerp(controller.height, targetHeight, Time.deltaTime * crouchTransitionSpeed);

        // hitrost kak hitro se premikas
        float currentSpeed = speed;

        if (isCrouching)
        {
            currentSpeed = crouchSpeed; // pomeni da med sprintom ne mores crouchati
        }
        else if (Input.GetKey(sprintKey)) // za sprint
        {
            currentSpeed = sprintSpeed; 
        }

        controller.Move(move * currentSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}

using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement instance;

    //player controller component
    public CharacterController controller;

    //player stats
    public float speed;             //Acceleration rate
    public float maxSpeed;         //Max speed
    public float sprintSpeed;       //Running speed
    public float walkSpeed;         //Walking speed
    public float airControl;        //A acceleration multiplier used to reduce player's speed in air
    public float gravity;           //Gravity
    public float jumpHeight;        //Jump Height
    public float friction;          //Friction, a speed multiplier, disabled when player is in air

    public Transform groundCheck;   //A game object used to check if the player is grounded
    public float groundDistance;    //The radius of ground checking sphere
    public LayerMask groundMask;    //Ground layer used for ground checking

    public Vector3 velocity_v;      //Vertical velocity
    public Vector3 velocity_h;      //Horizontal velocity
    public bool isGrounded;         //A flag that indicate if the player is grounded or not
    public bool isRunnning;
    public bool isDisabled;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //All intialization is done in the inspector
        maxSpeed = walkSpeed;
        isDisabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //If fall to the ground
        if(isGrounded && velocity_v.y < 0)
        {
            //Set velocity to a small negative number. not zero because we want to ensure player falls to the ground
            velocity_v.y = -2f;
        }

        float x = 0;
        float z = 0;

        if (!isDisabled)
        {
        //Get player movement input
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        }

        //Sprint control
        if(isGrounded && Input.GetKeyDown(KeyCode.LeftShift))
        {
            maxSpeed = sprintSpeed;
            isRunnning = true;
        }

        //If left-shift is lifted then player is no longer running - cannot reduce max speed right away because player might be in mid-air
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunnning = false;
        }

        //Slow down player if no long runnning
        if(isGrounded && !isRunnning && maxSpeed != walkSpeed)
        {
            maxSpeed = walkSpeed;
        }

        //Add horizontal velocity based on the input
        if (isGrounded)
        {
            velocity_h += (transform.right * x + transform.forward * z) * speed;
        }
        //Apply air control multiplier when in air
        else
        {
            velocity_h += (transform.right * x + transform.forward * z) * speed * airControl;
        }

        //Clamp velocity using max speed
        velocity_h = Vector3.ClampMagnitude(velocity_h, maxSpeed);

        //Move player horizontally
        controller.Move(velocity_h * Time.deltaTime);

        //Apply friction to horizontal velocity
        if (isGrounded)
        {
            velocity_h *= friction;
        }
        
        //Set horizontal velocity to zero if too small
        if(velocity_h.magnitude <= 0.3f)
        {
            velocity_h = Vector3.zero;
        }

        //Player can jump if on the ground
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //Calculate required velocity to reach jump height
            velocity_v.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Apply gravity
        velocity_v.y += gravity * Time.deltaTime;

        //Move player vertically
        controller.Move(velocity_v * Time.deltaTime);
    }
}

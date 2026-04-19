using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class FPSPlayer : MonoBehaviour
{
    [Header("Movement")]
    public Transform originalSpawn;
    public float walkSpeed = 5f;
    public float runSpeed = 9f;
    private float jumpForce = 5f;
    private bool isRunning;
    private bool jumpReady;

    //chosen spawn point
    private Vector3 newPos = new Vector3(-0.02f, 1.9f, -0.408f);
    private float health = 5f;

    [Header("Camera")]
    public Transform cameraTransform;
    public float lookSensitivity = 100f;
    private float yaw;
    private float pitch;
    public TextMeshProUGUI healthBar;
    public TextMeshProUGUI displayEnemiesLeft;
    public TextMeshProUGUI displayWaveNumber;
    private float enemiesLeft = 0;

    private Rigidbody rb;

    private Vector2 moveInput;
    private Vector2 lookInput;

    [Header("Grounding")]
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.5f;
    public float groundCheckDistance = 0.5f;
    public bool isGrounded;
    public Transform groundCheck;

    public GameObject enemySpawn;
    public int coinCount;
    int enemyAlive;
    int waveNum;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //freeze rotation?

        //optional lock cursor

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 

    }

    void Update()
    {
        CameraLook();
        GroundCheck();

        //reset spawn by hitting r
        if (Input.GetKeyDown(KeyCode.R))
        {
            //sets transform of player to new position that was assigned above
            originalSpawn.transform.position = newPos;
        }
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Enemy");
        int count = objectsWithTag.Length - 3;


        healthBar.text = "Health: " + health;

        //enemyAlive = enemySpawn.GetComponent<EnemySpawner>().GetEnemiesAlive();
        //waveNum = enemySpawn.GetComponent<EnemySpawner>().GetWaveNumber();
        displayEnemiesLeft.text = "Enemies Left: " + count;
        displayWaveNumber.text = "Current Wave: " + waveNum;


        if (health <= 0)
        {
            //move to death screen
            LoadScene("DeathScene");
        }
    }

    public void TakeDamage()
    {
        health--;
    }

    private void LoadScene(string sceneName)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin"))
        {
            coinCount++;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            health--;
        }
    }

    private void FixedUpdate()
    {
        float currentSpeed;
        //checking state of our variable & updating current speed based on bool
        if(isRunning)
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = walkSpeed; 
        }

        Vector3 move = transform.forward * moveInput.y * currentSpeed + 
            transform.right * moveInput.x * currentSpeed;

        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);   

        //come back l8r to do jump
        if(jumpReady && isGrounded)
        {
            jumpReady = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }

    void CameraLook()
    {
        //if there is no camera, exit function
        if (cameraTransform == null) return;

        //mouse scaled by sensitivity & frame time
        float mouseX = lookInput.x * lookSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * lookSensitivity * Time.deltaTime;
        
        // L/R
        yaw += mouseX;
        transform.rotation = Quaternion.Euler(0, yaw, 0);

        // U/D
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90, 90);

        cameraTransform.localRotation = Quaternion.Euler(pitch, 0, 0);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //taking WASD or Arrow input & saving it inside this variable & passing into movement code
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook (InputAction.CallbackContext context)
    {
        //taking our input from mouse & storing it inside variable which gets passed into CameraLook
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) jumpReady = true;
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        isRunning = context.ReadValueAsButton();
    }

    private void GroundCheck()
    {
        //if there is no groundCheck, turn to false & exit
        if(groundCheck == null)
        {
            isGrounded = false;
            return;
        }
        
        //inside of this sphere it will check ground check position, radius, distance & the layer the player is on
        //then it will change it true or false
        isGrounded = Physics.SphereCast(groundCheck.position, groundCheckRadius, Vector3.down, 
            out RaycastHit hit, groundCheckDistance, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        //visualize end postion of Spherecast
        Vector3 end = groundCheck.position + Vector3.down * groundCheckDistance;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(end, groundCheckRadius);
    }

}

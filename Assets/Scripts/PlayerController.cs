using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpPower = 3.0f;

    private InputManager inputManager;

    private Vector3 playerVelocity;

    private bool isGrounded = true;

    private Transform cameraTransform;
    public Transform playerBody;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        RotatePlayerCam();
        Movement();
    }

    void Movement()
    {

        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x * playerSpeed, rb.velocity.y, movement.y * playerSpeed);
        rb.velocity = transform.TransformDirection(move);

    }

    void Jump()
    {
        if (inputManager.PlayerJumpThisFrame() && isGrounded == true)
        {
            rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void RotatePlayerCam()
    {
        playerBody.transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}

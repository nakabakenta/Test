using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("�ړ��ݒ�")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("���_�ݒ�")]
    public Transform cameraTransform;
    public float mouseSensitivity = 2f;

    [Header("���ːݒ�")]
    public GameObject[] ballPrefabs; // 4�̃{�[���v���n�u
    public Transform shootPoint;
    public float shootForce = 20f;

    private Rigidbody rb;
    private int selectedBallIndex = 0;
    private float verticalRotation = 0f;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        LookAround();
        Move();
        HandleJump();
        HandleShoot();
        HandleBallSelection();
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDir = transform.right * h + transform.forward * v;
        Vector3 velocity = new Vector3(moveDir.x * moveSpeed, rb.linearVelocity.y, moveDir.z * moveSpeed);
        rb.linearVelocity = velocity;
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void HandleShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject ball = Instantiate(ballPrefabs[selectedBallIndex], shootPoint.position, Quaternion.identity);
            Rigidbody ballRb = ball.GetComponent<Rigidbody>();
            ballRb.AddForce(cameraTransform.forward * shootForce, ForceMode.Impulse);
        }
    }

    void HandleBallSelection()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            selectedBallIndex = (selectedBallIndex + 1) % ballPrefabs.Length;
        }
        else if (scroll < 0f)
        {
            selectedBallIndex = (selectedBallIndex - 1 + ballPrefabs.Length) % ballPrefabs.Length;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}

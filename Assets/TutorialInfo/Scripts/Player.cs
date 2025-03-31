using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float jumpForce = 5f;
    private float turnSmoothVelocity;
    private Vector3 velocity;
    private bool isGrounded;

    public Vector3 cameraOffset = new Vector3(0, 3, -5);
    public float cameraSmoothSpeed = 0.1f;

    public float mouseSensitivity = 100f; // حساسیت موس
    private float yaw = 0f;
    private float pitch = 0f;
    public float maxPitch = 80f; // محدودیت چرخش دوربین در محور عمودی

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void LateUpdate()
    {
        if (cam != null)
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, -maxPitch, maxPitch);

            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
            Vector3 desiredPosition = transform.position - (rotation * Vector3.forward * cameraOffset.magnitude);
            cam.position = Vector3.Lerp(cam.position, desiredPosition, cameraSmoothSpeed);
            cam.LookAt(transform.position + Vector3.up * 1.5f);
        }
    }
}

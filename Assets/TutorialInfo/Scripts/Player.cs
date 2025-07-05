using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator anim;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float jumpForce = 5f;
    private float turnSmoothVelocity;
    private Vector3 velocity;
    private bool isGrounded;

    public Vector3 cameraOffset = new Vector3(0, 3, -5);
    public float cameraSmoothSpeed = 0.1f;

    public float mouseSensitivity = 100f;
    private float yaw = 0f;
    private float pitch = 0f;
    public float maxPitch = 80f;

    public float maxHealth = 100f;
    public float currentHealth;
    public bool isDead = false;

    // Cooldown بین دو ضربه برای انیمیشن hit
    private float hitCooldown = 0.5f;
    private float lastHitTime = -1f;

    void Start()
    {
        currentHealth = maxHealth;

        if (anim == null)
            anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead || controller == null || !controller.enabled) return;

        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        bool isMoving = direction.magnitude >= 0.1f;
        anim.SetBool("run", isMoving);

        if (isMoving)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            anim.SetTrigger("Attack");
            PerformAttack();
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
            anim.SetBool("jump", true);
        }
        else
        {
            anim.SetBool("jump", false);
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void LateUpdate()
    {
        if (cam != null && !isDead)
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

    public void Heal(float amount)
    {
        if (isDead) return;
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        Debug.Log("Player healed! Current health: " + currentHealth);
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log("Player took damage! Current health: " + currentHealth);

        // ✅ پخش انیمیشن ضربه خوردن
        if (anim != null && Time.time - lastHitTime > hitCooldown)
        {
            anim.SetTrigger("hit");
            lastHitTime = Time.time;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Player DIED!");

        if (anim != null)
            anim.SetBool("isDead", true);

        if (controller != null)
            controller.enabled = false;
    }

    private void PerformAttack()
    {
        float attackRange = 2f;
        float attackDamage = 20f;

        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * 1.0f;

        if (Physics.Raycast(rayOrigin, transform.forward, out hit, attackRange))
        {
            EnemyAI enemy = hit.collider.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
                Debug.Log("Enemy hit by player!");
            }
        }

        Debug.DrawRay(rayOrigin, transform.forward * attackRange, Color.red, 0.5f);
    }
}

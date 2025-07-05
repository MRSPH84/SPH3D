using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackRange = 1.5f;
    public float damage = 10f;
    public float attackCooldown = 1f;

    public float maxHealth = 50f;
    private float currentHealth;
    private bool isDead = false;

    private Transform player;
    private float lastAttackTime = 0f;

    private Animator anim;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogWarning("EnemyAI: Animator not found!");
        }

        if (player == null)
        {
            Debug.LogWarning("EnemyAI: Player not found! Make sure your player has the tag 'Player'.");
        }
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            float moveStep = moveSpeed * Time.deltaTime;

            if (distance - moveStep > attackRange)
            {
                transform.position += direction * moveStep;

                if (anim != null)
                    anim.SetBool("isMoving", true);
            }
            else
            {
                if (anim != null)
                    anim.SetBool("isMoving", false);
            }

            Vector3 lookDir = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.LookAt(lookDir);
        }
        else
        {
            if (anim != null)
                anim.SetBool("isMoving", false);

            if (Time.time - lastAttackTime > attackCooldown)
            {
                Player playerScript = player.GetComponent<Player>();
                if (playerScript != null)
                {
                    playerScript.TakeDamage(damage);
                    lastAttackTime = Time.time;

                    if (anim != null)
                        anim.SetTrigger("attack");
                }
            }
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log("Enemy took damage! Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Enemy died!");

        if (anim != null)
            anim.SetTrigger("die");

        GetComponent<Collider>().enabled = false;

        // شروع حذف با تأخیر
        StartCoroutine(DestroyAfterDelay(3f)); // ۳ ثانیه بعد حذف می‌شه
    }

    private System.Collections.IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}

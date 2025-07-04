using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackRange = 1.5f;
    public float damage = 10f;
    public float attackCooldown = 1f;

    private Transform player;
    private float lastAttackTime = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogWarning("EnemyAI: Player not found! Make sure your player has the tag 'Player'.");
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            // فقط تا فاصله attackRange جلو می‌ره، نه بیشتر!
            Vector3 direction = (player.position - transform.position).normalized;
            float moveStep = moveSpeed * Time.deltaTime;

            // بررسی کنیم آیا حرکت بعدی باعث عبور از فاصله مجاز می‌شه یا نه
            if (distance - moveStep > attackRange)
            {
                transform.position += direction * moveStep;
            }

            // چرخش به سمت پلیر
            Vector3 lookDir = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.LookAt(lookDir);
        }
        else
        {
            // حمله
            if (Time.time - lastAttackTime > attackCooldown)
            {
                Player playerScript = player.GetComponent<Player>();
                if (playerScript != null)
                {
                    playerScript.TakeDamage(damage);
                    lastAttackTime = Time.time;
                }
            }
        }
    }
}

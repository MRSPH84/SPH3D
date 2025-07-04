using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healAmount = 25f;

    void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null && !player.isDead)
        {
            player.Heal(healAmount);   // جون اضافه کن
            Destroy(gameObject);       // آیتم رو حذف کن
        }
    }
}

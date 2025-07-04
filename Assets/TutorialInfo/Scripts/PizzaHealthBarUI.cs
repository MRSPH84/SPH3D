using UnityEngine;
using UnityEngine.UI;

public class PizzaHealthBarUI : MonoBehaviour
{
    public Player player;          // رفرنس پلیر
    public Image healthBarImage;   // Image که نوار جون رو نمایش میده

    void Update()
    {
        if (player == null || healthBarImage == null) return;

        float fillAmount = player.currentHealth / player.maxHealth;
        healthBarImage.fillAmount = fillAmount;
    }
}

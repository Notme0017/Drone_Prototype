using UnityEngine;
using UnityEngine.UI;
public class Drone_Health_Bar : MonoBehaviour
{
    const float MAX_HEALTH = 100;

    [SerializeField]
    private Image healthImage;

    [SerializeField]
    private Drone_Health Drone_Health;

    [Range(0, 100)]
    public float health = MAX_HEALTH;

    private void Start()
    {
        if(healthImage == null)
        {
            Debug.Log("No health image attached");
        }
    }

    private void Update()
    {
        if (health > 50) healthImage.color = Color.green;
        if (health <= 50 && health >= 30) healthImage.color = Color.yellow;
        if (health <30) healthImage.color = Color.red;

        if (health < 0) health = 0;

        healthImage.fillAmount = (health / MAX_HEALTH);
    }

    public void SetHealth(int currentHealth)
    {
        health = currentHealth;
    }
}

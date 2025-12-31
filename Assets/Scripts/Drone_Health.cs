using UnityEngine;
using UnityEngine.UI;

public class Drone_Health : MonoBehaviour
{
    public DroneAudio droneAudio;

    [SerializeField]
    private float maxHealth = 100;

    [SerializeField]
    private RawImage screenFlash;

    [SerializeField]
    private Drone_Health_Bar Health_Bar;

    private float currentHealth;

    private float alpha;

    private bool isScreenFlashing;

    private void Start()
    {
        currentHealth = maxHealth;

        alpha = 0f;

        if(screenFlash == null)
        {
            screenFlash = GameObject.Find("ScreenFlash").GetComponent<RawImage>();
        }
        screenFlash.color = new Color(1f, 0f, 0f, alpha);
    }

    private void Update()
    {
        if (isScreenFlashing)
        {
            alpha -= Time.deltaTime;
            screenFlash.color = new Color(1f, 0f, 0f, alpha);
            if(alpha <= 0f)
            {
                alpha = 0f;
                isScreenFlashing = false;
            }
        }
    }

    public void OnHit(int damage)
    {
        droneAudio.PlayHit();
        currentHealth -= damage;
        Health_Bar.SetHealth((int)currentHealth);
        Debug.Log("Current Health: " + currentHealth);
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Player died");
           //Destroy(this.gameObject);
        }

        ScreenFlash();
    }

    private void ScreenFlash()
    {
        alpha = 0.8f;
        isScreenFlashing = true;
    }

}

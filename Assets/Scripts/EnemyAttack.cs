using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject drone;

    [SerializeField]
    private GameObject bulletPrefab;

    public bool canAttack;

    public Transform firePoint;

    [SerializeField]
    private float fireRate = .5f;

    private float timer;
    private void Start()
    {
        if(drone == null)
        {
            drone = GameObject.FindGameObjectWithTag("Drone");
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        timer = Mathf.Clamp(timer, 0f, fireRate + 0.2f);

        if(canAttack && timer >= fireRate)
        {
            ShootAtDrone();
            timer = 0f;
        }
    }

    private void ShootAtDrone()
    {
        GameObject.Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Bullet");
    }

}

using UnityEngine;
// Import the Input System to support modern Unity configurations
using UnityEngine.InputSystem;

public class WeaponShoot : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public float fireRate = 0.2f;
    [SerializeField] public float spreadIntensity = 0.05f;
    [SerializeField] public float bulletSpeed = 20f;

    [Header("Multi-Shot Settings")]
    [SerializeField] public bool ShotGunMode = false;
    [Min(1)][SerializeField] public int bulletsPerShot = 5;

    private float nextFireTime = 1f;

    void Update()
    {
        bool isMouseHeld = false;

        if (Input.GetMouseButton(0))
        {
            isMouseHeld = true;
        }

        if (isMouseHeld && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null) return;

        int projectilestoSpawn = ShotGunMode ? bulletsPerShot : 1;

        for (int i = 0; i < projectilestoSpawn; i++)
        {
            CreateBullet();
        }
    }


    void CreateBullet()
    {
        Vector3 fireDirection = transform.right;

        fireDirection.x += Random.Range(-spreadIntensity / 100f, spreadIntensity / 100f);
        fireDirection.y += Random.Range(-spreadIntensity / 100f, spreadIntensity / 100f);
        fireDirection.z += Random.Range(-spreadIntensity / 100f, spreadIntensity / 100f);

        GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation);

        if (bulletInstance.TryGetComponent<Projectile>(out Projectile projectile))
        {
            projectile.Launch(fireDirection.normalized, bulletSpeed);
        }
    }
}
using UnityEngine;
// Import the Input System to support modern Unity configurations
using UnityEngine.InputSystem; 

public class WeaponShoot : MonoBehaviour
{
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public float fireRate = 0.2f;
    [SerializeField] public float spreadIntensity = 0.05f;
    
    private float nextFireTime = 0f;

    void Update()
    {
        // Check input across both systems to guarantee 100% functionality
        bool isMouseHeld = false;

        // 1. Check the New Input System
        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            isMouseHeld = true;
        }
        // 2. Fallback to the Legacy Input System
        else if (Input.GetMouseButton(0))
        {
            isMouseHeld = true;
        }

        // Execute shooting based on input handling and rate-of-fire cooldown
        if (isMouseHeld && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null) return;

        Vector3 fireDirection = transform.forward;
        fireDirection.x += Random.Range(-spreadIntensity, spreadIntensity);
        fireDirection.y += Random.Range(-spreadIntensity, spreadIntensity);
        fireDirection.z += Random.Range(-spreadIntensity, spreadIntensity);

        GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation);

        if (bulletInstance.TryGetComponent<Projectile>(out Projectile projectile))
        {
            projectile.Launch(fireDirection);
        }
    }
}
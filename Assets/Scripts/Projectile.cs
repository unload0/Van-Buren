using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifespan = 1.25f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 direction, float bulletSpeed = 20f)
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        
        if (rb != null)
        {
            rb.useGravity = false; 
            
            Vector3 velocityVector = direction.normalized * bulletSpeed;
            rb.linearVelocity = velocityVector;
        }
        else
        {
            Debug.LogError("Error: The projectile is missing a Rigidbody component!");
        }

        Invoke(nameof(Deactivate), lifespan);
    }

    private void Deactivate()
    {
        Destroy(this.gameObject);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.useGravity = true;
        rb.linearVelocity -= rb.linearVelocity;
    }
}
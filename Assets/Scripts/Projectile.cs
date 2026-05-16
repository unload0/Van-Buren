using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifespan = 2f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 direction)
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        
        if (rb != null)
        {
            // Disable gravity temporarily so the bullet flies straight
            rb.useGravity = false; 
            
            Vector3 velocityVector = direction.normalized * speed;
            rb.linearVelocity = velocityVector; // For modern Unity versions
            
            Debug.Log("Projectile velocity applied successfully!");
        }
        else
        {
            Debug.LogError("Error: The projectile is missing a Rigidbody component!");
        }

        Invoke(nameof(Deactivate), lifespan);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
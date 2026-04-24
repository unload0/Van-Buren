using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }
    }

    public virtual void OnInteract()
    {
        // base method
    }
}
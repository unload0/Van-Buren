using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool hasInteracted = false;

    private void OnTriggerStay(Collider other)
    {
        if (hasInteracted == false)
        {
            if (other.CompareTag("Player") && Input.GetKey(KeyCode.E))
            {
                OnInteract();
            }
        }
    }

    public virtual void OnInteract()
    {
        hasInteracted = true;
    }
}
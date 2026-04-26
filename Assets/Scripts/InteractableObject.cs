using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool hasInteracted = false;

    private void OnCollisionStay(Collision other)
    {
        if (hasInteracted == false)
        {
            if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E))
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
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    public bool hasInteracted = false;

    public virtual void OnInteract()
    {
        hasInteracted = true;
    }

    public void OnTriggerStay(Collider other)
    {
        if (hasInteracted = false)
            if (other.tag == "Player")
            {
                if (Input.GetKey(KeyCode.E))
                    OnInteract();
            }
    }
}

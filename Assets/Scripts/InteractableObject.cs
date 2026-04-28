using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool hasInteracted = false;
    [SerializeField] public bool AutoSetColliderAsTrigger = true;

    private void Awake()
    {
        if (AutoSetColliderAsTrigger)
            GetComponent<Collider>().isTrigger = true;
    }

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
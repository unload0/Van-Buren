using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public bool InteractOnce = true;

    [HideInInspector] public bool hasInteracted = false;
    [SerializeField] public bool AutoSetColliderAsTrigger = true;

    public UnityEvent events;

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
        events.Invoke();
        if (InteractOnce)
            hasInteracted = true;
    }
}
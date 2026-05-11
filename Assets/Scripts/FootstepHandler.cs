using UnityEngine;

public class FootstepHandler : MonoBehaviour
{
    [SerializeField] private float rayDistance = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    private GameObject lastGround;

    public void PlayFootstep()
    {
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 
        out RaycastHit hit, rayDistance, groundLayer))
        {
            if(lastGround == hit.transform.gameObject) return;
            lastGround = hit.transform.gameObject;
            
            PhysicsMaterial surfaceMat = hit.collider.sharedMaterial;

            if (surfaceMat != null)
            {
                switch (surfaceMat.name)
                {
                    case "PM_Stone":
                        AudioManagerScript.Instance?.playSound("StepSound_Stone");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
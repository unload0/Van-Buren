using UnityEngine;

public class testInteractable : InteractableObject
{
    public override void OnInteract()
    {
        Debug.Log("Interaction Successful!");
    }
}
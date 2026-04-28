using UnityEngine;

public class DoorButton : InteractableObject
{
    [SerializeField]
    private BasicDoorScript door = null;

    public override void OnInteract()
    {
        door.ToggleDoor();
    }
}

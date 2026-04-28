using UnityEngine;

public class DoorButton : InteractableObject
{
    [SerializeField]
    private Animator myDoorAnim = null;

    [SerializeField]
    private string idleAnimName = "idle";
    [SerializeField]
    private string openAnimName = "BasicDoorOpen";
    [SerializeField]
    private string closeAnimName = "BasicDoorClose";

    public override void OnInteract()
    {
        AnimatorStateInfo stateInfo = myDoorAnim.GetCurrentAnimatorStateInfo(0);

        if(stateInfo.normalizedTime <= 1.0f) return;

        if(AudioManagerScript.Instance != null)
        {
            AudioManagerScript.Instance.playSound("");
        }

        if (stateInfo.IsName(closeAnimName) || stateInfo.IsName(idleAnimName))
        {
            myDoorAnim.PlayInFixedTime(openAnimName);
        }
        else if (stateInfo.IsName(openAnimName))
        {
            myDoorAnim.PlayInFixedTime(closeAnimName);
        }
    }
}

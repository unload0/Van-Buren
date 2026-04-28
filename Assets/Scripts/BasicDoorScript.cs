using UnityEngine;

public class BasicDoorScript : MonoBehaviour
{
    private Animator doorAnim;

    [SerializeField]
    private string idleAnimName = "idle";
    [SerializeField]
    private string openAnimName = "BasicDoorOpen";
    [SerializeField]
    private string closeAnimName = "BasicDoorClose";

    void Awake()
    {
        doorAnim = GetComponentInChildren<Animator>();
    }

    public void ToggleDoor()
    {
        AnimatorStateInfo stateInfo = doorAnim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.normalizedTime <= 1.0f) return;

        if (AudioManagerScript.Instance != null)
        {
            AudioManagerScript.Instance.playSound("");
        }

        if (stateInfo.IsName(closeAnimName) || stateInfo.IsName(idleAnimName))
        {
            doorAnim.PlayInFixedTime(openAnimName);
        }
        else if (stateInfo.IsName(openAnimName))
        {
            doorAnim.PlayInFixedTime(closeAnimName);
        }
    }
}

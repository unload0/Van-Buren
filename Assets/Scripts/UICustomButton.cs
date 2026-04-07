using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("UI/CustomButton")]
public class UICustomButton : Button
{
    //may not appear in the inspector
    [SerializeField]
    public bool PlaySoundEffect = true;

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (GameObject.FindFirstObjectByType<AudioManagerScript>() is { } audioManagerObject)
            if (audioManagerObject.GetComponent<AudioManagerScript>() is { } audioManager)
            {
                audioManager.playSound("UIButtonClick");
            }
    }
}

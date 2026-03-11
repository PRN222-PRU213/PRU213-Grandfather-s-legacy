using UnityEngine;

[CreateAssetMenu(menuName = "Story/Trigger")]
public class StoryTriggerData : ScriptableObject
{
    public string triggerID;
    public TriggerAction triggerAction;
    public Transform triggerObject;
}
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Story/Trigger")]
public class StoryTriggerData : ScriptableObject
{
    public string triggerID;
    [TextArea]
    public string editorNote;   // ghi chú nội bộ

    public List<StoryCondition> conditions;  // TẤT CẢ phải đúng
    public TriggerAction action;
    public string targetID;    // questID / npcID / areaID / flagKey...
    public string extraData;   // dialogueID nếu action là QueueNPCDialogue
}
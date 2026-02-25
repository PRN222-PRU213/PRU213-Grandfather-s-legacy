using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Story/NPC")]
public class NPCData : ScriptableObject
{
    public string npcID;
    public string displayName;
    public List<ConditionalDialogue> dialoguePool;
}

[System.Serializable]
public class ConditionalDialogue
{
    public DialogueData dialogue;
    public int priority;
    public bool oneTimeOnly;
    public List<StoryCondition> conditions;
    [TextArea]
    public string editorNote;
}
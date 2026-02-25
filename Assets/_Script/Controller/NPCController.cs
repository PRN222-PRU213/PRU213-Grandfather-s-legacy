using UnityEngine;

public class NPCController : MonoBehaviour
{
    // Gọi khi player nhấn interact (E, click...)

    void OnEnable()
    {
        StoryEvent.OnStartDialogue += Interact;
    }

    void OnDisable()
    {
        StoryEvent.OnStartDialogue -= Interact;
    }
    public void Interact(NPCData data, StoryDatabase database)
    {
        string dialogueID = NPCManager.Instance.GetNextDialogue(data.npcID, data);

        if (string.IsNullOrEmpty(dialogueID))
        {
            Debug.Log($"[NPC] {data.displayName} không có gì để nói.");
            return;
        }

        // Tìm DialogueData trong database
        DialogueData dialogue = database.allDialogues
                                        .Find(d => d.dialogueID == dialogueID);

        if (dialogue != null)
            DialogueManager.Instance.StartDialogue(dialogue);
    }

    // Hiện indicator "!" khi có dialogue mới
    // public bool HasNewDialogue()
    // {
    //     string next = NPCManager.Instance.GetNextDialogue(data.npcID, data);
    //     return !string.IsNullOrEmpty(next);
    // }
}

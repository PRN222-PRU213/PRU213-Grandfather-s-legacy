using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager Instance;

    // Queue dialogue chờ — StoryDirector gọi vào đây
    private Dictionary<string, Queue<string>> pendingDialogues
        = new Dictionary<string, Queue<string>>();

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // StoryDirector gọi khi trigger bắn
    public void QueueDialogue(string npcID, string dialogueID)
    {
        if (!pendingDialogues.ContainsKey(npcID))
            pendingDialogues[npcID] = new Queue<string>();

        pendingDialogues[npcID].Enqueue(dialogueID);
        Debug.Log($"[NPCManager] Queued {dialogueID} for {npcID}");
    }

    // NPCController gọi khi player interact
    public string GetNextDialogue(string npcID, NPCData data)
    {
        // Ưu tiên 1: dialogue được queue bởi StoryDirector
        if (pendingDialogues.ContainsKey(npcID)
            && pendingDialogues[npcID].Count > 0)
            return pendingDialogues[npcID].Dequeue();

        // Ưu tiên 2: tìm dialogue phù hợp nhất trong pool
        ConditionalDialogue best = null;

        foreach (var cd in data.dialoguePool)
        {
            // Bỏ qua oneTimeOnly đã xem
            if (cd.oneTimeOnly
                && SaveManager.Instance.WorldState
                              .GetFlag($"dialogue_seen_{cd.dialogue.dialogueID}"))
                continue;

            if (!StoryDirector.Instance.CheckConditions(cd.conditions))
                continue;

            if (best == null || cd.priority > best.priority)
                best = cd;
        }

        if (best == null) return null;

        // Đánh dấu đã xem nếu oneTimeOnly
        if (best.oneTimeOnly)
            SaveManager.Instance.WorldState
                       .SetFlag($"dialogue_seen_{best.dialogue.dialogueID}", true);

        return best.dialogue.dialogueID;
    }
}
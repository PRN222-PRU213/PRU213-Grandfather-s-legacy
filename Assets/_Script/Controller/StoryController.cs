// using UnityEngine;

// public class StoryDirector : MonoBehaviour
// {
//     public static StoryDirector Instance;

//     [SerializeField] private StoryDatabase database;
//     private WorldStateManager worldState;
//     private ProgressionData progression; // class của bạn

//     void Awake() => Instance = this;

//     // Gọi sau MỌI hành động của player
//     public void EvaluateStoryTriggers(string eventType, string eventData = "")
//     {
//         foreach (var trigger in database.allTriggers)
//         {
//             if (IsTriggerFired(trigger)) continue;          // đã kích hoạt rồi
//             if (!CheckConditions(trigger.conditions)) continue; // chưa đủ điều kiện

//             FireTrigger(trigger);
//         }
//     }

//     private bool CheckConditions(List<StoryCondition> conditions)
//     {
//         foreach (var condition in conditions)
//         {
//             switch (condition.type)
//             {
//                 case ConditionType.HasFlag:
//                     if (!worldState.GetFlag(condition.key)) return false;
//                     break;
//                 case ConditionType.ValueGreaterThan:
//                     if (worldState.GetValue(condition.key) <= condition.value) return false;
//                     break;
//                 case ConditionType.QuestCompleted:
//                     if (!progression.completedQuests.Contains(condition.key)) return false;
//                     break;
//                 case ConditionType.SpeciesDiscovered:
//                     if (!progression.discoveredSpecies.Contains(condition.key)) return false;
//                     break;
//                 case ConditionType.AreaUnlocked:
//                     if (!progression.unlockedAreas.Contains(condition.key)) return false;
//                     break;
//             }
//         }
//         return true;
//     }

//     private void FireTrigger(StoryTrigger trigger)
//     {
//         worldState.SetFlag($"trigger_fired_{trigger.id}", true);

//         switch (trigger.action)
//         {
//             case TriggerAction.StartQuest:
//                 QuestManager.Instance.TryStartQuest(trigger.targetID);
//                 break;
//             case TriggerAction.PlayDialogue:
//                 // NPC tự động có dialogue mới khi player quay lại
//                 NPCManager.Instance.QueueDialogueForNPC(trigger.targetID, trigger.dialogueID);
//                 break;
//             case TriggerAction.RevealMapArea:
//                 MapManager.Instance.RevealArea(trigger.targetID);
//                 break;
//             case TriggerAction.SpawnAnomalyEvent:
//                 WorldEventManager.Instance.SpawnEvent(trigger.targetID);
//                 break;
//             case TriggerAction.SetWorldFlag:
//                 worldState.SetFlag(trigger.targetID, true);
//                 break;
//         }
//     }
// }

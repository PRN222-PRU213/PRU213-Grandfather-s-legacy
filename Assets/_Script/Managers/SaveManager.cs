using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private string savePath => Application.persistentDataPath + "/save.json";

    // Runtime data
    public ProgressionData Progression { get; private set; }
    public WorldStateManager WorldState { get; private set; }

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load(); // tự động load khi khởi động
    }

    // ── SAVE ───────────────────────────────────────────
    public void Save()
    {
        // Ghi WorldState vào ProgressionData trước khi save
        Progression.worldStateSaveData = WorldState.ToSaveData();

        string json = JsonUtility.ToJson(Progression, prettyPrint: true);
        File.WriteAllText(savePath, json);
        Debug.Log($"[SaveManager] Saved to {savePath}");
    }

    // ── LOAD ───────────────────────────────────────────
    public void Load()
    {
        WorldState = new WorldStateManager();

        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            Progression = JsonUtility.FromJson<ProgressionData>(json);
            WorldState.FromSaveData(Progression.worldStateSaveData);
            Debug.Log("[SaveManager] Loaded existing save.");
        }
        else
        {
            Progression = new ProgressionData(); // game mới
            Debug.Log("[SaveManager] No save found, starting fresh.");
        }
    }

    // ── DELETE ─────────────────────────────────────────
    public void DeleteSave()
    {
        if (File.Exists(savePath))
            File.Delete(savePath);

        Progression = new ProgressionData();
        WorldState = new WorldStateManager();
        Debug.Log("[SaveManager] Save deleted.");
    }
}
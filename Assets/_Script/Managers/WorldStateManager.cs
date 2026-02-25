using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldStateManager
{
    private Dictionary<string, bool> flags = new Dictionary<string, bool>();
    private Dictionary<string, int> values = new Dictionary<string, int>();

    // ── FLAGS ──────────────────────────────────────────
    public void SetFlag(string key, bool value)
    {
        flags[key] = value;
        Debug.Log($"[WorldState] Flag: {key} = {value}");
    }

    public bool GetFlag(string key)
        => flags.ContainsKey(key) && flags[key];

    // ── VALUES ─────────────────────────────────────────
    public void SetValue(string key, int value)
        => values[key] = value;

    public int GetValue(string key)
        => values.ContainsKey(key) ? values[key] : 0;

    public void Increment(string key, int amount = 1)
    {
        values[key] = GetValue(key) + amount;
        Debug.Log($"[WorldState] Value: {key} = {values[key]}");
    }

    // ── SAVE/LOAD ──────────────────────────────────────
    public WorldStateSaveData ToSaveData()
    {
        var data = new WorldStateSaveData();
        foreach (var kv in flags)
        {
            data.flagKeys.Add(kv.Key);
            data.flagValues.Add(kv.Value);
        }
        foreach (var kv in values)
        {
            data.valueKeys.Add(kv.Key);
            data.valueValues.Add(kv.Value);
        }
        return data;
    }

    public void FromSaveData(WorldStateSaveData data)
    {
        flags.Clear();
        values.Clear();
        for (int i = 0; i < data.flagKeys.Count; i++)
            flags[data.flagKeys[i]] = data.flagValues[i];
        for (int i = 0; i < data.valueKeys.Count; i++)
            values[data.valueKeys[i]] = data.valueValues[i];
    }
}

// Cần class riêng vì Dictionary không serialize được
[System.Serializable]
public class WorldStateSaveData
{
    public List<string> flagKeys = new List<string>();
    public List<bool> flagValues = new List<bool>();
    public List<string> valueKeys = new List<string>();
    public List<int> valueValues = new List<int>();
}
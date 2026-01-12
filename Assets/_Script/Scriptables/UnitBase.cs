using System;
using UnityEngine;

public class UnitBase : ScriptableObject
{
    [SerializeField] private UnitData _unitData;
    public UnitData UnitData => _unitData;
    public String description;
    public Sprite icon;
}

[Serializable]
public class UnitData
{
    public string unitName;
    public int sale;
    public int hardLevel;
    public int rareLevel;
    public string time;
    public string area;
}
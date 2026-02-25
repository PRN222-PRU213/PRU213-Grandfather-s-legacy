using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogName
{
    public Sprite avatar;
    public string name;
}

[System.Serializable]
public class DialogLine
{
    public DialogName name;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogLine> DialogLines = new List<DialogLine>();
}

public class DialogData : MonoBehaviour
{
    public Dialogue dialogue;
}
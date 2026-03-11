using UnityEngine;

public class Billboard : BasePanel
{
    public Transform target;
    public float heightOffset = 2f;

    void LateUpdate()
    {
        Vector3 worldPos = target.position + Vector3.up * heightOffset;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        transform.position = screenPos;
    }
}
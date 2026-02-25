using UnityEngine;

public class ButtonUI : MonoBehaviour
{
    public void OnStartClick(string functionName)
    {
        switch (functionName)
        {
            case "Shop":
                // UIManager.Instance.OnOpenOtherInventory(new InventoryData());
                break;
            case "Talk":

                break;
            default:
                Debug.LogWarning("No function assigned for: " + functionName);
                break;
        }
    }
}

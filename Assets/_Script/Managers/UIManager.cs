using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("UI")]
    [SerializeField] private GameObject InventoryUI;
    [SerializeField] private GameObject FishingUI;
    [SerializeField] private GameObject StoreUI;
    [SerializeField] private GameObject TalkUI;

    public enum state
    {
        None,
        Inventory,
        Fishing,
        Sale,
        Talk
    }

    protected override void Awake()
    {
        base.Awake();
    }
    public void SetUI(state ui)
    {
        switch (ui)
        {
            case state.None:
                SetInventoryUI(false);
                SetFishingUI(false);
                SetSaleUI(false);
                SetTalkUI(false);
                break;
            case state.Inventory:
                SetInventoryUI(true);
                SetFishingUI(false);
                SetSaleUI(false);
                SetTalkUI(false);
                break;
            case state.Fishing:
                SetInventoryUI(true);
                SetFishingUI(true);
                SetSaleUI(false);
                SetTalkUI(false);
                break;
            case state.Sale:
                SetInventoryUI(false);
                SetFishingUI(false);
                SetSaleUI(true);
                SetTalkUI(false);
                break;
            case state.Talk:
                SetInventoryUI(false);
                SetFishingUI(false);
                SetSaleUI(false);
                SetTalkUI(true);
                break;
        }
    }

    public void SetInventoryUI(bool value)
    {
        InventoryUI.SetActive(value);
    }

    public void SetFishingUI(bool value)
    {
        FishingUI.SetActive(value);
    }

    public void SetSaleUI(bool value)
    {
        // SaleUI.SetActive(value);
    }

    public void SetTalkUI(bool value)
    {
        // TalkUI.SetActive(value);
    }
}

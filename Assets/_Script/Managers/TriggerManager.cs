using UnityEngine;

public class TriggerManager : Singleton<TriggerManager>
{
    [Header("UI")]
    [SerializeField] private EndingView endingView;
    [SerializeField] private Billboard billboard;

    [SerializeField] private Transform minh;
    [SerializeField] private Transform duy;
    [SerializeField] private Transform tuan;
    [SerializeField] private Transform huy;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        if (DataManager.Instance.WorldState.IsEmpty())
        {
            TryTrigger("notification_minh");
        }
    }

    public void TryTrigger(string triggerID)
    {
        switch (triggerID)
        {
            case "normal_ending":
                ShowEnding();
                break;
            case "notification_minh":
                ShowBillBoard(minh);
                break;
            case "notification_duy":
                ShowBillBoard(duy);
                break;
            case "notification_tuan":
                ShowBillBoard(tuan);
                break;
            case "notification_huy":
                ShowBillBoard(huy);
                break;
            case "notification_hide":
                HideBillBoard();
                break;
        }
    }

    void ShowBillBoard(Transform npc)
    {
        billboard.target = npc;
        billboard.Show();
    }

    void HideBillBoard()
    {
        billboard.Hide();
    }

    void ShowEnding()
    {
        endingView.Show();
        InputManager.Instance.EnableEnding();
    }
}

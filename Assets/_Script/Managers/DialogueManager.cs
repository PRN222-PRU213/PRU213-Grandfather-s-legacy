using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image portraitImage;
    // [SerializeField] private GameObject continuePrompt; // "Nhấn Space để tiếp"

    private DialogueData current;
    private int lineIndex;
    private bool isPlaying;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    void OnEnable()
    {
        InputEvent.OnRightClickPressed += OnNextLine;
    }

    void OnDisable()
    {
        InputEvent.OnRightClickPressed -= OnNextLine;
    }

    void OnNextLine()
    {
        if (!isPlaying) return;

        NextLine();
    }

    // ── BẮT ĐẦU DIALOGUE ──────────────────────────────
    public void StartDialogue(DialogueData data)
    {
        if (data == null || data.lines.Count == 0) return;

        current = data;
        lineIndex = 0;
        isPlaying = true;

        ShowLine();
    }

    // ── HIỂN THỊ DÒNG HIỆN TẠI ────────────────────────
    private void ShowLine()
    {
        var line = current.lines[lineIndex];

        speakerText.text = line.speakerName;
        dialogueText.text = line.text;

        if (portraitImage != null)
        {
            portraitImage.sprite = line.portrait;
            portraitImage.enabled = line.portrait != null;
        }

        // continuePrompt.SetActive(true);
    }

    // ── SANG DÒNG TIẾP THEO ────────────────────────────
    public void NextLine()
    {
        // Xử lý hành động của dòng vừa xong
        ProcessLineActions(current.lines[lineIndex]);

        lineIndex++;

        if (lineIndex >= current.lines.Count)
            EndDialogue();
        else
            ShowLine();
    }

    // ── XỬ LÝ FLAG / QUEST SAU MỖI DÒNG ──────────────
    private void ProcessLineActions(DialogueLine line)
    {
        if (!string.IsNullOrEmpty(line.setFlagAfter))
        {
            SaveManager.Instance.WorldState.SetFlag(line.setFlagAfter, true);
            StoryDirector.Instance.Evaluate(); // kiểm tra trigger mới
        }

        // if (!string.IsNullOrEmpty(line.triggerQuestID))
        //     QuestManager.Instance.TryStartQuest(line.triggerQuestID);
    }

    // ── KẾT THÚC DIALOGUE ─────────────────────────────
    private void EndDialogue()
    {
        isPlaying = false;
        current = null;
        lineIndex = 0;


        StoryDirector.Instance.Evaluate(); // đánh giá lại sau khi nói xong
        SaveManager.Instance.Save();
    }
}
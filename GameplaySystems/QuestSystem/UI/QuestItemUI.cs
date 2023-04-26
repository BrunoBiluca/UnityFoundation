using Assets.UnityFoundation.Systems.QuestSystem;
using TMPro;
using UnityEngine;

public class QuestItemUI : MonoBehaviour
{
    private TextMeshProUGUI title;
    private TextMeshProUGUI progress;
    public QuestStatus QuestStatus { get; private set; }

    private void Awake()
    {
        title = transform.Find("name").GetComponent<TextMeshProUGUI>();
        progress = transform.Find("progress").GetComponent<TextMeshProUGUI>();
    }

    public void Setup(QuestStatus status)
    {
        QuestStatus = status;
        title.text = status.Quest.Title;
        progress.text = status.Progress
            + " / "
            + status.Quest.TotalObjectives;
    }
}

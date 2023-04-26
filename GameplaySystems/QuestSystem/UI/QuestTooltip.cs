using System.Linq;
using TMPro;
using UnityEngine;
using UnityFoundation.UI;

public class QuestTooltip : AbstractPointerTooltip
{
    private QuestItemUI questItem;

    private TextMeshProUGUI questTitle;
    private TextMeshProUGUI questObjectives;
    private TextMeshProUGUI questRewards;

    protected override void SetupTooltipObject(GameObject tooltipObject)
    {
        questTitle = tooltipObject.transform
            .Find("title").GetComponent<TextMeshProUGUI>();

        questObjectives = tooltipObject.transform
            .Find("objectives_values").GetComponent<TextMeshProUGUI>();

        questRewards = tooltipObject.transform
            .Find("rewards_values").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        questItem = GetComponent<QuestItemUI>();
    }

    protected override void UpdateTooltip(GameObject tooltipGO)
    {
        questTitle.text = questItem.QuestStatus.Quest.Title;
        questObjectives.text = string.Join(
            "\n", 
            questItem.QuestStatus.ObjetivesStatus.Select(os => {
                var desc = os.Objective.Description;

                if(!os.Objective.IsRequired)
                    desc += " (Optional)";

                if(os.IsComplete)
                    desc = $"FINISH - {desc}";

                return desc;
            })
        );
        questRewards.text = questItem.QuestStatus.Quest.Rewards;
    }
}

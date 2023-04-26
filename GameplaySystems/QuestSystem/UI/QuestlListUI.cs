using UnityFoundation.Code;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.QuestSystem
{
    public class QuestlListUI : MonoBehaviour
    {
        [SerializeField] private QuestItemUI questItemPrefab;

        private List<QuestStatus> questStatuses;
        private Transform content;

        private void Awake()
        {
            content = transform.FindTransform("quest_scroll_view.viewport.content");
        }

        public void Display(List<QuestStatus> questStatuses)
        {
            this.questStatuses = questStatuses;

            TransformUtils.RemoveChildObjects(content);

            foreach(var status in questStatuses)
            {
                Instantiate(questItemPrefab, content)
                    .Setup(status);
            }
        }
    }
}
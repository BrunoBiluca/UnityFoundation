using System.Linq;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    class DragDialogueNodeEvent : DialogueEditorEvent
    {
        public DragDialogueNodeEvent(DialogueEditor editor) : base(editor) { }

        public override bool IsActive(Event current) =>
            current.type == EventType.MouseDrag
            && editor.draggingNode.IsPresent;

        public override void Handle()
        {
            var dialogueNode = editor.draggingNode.Get();
            var newPosition = editor.GetMousePosition() + editor.draggingOffset;

            if(editor.multiSelection)
            {
                var dialogueNodes = editor.SelectedDialogue
                                    .GetNextDialogueNodesRecursively(dialogueNode)
                                    .ToList();

                dialogueNodes
                    .ForEach(nextDialogueNode =>
                        editor.Actions
                            .Add(
                                ChangeDialogueNodeAction
                                    .Create(nextDialogueNode)
                                    .AddPosition(newPosition - dialogueNode.Position)
                            )
                        );
            }

            editor.Actions.Add(
                ChangeDialogueNodeAction
                    .Create(dialogueNode)
                    .SetPosition(newPosition)
            );
        }
    }
}

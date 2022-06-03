using Assets.UnityFoundation.DialogueSystem;
using UnityEditor;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    public class DialogueEditorContextMenu
    {
        private readonly DialogueEditor editor;
        private readonly DialogueRepository dialogueRepository;
        private readonly DialogueCsvHandler dialogueCsvHandler;

        public DialogueEditorContextMenu(
            DialogueEditor editor,
            DialogueRepository dialogueRepository,
            DialogueCsvHandler dialogueCsvHandler
        )
        {
            this.editor = editor;
            this.dialogueRepository = dialogueRepository;
            this.dialogueCsvHandler = dialogueCsvHandler;
        }

        public void Show()
        {
            var contextMenu = new GenericMenu();

            contextMenu.AddItem(
                new GUIContent("New node"),
                false,
                NewNodeContextMenu,
                Event.current.mousePosition
            );

            contextMenu.AddItem(
                new GUIContent("Import csv"),
                false,
                dialogueCsvHandler.ImportCSV
            );

            contextMenu.AddItem(
                new GUIContent("Export csv"),
                false,
                dialogueCsvHandler.ExportCSV
            );
            contextMenu.ShowAsContext();
        }

        private void NewNodeContextMenu(object mousePosition)
        {
            editor.Actions.Add(
                new AddDialogueNode(dialogueRepository, null)
                    .SetCreatePosition((Vector2)mousePosition + editor.ScrollviewPosition)
            );
        }

        public void ShowNodeContextMenu(DialogueNode node)
        {
            var contextMenu = new GenericMenu();

            contextMenu.AddItem(
                new GUIContent("Set as Start line"),
                false,
                (args) => dialogueRepository.UpdateStartDialogueNode(node),
                Event.current.mousePosition
            );

            contextMenu.ShowAsContext();
        }
    }
}
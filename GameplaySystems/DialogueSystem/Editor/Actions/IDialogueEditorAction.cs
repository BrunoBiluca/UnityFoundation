namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    public interface IDialogueEditorAction
    {
        public void SetDialogueEditor(DialogueEditor editor);
        public void Handle();
    }
}
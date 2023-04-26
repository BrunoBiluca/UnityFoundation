using Assets.UnityFoundation.DialogueSystem;
using Assets.UnityFoundation.Systems.DialogueSystem.Editor;
using UnityEditor;
using UnityEngine;

public class DialogueNodeComponent
{
    private readonly DialogueEditor editor;
    private readonly DialogueEditorFactory guiFactory;
    private readonly DialogueEditorStyles styles;
    private readonly DialogueRepository repository;
    private readonly DialogueNode node;

    public DialogueNodeComponent(
        DialogueEditor editor,
        DialogueEditorFactory guiFactory,
        DialogueEditorStyles styles,
        DialogueRepository repository,
        DialogueNode node
    )
    {
        this.editor = editor;
        this.guiFactory = guiFactory;
        this.styles = styles;
        this.repository = repository;
        this.node = node;
    }

    public void Render()
    {
        RenderConnections(node);

        GUILayout.BeginArea(node.Rect, styles.GetNodeStyle(node));

        EditorGUI.BeginChangeCheck();

        var newSpearker = EditorGUILayout.ObjectField(
            node.Spearker == null ? "Spearker" : node.Spearker.SpearkerName,
            node.Spearker,
            typeof(SpearkerSO),
            false
        ) as SpearkerSO;

        var newText = EditorGUILayout.TextArea(node.Text, GUILayout.ExpandHeight(true));

        if(EditorGUI.EndChangeCheck())
        {
            editor.Actions.Add(
                ChangeDialogueNodeAction.Create(node)
                    .SetText(newText)
                    .SetSpeaker(newSpearker)
            );
        }

        GUILayout.BeginHorizontal();

        guiFactory.Button("x", new RemoveDialogueNode(repository, node));

        RenderLinkingButtons(node);

        guiFactory.Button("+", new AddDialogueNode(repository, node));

        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

    private void RenderLinkingButtons(DialogueNode node)
    {
        if(!editor.LinkingNodes.IsLinkingNodeSet)
        {
            guiFactory.Button("link", () => editor.LinkingNodes.SetLinkingNode(node));
            return;
        }

        if(node.name == editor.LinkingNodes.LinkingNode.Get().name)
        {
            guiFactory.Button("cancel", () => editor.LinkingNodes.Clear());
            return;
        }

        if(editor.LinkingNodes.LinkingNode.Get().HasLink(node))
        {
            guiFactory.Button("link", LinkDialogueNodes.Create(node));
        }
        else
        {
            guiFactory.Button("unlink", UnlinkDialogueNodes.Create(node));
        }
    }

    private void RenderConnections(DialogueNode node)
    {
        foreach(var childNode in editor.SelectedDialogue.GetNextDialogueNodes(node))
        {
            if(node.Rect.yMax < childNode.Rect.yMin)
                guiFactory.RenderConnectionLineBottom(node.Rect, childNode.Rect);
            else
                guiFactory.RenderConnectionLineRight(node.Rect, childNode.Rect);
        }
    }
}

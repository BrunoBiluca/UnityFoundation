using Assets.UnityFoundation.Systems.DialogueSystem.Editor;
using UnityEngine;

public class ToolsAreaComponent
{
    public static Texture2D background;
    private readonly DialogueEditor editor;
    private readonly DialogueEditorFactory guiFactory;
    private readonly DialogueCsvHandler csvHandler;

    public ToolsAreaComponent(
        DialogueEditor editor,
        DialogueEditorFactory guiFactory,
        DialogueCsvHandler csvHandler
    )
    {
        if(background == null)
            background = Resources.Load<Texture2D>("tools_area_background");
        this.editor = editor;
        this.guiFactory = guiFactory;
        this.csvHandler = csvHandler;
    }

    public void Render()
    {
        Rect toolsAreaRect = new Rect(0, 0, 200, 60);
        GUILayout.BeginArea(toolsAreaRect, new GUIStyle() {
            border = new RectOffset(0, 0, 0, 10),
            padding = new RectOffset(20, 20, 10, 10),
            normal = new GUIStyleState() { background = background }
        });

        GUILayout.BeginHorizontal();

        guiFactory.Button("Import", csvHandler.ImportCSV);

        guiFactory.Button("Export", csvHandler.ExportCSV);

        GUILayout.EndHorizontal();

        GUILayout.Toggle(editor.multiSelection, "Multiselection");

        GUILayout.EndArea();
    }
}

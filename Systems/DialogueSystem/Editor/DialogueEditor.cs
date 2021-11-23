using Assets.UnityFoundation.Code.Common;
using Assets.UnityFoundation.DialogueSystem;
using Assets.UnityFoundation.Systems.DialogueSystem.Editor.Events;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    public class DialogueEditor : EditorWindow
    {
        private const string windowBaseName = "Dialogue Editor";

        [MenuItem("Window/Dialogue Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, $"{windowBaseName}");
            EditorApplication.quitting -= OnEditorQuit;
            EditorApplication.quitting += OnEditorQuit;
        }

        private static void OnEditorQuit()
        {
            AssetDatabase.SaveAssets();
        }

        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            if(EditorUtility.InstanceIDToObject(instanceID) is DialogueSO)
            {
                ShowEditorWindow();
                return true;
            }

            return false;
        }

        private DialogueEditorFactory guiFactory;
        public DialogueEditorContextMenu contextMenu;
        private DialogueEditorStyles styles;

        private DialogueRepository dialogueRepository;
        private DialogueCsvHandler dialogueCsvHandler;
        public DialogueSO SelectedDialogue => selectedDialogue;
        private DialogueSO selectedDialogue;

        public Vector2 draggingOffset;
        public Optional<DialogueNode> draggingNode = Optional<DialogueNode>.None();

        public Vector2 lastMousePosition;
        public bool multiSelection;

        public LinkingNodeContainer LinkingNodes { get; } = new LinkingNodeContainer();
        public Vector2 ScrollviewPosition { get; set; }
        public List<IDialogueEditorAction> Actions { get; } = new List<IDialogueEditorAction>();

        private void OnEnable()
        {
            guiFactory = new DialogueEditorFactory(this);

            Selection.selectionChanged += () => {
                if(Selection.activeObject is DialogueSO dialogue)
                {
                    selectedDialogue = dialogue;
                    Repaint();
                }
            };
        }

        private void OnGUI()
        {
            if(selectedDialogue == null)
            {
                EditorGUILayout.LabelField("No Dialogue selected.");
                return;
            }

            InitializeEditor();

            EditorOnGUI();
        }

        private void InitializeEditor()
        {
            titleContent = new GUIContent($"{windowBaseName} - {selectedDialogue.name}");
            dialogueRepository = new DialogueRepository(selectedDialogue);
            dialogueCsvHandler = new DialogueCsvHandler(selectedDialogue);
            contextMenu = new DialogueEditorContextMenu(
                this, dialogueRepository, dialogueCsvHandler
            );
            styles = new DialogueEditorStyles(selectedDialogue);
        }

        private void EditorOnGUI()
        {
            ProcessEvents();

            new DialogueNodeAreaComponent(this, guiFactory, styles, dialogueRepository)
                .Render();

            new ToolsAreaComponent(this, guiFactory, dialogueCsvHandler)
                .Render();

            ProcessActions();
        }

        private void ProcessEvents()
        {
            var editorEvents = new List<DialogueEditorEvent>() {
                new UpdateMousePositionEvent(this),
                new ToggleMultiSelectionEvent(this),
                new OpenContextMenuEvent(this),

                new OnSelectDialogueNodeEvent(this),
                new OnDeselectDialogueNodeEvent(this),

                new PriorityHandleEvent(this)
                    .AddEvent(new DragDialogueNodeEvent(this))
                    .AddEvent(new MoveCanvasEvent(this))
            };

            editorEvents.ForEach(editorEvent => {
                if(!editorEvent.IsActive(Event.current))
                    return;

                editorEvent.Handle();
            });
        }

        private void ProcessActions()
        {
            if(Actions.Count == 0) return;

            if(Event.current.type == EventType.Repaint)
            {
                Actions.ForEach(action => {
                    action.SetDialogueEditor(this);
                    action.Handle();
                });
                Actions.Clear();
            }
            GUI.changed = true;
        }
    }
}
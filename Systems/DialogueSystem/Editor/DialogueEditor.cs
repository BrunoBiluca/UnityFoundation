using Assets.UnityFoundation.Code;
using Assets.UnityFoundation.DialogueSystem;
using System.Collections.Generic;
using System.Linq;
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
        private DialogueEditorContextMenu contextMenu;
        private DialogueEditorStyles styles;

        private DialogueRepository dialogueRepository;
        private DialogueCsvHandler dialogueCsvHandler;
        private DialogueSO selectedDialogue;

        private Vector2 draggingOffset;
        private Optional<DialogueNode> draggingNode = Optional<DialogueNode>.None();

        private Vector2 lastMousePosition;
        private Texture2D backgroundTex;

        public LinkingNodeContainer LinkingNodes { get; } = new LinkingNodeContainer();
        public Vector2 ScrollviewPosition { get; private set; }
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

            backgroundTex = Resources.Load<Texture2D>("background");
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

            ScrollviewPosition = GUILayout.BeginScrollView(ScrollviewPosition);
            Vector2 scrollviewSize = selectedDialogue.GetViewSize();
            var canvas = GUILayoutUtility.GetRect(scrollviewSize.x + 400, scrollviewSize.y + 400);

            GUI.DrawTextureWithTexCoords(
                canvas,
                backgroundTex,
                new Rect(
                    0,
                    MathX.Remainder(canvas.height / backgroundTex.height),
                    canvas.width / backgroundTex.width,
                    canvas.height / backgroundTex.height
                )
            );

            foreach(var node in selectedDialogue.DialogueNodesValues)
            {
                RenderConnections(node);
                RenderDialogueNode(node);
            }
            GUILayout.EndScrollView();
            ProcessActions();
        }

        private void ProcessEvents()
        {
            if(Event.current.type == EventType.MouseDown)
            {
                var mousePosition = Event.current.mousePosition + ScrollviewPosition;
                draggingNode = GetDialogueNodeBy(mousePosition);
                draggingNode
                    .Some(dialogueNode => {
                        draggingOffset = dialogueNode.Rect.position - mousePosition;
                        Selection.activeObject = dialogueNode;
                    })
                    .OrElse(() =>
                        Selection.activeObject = selectedDialogue
                    );

                lastMousePosition = Event.current.mousePosition;
            }

            if(Event.current.type == EventType.MouseUp)
            {
                draggingNode = Optional<DialogueNode>.None();
                return;
            }

            if(draggingNode.IsPresent)
            {
                draggingNode.Some(dialogueNode => {
                    if(Event.current.type != EventType.MouseDrag)
                        return;

                    var mousePosition = Event.current.mousePosition + ScrollviewPosition;
                    Actions.Add(
                        ChangeDialogueNodeAction
                            .create(dialogueNode)
                            .SetPosition(mousePosition + draggingOffset)
                    );
                });
                return;
            }

            if(Event.current.type == EventType.MouseDrag)
            {
                ScrollviewPosition += lastMousePosition - Event.current.mousePosition;
                lastMousePosition = Event.current.mousePosition;
                GUI.changed = true;
            }

            if(Event.current.type == EventType.ContextClick)
            {
                var mousePosition = Event.current.mousePosition + ScrollviewPosition;
                GetDialogueNodeBy(mousePosition)
                    .Some(node => contextMenu.ShowNodeContextMenu(node))
                    .OrElse(() => contextMenu.Show());
            }
        }

        private Optional<DialogueNode> GetDialogueNodeBy(Vector2 mousePosition)
        {
            var node = selectedDialogue.DialogueNodesValues
                .LastOrDefault(node => node.Rect.Contains(mousePosition));

            if(node == null) return Optional<DialogueNode>.None();

            return Optional<DialogueNode>.Some(node);
        }

        private void RenderDialogueNode(DialogueNode node)
        {
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
                Actions.Add(
                    ChangeDialogueNodeAction.create(node)
                        .SetText(newText)
                        .SetSpeaker(newSpearker)
                );
            }

            GUILayout.BeginHorizontal();

            guiFactory.Button("x", new RemoveDialogueNode(dialogueRepository, node));

            RenderLinkingButtons(node);

            guiFactory.Button("+", new AddDialogueNode(dialogueRepository, node));

            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        private void RenderLinkingButtons(DialogueNode node)
        {
            if(!LinkingNodes.IsLinkingNodeSet)
            {
                guiFactory.Button("link", () => LinkingNodes.SetLinkingNode(node));
                return;
            }

            if(node.name == LinkingNodes.LinkingNode.Get().name)
            {
                guiFactory.Button("cancel", () => LinkingNodes.Clear());
                return;
            }

            if(LinkingNodes.LinkingNode.Get().HasLink(node))
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
            foreach(var childNode in selectedDialogue.GetNextDialogueNodes(node))
            {
                if(node.Rect.yMax < childNode.Rect.yMin)
                    guiFactory.RenderConnectionLineBottom(node.Rect, childNode.Rect);
                else
                    guiFactory.RenderConnectionLineRight(node.Rect, childNode.Rect);
            }
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
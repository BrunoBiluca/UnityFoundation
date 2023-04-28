using Assets.UnityFoundation.DialogueSystem;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    public class DialogueCsvHandler
    {
        private readonly DialogueSO dialogue;

        public DialogueCsvHandler(DialogueSO dialogue)
        {
            this.dialogue = dialogue;
        }

        public void ImportCSV()
        {
            string path = EditorUtility.OpenFilePanel("Overwrite with csv", "", "csv");

            Debug.Log($"Importing file: {path}");

            if(string.IsNullOrEmpty(path)) return;

            dialogue.Clear();
            foreach(var row in ReadCsv(path))
            {
                dialogue.name = row.DialogueName;

                var newDialogueNode = ScriptableObject.CreateInstance<DialogueNode>();

                newDialogueNode.name = row.DialogueNodeName;
                newDialogueNode.Text = row.Text;

                if(!string.IsNullOrEmpty(row.NextDialogues))
                {
                    newDialogueNode.NextDialogueNodes.AddRange(row.NextDialogues.Split('|'));
                }

                if(!string.IsNullOrEmpty(row.PreviousDialogues))
                {
                    newDialogueNode.PreviousDialogueNodes
                        .AddRange(row.PreviousDialogues.Split('|'));
                }

                string[] rectDimensions = row.Rect.Split('|');
                newDialogueNode.Rect = new Rect(
                    float.Parse(rectDimensions[0]),
                    float.Parse(rectDimensions[1]),
                    float.Parse(rectDimensions[2]),
                    float.Parse(rectDimensions[3])
                );

                dialogue.DialogueNodes.Add(newDialogueNode.name, newDialogueNode);

                var speaker = AssetDatabase.LoadAssetAtPath<SpearkerSO>($"{row.SpearkerName}");
                if(speaker != null)
                {
                    Debug.Log($"Imported speaker on path: {row.SpearkerName}");
                    newDialogueNode.Spearker = speaker;
                }
                else
                {
                    Debug.Log(
                        $"Failed import speaker: {row.SpearkerName}."
                        + $" Please put the SpeakerSO asset in the same folder."
                    );
                }
            }

            string assetPath = AssetDatabase.GetAssetPath(dialogue.GetInstanceID());
            AssetDatabase.RenameAsset(assetPath, dialogue.name);
            AssetDatabase.SaveAssets();
        }

        private IEnumerable<DialogueCsvRow> ReadCsv(string path)
        {
            using var reader = new StreamReader(path);
            //using var csv = new CsvReader(
            //    reader,
            //    new CsvConfiguration(CultureInfo.InvariantCulture) {
            //        MissingFieldFound = null
            //    }
            //);

            //csv.Read();
            //csv.ReadHeader();
            //while(csv.Read())
            //{
            //    yield return csv.GetRecord<DialogueCsvRow>();
            //}
            yield return null;
        }

        public void ExportCSV()
        {
            string exportFolderPath = Application.dataPath + "/Export/Dialogues/";

            if(!Directory.Exists(exportFolderPath))
            {
                Directory.CreateDirectory(exportFolderPath);
            }

            string filePath = exportFolderPath
                + $"{dialogue.name}"
                + $"_{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}"
                + $".csv";

            Debug.Log($"CSV exported to path: {filePath}");

            using var writer = new StreamWriter(filePath);
            //using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            //csv.WriteRecords(
            //    dialogue
            //        .DialogueNodesValues
            //        .Select(node => {
            //            var row = new DialogueCsvRow(dialogue, node);

            //            var spearkerPath = AssetDatabase.GetAssetPath(node.Spearker);
            //            if(spearkerPath != null)
            //            {
            //                Debug.Log($"Export speaker on path: {spearkerPath}");
            //                row.SpearkerName = spearkerPath;
            //            }
            //            else
            //            {
            //                Debug.Log($"Couldn't find speaker {row.SpearkerName} file");
            //            }

            //            return row;
            //        })
            //);
        }
    }

    class DialogueCsvRow
    {
        public string DialogueName { get; set; }
        public string DialogueNodeName { get; set; }
        public string SpearkerName { get; set; }
        public string Text { get; set; }
        public string NextDialogues { get; set; }
        public string PreviousDialogues { get; set; } = null;
        public string Rect { get; set; }

        public DialogueCsvRow() { }

        public DialogueCsvRow(DialogueSO dialogueSO, DialogueNode dialogueNode)
        {
            DialogueName = dialogueSO.name;
            DialogueNodeName = dialogueNode.name;
            SpearkerName = dialogueNode.Spearker?.name;
            Text = dialogueNode.Text;
            NextDialogues = string.Join("|", dialogueNode.NextDialogueNodes);
            PreviousDialogues = string.Join("|", dialogueNode.PreviousDialogueNodes);
            Rect = string.Join("|", new float[] {
            dialogueNode.Rect.position.x,
            dialogueNode.Rect.position.y,
            dialogueNode.Rect.width,
            dialogueNode.Rect.height
        });
        }
    }
}
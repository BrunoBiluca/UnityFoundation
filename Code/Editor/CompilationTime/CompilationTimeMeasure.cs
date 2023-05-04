using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace Assets.UnityFoundation.Editor.CompilationTime
{
    [ExecuteInEditMode]
    public class CompilationTimeMeasure : MonoBehaviour
    {
        private double compileStartTime;
        private bool isCompiling;

        private void OnEnable()
        {
            EditorApplication.update += EditorUpdate;
            CompilationPipeline.compilationStarted += CompilationStarted;
        }

        private void OnDisable()
        {
            EditorApplication.update -= EditorUpdate;
            CompilationPipeline.compilationStarted -= CompilationStarted;
        }

        private void EditorUpdate()
        {
            if(!isCompiling) return;


            if(!EditorApplication.isCompiling)
            {
                CompilationFinished();
            }
        }

        private void CompilationStarted(object obj)
        {
            Debug.Log("Compile started...");
            isCompiling = true;
            compileStartTime = EditorApplication.timeSinceStartup;
        }

        private void CompilationFinished()
        {
            var compileTime = EditorApplication.timeSinceStartup - compileStartTime;
            isCompiling = false;
            Debug.Log($"Compile finished in {compileTime:F2} seconds.");
        }

    }
}
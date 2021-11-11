using UnityEngine;
using UnityEditor;
using UnityEditor.Compilation;

namespace Assets.UnityFoundation.Editor.CompilationTime
{
    [InitializeOnLoad]
    public static class CompilationTimeStatic
    {
        private static bool active = false;
        private static object compilerObj;
        private static double compileStartTime;

        static CompilationTimeStatic()
        {
            if(!active) return;

            Debug.Log("Initialize compile watch...");

            CompilationPipeline.compilationStarted -= CompilationStarted;
            CompilationPipeline.compilationFinished -= CompilationFinished;

            CompilationPipeline.compilationStarted += CompilationStarted;
            CompilationPipeline.compilationFinished += CompilationFinished;
        }

        private static void CompilationStarted(object obj)
        {
            Debug.Log("Compile started...");
            compilerObj = obj;
            compileStartTime = EditorApplication.timeSinceStartup;
        }

        private static void CompilationFinished(object obj)
        {
            if(obj != compilerObj) 
                Debug.LogWarning("Different compiler contexts");

            var compileTime = EditorApplication.timeSinceStartup - compileStartTime;
            Debug.Log($"Compile finished in {compileTime:F2} seconds.");
        }
    }
}
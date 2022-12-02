using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;
using UnityEngine.SceneManagement;

namespace EditorStronglyTyped
{
    public static class Scenes
    {
        private static string OutputFileName() => "Scenes.cs";
        private static string FullQualifiedNamespace() => "StronglyTyped";

        [UnityEditor.Callbacks.DidReloadScripts]
        public static void Refresh()
        {
            string filePath = Helper.GetFilePath(OutputFileName());
            bool fileExists = !string.IsNullOrEmpty(filePath);

            if (!fileExists)
                filePath = Path.Combine(Application.dataPath, Helper.OutputFolder(), OutputFileName());

            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    string[] extraLines = new string[] { 
                        "\n// ATTENTION",
                        "// This can only get scenes added to the SceneManager",
                        "// You can do that by following",
                        "// File > Build Settings:",
                        "// >>  Add open Scene",
                        "// >> Or drag and drop a Scene into the list",
                        "// More Info at",
                        "// https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.html",
                        "// https://docs.unity3d.com/Manual/BuildSettings.html\n",
                    };
                    Helper.WriteFileIntro(MethodBase.GetCurrentMethod(), streamWriter, extraLines);
                    Helper.WriteFileHeader(streamWriter, FullQualifiedNamespace(), nameof(Scenes));

                    foreach (var scene in EditorBuildSettings.scenes)
                    {
                        string sceneName = Path.GetFileNameWithoutExtension(scene.path);
                        Helper.WriteFileLine(streamWriter, sceneName);
                        if (SceneUtility.GetBuildIndexByScenePath(scene.path) >= 0)
                            Helper.WriteFileIntLine(streamWriter, $"{sceneName}Int", SceneUtility.GetBuildIndexByScenePath(scene.path));
                    }

                    Helper.WriteFileFooter(streamWriter);
                }

                bool areFilesDifferent = true;

                if (fileExists)
                {
                    byte[] existingFileContent = File.ReadAllBytes(filePath);
                    byte[] newFileContent = memoryStream.ToArray();

                    Hash128 existingFileHash = Hash128.Compute(existingFileContent);
                    Hash128 newFileHash = Hash128.Compute(newFileContent);

                    areFilesDifferent = existingFileHash != newFileHash;
                }

                if (areFilesDifferent)
                {
                    File.WriteAllBytes(filePath, memoryStream.ToArray());
                    AssetDatabase.Refresh();
                }
            }
        }
        public static void Delete()
        {
            string filePath = Helper.GetFilePath(OutputFileName());
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                AssetDatabase.Refresh();
            }
        }
        public static void DeleteAndRefresh()
        {
            Delete();
            Refresh();
        }
    }

}



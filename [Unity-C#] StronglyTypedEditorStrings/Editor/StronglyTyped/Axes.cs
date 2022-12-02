using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;
using System.Linq;

namespace EditorStronglyTyped
{
    public static class Axes
    {
        private static string OutputFileName() => "Axes.cs";
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
                    Helper.WriteFileIntro(MethodBase.GetCurrentMethod(), streamWriter);
                    Helper.WriteFileHeader(streamWriter, FullQualifiedNamespace(), nameof(Axes));

                    var inputManager = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset").FirstOrDefault();
                    IList<string> axes = new List<string>();
                    if (inputManager != null)
                    {
                        var serializedData = new SerializedObject(inputManager);
                        foreach(SerializedProperty axe in serializedData.FindProperty("m_Axes"))
                        {
                            var name = axe.FindPropertyRelative("m_Name").stringValue;
                            if (!axes.Contains(name))
                                axes.Add(name);
                        }

                        foreach (string axe in axes)
                        {
                            Helper.WriteFileLine(streamWriter, axe);
                        }
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



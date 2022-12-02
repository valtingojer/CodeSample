using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;

namespace EditorStronglyTyped
{
    public class Tags
    {
        private static string OutputFileName() => "Tags.cs";
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
                    Helper.WriteFileHeader(streamWriter, FullQualifiedNamespace(), nameof(Tags));

                    foreach (var tag in UnityEditorInternal.InternalEditorUtility.tags)
                        Helper.WriteFileLine(streamWriter, tag);

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



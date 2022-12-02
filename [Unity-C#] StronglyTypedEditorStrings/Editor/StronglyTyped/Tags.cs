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
                    Helper.WriteFileNamespace(streamWriter, FullQualifiedNamespace(), nameof(Tags));
                    Helper.WriteFileClass(streamWriter, nameof(Tags));

                    foreach (var tag in UnityEditorInternal.InternalEditorUtility.tags)
                        Helper.WriteFilePropertyLine(streamWriter, tag);

                    Helper.WriteCloseFileClass(streamWriter);

                    Helper.WriteFileEnum(streamWriter, $"{nameof(Tags)}Enum");
                    for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.tags.Length; i++)
                        Helper.WriteFileEnumLine(streamWriter, UnityEditorInternal.InternalEditorUtility.tags[i], i);
                    Helper.WriteCloseFileEnum(streamWriter);



                    Helper.WriteCloseFileNamespace(streamWriter);
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



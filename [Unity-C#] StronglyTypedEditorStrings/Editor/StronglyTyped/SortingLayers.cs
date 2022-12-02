using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;

namespace EditorStronglyTyped
{
    public static class SortingLayers
    {
        private static string OutputFileName() => "SortingLayers.cs";
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
                    Helper.WriteFileNamespace(streamWriter, FullQualifiedNamespace(), nameof(SortingLayers));
                    Helper.WriteFileClass(streamWriter, nameof(SortingLayers));

                    foreach (var sortingLayer in SortingLayer.layers)
                    {
                        Helper.WriteFilePropertyLine(streamWriter, sortingLayer.name);
                        Helper.WriteFileIntLine(streamWriter, $"{sortingLayer.name}Int", sortingLayer.id);
                    }
                        

                    Helper.WriteCloseFileClass(streamWriter);

                    Helper.WriteFileEnum(streamWriter, $"{nameof(SortingLayers)}Enum");
                    foreach (var sortingLayer in SortingLayer.layers)
                    {
                        Helper.WriteFileEnumLine(streamWriter, sortingLayer.name, sortingLayer.id);
                    }
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



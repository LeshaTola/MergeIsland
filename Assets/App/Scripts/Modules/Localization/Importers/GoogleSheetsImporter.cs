#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace App.Scripts.Modules.Localization.Importers
{
    public class GoogleSheetsImporter : OdinEditorWindow
    {
        [SerializeField] private string _savePath;

        [HorizontalGroup]
        [SerializeField] private string _tableId;

        [HorizontalGroup]
        [SerializeField] private string _listId;

        private string _fullPath;

        private void OnValidate()
        {
            _fullPath = Application.dataPath + _savePath;
        }

        [Button]
        private async UniTaskVoid Dowload()
        {
            var url = $"https://docs.google.com/spreadsheets/d/{_tableId}/export?format=csv&gid={_listId}";
            var request = UnityWebRequest.Get(url);

            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                await File.WriteAllTextAsync(_fullPath, request.downloadHandler.text);
                AssetDatabase.Refresh();
                Debug.Log($"Файл сохранён по пути: {Application.dataPath + _savePath}");
            }
            else
            {
                Debug.LogError($"Ошибка при загрузке файла: {request.error}");
            }
        }

        [Button]
        private void SeparateOnDifferentFiles()
        {
            var localizationFiles
                = SeparateLocalizationFile(File.ReadAllText(Application.dataPath + _savePath));

            var directory = Path.GetDirectoryName(_fullPath);
            foreach (var localizationFile in localizationFiles)
            {
                File.WriteAllText($"{directory}\\{CleanFileName(localizationFile.Key)}.txt", localizationFile.Value);
            }

            AssetDatabase.Refresh();
        }

        public string CleanFileName(string fileName)
        {
            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (var invalidChar in invalidChars)
            {
                fileName = fileName.Replace(invalidChar.ToString(), string.Empty);
            }

            return fileName;
        }

        private Dictionary<string, string> SeparateLocalizationFile(string fullLocalizationFile)
        {
            var lines = fullLocalizationFile.Split('\n');
            var regex = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

            var localizations = regex.Split(lines[0]);

            Dictionary<string, string> localizationFiles = new(localizations.Length);
            for (int i = 1; i < localizations.Length; i++)
            {
                var localization = string.Empty;
                foreach (var line in lines)
                {
                    var fields = regex.Split(line);
                    localization += $"{fields[0]},{fields[i]}\n";
                }

                localization = localization.TrimEnd();
                localizationFiles.Add(localizations[i], localization);
            }

            return localizationFiles;
        }

        [MenuItem("Tools/Google Sheets Importer")]
        private static void OpenWindow()
        {
            GetWindow<GoogleSheetsImporter>().Show();
        }
    }
}
#endif
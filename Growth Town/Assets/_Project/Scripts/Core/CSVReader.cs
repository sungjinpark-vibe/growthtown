using System.Collections.Generic;
using UnityEngine;

using System.Text.RegularExpressions;

namespace LifeTown.Core
{
    public class CSVReader : MonoBehaviour
    {
        [SerializeField] private TextAsset csvFile;

        
        public void ReadCSV()
        {
            if (csvFile == null)
            {
                Debug.LogError("CSV File is not assigned!");
                return;
            }

            var data = Read(csvFile.text);
            Debug.Log($"Successfully parsed {data.Count} rows from {csvFile.name}.");
        }

        public static List<Dictionary<string, string>> Read(string fileText)
        {
            var list = new List<Dictionary<string, string>>();
            string[] lines = Regex.Split(fileText, "\n|\r|\r\n");

            if (lines.Length <= 1) return list;

            string[] headers = Regex.Split(lines[0], ",");

            for (int i = 1; i < lines.Length; i++)
            {
                var values = Regex.Split(lines[i], ",");
                if (values.Length == 0 || values[0] == "") continue;

                var entry = new Dictionary<string, string>();
                for (int j = 0; j < headers.Length && j < values.Length; j++)
                {
                    entry[headers[j]] = values[j];
                }
                list.Add(entry);
            }
            return list;
        }
    }
}


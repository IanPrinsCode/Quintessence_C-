using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using static DamData.Headers;

namespace DamData
{
    class DataReader
    {
        public static Node _rootNode;
        private string _path;

        public DataReader(string filePath)
        {
            _path = filePath;
        }

        private Dam[] CreateDamObjects(ExcelFile workbook)
        {
            Dam[] damArray = new Dam[GetNames(workbook).Length];
            string[] names = GetNames(workbook);
            Dictionary<string, Dictionary<Enum, double?>>[] dataSets = GetData(workbook);

            for (int i = 0; i < names.Length; i++)
            {
                damArray[i] = new Dam(names[i], dataSets[i]);
            }
            return damArray;
        }

        private double? GetCurrentValue(string currentString)
        {
            string current;

            current = currentString.Split(',', 2)[0].Replace('.', ',');
            if (!current.Equals(""))
                return Double.Parse(current);
            return null;
        }

        private string[] GetNames(ExcelFile workbook)
        {
            string damNames = workbook.Worksheets[0].Rows[2].AllocatedCells[0].Value.ToString();
            string[] namesList = damNames.Trim(',').Split(",,,,");

            return namesList;
        }

        private Dictionary<string, Dictionary<Enum, double?>>[] GetData(ExcelFile workbook)
        {
            var dataArray = new Dictionary<string, Dictionary<Enum, double?>>[GetNames(workbook).Length];
            Dictionary<Enum, double?> headersDict;
            double? currentValue;
            string currentString;
            string date;

            for (int i = 0; i < dataArray.Length; i++)
            {
                dataArray[i] = new Dictionary<string, Dictionary<Enum, double?>>();
            }

            for (int i = 5; i < workbook.Worksheets[0].Rows.Count; i++)
            {
                currentString = workbook.Worksheets[0].Rows[i].AllocatedCells[0].Value.ToString();
                date = currentString.Split(',', 2)[0];
                currentString = currentString.Split(',', 2)[1];

                for (int j = 0; j < dataArray.Length; j++)
                {
                    headersDict = new Dictionary<Enum, double?>();

                    currentValue = GetCurrentValue(currentString);
                    headersDict.Add(DamHeader.HEIGHT, currentValue);
                    currentString = currentString.Split(',', 2)[1];

                    currentValue = GetCurrentValue(currentString);
                    headersDict.Add(DamHeader.STORAGE, currentValue);
                    currentString = currentString.Split(',', 2)[1];

                    currentValue = GetCurrentValue(currentString);
                    headersDict.Add(DamHeader.CURRENT, currentValue);
                    currentString = currentString.Split(',', 2)[1];

                    currentValue = GetCurrentValue(currentString);
                    headersDict.Add(DamHeader.LASTYEAR, currentValue);
                    if (currentString.Contains(','))
                        currentString = currentString.Split(',', 2)[1];

                    dataArray[j].Add(date, headersDict);
                }
            }
            return dataArray;
        }

        public Tree ReadToBST()
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            ExcelFile workbook = ExcelFile.Load(_path);

            Node root = null;
            Tree bst = new Tree();
            int SIZE = GetNames(workbook).Length;
            Dam[] a = new Dam[SIZE];

            //CreateDamObjects array
            for (int i = 0; i < SIZE; i++)
            {
                a[i] = ReadToArray()[i];
            }

            //insert elements into bst
            for (int i = 0; i < SIZE; i++)
            {
                root = bst.Insert(root, a[i]);
            }

            _rootNode = root;

            return bst;
        }

        public Dam[] ReadToArray()
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            ExcelFile workbook = ExcelFile.Load(_path);

            return CreateDamObjects(workbook);
        }
    }
}

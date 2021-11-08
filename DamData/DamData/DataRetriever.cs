using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static DamData.Headers;

namespace DamData
{
    class DataRetriever
    {
        private static Dam[] _damArray;
        private static Tree _binarySearchTree;
        public static int _opCount;

        public DataRetriever(Dam[] damArray)
        {
            _damArray = damArray;
            _opCount = 0;
        }

        public DataRetriever(Tree binarySearchTree)
        {
            _binarySearchTree = binarySearchTree;
            _opCount = 0;
        }

        public Dam GetCurrentDam(string damName)
        {
            if (_damArray != null)
            {
                foreach (Dam dam in _damArray)
                {
                    //Counting amount of operations
                    _opCount++;
                    if (String.Equals(dam.GetName(), damName, StringComparison.OrdinalIgnoreCase))
                        return dam;
                }
                return null;
            }
            else
            {
                //need to parse root from readToBST()
                _binarySearchTree.Traverse(DataReader._rootNode, damName);
                return _binarySearchTree._currentBstDam;
            }
        }

        private void PrintSingleDate(Dictionary<string, Dictionary<Enum, double?>> data, string date)
        {
            string stringHeader;

            Console.WriteLine(" -> " + date);
            foreach (DamHeader header in data[date].Keys)
            {
                switch (header)
                {
                    case DamHeader.HEIGHT:
                        stringHeader = "Height";
                        break;
                    case DamHeader.STORAGE:
                        stringHeader = "Storage";
                        break;
                    case DamHeader.CURRENT:
                        stringHeader = "Current";
                        break;
                    case DamHeader.LASTYEAR:
                        stringHeader = "L/Year";
                        break;
                    default:
                        stringHeader = "N/A";
                        break;
                }
                if (data[date][header] == null)
                    Console.WriteLine(stringHeader + "\t:\tN/A");
                else
                    Console.WriteLine(stringHeader + "\t:\t" + data[date][header]);
            }
        }

        private void PrintSingleDate(Dictionary<string, Dictionary<Enum, double?>> data, string date, StreamWriter writer)
        {
            string stringHeader;

            writer.WriteLine(" -> " + date);
            foreach (DamHeader header in data[date].Keys)
            {
                switch (header)
                {
                    case DamHeader.HEIGHT:
                        stringHeader = "Height";
                        break;
                    case DamHeader.STORAGE:
                        stringHeader = "Storage";
                        break;
                    case DamHeader.CURRENT:
                        stringHeader = "Current";
                        break;
                    case DamHeader.LASTYEAR:
                        stringHeader = "L/Year";
                        break;
                    default:
                        stringHeader = "N/A";
                        break;
                }
                if (data[date][header] == null)
                    writer.WriteLine(stringHeader + "\t:\tN/A");
                else
                    writer.WriteLine(stringHeader + "\t:\t" + data[date][header]);
            }
        }

        public void PrintDamInfoForDate(string date, Logger logger)
        {
            Dam dam = InputReader._currentDam;
            var data = dam.GetData();

            Task fileLogTask = new Task(() =>
            {
                StreamWriter writer = logger.StartLog();
                writer.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - logged at: " + DateTime.Now);
                writer.WriteLine("#### " + dam.GetName() + " ####");
                PrintSingleDate(data, date, writer);
                logger.EndLog();
            });
            //start logging task async
            fileLogTask.Start();

            //do log to console synchronously
            Console.WriteLine("#### " + dam.GetName() + " ####");
            PrintSingleDate(data, date);

            LogPerformance();
        }

        public void PrintAllDams(Logger logger)
        {
            Dictionary<string, Dictionary<Enum, double?>> data;

            Task fileLogTask = new Task(() =>
            {
                StreamWriter writer = logger.StartLog();
                foreach (Dam dam in _damArray)
                {
                    data = dam.GetData();

                    writer.WriteLine("#### " + dam.GetName() + " ####");
                    foreach (string date in data.Keys)
                    {
                        PrintSingleDate(data, date, writer);
                    }
                    writer.WriteLine();
                }
                logger.EndLog();
            });
            //start logging task async
            fileLogTask.Start();

            //do log to console synchronously
            foreach (Dam dam in _damArray)
            {
                data = dam.GetData();

                Console.WriteLine("#### " + dam.GetName() + " ####");
                foreach (string date in data.Keys)
                {
                    PrintSingleDate(data, date);
                }
                Console.WriteLine();
            }
        }

        private static void LogPerformance()
        {
            string performanceLogPath = @"C:\Users\27732\Desktop\Quintessence C# Projects\BonusProjects\DamData\DamData\PerformanceLogs.txt";
            Logger logger = new Logger(performanceLogPath);

            StreamWriter writer = logger.StartLog();
            if (_damArray != null)
            {
                writer.WriteLine("opCount for TRADITIONAL ARRAY:\t\t" + _opCount);
            }
            else
            {
                writer.WriteLine("opCount for BINARY SEARCH TREE:\t\t" + _opCount);
            }
            _opCount = 0;
            logger.EndLog();
        }
    }
}

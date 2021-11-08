using System;
using System.IO;

namespace DamData
{
    class Logger
    {
        private string _filePath;
        private FileStream _outputStream;
        private StreamWriter _writer;
        private TextWriter _oldOut = Console.Out;

        public Logger(string filePath)
        {
            _filePath = filePath;
        }

        public void Clear()
        {
            try
            {
                File.WriteAllText(_filePath, String.Empty);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Log was cleared successfully.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Log could not be cleared.");
                Console.ForegroundColor = ConsoleColor.Gray;
                _writer.WriteLine(e.Message);
            }
        }

        public void EndLog()
        {
            Console.SetOut(_oldOut);
            _writer.Close();
            _outputStream.Close();
        }

        public StreamWriter StartLog()
        {
            try
            {
                _outputStream = new FileStream(_filePath, FileMode.Append, FileAccess.Write);
                _writer = new StreamWriter(_outputStream);
            }
            catch (Exception e)
            {
                EndLog();
                _writer.WriteLine(e.Message);
                return null;
            }
            return _writer;
        }
    }
}

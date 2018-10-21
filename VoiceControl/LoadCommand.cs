using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceControl
{
    public class LoadCommand
    {
        public class LoadArrayCommands
        {
            private string _commandText;
            private List<Tuple<string, string>> _semanticResult = new List<Tuple<string, string>>();

            public LoadArrayCommands(string commandText)
            {
                _commandText = commandText;
            }

            public void SemanticResult(string command, string number)
            {
                _semanticResult.Add(new Tuple<string, string>(command, number));
            }

            public string CommandTextReturn()
            {
                return _commandText;
            }


            public List<Tuple<string, string>> SemanticResultReturn()
            {
                return _semanticResult;
            }
        }

        private string fileName = @"Command.txt";
        public List<LoadArrayCommands> _arrayCommands = new List<LoadArrayCommands>();

        public List<LoadArrayCommands> OpenRead()
        {
            string textFromFile = "";

            if (!File.Exists(fileName))
            {
                using (StreamWriter textFile = new StreamWriter(fileName, false, Encoding.Default))
                {
                    textFile.WriteLine(ProjectSettings());
                }
            }
            using (FileStream fstream = File.OpenRead(fileName))
            {
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                textFromFile = Encoding.Default.GetString(array);
            }
            


            Parser(textFromFile);

            return _arrayCommands;
        }

        private void Parser(string textFromFile)
        {
            var wordSplitting = textFromFile.Split(new[] { '\t', '\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var currentString in wordSplitting)
            {
                if (currentString.Contains('<'))
                {
                    var nameCommand = RemoveSpaces(currentString, "<", ">");
                    _arrayCommands.Add(new LoadArrayCommands(nameCommand));

                    continue;
                }
                else
                {
                    var currentText = currentString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    _arrayCommands[_arrayCommands.Count - 1].SemanticResult(currentText[0], currentText[1]);
                }
            }
        }

        //удаление символов из строки
        private string RemoveSpaces(string inpurString, params string[] valueDeletion)
        {
            foreach(var currentValuein in valueDeletion)
            {
                inpurString = inpurString.Replace(currentValuein, string.Empty);
                inpurString = inpurString.Trim().Replace(currentValuein, string.Empty);
            }

            return inpurString;
        }

        private string ProjectSettings()
        {
           string projectSettings = "<левой>\r\nодин 1\r\nдва 2\r\nтри 3\r\nчетыре 4\r\nпять 5\r\nшесть 6\r\nсемь 7\r\nвосемь 8\r\nдевять 9\r\n\r\n" +
                "<правой>\r\n\r\nодин 1\r\nдва 2\r\nтри 3\r\nчетыре 4\r\nпять 5\r\nшесть 6\r\nсемь 7\r\nвосемь 8\r\nдевять 9\r\n\r\n" +
                "<двойной>\r\n\r\nодин 1\r\nдва 2\r\nтри 3\r\nчетыре 4\r\nпять 5\r\nшесть 6\r\nсемь 7\r\nвосемь 8\r\nдевять 9\r\n\r\n" +
                "<масштаб>\r\n\r\nодин 1\r\nдва 2\r\nтри 3\r\nчетыре 4\r\nпять 5\r\nшесть 6\r\nсемь 7\r\nвосемь 8\r\nдевять 9\r\n\r\n" +
                "<лупа>\r\n\r\nодин 1\r\nдва 2\r\nтри 3\r\nчетыре 4\r\nпять 5\r\nшесть 6\r\nсемь 7\r\nвосемь 8\r\nдевять 9\r\n\r\n" +
                "<конец>\r\n\r\nмасштаб 11\r\nлупа 12";

            return projectSettings;
        }
    }
}

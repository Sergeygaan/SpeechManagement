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
            private List<Tuple<string, int>> _semanticResult = new List<Tuple<string, int>>();

            public LoadArrayCommands(string commandText)
            {
                _commandText = commandText;
            }

            public void SemanticResult(string command, int number)
            {
                _semanticResult.Add(new Tuple<string, int>(command, number));
            }

            public string CommandTextReturn()
            {
                return _commandText;
            }


            public List<Tuple<string, int>> SemanticResultReturn()
            {
                return _semanticResult;
            }
        }

        public List<LoadArrayCommands> _arrayCommands = new List<LoadArrayCommands>();

        public List<LoadArrayCommands> OpenRead()
        {
            string textFromFile = "";

            using (FileStream fstream = File.OpenRead(@"Command.txt"))
            {
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                textFromFile = System.Text.Encoding.Default.GetString(array);
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

                    _arrayCommands[_arrayCommands.Count - 1].SemanticResult(currentText[0], int.Parse(currentText[1]));
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
    }
}

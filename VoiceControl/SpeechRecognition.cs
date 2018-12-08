using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using WorkingScreen;
using Microsoft.Speech.Recognition;

using static VoiceControl.LogForm;
using ProjectSettings;

namespace VoiceControl
{
    class SpeechRecognition
    {
        private CultureInfo _culture;
        private SpeechRecognitionEngine _sre;
        private ScreenDelineation _screenDelineation;

        private LoadCommand _loadCommand;
        private List<string> _commandNames = new List<string>();

        private MethodAppendLine _appendLine;

        public SpeechRecognition(MethodAppendLine appendLine)
        {
            _appendLine = appendLine;

            _loadCommand = new LoadCommand();

            FillListCommands();

            _screenDelineation = new ScreenDelineation();
            _screenDelineation.Show();

            StartGrammar();
        }

        private void FillListCommands()
        {
            var loadArrayCommands = _loadCommand.OpenRead();

            foreach (var currentLoadArrayCommands in loadArrayCommands)
            {
                ProjectSettingsMain.ArrayCommands.Add(new ArrayCommands(currentLoadArrayCommands.CommandTextReturn(),
                                    CreateSample(currentLoadArrayCommands.SemanticResultReturn())));

                _commandNames.Add(currentLoadArrayCommands.CommandTextReturn());
            }
        }

        private void StartGrammar()
        {
            try
            {
                //_culture = new CultureInfo("en-US");
                _culture = new CultureInfo("ru-RU");
                _sre = new SpeechRecognitionEngine(_culture);

                // Setup event handlers
                _sre.SpeechDetected += new EventHandler<SpeechDetectedEventArgs>(sr_SpeechDetected);
                _sre.RecognizeCompleted += new EventHandler<RecognizeCompletedEventArgs>(sr_RecognizeCompleted);
                _sre.SpeechHypothesized += new EventHandler<SpeechHypothesizedEventArgs>(sr_SpeechHypothesized);
                _sre.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(sr_SpeechRecognitionRejected);
                _sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sr_SpeechRecognized);

                // select input source
                _sre.SetInputToDefaultAudioDevice();

                // load grammar
                LoadGrammar();

                // start recognition
                _sre.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private Choices CreateSample(List<Tuple<string, string>> semanticResult)
        {
            GrammarBuilder[] grammarBuilders = new GrammarBuilder[semanticResult.Count];

            int index = 0;

            foreach (var currentSemantic in semanticResult)
            {
                grammarBuilders[index] = new SemanticResultValue(currentSemantic.Item1, currentSemantic.Item2);

                index++;
            }

            return new Choices(grammarBuilders);
        }

        private void LoadGrammar()
        {
            //Загрузка команд из списка
            foreach (ArrayCommands currentCommand in ProjectSettingsMain.ArrayCommands)
            {
                _sre.LoadGrammar(CreateGrammar(currentCommand.CommandText, currentCommand.SemanticResult));
            }
        }

        private Grammar CreateGrammar(string commandText, Choices semanticResult)
        {
            var grammarBuilder = new GrammarBuilder(commandText, SubsetMatchingMode.SubsequenceContentRequired)
            {
                Culture = _culture
            };

            grammarBuilder.Append(new SemanticResultKey(commandText, semanticResult));

            return new Grammar(grammarBuilder);
        }

        private void sr_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            _appendLine("Speech Recognition Rejected: " + e.Result.Text, Color.WhiteSmoke);
        }

        private void sr_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            _appendLine("Speech Hypothesized: " + e.Result.Text + " (" + e.Result.Confidence + ")", Color.WhiteSmoke);
        }

        private void sr_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            _appendLine("Recognize Completed: " + e.Result.Text, Color.WhiteSmoke);
        }

        private void sr_SpeechDetected(object sender, SpeechDetectedEventArgs e)
        {
            _appendLine("Speech Detected: audio pos " + e.AudioPosition, Color.WhiteSmoke);
        }

        //Определение корректности текста
        private void sr_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            _appendLine("\t" + "Speech Recognized:", Color.WhiteSmoke);

            if (e.Result.Confidence < 0.35f)
            {
                _appendLine(e.Result.Text + " (" + e.Result.Confidence + ")", Color.LightCoral);
                return;
            }
            for (var i = 0; i < e.Result.Alternates.Count; ++i)
            {
                _appendLine("\t" + "Alternate: " + e.Result.Alternates[i].Text + " (" + e.Result.Alternates[i].Confidence + ")", Color.WhiteSmoke);
            }

            for (var i = 0; i < e.Result.Words.Count; ++i)
            {
                if (e.Result.Words[i].Confidence < 0.2f)
                {
                    _appendLine("\t" + "Word: " + e.Result.Words[i].Text + " (" + e.Result.Words[i].Confidence + ")", Color.LightCoral);
                    return;
                }
                else
                {
                    _appendLine("\t" + "Word: " + e.Result.Words[i].Text + " (" + e.Result.Words[i].Confidence + ")", Color.WhiteSmoke);
                }
            }

            _appendLine(e.Result.Text + " (" + e.Result.Confidence + ")", Color.YellowGreen);

            _appendLine("---------------------------------------------------------------------------------------", Color.WhiteSmoke);

            foreach (var s in e.Result.Semantics)
            {
                var number = s.Value.Value;

                for (int index = 0; index < _commandNames.Count; index++)
                {
                    if (_commandNames[index] == s.Key)
                    {
                        _screenDelineation.ApplyCommand(index, Convert.ToInt32(number) - 1);

                        ////Process.Start((string)number);

                        ////Process.Start("http://google.com");

                        //System.Windows.Forms.WebBrowser webBrowser = new WebBrowser();

                        //webBrowser.Navigate("http://google.com");
                        //webBrowser.ScriptErrorsSuppressed = true;

                        break;
                    }
                }

                //switch (s.Key)
                //{

                //    case "старт":

                //        Process.Start((string)number);

                //        break;

                //}
            }
        }
    }

    public class ArrayCommands
    {
        public string CommandText;
        public Choices SemanticResult;

        public ArrayCommands(string commandText, Choices semanticResult)
        {
            CommandText = commandText;
            SemanticResult = semanticResult;
        }
    }
}

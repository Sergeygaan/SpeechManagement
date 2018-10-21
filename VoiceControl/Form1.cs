﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Speech;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;

namespace VoiceControl
{
    public partial class Form1 : Form
    {
        private CultureInfo _culture;
        private SpeechRecognitionEngine _sre;
        private ScreenDelineation _screenDelineation;

        private List<ArrayCommands> _arrayCommands = new List<ArrayCommands>();

        public Form1()
        {
            InitializeComponent();

            FillListCommands();
 
            _screenDelineation = new ScreenDelineation();
            _screenDelineation.Show();
        }

        private void FillListCommands()
        {
            //_arrayCommands.Add(new ArrayCommands("left", CreateSampleNumbers()));
            //_arrayCommands.Add(new ArrayCommands("right", CreateSampleNumbers()));
            //_arrayCommands.Add(new ArrayCommands("double", CreateSampleNumbers()));
            //_arrayCommands.Add(new ArrayCommands("scale", CreateSampleNumbers()));
            //_arrayCommands.Add(new ArrayCommands("loupe", CreateSampleNumbers()));
            //_arrayCommands.Add(new ArrayCommands("end", CreateSampleEnd()));
            _arrayCommands.Add(new ArrayCommands("левой", CreateSampleNumbers()));
            _arrayCommands.Add(new ArrayCommands("правой", CreateSampleNumbers()));
            _arrayCommands.Add(new ArrayCommands("двойной", CreateSampleNumbers()));
            _arrayCommands.Add(new ArrayCommands("масштаб", CreateSampleNumbers()));
            _arrayCommands.Add(new ArrayCommands("лупа", CreateSampleNumbers()));
            _arrayCommands.Add(new ArrayCommands("конец", CreateSampleEnd()));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //_culture = new CultureInfo("en-US");
                _culture = new System.Globalization.CultureInfo("ru-RU");
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

        private Choices CreateSampleNumbers()
        {
            GrammarBuilder[] grammarBuilders = 
            {
                 //new SemanticResultValue("one", 1),
                 //new SemanticResultValue("two", 2),
                 //new SemanticResultValue("three", 3),
                 //new SemanticResultValue("four", 4),
                 //new SemanticResultValue("five", 5),
                 //new SemanticResultValue("six", 6),
                 //new SemanticResultValue("seven", 7),
                 //new SemanticResultValue("eight", 8),
                 //new SemanticResultValue("nine", 9)

                 new SemanticResultValue("один", 1),
                 new SemanticResultValue("два", 2),
                 new SemanticResultValue("три", 3),
                 new SemanticResultValue("четыре", 4),
                 new SemanticResultValue("пять", 5),
                 new SemanticResultValue("шесть", 6),
                 new SemanticResultValue("семь", 7),
                 new SemanticResultValue("восемь", 8),
                 new SemanticResultValue("девять", 9)
            };

            return new Choices(grammarBuilders);
        }

        private Choices CreateSampleEnd()
        {
            GrammarBuilder[] grammarBuilders =
            {
                 //new SemanticResultValue("scale", 11),
                 //new SemanticResultValue("loupe", 12),
                 new SemanticResultValue("масштаб", 11),
                 new SemanticResultValue("лупа", 12)
            };

            return new Choices(grammarBuilders);
        }

        private void LoadGrammar()
        {
            //Загрузка команд из списка
            foreach(var currentCommand in _arrayCommands)
            {
                _sre.LoadGrammar(CreateGrammar(currentCommand.CommandText, currentCommand.SemanticResult));
            }
        }

        private Grammar CreateGrammar(string commandText, Choices semanticResult)
        {
            var grammarBuilder = new GrammarBuilder(commandText, SubsetMatchingMode.SubsequenceContentRequired);
            grammarBuilder.Culture = _culture;
            grammarBuilder.Append(new SemanticResultKey(commandText, semanticResult));

            return new Grammar(grammarBuilder);
        }

        private void sr_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            AppendLine("Speech Recognition Rejected: " + e.Result.Text, Color.WhiteSmoke);
        }

        private void sr_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            AppendLine("Speech Hypothesized: " + e.Result.Text + " (" + e.Result.Confidence + ")", Color.WhiteSmoke);
        }

        private void sr_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            AppendLine("Recognize Completed: " + e.Result.Text, Color.WhiteSmoke);
        }

        private void sr_SpeechDetected(object sender, SpeechDetectedEventArgs e)
        {
            AppendLine("Speech Detected: audio pos " + e.AudioPosition, Color.WhiteSmoke);
        }

        //Определение корректности текста
        private void sr_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            AppendLine("\t" + "Speech Recognized:", Color.WhiteSmoke);

            if (e.Result.Confidence < 0.35f)
            {
                AppendLine(e.Result.Text + " (" + e.Result.Confidence + ")", Color.LightCoral);
                return;
            }
            for (var i = 0; i < e.Result.Alternates.Count; ++i)
            {
                AppendLine("\t" + "Alternate: " + e.Result.Alternates[i].Text + " (" + e.Result.Alternates[i].Confidence + ")", Color.WhiteSmoke);
            }

            for (var i = 0; i < e.Result.Words.Count; ++i)
            {
                if (e.Result.Words[i].Confidence < 0.2f)
                {
                    AppendLine("\t" + "Word: " + e.Result.Words[i].Text + " (" + e.Result.Words[i].Confidence + ")", Color.LightCoral);
                    return;
                }
                else
                {
                    AppendLine("\t" + "Word: " + e.Result.Words[i].Text + " (" + e.Result.Words[i].Confidence + ")", Color.WhiteSmoke);
                }
            }

            AppendLine(e.Result.Text + " (" + e.Result.Confidence + ")", Color.YellowGreen);

            AppendLine("---------------------------------------------------------------------------------------", Color.WhiteSmoke);

            foreach (var s in e.Result.Semantics)
            {
                var number = (int)s.Value.Value;

                switch (s.Key)
                {
                    case "левой":

                        _screenDelineation.ApplyCommand(0, number);
                      
                        break;

                    case "правой":

                        _screenDelineation.ApplyCommand(1, number);

                        break;

                    case "двойной":

                        _screenDelineation.ApplyCommand(2, number);

                        break;

                    case "масштаб":

                        _screenDelineation.ApplyCommand(3, number);

                        break;

                    case "лупа":

                        _screenDelineation.ApplyCommand(4, number);

                        break;

                    case "конец":

                        _screenDelineation.ApplyCommand(5, 0);

                        break;


                }
            }
        }

        private void AppendLine(string text, Color color )
        {
            ListViewItem listViewItem = new ListViewItem();
            listViewItem.Text = text;
            listViewItem.BackColor = color;

            listView.Items.Add(listViewItem);

            if (listView.Items.Count > 2)
            {
                listView.Items[listView.Items.Count - 2].Focused = false;
                listView.Items[listView.Items.Count - 2].Focused = false;

                listView.Items[listView.Items.Count - 1].Focused = true;
                listView.Items[listView.Items.Count - 1].Focused = true;
                listView.Items[listView.Items.Count - 1].EnsureVisible();
            }

            if (listView.Items.Count > 100)
            {
                listView.Items.Clear();
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

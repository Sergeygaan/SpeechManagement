using System;
using System.Diagnostics;
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

        public Form1()
        {
            InitializeComponent();
            _screenDelineation = new ScreenDelineation();
            _screenDelineation.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _culture = new CultureInfo("en-US");
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
                _sre.LoadGrammar(Left());
                _sre.LoadGrammar(Right());
                _sre.LoadGrammar(Double());
                _sre.LoadGrammar(Scale());
                _sre.LoadGrammar(End());
                _sre.LoadGrammar(Increase());

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
                 new SemanticResultValue("one", 1),
                 new SemanticResultValue("two", 2),
                 new SemanticResultValue("three", 3),
                 new SemanticResultValue("four", 4),
                 new SemanticResultValue("five", 5),
                 new SemanticResultValue("six", 6),
                 new SemanticResultValue("seven", 7),
                 new SemanticResultValue("eight", 8),
                 new SemanticResultValue("nine", 9)
            };

            return new Choices(grammarBuilders);
        }

        private Choices CreateSampleEnd()
        {
            GrammarBuilder[] grammarBuilders =
            {
                 new SemanticResultValue("scale", 11),
                 new SemanticResultValue("loupe", 12),
            };

            return new Choices(grammarBuilders);
        }

        private Grammar Left()
        {
            var programs = CreateSampleNumbers();

            var grammarBuilder = new GrammarBuilder("left", SubsetMatchingMode.SubsequenceContentRequired);
            grammarBuilder.Culture = _culture;
            grammarBuilder.Append(new SemanticResultKey("left", programs));

            return new Grammar(grammarBuilder);
        }

        private Grammar Right()
        {
            var programs = CreateSampleNumbers();

            var grammarBuilder = new GrammarBuilder("right", SubsetMatchingMode.SubsequenceContentRequired);
            grammarBuilder.Culture = _culture;
            grammarBuilder.Append(new SemanticResultKey("right", programs));

            return new Grammar(grammarBuilder);
        }

        private Grammar Double()
        {
            var programs = CreateSampleNumbers();

            var grammarBuilder = new GrammarBuilder("Double", SubsetMatchingMode.SubsequenceContentRequired);
            grammarBuilder.Culture = _culture;
            grammarBuilder.Append(new SemanticResultKey("double", programs));

            return new Grammar(grammarBuilder);
        }

        private Grammar Scale()
        {
            var programs = CreateSampleNumbers();

            var grammarBuilder = new GrammarBuilder("scale", SubsetMatchingMode.SubsequenceContentRequired);
            grammarBuilder.Culture = _culture;
            grammarBuilder.Append(new SemanticResultKey("scale", programs));

            return new Grammar(grammarBuilder);
        }

        private Grammar Increase()
        {
            var programs = CreateSampleNumbers();

            var grammarBuilder = new GrammarBuilder("loupe", SubsetMatchingMode.SubsequenceContentRequired);
            grammarBuilder.Culture = _culture;
            grammarBuilder.Append(new SemanticResultKey("loupe", programs));

            return new Grammar(grammarBuilder);
        }

        private Grammar End()
        {
            var programs = CreateSampleEnd();

            var grammarBuilder = new GrammarBuilder("end", SubsetMatchingMode.SubsequenceContentRequired);
            grammarBuilder.Culture = _culture;
            grammarBuilder.Append(new SemanticResultKey("end", programs));

            return new Grammar(grammarBuilder);
        }

        private void sr_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            AppendLine("Speech Recognition Rejected: " + e.Result.Text);
        }

        private void sr_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            AppendLine("Speech Hypothesized: " + e.Result.Text + " (" + e.Result.Confidence + ")");
        }

        private void sr_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            AppendLine("Recognize Completed: " + e.Result.Text);
        }

        private void sr_SpeechDetected(object sender, SpeechDetectedEventArgs e)
        {
            AppendLine("Speech Detected: audio pos " + e.AudioPosition);
        }

        private void sr_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            AppendLine("\t" + "Speech Recognized:");

            AppendLine(e.Result.Text + " (" + e.Result.Confidence + ")");

            if (e.Result.Confidence < 0.35f)
                return;

            for (var i = 0; i < e.Result.Alternates.Count; ++i)
            {
                AppendLine("\t" + "Alternate: " + e.Result.Alternates[i].Text + " (" + e.Result.Alternates[i].Confidence + ")");
            }

            for (var i = 0; i < e.Result.Words.Count; ++i)
            {
                AppendLine("\t" + "Word: " + e.Result.Words[i].Text + " (" + e.Result.Words[i].Confidence + ")");

                if (e.Result.Words[i].Confidence < 0.05f)
                    return;
            }

            foreach (var s in e.Result.Semantics)
            {
                var number = (int)s.Value.Value;

                switch (s.Key)
                {
                    case "left":

                        _screenDelineation.ApplyCommand(0, number);
                      
                        break;

                    case "right":

                        _screenDelineation.ApplyCommand(1, number);

                        break;

                    case "double":

                        _screenDelineation.ApplyCommand(2, number);

                        break;

                    case "scale":

                        _screenDelineation.ApplyCommand(3, number);

                        break;

                    case "loupe":

                        _screenDelineation.ApplyCommand(4, number);

                        break;

                    case "end":

                        _screenDelineation.ApplyCommand(5, 0);

                        break;


                }
            }
        }

        private void AppendLine(string text)
        {
            richTextBox1.AppendText(text + Environment.NewLine);
            richTextBox1.ScrollToCaret();
        }
    }
}

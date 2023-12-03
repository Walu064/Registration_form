using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;

namespace Registration_form
{
    public partial class MainWindow : Window
    {
        private SpeechRecognitionEngine speechRecognitionEngine;
        private SpeechSynthesizer speechSynthesizer;
        private bool isSpeechRecognitionEnabled = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeSpeechRecognition();
            InitializeSpeechSynthesizer();
        }

        private void InitializeSpeechRecognition()
        {
            speechRecognitionEngine = new SpeechRecognitionEngine();
            Grammar grammar = CreateGrammarFromWords();
            speechRecognitionEngine.LoadGrammar(grammar);
            speechRecognitionEngine.SetInputToDefaultAudioDevice();
            speechRecognitionEngine.SpeechRecognized += SpeechRecognized;
        }

        private void InitializeSpeechSynthesizer()
        {
            speechSynthesizer = new SpeechSynthesizer();
            speechSynthesizer.SetOutputToDefaultAudioDevice();
        }

        private Grammar CreateGrammarFromWords()
        {
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            List<string> words = new List<string> { "Jakub", "Jerzy", "Krzysztof", "Włodzimierz", "Andrzej",
            "Walczak", "Stanik", "Kononowicz", "Kwiatkowski", "Stasiak",
            "Polska", "Niemcy", "Litwa", "Łotwa", "Estonia",
            "1998", "1907", "1932", "1921", "1960",
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
            grammarBuilder.Append(new Choices(words.ToArray()));
            Grammar grammar = new Grammar(grammarBuilder);
            return grammar;
        }

        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string recognizedText = e.Result.Text;

            if (Keyboard.FocusedElement is TextBox textBox)
            {
                textBox.SelectedText += recognizedText;
            }
        }

        private void SpeechRecognitionToggle_Checked(object sender, RoutedEventArgs e)
        {
            if (!isSpeechRecognitionEnabled)
            {
                speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
                speechSynthesizer.Speak("Uzupełnianie głosowe uruchomione");
                isSpeechRecognitionEnabled = true;
            }
        }

        private void SpeechRecognitionToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            if (isSpeechRecognitionEnabled)
            {
                speechRecognitionEngine.RecognizeAsyncCancel();
                speechSynthesizer.Speak("Uzupełnianie głosowe zatrzymane");
                isSpeechRecognitionEnabled = false;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ClearBackgroundProperty();
            List<string> unfilledFields = GetUnfilledFields();

            if (unfilledFields.Count == 0)
            {
                ShowMessageBoxWithFormData();
            }
            else
            {
                string message;
                if (unfilledFields.Count == 1)
                {
                    message = $"Uzupełnij pole: {unfilledFields[0]}";
                }
                else
                {
                    message = $"Uzupełnij pola: {string.Join(", ", unfilledFields)}";
                }

                speechSynthesizer.SpeakAsync(message);

                foreach (var field in unfilledFields)
                {
                    HighlightField(field);
                }
            }
        }

        private void ShowMessageBoxWithFormData()
        {
            string message = $"Imię: {FirstNameTextBox.Text}\n"
                           + $"Nazwisko: {LastNameTextBox.Text}\n"
                           + $"Rok urodzenia: {BirthDatePickerTextBox.Text}\n"
                           + $"Kraj: {CountryComboBoxTextBox.Text}\n"
                           + $"Numer telefonu: {PhoneNumberTextBox.Text}\n"
                           + "Jeśli chcesz poprawić formularz wciśnij okej, kliknij przycisk WYCZYŚĆ i wypełnij ponownie.";
            ClearBackgroundProperty();
            speechSynthesizer.SpeakAsync(message);
            MessageBox.Show(message, "Zapisano", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private List<string> GetUnfilledFields()
        {
            List<string> unfilledFields = new List<string>();

            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
                unfilledFields.Add("Imię");

            if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
                unfilledFields.Add("Nazwisko");

            if (string.IsNullOrWhiteSpace(BirthDatePickerTextBox.Text))
                unfilledFields.Add("Rok urodzenia");

            if (string.IsNullOrWhiteSpace(CountryComboBoxTextBox.Text))
                unfilledFields.Add("Kraj");

            if (string.IsNullOrWhiteSpace(PhoneNumberTextBox.Text))
                unfilledFields.Add("Numer telefonu");

            return unfilledFields;
        }

        private void HighlightField(string fieldName)
        {
            switch (fieldName)
            {
                case "Imię":
                    FirstNameTextBox.Background = Brushes.Red;
                    break;
                case "Nazwisko":
                    LastNameTextBox.Background = Brushes.Red;
                    break;
                case "Rok urodzenia":
                    BirthDatePickerTextBox.Background = Brushes.Red;
                    break;
                case "Kraj":
                    CountryComboBoxTextBox.Background = Brushes.Red;
                    break;
                case "Numer telefonu":
                    PhoneNumberTextBox.Background = Brushes.Red;
                    break;
            }
        }

        private void ClearBackgroundProperty()
        {
            FirstNameTextBox.ClearValue(BackgroundProperty);
            LastNameTextBox.ClearValue(BackgroundProperty);
            BirthDatePickerTextBox.ClearValue(BackgroundProperty);
            CountryComboBoxTextBox.ClearValue(BackgroundProperty);
            PhoneNumberTextBox.ClearValue(BackgroundProperty);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearBackgroundProperty();
            FirstNameTextBox.Clear();
            LastNameTextBox.Clear();
            BirthDatePickerTextBox.Clear();
            CountryComboBoxTextBox.Clear();
            PhoneNumberTextBox.Clear();
        }
    }
}

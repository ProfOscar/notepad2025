using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Notepad
{
    static class FindReplaceService
    {
        /// <summary>
        /// Il RichTextBox sul quale avverrà la ricerca/sostituzione
        /// </summary>
        public static RichTextBox Target;

        /// <summary>
        /// La Struct che contiene tutti i parametri per la ricerca/sostituzione
        /// </summary>
        public struct Parameters
        {
            public static string TextToFind = "";
            public static string TextToReplace = "";
            public static bool IsUp = false;
            public static bool IsCaseSensive = false;
            public static bool IsWholeWord = false;
        }

        /// <summary>
        /// Cerca il parametro TextToFind all'interno del RichTextBox Target
        /// </summary>
        /// <returns>La posizione del testo trovato, oppure -1 se non trovato</returns>
        public static int Find()
        {
            int start = 0;
            int end = Target.TextLength;
            RichTextBoxFinds options = RichTextBoxFinds.None;

            if (Parameters.IsUp)
            {
                options = RichTextBoxFinds.Reverse;
                end = Target.SelectionStart;
                if (end == 0) return -1;
            }
            else
            {
                start = Target.SelectionStart + Target.SelectionLength;
                if (start >= end) return -1;
            }

            if (Parameters.IsCaseSensive)
                options |= RichTextBoxFinds.MatchCase;
            if (Parameters.IsWholeWord)
                options |= RichTextBoxFinds.WholeWord;

            Target.Focus();
            return Target.Find(Parameters.TextToFind, start, end, options);
        }

        /// <summary>
        /// Cerca nel Target la prima occorrenza del parametro TextToFind e la sostituisce con TextToReplace
        /// </summary>
        /// <returns>La posizione del testo sostituito, oppure -1 se non trovato</returns>
        public static int Replace()
        {
            StringComparison comparison = Parameters.IsCaseSensive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
            int start = String.Compare(Target.SelectedText, Parameters.TextToFind.ToString(), comparison);
            if (start == 0) Target.SelectedText = Parameters.TextToReplace;
            return Find();
        }

        /// <summary>
        /// Cerca nel Target tutte le occorrenze del parametro TextToFind e le sostituisce con TextToReplace
        /// </summary>
        /// <returns>True se ci sono state sostituzioni, altrimenti false</returns>
        public static bool ReplaceAll()
        {
            string textToFind = Parameters.IsWholeWord ? $@"\b{Parameters.TextToFind}\b" : Parameters.TextToFind;
            RegexOptions options = !Parameters.IsCaseSensive ? RegexOptions.IgnoreCase : RegexOptions.None;
            if (Regex.IsMatch(Target.Text, textToFind, options))
            {
                Target.Text = Regex.Replace(Target.Text, textToFind, Parameters.TextToReplace, options);
                return true;
            }
            return false;
        }

        public static void ShowNotFound()
        {
            MessageBox.Show(
                    $"Impossibile trovare \"{FindReplaceService.Parameters.TextToFind}\"",
                    "Blocco note",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
        }
    }
}

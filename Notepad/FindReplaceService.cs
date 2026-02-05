using System;
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
            return -1;
        }

        /// <summary>
        /// Cerca nel Target la prima occorrenza del parametro TextToFind e la sostituisce con TextToReplace
        /// </summary>
        /// <returns>La posizione del testo sostituito, oppure -1 se non trovato</returns>
        public static int Replace()
        {
            return -1;
        }

        /// <summary>
        /// Cerca nel Target tutte le occorrenze del parametro TextToFind e le sostituisce con TextToReplace
        /// </summary>
        /// <returns>True se ci sono state sostituzioni, altrimenti false</returns>
        public static bool ReplaceAll()
        {
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

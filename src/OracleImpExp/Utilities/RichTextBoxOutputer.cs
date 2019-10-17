using System;
using System.Drawing;
//
using System.Windows.Forms; 

namespace OracleImpExp.Utilities
{
    public class RichTextBoxOutputer
    {
        Color NormalColor = ColorTranslator.FromHtml("#12be1a");
        Color ImportantColor = ColorTranslator.FromHtml("#8609ef");
        Color WarnColor = ColorTranslator.FromHtml("#df8d17");
        Color ErrorColor = ColorTranslator.FromHtml("#f11030");

        public RichTextBoxOutputer(RichTextBox richTextBox)
        {
            this.richTextBox = richTextBox;
        }

        private RichTextBox richTextBox;

        public void AppendText(string text)
        {
            this.richTextBox.SelectionStart = this.richTextBox.TextLength;
            
            this.richTextBox.AppendText($"{text}{Environment.NewLine}");

            this.richTextBox.ScrollToCaret();
        }

        public void ResetText()
        {
            this.richTextBox.ResetText();
        }
    }
}

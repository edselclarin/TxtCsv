using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace TxtCsv
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            int i = 0;
            textBox1.TabIndex = i++;
            textBox2.TabIndex = i++;
            radioCRLF.TabIndex = i++;
            radioTab.TabIndex = i++;
            radioSpace.TabIndex = i++;
            checkCopyToClipboard.TabIndex = i++;
            buttonConvert.TabIndex = i++;

            checkCopyToClipboard.Checked = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckRadioButton(radioCRLF);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            InvokeOnClick(buttonConvert, EventArgs.Empty);
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            Convert(textBox1.Text);
        }

        private void Convert(string inputStr)
        {
            if (!String.IsNullOrEmpty(inputStr))
            {
                var separators = new List<char>();
                if (radioCRLF.Checked)
                {
                    separators.Add('\r');
                    separators.Add('\n');
                }
                else if (radioTab.Checked)
                {
                    separators.Add('\t');
                }
                else
                {
                    separators.Add(' ');
                }

                var items = inputStr.Split(separators.ToArray(), StringSplitOptions.RemoveEmptyEntries);
                var outputStr = String.Join(",", items);

                textBox2.Text = outputStr;

                if (checkCopyToClipboard.Checked)
                {
                    CopyToClipboard(outputStr, TextDataFormat.UnicodeText);
                }
            }
        }

        private void CheckRadioButton(RadioButton btn)
        {
            radioCRLF.Checked = 
            radioTab.Checked = 
            radioTab.Checked = false;

            btn.Checked = true;
        }

        #region COPY_TO_CLIPBOARD

        public static void CopyToClipboard(string txt, TextDataFormat format)
        {
            var clipboardThread = new Thread(() => SendToClipboard(txt, format));
            clipboardThread.SetApartmentState(ApartmentState.STA);
            clipboardThread.IsBackground = true;
            clipboardThread.Start();
        }

        private static void SendToClipboard(string txt, TextDataFormat format)
        {
            Clipboard.SetText(txt, format);
        }

        #endregion COPY_TO_CLIPBOARD
    }
}

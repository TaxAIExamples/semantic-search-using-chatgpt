using Azure;
using ChatGPTInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Text.Json;
using System.Windows.Forms;

namespace TrainingDataChatGPTApp
{
    public partial class MainSearchForm : Form
    {
        private KnowledgeRecordManager? recordManager;
        private string searchTerm;
        private int selectedId = 0;
        private readonly IConfiguration _configuration;

        public MainSearchForm(IConfiguration configuration)
        {
            InitializeComponent();
            _configuration = configuration;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RefreshControlState();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        private void RefreshControlState()
        {
            if (!string.IsNullOrEmpty(askChatGPTTextBox.Text))
            {
                askChatGPTButton.Enabled = true;
            }
            else
            {
                askChatGPTButton.Enabled = false;
            }
        }

        private void askChatGPTTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(askChatGPTTextBox.Text))
            {
                askChatGPTButton.Enabled = true;
            } else
            {
                askChatGPTButton.Enabled = false;
            }
        }

        private void askChatGPTButton_Click(object sender, EventArgs e)
        {
            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;
            chatGPTAnswerTextBox.Text = "";

            try
            {
                string response = chatGPTAnswerTextBox.Text = 
                    ChatGPTInterface.ChatGPTInterface.GetChatGPTAnswerForQuestion(askChatGPTTextBox.Text, 
                        lowDetailRadioButton.Checked, _configuration);

                if(response.Trim().ToLower().StartsWith("i don't know"))
                {
                    response = _configuration["NoResponse"];
                }

                chatGPTAnswerTextBox.Text = response;
            }
            catch (Exception q)
            {
                chatGPTAnswerTextBox.Text = "Could not get an answer from ChatGPT: " + q.Message;
            }

            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
        }

        private void lowDetailRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            lotsDetailRadioButton.Checked = !lowDetailRadioButton.Checked;
        }

        private void lotsOfDetailRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            lowDetailRadioButton.Checked = !lotsDetailRadioButton.Checked;
        }
    }
}
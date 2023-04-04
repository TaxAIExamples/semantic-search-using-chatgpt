using Azure;
using ChatGPTInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SemanticSearch;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Text.Json;
using System.Windows.Forms;

namespace TrainingDataChatGPTApp
{
    public partial class MainSearchForm : Form
    {
        private IKnowledgeRecordManager _recordManager;
        private string searchTerm;
        private int selectedId = 0;
        private readonly IConfiguration _configuration;
        private readonly IChatGPT _chatGPT;

        public MainSearchForm(
            IConfiguration configuration, 
            IChatGPT chatGPT,
            IKnowledgeRecordManager recordManager
            )
        {
            InitializeComponent();
            _configuration = configuration;
            _chatGPT = chatGPT;
            _recordManager = recordManager;
            contextPanel.Visible = false;
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
            contextPanel.Visible = false;
            contextDetailTextBox.Text = "";
            contextListBox.DataSource = null;

            try
            {
                (string response, List<KnowledgeRecordBasicContent> contextList) = 
                    _chatGPT.GetChatGPTAnswerForQuestion(askChatGPTTextBox.Text, lowDetailRadioButton.Checked);
                  
                if(response.Trim().ToLower().StartsWith("i don't know"))
                {
                    response = _configuration["NoResponse"];
                }

                chatGPTAnswerTextBox.Text = response;
                if(contextList.Count > 0)
                {
                    contextPanel.Visible = true;
                    contextListBox.DataSource = contextList;
                    contextListBox.DisplayMember = "DisplayText";
                    contextListBox.SelectedIndex = -1;
                } else
                {
                    contextPanel.Visible = false;
                }
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

        private void contextListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (contextListBox.SelectedItem is KnowledgeRecordBasicContent selectedRecord)
            {
                contextDetailTextBox.Text = selectedRecord.Title + "\n" + selectedRecord.Content;
            } else
            {
                contextDetailTextBox.Text = "";
            }
        }
    }
}
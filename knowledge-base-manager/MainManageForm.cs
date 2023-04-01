using ChatGPTInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SemanticSearch;
using System.ComponentModel;
using System.Data;
using System.Text.Json;
using System.Windows.Forms;

namespace TrainingDataChatGPTApp
{
    public partial class MainManageForm : Form
    {
        private KnowledgeRecordManager? recordManager;
        private string searchTerm;
        private int selectedId = 0;
        private readonly IConfiguration _configuration;

        public MainManageForm(IConfiguration configuration)
        {
            InitializeComponent();
            _configuration = configuration;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            recordManager = new KnowledgeRecordManager(_configuration);
            RefreshList();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        private void addRecordButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(contentTextBox.Text))
            {
                KnowledgeRecord trainingRecord = new KnowledgeRecord()
                {
                    Id = 0,
                    Content = contentTextBox.Text.Trim(),
                    Title = titleTextBox.Text.Trim()
                };

                KnowledgeRecord theRecord = recordManager.AddRecord(trainingRecord, _configuration);
                selectedId = theRecord.Id;

                ClearRecord();
                RefreshList();
            }
            else
            {
                MessageBox.Show("No text to add");
            }
        }

        private void RefreshList()
        {
            List<KnowledgeRecord> records;
            if (string.IsNullOrEmpty(searchTerm))
            {
                records = recordManager.GetAllRecordsNoTracking();
            }
            else
            {
                records = recordManager.GetAllRecordsNoTracking(searchTerm);
            }
            registrosLabel.Text = records.Count.ToString();
            trainingRecordDataGridView.DataSource = records;
            RefreshControlState();
        }

        private void trainingRecordDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedRowCount = trainingRecordDataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                KnowledgeRecord selectedRecord = (KnowledgeRecord)trainingRecordDataGridView.SelectedRows[0].DataBoundItem;
                idTextBox.Text = selectedRecord.Id.ToString();
                titleTextBox.Text = selectedRecord.Content;
                contentTextBox.Text = selectedRecord.Title;
                selectedId = selectedRecord.Id;
            }

            RefreshControlState();
        }

        private void RefreshControlState()
        {
            if (!string.IsNullOrWhiteSpace(idTextBox.Text))
            {
                addRecordButton.Enabled = false;
                deleteRecordButton.Enabled = true;
                updateRecordButton.Enabled = true;
                clearRecordButton.Enabled = true;
            }

            if (string.IsNullOrWhiteSpace(idTextBox.Text))
            {
                addRecordButton.Enabled = true;
                deleteRecordButton.Enabled = false;
                updateRecordButton.Enabled = false;
                clearRecordButton.Enabled = false;
            }

            if (selectedId > 0)
            {
                // iterate through each row in the DataGridView
                foreach (DataGridViewRow row in trainingRecordDataGridView.Rows)
                {
                    // check if the row's "Id" cell value matches the desired ID
                    if (row.Cells["Id"].Value != null && (int)row.Cells["Id"].Value == selectedId)
                    {
                        // set the row's "Selected" property to true
                        row.Selected = true;
                        break; // exit the loop once the desired row is found
                    }
                }
            }

        }

        private void updateRecordButton_Click(object sender, EventArgs e)
        {
            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            int id = Convert.ToInt32(idTextBox.Text);
            if (id == 0)
            {
                RefreshList();
                return;
            }

            KnowledgeRecord record = recordManager.GetSingleRecord(id);
            record.Content = contentTextBox.Text.Trim();
            record.Title = titleTextBox.Text.Trim();
            recordManager.ModifyRecord(record, _configuration);

            selectedId = record.Id;
            RefreshList();

            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
        }

        private void deleteRecordButton_Click(object sender, EventArgs e)
        {
            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            int id = Convert.ToInt32(idTextBox.Text);
            if (id == 0)
            {
                RefreshList();
                return;
            }

            recordManager.DeleteRecord(id);
            ClearRecord();
            selectedId = 0;
            RefreshList();

            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
        }

        private void clearRecordButton_Click(object sender, EventArgs e)
        {
            ClearRecord();
            RefreshControlState();
        }

        private void ClearRecord()
        {
            idTextBox.Text = "";
            titleTextBox.Text = "";
            contentTextBox.Text = "";
            selectedId = 0;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            searchTerm = searchTextBox.Text.Trim(); ;
            RefreshList();
        }
    }
}
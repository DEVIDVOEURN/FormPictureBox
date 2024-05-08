using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FormPictureBox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("Position", "Position");
            dataGridView1.Columns.Add("Filename", "Filename");

            dataGridView1.CellClick += DataGridView1_CellClick;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                    pictureBox1.Tag = openFileDialog.FileName; 
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPosition.Text))
            {
                MessageBox.Show("Please enter both a name and a position.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells["Name"].Value = txtName.Text.Trim();
            dataGridView1.Rows[index].Cells["Position"].Value = txtPosition.Text.Trim();     
            dataGridView1.Rows[index].Cells["Filename"].Value = pictureBox1.Tag; 

            txtName.Clear();
            txtPosition.Clear();
            pictureBox1.Image = null;
            pictureBox1.Tag = null; 

            MessageBox.Show("Data added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtPosition.Text = row.Cells["Position"].Value.ToString();

                string filename = row.Cells["Filename"].Value?.ToString();
                if (!string.IsNullOrEmpty(filename) && File.Exists(filename))
                {
                    pictureBox1.Image = Image.FromFile(filename);
                    pictureBox1.Tag = filename; 
                }
                else
                {
                    pictureBox1.Image = null;
                    pictureBox1.Tag = null;
                }
            }
        }


    }
}
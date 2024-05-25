using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFTP1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            string host = txtHost.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            int port = int.Parse(txtPort.Text);
            string localFilePath = txtLocalFilePath.Text;
            string remoteFilePath = txtRemoteFilePath.Text;

            using (var sftp = new SftpClient(host, port, username, password))
            {
                try
                {
                    sftp.Connect();
                    using (var fileStream = new FileStream(localFilePath, FileMode.Open))
                    {
                        sftp.UploadFile(fileStream, remoteFilePath);
                    }
                    MessageBox.Show("File transfer successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sftp.Disconnect();
                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtLocalFilePath.Text = openFileDialog.FileName;
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            string host = "127.0.0.1"; // Replace with the actual host
            string username = txtUsername.Text; // Get username from text box
            string password = txtPassword.Text; // Get password from text box
            int port = int.Parse(txtPort.Text); // Get port from text box
            string remoteFilePath = txtRemoteFilePath.Text; // Get remote file path from text box
            string localFolderPath = @"C:\Users\win\Desktop\DOWN"; // Specify the destination folder path
            string localFilePath = Path.Combine(localFolderPath, Path.GetFileName(remoteFilePath)); // Define local file path for download
            using (var sftp = new SftpClient(host, port, username, password))
            {
                try
                {
                    sftp.Connect();
                    using (var fileStream = File.Create(localFilePath))
                    {
                        sftp.DownloadFile(remoteFilePath, fileStream);
                    }
                    MessageBox.Show("File downloaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sftp.Disconnect();
                }
            }
        }
        }
    }


using System;
using System.Windows.Forms;

namespace ChatBotWithLLM
{
    public class SettingsForm : Form
    {
        private TextBox txtApiKey;
        private TextBox txtApiUrl;
        private Button btnSave;
        private Button btnCancel;
        private Label lblApiKey;
        private Label lblApiUrl;
        private Label lblInfo;

        public string ApiKey { get; private set; }
        public string ApiUrl { get; private set; }

        public SettingsForm(string currentApiKey = "", string currentApiUrl = "")
        {
            ApiKey = currentApiKey;
            ApiUrl = currentApiUrl;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.txtApiUrl = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblApiKey = new System.Windows.Forms.Label();
            this.lblApiUrl = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            
            // 
            // lblApiKey
            // 
            this.lblApiKey.AutoSize = true;
            this.lblApiKey.Location = new System.Drawing.Point(12, 15);
            this.lblApiKey.Name = "lblApiKey";
            this.lblApiKey.Size = new System.Drawing.Size(74, 15);
            this.lblApiKey.Text = "Groq API Key:";
            // 
            // txtApiKey
            // 
            this.txtApiKey.Location = new System.Drawing.Point(12, 33);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.Size = new System.Drawing.Size(400, 23);
            this.txtApiKey.TabIndex = 0;
            this.txtApiKey.Text = ApiKey;
            this.txtApiKey.UseSystemPasswordChar = true;
            // 
            // lblApiUrl
            // 
            this.lblApiUrl.AutoSize = true;
            this.lblApiUrl.Location = new System.Drawing.Point(12, 65);
            this.lblApiUrl.Name = "lblApiUrl";
            this.lblApiUrl.Size = new System.Drawing.Size(54, 15);
            this.lblApiUrl.Text = "API URL:";
            // 
            // txtApiUrl
            // 
            this.txtApiUrl.Location = new System.Drawing.Point(12, 83);
            this.txtApiUrl.Name = "txtApiUrl";
            this.txtApiUrl.Size = new System.Drawing.Size(400, 23);
            this.txtApiUrl.TabIndex = 1;
            // Eski Gemini URL'lerini otomatik olarak Groq URL'ine çevir
            string url = ApiUrl;
            if (string.IsNullOrEmpty(url) || url.Contains("generativelanguage.googleapis.com") || url.Contains("gemini"))
            {
                url = "https://api.groq.com/openai/v1/chat/completions";
            }
            this.txtApiUrl.Text = url;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.Color.Green;
            this.lblInfo.Location = new System.Drawing.Point(12, 115);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(380, 30);
            this.lblInfo.Text = "Groq API anahtarı (ücretsiz): https://console.groq.com/keys\r\nModel: llama-3.3-70b-versatile (hızlı ve ücretsiz)";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(256, 155);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Kaydet";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(337, 155);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "İptal";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnSave;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(424, 197);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.txtApiUrl);
            this.Controls.Add(this.lblApiUrl);
            this.Controls.Add(this.txtApiKey);
            this.Controls.Add(this.lblApiKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Groq API Ayarları";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ApiKey = txtApiKey.Text.Trim();
            ApiUrl = txtApiUrl.Text.Trim();
            
            // API key kontrolü
            if (string.IsNullOrEmpty(ApiKey))
            {
                MessageBox.Show("Lütfen Groq API anahtarını girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Groq API key formatı kontrolü (gsk_ ile başlamalı)
            if (!ApiKey.StartsWith("gsk_"))
            {
                DialogResult result = MessageBox.Show(
                    "API key 'gsk_' ile başlamıyor. Bu doğru bir Groq API key'i mi?\n\nDevam etmek istiyor musunuz?",
                    "Uyarı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
                if (result == DialogResult.No)
                    return;
            }
            
            if (string.IsNullOrEmpty(ApiUrl))
                ApiUrl = "https://api.groq.com/openai/v1/chat/completions";

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

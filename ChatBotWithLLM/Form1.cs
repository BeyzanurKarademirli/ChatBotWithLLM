using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatBotWithLLM
{
    public partial class Form1 : Form
    {
        private LLMService _llmService;
        private List<ChatMessage> _conversationHistory;

        public Form1()
        {
            InitializeComponent();
            _llmService = new LLMService();
            _conversationHistory = new List<ChatMessage>();
            
            // Dark tema - Uzay temalı (koyu mavi-mor)
            Color spaceBg = Color.FromArgb(20, 20, 40);
            this.BackColor = spaceBg;
            
            // RichTextBox'u yarı saydam yapmak için özel ayar
            // Not: WinForms RichTextBox Transparent desteklemez, bu yüzden koyu renk kullanıyoruz
            txtChatHistory.BackColor = spaceBg;
            txtChatHistory.ForeColor = Color.White;
            
            // Panel'in arka planını görmek için RichTextBox'un arka planını kaldırmaya çalışıyoruz
            // Ama WinForms'ta bu mümkün değil, bu yüzden Panel'in Paint event'inde çizilen arka plan
            // RichTextBox'un arkasında kalacak ve görünmeyecek
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ZORLA DARK TEMA - Uzay temalı (koyu mavi-mor)
            Color spaceBg = Color.FromArgb(20, 20, 40);
            this.BackColor = spaceBg;
            txtChatHistory.BackColor = spaceBg; // Koyu arka plan
            txtChatHistory.ForeColor = Color.White;
            
            // RichTextBox'u ayarla (Enabled = true, ReadOnly = true, Cursor = Arrow)
            txtChatHistory.Enabled = true;
            txtChatHistory.ReadOnly = true;
            txtChatHistory.Cursor = Cursors.Arrow;
            txtChatHistory.SelectionStart = 0;
            if (txtChatHistory.TextLength > 0)
            {
                txtChatHistory.SelectionLength = txtChatHistory.TextLength;
                txtChatHistory.SelectionBackColor = spaceBg;
            }
            txtChatHistory.SelectionStart = txtChatHistory.TextLength;
            txtChatHistory.SelectionLength = 0;
            
            // Kaydedilmiş Groq ayarlarını yükle
            string apiKey = (Properties.Settings.Default.ApiKey ?? "").Trim();
            string apiUrl = (Properties.Settings.Default.ApiUrl ?? "https://api.groq.com/openai/v1/chat/completions").Trim();
            
            // Eski Gemini URL'lerini otomatik düzelt
            if (apiUrl.Contains("generativelanguage.googleapis.com") || apiUrl.Contains("gemini"))
            {
                apiUrl = "https://api.groq.com/openai/v1/chat/completions";
                Properties.Settings.Default.ApiUrl = apiUrl;
                Properties.Settings.Default.Save();
            }

            LLMProvider provider = LLMProvider.Groq;
            _llmService.SetProvider(provider, apiKey, apiUrl);

            if (string.IsNullOrEmpty(apiKey))
            {
                lblStatus.Text = "Groq API anahtarı ayarlanmamış. Lütfen ayarlardan API anahtarınızı girin.";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {
                lblStatus.Text = "Hazır (Groq)";
                lblStatus.ForeColor = Color.Green;
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            string userMessage = txtUserInput.Text.Trim();
            if (string.IsNullOrEmpty(userMessage))
                return;

            AddMessageToChat("user", userMessage);
            txtUserInput.Clear();

            // UI'ı güncelle
            btnSend.Enabled = false;
            txtUserInput.Enabled = false;
            lblStatus.Text = "Yanıt bekleniyor...";
            lblStatus.ForeColor = Color.Blue;

            try
            {
                // LLM'den yanıt al
                string response = await _llmService.SendMessageAsync(userMessage, _conversationHistory);

                // Assistant yanıtını ekle
                AddMessageToChat("assistant", response);

                // Conversation history'yi güncelle
                _conversationHistory.Add(new ChatMessage { Role = "user", Content = userMessage });
                _conversationHistory.Add(new ChatMessage { Role = "assistant", Content = response });

                lblStatus.Text = "Hazır (Groq)";
                lblStatus.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Hata: " + ex.Message;
                lblStatus.ForeColor = Color.Red;
                AddMessageToChat("system", $"Hata: {ex.Message}");
                
                // Limit aşıldıysa sohbeti durdur
                if (ex.Message.Contains("LİMİT AŞILDI"))
                {
                    btnSend.Enabled = false;
                    txtUserInput.Enabled = false;
                    MessageBox.Show(
                        "⚠️ ÜCRETSİZ KOTA DOLDU!\n\n" +
                        "Groq ücretsiz kotanız doldu. Sohbet durduruldu.\n\n" +
                        "Kota durumunu kontrol edin: https://console.groq.com\n\n" +
                        "Uygulamayı yeniden başlatarak tekrar deneyebilirsiniz.",
                        "Limit Aşıldı",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            finally
            {
                // Limit aşılmadıysa UI'ı tekrar aktif et
                if (!lblStatus.Text.Contains("LİMİT AŞILDI"))
                {
                    btnSend.Enabled = true;
                    txtUserInput.Enabled = true;
                    txtUserInput.Focus();
                }
            }
        }

        private void txtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                btnSend_Click(sender, e);
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            string currentApiKey = Properties.Settings.Default.ApiKey ?? "";
            string currentApiUrl = Properties.Settings.Default.ApiUrl ?? "https://api.groq.com/openai/v1/chat/completions";

            using (SettingsForm settingsForm = new SettingsForm(currentApiKey, currentApiUrl))
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.ApiKey = settingsForm.ApiKey;
                    Properties.Settings.Default.ApiUrl = settingsForm.ApiUrl;
                    Properties.Settings.Default.Save();

                    // Groq'u ayarla
                    LLMProvider provider = LLMProvider.Groq;
                    _llmService.SetProvider(provider, settingsForm.ApiKey, settingsForm.ApiUrl);

                    if (string.IsNullOrEmpty(settingsForm.ApiKey))
                    {
                        lblStatus.Text = "Groq API anahtarı ayarlanmamış. Lütfen ayarlardan API anahtarınızı girin.";
                        lblStatus.ForeColor = Color.Red;
                    }
                    else
                    {
                        lblStatus.Text = "Hazır (Groq)";
                        lblStatus.ForeColor = Color.Green;
                    }
                }
            }
        }

        private void AddMessageToChat(string role, string message)
        {
            if (string.IsNullOrEmpty(message))
                return;
                
            // RichTextBox'u aktif et (mesajların görünmesi için)
            txtChatHistory.Enabled = true;
            txtChatHistory.ReadOnly = false; // Geçici olarak yazılabilir yap
            
            // Arka plan rengi
            Color spaceBg = Color.FromArgb(20, 20, 40);
            txtChatHistory.BackColor = spaceBg;
            txtChatHistory.ForeColor = Color.White; // Varsayılan metin rengi
            
            // Renkler - Bordo-Gold teması (uzay temalı) - Çok daha parlak renkler
            Color botColor = Color.FromArgb(120, 120, 140); // Çok daha açık gri-mavi for bot
            Color userColor = Color.FromArgb(180, 0, 60); // Daha parlak maroon for user
            Color textColor = Color.White;

            int startPos = txtChatHistory.TextLength;

            if (role == "assistant")
            {
                // Bot mesajı - Sol hizalı, açık gri arka plan
                string fullMessage = "Bot: " + message + Environment.NewLine;
                
                txtChatHistory.SelectionStart = startPos;
                txtChatHistory.SelectionLength = 0;
                txtChatHistory.SelectionAlignment = HorizontalAlignment.Left;
                txtChatHistory.SelectionBackColor = botColor;
                txtChatHistory.SelectionColor = textColor;
                txtChatHistory.SelectedText = fullMessage;
                
                // Boş satır ekle
                txtChatHistory.SelectionStart = txtChatHistory.TextLength;
                txtChatHistory.SelectionLength = 0;
                txtChatHistory.SelectionBackColor = spaceBg;
                txtChatHistory.SelectionColor = Color.White;
                txtChatHistory.SelectedText = Environment.NewLine;
            }
            else if (role == "user")
            {
                // Kullanıcı mesajı - Sağ hizalı, maroon arka plan
                string fullMessage = "Siz: " + message + Environment.NewLine;
                
                txtChatHistory.SelectionStart = startPos;
                txtChatHistory.SelectionLength = 0;
                txtChatHistory.SelectionAlignment = HorizontalAlignment.Right;
                txtChatHistory.SelectionBackColor = userColor;
                txtChatHistory.SelectionColor = textColor;
                txtChatHistory.SelectedText = fullMessage;
                
                // Boş satır ekle
                txtChatHistory.SelectionStart = txtChatHistory.TextLength;
                txtChatHistory.SelectionLength = 0;
                txtChatHistory.SelectionBackColor = spaceBg;
                txtChatHistory.SelectionColor = Color.White;
                txtChatHistory.SelectedText = Environment.NewLine;
            }
            else
            {
                // Sistem mesajı
                txtChatHistory.SelectionStart = startPos;
                txtChatHistory.SelectionLength = 0;
                txtChatHistory.SelectionAlignment = HorizontalAlignment.Left;
                txtChatHistory.SelectionBackColor = spaceBg;
                txtChatHistory.SelectionColor = Color.Red;
                txtChatHistory.SelectedText = message + Environment.NewLine + Environment.NewLine;
            }

            // İmleci sona al ve scroll yap
            txtChatHistory.SelectionStart = txtChatHistory.TextLength;
            txtChatHistory.SelectionLength = 0;
            txtChatHistory.SelectionBackColor = spaceBg;
            txtChatHistory.SelectionColor = Color.White;
            txtChatHistory.ScrollToCaret();
            
            // RichTextBox'u yenile (mesajların görünmesi için)
            txtChatHistory.Invalidate();
            txtChatHistory.Refresh();
            txtChatHistory.Update();
            Application.DoEvents(); // UI'ı güncelle
            
            // Tekrar devre dışı bırak (imleç görünmesin)
            txtChatHistory.ReadOnly = true;
            txtChatHistory.Enabled = true; // Enabled = true kalacak (mesajların görünmesi için)
            txtChatHistory.Cursor = Cursors.Arrow; // Cursor = Arrow olduğu için imleç görünmez
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            // Form gösterildikten sonra arka planı tekrar ayarla (Windows tema ayarlarından etkilenmemesi için)
            Color spaceBg = Color.FromArgb(20, 20, 40);
            this.BackColor = spaceBg;
            txtChatHistory.BackColor = spaceBg;
            txtChatHistory.ForeColor = Color.White;
            txtChatHistory.Invalidate(); // CustomRichTextBox'un arka planını yeniden çiz
        }


        private void lblStatus_Click(object sender, EventArgs e)
        {

        }
    }
}

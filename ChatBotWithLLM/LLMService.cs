using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotWithLLM
{
    public enum LLMProvider
    {
        Groq  // Groq API - Ücretsiz tier, hızlı yanıtlar
    }

    public class LLMService
    {
        private readonly HttpClient _httpClient;
        private string _apiKey;
        private string _apiUrl;
        private LLMProvider _provider;

        public LLMService()
        {
            _httpClient = new HttpClient();
            _provider = LLMProvider.Groq;
            _apiUrl = "https://api.groq.com/openai/v1/chat/completions";
        }

        public void SetProvider(LLMProvider provider, string apiKey, string apiUrl = null)
        {
            _provider = provider;
            _apiKey = apiKey?.Trim();
            _apiUrl = apiUrl ?? "https://api.groq.com/openai/v1/chat/completions";
            
            _httpClient.DefaultRequestHeaders.Clear();
            if (!string.IsNullOrEmpty(_apiKey))
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
            }
        }

        public async Task<string> SendMessageAsync(string userMessage, List<ChatMessage> conversationHistory = null)
        {
            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new Exception("API anahtarı ayarlanmamış. Lütfen ayarlardan Groq API anahtarınızı girin.");
            }

            try
            {
                // Groq OpenAI uyumlu format kullanır
                string json = BuildJsonRequest(userMessage, conversationHistory, "llama-3.3-70b-versatile");
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                // Debug: API key'i kontrol et (ilk 10 karakter)
                System.Diagnostics.Debug.WriteLine($"API Key (first 10): {_apiKey?.Substring(0, Math.Min(10, _apiKey?.Length ?? 0))}...");
                System.Diagnostics.Debug.WriteLine($"API URL: {_apiUrl}");
                System.Diagnostics.Debug.WriteLine($"Request JSON: {json}");
                
                var response = await _httpClient.PostAsync(_apiUrl, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                
                System.Diagnostics.Debug.WriteLine($"Response Status: {response.StatusCode}");
                System.Diagnostics.Debug.WriteLine($"Response Content: {responseContent.Substring(0, Math.Min(500, responseContent.Length))}");

                if (!response.IsSuccessStatusCode)
                {
                    // Rate limit veya quota kontrolü (429 = TooManyRequests)
                    int statusCode = (int)response.StatusCode;
                    if (statusCode == 429 ||
                        response.StatusCode == System.Net.HttpStatusCode.PaymentRequired ||
                        responseContent.Contains("rate_limit_exceeded") ||
                        responseContent.Contains("insufficient_quota") ||
                        responseContent.Contains("quota_exceeded") ||
                        responseContent.Contains("billing_limit") ||
                        responseContent.Contains("429"))
                    {
                        throw new Exception("⚠️ LİMİT AŞILDI! Groq ücretsiz kotanız doldu. Sohbet durduruldu. Lütfen kontrol edin: https://console.groq.com");
                    }
                    
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || 
                        statusCode == 401 ||
                        responseContent.Contains("invalid_api_key") ||
                        responseContent.Contains("authentication") ||
                        responseContent.Contains("Unauthorized"))
                    {
                        throw new Exception($"❌ API anahtarı geçersiz! (Status: {statusCode})\n\nLütfen kontrol edin:\n1. API key'i doğru kopyaladınız mı?\n2. Başında/sonunda boşluk var mı?\n3. Yeni bir API key oluşturun: https://console.groq.com/keys\n\nHata detayı: {responseContent.Substring(0, Math.Min(200, responseContent.Length))}");
                    }
                    
                    throw new Exception($"Groq API Hatası: {response.StatusCode} - {responseContent.Substring(0, Math.Min(300, responseContent.Length))}");
                }

                string assistantMessage = ParseJsonResponse(responseContent);
                if (string.IsNullOrEmpty(assistantMessage))
                {
                    throw new Exception("Groq'dan geçerli bir yanıt alınamadı.");
                }
                return assistantMessage;
            }
            catch (Exception ex)
            {
                // Limit hatası ise özel mesaj göster
                if (ex.Message.Contains("LİMİT AŞILDI"))
                {
                    throw ex; // Özel mesajı olduğu gibi fırlat
                }
                throw new Exception($"Mesaj gönderilirken hata oluştu: {ex.Message}", ex);
            }
        }

        private string BuildJsonRequest(string userMessage, List<ChatMessage> conversationHistory, string modelName = "llama-3.3-70b-versatile")
        {
            var sb = new StringBuilder();
            sb.Append("{");
            sb.Append($"\"model\":\"{modelName}\",");
            sb.Append("\"messages\":[");

            // Conversation history
            if (conversationHistory != null && conversationHistory.Count > 0)
            {
                for (int i = 0; i < conversationHistory.Count; i++)
                {
                    if (i > 0) sb.Append(",");
                    sb.Append("{\"role\":\"");
                    sb.Append(EscapeJson(conversationHistory[i].Role));
                    sb.Append("\",\"content\":\"");
                    sb.Append(EscapeJson(conversationHistory[i].Content));
                    sb.Append("\"}");
                }
                sb.Append(",");
            }

            // Current user message
            sb.Append("{\"role\":\"user\",\"content\":\"");
            sb.Append(EscapeJson(userMessage));
            sb.Append("\"}");

            sb.Append("],");
            sb.Append("\"temperature\":0.7,");
            sb.Append("\"max_tokens\":1000");
            sb.Append("}");

            return sb.ToString();
        }

        private string ParseJsonResponse(string jsonResponse)
        {
            try
            {
                // Groq/OpenAI API response yapısı: {"choices":[{"message":{"content":"..."}}]}
                int choicesIndex = jsonResponse.IndexOf("\"choices\":[");
                if (choicesIndex == -1) return null;

                int messageIndex = jsonResponse.IndexOf("\"message\":{", choicesIndex);
                if (messageIndex == -1) return null;

                int contentIndex = jsonResponse.IndexOf("\"content\":\"", messageIndex);
                if (contentIndex == -1) return null;

                contentIndex += "\"content\":\"".Length;
                int endIndex = contentIndex;
                bool escaped = false;

                // String sonunu bul (escape karakterlerini dikkate alarak)
                while (endIndex < jsonResponse.Length)
                {
                    char c = jsonResponse[endIndex];
                    if (!escaped && c == '"')
                    {
                        break;
                    }
                    escaped = (c == '\\' && !escaped);
                    endIndex++;
                }

                if (endIndex > contentIndex)
                {
                    string content = jsonResponse.Substring(contentIndex, endIndex - contentIndex);
                    return UnescapeJson(content);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"JSON Parse Error: {ex.Message}");
            }

            return null;
        }

        private string EscapeJson(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            return text.Replace("\\", "\\\\")
                       .Replace("\"", "\\\"")
                       .Replace("\n", "\\n")
                       .Replace("\r", "\\r")
                       .Replace("\t", "\\t");
        }

        private string UnescapeJson(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            // Önce çift backslash'i değiştir, sonra diğer escape karakterlerini
            var result = new StringBuilder(text);
            result.Replace("\\\\", "\x01"); // Geçici placeholder
            result.Replace("\\\"", "\"");
            result.Replace("\\n", "\n");
            result.Replace("\\r", "\r");
            result.Replace("\\t", "\t");
            result.Replace("\x01", "\\"); // Backslash'i geri koy
            return result.ToString();
        }
    }

    public class ChatMessage
    {
        public string Role { get; set; } // "user" veya "assistant"
        public string Content { get; set; }
    }
}

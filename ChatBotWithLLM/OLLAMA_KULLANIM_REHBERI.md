# Ollama Kullanım Rehberi 🦙

## ✅ Ollama Kurulu! (1.2 GB)

Artık yerel, tamamen ücretsiz LLM kullanabilirsiniz! 🎉

---

## 📥 ADIM 1: Model İndirme

Ollama çalışıyor mu kontrol edin ve bir model indirin:

### Terminal/PowerShell'de:

```powershell
# Ollama çalışıyor mu kontrol et
ollama serve

# Basit ve küçük model (1.7 GB)
ollama pull llama2

# Veya daha güçlü model (3.8 GB)
ollama pull llama3

# Türkçe için iyi model (2.0 GB)
ollama pull mistral

# En küçük model (0.6 GB - hızlı ama düşük kalite)
ollama pull tinyllama
```

### Önerilen Model:
- **llama2** veya **llama3**: Genel kullanım için iyi
- **mistral**: Türkçe için daha iyi
- **tinyllama**: En hızlı ama kalitesi düşük

---

## 🔧 ADIM 2: Uygulamada Ayarlama

### 1. Uygulamayı Açın
- Projeyi çalıştırın (F5)

### 2. Ayarlara Gidin
- Sağ alt köşedeki **"Ayarlar"** butonuna tıklayın

### 3. Ollama Seçin
- "Sağlayıcı" dropdown'ından **"Ollama (Yerel/Ücretsiz)"** seçin

### 4. Model Seçin
- "Ollama Model" dropdown'ından indirdiğiniz modeli seçin
  - Örn: `llama2`, `llama3`, `mistral` vb.

### 5. Kaydedin
- **"Kaydet"** butonuna tıklayın

---

## 🧪 ADIM 3: Test Edin

1. Ollama'nın çalıştığından emin olun:
   - Terminal'de: `ollama list` → Model listelenmeli
   - Veya: `ollama serve` → Çalışıyor olmalı

2. Uygulamada mesaj yazın (örn: "Merhaba")

3. **"Gönder"** butonuna tıklayın

4. Yanıt gelmeli! ✅

---

## ❓ SORUN GİDERME

### Sorun: "Ollama Hatası: Connection refused"
**Çözüm:** 
- Terminal'de `ollama serve` çalıştırın
- Ollama'nın çalıştığından emin olun

### Sorun: "Model bulunamadı"
**Çözüm:**
- Terminal'de `ollama list` → Model listesini görün
- Model yoksa: `ollama pull llama2` (veya başka model)

### Sorun: Yanıt çok yavaş
**Çözüm:**
- Daha küçük model deneyin (örn: tinyllama)
- Daha güçlü bilgisayar gerekebilir

---

## 🎯 AVANTAJLAR

✅ **Tamamen Ücretsiz**
✅ **İnternet Gerektirmez**
✅ **Verileriniz Bilgisayarınızda Kalır**
✅ **Gizlilik Mükemmel**
✅ **Sınırsız Kullanım**

---

## 📝 NOTLAR

- İlk kullanım biraz yavaş olabilir (model yükleniyor)
- Model ne kadar büyükse o kadar kaliteli (ama daha yavaş)
- Bilgisayarınızın RAM'i yeterli olmalı (4GB+ önerilir)

**Başarılar! 🚀**


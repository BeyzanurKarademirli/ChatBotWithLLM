# TinyLlama Kurulum Rehberi 🚀

## ✅ ADIM ADIM KURULUM

### 1. Model İndirme

PowerShell veya Terminal'i açın ve şunu yazın:

```powershell
ollama pull tinyllama
```

**Bu komut:**
- TinyLlama modelini indirir (0.6 GB)
- Birkaç dakika sürebilir
- İndirme ilerlemesini gösterir

**Tamamlandığında:** "successfully pulled" mesajı görünür.

---

### 2. Ollama'yı Çalıştırma

**Yeni bir PowerShell/Terminal penceresi açın** ve:

```powershell
ollama serve
```

**ÖNEMLİ:** 
- Bu komut çalışırken bırakın (kapatmayın!)
- Arka planda çalışmalı
- "Ollama is running" gibi bir mesaj görürsünüz

---

### 3. Uygulamada Ayarlama

1. **Uygulamayı çalıştırın** (F5 veya Debug)

2. **Ayarlar butonuna tıklayın** (sağ alt köşe)

3. **"Sağlayıcı" dropdown'ından:**
   - **"Ollama (Yerel/Ücretsiz)"** seçin

4. **"Ollama Model" dropdown'ından:**
   - **"tinyllama"** seçin

5. **"Kaydet"** butonuna tıklayın

6. **Durum kontrolü:**
   - Durum çubuğunda **"Hazır (Ollama)"** yazmalı

---

### 4. Test Edin

1. Mesaj yazın (örn: "Merhaba, nasılsın?")
2. **"Gönder"** butonuna tıklayın
3. TinyLlama'dan yanıt gelmeli! ✅

---

## ⚡ TINYLLAMA ÖZELLİKLERİ

✅ **Çok Hızlı** (0.6 GB - küçük model)
✅ **Düşük RAM Kullanımı**
✅ **Hızlı Yanıtlar**
⚠️ **Kalite düşük** (basit sorular için yeterli)
⚠️ **Türkçe desteği sınırlı**

---

## ❓ SORUN GİDERME

### Sorun: "ollama: command not found"
**Çözüm:** 
- Ollama'nın kurulu olduğundan emin olun
- PowerShell'i yeniden başlatın
- Ollama'yı PATH'e ekleyin

### Sorun: "Ollama Hatası: Connection refused"
**Çözüm:**
- `ollama serve` çalıştırdığınızdan emin olun
- Yeni bir terminal açıp `ollama serve` yazın
- Çalışırken bırakın (kapatmayın!)

### Sorun: "Model bulunamadı"
**Çözüm:**
- `ollama pull tinyllama` komutunu tekrar çalıştırın
- İndirme tamamlanana kadar bekleyin
- `ollama list` ile model listesini kontrol edin

### Sorun: Yanıt gelmiyor
**Çözüm:**
- `ollama serve` çalışıyor mu kontrol edin
- Uygulamada model adının "tinyllama" olduğunu kontrol edin
- Uygulamayı yeniden başlatın

---

## 📝 NOTLAR

- **İlk kullanım:** Model yüklenirken biraz yavaş olabilir
- **Hız:** TinyLlama çok hızlı ama kalitesi düşük
- **Daha iyi kalite isterseniz:** `ollama pull llama2` (1.7 GB)

**Başarılar! 🎉**


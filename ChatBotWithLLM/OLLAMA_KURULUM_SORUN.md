# Ollama Kurulum Sorunu Çözümü 🔧

## ❌ SORUN: "ollama is not recognized"

Bu hata, Ollama'nın kurulu olmadığı veya PATH'e eklenmediği anlamına gelir.

---

## ✅ ÇÖZÜM: Ollama'yı Kurun

### ADIM 1: Ollama'yı İndirin ve Kurun

1. **Tarayıcıda açın:** https://ollama.com/download
2. **Windows için indirin:**
   - "Download for Windows" butonuna tıklayın
   - `OllamaSetup.exe` dosyası indirilecek

3. **Kurulum:**
   - İndirilen `OllamaSetup.exe` dosyasına çift tıklayın
   - Kurulum sihirbazını takip edin
   - "Install" butonuna tıklayın
   - Kurulum tamamlanana kadar bekleyin

4. **Kurulum sonrası:**
   - PowerShell'i **YENİDEN BAŞLATIN** (kapatıp açın)
   - PATH güncellenmesi için gerekli

---

### ADIM 2: Kurulumu Kontrol Edin

Yeni bir PowerShell açın ve:

```powershell
ollama --version
```

**Eğer versiyon görünüyorsa:** ✅ Kurulum başarılı!

**Eğer hala hata alıyorsanız:** Aşağıdaki adımları deneyin.

---

### ADIM 3: Manuel PATH Ekleme (Gerekirse)

Eğer hala çalışmıyorsa:

1. **Ollama'nın kurulu olduğu yeri bulun:**
   - Genellikle: `C:\Users\[KullanıcıAdı]\AppData\Local\Programs\Ollama`
   - Veya: `C:\Program Files\Ollama`

2. **PATH'e ekleyin:**
   - Windows tuşu + R → `sysdm.cpl` → Enter
   - "Advanced" sekmesi → "Environment Variables"
   - "System variables" altında "Path" seçin → "Edit"
   - "New" → Ollama klasörünün yolunu ekleyin
   - Tamam → Tamam → Tamam
   - PowerShell'i yeniden başlatın

---

### ADIM 4: Alternatif - Tam Yol ile Çalıştırma

PATH eklemek istemiyorsanız, tam yol ile çalıştırabilirsiniz:

```powershell
C:\Users\ASUS\AppData\Local\Programs\Ollama\ollama.exe pull tinyllama
```

(Önce Ollama'nın gerçekten bu yolda olup olmadığını kontrol edin)

---

## 🎯 HIZLI ÇÖZÜM

**En kolay yol:**

1. https://ollama.com/download → Windows için indir
2. `OllamaSetup.exe` çalıştır → Kur
3. PowerShell'i **YENİDEN BAŞLAT**
4. `ollama pull tinyllama` yaz

**Bu kadar!** 🚀

---

## ❓ HALA ÇALIŞMIYORSA

1. Ollama'yı kaldırıp yeniden kurun
2. Bilgisayarı yeniden başlatın
3. Yönetici olarak PowerShell açıp deneyin


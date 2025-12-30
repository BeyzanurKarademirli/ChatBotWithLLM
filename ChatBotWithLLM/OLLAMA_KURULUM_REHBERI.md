# Ollama Kurulum ve Kullanım Rehberi 🚀

## 📥 ADIM 1: Ollama İndirme ve Kurulum

### Windows için:
1. **Ollama'yı indirin:**
   - Tarayıcınızda şu adrese gidin: https://ollama.com/download
   - "Download for Windows" butonuna tıklayın
   - İndirilen `.exe` dosyasını çalıştırın

2. **Kurulum:**
   - Kurulum sihirbazını takip edin (Next, Next, Install)
   - Kurulum otomatik olarak tamamlanır
   - Ollama arka planda çalışmaya başlar

3. **Kontrol:**
   - Komut satırını (PowerShell veya CMD) açın
   - Şu komutu yazın: `ollama --version`
   - Versiyon numarasını görüyorsanız başarılı! ✅

---

## 📦 ADIM 2: Model İndirme

Ollama kurulduktan sonra, kullanmak istediğiniz modeli indirmeniz gerekir:

### Popüler Modeller:

#### 1. **Llama 2** (Önerilen - Orta boyut, iyi performans)
```bash
ollama pull llama2
```
- Boyut: ~3.8 GB
- Özellikler: Genel amaçlı, Türkçe destekli

#### 2. **Mistral** (Hafif ve hızlı)
```bash
ollama pull mistral
```
- Boyut: ~4.1 GB
- Özellikler: Çok hızlı, verimli

#### 3. **Code Llama** (Kod yazımı için)
```bash
ollama pull codellama
```
- Boyut: ~3.8 GB
- Özellikler: Programlama kodu üretimi

#### 4. **Türkçe Model** (Türkçe için optimize)
```bash
ollama pull llama2-uncensored
```
- Boyut: ~3.8 GB
- Özellikler: Türkçe için daha iyi

### Model İndirme İpuçları:
- İlk indirme biraz zaman alabilir (internet hızınıza bağlı)
- İndirilen modeller `C:\Users\[KullanıcıAdınız]\.ollama\models` klasöründe saklanır
- Birden fazla model indirebilirsiniz

---

## ✅ ADIM 3: Ollama'nın Çalıştığını Kontrol Etme

### Yöntem 1: Komut Satırı
```bash
ollama list
```
İndirdiğiniz modelleri gösterir.

### Yöntem 2: Tarayıcı
Tarayıcınızda şu adrese gidin: http://localhost:11434
Eğer bir sayfa görüyorsanız Ollama çalışıyor! ✅

### Yöntem 3: Test Sorgusu
```bash
ollama run llama2 "Merhaba, nasılsın?"
```
Eğer cevap alıyorsanız her şey hazır! ✅

---

## 🎮 ADIM 4: ChatBot Uygulamanızda Kullanma

### 1. Ollama'yı Başlatın:
- Ollama zaten çalışıyorsa bir şey yapmanıza gerek yok
- Eğer çalışmıyorsa: Başlat menüsünden "Ollama" programını çalıştırın

### 2. ChatBot Uygulamanızı Açın:
- Uygulamayı çalıştırın
- Sağ alt köşedeki **"Ayarlar"** butonuna tıklayın

### 3. Provider Seçin:
- **"Sağlayıcı"** dropdown'ından **"Ollama (Yerel/Ücretsiz)"** seçin
- API Anahtarı alanı otomatik olarak devre dışı kalacak (Ollama API key gerektirmez)
- API URL: `http://localhost:11434/api/chat` olarak ayarlanacak

### 4. Kaydet ve Kullan:
- **"Kaydet"** butonuna tıklayın
- Artık sohbet başlatabilirsiniz!

---

## 🔧 Sorun Giderme

### Ollama çalışmıyor:
1. Görev Yöneticisini açın (Ctrl+Shift+Esc)
2. "Ollama" işlemini arayın
3. Yoksa: Başlat menüsünden Ollama'yı çalıştırın

### Model bulunamıyor hatası:
```bash
ollama list
```
komutu ile indirilen modelleri kontrol edin. Model yoksa tekrar indirin.

### Port 11434 kullanımda hatası:
- Başka bir program bu portu kullanıyor olabilir
- Ollama'yı kapatıp tekrar açın
- Bilgisayarı yeniden başlatın

### Yavaş yanıtlar:
- Model ilk çalıştırmada yavaş olabilir (RAM'e yükleniyor)
- Daha küçük bir model deneyin (mistral gibi)
- Bilgisayarınızın RAM'ini kontrol edin (en az 8GB önerilir)

---

## 💡 İpuçları

1. **İlk kullanım:** Model ilk çalıştırmada yavaş olabilir, sabırlı olun
2. **RAM:** Ollama modeller RAM'de çalışır, yeterli RAM olduğundan emin olun
3. **Birden fazla model:** İstediğiniz kadar model indirebilirsiniz
4. **Offline çalışır:** İnternet bağlantısı gerekmez (indirme sonrası)
5. **Ücretsiz:** Tüm modeller tamamen ücretsiz!

---

## 📚 Ekstra Kaynaklar

- **Ollama Resmi Site:** https://ollama.com
- **Model Galerisi:** https://ollama.com/library
- **Dokümantasyon:** https://github.com/ollama/ollama

---

**Hazırsınız! Artık kendi yerel LLM'inizle sohbet edebilirsiniz! 🎉**


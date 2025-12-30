# Gemini API Detaylı Kullanım Rehberi 🔍

## ❌ SORUN: Model Bulunamıyor Hatası

Bu hata genellikle şu nedenlerden kaynaklanır:
1. **Yanlış API versiyonu** (v1 yerine v1beta kullanılmalı)
2. **Yanlış model adı** (gemini-1.5-flash yerine başka bir model)
3. **Yanlış endpoint formatı**

---

## ✅ DOĞRU KULLANIM

### 1. API Key Alma (Adım Adım)

#### Adım 1: Google AI Studio'ya Gidin
- Tarayıcıda: https://aistudio.google.com/app/apikey
- Google hesabınızla giriş yapın

#### Adım 2: Proje Seçin veya Oluşturun
- Eğer proje yoksa: "Create Project" veya "Proje Oluştur"
- Mevcut bir projeyi seçin

#### Adım 3: API Key Oluşturun
- "Create API Key" veya "API anahtarı oluşturun" butonuna tıklayın
- **ÖNEMLİ:** Key'i hemen kopyalayın (tekrar gösterilmez!)

#### Adım 4: Key Formatı Kontrolü
- Key uzun bir string olmalı (örn: `AIzaSyCQr_PTgYmZIURIn6XDCssp6Sc8WNL90xM`)
- Başında/sonunda boşluk olmamalı
- Tüm karakterler dahil olmalı

---

### 2. API Endpoint ve Model Adları

#### ✅ DOĞRU Format:
```
https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key=YOUR_API_KEY
```

#### ❌ YANLIŞ Formatlar:
- `v1/models/...` ❌ (v1beta kullanılmalı)
- `gemini-pro` ❌ (artık desteklenmiyor)
- `gemini-1.5-pro` ❌ (bazı durumlarda çalışmıyor)

#### ✅ Çalışan Model Adları:
1. **gemini-1.5-flash** (Önerilen - Hızlı ve ücretsiz)
2. **gemini-1.5-pro** (Daha güçlü ama yavaş)
3. **gemini-2.0-flash-exp** (Deneysel)

---

### 3. API Key Kontrolü

#### Key'in Geçerli Olduğunu Nasıl Anlarsınız?

**Yöntem 1: Tarayıcıda Test**
1. Tarayıcıda şu URL'i açın (YOUR_API_KEY yerine key'inizi yazın):
```
https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key=YOUR_API_KEY
```
2. Eğer bir JSON yanıt görüyorsanız key geçerli ✅
3. Eğer "API key not valid" görüyorsanız key geçersiz ❌

**Yöntem 2: cURL ile Test (PowerShell)**
```powershell
curl -X POST "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key=YOUR_API_KEY" -H "Content-Type: application/json" -d '{\"contents\":[{\"parts\":[{\"text\":\"Merhaba\"}]}]}'
```

---

### 4. Sık Yapılan Hatalar

#### ❌ Hata 1: Key'i Yanlış Kopyalama
- Key'in başında/sonunda boşluk olabilir
- **Çözüm:** Key'i temizleyin (Trim)

#### ❌ Hata 2: Yanlış API Versiyonu
- `v1` yerine `v1beta` kullanılmalı
- **Çözüm:** Endpoint'i kontrol edin

#### ❌ Hata 3: Model Adı Yanlış
- Eski model adları kullanılıyor olabilir
- **Çözüm:** `gemini-1.5-flash` kullanın

#### ❌ Hata 4: Key'in İzinleri Yok
- Key oluşturulurken yanlış proje seçilmiş olabilir
- **Çözüm:** Yeni key oluşturun

---

### 5. Adım Adım Çözüm

1. **Yeni API Key Oluşturun:**
   - https://aistudio.google.com/app/apikey
   - Eski key'i silin, yeni oluşturun

2. **Key'i Kontrol Edin:**
   - Key'in tamamını kopyaladığınızdan emin olun
   - Başında/sonunda boşluk olmamalı

3. **Uygulamada Ayarlayın:**
   - Ayarlar → Google Gemini
   - Yeni key'i yapıştırın
   - URL'in `v1beta/models/gemini-1.5-flash` içerdiğinden emin olun

4. **Test Edin:**
   - Basit bir mesaj gönderin (örn: "merhaba")
   - Hata alırsanız hata mesajını kontrol edin

---

### 6. Alternatif: DeepSeek Kullanın

Gemini'de sorun yaşıyorsanız, **DeepSeek** kullanabilirsiniz:
- ✅ Daha kolay kurulum
- ✅ OpenAI uyumlu (sorunsuz çalışır)
- ✅ Ücretsiz
- ✅ Hızlı

**DeepSeek API Key:** https://platform.deepseek.com/api_keys

---

## 📞 Yardım

Hala sorun yaşıyorsanız:
1. Hata mesajının tamamını paylaşın
2. API key'in ilk 5 ve son 5 karakterini paylaşın (güvenlik için tam key değil)
3. Hangi adımda takıldığınızı belirtin


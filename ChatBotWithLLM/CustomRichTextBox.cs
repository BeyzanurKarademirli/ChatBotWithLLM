using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ChatBotWithLLM
{
    public class CustomRichTextBox : RichTextBox
    {
        protected override void WndProc(ref Message m)
        {
            // WM_PAINT mesajını yakala
            const int WM_PAINT = 0x000F;
            if (m.Msg == WM_PAINT)
            {
                // Önce arka planı çiz
                DrawBackground();
                // Sonra normal paint işlemini yap
                base.WndProc(ref m);
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        private void DrawBackground()
        {
            using (Graphics g = this.CreateGraphics())
            {
                Rectangle rect = this.ClientRectangle;
                
                // Gradient arka plan (koyu mavi-mor)
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    rect,
                    Color.FromArgb(20, 20, 40),  // Koyu mavi-mor
                    Color.FromArgb(30, 20, 50),  // Biraz daha açık
                    LinearGradientMode.Vertical))
                {
                    g.FillRectangle(brush, rect);
                }
                
                // Yıldızlar ekle
                Random rnd = new Random(123); // Sabit seed (tutarlı görünüm için)
                using (SolidBrush starBrush = new SolidBrush(Color.White))
                {
                    for (int i = 0; i < 150; i++)
                    {
                        int x = rnd.Next(0, rect.Width);
                        int y = rnd.Next(0, rect.Height);
                        int size = rnd.Next(1, 3);
                        g.FillEllipse(starBrush, x, y, size, size);
                    }
                }
                
                // Satürn gezegeni çiz (sağ üst köşede)
                int planetX = rect.Width - 200;
                int planetY = 50;
                int planetSize = 120;
                
                // Gezegen gölgesi
                using (SolidBrush planetBrush = new SolidBrush(Color.FromArgb(100, 60, 30, 50)))
                {
                    g.FillEllipse(planetBrush, planetX - 10, planetY + 10, planetSize, planetSize);
                }
                
                // Gezegen (gradient)
                Rectangle planetRect = new Rectangle(planetX, planetY, planetSize, planetSize);
                using (LinearGradientBrush planetGradient = new LinearGradientBrush(
                    planetRect,
                    Color.FromArgb(150, 80, 40),  // Turuncu-kırmızı
                    Color.FromArgb(100, 50, 80),  // Mor
                    LinearGradientMode.Horizontal))
                {
                    g.FillEllipse(planetGradient, planetRect);
                }
                
                // Gezegen halkaları (Satürn)
                using (Pen ringPen = new Pen(Color.FromArgb(200, 255, 215, 0), 3)) // Gold
                {
                    // Üst halka
                    g.DrawEllipse(ringPen, planetX - 20, planetY + 20, planetSize + 40, 30);
                    // Orta halka
                    g.DrawEllipse(ringPen, planetX - 15, planetY + 40, planetSize + 30, 25);
                    // Alt halka
                    g.DrawEllipse(ringPen, planetX - 10, planetY + 60, planetSize + 20, 20);
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Arka planı özel çiz
            Rectangle rect = this.ClientRectangle;
            
            // Gradient arka plan (koyu mavi-mor)
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect,
                Color.FromArgb(20, 20, 40),  // Koyu mavi-mor
                Color.FromArgb(30, 20, 50),  // Biraz daha açık
                LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
            
            // Yıldızlar ekle
            Random rnd = new Random(123); // Sabit seed (tutarlı görünüm için)
            using (SolidBrush starBrush = new SolidBrush(Color.White))
            {
                for (int i = 0; i < 150; i++)
                {
                    int x = rnd.Next(0, rect.Width);
                    int y = rnd.Next(0, rect.Height);
                    int size = rnd.Next(1, 3);
                    e.Graphics.FillEllipse(starBrush, x, y, size, size);
                }
            }
            
            // Satürn gezegeni çiz (sağ üst köşede)
            int planetX = rect.Width - 200;
            int planetY = 50;
            int planetSize = 120;
            
            // Gezegen gölgesi
            using (SolidBrush planetBrush = new SolidBrush(Color.FromArgb(100, 60, 30, 50)))
            {
                e.Graphics.FillEllipse(planetBrush, planetX - 10, planetY + 10, planetSize, planetSize);
            }
            
            // Gezegen (gradient)
            Rectangle planetRect = new Rectangle(planetX, planetY, planetSize, planetSize);
            using (LinearGradientBrush planetGradient = new LinearGradientBrush(
                planetRect,
                Color.FromArgb(150, 80, 40),  // Turuncu-kırmızı
                Color.FromArgb(100, 50, 80),  // Mor
                LinearGradientMode.Horizontal))
            {
                e.Graphics.FillEllipse(planetGradient, planetRect);
            }
            
            // Gezegen halkaları (Satürn)
            using (Pen ringPen = new Pen(Color.FromArgb(200, 255, 215, 0), 3)) // Gold
            {
                // Üst halka
                e.Graphics.DrawEllipse(ringPen, planetX - 20, planetY + 20, planetSize + 40, 30);
                // Orta halka
                e.Graphics.DrawEllipse(ringPen, planetX - 15, planetY + 40, planetSize + 30, 25);
                // Alt halka
                e.Graphics.DrawEllipse(ringPen, planetX - 10, planetY + 60, planetSize + 20, 20);
            }
        }
    }
}


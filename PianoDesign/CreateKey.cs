using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PianoDesign
{
    /// <summary>
    /// Form'un içinde bulunan bütün nesneleri oluşturmak için açılmış sınıf
    /// </summary>
    class CreateKey
    {
        /// <summary>
        /// Dinamik olarak özelleştirilmiş siyah butonlar oluşturuyor
        /// </summary>
        /// <param name="_width"> siyah butonun genişliğini sağlayan değişken </param>
        /// <param name="_height"> siyah butonun yüksekliğini sağlayan değişken</param>
        /// <param name="_whiteKeyWidth"> beyaz butonun genişliğini alan değişken ancak burada siyah butonların konumlandırılmasında kullanılıyor</param>
        /// <param name="bosluk"> siyah tuşlar arasındaki boşluğuk değerini sağlayan değişken </param>
        /// <param name="blackKeyNode"> dizi olarak siyah tuşların sırasıyla hangi ismi alacağını belirleyen değişken</param>
        /// <param name="panel"> Panel sınıfından oluşan panel adında bir panel</param>
        public void CreateBlackKey(int _width, int _height, int _whiteKeyWidth, int bosluk, string[] blackKeyNode, Panel panel)
        {
            for (int i = 0; i < blackKeyNode.Length; i++)//siyah butonların istenilen adette oluşmasını sağlayan döngü
            {
                if (blackKeyNode[i] != "_")//beyaz ve siyah butonlar oluşturulurken blackKeyNode "_" simgesine eşit olmadığı zamanlarda çalışmasını sağlıyor
                {
                    Button blackKey = new Button(); //blackKey adında buton oluşturuluyor
                    blackKey.Size = new Size(_width, _height); //blackKey butonunun boyutları atanıyor
                    blackKey.BackColor = Color.Black; //butona arkaplan rengi ataması yapılıyor
                    blackKey.ForeColor = Color.White; //butona önplan rengi ataması yapılıyor
                    blackKey.Name = blackKeyNode[i].ToString();//blackKeyNode dizisinin nota isimleri string'e çevrilerek blackKey butonunun adına atılıyor
                    int B = blackKey.Location.X + (i * _whiteKeyWidth) + bosluk; //B değişkenine atanmak üzere siyah butonların konumu için işlem yapılıyor
                    blackKey.Location = new Point(B, blackKey.Location.Y); /*oluşturulan B değişkeni x ekseni için ve otamatik olarak y ekseni için de değer atandıktan
                                                                            * sonra siyah butonun lokasyonu nihai olarak belirleniyor*/
                    blackKey.Text = blackKeyNode[i];//butonun text kısmına nota bilgisi atanıyor
                    blackKey.Click += PlaySound; //butona tıklandığı zaman olacak olay için PlaySound adındaki metod çağırılıyor
                    panel.Controls.Add(blackKey); //panel içerisine blackKey adındaki buton ekleniyor
                }
            }
        }

        /// <summary>
        /// Dinamik olarak özelleştirilmiş beyaz butonlar oluşturuyor
        /// </summary>
        /// <param name="_width">beyaz butonun genişliğini sağlayan değişken</param>
        /// <param name="_height">beyaz butonun yüksekliğini sağlayan değişken</param>
        /// <param name="whiteKeyNode">dizi olarak beyaz tuşların sırasıyla hangi ismi alacağını belirleyen değişken</param>
        /// <param name="panel">Panel sınıfından oluşan panel adında bir panel</param>
        public void CreatWhiteKey(int _width, int _height, string[] whiteKeyNode, Panel panel)
        {
            for (int i = 0; i < whiteKeyNode.Length; i++)// beyaz butonların istenilen adette oluşmasını sağlayan döngü
            {
                Button whiteKey = new Button(); //whiteKey adında buton oluşturuluyor
                whiteKey.Name = whiteKeyNode[i].ToString(); //whiteKeyNode dizisinin nota isimleri string'e çevrilerek whiteKey butonunun adına atılıyor
                whiteKey.Text = whiteKeyNode[i]; //whiteKeyNode dizisinin nora isimleri kullanıcının görmesi için butonun üzerine atanıyor
                whiteKey.BackColor = Color.White; //butona arkaplan rengi ataması yapılıyor
                whiteKey.ForeColor = Color.Black; //butona önplan rengi ataması yapılıyor
                whiteKey.TextAlign = ContentAlignment.BottomCenter; //butonun üzerindeki yazı için hizalama işlemi uygulanıyor
                whiteKey.Size = new Size(_width, _height); //buton için boyutlandırma işlemi uygulanıyor
                int W = whiteKey.Location.X + (i * _width); //butonun konumu için gerekli işlemlerin sonucu w değişkenine atanıyor
                whiteKey.Location = new Point(W, whiteKey.Location.Y); //buton konumlandırması yapılıyor
                whiteKey.Click += PlaySound; //butona tıklandığı zaman olacak olay için PlaySound adındaki metod çağırılıyor
                panel.Controls.Add(whiteKey); //panel içerisine whiteKey adındaki buton ekleniyor
            }
        }

        /// <summary>
        /// buton tıklandığında kaynaktan müzik dosyasını alıp çalan metot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PlaySound(object sender, EventArgs e)
        {
            Button Btnsound = (Button)sender; //sender değişkeni ile basılan butonun bütün özellikleri Btnsound butonuna atılıyor
            Stream str = (Stream)Properties.Resources.ResourceManager.GetObject(Btnsound.Name.ToString().Replace("#", "_")); 
            // Btnsound üzerinde yazan yazıyı gerekli işlemler yaparak ses klasöründe bulunan ses dosyalarından aynı isimde olanı alıp str değişkenine atıyor 
            SoundPlayer sound = new SoundPlayer(str); //str nin tuttuğu veriyi SoundPlayer sınıfından üretilan sound nesnesine atıyoruz
            sound.Play(); //sound nesnesinin tuttuğu sesi çalması sağlanıyor
        }

        /// <summary>
        /// müzik seçiminde seçilen müziğin notalarının her biri bu metot üzerinden çalınır
        /// </summary>
        /// <param name="_nota">seçilen müzikteki her bir notanın adını tek tek alan değişken</param>
        public void MusicPlayer(string _nota)
        {
            //verilen nota ismi # değerini içeriyor fakat resources da # işareti yerine _ var. Bu yüzden replace ile ikisi değiştirilerek işlem yapılır.
            Stream str = (Stream)Properties.Resources.ResourceManager.GetObject(_nota.Replace("#", "_")); 
            //gelen nota adını klasörde aynı isimde bulunan ses dosyasını bulmak için kullanıp str değişkenine atıyor
            SoundPlayer sound = new SoundPlayer(str); //str nin tuttuğu veriyi SounPlayer sınıfından üretilen sound nesnesine atıyor
            sound.Play(); //sound nesnesinin tuttuğu sesi çalması sağlanıyor
        }

        /// <summary>
        /// şarkı listesinden seçilen şarkının otamatik olarak çalınırken uygulamanın hangi tuşlara bastığını kullanıcının görmesi için 
        /// oluşturulan, butonların rengini değiştiren metod
        /// </summary>
        /// <param name="btnName">butonun adını alan değişken</param>
        /// <param name="bcolor">butonun arkaplan rengini alan değişken</param>
        /// <param name="fcolor">butonun önplan rengini alan değişken</param>
        /// <param name="panel">Panel sınıfından oluşan panel adında bir panel</param>
        public void SetButtonColor(string btnName, Color bcolor, Color fcolor, Panel panel)
        {
            Button btn = null; //boş bir buton nesnesi oluşturuluyor
            if (panel.Controls.ContainsKey(btnName)) //btnName nin tuttuğu değer panelde olduğu durum için geçerli
            {
                btn = panel.Controls[btnName] as Button; //btn butonu panel içindeki btnName adındaki butonun yerine geçiyor
            }

            if (null != btn) //btn nin boş olmadığı durum için geçerli
            {
                btn.BackColor = bcolor; //btn nin arka ve ön plan renklerine bu metodu çağıran metod tarafından gönderilen renkler atanıyor
                btn.ForeColor = fcolor;
            }
        }

        /// <summary>
        /// seçilen şarkılardaki notaların her birisinde bu metod çalışır ve notanın çalınmasına ve butonun renk değiştirmesine aracılık eder
        /// </summary>
        /// <param name="nota">şarkıdan gelen notaların atandığı değişken</param>
        /// <param name="durak">şarkıdaki bekleme süresinin atandığı değişken</param>
        /// <param name="panel">Panel sınıfından oluşan panel adında bir panel</param>
        public void PlayMusicPlayer(string nota, int durak, Panel panel)
        {
            MusicPlayer(nota); //gelen nota değerini MusicPlayer adlı metodu çağırarak ona gönderir

            if (nota.Contains("#")) //notanın içinde # simgesi olup olmadığını kontrol eden yapı, var olduğu durum için geçerli
            {
                SetButtonColor(nota, Color.Gray, Color.White, panel); //SetButtonColor metodunu çağırıp renk nota ve panel bilgilerini göndermekte
                Task.Delay(125 * durak).Wait(); //gelen durak değerine oranlı bir şekilde beklemeyi sağlar
                SetButtonColor(nota, Color.Black, Color.White, panel); //SetButtonColor metodunu çağırıp renk nota ve panel bilgilerini göndermekte
            }
            else //notada # olmadığı durumlar için geçerli
            {
                SetButtonColor(nota, Color.Gray, Color.White, panel); //SetButtonColor metodunu çağırıp renk nota ve panel bilgilerini göndermekte
                Task.Delay(125 * durak).Wait(); //gelen durak değerine oranlı bir şekilde beklemeyi sağlar
                SetButtonColor(nota, Color.White, Color.Black, panel); //SetButtonColor metodunu çağırıp renk nota ve panel bilgilerini göndermekte
            }
        }

        /* ----Finalde yapacağımız geliştirmeler için kullanılacak bir alan bu yüzden yorum satırı haline getirildi----
        private void createLabelWhite(Panel panel)
        {
            Label[] label11 = new Label[11];
            string[] tuslar = { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "M" };
            for (int i = 0; i < 11; i++)
            {
                label11[i] = new Label();
                label11[i].Size = new Size(79, 40);
                label11[i].Location = new Point(37 + 81 * i, 475);
                label11[i].Text = tuslar[i];
                label11[i].BackColor = Color.Gray;
                label11[i].ForeColor = Color.Black;
                label11[i].TextAlign = ContentAlignment.MiddleCenter;
            }
            for (int i = 0; i < 11; i++)
            {
                panel.Controls.Add(label11[i]);
            }
        }

        private void createLabelBlack(Panel panel)
        {
            Label[] label7 = new Label[9];
            string[] tuslar = { "2", "3", "_", "5", "6", "7", "_", "9", "0" };
            for (int i = 0; i < 9; i++)
            {
                if (!tuslar[i].Contains("_"))
                {
                    label7[i] = new Label();
                    label7[i].Size = new Size(79, 40);
                    label7[i].BackColor = Color.Gray;
                    label7[i].ForeColor = Color.Black;
                    label7[i].TextAlign = ContentAlignment.MiddleCenter;
                    label7[i].Location = new Point(37 + 40 + 81 * i, 475 + 40 + 2);
                    label7[i].Text = tuslar[i];
                }
                else
                {
                    label7[i] = null;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                if (!tuslar[i].Contains("_"))
                {
                    panel.Controls.Add(label7[i]);
                }
            }
        } */

        bool Bayrak; //panelin görünür veya görünmez olmasına karar verecek olan değeri tutan değişken
        Panel panel; //Panel sınıfından oluşturulan panel nesnesi. Bu panel piyano butonlarını dinamik olarak içinde tutmak için oluşturuluyor

        /// <summary>
        /// panel oluşturmaya ve gerekli özelliklerini atmaya yarayan metod
        /// </summary>
        /// <param name="panelWidth">panelin genişliğini tutar</param>
        /// <param name="panelHeight">panelin yüksekliğini tutar</param>
        /// <param name="panelLocationX">panelin form içinde bulanacağı x düzlemindeki konumunu gösterir</param>
        /// <param name="panelLocationY">panelin form içinde bulunacağı y düzlemindeki konumunu gösterir</param>
        /// <param name="form">Form sınıfından oluşturulan form nesnesi. Bütün işlemler bu form üzerinde tutulacak</param>
        /// <param name="isim">panel adını tutan değişken</param>
        /// <param name="bC">arkaplan rengini tutan değişken</param>
        /// <param name="bayrak">panelin görünür veya görünmez olacağının if yapısı için kullanılan değişken finalde daha etkili bir kullanımı olacaktır</param>
        /// <returns></returns>
        public Panel createPanel(int panelWidth, int panelHeight, int panelLocationX, int panelLocationY, Form form, string isim, Color bC, bool bayrak)
        {
            panel = new Panel(); //panel nesnesi oluşturulur
            panel.Size = new Size(panelWidth, panelHeight); //panelin en boy değerlerinin ataması yapılır
            panel.Location = new Point(panelLocationX, panelLocationY); //panelin konum değerlerinin ataması yapılır
            panel.Name = isim.ToString(); //panelin isim değerinin ataması yapılır
            panel.BackColor = bC; //panelin arkaplan değerinin ataması yapılır
            Bayrak = bayrak; //metod içindeki bayrak değişkeninin değeri metod dışındaki Bayrak değişkenine atanır
            if (Bayrak) // Bayrak değişkenine göre if yapısı oluşturulur, bu yapı Bayrak true ise geçerlidir. Ayrıca panelin görünür veya görünmez olmasına karar verir pozisyondadır
            {
                panel.Visible = true; //panelin kullanıcı tarafından görünür olmasını sağlar
            }
            else panel.Visible = false; //if yapısının geçerli olmadığı durumda else yapısı çalışır ve panelin kullanıcı tarafından görünmez olmasını sağlar
            form.Controls.Add(panel); //form içinde gerekli özellikleri atanan panelin oluşturulmasını sağlar
            return form.Controls[panel.Name] as Panel; //panel.name isiminde olan paneli panel olarak geri döndürme işlemi yapılır

        }

        Label label; //form üzerinde yazı yazdırabilmek için kullanılan eleman

        /// <summary>
        /// form üzerinde yazı yazdırabilmek ve yazının gerekli özelliklerini atayabilmek için kullanılan metod. form'un orta kısımları için işlevlidir
        /// </summary>
        /// <param name="panel">elemanların içine yerleştirilebilmesi için oluşturulan panel</param>
        /// <param name="lblText">label içine yazılacak olan yazıyı tutan değişken</param>
        /// <param name="lblLocationX">labelin konum değerini sağlayacak olan eleman</param>
        /// <param name="lblSizeX">labelin boyut değerini sağlayacak eleman</param>
        /// <returns></returns>
        public Label createLabelH(Panel panel, string lblText, int lblLocationX, int lblSizeX)
        {
            label = new Label(); //label adında nesne oluşturulur. Ekranda yazı gösterir

            label.Text = lblText; //lblText e gelen değeri labelin text kısmına atar
            label.Name = lblText; //lblText e gelen değeri labelin Name kısmına atar
            label.Size = new Size(lblSizeX, 30); //labelin boyut değerleri atanır
            label.Font = new Font("Arial", 12); //labelin içindeki yazının font özellikleri atanır
            label.ForeColor = Color.LightGray; //labelin renk ataması yapılır
            label.TextAlign = ContentAlignment.MiddleLeft; //labelin içindeki yazının orta sol kısma hizalanması sağlanır
            label.Location = new Point(lblLocationX, 12); //labelin panel içinde konumlandırması yapılır

            label.MouseEnter += mouseEnter; //fare labelin üzerine geldiği anda çalışacak olan metod mouseEnter'dir.
            label.MouseLeave += mouseLeave; //fare labelin üzerinden ayrıldığı anda çalışacak olan metod mouseLeave'dir

            panel.Controls.Add(label); //panel içine label eklenir
            return panel.Controls[label.Name] as Label; //label.Name ismindeki labeli label olarak geri döndürme işlemi yapar
        }


        /// <summary>
        /// müzik seçim kısmındaki "select music" ve "play music" label'larını oluşturan metod
        /// </summary>
        /// <param name="panel">nesneleri içinde tutacak panel</param>
        /// <param name="lblText">labelin üzerinde yazan yazıyı alan değişken</param>
        /// <param name="lblLocationX">labelin x eksenindeki konum bilgisini alan değişken</param>
        /// <param name="lblLocationY">labelin y eksenindeki konum bilgisini alan değişken<</param>
        /// <param name="lblSizeX">labelin x eksenindeki boyut deerini alan değişken</param>
        /// <param name="lblSizeY">labelin y eksenindeki boyut deerini alan değişken</param>
        /// <param name="bgC">arka plan rengini alan değişken</param>
        /// <param name="fgC">ön plan rengini alan değişken</param>
        /// <param name="fontSize">label içinde yazan yazının boyutunu alan değişken</param>
        /// <param name="lblName">labelin adını tutan değişken</param>
        /// <returns></returns>
        public Label createLabelN(Panel panel, string lblText, int lblLocationX, int lblLocationY, int lblSizeX, int lblSizeY, Color bgC, Color fgC, int fontSize, string lblName)
        {
            label = new Label(); //label adında nesne oluşturulur. Ekranda yazı gösterir

            label.Text = lblText; //labelin text kısmına gelen lblText değişkeni atanır
            label.Name = lblText; //labelin name kısmına gelen lblText değişkeni atanır
            label.Size = new Size(lblSizeX, lblSizeY); //labelin boyutlandırması yapılır
            label.ForeColor = fgC; //labelin renk ataması yapılır
            label.BackColor = bgC; // labelin renk ataması yapılır
            label.Font = new Font("Arial", fontSize); //labelin içindeki yazının font özellikleri atanır
            label.TextAlign = ContentAlignment.MiddleLeft; //labelin içindeki yazının orta sol kısma hizalanması sağlanır
            label.Location = new Point(lblLocationX, lblLocationY);//labelin panel içinde konumlandırması yapılır

            if (lblName == "Play Music") //lblName "Play Music" yazısına eşit olduğu durumda geçerlidir
            {
                label.MouseClick += mouseClick; //label a mouse tıklandığı zaman çağırılacak olan metod mouseClick'dir
            }

            panel.Controls.Add(label); //panel içine özellikleri atanan label yerleştirilir
            return panel.Controls[label.Name] as Label; //label.Name ismindeki labeli label olarak geri döndürme işlemi yapar
        }


        /// <summary>
        /// Form'a görsellik katmak için oluşturulmuş bir metod
        /// </summary>
        /// <param name="panel">nesneleri içinde tutacak panel</param>
        /// <param name="dosyaAdı">gelen dosya adı tutarak klasör içinde arama yapılmasına yardımcı olacak değişken</param>
        /// <param name="form">işlemlerin üzerinde gerçekleşeceği form yapısı</param>
        /// <returns></returns>
        public PictureBox createPictureBox(Panel panel, string dosyaAdı, Form form)
        {
            PictureBox pictureBox = new PictureBox(); //görüntü kutusu anlamına gelen bu nesne içine görsel yerleştirmek için kullanılır

            if (dosyaAdı == "record") //dosyaAdı değişkeninin "reocord" yazısına eşit olduğu durumlar için geçerlidir. record yazısının görselini görüntüleyen kısım
            {
                pictureBox.Size = new Size(20, 20); //picture box'ın boyutunu belirleyer
                pictureBox.Location = new Point(415, 15); //konumunu belirler 
                panel.Controls.Add(pictureBox); //panel içine kutuyu yerleştirir
                pictureBox.Image = Properties.Resources.record; //görüntü kutusu içine istenilen görseli dosyadan çekerek yerleştirir
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage; //görüntünün nasıl görüntüleneceğini gösterir
            }
            if (dosyaAdı == "save") //dosyaAdı değişkeninin "save" yazısına eşit olduğu durumlar için geçerlidir. save yazısının görselini görüntüleyen kısım
            {
                pictureBox.Size = new Size(20, 20); //picture box'ın boyutunu belirleyer
                pictureBox.Location = new Point(575, 15); //konumunu belirler
                panel.Controls.Add(pictureBox); //panel içine kutuyu yerleştirir
                pictureBox.Image = Properties.Resources.save; //görüntü kutusu içine istenilen görseli dosyadan çekerek yerleştirir
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage; //görüntünün nasıl görüntüleneceğini gösterir
            }
            //aynı metod içinde bulunan sonraki kodlar da aynı işlemleri farklı değerler ile yapmaktadır her birisi için açıklamaya gerek yok
            if (dosyaAdı == "play") //play yazısının görselini görüntüleyen kısım
            {
                pictureBox.Size = new Size(20, 20);
                pictureBox.Location = new Point(740, 15);
                panel.Controls.Add(pictureBox);
                pictureBox.Image = Properties.Resources.play;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            if (dosyaAdı == "metronom") //metronom yazısının görselini görüntüleyen kısım
            {
                pictureBox.Size = new Size(20, 20);
                pictureBox.Location = new Point(890, 15);
                panel.Controls.Add(pictureBox);
                pictureBox.Image = Properties.Resources.metronom;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            }
            if (dosyaAdı == "style") //style yazısının görselini görüntüleyen kısım
            {
                pictureBox.Size = new Size(20, 20);
                pictureBox.Location = new Point(1090, 15);
                panel.Controls.Add(pictureBox);
                pictureBox.Image = Properties.Resources.record;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            if (dosyaAdı == "hoparlorSol") //sol tarafa görüntünün güzel görünmesi ve piyanoya daha yakın bir hava yaratılabilmesi için hoparlöre benzer görsel oluşturur
            {
                pictureBox.Size = new Size(249, 119);
                pictureBox.Location = new Point(13, 13);
                form.Controls.Add(pictureBox);
                pictureBox.Image = Properties.Resources.hoparlor;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            if (dosyaAdı == "hoparlorSag") //sağ tarafa görüntünün güzel görünmesi ve piyanoya daha yakın bir hava yaratılabilmesi için hoparlöre benzer görsel oluşturur
            {
                pictureBox.Size = new Size(249, 119);
                pictureBox.Location = new Point(1347, 13);
                form.Controls.Add(pictureBox);
                pictureBox.Image = Properties.Resources.hoparlor;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            return pictureBox as PictureBox;
        }

        /// <summary>
        /// fare metodun çağırıldığı kısmın üstüne geldiği zaman çalışacak olan metod
        /// </summary>
        /// <param name="sender">çağırılan noktadaki nesnenin özelliklerini tutar</param>
        /// <param name="e"></param>
        private void mouseEnter(object sender, EventArgs e)
        {
            Label theLabel = (Label)sender; //sender tarafından tutulan nesneyi Label sınıfından oluşan theLable nesnesine atar
            if (theLabel != label) //theLabel nesnesi label nesnesine eşit olmadığı durumlar için geçerlidir
            {
                theLabel.ForeColor = Color.White; //fare labelin üstüne geldiği zaman yazının rengi değişir
                theLabel.BackColor = Color.Gray; //fare labelin üzerine geldiği zaman arka plan arka plan rengi değişir
            }
        }


        /// <summary>
        /// fare metodun çağırıldığı kısmın üserinden ayrıldığı zaman çalışacak olan metod 
        /// </summary>
        /// <param name="sender">çağırılan noktadaki nesnenin özelliklerini tutar</param>
        /// <param name="e"></param>
        private void mouseLeave(object sender, EventArgs e) 
        {
            Label theLabel = (Label)sender; //sender tarafından tutulan nesneyi Label sınıfından oluşan theLable nesnesine atar
            if (theLabel != label) //theLabel nesnesi label nesnesine eşit olmadığı durumlar için geçerlidir
            {
                theLabel.ForeColor = Color.LightGray; //fare labelin üzerinden ayrıldığı zaman yazının rengi değişir
                theLabel.BackColor = Color.FromArgb(33, 33, 33); //fare labelin üzerinden ayrıldığı zaman arka plan arka plan rengi değişir
            }
        }


        /// <summary>
        /// uygulamada otamatik olarak seçilen şarkının çalmasını sağlayan metod
        /// </summary>
        /// <param name="sender">çağırılan noktadaki nesnenin özelliklerini tutar</param>
        /// <param name="e"></param>
        private void mouseClick(object sender, EventArgs e)
        {
            Label theLabel = (Label)sender; //sender tarafından tutulan nesneyi Label sınıfından oluşan theLable nesnesine atar
            ComboBox cmb = Application.OpenForms["Form1"].Controls["combobox1"] as ComboBox; //ComboBox sınıfında cmb nesnesi oluşturup form1 sınıfındaki combobox1 nesnesine eşitlenir

            Panel pnl = Application.OpenForms["Form1"].Controls["panelPiano"] as Panel; //Panel sınıfında pnl nesnesi oluşturup form1 sınıfındaki panelPiano nesnesine eşitlenir

            /*bu kısımda yapılan işlem seçilen müziğin çalınmasını sağlamak. Her bir if yapısında comboBox içinde seçilen 
             * şarkının hangisi olduğunu if yapısı ile bulup istenilen şarkıdaki notları tek tek oynatarak şarkıyı tamamlıyor.
             Notaları oynatma şekli ise istenilen notayı, vuruş miktarını, ve panel nesnesini oynatma işlemi yapan metoda göndererek gerçekleştiriliyor*/
            if (theLabel == label && cmb.SelectedItem == "Fur Elise") /*theLabel nesnesi label nesnesine eşit olduğu durumda ve 
                                                                       * comboBox kısmında seçilen yazının "Fur Elise" olduğu durum için geçerlidir*/
            {
                //z kısmı
                PlayMusicPlayer("E5", 2, pnl);
                PlayMusicPlayer("D#5", 2, pnl);
                PlayMusicPlayer("E5", 2, pnl);
                PlayMusicPlayer("D#5", 2, pnl);
                PlayMusicPlayer("E5", 2, pnl);

                //alfa kısmı
                PlayMusicPlayer("B4", 2, pnl);
                PlayMusicPlayer("D5", 2, pnl);
                PlayMusicPlayer("C5", 2, pnl);
                PlayMusicPlayer("A4", 4, pnl);
                Task.Delay(250).Wait();

                //Y KISMI
                PlayMusicPlayer("C4", 2, pnl);
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("A4", 2, pnl);
                PlayMusicPlayer("B4", 4, pnl);
                Task.Delay(250).Wait();

                //BOŞKÜME KISMI
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("G#4", 2, pnl);
                PlayMusicPlayer("B4", 2, pnl);
                PlayMusicPlayer("C5", 4, pnl);
                Task.Delay(250).Wait();

                PlayMusicPlayer("E4", 2, pnl);

                //z kısmı
                PlayMusicPlayer("E5", 2, pnl);
                PlayMusicPlayer("D#5", 2, pnl);
                PlayMusicPlayer("E5", 2, pnl);
                PlayMusicPlayer("D#5", 2, pnl);
                PlayMusicPlayer("E5", 2, pnl);

                //alfa kısmı
                PlayMusicPlayer("B4", 2, pnl);
                PlayMusicPlayer("D5", 2, pnl);
                PlayMusicPlayer("C5", 2, pnl);
                PlayMusicPlayer("A4", 4, pnl);
                Task.Delay(250).Wait();

                //Y KISMI
                PlayMusicPlayer("C4", 2, pnl);
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("A4", 2, pnl);
                PlayMusicPlayer("B4", 4, pnl);
                Task.Delay(250).Wait();

                //u kısmı
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("C5", 2, pnl);
                PlayMusicPlayer("B4", 2, pnl);
                PlayMusicPlayer("A4", 4, pnl);
                Task.Delay(250).Wait();

                //z kısmı
                PlayMusicPlayer("E5", 2, pnl);
                PlayMusicPlayer("D#5", 2, pnl);
                PlayMusicPlayer("E5", 2, pnl);
                PlayMusicPlayer("D#5", 2, pnl);
                PlayMusicPlayer("E5", 2, pnl);

                //alfa kısmı
                PlayMusicPlayer("B4", 2, pnl);
                PlayMusicPlayer("D5", 2, pnl);
                PlayMusicPlayer("C5", 2, pnl);
                PlayMusicPlayer("A4", 4, pnl);
                Task.Delay(250).Wait();


                //Y KISMI
                PlayMusicPlayer("C4", 2, pnl);
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("A4", 2, pnl);
                PlayMusicPlayer("B4", 4, pnl);
                Task.Delay(250).Wait();

                //BOŞKÜME KISMI
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("G#4", 2, pnl);
                PlayMusicPlayer("B4", 2, pnl);
                PlayMusicPlayer("C5", 4, pnl);
                Task.Delay(250).Wait();

                PlayMusicPlayer("E4", 2, pnl);

                //z kısmı
                PlayMusicPlayer("E5", 2, pnl);
                PlayMusicPlayer("D#5", 2, pnl);
                PlayMusicPlayer("E5", 2, pnl);
                PlayMusicPlayer("D#5", 2, pnl);
                PlayMusicPlayer("E5", 2, pnl);

                //alfa kısmı
                PlayMusicPlayer("B4", 2, pnl);
                PlayMusicPlayer("D5", 2, pnl);
                PlayMusicPlayer("C5", 2, pnl);
                PlayMusicPlayer("A4", 4, pnl);
                Task.Delay(250).Wait();


                //Y KISMI
                PlayMusicPlayer("C4", 2, pnl);
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("A4", 2, pnl);
                PlayMusicPlayer("B4", 4, pnl);
                Task.Delay(250).Wait();

                //u kısmı
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("C5", 2, pnl);
                PlayMusicPlayer("B4", 2, pnl);
                PlayMusicPlayer("A4", 4, pnl);
                Task.Delay(250).Wait();
            }

            if (theLabel == label && cmb.SelectedItem == "Mary Had a Little Lamb")
            {
                PlayMusicPlayer("E4", 3, pnl); //A KISMI
                PlayMusicPlayer("D4", 1, pnl);
                PlayMusicPlayer("C4", 2, pnl);
                PlayMusicPlayer("D4", 2, pnl);
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("E4", 4, pnl);

                PlayMusicPlayer("D4", 2, pnl); //B KISMI
                PlayMusicPlayer("D4", 2, pnl);
                PlayMusicPlayer("D4", 4, pnl);
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("E4", 4, pnl);

                PlayMusicPlayer("E4", 3, pnl); //A KISMI
                PlayMusicPlayer("D4", 1, pnl);
                PlayMusicPlayer("C4", 2, pnl);
                PlayMusicPlayer("D4", 2, pnl);
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("E4", 2, pnl);

                PlayMusicPlayer("E4", 2, pnl); //C KISMI
                PlayMusicPlayer("D4", 2, pnl);
                PlayMusicPlayer("D4", 2, pnl);
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("D4", 2, pnl);
                PlayMusicPlayer("C4", 4, pnl);
                Task.Delay(1000).Wait();
            }

            if (theLabel == label && cmb.SelectedItem == "London Bridge")
            {

                PlayMusicPlayer("G4", 3, pnl); //A
                PlayMusicPlayer("A4", 1, pnl);
                PlayMusicPlayer("G4", 2, pnl);
                PlayMusicPlayer("F4", 2, pnl);
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("F4", 2, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("D4", 2, pnl);
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("F4", 4, pnl);
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("F4", 2, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("G4", 3, pnl);
                PlayMusicPlayer("A4", 1, pnl);
                PlayMusicPlayer("G4", 2, pnl);
                PlayMusicPlayer("F4", 2, pnl);
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("F4", 2, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("D4", 4, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("C4", 6, pnl);

                PlayMusicPlayer("G4", 3, pnl); //A
                PlayMusicPlayer("A4", 1, pnl);
                PlayMusicPlayer("G4", 2, pnl);
                PlayMusicPlayer("F4", 2, pnl);
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("F4", 2, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("D4", 2, pnl);
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("F4", 4, pnl);
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("F4", 2, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("G4", 3, pnl);
                PlayMusicPlayer("A4", 1, pnl);
                PlayMusicPlayer("G4", 2, pnl);
                PlayMusicPlayer("F4", 2, pnl);
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("F4", 2, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("D4", 4, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("E4", 2, pnl);
                PlayMusicPlayer("C4", 6, pnl);
            }

            if (theLabel == label && cmb.SelectedItem == "Old MacDonald")
            {
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("D4", 4, pnl);//ALFA BAŞLANGIÇ
                PlayMusicPlayer("E4", 4, pnl);
                PlayMusicPlayer("E4", 4, pnl);
                PlayMusicPlayer("D4", 8, pnl);
                PlayMusicPlayer("B4", 4, pnl);
                PlayMusicPlayer("B4", 4, pnl);
                PlayMusicPlayer("A4", 4, pnl);
                PlayMusicPlayer("A4", 4, pnl);
                PlayMusicPlayer("G4", 8, pnl);//ALFA BİTİŞ
                Task.Delay(500).Wait();
                                       
                PlayMusicPlayer("D4", 4, pnl);
                                       
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("D4", 4, pnl);//ALFA BAŞLANGIÇ
                PlayMusicPlayer("E4", 4, pnl);
                PlayMusicPlayer("E4", 4, pnl);
                PlayMusicPlayer("D4", 8, pnl);
                PlayMusicPlayer("B4", 4, pnl);
                PlayMusicPlayer("B4", 4, pnl);
                PlayMusicPlayer("A4", 4, pnl);
                PlayMusicPlayer("A4", 4, pnl);
                PlayMusicPlayer("G4", 8, pnl);//ALFA BİTİŞ
                Task.Delay(500).Wait();
                                       
                PlayMusicPlayer("D4", 2, pnl);
                PlayMusicPlayer("D4", 2, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("D4", 2, pnl);
                PlayMusicPlayer("D4", 2, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("G4", 8, pnl);
                PlayMusicPlayer("G4", 2, pnl);
                PlayMusicPlayer("G4", 2, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("G4", 2, pnl);
                PlayMusicPlayer("G4", 2, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("G4", 2, pnl);
                PlayMusicPlayer("G4", 2, pnl);
                PlayMusicPlayer("G4", 2, pnl);
                PlayMusicPlayer("G4", 2, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                PlayMusicPlayer("G4", 4, pnl);
                                       
                PlayMusicPlayer("D4", 4, pnl);//ALFA BAŞLANGIÇ
                PlayMusicPlayer("E4", 4, pnl);
                PlayMusicPlayer("E4", 4, pnl);
                PlayMusicPlayer("D4", 8, pnl);
                PlayMusicPlayer("B4", 4, pnl);
                PlayMusicPlayer("B4", 4, pnl);
                PlayMusicPlayer("A4", 4, pnl);
                PlayMusicPlayer("A4", 4, pnl);
                PlayMusicPlayer("G4", 8, pnl);//ALFA BİTİŞ
            }

        }

        ComboBox combobox; //şarkıların seçilme işlemi burda oluşturulan comboBox içinde gerçekleşiyor

        /// <summary>
        /// oluşturulan comboBox nesnesi içine istenilen şarkı isimlerini koyarak kullanıcının birisi seçmesi sağlanıyor
        /// seçtiği şarkıya göre daha sonra üstteki metodda çalma işlemi gerçekleştiriliyor
        /// </summary>
        /// <param name="panel">comboBox'ı içinde tutacak olan panel nesnesi</param>
        /// <param name="cmbName">box'ın ismini alan değişken</param>
        /// <param name="cmbLocationX">box'ın x eksenindeki konumunu alan değişken</param>
        /// <param name="cmbLocationY">box'ın y eksenindeki konumunu alan değişken</param>
        /// <param name="cmbSizeX">box'ın x eksenindeki boyutunu alan değişken</param>
        /// <param name="cmbSizeY">box'ın y eksenindeki boyutunu alan değişken</param>
        /// <param name="secenekDizisi">içinde müzikleri tutan string bir dizi</param>
        /// <param name="fontSize">şarkıların font boyutunu tutan değişken</param>
        /// <param name="bC">box'ın arkaplan rengini tutan değişken</param>
        /// <returns></returns>
        public ComboBox createComboBox(Panel panel, string cmbName, int cmbLocationX, int cmbLocationY, int cmbSizeX, int cmbSizeY, string[] secenekDizisi, int fontSize, Color bC)
        {
            combobox = new ComboBox(); //comboBox nesnesi oluşturulur
            combobox.Name = cmbName; //comboBox'ın adı atanır
            combobox.Location = new Point(cmbLocationX, cmbLocationY); //comboBox'ın konumu atanır
            combobox.Size = new Size(cmbSizeX, cmbSizeY); //comboBox'ın boyutu atanır
            combobox.Font = new Font("Arial", fontSize); //comboBox'ın içindeki şarkıların font özellikleri atanır
            combobox.BackColor = bC; //comboBox'ın arkaplan rengi atanır


            for (int i = 0; i < secenekDizisi.Length; i++) //müzikler dizisinin uzunluğunu kadar dönerek comboBox içine isimleri tek tek yerleştiren döngü
            {
                combobox.Items.Add(secenekDizisi[i]); //comboBox içine şarkı isimleri eklenir
            }
            panel.Controls.Add(combobox); //panel içine comboBox eklenir
            return panel.Controls[combobox.Name] as ComboBox; //comboBox.Name ismindeki ComboBox'ı comboBox olarak geri döndürme işlemi yapar
        }
    }
}

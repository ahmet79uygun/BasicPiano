using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PianoDesign
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// form'un içine istenilen nesnelerin yerleştirildiği metod, ayrıca değerlerin çoğu buradan gidiyor yani bir bakıma kontrol sayfası demek yanlış olmaz
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            CreateKey create = new CreateKey(); //CreateKey sınıfından create adında nesne oluşturulmuştur. Bu nesne ile CreateKey sınıfına erişip metodlara atama yapılmaktadır

            Form1 form = this; //Form1 sınıfından nesne üretilir
            form.BackColor =Color.FromArgb(51,51,51); //form'un arkaplanı Argb komutu ile atanır
            Panel panelPiano = new Panel(); //panelPiano adında Panel sınıfından nesne oluşturulur
            Panel panelBackGround = new Panel(); //panelBackground adında Panel sınıfından nesne oluşturulur

            panelPiano = create.createPanel(1584, 262, 13, 217, form, "panelPiano", SystemColors.Control,true); //piyano kısmı için panel oluşturma metodu çağırılır ve değerleri gönderilir. Oluşturulan panel panelPiano adlı panel nesnesine atanır


            //siyah notaların isimleri string dizisi içerisine yazılmıştır. Ek olarak klasör içinden dosya çekme işlemleri de bu isimler aracılığıyla yapılmaktadır
            string[] Black = { "C#2", "D#2", "_", "F#2", "G#2", "A#2", "_", "C#3", "D#3", "_", "F#3", "G#3", "A#3", "_", "C#4", "D#4", "_", "F#4", "G#4", "A#4", "_", "C#5", "D#5", "_", "F#5", "G#5", "A#5", "_", "C#6", "D#6", "_", "F#6", "G#6", "A#6" };

            //beyaz notaların isimleri string dizisi içerisine yazılmıştır. Ek olarak klasör içinden dosya çekme işlemleri de bu isimler aracılığıyla yapılmaktadır
            string[] White = { "C2", "D2", "E2", "F2", "G2", "A2", "B2", "C3", "D3", "E3", "F3",
                "G3", "A3", "B3", "C4", "D4", "E4", "F4", "G4", "A4", "B4", "C5", "D5", "E5", "F5", "G5",
                "A5", "B5", "C6", "D6", "E6", "F6", "G6", "A6", "B6", "C7" };

            create.CreateBlackKey(26, 169, 44, 31, Black, panelPiano); //create nesnesi ile CreateBlackKey metodu çağırılır ve değerleri gönderilir
            create.CreatWhiteKey(44, 262, White, panelPiano); //create nesnesi ile CreatWhiteKey metodu çağırılır ve değerleri gönderilir

            panelBackGround = create.createPanel(1584, 47, 13, 150, form, "bcPanel", Color.FromArgb(33, 33, 33), true); //panel oluşturma metoduna istenilen değerler gönderilir oluşan panel nesnesi panelBackgorund nesnesine atanır
           
            create.createLabelH(panelBackGround, "Record", 440, 90); //Buton oluşturabilmek için label oluşturma metodu çağırılır ve istenilen değerler gönderilir. 
            //Bu butonlar şuan için işlevsizdir görsellik katmak ve finalde yardımcı olmaları için eklenmişlerdir
            create.createLabelH(panelBackGround, "Save", 600, 60);
            create.createLabelH(panelBackGround, "Play", 760, 50);
            create.createLabelH(panelBackGround, "Styles", 1110, 90);
            create.createLabelH(panelBackGround, "Key Assist", 910, 120); 
            create.createLabelH(panelBackGround, "", 1500, 90);


            create.createPictureBox(panelBackGround,"record",form); //oluşturulan label'ların ve genel olarak form'un görselliğini iyileştirebilmek için PictureBox kullanılarak form içine klasörden görseller eklenir. 
            //PictureBox oluşturma metodu çağırılır ve istenilen değerler gönderilir
            create.createPictureBox(panelBackGround,"play",form);
            create.createPictureBox(panelBackGround,"save",form);
            create.createPictureBox(panelBackGround,"metronom",form);
            create.createPictureBox(panelBackGround,"style",form);
            create.createPictureBox(panelBackGround,"hoparlorSol",form);
            create.createPictureBox(panelBackGround,"hoparlorSag",form);

            Panel bG2 = new Panel(); //Panel sınıfından bG2 adında yeni bir panel nesnesi oluşturulur
            bG2 = create.createPanel(1028, 124, 289, 13, form, "panelBackGround2", Color.FromArgb(33,33,33), true); //form'un üst kısmı için panel oluşturmak için createPanel metodu çağırılır ve istenilen değerler gönderilir. 
            //Piyano butonları dışındaki butonlar ve görseller bu kısımda bulunur.

            for (int i = 0; i < 20; i++) //bu kısım kullanılarak "select music" ve "play music" arasına çizgi çekilir
            {
                create.createLabelN(bG2, "|", 512, 10 + (i * 5), 3, 10, Color.Black,Color.White,20,"");
            }

            create.createLabelN(bG2, "Select Music", 300, 10, 210, 50, Color.FromArgb(33, 33, 33), Color.Orange, 20,""); //music seçim kısmı için değerleri gönderilen label çağırılan metod ile oluşturulur

            create.createLabelN(bG2, "Play Music", 520, 10, 350, 50, Color.FromArgb(33, 33, 33), Color.OrangeRed, 20,"Play Music"); //seçilen müziği oynatabilmek için label olan bir buton oluşturulur

            string[] muzikler = { "Fur Elise", "London Bridge" }; //müziklerin isimlerini tutan string dizi.
           // ComboBox cmbMuzikler = new ComboBox();
           // cmbMuzikler = create.createComboBox(bG2, "cmbMuzikler", 300, 70, 200, 50, muzikler, 16, Color.Gray);//////////////////////////////

            string[] secenek = { "Keyboard mode", "Nota Mod" }; //finalde daha işlevli olacak olan bu dizi piyano kısmının klavyeden çalınıp çalınamamasına karar verecek yapıdır


            /* finalde eklenecek olan özellikler için kullanılacak yarım kalan işlemler
            ComboBox keySet = create.createComboBox(panelBackGround, "", 910, 10, 100, 50, secenek, 14, Color.FromArgb(33, 33, 33));
            string[] secenekler = { "Keyboard Mode", "Nota Mode" };
            Panel panelSecenek = create.createPanel(161, secenekler.Length * 50, 880, 195, form, "pnlKeySet", Color.FromArgb(33, 33, 33), false);
            */
        }
    }
}

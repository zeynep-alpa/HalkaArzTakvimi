using ClosedXML.Excel;
using Proje1;
using System.Data.SQLite;
using System.Net;
using System.Text;

namespace project2
{
    public partial class Form1 : Form
    {
        public string HalkaArzHtml;
        public Uri HalkaArzUrl;
        static string dbFilePath;
        List<HisseModel> halkaArz = new List<HisseModel>();
        List<HisseModel> taslakArz = new List<HisseModel>();
        private DataGridViewFormatter dgvFormatter = new DataGridViewFormatter();
        private ExcelManager excelManager = new ExcelManager();
        private VeritabaniManager veritabaniManager = new VeritabaniManager(dbFilePath);

        //dgvHalkaArz'�n h�cre i�eri�inin bi�imlendirilmesi veya �zelle�tirilmesi
        //gibi g�rsel de�i�iklikler yapmak i�indir. Renklendirmeyi sa�lar.
        public Form1()
        {
            InitializeComponent();
            dgvHalkaArz.CellFormatting += dgvHalkaArz_CellFormatting;
        }

        //  "halka" ve "taslak" gibi arzlar�n verilerinin s�ralanmas�n� sa�lar.
        public enum arzTipi
        {
            halka,
            taslak
        }
        public void ClearList(arzTipi tip)
        {
            if (tip == arzTipi.halka)
                halkaArz.Clear();
            else
                taslakArz.Clear();
        }

        public void VeriAl(string Url, string XPath, arzTipi tip)
        {
            ClearList(tip);
            //URL a��l�rken hata verirse ihtimaline kar�� yaz�lan kod a�a��da yer al�yor.
            try
            {
                HalkaArzUrl = new Uri(Url);
            }
            catch (UriFormatException) //URL'nin do�rulu�unun sa�lanmas� i�in bir g�venlik �nlemidir
            {
                if (MessageBox.Show("Hatal� Url", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK) { }
            }
            catch (ArgumentNullException)
            {
                if (MessageBox.Show("Hatal� Url", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK) { }
            }

            WebClient client = new WebClient(); //web sitesiyle ileti�im kurmay� sa�lar.
            client.Encoding = Encoding.UTF8;
            try
            {
                HalkaArzHtml = client.DownloadString(HalkaArzUrl); //HTML i�eri�i indirilir ve html de�i�kenine atan�r.
            }
            catch (WebException) //web sitesine eri�imde bir hata olursa
            {
                if (MessageBox.Show("Hatal� Url", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK) { }
            }
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument(); //HTML i�eri�i i�leniyor.
            doc.LoadHtml(HalkaArzHtml);
            try
            {
                //Veriler d�zenleniyor ve i�leniyor...
                //liste i�eri�i d�zenlenir ve i�indeki bo�luklar, yeni sat�rlar ve di�er �zel karakterler kald�r�l�r.

                string icerik = doc.DocumentNode.SelectSingleNode(XPath).InnerHtml;

                icerik = icerik.Replace("  ", "");
                icerik = icerik.Replace('\n', '|').Replace('\t', '|');
                icerik = icerik.Replace("|", "");

                var li_kaldirildi = icerik.Split("</li>");

                HtmlAgilityPack.HtmlDocument veriler = new HtmlAgilityPack.HtmlDocument(); //HTML i�eri�i i�leniyor.

                foreach (var item in li_kaldirildi)
                {
                    if (string.IsNullOrEmpty(item))
                        break;
                    veriler.LoadHtml(item + "</li>");
                    string kod = veriler.DocumentNode.SelectSingleNode("//*[@class='il-bist-kod']").InnerHtml;

                    string kod1 = veriler.DocumentNode.SelectSingleNode("//*[@class='il-halka-arz-sirket']").InnerHtml;
                    veriler.LoadHtml(kod1);
                    string baslik = veriler.DocumentNode.InnerText;

                    veriler.LoadHtml(item);
                    string tarih = string.Empty;
                    DateTime? sonTarih = null;
                    if (tip == arzTipi.halka)
                    {
                        tarih = veriler.DocumentNode.SelectSingleNode("//*[@class='il-halka-arz-tarihi']").InnerText;
                        sonTarih = TarihClass.tarihGetir(tarih);
                    }
                    if (tip == arzTipi.halka)
                        halkaArz.Add(new HisseModel() { HisseKodu = kod, Aciklamasi = baslik, ArzTarihi = tarih, SonTarih = sonTarih });
                    else
                        taslakArz.Add(new HisseModel() { HisseKodu = kod, Aciklamasi = baslik });
                }
            }
            catch (NullReferenceException)
            {
                if (MessageBox.Show("Hatal� XPath", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK) { }
            }
        }
        private void dgvHalkaArz_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dgvFormatter.FormatCell(e, dgvHalkaArz);
        }

        private void deleteArzTarihiColumn()
        {
            // Silmek istedi�imiz s�tunun ad�n� belirtiyoruz
            string columnName = "ArzTarihi";

            // dgvTaslakArz i�erisindeki t�m s�tunlar� kontrol ediyoruz
            foreach (DataGridViewColumn column in dgvTaslakArz.Columns)
            {
                // E�le�en s�tunu bulup siliyoruz
                if (column.HeaderText == columnName)
                {
                    dgvTaslakArz.Columns.Remove(column);
                    break;
                }
            }
        }
        private void deleteSonTarihColumn()
        {
            // Silmek istedi�imiz s�tunun ad�n� belirtiyoruz
            string columnName = "SonTarih";
            // dgvTaslakArz i�erisindeki t�m s�tunlar� kontrol ediyoruz
            foreach (DataGridViewColumn column in dgvTaslakArz.Columns)
            {
                // E�le�en s�tunu bulup siliyoruz
                if (column.HeaderText == columnName)
                {
                    dgvTaslakArz.Columns.Remove(column);
                    break;
                }
            }
        }
        private void btnVeriCek_Click(object sender, EventArgs e)
        {
            VeriAl("https://halkarz.com/", "//*[@class=\'halka-arz-list\']", arzTipi.halka);
            VeriAl("https://halkarz.com/", "//*[@class='halka-arz-list taslak']", arzTipi.taslak);

            dgvHalkaArz.DataSource = halkaArz;
            dgvTaslakArz.DataSource = taslakArz;

            deleteArzTarihiColumn();
            deleteSonTarihColumn();
        }
        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            // Veriler y�klenmemi�se hata mesaj� g�ster
            if (!halkaArz.Any() && !taslakArz.Any())
            {
                MessageBox.Show("Veriler y�klenmemi�tir. L�tfen verileri y�kleyiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ExcelManager.SaveToExcel(halkaArz, taslakArz);
            MessageBox.Show("Veriler Excel dosyas�na ba�ar�yla yazd�r�ld�!", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnData_Click(object sender, EventArgs e)
        {
            // Veriler y�klenmemi�se hata mesaj� g�ster
            if (!halkaArz.Any() && !taslakArz.Any())
            {
                MessageBox.Show("Veriler y�klenmemi�tir. L�tfen verileri y�kleyiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string dbFilePath = "C:\\Users\\Zeynep ALPA\\Desktop\\staj\\project2\\project2\\bin\\Debug\\veritabani.db";
            veritabaniManager.VeriTabaninaKaydet(halkaArz);
            MessageBox.Show("Veriler veri taban�na ba�ar�yla kaydedildi!", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
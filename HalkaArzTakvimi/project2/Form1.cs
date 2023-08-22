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

        //dgvHalkaArz'ýn hücre içeriðinin biçimlendirilmesi veya özelleþtirilmesi
        //gibi görsel deðiþiklikler yapmak içindir. Renklendirmeyi saðlar.
        public Form1()
        {
            InitializeComponent();
            dgvHalkaArz.CellFormatting += dgvHalkaArz_CellFormatting;
        }

        //  "halka" ve "taslak" gibi arzlarýn verilerinin sýralanmasýný saðlar.
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
            //URL açýlýrken hata verirse ihtimaline karþý yazýlan kod aþaðýda yer alýyor.
            try
            {
                HalkaArzUrl = new Uri(Url);
            }
            catch (UriFormatException) //URL'nin doðruluðunun saðlanmasý için bir güvenlik önlemidir
            {
                if (MessageBox.Show("Hatalý Url", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK) { }
            }
            catch (ArgumentNullException)
            {
                if (MessageBox.Show("Hatalý Url", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK) { }
            }

            WebClient client = new WebClient(); //web sitesiyle iletiþim kurmayý saðlar.
            client.Encoding = Encoding.UTF8;
            try
            {
                HalkaArzHtml = client.DownloadString(HalkaArzUrl); //HTML içeriði indirilir ve html deðiþkenine atanýr.
            }
            catch (WebException) //web sitesine eriþimde bir hata olursa
            {
                if (MessageBox.Show("Hatalý Url", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK) { }
            }
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument(); //HTML içeriði iþleniyor.
            doc.LoadHtml(HalkaArzHtml);
            try
            {
                //Veriler düzenleniyor ve iþleniyor...
                //liste içeriði düzenlenir ve içindeki boþluklar, yeni satýrlar ve diðer özel karakterler kaldýrýlýr.

                string icerik = doc.DocumentNode.SelectSingleNode(XPath).InnerHtml;

                icerik = icerik.Replace("  ", "");
                icerik = icerik.Replace('\n', '|').Replace('\t', '|');
                icerik = icerik.Replace("|", "");

                var li_kaldirildi = icerik.Split("</li>");

                HtmlAgilityPack.HtmlDocument veriler = new HtmlAgilityPack.HtmlDocument(); //HTML içeriði iþleniyor.

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
                if (MessageBox.Show("Hatalý XPath", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK) { }
            }
        }
        private void dgvHalkaArz_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dgvFormatter.FormatCell(e, dgvHalkaArz);
        }

        private void deleteArzTarihiColumn()
        {
            // Silmek istediðimiz sütunun adýný belirtiyoruz
            string columnName = "ArzTarihi";

            // dgvTaslakArz içerisindeki tüm sütunlarý kontrol ediyoruz
            foreach (DataGridViewColumn column in dgvTaslakArz.Columns)
            {
                // Eþleþen sütunu bulup siliyoruz
                if (column.HeaderText == columnName)
                {
                    dgvTaslakArz.Columns.Remove(column);
                    break;
                }
            }
        }
        private void deleteSonTarihColumn()
        {
            // Silmek istediðimiz sütunun adýný belirtiyoruz
            string columnName = "SonTarih";
            // dgvTaslakArz içerisindeki tüm sütunlarý kontrol ediyoruz
            foreach (DataGridViewColumn column in dgvTaslakArz.Columns)
            {
                // Eþleþen sütunu bulup siliyoruz
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
            // Veriler yüklenmemiþse hata mesajý göster
            if (!halkaArz.Any() && !taslakArz.Any())
            {
                MessageBox.Show("Veriler yüklenmemiþtir. Lütfen verileri yükleyiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ExcelManager.SaveToExcel(halkaArz, taslakArz);
            MessageBox.Show("Veriler Excel dosyasýna baþarýyla yazdýrýldý!", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnData_Click(object sender, EventArgs e)
        {
            // Veriler yüklenmemiþse hata mesajý göster
            if (!halkaArz.Any() && !taslakArz.Any())
            {
                MessageBox.Show("Veriler yüklenmemiþtir. Lütfen verileri yükleyiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string dbFilePath = "C:\\Users\\Zeynep ALPA\\Desktop\\staj\\project2\\project2\\bin\\Debug\\veritabani.db";
            veritabaniManager.VeriTabaninaKaydet(halkaArz);
            MessageBox.Show("Veriler veri tabanýna baþarýyla kaydedildi!", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
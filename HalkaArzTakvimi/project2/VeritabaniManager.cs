using Proje1;
using System.Data.SQLite;

namespace project2
{
    public class VeritabaniManager
    {
        private string connectionString;

        public VeritabaniManager(string dbFilePath)
        {
            connectionString = $"Data Source=\"{dbFilePath}\"; Version=3;";
        }

        public void VeriTabaninaKaydet(List<HisseModel> halkaArz)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    foreach (var veri in halkaArz)
                    {
                        command.CommandText = "INSERT INTO HisseData (HisseKodu, Aciklamasi, ArzTarihi, SonTarih) VALUES (@HisseKodu, @Aciklamasi, @ArzTarihi, @SonTarih)";
                        command.Parameters.AddWithValue("@HisseKodu", veri.HisseKodu);
                        command.Parameters.AddWithValue("@Aciklamasi", veri.Aciklamasi);
                        command.Parameters.AddWithValue("@ArzTarihi", veri.ArzTarihi);
                        command.Parameters.AddWithValue("@SonTarih", veri.SonTarih);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}


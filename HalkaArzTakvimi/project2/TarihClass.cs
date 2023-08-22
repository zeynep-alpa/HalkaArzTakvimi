using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project2
{
    public class TarihClass
    {
        public static DateTime tarihGetir(string tarih)
        {
            if (tarih == "Hazırlanıyor...")
            {
                return DateTime.MaxValue;
            }
            else
            {

                string[] strTarih = tarih.Split(' ');
                string[] gunler = strTarih[0].Split('-');
                int ayIndex = 0;
                switch (strTarih[1])
                {
                    case "Ocak":
                        ayIndex = 1;
                        break;
                    case "Şubat":
                        ayIndex = 2;
                        break;
                    case "Mart":
                        ayIndex = 3;
                        break;
                    case "Nisan":
                        ayIndex = 4;
                        break;
                    case "Mayıs":
                        ayIndex = 5;
                        break;
                    case "Haziran":
                        ayIndex = 6;
                        break;
                    case "Temmuz":
                        ayIndex = 7;
                        break;
                    case "Ağustos":
                        ayIndex = 8;
                        break;
                    case "Eylül":
                        ayIndex = 9;
                        break;
                    case "Ekim":
                        ayIndex = 10;
                        break;
                    case "Kasım":
                        ayIndex = 11;
                        break;
                    case "Aralık":
                        ayIndex = 12;
                        break;
                    default:
                        break;
                }
                int gun = Convert.ToInt32(gunler[gunler.Length - 1]);

                return new DateTime(Convert.ToInt32(strTarih[strTarih.Length - 1]), ayIndex, gun);
            }
        }
    }
}

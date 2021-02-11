using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;

namespace staj_proje
{
    public class db_islemler
    {
        SqlConnection baglan;
        public db_islemler()
        {
            baglan = new SqlConnection(WebConfigurationManager.ConnectionStrings["baglantii"].ConnectionString);
        }
        /* Gmail üzerinden gerekli ayarları yaptıktan sonra sıkıntı olmadan kullanabilirsiniz. */
        public String mail_adres = "gönderiliecek mail adresinizi giriniz.";
        public String mail_sifre = "mail adresinize ait şifrenizi giriniz.";
        public Boolean kul_giris(String mail, String sif)
        {
            Boolean giris_durum = false;

            baglan.Open();

            SqlCommand sql = new SqlCommand("Select * from kul_giris where kul_ad=@ad and kul_sif=@sif", baglan);
            sql.Parameters.AddWithValue("@ad", mail);
            sql.Parameters.AddWithValue("@sif", sif);
            SqlDataReader dr = sql.ExecuteReader();
            if (dr.Read())
            {
                giris_durum = true;
            }
            dr.Close();
            baglan.Close();

            return giris_durum;
        }
        public int kul_bolum_getir(String mail, String sif)
        {
            int bolum = -1;
            baglan.Open();

            SqlCommand sql = new SqlCommand("Select * from kul_giris where kul_ad=@ad and kul_sif=@sif", baglan);
            sql.Parameters.AddWithValue("@ad", mail);
            sql.Parameters.AddWithValue("@sif", sif);
            SqlDataReader dr = sql.ExecuteReader();
            if (dr.Read())
            {
                bolum = int.Parse(dr["birim_id"].ToString());
            }
            dr.Close();
            baglan.Close();

            return bolum;
        }
        public void kul_kayit(String mail, String sif, int kis_id, int birim_id)
        {

            baglan.Open();

            SqlCommand sql = new SqlCommand("Insert into kul_giris (kul_ad,kul_sif,kis_id,birim_id) values (@mail,@sif,@kid,@bid)", baglan);
            sql.Parameters.AddWithValue("@mail", mail);
            sql.Parameters.AddWithValue("@sif", sif);
            sql.Parameters.AddWithValue("@kid", kis_id);
            sql.Parameters.AddWithValue("@bid", birim_id);
            sql.ExecuteNonQuery();

            baglan.Close();

        }
        public void kis_kayit(String kis_ad, String kis_sad, String kis_tel)
        {
            baglan.Open();

            SqlCommand sql = new SqlCommand("Insert into kis_bilgi (kis_ad,kis_sad,kis_tel) values (@ad,@sad,@tel)", baglan);
            sql.Parameters.AddWithValue("@ad", kis_ad);
            sql.Parameters.AddWithValue("@sad", kis_sad);
            sql.Parameters.AddWithValue("@tel", kis_tel);
            sql.ExecuteNonQuery();

            baglan.Close();
        }
        public int kis_id_getir(String kul_mail, String kul_sif)
        {
            int id = -1;
            baglan.Open();

            SqlCommand sql = new SqlCommand("Select * from kul_giris where kul_ad=@ad and kul_sif=@sif", baglan);
            sql.Parameters.AddWithValue("@ad", kul_mail);
            sql.Parameters.AddWithValue("@sif", kul_sif);
            SqlDataReader dr = sql.ExecuteReader();
            if (dr.Read())
            {
                id = int.Parse(dr["kis_id"].ToString());
            }

            baglan.Close();

            return id;
        }
        public int kis_id_getir(String ad, String sad, String tel)
        {
            int id = -1;
            baglan.Open();

            SqlCommand sql = new SqlCommand("Select * from kis_bilgi where kis_ad=@ad and kis_sad=@sad and kis_tel=@tel", baglan);
            sql.Parameters.AddWithValue("@ad", ad);
            sql.Parameters.AddWithValue("@sad", sad);
            sql.Parameters.AddWithValue("@tel", tel);
            SqlDataReader dr = sql.ExecuteReader();
            if (dr.Read())
            {
                id = int.Parse(dr["kis_id"].ToString());
            }

            baglan.Close();

            return id;
        }
        public void kul_ad_sad_getir(int id,TextBox ad, TextBox sad)
        {
            baglan.Open();

            SqlCommand sql = new SqlCommand("Select * from kis_bilgi where kis_id=@id",baglan);
            sql.Parameters.AddWithValue("@id",id);
            SqlDataReader dr = sql.ExecuteReader();
            if (dr.Read())
            {
                ad.Text = dr["kis_ad"].ToString();
                sad.Text = dr["kis_sad"].ToString();
            }
            dr.Close();
            baglan.Close();
        }
        public void talep_getir(TextBox ad, TextBox sad,TextBox konu, TextBox aciklama, DropDownList durum, TextBox durum_aciklama, String id)
        {
            baglan.Open();

            SqlCommand sql = new SqlCommand("Select * from talepp where tp_id=@id", baglan);
            sql.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = sql.ExecuteReader();
            if (dr.Read())
            {
                ad.Text = dr["tp_ad"].ToString();
                sad.Text = dr["tp_sad"].ToString();
                konu.Text = dr["tp_konu"].ToString();
                aciklama.Text = dr["tp_aciklama"].ToString();
                durum.SelectedValue = dr["tp_durum"].ToString();
                if (dr["tp_durum"].ToString() == "1")
                {
                    durum.Attributes.Add("style", "background-color: Green !important;");
                }
                else if (dr["tp_durum"].ToString() == "-1")
                {
                    durum.Attributes.Add("style", "background-color: Red !important;");
                }
                else
                {
                    durum.Attributes.Add("style", "background-color: Blue !important;");
                }
                durum_aciklama.Text = dr["tp_durum_aciklama"].ToString();
            }
            dr.Close();
            baglan.Close();
        }
        public void talep_kayıt(int id, int durum, String aciklama)
        {
            baglan.Open();

            SqlCommand sql = new SqlCommand("Update talepp set tp_durum=@durum, tp_durum_aciklama=@aciklama where tp_id=@id", baglan);

            sql.Parameters.AddWithValue("@durum", durum);
            sql.Parameters.AddWithValue("@aciklama", aciklama);
            sql.Parameters.AddWithValue("@id", id);

            sql.ExecuteNonQuery();

            baglan.Close();
        }
        public void talep_ekle(int kis_id, TextBox ad, TextBox sad, TextBox konu, TextBox aciklama, String date, String yol)
        {
            baglan.Open();

            SqlCommand sql = new SqlCommand("Insert into talepp (kis_id,tp_ad,tp_sad,tp_konu,tp_aciklama,tp_belge,tp_tarih,tp_durum) values (@id,@ad,@sad,@konu,@aciklama,@yol,@date,@durum)", baglan);
            sql.Parameters.AddWithValue("@id", kis_id);
            sql.Parameters.AddWithValue("@ad", ad.Text);
            sql.Parameters.AddWithValue("@sad", sad.Text);
            sql.Parameters.AddWithValue("@konu", konu.Text);
            sql.Parameters.AddWithValue("@aciklama", aciklama.Text);
            sql.Parameters.AddWithValue("@yol", yol);
            sql.Parameters.AddWithValue("@date", date);
            sql.Parameters.AddWithValue("@durum", 0);
            sql.ExecuteNonQuery();
            
            baglan.Close();
        }
        public string kul_ad_getir(String mail, String sif)
        {
            String ad = "";

            baglan.Open();

            SqlCommand sql = new SqlCommand("Select * from kul_islem where kul_mail=@mail and kul_sif=@sif", baglan);
            sql.Parameters.AddWithValue("@mail", mail);
            sql.Parameters.AddWithValue("@sif", sif);
            SqlDataReader dr = sql.ExecuteReader();
            if (dr.Read())
            {
                ad = dr["kul_ad"].ToString();
            }
            dr.Close();

            baglan.Close();

            return ad;
        }
        public string kul_sad_getir(String mail, String sif)
        {
            String sad = "";

            baglan.Open();

            SqlCommand sql = new SqlCommand("Select * from kul_islem where kul_mail=@mail and kul_sif=@sif", baglan);
            sql.Parameters.AddWithValue("@mail", mail);
            sql.Parameters.AddWithValue("@sif", sif);
            SqlDataReader dr = sql.ExecuteReader();
            if (dr.Read())
            {
                sad = dr["kul_sad"].ToString();
            }
            dr.Close();

            baglan.Close();

            return sad;
        }
        public string dosya_adı_getir(int id)
        {
            String ad = "";
            baglan.Open();

            SqlCommand sql = new SqlCommand("Select * from talep where tp_id=@id", baglan);
            sql.Parameters.AddWithValue("id", id);
            SqlDataReader dr = sql.ExecuteReader();
            if (dr.Read())
            {
                ad = dr["tp_belge"].ToString();
            }
            dr.Close();
            baglan.Close();


            return ad;
        }
        public void mail(String mail, String mail_icerik)
        {

            MailMessage email = new MailMessage();
            email.From = new MailAddress(mail_adres);
            email.To.Add(mail);
            email.Subject = "Talep Sonuç";
            email.Body = mail_icerik;
            email.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential(mail_adres, mail_sifre);
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(email);
            }
            catch (Exception)
            {

                throw;
            }


        }
        public Boolean mail_knt(String mail)
        {
            Boolean knt1 = false;
            Boolean knt2 = false;
            Boolean kntall = false;
            for (int i = 0; i < mail.Length; i++)
            {
                if (mail[i] == '@')
                {
                    knt1 = true;
                }
                if (knt1 == true)
                {
                    if (mail[i] == '.')
                    {
                        knt2 = true;
                    }
                }
            }
            if (knt1 == true && knt2 == true)
            {
                kntall = true;
            }
            return kntall;
        }
        public Boolean tel_knt(String tel)
        {
            Boolean knt = false;
            if (tel[0] == '0' && tel.Length == 11)
            {
                knt = true;
            }
            else if (tel[0] == '5' && tel.Length == 10)
            {
                knt = true;
            }
            return knt;
        }
        public String kul_mail_getir(int id)
        {
            String kul_mail = "", kis_id = "";
            baglan.Open();
            SqlCommand sql = new SqlCommand("Select * from talepp where tp_id=@id", baglan);
            sql.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = sql.ExecuteReader();
            if (dr.Read())
            {
                kis_id = dr["kis_id"].ToString();
            }
            dr.Close();
            sql = new SqlCommand("Select * from kul_giris where kis_id=@id", baglan);
            sql.Parameters.AddWithValue("@id", kis_id);
            dr = sql.ExecuteReader();
            if (dr.Read())
            {
                kul_mail = dr["kul_ad"].ToString();
            }

            dr.Close();
            baglan.Close();
            return kul_mail;
        }
    }
}
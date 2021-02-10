using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace staj_proje
{
    public partial class giris : System.Web.UI.Page
    {
        db_islemler db = new db_islemler();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void bt_giris_Click(object sender, EventArgs e)
        {
            if (tb_mail.Text != "" || tb_sif.Text != "")
            {
                //if (db.mail_knt(tb_mail.Text))
                //{
                    if (db.kul_giris(tb_mail.Text.ToString(), tb_sif.Text.ToString()))
                    {
                        Session.Add("bolum", db.kul_bolum_getir(tb_mail.Text.ToString(), tb_sif.Text.ToString()));
                        Session.Add("kul_mail",tb_mail.Text.ToString());
                        Session.Add("kis_id", db.kis_id_getir(tb_mail.Text, tb_sif.Text));
                        Response.Redirect("icerik.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('Kullanıcı adı veya şifre yanlış.')</script>");
                    }
               //}
                /*else
                {
                    Response.Write("<script>alert('Lütfen geçerli bir mail adresi giriniz.')</script>");
                }*/
            }
            else
            {
                Response.Write("<script>alert('Lütfen kullanıcı adı ve şifre alanını boş bırakmayınız.')</script>");
            }
        }

        protected void bt_kayit_git_Click(object sender, EventArgs e)
        {
            if (panel_kayit.Visible == true)
            {
                panel_kayit.Visible = false;
            }
            else
            {
                panel_kayit.Visible = true;
            }
        }

        protected void bt_kayit_Click(object sender, EventArgs e)
        {
            if (tb_kayit_mail.Text != "" || tb_kayit_sif.Text != "" || tb_kayit_ad.Text != "" || tb_kayit_sad.Text != "" || tb_kayit_tel.Text != "")
            {
                if (db.mail_knt(tb_kayit_mail.Text))
                {
                    if (db.tel_knt(tb_kayit_tel.Text))
                    {
                        db.kis_kayit(tb_kayit_ad.Text,tb_kayit_sad.Text,tb_kayit_tel.Text);
                        int id = db.kis_id_getir(tb_kayit_ad.Text, tb_kayit_sad.Text, tb_kayit_tel.Text);
                        db.kul_kayit(tb_kayit_mail.Text,tb_kayit_sif.Text, id, 1);
                        Response.Write("<script>alert('Kayıt Başarılı.')</script>");
                        panel_kayit.Visible = false;
                    }
                    else
                    {
                        Response.Write("<script>alert('Telefon numaranızı lütfen düzgün biriniz.')</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Mail adresinizde @ ve . işareti olmak zorunda.')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Lütfen Kayıt alanlarınızı boş geçmeyiniz.')</script>");
            }
        }
    }
}
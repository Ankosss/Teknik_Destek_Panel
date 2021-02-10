using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace staj_proje
{
    public partial class icerik : System.Web.UI.Page
    {
        db_islemler db = new db_islemler();
        public string tp_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["bolum"] == null)
                {
                    Response.Redirect("giris.aspx");
                }
                else
                {
                    int bolum = int.Parse(Session["bolum"].ToString());

                    if (bolum == 0)
                    {
                        panel_admin.Visible = true;
                        panel_kul.Visible = false;
                        panel_talepler.Visible = true;
                        panel_taleplerim.Visible = false;
                        panel_talep_gonder.Visible = false;
                    }
                    else
                    {
                        panel_admin.Visible = false;
                        panel_kul.Visible = true;
                        panel_talepler.Visible = false;
                        panel_taleplerim.Visible = false;
                        panel_talep_gonder.Visible = false;
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("giris.aspx"); throw;
            }


        }

        protected void nesne(object sender, EventArgs e)
        {
            try
            {
                Button bt = (Button)sender;
                tp_id = bt.CssClass.ToString();
                Session.Add("tp_id", bt.CssClass);
                panel_talep_goster.Visible = true;
                db.talep_getir(tb_talep_ad, tb_talep_sad, tb_talep_konu, tb_talep_aciklama, cb_durum, tb_talep_durum_aciklama, tp_id);
            }
            catch (Exception es)
            {
                Response.Write("<script>alert('" + "Hata" + es.ToString() + "')</script>");

            }
        }
        protected void nesne2(object sender, EventArgs e)
        {
            try
            {
                Button bt = (Button)sender;
                tp_id = bt.CssClass.ToString();
                Session.Add("tp_id", bt.CssClass);
                panel_talep_goster.Visible = true;
                db.talep_getir(tb_talep_ad, tb_talep_sad, tb_talep_konu, tb_talep_aciklama, cb_durum, tb_talep_durum_aciklama, tp_id);
                cb_durum.Enabled = false;
                tb_talep_durum_aciklama.Enabled = false;
                bt_talep_kayit.Enabled = false;
                bt_talep_kayit.Visible = false;
                tb_talep_ad.ReadOnly = true;
                tb_talep_sad.ReadOnly = true;
                tb_talep_aciklama.ReadOnly = true;

            }
            catch (Exception es)
            {
                Response.Write("<script>alert('" + "Hata" + es.ToString() + "')</script>");

            }
        }

        protected void bt_talep_kayit_Click(object sender, EventArgs e)
        {
            try
            {
                if (cb_durum.SelectedValue != "0" && tb_talep_durum_aciklama.Text != "")
                {
                    int id = int.Parse(Session["tp_id"].ToString());
                    String kul_mail = db.kul_mail_getir(int.Parse(Session["tp_id"].ToString()));
                    db.talep_kayıt(id, int.Parse(cb_durum.SelectedValue), tb_talep_durum_aciklama.Text.ToString());
                    db.mail(kul_mail, tb_talep_durum_aciklama.Text);
                    Response.Redirect("icerik.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Lütfen alanları boş geçmeyiniz.')</script>");
                }

            }
            catch (Exception)
            {
                Response.Write("<script>alert('Hata')</script>");
            }

        }

        protected void bt_cikis_Click(object sender, EventArgs e)
        {
            Response.Redirect("giris.aspx");
        }

        protected void bt_kul_talep_Click(object sender, EventArgs e)
        {
            if (panel_talep_gonder.Visible == false)
            {
                panel_talep_gonder.Visible = true;
                panel_taleplerim.Visible = false;
                panel_talep_goster.Visible = false;
                db.kul_ad_sad_getir(int.Parse(Session["kis_id"].ToString()), tb_talep_gonder_ad, tb_talep_gonder_sad);

            }
            else
            {
                panel_talep_gonder.Visible = false;
                panel_taleplerim.Visible = false;
                panel_talep_goster.Visible = false;
            }
        }

        protected void bt_kul_talep_gor_Click(object sender, EventArgs e)
        {
            if (panel_taleplerim.Visible == false)
            {
                panel_taleplerim.Visible = true;
                panel_talep_gonder.Visible = false;
                panel_talep_goster.Visible = false;
            }
            else
            {
                panel_taleplerim.Visible = false;
                panel_talep_gonder.Visible = false;
                panel_talep_goster.Visible = false;
            }
        }

        protected void bt_talep_gonder_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                /*if (FileUpload1.PostedFile.ContentType == "image/png")
                {*/
                    FileUpload1.SaveAs(Server.MapPath("/dosyalar/") + FileUpload1.FileName);
                    String yol = "dosyalar/" + FileUpload1.FileName;
                    if (yol != "")
                    {
                        if (tb_talep_gonder_ad.Text != "" || tb_talep_gonder_sad.Text != "")
                        {
                            int kis_id = int.Parse(Session["kis_id"].ToString());
                            db.talep_ekle(kis_id, tb_talep_gonder_ad, tb_talep_gonder_sad, tb_talep_gonder_konu, tb_talep_gonder_aciklama, DateTime.Now.ToString("dd,MM,yyyy"), yol);
                            Response.Redirect("icerik.aspx");
                        }
                        else
                        {
                            Response.Write("<script>alert('Lütfen adınızı ve soyadınızı düzgün bir biçimde giriniz.')</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Lütfen dosya seçiniz')</script>");
                    }
                /*}
                else
                {
                    Response.Write("<script>alert('Lütfen sadece png dosyası seçiniz')</script>");
                }*/
            }
            else
            {
                Response.Write("<script>alert('Lütfen dosya seçiniz')</script>");
            }
        }

        protected void indir_3_Click(object sender, EventArgs e)
        {
            try
            {
                String dosya = Server.MapPath(db.dosya_adı_getir(int.Parse(Session["tp_id"].ToString())));
                WebClient wc = new WebClient();
                Uri uri = new Uri(dosya);
                wc.DownloadFile(uri, @"C:\Users\asus\Desktop\staj\staj_proje\staj_proje\indirmeler\");
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Dosya indirilirken bir hata ile karşılaşıldı.')</script>");
            }
        }
    }
}
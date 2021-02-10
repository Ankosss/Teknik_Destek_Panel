<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="icerik.aspx.cs" Inherits="staj_proje.icerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Talep</title>
    <link href="style.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            margin-bottom: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="ust">
        </div>
        <div id="sol">
            <asp:Panel ID="panel_admin" runat="server">
                <asp:Button ID="bt_admin_talep" Text="Talepleri Gör" runat="server" /><br />
            </asp:Panel>
            <asp:Panel ID="panel_kul" runat="server">
                <asp:Button ID="bt_kul_talep" Text="Talep oluştur" runat="server" OnClick="bt_kul_talep_Click" /><br />
                <asp:Button ID="bt_kul_talep_gor" Text="Taleplerim" runat="server" OnClick="bt_kul_talep_gor_Click" />
            </asp:Panel>
            <asp:Button ID="bt_cikis" Text="Çıkış" runat="server" OnClick="bt_cikis_Click" />
        </div>
        <div id="orta">
            <asp:Panel ID="panel_talepler" runat="server">
                <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1">
                    <ItemTemplate>
                        <asp:Button ID="tp" CssClass='<%#Eval ("tp_id") %>' Text='<%#Eval ("tp_konu") %>' runat="server" OnClick="nesne" Visible=' <%#Eval ("tp_durum").ToString()== "0" ? true:false%>' />
                    </ItemTemplate>
                </asp:ListView>
            </asp:Panel>
            <asp:Panel ID="panel_talep_goster" runat="server" Visible="false">
                <div>
                    <asp:TextBox ID="tb_talep_ad" runat="server" placeholder="E-Mail" ReadOnly="True" /><asp:TextBox ID="tb_talep_sad" runat="server" placeholder="Şifre" ReadOnly="True" /><br />
                    <asp:TextBox ID="tb_talep_konu" runat="server" Width="245px" ReadOnly="true" />
                    <br />
                    <asp:TextBox ID="tb_talep_aciklama" runat="server" Height="137px" placeholder="Ad" ReadOnly="True" TextMode="MultiLine" Width="705px" />
                    <br />
                    <asp:HyperLink Text="İndir" NavigateUrl='<%#Eval ("tp_belge") %>' runat="server" />
                    <a href='<%#Eval ("tp_belge") %>' runat="server" download>İndir-2</a>
                    <asp:Button Text="İndir-3" runat="server" OnClick="indir_3_Click" />
                    <asp:DropDownList ID="cb_durum" runat="server" Height="16px" Width="135px" Font-Bold="True" ForeColor="White">
                        <asp:ListItem Value="0">Beklemede</asp:ListItem>
                        <asp:ListItem Value="1">Onaylandı</asp:ListItem>
                        <asp:ListItem Value="-1">Onaylanmadı</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:TextBox ID="tb_talep_durum_aciklama" runat="server" placeholder="Durum Açıklama" Height="100px" TextMode="MultiLine" Width="705px" /><br />
                    <asp:Button ID="bt_talep_kayit" Text="kayıt" runat="server" OnClick="bt_talep_kayit_Click" />
                </div>
            </asp:Panel>

            <asp:Panel ID="panel_talep_gonder" runat="server">
                <asp:TextBox ID="tb_talep_gonder_ad" runat="server" placeholder="Ad" />
                <asp:TextBox ID="tb_talep_gonder_sad" runat="server" placeholder="Soyad" /><br />
                <asp:TextBox ID="tb_talep_gonder_konu" runat="server" Width="246px" placeholder="Konu" />
                <br />
                <asp:TextBox ID="tb_talep_gonder_aciklama" runat="server" Height="137px" placeholder="Açıklama" TextMode="MultiLine" Width="705px" />
                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="auto-style1" />
                <asp:Button ID="bt_talep_gonder" runat="server" OnClick="bt_talep_gonder_Click" Text="Gönder" />
            </asp:Panel>
            <asp:Panel ID="panel_taleplerim" runat="server">
                <asp:ListView ID="ListView2" runat="server" DataSourceID="SqlDataSource1">
                    <ItemTemplate>
                        <asp:Button ID="tp" CssClass='<%#Eval ("tp_id") %>' Text='<%#Eval ("tp_konu") %>' runat="server" OnClick="nesne2" Visible=' <%#Eval ("kis_id").ToString() == Session["kis_id"].ToString() ? true:false%>' />
                    </ItemTemplate>
                </asp:ListView>

            </asp:Panel>
        </div>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:baglantii %>" SelectCommand="SELECT * FROM [talepp] ORDER BY [tp_tarih] DESC"></asp:SqlDataSource>


    </form>
</body>
</html>

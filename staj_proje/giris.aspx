<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="giris.aspx.cs" Inherits="staj_proje.giris" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-form">
            <asp:TextBox ID="tb_mail" runat="server" placeholder="E-mail" />
            <br />
            <asp:TextBox ID="tb_sif" runat="server" placeholder="şifre" TextMode="Password" />
            <br />
            <asp:Button ID="bt_giris" runat="server" Text="Giriş" OnClick="bt_giris_Click" />
            <asp:LinkButton ID="bt_kayit_git" Text="Kayıt Ol" runat="server" OnClick="bt_kayit_git_Click" />
        </div>
        
        <asp:Panel CssClass="login-form" ID="panel_kayit" runat="server" Visible="False">
            <asp:TextBox ID="tb_kayit_mail" runat="server" placeholder="E-Mail" /><br />
            <asp:TextBox ID="tb_kayit_sif" runat="server" placeholder="Şifre" /><br />
            <asp:TextBox ID="tb_kayit_ad" runat="server" placeholder="Ad" /><br />
            <asp:TextBox ID="tb_kayit_sad" runat="server" placeholder="Soyad" /><br />
            <asp:TextBox ID="tb_kayit_tel" runat="server" placeholder="Telefon" /><br />
            <asp:Button ID="bt_kayit" runat="server" Text="Kayıt Ol" OnClick="bt_kayit_Click" />
        </asp:Panel>
    </form>
</body>
</html>

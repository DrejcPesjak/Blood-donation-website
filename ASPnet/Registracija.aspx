<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Registracija.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="vnos">
        <h3>Registracija</h3>        
        Uporabniško ime:  <asp:TextBox ID="TextBoxUser" runat="server"></asp:TextBox> <br />
        Geslo: <asp:TextBox ID="TextBoxPass" runat="server" TextMode="Password"></asp:TextBox> <br />
        <asp:Button ID="ButtonRegistriraj" runat="server" Text="Registriraj" OnClick="ButtonRegistriraj_Click"/>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Prijava.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table id="prijava">
        <tr>
            <td colspan="2"><h2>Prijava</h2></td>
        </tr>
        <tr>
            <td>Uporabniško ime:</td>
            <td><asp:TextBox ID="TextBoxUser" runat="server" onkeydown="this.value = this.value.replace(/[' =-]/, '')"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Geslo:</td>
            <td><asp:TextBox ID="TextBoxPass" runat="server" TextMode="Password"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2"><asp:Button ID="Button1" runat="server" Text="Prijava" OnClick="Button1_Click" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="LabelWarning" runat="server" Text="Prijava neuspešna. Poskusite znova." ForeColor="Red"></asp:Label></td>
        </tr>
    </table>
</asp:Content>


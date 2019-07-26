<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Statistika.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Statistika</h2>

    Izberite akcijo: <asp:DropDownList ID="DropDownListAkcije" runat="server" OnSelectedIndexChanged="DropDownListAkcije_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList><br />
    <div id="StatAkcija" runat="server">
    </div>
</asp:Content>


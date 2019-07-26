<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OddajaKrvi.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Oddaja krvi</h2>
    <table id="dinamicna">
        <thead>
            <tr>
                <td>Številka krvodajalca <asp:RadioButton ID="RadioButtonIDKrvDaj" GroupName="OrderBy"  runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
                <td>Ime krvodajalca <asp:RadioButton ID="RadioButtonIme" GroupName="OrderBy" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
                <td>Priimek krvodajalca <asp:RadioButton ID="RadioButtonPriimek" GroupName="OrderBy" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
                <td>Datum <asp:RadioButton ID="RadioButtonDatum" GroupName="OrderBy" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
                <td>Količina <asp:RadioButton ID="RadioButtonKol" GroupName="OrderBy" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
                <td>Številka akcije <asp:RadioButton ID="RadioButtonIDAkc" GroupName="OrderBy" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>           
            </tr>            
        </thead>    
        
        <tbody id="OddajaKrvi" runat="server">
        </tbody> 
    </table>

    <div class="vnos">
        <h3>Dodaj</h3>
        Številka krvodajalca: <asp:DropDownList ID="DropDownListKrvDaj" runat="server"></asp:DropDownList><br />
        Številka akcije:  <asp:DropDownList ID="DropDownListAkc" runat="server"></asp:DropDownList><br />
        Datum: <asp:TextBox ID="TextBoxDatum" runat="server"></asp:TextBox><br />
        Količina: <asp:TextBox ID="TextBoxKol" runat="server"></asp:TextBox><br />
                    
        <asp:Button ID="ButtonAdd" runat="server" Text="Dodaj" OnClick="ButtonAdd_Click" />  
        
        <asp:Label ID="LabelWarning" runat="server" Text="Napaka" ForeColor="Red"></asp:Label>
    </div>

    <div class="vnos">
        <h3>Izbriši</h3>        
        Zaporedna številka: <asp:DropDownList ID="DropDownListIDIzbrisi" runat="server"></asp:DropDownList>
        <asp:Button ID="ButtonIzbrisi" runat="server" Text="Izbriši" OnClick="ButtonIzbrisi_Click" />
    </div>
</asp:Content>


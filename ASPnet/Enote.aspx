<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Enote.aspx.cs" Inherits="Default4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Enote</h2>
    <table id="dinamicna">
        <thead>
            <tr>
                <td>Zaporedna številka enote <asp:RadioButton ID="RadioButtonID" GroupName="OrderBy"  runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
                <td>Naziv enote<asp:RadioButton ID="RadioButtonNaziv" GroupName="OrderBy" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
                <td>Naslov <asp:RadioButton ID="RadioButtonNaslov" GroupName="OrderBy" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
                <td>Poštna številka <asp:RadioButton ID="RadioButtonPosta" GroupName="OrderBy" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
                <td>Kraj <asp:RadioButton ID="RadioButtonKraj" GroupName="OrderBy" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
                <td>Telefonska številka <asp:RadioButton ID="RadioButtonTel" GroupName="OrderBy" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
            </tr>            
        </thead>    
        
        <tbody id="KrvodajalskeEnote" runat="server">
        </tbody> 
    </table>

    <div class="vnos">
        <h3>Dodaj</h3>
        Naziv enote: <asp:TextBox ID="TextBoxNaziv" runat="server"></asp:TextBox><br />
        Naslov: <asp:TextBox ID="TextBoxNaslov" runat="server"></asp:TextBox><br />
        Telefonska številka: <asp:TextBox ID="TextBoxTel" runat="server"></asp:TextBox><br />
        Poštna številka: <asp:TextBox ID="TextBoxPosta" runat="server"></asp:TextBox><br />
                    
        <asp:Button ID="ButtonAdd" runat="server" Text="Dodaj" OnClick="ButtonAdd_Click" />   
        
        <asp:Label ID="LabelWarning" runat="server" Text="Napaka" ForeColor="Red"></asp:Label>
    </div>

    <div class="vnos">
        <h3>Spremeni</h3>
        Zaporedna številka <asp:DropDownList ID="DropDownListIDSpremeni" runat="server"></asp:DropDownList><br />
        Naziv: <asp:TextBox ID="TextBoxSpremeniNaziv" runat="server"></asp:TextBox><br />
        Telefonska številka:<asp:TextBox ID="TextBoxSpremeniTele" runat="server"></asp:TextBox><br />
        
        <asp:Button ID="ButtonSpremeni" runat="server" Text="Spremeni" OnClick="ButtonSpremeni_Click" />
    </div>

    <div class="vnos">
        <h3>Izbriši</h3>
        Zaporedna številka: <asp:DropDownList ID="DropDownListIDIzbrisi" runat="server"></asp:DropDownList> <asp:Button ID="ButtonIzbrisi" runat="server" Text="Izbriši" OnClick="ButtonIzbrisi_Click" />
        
        <asp:Label ID="LabelWarningDelete" runat="server" Text="Napaka" ForeColor="Red"></asp:Label>
    </div>
</asp:Content>


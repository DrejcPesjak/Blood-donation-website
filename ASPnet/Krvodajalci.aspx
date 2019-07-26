<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Krvodajalci.aspx.cs" Inherits="Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Krvodajalci</h2>
    <table id="dinamicna">
        <thead>
            <tr>
                <td>Zaporedna številka <asp:RadioButton ID="RadioButtonID" GroupName="OrderBy"  runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
                <td>Ime krvodajalca <asp:RadioButton ID="RadioButtonIme" GroupName="OrderBy" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
                <td>Priimek krvodajalca <asp:RadioButton ID="RadioButtonPriimek" GroupName="OrderBy" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
                <td>EMŠO <asp:RadioButton ID="RadioButtonEMSO" GroupName="OrderBy" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
                <td>Spol <asp:RadioButton ID="RadioButtonSpol" GroupName="OrderBy" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
                <td>Krvna skupina <asp:RadioButton ID="RadioButtonKrvnSk" GroupName="OrderBy" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
                <td>Naslov<asp:RadioButton ID="RadioButtonNasl" GroupName="OrderBy" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
                <td>Poštna številka <asp:RadioButton ID="RadioButtonPostna" GroupName="OrderBy" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
                <td>Kraj <asp:RadioButton ID="RadioButtonKraj" GroupName="OrderBy" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>                
            </tr>            
        </thead>    
        
        <tbody id="Krvodajalci" runat="server">
        </tbody> 
    </table>

    <div class="vnos">
        <h3>Dodaj</h3>
        Ime: <asp:TextBox ID="TextBoxIme" runat="server"></asp:TextBox><br />
        Priimek: <asp:TextBox ID="TextBoxPriimek" runat="server"></asp:TextBox><br />
        EMŠO: <asp:TextBox ID="TextBoxEMSO" runat="server"></asp:TextBox><br />
        Spol: <asp:TextBox ID="TextBoxSpol" runat="server"></asp:TextBox><br />
        Krvna sk: <asp:TextBox ID="TextBoxKrvSk" runat="server"></asp:TextBox><br />
        Naslov: <asp:TextBox ID="TextBoxNasl" runat="server"></asp:TextBox><br />
        Poštna št.: <asp:TextBox ID="TextBoxPostna" runat="server"></asp:TextBox><br />
                    
        <asp:Button ID="ButtonAdd" runat="server" Text="Dodaj" OnClick="ButtonAdd_Click" />   
        
        <asp:Label ID="LabelWarning" runat="server" Text="Napaka" ForeColor="Red"></asp:Label>
    </div>

    <div class="vnos">
        <h3>Spremeni</h3>
        Zaporedna številka <asp:DropDownList ID="DropDownListIDSpremeni" runat="server"></asp:DropDownList><br />
        Ime: <asp:TextBox ID="TextBoxSpremeniIme" runat="server"></asp:TextBox><br />
        Priimek: <asp:TextBox ID="TextBoxSpremeniPriimek" runat="server"></asp:TextBox><br />
        Naslov: <asp:TextBox ID="TextBoxSpremeniNasl" runat="server"></asp:TextBox><br />
        Poštna št.: <asp:TextBox ID="TextBoxSpremeniPostna" runat="server"></asp:TextBox><br /> 

        <asp:Button ID="ButtonSpremeni" runat="server" Text="Spremeni" OnClick="ButtonSpremeni_Click" />
    </div>

    <div class="vnos">
        <h3>Izbriši</h3>
        Zaporedna številka: <asp:DropDownList ID="DropDownListIDIzbrisi" runat="server"></asp:DropDownList> <asp:Button ID="ButtonIzbrisi" runat="server" Text="Izbriši" OnClick="ButtonIzbrisi_Click" />
        
        <asp:GridView ID="GridViewPotrebnoIzbrisati" runat="server" Visible="False" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField HeaderText="Ime" />
                <asp:BoundField HeaderText="Priimek" />
                <asp:BoundField HeaderText="Datum Oddaje" />
                <asp:BoundField HeaderText="Količina" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
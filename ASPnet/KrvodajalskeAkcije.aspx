<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="KrvodajalskeAkcije.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Krvodajalske Akcije</h2>
    <asp:CheckBox ID="CheckBoxDoDanes" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True"></asp:CheckBox> Prikaži le aktualne akcije.
    <table id="dinamicna">
        <thead>
            <tr>
                <td>Zaporedna številka akcije <asp:RadioButton ID="RadioButtonID" GroupName="OrderBy"  runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
                <td>Začetek akcije <asp:RadioButton ID="RadioButtonZDatum" GroupName="OrderBy" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
                <td>Konec akcije <asp:RadioButton ID="RadioButtonKDatum" GroupName="OrderBy" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
                <td>Vrsta <asp:RadioButton ID="RadioButtonVrsta" GroupName="OrderBy" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
                <td>Naziv enote <asp:RadioButton ID="RadioButtonEnota" GroupName="OrderBy" runat="server" OnCheckedChanged="CheckedChangedAll" AutoPostBack="True" /></td>
            </tr>            
        </thead>    
        
        <tbody id="KrvodajalskeAkcije" runat="server">
        </tbody> 
    </table>

    <div class="vnos">
        <h3>Dodaj</h3>
        Začetni datum: <asp:TextBox ID="TextBoxZacetek" runat="server"></asp:TextBox><br />
        Končni datum: <asp:TextBox ID="TextBoxKonec" runat="server"></asp:TextBox><br />
        Vrsta akcije: <asp:TextBox ID="TextBoxVrsta" runat="server"></asp:TextBox><br />
        Naziv enote: <asp:TextBox ID="TextBoxEnota" runat="server"></asp:TextBox><br />
                    
        <asp:Button ID="ButtonAdd" runat="server" Text="Dodaj" OnClick="ButtonAdd_Click" />   
        
        <asp:Label ID="LabelWarning" runat="server" Text="Napaka" ForeColor="Red"></asp:Label>
    </div>

    <div class="vnos">
        <h3>Spremeni</h3>
        Zaporedna številka <asp:DropDownList ID="DropDownListIDSpremeni" runat="server"></asp:DropDownList><br />
        Začetni datum: <asp:TextBox ID="TextBoxSpremeniZD" runat="server"></asp:TextBox><br />
        Končni datum:<asp:TextBox ID="TextBoxSpremeniKD" runat="server"></asp:TextBox><br />
        
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


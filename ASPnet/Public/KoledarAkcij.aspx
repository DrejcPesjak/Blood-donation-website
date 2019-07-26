<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="KoledarAkcij.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Koledar akcij</h2>
    <table id="dinamicna">
        <thead>
            <tr>
                <td>Začetek akcije</td>
                <td>Konec akcije</td>
                <td>Kraj</td>
                <td>Naziv enote</td>
            </tr>            
        </thead>    
        
        <tbody id="koledarAkcijTabela" runat="server">
        </tbody>    
    </table>
</asp:Content>


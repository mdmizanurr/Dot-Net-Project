<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckoutError.aspx.cs" Inherits="ToyDemoProj.Chekout.CheckoutError" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Checkout Error</h1>
    <p></p>

    <table id="ErrorTable">
        <tr>
            <td class="field"></td>
            <td><%=Request.QueryString.Get("ErrorCode") %></td> 

        </tr>

        <tr>
            <td class="fiels" ></td>
            <td><%=Request.QueryString.Get("Desc") %></td>

        </tr>

        <tr>
            <td class="field"></td>
            <td><%=Request.QueryString.Get("Desc2") %></td>
        </tr>

    </table>

    <p></p>

</asp:Content>

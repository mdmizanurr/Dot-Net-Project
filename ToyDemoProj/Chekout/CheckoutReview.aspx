<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
CodeBehind="CheckoutReview.aspx.cs" Inherits="ToyDemoProj.Checkout.CheckoutReview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Order Review</h1>
        <p></p>
    <h3 style="padding-left: 33px">Products:</h3>
    <asp:GridView ID="OrderItemList" runat="server" AutoGenerateColumns="False" GridLines="Both"
        CellPadding="10" Width="500" BorderColor="#efeeef" BorderWidth="33">
    <Columns>
        <asp:BoundField DataField="ProductId" HeaderText=" Product ID" />
        <asp:BoundField DataField="Product.ProductName" HeaderText=" Product Name" />
        <asp:BoundField DataField="Product.UnitPrice" HeaderText="Price (each)" DataFormatString="{0:c}"/>
        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
    </Columns>
    </asp:GridView>
    <asp:DetailsView ID="ShipInfo" runat="server" AutoGenerateRows="False" GridLines="None"
        CellPadding="10" BorderStyle="None" CommandRowStyle-BorderStyle="None">
<CommandRowStyle BorderStyle="None"></CommandRowStyle>
        <Fields>
            <asp:TemplateField> 

            </asp:TemplateField>

            <asp:TemplateField>
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>

        </Fields>

    </asp:DetailsView>
    <p></p>
    <hr />
    <asp:Button ID="CheckoutConfirm" runat="server" Text="Complete Order"
        OnClick="CheckoutConfirm_Click" />

</asp:Content>
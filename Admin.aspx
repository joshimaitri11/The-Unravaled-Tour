<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
<h2>Admin: tour dates</h2>
<div class="panel">
  <asp:Panel ID="pnlAdd" runat="server" DefaultButton="btnAdd" style="display:flex;gap:.5rem;flex-wrap:wrap">
    <asp:TextBox ID="txtDate" runat="server" TextMode="Date"></asp:TextBox>
    <asp:TextBox ID="txtCity" runat="server" placeholder="City"></asp:TextBox>
    <asp:TextBox ID="txtCountry" runat="server" placeholder="Country"></asp:TextBox>
    <asp:TextBox ID="txtVenue" runat="server" placeholder="Venue"></asp:TextBox>
    <asp:Button ID="btnAdd" runat="server" Text="ADD DATE" CssClass="btn" OnClick="btnAdd_Click" />
  </asp:Panel>
  <asp:Label ID="lblMsg" runat="server" CssClass="err"></asp:Label>
</div>
<div class="tw">
<asp:GridView ID="gvTours" runat="server" DataKeyNames="TourId" AutoGenerateColumns="false" AllowPaging="true" PageSize="15" CssClass="admin"
  OnPageIndexChanging="gvTours_PageIndexChanging" OnRowEditing="gvTours_RowEditing" OnRowCancelingEdit="gvTours_RowCancelingEdit"
  OnRowUpdating="gvTours_RowUpdating" OnRowDeleting="gvTours_RowDeleting">
  <Columns>
    <asp:BoundField DataField="TourDate" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" ApplyFormatInEditMode="true" />
    <asp:BoundField DataField="City" HeaderText="City" />
    <asp:BoundField DataField="Country" HeaderText="Country" />
    <asp:BoundField DataField="Venue" HeaderText="Venue" />
    <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
  </Columns>
</asp:GridView>
</div>
</asp:Content>

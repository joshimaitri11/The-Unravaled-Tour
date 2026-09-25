<%@ Page Title="Tour Dates" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Tours.aspx.cs" Inherits="Tours" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
<h2>Tour dates</h2><p class="hw">every city, every night</p>
<asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch" CssClass="bar">
  <asp:TextBox ID="txtQ" runat="server" placeholder="Search city or country" style="flex:1;min-width:200px"></asp:TextBox>
  <asp:Button ID="btnSearch" runat="server" Text="SEARCH" CssClass="btn" OnClick="btnSearch_Click" />
</asp:Panel>
<div class="grid">
<asp:Repeater ID="rptTours" runat="server">
  <ItemTemplate>
    <article class="tk">
      <div class="stub" style='background:<%# StubColor(Container.ItemIndex) %>'>
        <span><%# ((DateTime)Eval("TourDate")).ToString("MMM").ToUpper() %></span><b><%# ((DateTime)Eval("TourDate")).Day %></b><span><%# ((DateTime)Eval("TourDate")).Year %></span>
      </div>
      <div class="in">
        <h3><%# Eval("City") %></h3><p><%# Eval("Country") %></p><p><b><%# Eval("Venue") %></b></p>
        <div class="dt"><%# Eval("TourDate", "{0:MMMM d, yyyy}") %><br />Special guest varies per city.<br />From $89 in this planner.</div>
        <div class="row"><button type="button" class="btn alt" onclick="this.closest('.tk').classList.toggle('open')">VIEW DETAILS</button><a class="btn" href='Plan.aspx?id=<%# Eval("TourId") %>'>PLAN THIS NIGHT</a></div>
      </div>
    </article>
  </ItemTemplate>
</asp:Repeater>
</div>
<asp:Label ID="lblNone" runat="server" CssClass="hw" Visible="false" Text="no city found. try another search."></asp:Label>
</asp:Content>

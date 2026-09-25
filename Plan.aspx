<%@ Page Title="Plan Your Concert" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Plan.aspx.cs" Inherits="Plan" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
<h2>Plan your concert</h2><p class="hw">tickets, travel and your whole night in one place</p>
<asp:Literal ID="litTicket" runat="server"></asp:Literal>
<div class="grid" style="align-items:start;grid-template-columns:repeat(auto-fit,minmax(300px,1fr))">
  <div>
    <div class="panel" style="margin-top:0">
      <div class="f"><label for="<%= ddlShow.ClientID %>">1. Choose your show</label>
        <asp:DropDownList ID="ddlShow" runat="server" AutoPostBack="true"></asp:DropDownList></div>
      <p class="hw" style="margin:.2rem 0"><asp:Label ID="lblCd" runat="server"></asp:Label></p>
      <div class="f" style="margin-top:.8rem">2. Choose your pass</div>
      <asp:RadioButtonList ID="rblPass" runat="server" CssClass="opts" RepeatLayout="Flow" AutoPostBack="true">
        <asp:ListItem Value="89" Selected="True">General Admission ($89, standing floor)</asp:ListItem>
        <asp:ListItem Value="149">Lower Bowl ($149, seated close view)</asp:ListItem>
        <asp:ListItem Value="299">Pit VIP ($299, front section and tote)</asp:ListItem>
      </asp:RadioButtonList>
      <div class="f" style="margin-top:1rem"><label for="<%= ddlQty.ClientID %>">3. How many tickets</label>
        <asp:DropDownList ID="ddlQty" runat="server" AutoPostBack="true">
          <asp:ListItem>1</asp:ListItem><asp:ListItem Selected="True">2</asp:ListItem><asp:ListItem>3</asp:ListItem><asp:ListItem>4</asp:ListItem>
          <asp:ListItem>5</asp:ListItem><asp:ListItem>6</asp:ListItem><asp:ListItem>7</asp:ListItem><asp:ListItem>8</asp:ListItem>
        </asp:DropDownList></div>
    </div>
    <div class="panel">
      <div class="f">4. Plan the rest of your night</div>
      <asp:CheckBoxList ID="cblExtras" runat="server" CssClass="opts" RepeatLayout="Flow" AutoPostBack="true">
        <asp:ListItem Value="60">Tour merch budget ($60)</asp:ListItem>
        <asp:ListItem Value="40">Travel to the city ($40)</asp:ListItem>
        <asp:ListItem Value="110">Hotel night ($110)</asp:ListItem>
        <asp:ListItem Value="35">Pre-show dinner ($35)</asp:ListItem>
      </asp:CheckBoxList>
      <div class="f"><label for="<%= ddlVibe.ClientID %>">Outfit vibe</label>
        <asp:DropDownList ID="ddlVibe" runat="server">
          <asp:ListItem>Paper doll pink</asp:ListItem><asp:ListItem>Powder blue dreamy</asp:ListItem>
          <asp:ListItem>Lavender diary</asp:ListItem><asp:ListItem>All black with pink laces</asp:ListItem>
        </asp:DropDownList></div>
    </div>
  </div>
  <div class="panel" style="margin-top:0;position:sticky;top:calc(70px + env(safe-area-inset-top,0px))">
    <h3>Your night</h3>
    <p>Tickets <b><asp:Label ID="lblTickets" runat="server"></asp:Label></b><br />Extras <b><asp:Label ID="lblExtras" runat="server"></asp:Label></b></p>
    <div class="total"><asp:Label ID="lblTotal" runat="server"></asp:Label></div>
    <div class="f"><label for="<%= txtName.ClientID %>">Your name</label><asp:TextBox ID="txtName" runat="server" autocomplete="name"></asp:TextBox></div>
    <div class="f">Email <asp:Label ID="lblEmail" runat="server" Font-Bold="true"></asp:Label></div>
    <asp:Button ID="btnBook" runat="server" Text="BOOK MY TICKETS" CssClass="btn" OnClick="btnBook_Click" />
    <asp:Label ID="lblErr" runat="server" CssClass="err"></asp:Label>
  </div>
</div>
<h3 style="margin-top:2rem">Your tickets</h3>
<div class="grid" style="margin-top:1rem">
<asp:Repeater ID="rptMine" runat="server" OnItemCommand="rptMine_ItemCommand">
  <ItemTemplate>
    <article class="conf" style="margin:0;padding:1.2rem;transform:none">
      <h2 style="font-size:1.8rem"><%# Eval("City") %></h2>
      <dl>
        <dt>Date</dt><dd><%# Eval("TourDate", "{0:MMM d, yyyy}") %></dd>
        <dt>Venue</dt><dd><%# Eval("Venue") %></dd>
        <dt>Ticket</dt><dd><%# Eval("Pass") %></dd>
        <dt>Quantity</dt><dd><%# Eval("Quantity") %></dd>
        <dt>Total</dt><dd>$<%# Eval("TicketTotal") %></dd>
        <dt>Night budget</dt><dd>$<%# Eval("NightBudget") %></dd>
        <dt>Outfit</dt><dd><%# Eval("Outfit") %></dd>
        <dt>Booking ID</dt><dd><%# Eval("BookingId") %></dd>
      </dl>
      <asp:LinkButton runat="server" CssClass="btn alt" CommandName="cancel" CommandArgument='<%# Eval("BookingId") %>' OnClientClick="return confirm('Cancel this booking?');" CausesValidation="false">CANCEL</asp:LinkButton>
    </article>
  </ItemTemplate>
</asp:Repeater>
</div>
<asp:Label ID="lblNoTix" runat="server" Text="No tickets yet. Book a show above and it will show up here." Visible="false"></asp:Label>
</asp:Content>

<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
<div class="hero">
  <svg class="thread" viewBox="0 0 1000 500" preserveAspectRatio="none"><path d="M0 420C120 300 200 480 330 380S520 260 640 360 860 300 1000 120"/></svg>
  <svg class="fl" style="left:52%;top:0"><use href="#fl"/></svg><svg class="fl" style="left:3%;bottom:0;color:var(--lv)"><use href="#fl"/></svg>
  <div><small>OLIVIA RODRIGO</small><h1>THE UNRAVELED TOUR</h1><p class="hw" style="font-size:1.7rem;margin:.6rem 0 1.4rem">you seem pretty sad for a girl so in love</p><a class="btn" href="Tours.aspx">EXPLORE THE TOUR</a></div>
  <div class="polaroid" style="max-width:340px;justify-self:center"><i class="tape"></i><img src="images/album.jpg" alt="you seem pretty sad for a girl so in love album cover" /><span class="cap">the album, on repeat</span></div>
</div>
<div class="poster"><img src="images/banner.webp" alt="The Unraveled Tour banner" /></div>
<div class="torn"></div>
<div class="grid">
  <div class="panel"><h3>Find your night</h3><p>Browse every city and date on the tour.</p><a class="btn alt" href="Tours.aspx">TOUR DATES</a></div>
  <div class="panel" style="background:var(--bl);color:#171417"><h3>Plan your concert</h3><p>Tickets, travel, stay and outfit, planned in one place.</p><a class="btn" href="Plan.aspx">PLAN YOUR CONCERT</a></div>
</div>
</asp:Content>

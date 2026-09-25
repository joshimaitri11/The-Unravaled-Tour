<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
<meta charset="utf-8"/><meta name="viewport" content="width=device-width, initial-scale=1, viewport-fit=cover"/>
<title>Log in | The Unraveled Tour</title>
<link href="https://fonts.googleapis.com/css2?family=Courier+Prime:wght@400;700&amp;family=Caveat:wght@600&amp;family=DM+Sans:wght@400;500;700&amp;family=DM+Serif+Display&amp;display=swap" rel="stylesheet"/>
<link href="style.css" rel="stylesheet"/>
</head>
<body>
<svg width="0" height="0" style="position:absolute"><symbol id="fl" viewBox="0 0 40 40"><g fill="none" stroke="currentColor" stroke-width="2.2" stroke-linecap="round"><ellipse cx="20" cy="9" rx="4.5" ry="7"/><ellipse cx="20" cy="9" rx="4.5" ry="7" transform="rotate(72 20 20)"/><ellipse cx="20" cy="9" rx="4.5" ry="7" transform="rotate(144 20 20)"/><ellipse cx="20" cy="9" rx="4.5" ry="7" transform="rotate(216 20 20)"/><ellipse cx="20" cy="9" rx="4.5" ry="7" transform="rotate(288 20 20)"/></g></symbol></svg>
<form id="form1" runat="server">
<section id="login" class="v">
  <div class="strip"></div>
  <svg class="fl" style="left:12px;top:52px;color:#fff"><use href="#fl"/></svg>
  <svg class="fl" style="right:16px;bottom:24px;color:var(--hot)"><use href="#fl"/></svg>
  <div class="lg">
    <asp:Panel ID="pnlLogin" runat="server" DefaultButton="btnLogin">
      <h2>log in to<br />the unraveled tour</h2>
      <p style="text-align:center;margin:.4rem 0 1.2rem;font-family:Caveat,cursive;font-size:1.4rem;text-transform:none">you seem pretty sad for a girl so in love</p>
      <label for="txtEmail" style="font-weight:700;display:block;margin-bottom:.5rem">enter your email</label>
      <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeholder="enter your email"></asp:TextBox>
      <asp:Button ID="btnLogin" runat="server" Text="log in" CssClass="pill" OnClick="btnLogin_Click" />
      <asp:Label ID="lblErr" runat="server" CssClass="err"></asp:Label>
      <small>no password needed. your plans are saved under this email.<br />admin demo: admin@unraveled.fan</small>
    </asp:Panel>
    <div class="sp">
      <div class="sph"><img src="images/album.jpg" alt="Album cover" /><div><b>you seem pretty sad for a girl so in love</b><span>Olivia Rodrigo</span><br /><a class="sv" href="https://open.spotify.com/search/you%20seem%20pretty%20sad%20for%20a%20girl%20so%20in%20love" target="_blank" rel="noopener">+ Save on Spotify</a></div></div>
      <a class="play" href="https://open.spotify.com/search/you%20seem%20pretty%20sad%20for%20a%20girl%20so%20in%20love" target="_blank" rel="noopener" aria-label="Play on Spotify">&#9654;</a>
      <ol><li><span>1&nbsp; serena joy</span><i>3:05</i></li><li><span>2&nbsp; stupid song</span><i>3:29</i></li><li><span>3&nbsp; drop dead</span><i>3:44</i></li></ol>
      <a href="https://open.spotify.com/search/you%20seem%20pretty%20sad%20for%20a%20girl%20so%20in%20love" target="_blank" rel="noopener" style="display:block;text-align:center;font-size:.85rem;color:#b3b3b3;margin-top:.4rem">open the full album on Spotify</a>
    </div>
  </div>
</section>
</form>
<script src="site.js"></script>
</body>
</html>

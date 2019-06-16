<%@ Page Title="" Language="C#" MasterPageFile="~/GameMachine.Master" AutoEventWireup="true" 
    CodeBehind="Home.aspx.cs" Inherits="GameMachineWebApp.secure.Home" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../css/home.css" type="text/css" rel="stylesheet" />

    <div id="heading">
        <asp:Label ID="lblWelcomeMsg" runat="server" Font-Size="X-Large" ForeColor="White">

        </asp:Label>
        <br />
        <asp:Label ID="lblAllWins" runat="server" Font-Size="Medium" ForeColor="White" Font-Italic="true">
        </asp:Label>
    </div>

    <div class="picArea">
        
        <div id="bjack" class="pics">
            <img src="../images/blackjack2.jpg" width="225" class="imgs" />
            <br />
            <asp:Label ID="lblBWins" runat="server"></asp:Label>
            <br /><br />
            <asp:Button ID="btnPlayBLA" runat="server" CssClass="btns" Text="Play" 
                OnClick="btnPlayBLA_Click"/>
        </div>

        <div id="conn4" class="pics">
            <img src="../images/connect4.jpg" width="225" class="imgs"/>
            <br />
            <asp:Label ID="lblCWins" runat="server"></asp:Label>
            <br /><br />
            <asp:Button ID="btnPlayCON" runat="server" Text="Play" CssClass="btns" 
                OnClick="btnPlayCON_Click" />
        </div>
    </div>

</asp:Content>

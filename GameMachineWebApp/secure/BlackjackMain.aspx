<%@ Page Title="" Language="C#" MasterPageFile="~/GameMachine.Master" AutoEventWireup="true" 
    CodeBehind="BlackjackMain.aspx.cs" Inherits="GameMachineWebApp.secure.BlackjackMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../css/blackjack.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../scripts/blackjack.js"></script>
    <asp:ScriptManager ID="scriptManager1" runat="server"></asp:ScriptManager>

    <div class="flexContainer">

        <div id="dealerArea">

            <div id="newGameBtn">
                <asp:Button ID="btnNewGame" CssClass="btns" runat="server" Text="New Game" OnClick="btnNewGame_Click"/>
            </div>

            <div id="dealerCards">

                <asp:Panel ID="dPanel" runat="server" HorizontalAlign="Center" Enabled="true" CssClass="panel" >
                    <asp:Image ID="dCard1" runat="server" Width="100"/>
                    <asp:Image ID="dCard2" runat="server" Width="100"/>
                    <asp:Image ID="dCard3" runat="server" Width="100"/>
                    <asp:Image ID="dCard4" runat="server" Width="100"/>
                    <asp:Image ID="dCard5" runat="server" Width="100"/>
                </asp:Panel>
            </div>

            <div id="dName" class="nameArea">
                Dealer
                <asp:Label ID="dScore" runat="server"></asp:Label>
            </div>
        </div>

        <br /><br />
        <asp:Label ID="lblResultMsg" runat="server" Visible="false" BackColor="WhiteSmoke" ForeColor="DarkRed" 
            BorderColor="DarkRed" BorderStyle="Groove" Font-Bold="true" Font-Size="30"
            >
        </asp:Label>
        <br /><br /><br />

        <div id="playerArea">

            <div id="playerBtns">
                <asp:Button ID="btnHit" CssClass="btns" runat="server" Text="HIT" OnClick="btnHit_Click"/>
                <br /><br /><br />
                <asp:Button ID="btnStand" CssClass="btns" runat="server" Text="STAND" OnClick="btnStand_Click"/>
            </div>

            <div id="playerCards">

                <asp:Panel ID="pPanel" runat="server" HorizontalAlign="Center" Enabled="true" CssClass="panel" >
                    <asp:Image ID="pCard1" runat="server" Width="100"/>
                    <asp:Image ID="pCard2" runat="server" Width="100"/>
                    <asp:Image ID="pCard3" runat="server" Width="100"/>
                    <asp:Image ID="pCard4" runat="server" Width="100"/>
                    <asp:Image ID="pCard5" runat="server" Width="100"/>
                </asp:Panel>
            </div>

            <div id="pName" class="nameArea">
                You
                <asp:Label ID="pScore" runat="server"></asp:Label>
            </div>
        
        </div>


    </div>

</asp:Content>

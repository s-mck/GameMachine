<%@ Page Title="" Language="C#" MasterPageFile="~/GameMachine.Master" AutoEventWireup="true" 
    CodeBehind="ConnectFourMain.aspx.cs" Inherits="GameMachineWebApp.secure.ConnectFourMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" href="../css/connectfour.css" />
        <div>
            <asp:Table ID="tblBoard" runat="server" HorizontalAlign="Center" Height="36px" Width="53px" CssClass ="table">
            </asp:Table>
                
            <asp:Button ID="btnCol1" runat="server" Text="Place Tile" OnClick="btnCol1_Click" CssClass ="tileBtn" />
            <asp:Button ID="btnCol2" runat="server" Text="Place Tile" OnClick="btnCol2_Click" CssClass ="tileBtn" />
            <asp:Button ID="btnCol3" runat="server" Text="Place Tile" OnClick="btnCol3_Click" CssClass ="tileBtn" />
            <asp:Button ID="btnCol4" runat="server" Text="Place Tile" OnClick="btnCol4_Click" CssClass ="tileBtn" />
            <asp:Button ID="btnCol5" runat="server" Text="Place Tile" OnClick="btnCol5_Click" CssClass ="tileBtn" />
            <asp:Button ID="btnCol6" runat="server" Text="Place Tile" OnClick="btnCol6_Click" CssClass ="tileBtn" />
            <asp:Button ID="btnCol7" runat="server" Text="Place Tile" OnClick="btnCol7_Click" CssClass ="tileBtn" />
        </div>
    <br />
    <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
    <br />
    <br />
    <asp:Button ID="btnNewGame" runat="server" Text="New Game" OnClick="btnNewGame_Click" CssClass ="newGame"/>
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/GameMachine.Master" AutoEventWireup="true" 
    CodeBehind="Login.aspx.cs" Inherits="GameMachineWebApp.Login" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <link href="css/loginSignup.css" type="text/css" rel="stylesheet" />

    <div class="flexContainer">

    <h2 class="head">Welcome to the Game Machine!</h2>

    <div id="loginData" class="mainInfo">
        <asp:Label ID="lblLoginEmail" runat="server">Email:</asp:Label>
        <asp:TextBox ID="txtLoginEmail" runat="server"></asp:TextBox><br />

        <br />
        <asp:Label ID="lblLoginPass" runat="server">Password:</asp:Label>
        <asp:TextBox ID="txtLoginPass" runat="server" TextMode="Password"></asp:TextBox><br />

        <br />
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btns" />
        <br /><br />
    </div>

    <br />
    <div id="signUp" class="bottom">
        Not a registered user? Click <a href="SignUp.aspx">here</a> to sign up!
        <br /><br />
        <asp:Label ID="lblAuthFail" runat="server" Visible="false" ForeColor="DarkRed">
        </asp:Label>
    </div>
 </div>

</asp:Content>

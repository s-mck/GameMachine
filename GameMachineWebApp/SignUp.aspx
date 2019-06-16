<%@ Page Title="" Language="C#" MasterPageFile="~/GameMachine.Master" AutoEventWireup="true" 
    CodeBehind="SignUp.aspx.cs" Inherits="GameMachineWebApp.SignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <link href="css/loginSignup.css" type="text/css" rel="stylesheet" />

    <div class="flexContainer">

    <h3 class="head">Enter your details below to register.</h3>

    <div id="formData" class="mainInfo">

        <asp:Label ID="lblName" runat="server">Name:</asp:Label>
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" 
            Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red">
            Required Field</asp:RequiredFieldValidator>
        <br /><br />

        <asp:Label ID="lblEmail" runat="server">Email:</asp:Label>
        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>

        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail" 
            Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red">
            Required Field
        </asp:RequiredFieldValidator>

        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" 
            Display="Dynamic" ErrorMessage="RegularExpressionValidator" ForeColor="Red"
            ValidationExpression="^[\w\.-]+@[\w- ]+\.[\w\.-]+$">Invalid format
        </asp:RegularExpressionValidator>
        <br /><br />

        <asp:Label ID="lblPass" runat="server">Create a Strong Password:</asp:Label>
        <asp:TextBox ID="txtPass" TextMode="Password" runat="server"></asp:TextBox>

        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPass" 
            Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red">
            Required Field
        </asp:RequiredFieldValidator>

        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPass" 
            Display="Dynamic" ErrorMessage="RegularExpressionValidator" ForeColor="Red"
            ValidationExpression="^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,14}$">Not strong enough
        </asp:RegularExpressionValidator>

        <br /><br />
        <asp:Button class="btns" ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" 
            CausesValidation="true" CssClass="btns"/>

        <br /><br />
     </div>

    <br />
    <div id="logIn" class="bottom">
        Already registered? Click <a href="Login.aspx">here</a> to log in!
        <br /><br />
        <asp:Label ID="lblSignupFail" runat="server" Visible="false" ForeColor="DarkRed">
        </asp:Label>
    </div>

</div>
</asp:Content>

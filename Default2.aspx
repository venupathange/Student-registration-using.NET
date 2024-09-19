<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        form {
            background-color: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            width: 100%;
            max-width: 400px; /* Adjust width for better responsiveness */
        }

        div {
            margin: 0;
            padding: 0;
        }

        .form-label {
            display: block;
            margin-bottom: 8px;
            font-weight: bold;
        }

        .form-control {
            display: block;
            width: 100%;
            padding: 8px;
            margin-bottom: 16px;
            border: 1px solid #ddd;
            border-radius: 4px;
        }

        .form-button {
            background-color: #4CAF50;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

        .form-button:hover {
            background-color: #45a049;
        }

        .error {
            color: red;
            margin-bottom: 16px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="First Name:"></asp:Label>
            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
            
            <asp:Label ID="Label2" runat="server" CssClass="form-label" Text="Date of Birth (yyyy-mm-dd):"></asp:Label>
            <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="form-control"></asp:TextBox>
            
            <asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" ControlToValidate="txtDateOfBirth" ErrorMessage="Date of Birth is required." CssClass="error"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revDateOfBirth" runat="server" ControlToValidate="txtDateOfBirth" ErrorMessage="Invalid date format. Use yyyy-mm-dd." CssClass="error" ValidationExpression="\d{4}-\d{2}-\d{2}"></asp:RegularExpressionValidator>
            
            <asp:Button ID="btnLogin" runat="server" CssClass="form-button" Text="Login" OnClick="btnLogin_Click" />
        </div>
    </form>
</body>
</html>

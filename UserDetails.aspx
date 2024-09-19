<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserDetails.aspx.cs" Inherits="UserDetails" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>User Details</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 20px;
        }
        #form1 {
            max-width: 600px;
            margin: 0 auto;
            background: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }
        #lblDetails {
            display: block;
            font-size: 18px;
            color: #333;
            margin: 10px 0;
        }
  
        .btn {
            background-color: #007bff;
            color: #fff;
            border: none;
            padding: 10px 15px;
            border-radius: 5px;
            cursor: pointer;
            margin: 5px;
        }
        .btn-cancel {
            background-color: #6c757d;
        }
        #hiddenid
        {
        	display:none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblDetails" runat="server" />
            
            <asp:TextBox ID="txtFirstName" runat="server" CssClass="hidden" />
            <asp:TextBox ID="txtLastName" runat="server" CssClass="hidden" />
            <asp:TextBox ID="txtEmail" runat="server" CssClass="hidden" />
            <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="hidden" />
            <asp:TextBox ID="txtDOB" runat="server" CssClass="hidden" />
            <asp:TextBox ID="txtAddress" runat="server" CssClass="hidden" />
             <asp:TextBox ID="txtid" runat="server" CssClass="hiddenid" />

            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn" OnClick="btnUpdate_Click" />
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn hidden" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-cancel hidden" OnClick="btnCancel_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn" OnClick="btnDelete_Click" />
            
     <asp:HiddenField ID="hdnFirstName" runat="server" />
  <%--//   <asp:HiddenField ID="HiddenField1" runat="server" />--%>
        </div>
    </form>
</body>
</html>

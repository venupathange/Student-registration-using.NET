        <%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Student Registration Form</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }
        #form1 {
            width: 60%;
            margin: 0 auto;
            background-color: #fff;
            padding: 20px;
        }
        h2 {
            color: #333;
            margin-bottom: 20px;
        }
        p {
            margin: 15px 0;
        }
        label {
            width: 150px;
            vertical-align: top;
        
        }
        .form-field {
            margin-left: 160px;
            margin-top: 5px;
        }
        .form-field input[type="text"],
        .form-field select,
        .form-field .aspNetCalendar,
        .form-field .aspNetButton,
        .form-field input[type="file"] {
            padding: 10px;
            border: 1px solid #ccc;
        }
        .aspNetButton {
            background-color: #007bff;
            color: #fff;
            border: none;
            cursor: pointer;
            padding: 10px 15px;
        }
        .aspNetButton:hover {
            background-color: #0056b3;
        }
        .validation-summary {
            border: 1px solid #f00;
            background-color: #fdd;
            padding: 10px;
            margin-bottom: 20px;
        }
        .gridview-container {
            margin-top: 20px;
        }
        .aspNetGridView {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
            background-color: #fff;
            border: 1px solid #ddd;
        }
        .aspNetGridView th, .aspNetGridView td {
            padding: 10px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }
        .aspNetGridView th {
            background-color: #f4f4f4;
        }
        .aspNetGridView tr:nth-child(even) {
            background-color: #f9f9f9;
        }
        .aspNetGridView tr:hover {
            background-color: #f1f1f1;
        }
    </style>
    <script type="text/javascript">
        function updateDateOfBirth() {
            var dobInput = document.getElementById('HiddenDateOfBirth').value;
            var dob = new Date(dobInput);
            var today = new Date();

            if (dobInput) {
                var age = today.getFullYear() - dob.getFullYear();
                var monthDiff = today.getMonth() - dob.getMonth();
                if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < dob.getDate())) {
                    age--;
                }
                document.getElementById('ageOutput').innerText = 'Age: ' + age + ' years';
            } else {
                document.getElementById('ageOutput').innerText = 'Please select a date.';
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
        <div>
            <h2>STUDENT REGISTRATION FORM</h2>

            <p>
                <label for="TextBox1">First Name</label>
                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-field"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorFirstName" runat="server"
                    ControlToValidate="TextBox1"
                    ErrorMessage="First Name is required"
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </p>

            <p>
                <label for="TextBox2">Last Name</label>
                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-field"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorLastName" runat="server"
                    ControlToValidate="TextBox2"
                    ErrorMessage="Last Name is required"
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </p>

            <p>
                <label for="TextBox3">Email</label>
                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-field"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server"
                    ControlToValidate="TextBox3"
                    ErrorMessage="Email is required"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server"
                    ControlToValidate="TextBox3"
                    ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                    ErrorMessage="Invalid email address"
                    ForeColor="Red"></asp:RegularExpressionValidator>
            </p>

            <p>
                <label for="TextBox6">Phone Number</label>
                <asp:TextBox ID="TextBox6" runat="server" CssClass="form-field"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPhone" runat="server"
                    ControlToValidate="TextBox6"
                    ErrorMessage="Phone number is required"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorPhone" runat="server"
                    ControlToValidate="TextBox6"
                    ValidationExpression="^\d{10}$" 
                    ErrorMessage="Invalid phone number (must be 10 digits)"
                    ForeColor="Red"></asp:RegularExpressionValidator>
            </p>

            <p>
                <label for="Calendar1">Date of Birth</label>
                <asp:Calendar ID="Calendar1" runat="server" CssClass="form-field" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
            </p>

            <p>
                <asp:HiddenField ID="HiddenDateOfBirth" runat="server" />
                <span id="ageOutput">Age: </span>
            </p>

            <p>
                <label for="DropDownList1">Gender</label>
                <asp:DropDownList ID="DropDownList1" DataTextField="name" DataValueField="id" runat="server" CssClass="form-field">
                    <asp:ListItem Value="">Select the gender</asp:ListItem>
                    <asp:ListItem Value="Male">Male</asp:ListItem>
                    <asp:ListItem Value="Female">Female</asp:ListItem>
                    <asp:ListItem Value="Transgender">Transgender</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorGender" runat="server"
                    ControlToValidate="DropDownList1"
                    InitialValue=""
                    ErrorMessage="Gender is required"
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </p>

            <p>
                <label for="TextBox4">Address</label>
                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-field"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddress" runat="server"
                    ControlToValidate="TextBox4"
                    ErrorMessage="Address is required"
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </p>

  
            <p>
                <label for="FileUpload2">Upload Resume</label>
                <asp:FileUpload ID="FileUpload2" runat="server" CssClass="form-field" />
                <asp:Button ID="Button3" runat="server" Text="Upload" OnClick="Button3_Click" CssClass="aspNetButton" />
                <asp:Label ID="Label2" runat="server" />
            </p>

            <p>
                <label for="FileUpload1">Upload Document</label>
                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-field" />
                <asp:Button ID="Button2" runat="server" Text="Upload" OnClick="Button2_Click" CssClass="aspNetButton" />
                <asp:Label ID="Label1" runat="server" />
            </p>

            <div>
                <asp:Image ID="UploadedImage" runat="server" Width="300px" Height="200px" />
            </div>

            <p>
                <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="form-field aspNetButton" OnClick="Button1_Click" />
            </p>

            <p>
                <asp:Label ID="lblMessage" runat="server" CssClass="form-field" />
            </p>

          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="student_id" OnRowCommand="GridView1_RowCommand">
    <Columns><asp:BoundField DataField="student_id" HeaderText="Student ID" ReadOnly="True" />
        <asp:BoundField DataField="first_Name" HeaderText="First Name" />
        <asp:BoundField DataField="last_Name" HeaderText="Last Name" />
        <asp:BoundField DataField="email" HeaderText="Email" />
        <asp:BoundField DataField="phone_number" HeaderText="Phone Number" />
        <asp:BoundField DataField="date_of_birth" HeaderText="Date of Birth" />
        <asp:BoundField DataField="gender" HeaderText="Gender" />
        <asp:BoundField DataField="address" HeaderText="Address" />
        <asp:BoundField DataField="document_path" HeaderText="Document Path" />
        <asp:BoundField DataField="resume" HeaderText="Resume" />
        <asp:ButtonField CommandName="Delete" Text="Delete" />
    </Columns>
</asp:GridView>
            </div>
     
    </form>
    <script type="text/javascript">
        document.getElementById('<%= HiddenDateOfBirth.ClientID %>').addEventListener('change', updateDateOfBirth);
    </script>
</body>
</html>

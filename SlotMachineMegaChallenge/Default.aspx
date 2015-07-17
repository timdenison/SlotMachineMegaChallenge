<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SlotMachineMegaChallenge.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Image ID="Image1" runat="server" Height="213px" ImageUrl="ImageUrl = &quot;C:\\Users\\timde_000\\Desktop\\C# Projects\\SlotMachineMegaChallenge\\SlotMachineMegaChallenge\\Images\\Plum.png&quot;" Width="213px" />
        <asp:Image ID="Image2" runat="server" Height="213px" Width="213px" />
        <asp:Image ID="Image3" runat="server" Height="213px" Width="213px" />
        <p>
            Your Bet:
            <asp:TextBox ID="betBox" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="leverButton" runat="server" OnClick="leverButton_Click" Text="Pull the Lever!" />
        </p>
        <asp:Label ID="resultLabel" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="moneyLabel" runat="server"></asp:Label>
        <br />
        <br />
        1 Cherry - x2 Your Bet<br />
        2 Cherries - x3 Your Bet<br />
        3 Cherries - x4 Your Bet<br />
        <br />
        3 7s - Jackpot! x100 Your Bet<br />
        <br />
        HOWEVER - If you get even a single &#39;bar&#39;, you win nothing.</form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Ingreso de Departamentos</h1>
            Nombre <asp:TextBox runat ="server" ID="TBNombre"></asp:TextBox>
        <br />
            <br />
            Descripcion <asp:TextBox runat ="server" ID="TBDescripcion"></asp:TextBox>
            <br />
            <br />
            <asp:Button runat="server" ID="BTInsert" Text="Ingresar" OnClick="BTInsert_Click" />
            <br />
            <br />
            <asp:Label ID="LBError" runat="server" Font-Bold="True" Font-Size="12pt" Font-Underline="True" ForeColor="Red"></asp:Label>
            <br />
        </div>
        <asp:GridView runat="server" ID="GVDepartamentos" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="Id" DataSourceID="DSDepartamentos" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
        </asp:GridView>
        <asp:SqlDataSource ID="DSDepartamentos" runat="server" ConnectionString="<%$ ConnectionStrings:MiConexion %>" SelectCommand="Departamentos_list" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
    </form>
</body>
</html>

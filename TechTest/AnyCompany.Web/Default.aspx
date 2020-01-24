<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AnyCompany.Web.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .dropdown {
            position: relative;
            display: inline-block;
        }

        .dropdown-content {
            display: none;
            position: absolute;
            background-color: #f9f9f9;
            min-width: 160px;
            box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
            padding: 12px 16px;
            z-index: 1;
        }

        .dropdown:hover .dropdown-content {
            display: block;
        }

        fieldset {
            border: 1px solid #ddd !important;
            margin: 0;
            xmin-width: 0;
            padding: 10px;
            position: relative;
            border-radius: 4px;
            background-color: #f5f5f5;
            padding-left: 10px !important;
        }

        legend {
            font-size: 14px;
            font-weight: bold;
            margin-bottom: 0px;
            width: 35%;
            border: 1px solid #ddd;
            border-radius: 4px;
            padding: 5px 5px 5px 10px;
            background-color: #ffffff;
        }
        .auto-style1 {
            width: 8px;
        }
    </style>

    <link href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/css/bootstrap.min.css" rel="stylesheet" />
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/js/bootstrap.min.js"></script>
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <div class="container">
            <div class="panel panel-default">
                <div class="panel-heading">Place an order</div>

                <div class="panel-body">
                    <table border="0" width="50%" cellpadding="2" cellspacing="1" class="table" aria-live="off">
                        <tr align="LEFT">
                            <td><strong>Customer</strong></td>
                            <td class="auto-style1">:</td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlcustomer" DataSourceID="SqlDataSource2" class="dropdown form-control" DataTextField="Name" DataValueField="CustomerId" Width="150px" Height="30">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqFavoriteColor" Text="(Required)" InitialValue="0" ControlToValidate="ddlcustomer" runat="server" ForeColor="Red" ValidationGroup="order" />
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:AnyCompanyConnectionString %>" SelectCommand="select '--Select--' as [Name], 0 as [CustomerId] union SELECT [Name], [CustomerId] FROM [Customer]"></asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr align="LEFT">
                            <td><strong>Order Id</strong></td>
                            <td class="auto-style1">:</td>
                            <td>
                                <asp:Label runat="server" Style="vertical-align:middle; font-weight: 700;" ID="lblorderid"></asp:Label>
                            </td>
                        </tr>
                        <tr align="LEFT">
                            <td><strong>Order Amount</strong></td>
                            <td class="auto-style1">:</td>
                            <td>
                                <asp:TextBox ID="txtorderamt" runat="server" CssClass="form-control" Width="80px"></asp:TextBox>
                                <div align="LEFT">
                                    <asp:RequiredFieldValidator ID="RFVtxtorderamt" Text="(Required)" runat="server" ControlToValidate="txtorderamt" ErrorMessage="Order Amount Required..!!!" ForeColor="Red" ValidationGroup="order"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtorderamt" ErrorMessage="Invalid Order amount" ForeColor="Red" ValidationExpression="([0-9])[0-9]*[.]?[0-9]*" ValidationGroup="order"></asp:RegularExpressionValidator>
                                </div>
                            </td>
                        </tr>
                        <tr align="LEFT">
                            <td>&nbsp;</td>
                            <td class="auto-style1"></td>
                            <td>
                                <asp:Button runat="server" ID="btnsubmit" Text="Submit" OnClick="btnsubmit_Click" class="btn btn-primary" ValidationGroup="order" />
                                &nbsp;&nbsp;<asp:Button runat="server" ID="btnReset" Text="Reset" class="btn btn-primary" OnClick="btnReset_Click" />
                            </td>
                        </tr>
                    </table>
                </div>


                <div class="panel-heading">Customer Order List</div>
                <div class="panel-body">

                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="OrderId" DataSourceID="SqlDataSource1" ForeColor="#333333" AllowPaging="True" AllowSorting="True" CaptionAlign="Left" CellSpacing="2" GridLines="None" CssClass="table">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <%--Text='<%# Container.DataItemIndex + 1 %>'--%>
                            <%--<asp:BoundField DataField='<%# Container.DataItemIndex + 1 %>' HeaderText="Sr.No" ReadOnly="True" HeaderStyle-HorizontalAlign="Left"/>--%>
                            <asp:TemplateField HeaderText="Sr.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="OrderId" HeaderText="OrderId" ReadOnly="True" SortExpression="OrderId" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Customer Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="VAT" HeaderText="VAT" SortExpression="VAT" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>

                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:AnyCompanyConnectionString %>" SelectCommand="SELECT Orders.OrderId, ROUND(Orders.Amount,2) as Amount, ROUND(Orders.VAT,2) as VAT, Orders.CustomerId, Customer.Name FROM Orders INNER JOIN Customer ON Orders.CustomerId = Customer.CustomerId order by Orders.OrderId DESC"></asp:SqlDataSource>

                </div>


            </div>
        </div>
    </form>
</body>
</html>

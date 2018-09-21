<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Users.aspx.cs" Inherits="Admin_Users" Theme="SkinFile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td rowspan="2" valign="top">
                <asp:GridView ID="GridView1" runat="server" SkinID="igoogle-summer_paging" AutoGenerateColumns="False"
                    DataKeyNames="USER_ID" DataSourceID="AllUsersDS" EmptyDataText="No Users Found" Width="850px">
                    <Columns>
                        <asp:TemplateField HeaderText="ID" SortExpression="USER_ID">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("USER_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" Text='<%# Eval("USER_ID") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FULL_NAME" HeaderText="Name" SortExpression="FULL_NAME" />
                        <asp:BoundField DataField="PHONE" HeaderText="Phone" SortExpression="PHONE" />
                        <asp:BoundField DataField="EMAIL" HeaderText="eMail" SortExpression="EMAIL" />
                        <asp:BoundField DataField="ROLE_NME" HeaderText="Role" SortExpression="ROLE_NME"
                            HtmlEncode="False" />
                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="Update"></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="Cancel"></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                    Text="Delete" OnClientClick='return confirm("Are you sure you want to delete this user?");'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div style="padding-top:10px">
                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New User" />
                        &nbsp;&nbsp;&nbsp;<asp:TextBox ID="searchTextBox" runat="server"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
                        <asp:Button ID="clearButton" runat="server" OnClick="clearButton_Click" Text="Clear" />
                    </asp:Panel>
                </div>
            </td>
            <td rowspan="2" valign="top">
                &nbsp;
            </td>
        </tr>
    </table>
    <cc1:ModalPopupExtender ID="mpeUser" BehaviorID="mdlPopup" runat="server" TargetControlID="btnAdd"
        PopupControlID="pnlPopup" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="pnlPopup" runat="server" CssClass="confirm-dialog" Style="display: none;">
        <div class="inner">
            <h2>
                 Add / Edit User</h2>
            <div class="base">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="LBLerror" runat="server" Text="" Font-Bold="true" Font-Size="Small"
                                ForeColor="Red" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <asp:Panel ID="PanelEdit" runat="server" Visible="false">
                                    <tr>
                                        <td align="right">
                                            ID
                                        </td>
                                        <td>
                                            <asp:Label ID="LBLuserId" runat="server" />
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="PanelAdd" runat="server">
                                    <tr>
                                        <td align="right">
                                            ID
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TXTuserId" runat="server" MaxLength="10" AutoPostBack="true" Width="125px" OnTextChanged="TXTuserId_TextChanged" />
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <tr>
                                    <td align="right">
                                        First Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TXTfName" runat="server" MaxLength="30" Width="200px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Last Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TXTlName" runat="server" MaxLength="30" Width="200px" autocomplete="off" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Phone Number
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TXTphone" runat="server" MaxLength="30" Width="125px" />
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TXTphone"
                                            MaskType="Number" Mask="(999) 999-9999" InputDirection="LeftToRight" ClearMaskOnLostFocus="false">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        E-Mail
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TXTemail" runat="server" MaxLength="50" Width="225px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Active
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlActive" runat="server">
                                            <asp:ListItem Value="T" Selected="True">T</asp:ListItem>
                                            <asp:ListItem Value="F">F</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        Roles
                                    </td>
                                    <td>
                                        <asp:CheckBoxList ID="CBLroles" runat="server" DataSourceID="roleDS" DataTextField="ROLE_NME"
                                            DataValueField="ROLE_ID">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="BTNsubmit" runat="server" Text="Submit" OnClick="BTNsubmit_Click" Width="80px" />
                                        &nbsp;<asp:Button ID="BTNcancel" runat="server" Text="Cancel" OnClick="BTNcancel_Click" Width="80px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="btnHiddenUser" runat="Server" Style="display: none" />
    <br />
    <asp:HiddenField ID="searchHF" runat="server" Value="%" />
    <asp:HiddenField ID="userIdHF" runat="server" />
    <asp:SqlDataSource ID="AllUsersDS" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        DeleteCommand="BEGIN DELETE FROM USERS_ROLE WHERE USER_ID = :USER_ID; DELETE FROM USERS WHERE USER_ID = :USER_ID; END;"
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT  t.USER_ID, LNAME || ', ' || FNAME AS FULL_NAME, PHONE, EMAIL, substr(MAX(SUBSTR(sys_connect_by_path(ROLE_NME,'<br>'),1,1000)), 5) AS ROLE_NME FROM (SELECT DISTINCT u.USER_ID, FNAME, LNAME, phone, email, row_number() over (PARTITION BY u.USER_ID ORDER BY u.USER_ID, ROLE_NME) rn, ROLE_NME FROM USERS_ROLE UR, ROLES r, users u WHERE ur.ROLE_ID = r.ROLE_ID and ur.active = 'T' and ur.user_id = u.user_id AND( UPPER(LNAME || ', ' || FNAME) LIKE '%' || UPPER(:search) || '%' or UPPER(u.USER_ID) LIKE '%' || UPPER(:search) || '%' )) t START WITH t.rn = 1 CONNECT BY t.rn = PRIOR t.rn + 1 AND USER_ID = PRIOR USER_ID GROUP BY USER_ID, LNAME, FNAME, phone, email ORDER BY FULL_NAME">
        <DeleteParameters>
            <asp:Parameter Name="USER_ID" Type="String" />
        </DeleteParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="searchHF" Name="search" PropertyName="Value" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="roleDS" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT ROLE_ID, ROLE_NME || DECODE(ROLE_DESC, null, null,  ' - ' || ROLE_DESC) AS ROLE_NME FROM ROLES ORDER BY ROLE_NME">
    </asp:SqlDataSource>
</asp:Content>

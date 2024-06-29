<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridViewPaging.ascx.cs" Inherits="SGH_CCES.GridViewPaging" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<div id="divPage" runat="server" style="font-size: 8pt; font-family: Verdana;">
    <asp:Button ID="btnFirst" runat="server" Text="<<" OnClick="btnFirst_Click" />
    <asp:Button ID="btnPrevious" runat="server" Text="<" OnClick="btnPrevious_Click" />&nbsp;
    <asp:TextBox ID="txtPage" runat="server" Text="1" Width="50px" MaxLength="10" onkeypress="return txtPage_KeyPress(event, this);" onblur="txtPage_Blur(this);"></asp:TextBox>
    <ajax:FilteredTextBoxExtender ID="ftePage" runat="server" TargetControlID="txtPage" FilterType="Numbers" />
    <span>&nbsp;of&nbsp;</span>
    <asp:Label ID="lblTotal" runat="server" Text="1"></asp:Label>&nbsp;
    <asp:Button ID="btnNext" runat="server" Text=">" OnClick="btnNext_Click" />
    <asp:Button ID="btnLast" runat="server" Text=">>" OnClick="btnLast_Click" />
    
    <asp:HiddenField ID="hfPage" runat="server" Value="1" />
    <asp:LinkButton ID="lbtnPage" runat="server" OnClick="lbtnPage_Click"></asp:LinkButton>

</div>

<script type="text/javascript">
    function txtPage_KeyPress(event, txt)
    {
        var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
        if (keyCode == 13)
        {
            txtPage_Blur(txt);
            return false;
        }

        return true;
    }

    function txtPage_Blur(txt) {
        if (isValid(txt)) {
            document.getElementById("<%=lbtnPage.ClientID%>").click();
        }
    }

    function isValid(txt) {
        try {
            var hf = document.getElementById("<%=hfPage.ClientID%>");
            if (txt.value.length > 0 && isInt(txt.value)) {
                if (parseInt(txt.value) == 0) {
                    txt.value = hf.value = "1";
                    return true;
                }
                else {
                    var tot = document.getElementById("<%=lblTotal.ClientID%>").innerText;
                    if (parseInt(txt.value) > parseInt(tot)) {
                        txt.value = hf.value = tot;
                        return true;
                    }
                    else if (hf.value != txt.value) {
                        if (parseInt(txt.value) == parseInt(hf.value)) {
                            txt.value = hf.value;
                        }
                        else {
                            hf.value = txt.value;
                            return true;
                        }
                    }
                }
            }
            else {
                txt.value = hf.value;
            }
        }
        catch (e) {

        }
        return false;
    }

    function isInt(value) {
        return !isNaN(value) && (function (x) { return (x | 0) === x; })(parseInt(value));
    }
</script>
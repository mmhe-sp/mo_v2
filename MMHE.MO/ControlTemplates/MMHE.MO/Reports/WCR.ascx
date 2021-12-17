<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WCR.ascx.cs" Inherits="MMHE.MO.Controls.Reports.WCR" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<div class="row">
    <div class="col-12">
        <a href="wcr-list.aspx" class="btn btn-primary me-1" onclick="goBack()"><i class="mdi mdi-keyboard-backspace me-1"></i>Go Back</a>
        <asp:Button runat="server" ID="btnComplete" Disabled="true" class="btn btn-primary me-1" OnClick="btnComplete_Click" OnClientClick="if (!confirm('Are you sure you want proceed?')) return false;" Text="Complete"></asp:Button>
    </div>
</div>

<div class="row">
    <div class="col-12">
        <div class="card">
            <div class="card-body">
                <rsweb:reportviewer id="WDRSReport" pagecountmode="Actual" asyncrendering="false" showrefreshbutton="false" runat="server" showexportcontrols="true"
                    showfindcontrols="false" showzoomcontrol="false" width="100%" height="100%" enabletheming="true" showprintbutton="true"
                    style="border: 1px solid #ccc" borderstyle="Solid" borderwidth="1px" sizetoreportcontent="true">
                    </rsweb:reportviewer>
            </div>
        </div>
    </div>
</div>

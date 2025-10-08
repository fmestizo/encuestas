<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EncuestasWeb.Default" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <title>Encuestas disponibles</title>
    <link href="Content/Site.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page">
            <h1>Encuestas disponibles</h1>
            <asp:Literal runat="server" ID="MensajeLiteral" />
            <asp:Repeater runat="server" ID="EncuestasRepeater">
                <ItemTemplate>
                    <div class="card">
                        <h2><%# Eval("EncuestaNombre") %></h2>
                        <asp:HyperLink runat="server" NavigateUrl='<%# "Preguntas.aspx?id=" + Eval("EncuestaId") %>' CssClass="button" Text="Responder" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>

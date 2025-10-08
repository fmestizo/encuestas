<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Respuestas.aspx.cs" Inherits="EncuestasWeb.Respuestas" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <title>Resumen de respuestas</title>
    <link href="Content/Site.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page">
            <asp:HyperLink runat="server" ID="InicioLink" NavigateUrl="~/Default.aspx" Text="← Volver al inicio" CssClass="back-link" />
            <h1><asp:Literal runat="server" ID="TituloLiteral" /></h1>
            <asp:Literal runat="server" ID="MetaLiteral" />
            <asp:Repeater runat="server" ID="RespuestasRepeater">
                <ItemTemplate>
                    <div class="answer">
                        <h2><%# Eval("Pregunta") %></h2>
                        <p><%# Eval("Respuesta") %></p>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Literal runat="server" ID="MensajeLiteral" />
            <asp:Button runat="server" ID="ImprimirButton" Text="Imprimir" CssClass="button" OnClientClick="window.print(); return false;" />
        </div>
    </form>
</body>
</html>

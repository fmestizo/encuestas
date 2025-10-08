<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Preguntas.aspx.cs" Inherits="EncuestasWeb.Preguntas" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <title>Responder encuesta</title>
    <link href="Content/Site.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page">
            <asp:HyperLink runat="server" ID="VolverLink" NavigateUrl="~/Default.aspx" Text="← Volver" CssClass="back-link" />
            <h1><asp:Literal runat="server" ID="TituloLiteral" /></h1>
            <asp:Literal runat="server" ID="DescripcionLiteral" />
            <asp:Panel runat="server" ID="PreguntasPanel" CssClass="questions"></asp:Panel>
            <asp:Panel runat="server" ID="ErroresPanel" CssClass="validation" Visible="false">
                <asp:Literal runat="server" ID="ErroresLiteral" EnableViewState="false" />
            </asp:Panel>
            <asp:Button runat="server" ID="FinalizarButton" Text="Finalizar" CssClass="button primary" OnClick="FinalizarButton_Click" />
        </div>
    </form>
</body>
</html>

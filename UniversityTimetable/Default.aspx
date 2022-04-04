<%@ Page Title="Timetable" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UniversalTimetable._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    
        <div class="jumbotron">
            <p style="text-align: center"><asp:Image ID="Image1" style="align-content:center" runat="server"/>
                    <img alt="" src="Content/Images/timeschedule.png" style="width: 235px; height: 240px" /></p>
            <h1 class="text-center">Timetable</h1>
            <p class="lead">
                National Technical University of Ukraine "Kyiv Polytechnic Institute"
            </p>
            <p>
                <asp:TextBox ID="TextBoxGroupName" runat="server" Width="195px"></asp:TextBox>
            </p>
            <p>
                <asp:Button ID="Button1" runat="server" class="btn btn-primary btn-lg" OnClick="Button1_Click" Text="Show timetable" />
            </p>
        </div>
  

</asp:Content>

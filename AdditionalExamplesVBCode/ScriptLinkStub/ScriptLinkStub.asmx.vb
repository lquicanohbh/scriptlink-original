Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Net.Mail

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class Service1
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function GetVersion() As String
        Return "Version 1.0"
    End Function

    <WebMethod()> _
    Public Function RunScript(ByVal optionObject As OptionObject, ByVal scriptName As String) As OptionObject

        Dim returnOptionObject As OptionObject
        returnOptionObject = New OptionObject

        If (scriptName.Equals("YourScriptNameHere")) Then

        End If

        Return returnOptionObject

    End Function

    
End Class
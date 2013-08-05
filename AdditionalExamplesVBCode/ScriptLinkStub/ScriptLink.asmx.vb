Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Net.Mail
Imports System.Data.Odbc

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class ScriptLink
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function GetVersion() As String
        Return "Version 1.0"
    End Function

    <WebMethod()> _
    Public Function RunScript(ByVal optionObject As OptionObject, ByVal scriptName As String) As OptionObject

        Dim returnOptionObject As OptionObject
        returnOptionObject = New OptionObject

        If scriptName.Equals("SuicideScore") Then
      
            Dim score As Integer

            score = 0

            For i = 0 To (optionObject.Forms.Length - 1)
                For j = 0 To (optionObject.Forms(i).CurrentRow.Fields.Length - 1)
                    Try

                        Select Case (optionObject.Forms(i).CurrentRow.Fields(j).FieldNumber)
                            Case "126.97"
                                score = score + Integer.Parse(optionObject.Forms(i).CurrentRow.Fields(j).FieldValue)
                            Case "126.99"
                                score = score + Integer.Parse(optionObject.Forms(i).CurrentRow.Fields(j).FieldValue)
                            Case "127.01"
                                score = score + Integer.Parse(optionObject.Forms(i).CurrentRow.Fields(j).FieldValue)
                            Case "127.03"
                                score = score + Integer.Parse(optionObject.Forms(i).CurrentRow.Fields(j).FieldValue)
                            Case "127.05"
                                score = score + Integer.Parse(optionObject.Forms(i).CurrentRow.Fields(j).FieldValue)
                            Case "127.07"
                                score = score + Integer.Parse(optionObject.Forms(i).CurrentRow.Fields(j).FieldValue)
                            Case "127.09"
                                score = score + Integer.Parse(optionObject.Forms(i).CurrentRow.Fields(j).FieldValue)
                            Case "127.11"
                                score = score + Integer.Parse(optionObject.Forms(i).CurrentRow.Fields(j).FieldValue)
                            Case "127.13"
                                score = score + Integer.Parse(optionObject.Forms(i).CurrentRow.Fields(j).FieldValue)
                            Case "127.15"
                                score = score + Integer.Parse(optionObject.Forms(i).CurrentRow.Fields(j).FieldValue)
                            Case "127.17"
                                score = score + Integer.Parse(optionObject.Forms(i).CurrentRow.Fields(j).FieldValue)
                            Case "127.19"
                                score = score + Integer.Parse(optionObject.Forms(i).CurrentRow.Fields(j).FieldValue)
                            Case "127.25"
                                score = score + Integer.Parse(optionObject.Forms(i).CurrentRow.Fields(j).FieldValue)
                            Case Else
                        End Select
                    Catch ex As Exception

                    End Try

                    If (optionObject.Forms(i).CurrentRow.Fields(j).FieldNumber.Equals("127.21")) Then
                        If (optionObject.Forms(i).CurrentRow.Fields(j).FieldValue.Equals("1")) Then
                            score = score + 1
                        End If
                    End If

                Next

            Next

            Dim returnMessage = "Client is at low to moderate risk of suicide, supportive services should be arranged as appropriate."

            If (score >= 9) Then
                returnMessage = "Results indicate that client is at HIGH RISK of attempting or committing suicide. Immediate inpatient psychiatric services are strongly recommended."
                ' send text message
                Dim sendTo As String
                Dim subject As String
                Dim message As String
                Dim retMessage As String

                sendTo = "who@yourdomain.com"
                subject = "Suicide Alert"
                message = "Results indicate that a client in your caseload is at HIGH RISK of attempting or committing suicide. Immediate inpatient psychiatric services are strongly recommended. Please call 555-1212."
                ' send message

                'retMessage = SendMail(sendTo, subject, message)

                'returnOptionObject.ErrorMesg = returnMessage & ControlChars.CrLf & ControlChars.CrLf & retMessage

            End If

            Dim fields(0) As FieldObject
            Dim forms(0) As FormObject

            Dim currentRow As RowObject
            currentRow = New RowObject()

            Dim formObject = New FormObject()
            Dim field = New FieldObject()

            field.FieldNumber = "128.56"
            field.FieldValue = score
            field.Enabled = "0"
            fields(0) = field

            currentRow.Fields = fields
            currentRow.RowId = optionObject.Forms(0).CurrentRow.RowId
            currentRow.ParentRowId = optionObject.Forms(0).CurrentRow.ParentRowId
            currentRow.RowAction = "EDIT"

            formObject.FormId = "927"
            formObject.CurrentRow = currentRow
            forms(0) = formObject

            returnOptionObject.ErrorCode = 3
            returnOptionObject.ErrorMesg = returnMessage
            returnOptionObject.EntityID = optionObject.EntityID
            returnOptionObject.OptionId = optionObject.OptionId
            returnOptionObject.SystemCode = optionObject.SystemCode
            returnOptionObject.Facility = optionObject.Facility
            returnOptionObject.Forms = forms


        ElseIf scriptName.Equals("ViewOptionObject") Then

            Dim optionHTMLPage As HTMLPage = New HTMLPage()
            Dim fileName As String = optionHTMLPage.CreateHTMLPage(optionObject)

            returnOptionObject.EntityID = optionObject.EntityID
            returnOptionObject.OptionId = optionObject.OptionId
            returnOptionObject.Facility = optionObject.Facility
            returnOptionObject.SystemCode = optionObject.SystemCode

            returnOptionObject.ErrorCode = 5

            returnOptionObject.ErrorMesg = "http://" & Context.Request.Url.Host & System.Web.VirtualPathUtility.GetDirectory(Context.Request.RawUrl) & fileName


        End If

        Return returnOptionObject

    End Function

    Function SendMail(ByVal sendTo As String, ByVal subject As String, ByVal body As String)

        Try

            Dim message As MailMessage = New MailMessage()

            message.From = New MailAddress("who@yourdomain.com")

            message.To.Add(New MailAddress(sendTo))

            message.Subject = subject

            message.Body = body

            message.IsBodyHtml = True

            Dim client As SmtpClient = New SmtpClient()
            client.Host = "smtp"
            client.Port = 587
            client.UseDefaultCredentials = False
            client.DeliveryMethod = SmtpDeliveryMethod.Network

            client.Credentials = New System.Net.NetworkCredential("username", "password")
            client.Send(message)

        Catch ex As Exception
            ' received an error sending mail, can send back error to Avatar if 
            ' you wish stop running scripts, or let users know an error occurred.
            Return ex.Message
        End Try


        Return "Email message was sent to the crisis team."

    End Function
    
End Class
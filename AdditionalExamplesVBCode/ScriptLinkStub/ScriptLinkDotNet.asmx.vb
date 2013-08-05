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
Public Class ScriptLinkDotNet
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function GetVersion() As String
        Return "Version 1.0"
    End Function

    <WebMethod()> _
    Public Function RunScript(ByVal optionObject As OptionObject, ByVal scriptName As String) As OptionObject

        Dim returnOptionObject As OptionObject
        returnOptionObject = New OptionObject

        If scriptName.Equals("AdmissionFormPostFile") Then

            Dim patientId As String
            Dim programCode As String
            Dim sendTo As String
            Dim programValue As String
            Dim arsonRisk As Boolean

            arsonRisk = False
            programValue = ""
            sendTo = ""
            patientId = optionObject.EntityID
            returnOptionObject.EntityID = optionObject.EntityID
            returnOptionObject.OptionId = optionObject.OptionId
            returnOptionObject.SystemCode = optionObject.SystemCode
            returnOptionObject.Facility = optionObject.Facility

            For i = 0 To (optionObject.Forms.Length - 1)
                If (optionObject.Forms(i).FormId.Equals("510")) Then
                    'Look for program (field 5) in current row
                    For j = 0 To (optionObject.Forms(i).CurrentRow.Fields.Length - 1)
                        If (optionObject.Forms(i).CurrentRow.Fields(j).FieldNumber.Equals("5")) Then
                            programCode = optionObject.Forms(i).CurrentRow.Fields(j).FieldValue
                            programValue = GetDictionaryValue(optionObject.Facility, "5", programCode)
                        End If

                        'Look for type of alert (field 508)
                        If (optionObject.Forms(i).CurrentRow.Fields(j).FieldNumber.Equals("508")) Then
                            'check to see if client is an arson risk (code = 30)
                            If (optionObject.Forms(i).CurrentRow.Fields(j).FieldValue.Equals("30")) Then
                                arsonRisk = True
                            End If
                        End If
                    Next
                End If
            Next

            'if client is an arson risk send an email
            If arsonRisk Then
                sendTo = programValue
                sendTo = sendTo.Replace(" ", String.Empty)
                sendTo = sendTo & "@gmail.com"
                SendMail(sendTo, "ARSON RISK MR# " & patientId, "An arson risk client assigned with MR# " & patientId & " was admitted to " & programValue & " Program on " & Today() & ".")
            End If

        End If

        If scriptName.Equals("PreScreenAssessment") Then

            Dim forms(1) As FormObject
            Dim field As FieldObject

            returnOptionObject.EntityID = optionObject.EntityID
            returnOptionObject.OptionId = optionObject.OptionId
            returnOptionObject.SystemCode = optionObject.SystemCode
            returnOptionObject.Facility = optionObject.Facility

            ' Default Current Medications
            For i = 0 To (optionObject.Forms.Length - 1)
                If (optionObject.Forms(i).FormId.Equals("51")) Then
                    Dim fields(0) As FieldObject

                    Dim currentRow As RowObject
                    currentRow = New RowObject()

                    ' default current medications
                    Dim formObject = New FormObject()
                    formObject.FormId = "51"

                    field = New FieldObject()
                    field.FieldNumber = "13.85"
                    field.FieldValue = GetCurrentMedications(optionObject.Facility, optionObject.EntityID)
                    field.Lock = "1"
                    fields(0) = field
                    forms(0) = formObject

                    currentRow.Fields = fields
                    currentRow.RowId = optionObject.Forms(i).CurrentRow.RowId

                    formObject.CurrentRow = currentRow


                End If

                ' Default Demographics
                If (optionObject.Forms(i).FormId.Equals("50")) Then


                    Dim formObject As FormObject

                    formObject = GetDemographics(optionObject.Facility, optionObject.EntityID)
                    formObject.FormId = "50"

                    formObject.CurrentRow.RowId = optionObject.Forms(3).CurrentRow.RowId
                    forms(1) = formObject

                End If

            Next

            returnOptionObject.Forms = forms

        End If

        If scriptName.Equals("FileAssessment") Then
            ' File Demographics via Web Service
            ' Create a Charge via Web Service

            returnOptionObject.EntityID = optionObject.EntityID
            returnOptionObject.OptionId = optionObject.OptionId
            returnOptionObject.SystemCode = optionObject.SystemCode
            returnOptionObject.Facility = optionObject.Facility

            Dim message1 As String
            Dim message2 As String
            message1 = SaveDemographics(optionObject)
            message2 = CreateService(optionObject)
            'returnOptionObject.ErrorMesg = message1
            'returnOptionObject.ErrorCode = 3

        End If

        Return returnOptionObject

    End Function

    Sub SendMail(ByVal sendTo As String, ByVal subject As String, ByVal body As String)

        Try

            Dim message As MailMessage = New MailMessage()

            message.From = New MailAddress("fromemailaddress@yourdomain.com")

            message.To.Add(New MailAddress(sendTo))

            message.Subject = subject

            message.Body = body

            message.IsBodyHtml = True

            Dim client As SmtpClient = New SmtpClient()
            client.Host = "yourdomain.com"
            client.Port = 587
            client.UseDefaultCredentials = False
            client.DeliveryMethod = SmtpDeliveryMethod.Network

            client.Credentials = New System.Net.NetworkCredential("username", "password")
            client.Send(message)

        Catch ex As Exception
            ' recieved an error sending mail, can send back error to Avatar if 
            ' you wish stop running scripts, or let users know an error occurred.
        End Try




    End Sub
    Private Function GetDictionaryValue(ByVal Facility As String, ByVal FieldNumber As String, ByVal DictionaryCode As String)

        Dim conn As OdbcConnection
        Dim comm As OdbcCommand
        Dim dr As OdbcDataReader
        Dim connectionString As String
        Dim sql As String
        Dim dictionaryValue As String

        dictionaryValue = Facility & "^" & FieldNumber & "^" & DictionaryCode
        Try

            connectionString = "DSN=SCRIPTLINKPM;UID=SAMPLE:PMSYSADM;Pwd=SYSADM99;"
            sql = "SELECT dictionary_value FROM dictionaries_patient where facility='" & Facility & "' and " & "field_number='" & FieldNumber & "' and dictionary_code = '" & DictionaryCode & "'"
            conn = New OdbcConnection(connectionString)
            conn.Open()
            comm = New OdbcCommand(sql, conn)
            dr = comm.ExecuteReader()
            While (dr.Read())
                dictionaryValue = dr("dictionary_value")
            End While

            conn.Close()
            dr.Close()
            comm.Dispose()
            conn.Dispose()

        Catch ex As Exception
            dictionaryValue = ex.Message
        End Try

        Return dictionaryValue

    End Function
    Private Function GetDemographics(ByVal Facility As String, ByVal PATID As String)

        Dim conn As OdbcConnection
        Dim comm As OdbcCommand
        Dim dr As OdbcDataReader
        Dim connectionString As String
        Dim sql As String
        Dim field As FieldObject
        Dim fields(6) As FieldObject

        Dim currentRow As RowObject
        currentRow = New RowObject()

        Dim formObject As FormObject
        formObject = New FormObject()

        connectionString = "DSN=SCRIPTLINKPM;UID=SAMPLE:PMSYSADM;Pwd=SYSADM99;"
        sql = "SELECT * FROM Patient_Current_Demographics where PATID='" & PATID & "' and facility='" & Facility & "'"
        conn = New OdbcConnection(connectionString)
        conn.Open()
        comm = New OdbcCommand(sql, conn)
        dr = comm.ExecuteReader()
        While (dr.Read())
            'need to check for DBNULL, if DBNULL then set the field value to an empty string
            field = New FieldObject()
            field.FieldNumber = "13.78"
            If (Not (IsDBNull(dr("patient_add_street_1")))) Then
                field.FieldValue = dr("patient_add_street_1")
            End If
            fields(0) = field

            field = New FieldObject()
            field.FieldNumber = "13.79"
            If (Not (IsDBNull(dr("patient_add_street_2")))) Then
                field.FieldValue = dr("patient_add_street_2")
            End If
            fields(1) = field

            field = New FieldObject()
            field.FieldNumber = "13.8"
            If (Not (IsDBNull(dr("patient_add_zipcode")))) Then
                field.FieldValue = dr("patient_add_zipcode")
            End If
            fields(2) = field

            field = New FieldObject()
            field.FieldNumber = "13.81"
            If (Not (IsDBNull(dr("patient_add_city")))) Then
                field.FieldValue = dr("patient_add_city")
            End If
            fields(3) = field

            field = New FieldObject()
            field.FieldNumber = "13.82"
            If (Not (IsDBNull(dr("patient_add_county_code")))) Then
                field.FieldValue = dr("patient_add_county_code")
            End If
            fields(4) = field

            field = New FieldObject()
            field.FieldNumber = "13.83"
            If (Not (IsDBNull(dr("patient_add_state_code")))) Then
                field.FieldValue = dr("patient_add_state_code")
            End If
            fields(5) = field

            field = New FieldObject()
            field.FieldNumber = "13.84"
            If (Not (IsDBNull(dr("patient_home_phone")))) Then
                field.FieldValue = dr("patient_home_phone")
            End If
            fields(6) = field

            currentRow.Fields = fields
            formObject.CurrentRow = currentRow

        End While

        conn.Close()
        dr.Close()
        comm.Dispose()
        conn.Dispose()

        Return formObject

    End Function

    Private Function SaveDemographics(ByVal optionObject As OptionObject)
        Dim webService As ClientDemWS.ClientDemographicsSoapClient
        Dim clientDemographics As ClientDemWS.ClientDemographicsObject
        Dim webServiceResponse As ClientDemWS.WebServiceResponse
        Dim i As Integer
        Dim j As Integer
        Dim message As String

        message = ""

        clientDemographics = New ClientDemWS.ClientDemographicsObject
        For i = 0 To (optionObject.Forms.Length - 1)
            If (optionObject.Forms(i).FormId.Equals("50")) Then

                For j = 0 To (optionObject.Forms(i).CurrentRow.Fields.Length - 1)
                    If (optionObject.Forms(i).CurrentRow.Fields(j).FieldNumber.Equals("13.78")) Then
                        clientDemographics.ClientAddressStreet = optionObject.Forms(i).CurrentRow.Fields(j).FieldValue
                    End If

                    If (optionObject.Forms(i).CurrentRow.Fields(j).FieldNumber.Equals("13.79")) Then
                        clientDemographics.ClientAddressStreet2 = optionObject.Forms(i).CurrentRow.Fields(j).FieldValue
                    End If

                    If (optionObject.Forms(i).CurrentRow.Fields(j).FieldNumber.Equals("13.8")) Then
                        clientDemographics.ClientAddressZipcode = optionObject.Forms(i).CurrentRow.Fields(j).FieldValue
                    End If

                    If (optionObject.Forms(i).CurrentRow.Fields(j).FieldNumber.Equals("13.81")) Then
                        clientDemographics.ClientAddressCity = optionObject.Forms(i).CurrentRow.Fields(j).FieldValue
                    End If

                    If (optionObject.Forms(i).CurrentRow.Fields(j).FieldNumber.Equals("13.82")) Then
                        clientDemographics.ClientAddressCounty = optionObject.Forms(i).CurrentRow.Fields(j).FieldValue
                    End If

                    If (optionObject.Forms(i).CurrentRow.Fields(j).FieldNumber.Equals("13.83")) Then
                        clientDemographics.ClientAddressState = optionObject.Forms(i).CurrentRow.Fields(j).FieldValue
                    End If

                    If (optionObject.Forms(i).CurrentRow.Fields(j).FieldNumber.Equals("13.84")) Then
                        clientDemographics.ClientHomePhone = optionObject.Forms(i).CurrentRow.Fields(j).FieldValue
                    End If

                Next
            End If
            webService = New ClientDemWS.ClientDemographicsSoapClient()
            webService.Endpoint.Address = New System.ServiceModel.EndpointAddress("http://cache2008:8972/csp/ScriptLinkPM/WEBSVC.ClientDemographics.cls")
            webServiceResponse = webService.UpdateClientDemographics(optionObject.SystemCode, "PMSYSADM", "SYSADM99", clientDemographics, optionObject.EntityID)
            message = webServiceResponse.Message
        Next

        Return message

    End Function

    Private Function CreateService(ByVal optionObject As OptionObject)
        Dim webService As ClientChargeWS.ClientChargeInputSoapClient
        Dim clientChargeInputObject As ClientChargeWS.ClientChargeInputObject
        Dim dateOfService As String
        Dim webServiceResponse As ClientChargeWS.WebServiceResponse
        dateOfService = ""

        For i = 0 To (optionObject.Forms.Length - 1)
            If (optionObject.Forms(i).FormId.Equals("27")) Then
                For j = 0 To (optionObject.Forms(i).CurrentRow.Fields.Length - 1)
                    If (optionObject.Forms(i).CurrentRow.Fields(j).FieldNumber.Equals("7.28")) Then
                        dateOfService = optionObject.Forms(i).CurrentRow.Fields(j).FieldValue
                    End If
                Next
            End If
        Next

        clientChargeInputObject = New ClientChargeWS.ClientChargeInputObject
        clientChargeInputObject.ClientID = optionObject.EntityID
        clientChargeInputObject.DateOfService = dateOfService

        clientChargeInputObject.Duration = 30
        clientChargeInputObject.ServiceCode = "1002"
        clientChargeInputObject.Practitioner = optionObject.OptionStaffId
        clientChargeInputObject.Program = GetProgramCodeForEpisode(optionObject)
        clientChargeInputObject.EpisodeNumber = optionObject.EpisodeNumber

        webService = New ClientChargeWS.ClientChargeInputSoapClient
        webService.Endpoint.Address = New System.ServiceModel.EndpointAddress("http://cache2008:8972/csp/scriptlinkpm/WEBSVC.ClientChargeInput.cls")
        webServiceResponse = webService.FileClientChargeInput(optionObject.SystemCode, "PMSYSADM", "SYSADM99", clientChargeInputObject)

        Return webServiceResponse.Message

    End Function

    Private Function GetCurrentMedications(ByVal Facility As String, ByVal PATID As String)
        Dim conn As OdbcConnection
        Dim comm As OdbcCommand
        Dim dr As OdbcDataReader
        Dim connectionString As String
        Dim sql As String
        Dim returnValue As String
        returnValue = ""
        Try
            connectionString = "DSN=SCRIPTLINKCWS;UID=SAMPLE:PMSYSADM;Pwd=SYSADM99"
            sql = "SELECT * FROM orderentry.history_client_order where PATID='" & PATID & "' and facility='" & Facility & "'" & " and order_type_category_code='P' and start_date<='" & Today() & "'"   ' & "' and (effective_stop_date is Null Or effective_stop_date>'" & Today() & "')"
            conn = New OdbcConnection(connectionString)
            conn.Open()
            comm = New OdbcCommand(sql, conn)
            dr = comm.ExecuteReader()

            While (dr.Read())

                returnValue = returnValue & dr("order_description") & " " & dr("dosage") & " " & dr("dosage_unit") & " " & dr("administration_route") & " " & dr("dosage_form") & vbCrLf

            End While

            conn.Close()
            dr.Close()
            comm.Dispose()
            conn.Dispose()
        Catch ex As Exception
            returnValue = ex.Message
        End Try

        Return returnValue
    End Function

    Private Function GetProgramCodeForEpisode(ByVal optionObject As OptionObject)

        Dim conn As OdbcConnection
        Dim comm As OdbcCommand
        Dim dr As OdbcDataReader
        Dim connectionString As String
        Dim sql As String
        Dim returnValue As String
        returnValue = ""
        Try
            connectionString = "DSN=SCRIPTLINKPM;UID=SAMPLE:PMSYSADM;Pwd=SYSADM99"
            sql = "SELECT program_code FROM episode_history where PATID='" & optionObject.EntityID & "' and facility='" & optionObject.Facility & "'" & " and episode_number='" & optionObject.EpisodeNumber & "'"
            conn = New OdbcConnection(connectionString)
            conn.Open()
            comm = New OdbcCommand(sql, conn)
            dr = comm.ExecuteReader()
            While (dr.Read())
                returnValue = dr("program_code")
            End While

            conn.Close()
            dr.Close()
            comm.Dispose()
            conn.Dispose()
        Catch ex As Exception

        End Try

        Return returnValue

    End Function

End Class
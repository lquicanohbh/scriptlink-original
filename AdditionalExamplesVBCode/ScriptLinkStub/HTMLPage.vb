Imports System.IO

Public Class HTMLPage
    Public Function CreateHTMLPage(ByVal OptionObj As OptionObject) As String
        Dim fileName As String = Path.GetRandomFileName() & ".html"
        Dim file As System.IO.StreamWriter = CreateFile(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings("Path")) & "\" & fileName)

        file.WriteLine(CreateHeader())
        file.WriteLine(CreateTable())

        file.WriteLine(CreateTableRow("SystemCode", OptionObj.SystemCode.ToString()))
        file.WriteLine(CreateTableRow("Facility", OptionObj.Facility))
        file.WriteLine(CreateTableRow("OptionUserID", OptionObj.OptionUserId))
        file.WriteLine(CreateTableRow("OptionStaffID", OptionObj.OptionStaffId))
        file.WriteLine(CreateTableRow("OptionID", OptionObj.OptionId))
        file.WriteLine(CreateTableRow("EntityID", OptionObj.EntityID))
        file.WriteLine(CreateTableRow("EpisodeNumber", OptionObj.EpisodeNumber))
        file.WriteLine("<tr><td class=grey colspan=2>Forms</td></tr>")

        Dim optionForm As FormObject
        Dim formtables As String = ""
        For Each optionForm In OptionObj.Forms
            formtables = formtables & CreateTable()
            formtables = formtables & CreateTableRow("FormID", optionForm.FormId)
            formtables = formtables & CreateTableRow("MultipleIteration", optionForm.MultipleIteration.ToString())
            formtables = formtables & "<tr><td class=grey colspan=2>CurrentRow</td></tr>"

            Dim formFields As String
            formFields = CreateTable()
            formFields = formFields & "<tr><td class=grey>FieldNumber</td><td class=grey>FieldValue</td><td class=grey>Enabled</td><td class=grey>Lock</td><td class=grey>Required</td></tr>"

            Dim optionField As FieldObject
            For Each optionField In optionForm.CurrentRow.Fields
                formFields = formFields & "<tr><td>" & optionField.FieldNumber & "&nbsp;</td><td>" & optionField.FieldValue & "&nbsp;</td><td>" & optionField.Enabled & "&nbsp;</td><td>" & optionField.Lock & "&nbsp;</td><td>" & optionField.Required & "&nbsp;</td></tr>"
            Next
            formFields = formFields & EndTable()

            formtables = formtables & CreateTableRow("", CreateTable() & _
                                          CreateTableRow("RowID", optionForm.CurrentRow.RowId) & _
                                          CreateTableRow("RowAction", optionForm.CurrentRow.RowAction) & _
                                          CreateTableRow("ParentRowID", optionForm.CurrentRow.ParentRowId) & _
                                          "<tr><td class=grey colspan=2 style=""width: 100%"">Fields</td></tr>" & _
                                          CreateTableRow("", formFields) & _
                                          EndTable())


            If Not (optionForm.OtherRows Is Nothing) Then
                formtables = formtables & "<tr><td class=grey colspan=2>OtherRows</td></tr>"

                Dim optionRow As RowObject
                For Each optionRow In optionForm.OtherRows

                    formFields = CreateTable()
                    formFields = formFields & "<tr><td class=grey>FieldNumber</td><td class=grey>FieldValue</td><td class=grey>Enabled</td><td class=grey>Lock</td><td class=grey>Required</td></tr>"

                    For Each optionField In optionRow.Fields
                        formFields = formFields & "<tr><td>" & optionField.FieldNumber & "&nbsp;</td><td>" & optionField.FieldValue & "&nbsp;</td><td>" & optionField.Enabled & "&nbsp;</td><td>" & optionField.Lock & "&nbsp;</td><td>" & optionField.Required & "&nbsp;</td></tr>"
                    Next
                    formFields = formFields & EndTable()

                    formtables = formtables & CreateTableRow("", CreateTable() & _
                                                  CreateTableRow("RowID", optionRow.RowId) & _
                                                  CreateTableRow("RowAction", optionRow.RowAction) & _
                                                  CreateTableRow("ParentRowID", optionRow.ParentRowId) & _
                                                  "<tr><td class=grey colspan=2 style=""width: 100%"">Fields</td></tr>" & _
                                                  CreateTableRow("", formFields) & _
                                                  EndTable())
                Next
            End If

            formtables = formtables & EndTable()

            file.WriteLine(CreateTableRow("", formtables))
            formtables = ""
        Next
        file.WriteLine(EndTable())
        file.Flush()
        file.Close()

        Return (ConfigurationManager.AppSettings("Path") & "/" & fileName)
    End Function

    Private Function CreateFile(ByVal fileName As String) As System.IO.StreamWriter
        Dim file As System.IO.StreamWriter = System.IO.File.CreateText(fileName)
        Return file
    End Function

    Private Function CreateHeader() As String
        Dim rtn As String
        rtn = "<HTML>" & vbCrLf
        rtn = rtn & "<head>" & vbCrLf
        rtn = rtn & "<title></title>" & vbCrLf
        rtn = rtn & "<style type=""text/css"">" & vbCrLf
        rtn = rtn & "   .style1()" & vbCrLf
        rtn = rtn & "   {" & vbCrLf
        rtn = rtn & "    width: 100%;" & vbCrLf
        rtn = rtn & "   }" & vbCrLf
        rtn = rtn & "   .style2()" & vbCrLf
        rtn = rtn & "   {" & vbCrLf
        rtn = rtn & "    width: 110px;" & vbCrLf
        rtn = rtn & "   }" & vbCrLf
        rtn = rtn & "   .style3()" & vbCrLf
        rtn = rtn & "   {" & vbCrLf
        rtn = rtn & "    width: 101px;" & vbCrLf
        rtn = rtn & "   }" & vbCrLf
        rtn = rtn & "   td {padding: 1pt 3pt 2pt 3pt; border-style: solid; border-width: 1} "
        rtn = rtn & "   table {border-style: solid ; border-width: thin; border-color: White; border-collapse: collapse ; font-size: 11pt; text-align: justify } "
        rtn = rtn & "   table.grey {border-color: Silver; }"
        rtn = rtn & "   td.grey {background-color: Gray; color: white; border-color: Silver; font-weight: bold; text-align: center } "
        rtn = rtn & "</style>" & vbCrLf
        rtn = rtn & "</head>" & vbCrLf
        rtn = rtn & "<Body>" & vbCrLf
        Return rtn
    End Function

    Private Function CreateFooter() As String
        Return "</Body></HTML>"
    End Function

    Private Function CreateTable() As String
        Return "<table class=style1>"
    End Function

    Private Function EndTable() As String
        Return "</table>"
    End Function

    Private Function CreateTableRow(ByVal Column1 As String, _
                             ByVal Column2 As String) As String
        Dim rtn As String
        rtn = "<tr>"
        If Column1.Length > 0 Then
            rtn = rtn & "<td class=grey>" & Column1 & "</td>"
        Else
            rtn = rtn & "<td class=style2>" & Column1 & "</td>"
        End If
        rtn = rtn & "<td>" & Column2 & "&nbsp;</td>"
        rtn = rtn & "</tr>"

        Return rtn
    End Function
End Class

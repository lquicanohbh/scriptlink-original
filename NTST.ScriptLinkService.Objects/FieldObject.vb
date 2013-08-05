Public Class FieldObject

    Public Enabled As String

    Public FieldNumber As String

    Public FieldValue As String

    Public Lock As String

    Public Required As String
    Public Sub New()
    End Sub
    Public Sub New(fieldNumber As String)
        Me.FieldNumber = fieldNumber
    End Sub
End Class


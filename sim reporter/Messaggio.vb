Public Class Messaggio

    Public Property t As String 'nome
    Public Property s As String 'tel
    Public Property d As ULong  'timestamp
    Public Property time As Date
    Public Property m As String 'testo mex
    Public Const ParameterSize As Integer = 4

    Sub New(Optional t = "", Optional s = "", Optional d = ULong.MinValue, Optional m = "")
        Me.t = t
        Me.s = s
        Me.d = d
        Me.m = m
        Me.time = New DateTime(1970, 1, 1, 0, 0, 0)
    End Sub
End Class

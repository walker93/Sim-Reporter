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

    Public Shared Operator =(message1 As Messaggio, message2 As Messaggio) As Boolean
        If message1.m <> message2.m Then Return False
        If message1.t <> message2.t Then Return False
        If message1.s <> message2.s Then Return False
        If message1.d <> message2.d Then Return False
        Return True

    End Operator

    Public Shared Operator <>(message1 As Messaggio, message2 As Messaggio) As Boolean
        If message1.m <> message2.m Then Return True
        If message1.t <> message2.t Then Return True
        If message1.s <> message2.s Then Return True
        If message1.d <> message2.d Then Return True
        Return False
    End Operator
End Class

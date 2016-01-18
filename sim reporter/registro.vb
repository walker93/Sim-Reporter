Public Class registro
    Public Property n As String  'name
    Public Property t As String  'tel
    Public Const ParameterSize As Integer = 2

    Sub New(Optional n = "", Optional t = "")
        Me.n = n
        Me.t = t
    End Sub

    Public Shared Operator =(reg1 As registro, reg2 As registro) As Boolean
        If reg1.n <> reg2.n Then Return False
        If reg1.t <> reg2.t Then Return False
        Return True

    End Operator

    Public Shared Operator <>(reg1 As registro, reg2 As registro) As Boolean
        If reg1.n <> reg2.n Then Return True
        If reg1.t <> reg2.t Then Return True
        Return False
    End Operator
End Class

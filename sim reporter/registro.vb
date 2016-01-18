Public Class registro
    Public Property n As String  'name
    Public Property t As String  'tel
    Public Const ParameterSize As Integer = 2

    Sub New(Optional n = "", Optional t = "")
        Me.n = n
        Me.t = t
    End Sub
End Class

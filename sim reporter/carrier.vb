Public Class carrier
    Public Property mcc As Integer
    Public Property mnc As Integer
    Public Property access As Integer
    Public Const ParameterSize As Integer = 3

    Sub New(Optional mcc = 0, Optional mnc = 0, Optional access = 0)
        Me.mcc = mcc
        Me.mnc = mnc
        Me.access = access
    End Sub
End Class

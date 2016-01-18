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

    Public Shared Operator =(op1 As carrier, op2 As carrier) As Boolean
        If op1.mcc <> op2.mcc Then Return False
        If op1.mnc <> op2.mnc Then Return False
        Return True

    End Operator

    Public Shared Operator <>(op1 As carrier, op2 As carrier) As Boolean
        If op1.mcc <> op2.mcc Then Return True
        If op1.mnc <> op2.mnc Then Return True
        Return False
    End Operator
End Class

Public Class Contatto
    Public Property n As String 'nome
    Public Property t As String 'tel
    Public Property a As String 'tel alternativo
    Public Property at As Integer  ' ?
    Public Property gr As String  'group index
    Public Property emln As Integer 'email index
    Public Property snn As Integer 'cognome index
    Public Const ParameterSize As Integer = 7


    Sub New(Optional n = "", Optional t = "", Optional a = "", Optional at = "", Optional gr = "", Optional emln = 0, Optional snn = 0)
        Me.n = n
        Me.t = t
        Me.a = a
        Me.at = at
        Me.gr = gr
        Me.emln = emln
        Me.snn = snn
    End Sub
End Class

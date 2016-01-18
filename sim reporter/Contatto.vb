Public Class Contatto
    Public Property n As String 'nome
    Public Property t As String 'tel
    Public Property a As String 'tel alternativo
    Public Property at As Integer  ' ?
    Public Property gr As String  'group index
    Public Property emln As Integer 'email index
    Public Property snn As Integer 'cognome index
    Public Const ParameterSize As Integer = 7


    Sub New(Optional n = "", Optional t = "", Optional a = "", Optional at = "255", Optional gr = "0000000000FFFFFFFFFF", Optional emln = 255, Optional snn = 255)
        Me.n = n
        Me.t = t
        Me.a = a
        Me.at = at
        Me.gr = gr
        Me.emln = emln
        Me.snn = snn
    End Sub

    Public Shared Operator =(contact1 As Contatto, contact2 As Contatto) As Boolean

        If contact1.n <> contact2.n Then Return False
        If contact1.t <> contact2.t Then Return False
        If contact1.a <> contact2.a Then Return False
        If contact1.at <> contact2.at Then Return False
        If contact1.gr <> contact2.gr Then Return False
        If contact1.emln <> contact2.emln Then Return False
        If contact1.snn <> contact2.snn Then Return False
        Return True

    End Operator

    Public Shared Operator <>(contact1 As Contatto, contact2 As Contatto) As Boolean
        If contact1.n <> contact2.n Then Return True
        If contact1.t <> contact2.t Then Return True
        If contact1.a <> contact2.a Then Return True
        If contact1.at <> contact2.at Then Return True
        If contact1.gr <> contact2.gr Then Return True
        If contact1.emln <> contact2.emln Then Return True
        If contact1.snn <> contact2.snn Then Return True
        Return False

    End Operator
End Class

Public Class info
    Public Property PhGrSz As Integer
    Public Property PhTySz As Integer
    Public Property EmlType As Integer
    Public Property EmlSz As Integer
    Public Property SNSz As Integer
    Public Property is_3g As Boolean
    Public Const ParameterSize As Integer = 6

    Sub New(Optional is_3g = True, Optional PhoneGroupSize = 0, Optional PhoneCategoriesSize = 0, Optional EmailType = 0, Optional EmailSize = 0, Optional SecondNameSize = 0)
        PhGrSz = PhoneGroupSize
        PhTySz = PhoneCategoriesSize
        EmlSz = EmailSize
        EmlType = EmailType
        SNSz = SecondNameSize
        Me.is_3g = is_3g
    End Sub
End Class

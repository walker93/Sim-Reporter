Module Variabili
    Public SNM() As String
    Public groups() As String
    Public categories() As String
    Public EmL() As String
    Public numeri_servizio As New List(Of registro) 'numeri servizio
    Public avaiable_carriers As New List(Of carrier) 'operatori disponibile
    Public forbidden_carriers As New List(Of carrier) 'operatori vietati
    Public sms_flags As New List(Of String) 'SMS Flags
    Public messaggi As New List(Of Messaggio) 'messaggi
    Public numeri_fissi As New List(Of registro) 'numeri fissi
    Public ultime_chiamate As New List(Of registro) 'ultime chiamate
    Public numeri_personali As New List(Of registro) 'numeri personali
    Public contacts As New List(Of Contatto) 'contatti
    Public memory_index As New Dictionary(Of String, Integer)
    Public memory_count As New Dictionary(Of String, Integer)
    Public righe_file As New List(Of String)
    Public version_file As String()
    Public pretty_info As info
    Public backup_righe_file As New List(Of String)
    Public info_CSV As New List(Of Tuple(Of String, String))

    Public nome_tecnico As String
    Public note As String
    Public nome_caso As String
    Public titolo_file As String
End Module

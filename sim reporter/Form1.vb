Public Class Form1

    Dim SNM() As String
    Dim groups() As String
    Dim categories() As String
    Dim EmL() As String
    Dim numeri_servizio As New List(Of registro) 'numeri servizio
    Dim avaiable_carriers As New List(Of carrier) 'operatori disponibile
    Dim forbidden_carriers As New List(Of carrier) 'operatori vietati
    Dim sms_flags As New List(Of String) 'SMS Flags
    Dim messaggi As New List(Of Messaggio) 'messaggi
    Dim numeri_fissi As New List(Of registro) 'numeri fissi
    Dim ultime_chiamate As New List(Of registro) 'ultime chiamate
    Dim numeri_personali As New List(Of registro) 'numeri personali
    Dim contacts As New List(Of Contatto) 'contatti
    Dim memory_index As New Dictionary(Of String, Integer)
    Dim memory_count As New Dictionary(Of String, Integer)
    Dim righe_file As New List(Of String)
    Dim version_file As String()
    Dim pretty_info As info
    Dim backup_righe_file As New List(Of String)

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.Filter = "Dekart phonebook file|*.phn"
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim file As String = OpenFileDialog1.FileName
            TextBox1.Text = file
            btn_carica.Enabled = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btn_carica.Click

        righe_file.AddRange(IO.File.ReadAllLines(TextBox1.Text))
        backup_righe_file.AddRange(righe_file)
        'righe_file.RemoveRange(4018, 7202)

        'mapping

        mapping()

        'loading

        version_file = righe_file.Take(1).ToArray
        righe_file.RemoveRange(0, 2)
        If righe_file.Contains("3g=1") Then
            loading()
        Else
            loading_old()
        End If

        'parsing

        For y = 0 To groups.Length - 1
            groups(y) = groups(y).Substring(groups(y).IndexOf("=") + 1)
        Next
        For y = 0 To SNM.Length - 1
            SNM(y) = SNM(y).Substring(SNM(y).IndexOf("=") + 1)
        Next
        For y = 0 To EmL.Length - 1
            EmL(y) = EmL(y).Substring(EmL(y).IndexOf("=") + 1)
        Next
        For y = 0 To categories.Length - 1
            categories(y) = categories(y).Substring(categories(y).IndexOf("=") + 1)
        Next

        generahtml()

        file_version_lbl.Text += " " & version_file(0).Substring(version_file(0).Length - 2)
    End Sub

    Private Sub generahtml()
        Dim header As String = "<head><style>table, th, td { border: 1px solid black; padding: 5px; } table { border-collapse: collapse; }</style></head>"
        WebBrowser1.DocumentText = header
    End Sub

    Sub mapping()
        memory_index.Clear()
        memory_count.Clear()
        For Each row In righe_file
            Select Case row
                Case "[0000]"
                    memory_index.Add("[0000]", righe_file.IndexOf(row) + 1)
                Case "[0001]"
                    memory_index.Add("[0001]", righe_file.IndexOf(row) + 1)
                Case "[0003]"
                    memory_index.Add("[0003]", righe_file.IndexOf(row) + 1)
                Case "[0005]"
                    memory_index.Add("[0005]", righe_file.IndexOf(row) + 1)
                Case "[0006]"
                    memory_index.Add("[0006]", righe_file.IndexOf(row) + 1)
                Case "[0007]"
                    memory_index.Add("[0007]", righe_file.IndexOf(row) + 1)
                Case "[6F3C]"
                    memory_index.Add("[6F3C]", righe_file.IndexOf(row) + 1)
                Case "[SMSFlags]"
                    memory_index.Add("[SMSFlags]", righe_file.IndexOf(row) + 1)
                Case "[FPLMN]"
                    memory_index.Add("[FPLMN]", righe_file.IndexOf(row) + 1)
                Case "[PLMN]"
                    memory_index.Add("[PLMN]", righe_file.IndexOf(row) + 1)
                Case "[OPLMN]"
                    memory_index.Add("[OPLMN]", righe_file.IndexOf(row) + 1)
            End Select
        Next
        For i = 1 To memory_index.Count
            memory_count.Add(memory_index.Keys(i - 1), memory_index.Values(i) - memory_index.Values(i - 1) - 1)
        Next

    End Sub

    Sub loading()
        Dim info() As String = righe_file.Take(6).ToArray
        righe_file.RemoveRange(0, 6)
        For y = 0 To 5
            info(y) = info(y).Substring(info(y).IndexOf("=") + 1)
        Next
        pretty_info = New info(info(0), info(1), info(2), info(3), info(4), info(5))


        groups = righe_file.Take(pretty_info.PhGrSz).ToArray 'groups
        righe_file.RemoveRange(0, pretty_info.PhGrSz)

        categories = righe_file.Take(pretty_info.PhTySz).ToArray 'categories
        righe_file.RemoveRange(0, pretty_info.PhTySz)

        EmL = righe_file.Take(pretty_info.EmlSz).ToArray  'email
        righe_file.RemoveRange(0, pretty_info.EmlSz)

        SNM = righe_file.Take(pretty_info.SNSz).ToArray  'cognome
        righe_file.RemoveRange(0, pretty_info.SNSz)


        For i = 1 To getLenght("[0007]", Contatto.ParameterSize) 'Contatti
            Dim temp() As String = righe_file.Take(Contatto.ParameterSize).ToArray
            For y = 0 To Contatto.ParameterSize - 1
                temp(y) = temp(y).Substring(temp(y).IndexOf("=") + 1)
            Next
            righe_file.RemoveRange(0, Contatto.ParameterSize)
            Dim contact As New Contatto(temp(0), temp(1), temp(2), temp(3), temp(4), temp(5), temp(6))
            contacts.Add(contact)
        Next
        righe_file.RemoveAt(0)

        For i = 1 To getLenght("[0005]", registro.ParameterSize) 'Numeri personali
            Dim temp() As String = righe_file.Take(registro.ParameterSize).ToArray
            For y = 0 To registro.ParameterSize - 1
                temp(y) = temp(y).Substring(temp(y).IndexOf("=") + 1)
            Next
            righe_file.RemoveRange(0, registro.ParameterSize)
            Dim reg As New registro(temp(0), temp(1))
            numeri_personali.Add(reg)
        Next
        righe_file.RemoveAt(0)

        For i = 1 To getLenght("[0003]", registro.ParameterSize) 'Chiamate recenti
            Dim temp() As String = righe_file.Take(registro.ParameterSize).ToArray
            For y = 0 To registro.ParameterSize - 1
                temp(y) = temp(y).Substring(temp(y).IndexOf("=") + 1)
            Next
            righe_file.RemoveRange(0, registro.ParameterSize)
            Dim reg As New registro(temp(0), temp(1))
            ultime_chiamate.Add(reg)
        Next
        righe_file.RemoveAt(0)
        For i = 1 To getLenght("[6F3C]", registro.ParameterSize) 'numeri Fissi
            Dim temp() As String = righe_file.Take(2).ToArray
            For y = 0 To 1
                temp(y) = temp(y).Substring(temp(y).IndexOf("=") + 1)
            Next
            righe_file.RemoveRange(0, 2)
            Dim reg As New registro(temp(0), temp(1))
            numeri_fissi.Add(reg)
        Next
        righe_file.RemoveAt(0)

        For i = 1 To getLenght("[SMSFlags]", Messaggio.ParameterSize) 'messaggi
            Dim temp() As String = righe_file.Take(Messaggio.ParameterSize).ToArray
            For y = 0 To Messaggio.ParameterSize - 1
                temp(y) = temp(y).Substring(temp(y).IndexOf("=") + 1)
            Next
            righe_file.RemoveRange(0, Messaggio.ParameterSize)
            Dim reg As New Messaggio(temp(0), temp(1), temp(2), temp(3))
            messaggi.Add(reg)
        Next
        righe_file.RemoveAt(0)

        For i = 1 To getLenght("[FPLMN]", 1) 'flags
            Dim temp As String = righe_file.Take(1).ToArray(0)
            temp = temp.Substring(temp.IndexOf("=") + 1)
            righe_file.RemoveRange(0, 1)
            'Dim reg As New Messaggio(temp(0))
            sms_flags.Add(temp)
        Next
        righe_file.RemoveAt(0)

        For i = 1 To getLenght("[PLMN]", 2) 'operatori vietati
            Dim temp() As String = righe_file.Take(2).ToArray
            For y = 0 To 1
                temp(y) = temp(y).Substring(temp(y).IndexOf("=") + 1)
            Next
            righe_file.RemoveRange(0, 2)
            Dim reg As New carrier(temp(0), temp(1))
            forbidden_carriers.Add(reg)
        Next
        righe_file.RemoveAt(0)

        For i = 1 To getLenght("[OPLMN]", 2) 'operatori consentiti
            Dim temp() As String = righe_file.Take(2).ToArray
            For y = 0 To 1
                temp(y) = temp(y).Substring(temp(y).IndexOf("=") + 1)
            Next
            righe_file.RemoveRange(0, 2)
            Dim reg As New carrier(temp(0), temp(1))
            avaiable_carriers.Add(reg)
        Next
        righe_file.RemoveAt(0)

    End Sub

    Sub loading_old()


    End Sub

    Function getLenght(ByVal Memory_key As String, ByVal ParameterSize As Integer) As Integer
        Dim lungh = 0
        mapping()
        memory_index.TryGetValue(Memory_key, lungh)
        lungh -= 1
        lungh \= ParameterSize
        Return lungh
    End Function
End Class
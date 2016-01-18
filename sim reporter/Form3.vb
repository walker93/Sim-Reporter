Public Class Form3
    Dim doc As HtmlDocument
    Dim rubrica_n() As String = {"rubrica_nome", "rubrica_cognome", "rubrica_numero", "rubrica_email", "rubrica_num_alt", "rubrica_gruppo", "rubrica_categoria"}
    Dim messaggi_n() As String = {"messaggi_stato", "messaggi_nome", "messaggi_numero", "messaggi_data"}
    Dim registro_n() As String = {"registro_nome", "registro_numero"}
    Dim op_n() As String = {"op_MCC", "op_MNC"}
    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles web.DocumentCompleted
        doc = web.Document
        fill_riepilogo()
        fill_SimInfo()
        fill_rubrica()
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        web.Navigate("F:\Workstation 2\Documenti\Visual Studio 2015\Projects\sim reporter\sim reporter\bin\Debug\template.html")
    End Sub

    Sub fill_riepilogo()
        doc.GetElementById("nome_tecnico").InnerText = nome_tecnico
        doc.GetElementById("titolo").InnerText = titolo_file
        doc.GetElementById("note").InnerText = note
        doc.GetElementById("nome_caso").InnerText = nome_caso
        doc.GetElementById("data_estrazione").InnerText = info_CSV.Last.Item2
    End Sub

    Sub fill_SimInfo()
        doc.GetElementById("ICCID").InnerText = info_CSV(0).Item2
        doc.GetElementById("IMSI").InnerText = info_CSV(2).Item2
        doc.GetElementById("fornitore").InnerText = info_CSV(1).Item2
    End Sub

    Sub fill_rubrica()
        Dim rubrica As HtmlElement = doc.GetElementById("rubrica")
        For i = 0 To contacts.Count - 1 'numero di righe (in questo caso contatti)
            If contacts(i) = (New Contatto()) Then Continue For
            Dim riga_element As HtmlElement = doc.CreateElement("tr")
            For j = 0 To Contatto.ParameterSize - 1 'numero di paramentri
                Dim td As HtmlElement = doc.CreateElement("td")
                td.Name = rubrica_n(j)
                Select Case j
                    Case 0
                        td.InnerText = contacts(i).n
                    Case 1
                        Try
                            td.InnerText = SNM(contacts(i).snn)
                        Catch
                            td.InnerText = ""
                        End Try
                    Case 2
                        td.InnerText = contacts(i).t
                    Case 3
                        Try
                            td.InnerText = EmL(contacts(i).emln)
                        Catch
                            td.InnerText = ""
                        End Try
                    Case 4
                        td.InnerText = contacts(i).a
                    Case 5
                        td.InnerText = If(contacts(i).gr = "0000000000FFFFFFFFFF", "", groups(contacts(i).gr))
                    Case 6
                        Try
                            td.InnerText = categories(contacts(i).at)
                        Catch
                            td.InnerText = ""
                        End Try
                End Select

                riga_element.AppendChild(td)
            Next
            rubrica.FirstChild.AppendChild(riga_element)
        Next




        'Dim y = 0
        'For Each nome As HtmlElement In doc.GetElementsByTagName("td").GetElementsByName("rubrica_nome")
        '    nome.InnerText = contacts(y).n
        '    y += 1
        'Next
    End Sub
End Class
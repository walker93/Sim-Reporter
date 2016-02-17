Imports System.Xml
Public Class Form3
    Dim doc As HtmlDocument
    Dim rubrica_n() As String = {"rubrica_nome", "rubrica_cognome", "rubrica_numero", "rubrica_email", "rubrica_num_alt", "rubrica_gruppo", "rubrica_categoria"}
    Dim messaggi_n() As String = {"messaggi_stato", "messaggi_numero", "messaggi_testo", "messaggi_data"}
    Dim registro_n() As String = {"registro_nome", "registro_numero"}
    Dim op_n() As String = {"op_network", "op_MCC", "op_MNC"}
    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles web.DocumentCompleted
        doc = web.Document
        fill_riepilogo()
        fill_SimInfo()
        If is_old Then
            fill_rubrica_old()
        Else
            fill_rubrica()
        End If
        fill_messaggi()
        fill_personali()
        fill_ultime()
        fill_fissi()
        fill_operatori()
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

    Sub fill_rubrica_old()
        Dim del_rubrica As HtmlElement = doc.GetElementById("rubrica")
        del_rubrica.Style = "Display: none;"
        Dim rubrica As HtmlElement = doc.GetElementById("rubrica_old")
        For i = 0 To contacts_old.Count - 1 'numero di righe (in questo caso contatti)
            If contacts_old(i) = (New registro()) Then Continue For
            Dim riga_element As HtmlElement = doc.CreateElement("tr")
            For j = 0 To registro.ParameterSize - 1 'numero di paramentri
                Dim td As HtmlElement = doc.CreateElement("td")
                td.Name = rubrica_n(j)
                Select Case j
                    Case 0
                        td.InnerText = contacts_old(i).n
                    Case 1
                        td.InnerText = contacts_old(i).t
                End Select
                riga_element.AppendChild(td)
            Next
            rubrica.FirstChild.AppendChild(riga_element)
        Next
    End Sub

    Sub fill_rubrica()
        Dim del_rubrica As HtmlElement = doc.GetElementById("rubrica_old")
        del_rubrica.Style = "Display: none;"
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
    End Sub

    Sub fill_messaggi()
        Dim messaggi_table As HtmlElement = doc.GetElementById("messaggi")
        For Each mes As Messaggio In messaggi
            If mes = New Messaggio Then Continue For
            Dim riga_element As HtmlElement = doc.CreateElement("tr")
            For j = 0 To messaggi_n.Length - 1 'numero di paramentri
                Dim td As HtmlElement = doc.CreateElement("td")
                td.Name = messaggi_n(j)
                Select Case j
                    Case 0
                        td.InnerText = sms_flags(messaggi.IndexOf(mes))
                    Case 1
                        td.InnerText = mes.t
                    Case 2
                        td.InnerText = mes.m
                    Case 3
                        td.InnerText = mes.time
                End Select
                riga_element.AppendChild(td)
            Next
            messaggi_table.FirstChild.AppendChild(riga_element)
        Next

    End Sub

    Sub fill_personali()
        Dim personali As HtmlElement = doc.GetElementById("personali")
        For Each num As registro In numeri_personali
            If num = New registro() Then Continue For
            Dim riga_element As HtmlElement = doc.CreateElement("tr")
            For j = 0 To registro_n.Length - 1 'numero di paramentri
                Dim td As HtmlElement = doc.CreateElement("td")
                td.Name = registro_n(j)
                Select Case j
                    Case 0
                        td.InnerText = num.n
                    Case 1
                        td.InnerText = num.t
                End Select
                riga_element.AppendChild(td)
            Next
            personali.FirstChild.AppendChild(riga_element)
        Next
    End Sub

    Sub fill_ultime()
        Dim ultime As HtmlElement = doc.GetElementById("ultime")
        For Each num As registro In ultime_chiamate
            If num = New registro() Then Continue For
            Dim riga_element As HtmlElement = doc.CreateElement("tr")
            For j = 0 To registro_n.Length - 1 'numero di paramentri
                Dim td As HtmlElement = doc.CreateElement("td")
                td.Name = registro_n(j)
                Select Case j
                    Case 0
                        td.InnerText = num.n
                    Case 1
                        td.InnerText = num.t
                End Select
                riga_element.AppendChild(td)
            Next
            ultime.FirstChild.AppendChild(riga_element)
        Next
    End Sub

    Sub fill_fissi()
        Dim fissi As HtmlElement = doc.GetElementById("fissi")
        For Each num As registro In numeri_fissi
            If num = New registro() Then Continue For
            Dim riga_element As HtmlElement = doc.CreateElement("tr")
            For j = 0 To registro_n.Length - 1 'numero di paramentri
                Dim td As HtmlElement = doc.CreateElement("td")
                td.Name = registro_n(j)
                Select Case j
                    Case 0
                        td.InnerText = num.n
                    Case 1
                        td.InnerText = num.t
                End Select
                riga_element.AppendChild(td)
            Next
            fissi.FirstChild.AppendChild(riga_element)
        Next
    End Sub

    Sub fill_operatori()
        Dim op As HtmlElement = doc.GetElementById("op")
        For Each car As carrier In forbidden_carriers
            If car = New carrier() Then Continue For
            Dim riga_element As HtmlElement = doc.CreateElement("tr")
            For j = 0 To op_n.Length - 1 'numero di paramentri
                Dim td As HtmlElement = doc.CreateElement("td")
                td.Name = op_n(j)
                Select Case j
                    Case 0
                        td.InnerText = getCarrierName(car)
                    Case 1
                        td.InnerText = car.mcc
                    Case 2
                        td.InnerText = car.mnc
                End Select
                riga_element.AppendChild(td)
            Next
            op.FirstChild.AppendChild(riga_element)
        Next
    End Sub

    Function getCarrierName(op As carrier) As String
        Dim xml As New XmlDocument()
        xml.Load("F:\Workstation 2\Documenti\Visual Studio 2015\Projects\sim reporter\sim reporter\mcc-mnc-table.xml")
        Dim elements As XmlNodeList
        elements = xml.GetElementsByTagName("carrier")
        Dim xmlmnc As String
        Dim xmlmcc As Integer
        For Each elem As XmlNode In elements
            xmlmnc = elem.SelectSingleNode("mnc").InnerText
            xmlmcc = elem.SelectSingleNode("mcc").InnerText
            If xmlmnc = "n/a" Then Continue For
            If Integer.Parse(xmlmnc) = op.mnc AndAlso xmlmcc = op.mcc Then
                Return elem.SelectSingleNode("network").InnerText
            End If
        Next
        Return ""
    End Function

End Class
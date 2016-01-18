Public Class Form3
    Dim doc As HtmlDocument

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles web.DocumentCompleted
        doc = web.Document
        fill_riepilogo()
        fill_SimInfo()
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
        doc.GetElementById("IMSI").InnerText = info_CSV(1).Item2
        doc.GetElementById("fornitore").InnerText = info_CSV(2).Item2
    End Sub

End Class
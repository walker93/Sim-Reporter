Public Class Form2
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        nome_tecnico = TextBox1.Text
        nome_caso = TextBox2.Text
        note = TextBox3.Text
        titolo_file = TextBox4.Text
        If nome_tecnico = "" Or titolo_file = "" Then
            MsgBox("Inserire tutti i campi richiesti")
        Else
            Form3.Show()
            Close()
        End If
    End Sub
End Class
Imports System.Runtime.CompilerServices

Module Module1
    <Extension()>
    Public Function PopFirst(Of T)(ByRef source As IEnumerable(Of T)) As String
        Dim element = source.First
        source.ToList.RemoveAt(0)
        Return element.ToString
    End Function


End Module

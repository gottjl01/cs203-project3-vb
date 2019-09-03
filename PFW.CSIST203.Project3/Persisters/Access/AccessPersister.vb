Namespace PFW.CSIST203.Project3.Persisters.Access

    ''' <summary>
    ''' Access persister that interacts with an Access database
    ''' </summary>
    Public Class AccessPersister
        Implements IPersistData

        Friend ReadOnly accessFile As String = Nothing
        Friend noDatabase As Boolean = False
        Friend IsDisposed As Boolean = False

        Public Sub New()
            'TODO: Implement
        End Sub

        Public Sub New(accessFile As String)
            Throw New NotImplementedException()
        End Sub

        ''' <summary>
        ''' The filter used by the open dialog to find files that this persister will handle
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property FileFilter As String Implements IPersistData.FileFilter
            Get
                Return "Access Database Files|*.mdb;*.accdb"
            End Get
        End Property

        Public Function GetRow(id As Integer) As DataRow Implements IPersistData.GetRow
            Return Nothing
        End Function

        Public Function CountRows() As Integer Implements IPersistData.CountRows
            Return 0
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
            ' TODO: Implement
        End Sub

        Public Function GetData() As DataTable Implements IPersistData.GetData
            Return New DataTable()
        End Function

        Public Sub UpdateRow(row As DataRow) Implements IPersistData.UpdateRow
            Throw New NotImplementedException()
        End Sub

    End Class

End Namespace


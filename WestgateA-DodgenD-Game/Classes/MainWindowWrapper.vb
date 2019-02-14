Namespace Classes
    ' ReSharper disable once ClassNeverInstantiated.Global
    Public Class MainWindowWrapper
        ''' <summary>
        ''' Gets the MainWindow as an object and provides it as a static object
        ''' </summary>
        Public Shared ReadOnly MainWindowInstance As MainWindow =
                                   Application.Current.MainWindow
    End Class
End Namespace
Imports JetBrains.Annotations

Namespace Classes
    <UsedImplicitly>
    Public Class MainWindowWrapper
        ''' <summary>
        ''' Get the MainWindow as an object and provides it as a static object
        ''' </summary>
        Public Shared ReadOnly MainWindowInstance As MainWindow =
                                   Application.Current.MainWindow
    End Class
End Namespace
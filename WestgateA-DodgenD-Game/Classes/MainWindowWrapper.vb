Namespace Classes
    ' ReSharper disable once ClassNeverInstantiated.Global
    Public Class MainWindowWrapper
        ''' <summary>
        ''' Gets the MainWindow as an object and provides it as a static object
        ''' </summary>
        Public Shared ReadOnly MainWindowInstance As MainWindow = Application.Current.MainWindow

        ''' <summary>
        ''' 
        ''' </summary>
        Public Shared ReadOnly CanvasGameScreen As Controls.Canvas = MainWindowInstance.CanvasGameScreen

        ''' <summary>
        ''' Width of canvas in pixels
        ''' </summary>
        Public Const CanvasWidth As Double = 672

        ''' <summary>
        ''' Height of canvas in pixels
        ''' </summary>
        Public Const CanvasHeight As Double = 768
    End Class
End Namespace
Imports WestgateA_DodgenD_Game.Classes.Entities

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

        ''' <summary>
        ''' Sets the canvas location of a control
        ''' </summary>
        ''' <param name="locationX">X coordinate of desired location</param>
        ''' <param name="locationY">Y coordinate of desired location</param>
        ''' <param name="control">Control to be placed</param>
        Public Shared Sub SetCanvasLocation(locationX As Double,
                                            locationY As Double,
                                            control As Object)
            Controls.Canvas.SetLeft(control, locationX)
            Controls.Canvas.SetBottom(control, locationY)
        End Sub

        ''' <summary>
        ''' Adds a control to canvas
        ''' </summary>
        ''' <param name="localControl">Object representing control</param>
        Public Shared Sub AddToCanvas(localControl)
            If (localControl.GetType().IsSubclassOf((New EntityClasses.EntityBase).GetType().BaseType)) Then
                CanvasGameScreen.Children.Add(localControl.EntityControl)
            Else

            End If

        End Sub
    End Class
End Namespace
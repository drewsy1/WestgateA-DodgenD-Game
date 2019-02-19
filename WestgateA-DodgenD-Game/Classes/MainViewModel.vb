Imports System.Collections.ObjectModel
Imports WestgateA_DodgenD_Game.Classes.Entities

Namespace Classes
    ' ReSharper disable once ClassNeverInstantiated.Global
    Public Class MainViewModel
        ''' <summary>
        ''' Gets the MainWindow as an object and provides it as a static object
        ''' </summary>
        Public Shared MainWindowInstance As MainWindow

        ''' <summary>
        ''' TODO Write CanvasGameScreen summary
        ''' </summary>
        Public Shared CanvasGameScreen As Canvas

        ''' <summary>
        ''' Variable for current EntityPlayer object
        ''' </summary>
        Public Shared EntityPlayerObject As EntityClasses.EntityPlayer

        ''' <summary>
        ''' Height of canvas in pixels
        ''' </summary>
        Public Const CanvasHeight As Double = 768

        ''' <summary>
        ''' Width of canvas in pixels
        ''' </summary>
        Public Const CanvasWidth As Double = 672

        ''' <summary>
        ''' TODO Write GameScore summary
        ''' </summary>
        Public Shared GameScore As Integer

        ''' <summary>
        ''' TODO Write GameLives summary
        ''' </summary>
        Public Shared GameLives As Integer

        ''' <summary>
        ''' TODO Write GameLevel summary
        ''' </summary>
        Public Shared GameLevel As Integer

        ''' <summary>
        ''' TODO Write ObjectCollection summary
        ''' </summary>
        Public Shared ReadOnly EnemyCollection As ObservableCollection(Of EntityClasses.EntityEnemyBase) =
                                   New ObservableCollection(Of EntityClasses.EntityEnemyBase)()

        ''' <summary>
        ''' Adds a control to canvas
        ''' </summary>
        ''' <param name="localControl">Object representing control</param>
        Public Shared Sub AddToCanvas(localControl)
            If (localControl.GetType().IsSubclassOf(GetType(CanvasObjects).BaseType)) Then
                CanvasGameScreen.Children.Add(localControl.ObjectControl)
            Else

            End If
        End Sub

        ''' <summary>
        ''' Sets the canvas location of a control
        ''' </summary>
        ''' <param name="localLocation">Coordinate of desired location</param>
        ''' <param name="control">Control to be placed</param>
        Public Shared Sub SetCanvasLocation(localLocation As Point,
                                            control As Object)
            Canvas.SetLeft(control, localLocation.X)
            Canvas.SetBottom(control, localLocation.Y)
        End Sub
    End Class
End Namespace
Imports System.Collections.ObjectModel
Imports System.Windows.Threading
Imports JetBrains.Annotations
Imports WestgateA_DodgenD_Game.Classes


<UsedImplicitly>
Public Class MainWindow

    ReadOnly _dtTimer As DispatcherTimer
    Dim _currentKeyPress As Key
    Dim PlayerObject As Player

    Sub New()
        InitializeComponent()

        _dtTimer = New DispatcherTimer With {
            .Interval = TimeSpan.FromMilliseconds(1)
        }
        AddHandler _dtTimer.Tick, AddressOf GameTimeUpdater
        _dtTimer.Start()

        PlayerObject = New Player()
        PlayerObject.AddToCanvas()

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GameTimeUpdater(sender As Object, e As EventArgs)
        RegisterKeypresses()
        Projectile.ProjectilesCollection.ForEach(Sub(obj As Projectile) obj.UpdateLocation())
    End Sub

    Private Sub RegisterKeypresses()
        Select Case _currentKeyPress
            Case Key.Left
                PlayerObject.MoveLeft()
            Case Key.Right
                PlayerObject.MoveRight()
            Case Key.Space
                PlayerObject.FireWeapon()
        End Select
    End Sub

#Region "Window Events"
    ''' <summary>
    ''' Handles event raised when a key is pressed within the Window
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Window_KeyDown(sender As Object, e As KeyEventArgs)
        _currentKeyPress = e.Key
    End Sub

    ''' <summary>
    ''' Handles event raised when a key is released within the Window
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Window_KeyUp(sender As Object, e As KeyEventArgs)
        _currentKeyPress = Nothing
    End Sub
#End Region

End Class

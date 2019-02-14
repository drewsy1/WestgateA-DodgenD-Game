
Imports System.Windows.Threading
Imports JetBrains.Annotations
Imports WestgateA_DodgenD_Game.Classes


<UsedImplicitly>
Public Class MainWindow

    '
    ReadOnly _dtTimer As DispatcherTimer = New DispatcherTimer With {
        .Interval = TimeSpan.FromMilliseconds(16.7)
    }

    '
    Dim _currentKeyPress As Key

    '
    ReadOnly _playerObject As Player

    ''' <summary>
    ''' 
    ''' </summary>
    Sub New()
        InitializeComponent()

        ' Add handler pointing each tick of dtTimer to GameTimeUpdater
        AddHandler _dtTimer.Tick, AddressOf GameTimeUpdater
        _dtTimer.Start()

        _playerObject = New Player()
        _playerObject.AddToCanvas()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GameTimeUpdater(sender As Object, e As EventArgs)
        RegisterKeypresses()
        ProjectileClasses.UpdateProjectiles()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub RegisterKeypresses()
        Select Case _currentKeyPress
            Case Key.Left
                _playerObject.MoveLeft()
            Case Key.Right
                _playerObject.MoveRight()
        End Select
    End Sub

#Region "Window Events"
    ''' <summary>
    ''' Handles event raised when a key is pressed within the Window
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Window_KeyDown(sender As Object, e As KeyEventArgs)
        Select Case e.Key
            Case Key.Left
                _currentKeyPress = e.Key
            Case Key.Right
                _currentKeyPress = e.Key
            Case Key.Space
                _playerObject.FireWeapon()
        End Select

    End Sub

    ''' <summary>
    ''' Handles event raised when a key is released within the Window
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Window_KeyUp(sender As Object, e As KeyEventArgs)
        Select Case e.Key
            Case Key.Left
                _currentKeyPress = Nothing
            Case Key.Right
                _currentKeyPress = Nothing
        End Select
    End Sub
#End Region

End Class

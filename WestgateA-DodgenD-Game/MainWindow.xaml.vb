Imports System.Windows.Threading
Imports WestgateA_DodgenD_Game.Classes
Imports WestgateA_DodgenD_Game.Classes.Projectile


Public Class MainWindow
    ''' <summary>
    ''' Timer that keeps track of game events/objects
    ''' </summary>
    ReadOnly _dtTimer As DispatcherTimer = New DispatcherTimer With {
        .Interval = TimeSpan.FromMilliseconds(1)
    }

    ''' <summary>
    ''' Key (left or right) that controls movement of player cursor
    ''' </summary>
    Dim _currentKeyPress As Key

    ''' <summary>
    ''' Variable for current Player object
    ''' </summary>
    ReadOnly _playerObject As Player

    ''' <summary>
    ''' Instantiates the MainWindow, starts the _dtTimer, and creates/adds the Player instance
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
    ''' This method is called on every tick of the _dtTimer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GameTimeUpdater(sender As Object, e As EventArgs)
        RegisterKeypresses(_currentKeyPress)
        ProjectileClasses.UpdateProjectiles()
    End Sub

    ''' <summary>
    ''' Reads a Key variable and carries out specific actions based on its value
    ''' </summary>
    ''' <param name="currentKeyPress">Key variable to be read/acted upon</param>
    Private Sub RegisterKeypresses(currentKeyPress As Key)
        Select Case currentKeyPress
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
        If Not Keyboard.IsKeyDown(Key.Left) And Not Keyboard.IsKeyDown(Key.Right) Then
            _currentKeyPress = Nothing
        End If
    End Sub
#End Region
End Class

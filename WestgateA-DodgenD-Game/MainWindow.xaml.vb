Imports System.Security.Cryptography.X509Certificates
Imports WestgateA_DodgenD_Game.Classes
Imports WestgateA_DodgenD_Game.Classes.Entities

Public Class MainWindow
    ''' <summary>
    ''' Key (left or right) that controls movement of player cursor
    ''' </summary>
    Dim _currentKeyPress As Key

    ''' <summary>
    ''' Variable for current EntityPlayer object
    ''' </summary>
    ReadOnly _entityPlayerObject As EntityClasses.EntityPlayer

    ''' <summary>
    ''' Instantiates the MainWindow, starts the _dtTimer, and creates/adds the EntityPlayer instance
    ''' </summary>
    Sub New()
        GameTimer.Start()
        InitializeComponent()

        ' Add handler pointing each tick of dtTimer to GameTimeUpdater
        AddHandler GameTimer.Tick, AddressOf GameTimeUpdater

        _entityPlayerObject = New EntityClasses.EntityPlayer()
        MainWindowWrapper.AddToCanvas(_entityPlayerObject)

        Dim EnemyArray(5, 9) As EntityClasses.EntityEnemy

        Dim EnemyAList As List(Of EntityClasses.EntityEnemyA) = New List(Of EntityClasses.EntityEnemyA)

        For b As Integer = 3 To 6
            EnemyArray(0, b) = (New EntityClasses.EntityEnemyD(117 + (45 * b), 630 - (36 * 0)))
        Next

        For b As Integer = 2 To 7
            EnemyArray(1, b) = (New EntityClasses.EntityEnemyC(117 + (45 * b), 630 - (36 * 1)))
        Next

        For b As Integer = 1 To 8
            EnemyArray(2, b) = (New EntityClasses.EntityEnemyB(117 + (45 * b), 630 - (36 * 2)))
        Next

        For a As Integer = 3 To 5
            For b As Integer = 0 To 9
                EnemyArray(a, b) = (New EntityClasses.EntityEnemyA(117 + (45 * b), 630 - (36 * a)))
            Next
        Next

        For Each obj As EntityClasses.EntityEnemy In EnemyArray
            If Not IsNothing(obj) Then MainWindowWrapper.AddToCanvas(obj)
        Next
    End Sub

    ''' <summary>
    ''' This method is called on every tick of the _dtTimer
    ''' </summary>
    Private Sub GameTimeUpdater()
        RegisterKeypresses(_currentKeyPress)
    End Sub

    ''' <summary>
    ''' Reads a Key variable and carries out specific actions based on its value
    ''' </summary>
    ''' <param name="currentKeyPress">Key variable to be read/acted upon</param>
    Private Sub RegisterKeypresses(currentKeyPress As Key)
        Select Case currentKeyPress
            Case Key.Left
                _entityPlayerObject.MoveLeft()
            Case Key.Right
                _entityPlayerObject.MoveRight()
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
                _entityPlayerObject.FireWeapon()
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

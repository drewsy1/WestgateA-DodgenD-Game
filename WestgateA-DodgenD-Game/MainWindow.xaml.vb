Imports System.ComponentModel
Imports WestgateA_DodgenD_Game.Classes
Imports WestgateA_DodgenD_Game.Classes.Entities

Public Class MainWindow
    ''' <summary>
    ''' Key (left or right) that controls movement of player cursor
    ''' </summary>
    Dim _currentKeyPress As Key

    ''' <summary>
    ''' ToDo Write _newDebugWindow summary
    ''' </summary>
    Private Shared _newDebugWindow As DebugWindow


    ''' <summary>
    ''' Instantiates the MainWindow, starts the _dtTimer, and creates/adds the EntityPlayer instance
    ''' </summary>
    Sub New()
        InitializeComponent()

        labelCurrentScore_Copy.Content = My.Settings.HighScore

        GameTimer.Start()

        Application.MainWindowInstance = Me
        Application.CanvasGameScreen = CanvasGameScreen
        Application.EntityPlayerObject = New EntityClasses.EntityPlayer()
        Application.AddToCanvas(Application.EntityPlayerObject)

        ' Add handler pointing each tick of dtTimer to GameTimeUpdater
        AddHandler GameTimer.Tick, AddressOf GameTimeUpdater
        AddHandler Application.ProjectileHit, AddressOf Application.ProcessProjectileHit

        If Debugger.IsAttached Then
            _newDebugWindow = New DebugWindow
            _newDebugWindow.Show()
        End If

        Dim enemyArray(5, 9) As EntityClasses.EntityEnemyBase

        For a = 0 To 5
            Select Case a
                Case 0
                    For b = 3 To 6
                        enemyArray(a, b) = New EntityClasses.EntityEnemyD(("EnemyD_" + CStr(a) + "," + CStr(b)), New Point(117 + (45 * b), 630 - (36 * a)))
                    Next
                Case 1
                    For b = 2 To 7
                        enemyArray(a, b) = New EntityClasses.EntityEnemyC(("EnemyC_" + CStr(a) + "," + CStr(b)), New Point(117 + (45 * b), 630 - (36 * a)))
                    Next
                Case 2
                    For b = 1 To 8
                        enemyArray(a, b) = New EntityClasses.EntityEnemyB(("EnemyB_" + CStr(a) + "," + CStr(b)), New Point(117 + (45 * b), 630 - (36 * a)))
                    Next
                Case 3 To 5
                    For b = 0 To 9
                        enemyArray(a, b) = New EntityClasses.EntityEnemyA(("EnemyA_" + CStr(a) + "," + CStr(b)), New Point(117 + (45 * b), 630 - (36 * a)))
                    Next
            End Select

        Next

        For Each obj As EntityClasses.EntityEnemyBase In enemyArray
            If Not IsNothing(obj) Then Application.AddToCanvas(obj)
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
    Private Shared Sub RegisterKeypresses(currentKeyPress As Key)
        Select Case currentKeyPress
            Case Key.Left
                Application.EntityPlayerObject.MoveLeft()
            Case Key.Right
                Application.EntityPlayerObject.MoveRight()
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
                Application.EntityPlayerObject.FireWeapon()
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
        If e.Key = Key.Space Then
            Application.RaiseReleaseFireButton()
        End If
    End Sub


    Private Sub Window_Closing(sender As Object, e As CancelEventArgs)
        My.Settings.Save()
    End Sub


#End Region
End Class

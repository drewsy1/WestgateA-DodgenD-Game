Imports System.Collections.ObjectModel
Imports System.Security.AccessControl
Imports System.Timers
Imports System.Windows.Threading
Imports WestgateA_DodgenD_Game.Classes
Imports WestgateA_DodgenD_Game.Classes.Entities
Imports WestgateA_DodgenD_Game.Classes.Projectile
' ReSharper disable VBPossibleMistakenCallToGetType.2
Public Class Application

    Private Shared WithEvents _introTimer As DispatcherTimer

    Private Shared _introTimerElapsed As Integer

    Public Shared GameStatus As Integer

    ''' <summary>
    ''' TODO Write EnemyCollection summary
    ''' </summary>
    Public Shared ReadOnly EnemyCollection As ObservableCollection(Of EntityClasses.EntityEnemyBase) =
                               New ObservableCollection(Of EntityClasses.EntityEnemyBase)()

    ''' <summary>
    ''' TODO Write ActiveEnemies summary
    ''' </summary>
    Public Shared ReadOnly ActiveEnemies As ObservableCollection(Of EntityClasses.EntityEnemyBase) =
                               New ObservableCollection(Of EntityClasses.EntityEnemyBase)()

    ''' <summary>
    ''' ToDO Write EnemyArray summary
    ''' </summary>
    Public Shared EnemyArray(5, 9) As EntityClasses.EntityEnemyBase

    ''' <summary>
    ''' TODO Write CanvasGameScreen summary
    ''' </summary>
    Public Shared CanvasGameScreen As Canvas

    ''' <summary>
    ''' Height of canvas in pixels
    ''' </summary>
    Public Shared Property CanvasHeight As Double = 768

    ''' <summary>
    ''' Width of canvas in pixels
    ''' </summary>
    Public Shared Property CanvasWidth As Double = 672

    ''' <summary>
    ''' Variable for current EntityPlayer object
    ''' </summary>
    Public Shared Property EntityPlayerObject As EntityClasses.EntityPlayer

    Public Shared Property CurrentGameStats As GameStats

    ''' <summary>
    ''' Gets the MainWindow as an object and provides it as a static object
    ''' </summary>
    Public Shared Property MainWindowInstance As MainWindow

#Region "Events"
    ''' <summary>
    ''' ToDo Write EnemyHit summary
    ''' </summary>
    ''' <param name="enemy"></param>
    Public Shared Event EnemyHit(enemy As EntityClasses.EntityEnemyBase)

    ''' <summary>
    ''' ToDo Write PlayerProjectileRemove summary
    ''' </summary>
    ''' <param name="projectile"></param>
    Public Shared Event PlayerProjectileRemove(projectile As ProjectileClasses.ProjectilePlayer)

    ''' <summary>
    ''' ToDo Write PressFireButton summary
    ''' </summary>
    Public Shared Event PressFireButton()

    ''' <summary>
    ''' ToDo Write ProjectileHit summary
    ''' </summary>
    ''' <param name="projectile"></param>
    ''' <param name="entity"></param>
    Public Shared Event ProjectileHit(ByRef projectile As ProjectileClasses.ProjectileBase, ByRef entity As Object)

    ''' <summary>
    ''' ToDo Write CollisionHit summary
    ''' </summary>
    ''' <param name="player"></param>
    ''' <param name="entity"></param>
    Public Shared Event CollisionHit(ByRef player As EntityClasses.EntityPlayer, ByRef entity As Object)

    ''' <summary>
    ''' ToDo Write ReleaseFireButton summary
    ''' </summary>
    Public Shared Event ReleaseFireButton()

    ''' <summary>
    ''' ToDo Write LevelCleared summary
    ''' </summary>
    Public Shared Event LevelCleared()
#End Region

#Region "Event Friend Functions"

    ''' <summary>
    ''' ToDo Write RaiseEnemyHit summary
    ''' </summary>
    ''' <param name="enemy"></param>
    Friend Shared Sub RaiseEnemyHit(enemy As EntityClasses.EntityEnemyBase)
        RaiseEvent EnemyHit(enemy)
    End Sub

    ''' <summary>
    ''' ToDo Write RaisePlayerProjectileRemove Summary
    ''' </summary>
    ''' <param name="projectile"></param>
    Friend Shared Sub RaisePlayerProjectileRemove(projectile As ProjectileClasses.ProjectilePlayer)
        RaiseEvent PlayerProjectileRemove(projectile)
    End Sub

    ''' <summary>
    ''' ToDo Write RaisePressFireButton summary
    ''' </summary>
    Friend Shared Sub RaisePressFireButton()
        RaiseEvent PressFireButton()
    End Sub

    ''' <summary>
    ''' ToDo Write RaiseProjectileHit summary
    ''' </summary>
    ''' <param name="projectile"></param>
    ''' <param name="entity"></param>
    Friend Shared Sub RaiseProjectileHit(ByRef projectile As ProjectileClasses.ProjectileBase, ByRef entity As Object)
        RaiseEvent ProjectileHit(projectile, entity)
    End Sub

    ''' <summary>
    ''' ToDo Write RaiseCollisionHit summary
    ''' </summary>
    ''' <param name="player"></param>
    ''' <param name="entity"></param>
    Friend Shared Sub RaiseCollisionHit(ByRef player As EntityClasses.EntityPlayer, ByRef entity As Object)
        RaiseEvent CollisionHit(player, entity)
    End Sub

    ''' <summary>
    ''' ToDo Write RaiseReleaseFireButton summary
    ''' </summary>
    Friend Shared Sub RaiseReleaseFireButton()
        RaiseEvent ReleaseFireButton()
    End Sub

    ''' <summary>
    ''' ToDo Write RaiseLevelCleared summary
    ''' </summary>
    Friend Shared Sub RaiseLevelCleared()
        RaiseEvent LevelCleared()
    End Sub
#End Region

#Region "Event Methods"

    ''' <summary>
    ''' ToDo Write OnProjectileHit summary
    ''' </summary>
    ''' <param name="projectile"></param>
    ''' <param name="entity"></param>
    Public Shared Sub OnProjectileHit(ByRef projectile As ProjectileClasses.ProjectileBase, ByRef entity As Object)
        UpdateScore(entity)
        projectile.Remove()
        entity.Remove()
    End Sub

    ''' <summary>
    ''' ToDo Write OnCollisionHit summary
    ''' </summary>
    ''' <param name="player"></param>
    ''' <param name="entity"></param>
    Public Shared Sub OnCollisionHit(ByRef player As EntityClasses.EntityPlayer, ByRef entity As Object)
        player.Remove()
        System.Threading.Thread.Sleep(1000)
        SubtractLife()
        NewGame()

        If CurrentGameStats.GameLives = 0 Then GameOver()
    End Sub

    ''' <summary>
    ''' ToDo Write UpdateScore summary
    ''' </summary>
    ''' <param name="entity"></param>
    Public Shared Sub UpdateScore(ByRef entity As Object)
        If entity.GetType().IsSubclassOf(GetType(EntityClasses.EntityEnemyBase)) Then
            CurrentGameStats.GameScore += entity.ObjectScoreValue
            If My.Settings.HighScore < CurrentGameStats.GameScore Then
                My.Settings.HighScore = CurrentGameStats.GameScore
            End If

            ' Add life if player has earned another 3000 pts
            If Math.IEEERemainder(CurrentGameStats.GameScore,MySettings.Default.NewLifeScore).CompareTo(0) = 0 Then AddLife()
        End If
    End Sub

    ''' <summary>
    ''' ToDo Write OnLevelCleared Summary
    ''' </summary>
    Public Shared Sub OnLevelCleared()
        System.Threading.Thread.Sleep(1000)
        RaiseLevel()
        NewGame()
    End Sub

#End Region

#Region "Shared Methods"

    ''' <summary>
    ''' Adds a control to canvas
    ''' </summary>
    ''' <param name="localControl">Object representing control</param>
    Public Shared Sub AddToCanvas(localControl)
        If (localControl.GetType().IsSubclassOf(GetType(CanvasObjects).BaseType)) Then
            If Not CanvasGameScreen.Children.Contains(localControl.ObjectControl) Then
                CanvasGameScreen.Children.Add(localControl.ObjectControl)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Removes a control to canvas
    ''' </summary>
    ''' <param name="localControl">Object representing control</param>
    Public Shared Sub RemoveFromCanvas(localControl)
        If (localControl.GetType().IsSubclassOf(GetType(CanvasObjects).BaseType)) Then
            CanvasGameScreen.Children.Remove(localControl.ObjectControl)
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

    ''' <summary>
    ''' 
    ''' </summary>
    Public Shared Sub Intro()
        GameStatus = 0
        MainWindowInstance.LabelIntro1.Visibility = Visibility.Hidden
        MainWindowInstance.LabelIntro2.Visibility = Visibility.Hidden
        MainWindowInstance.LabelIntro_PressEnter.Visibility = Visibility.Hidden

        _introTimer  = New DispatcherTimer() With {
            .Interval = TimeSpan.FromMilliseconds(500)
            }
        _introTimer.Start()
        
        AddHandler _introTimer.Tick, AddressOf OnIntroTimer
    End Sub

    Private Shared Sub OnIntroTimer(source As Object,e As EventArgs)
        _introTimerElapsed += 1
        Select _introTimerElapsed
            Case 2
                MainWindowInstance.LabelIntro1.Visibility = Visibility.Visible
            Case 4
                MainWindowInstance.LabelIntro2.Visibility = Visibility.Visible
        End Select
        
        Select MainWindowInstance.LabelIntro_PressEnter.Visibility
            Case Visibility.Hidden
                MainWindowInstance.LabelIntro_PressEnter.Visibility = Visibility.Visible
            Case Visibility.Visible
                MainWindowInstance.LabelIntro_PressEnter.Visibility = Visibility.Hidden
        End Select
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Public Shared Sub NewGame()
        _introTimer.Stop()
        GameStatus = 1
        MainWindowInstance.CanvasGameOver.Visibility = Visibility.Hidden
        MainWindowInstance.CanvasIntro.Visibility = Visibility.Hidden

        MainWindowInstance.LabelCurrentScore.Visibility = Visibility.Visible
        MainWindowInstance.ImageLevel1.Visibility = Visibility.Visible

        For j As Integer = 1 To CurrentGameStats.GameLives-1
            MainWindowInstance.FindName("Life"+j.ToString()).Visibility = Visibility.Visible
        Next

        ActiveEnemies.Clear()

        For Each obj As EntityClasses.EntityEnemyBase In enemyArray
            If Not IsNothing(obj) Then
                ActiveEnemies.Add(obj)
                obj.ObjectEnabled = True
                obj.ObjectTransform_Translate.X = 0
                obj.ObjectTransform_Translate.Y = 0
                obj.MovementSpeed = 6 * Math.Pow(1.25,CurrentGameStats.GameLevel - 1)
                AddToCanvas(obj)
            End If
        Next

        EntityPlayerObject.ObjectControl.Visibility = Visibility.Visible
        AddToCanvas(EntityPlayerObject)
        GameTimer.Start()

    End Sub

    ''' <summary>
    ''' ToDO Write RaiseLevel summary
    ''' </summary>
    Private Shared Sub RaiseLevel()
        CurrentGameStats.GameLevel += 1
        CurrentGameStats.LvlX5 = Math.DivRem(CurrentGameStats.GameLevel,5,CurrentGameStats.LvlX1)
        For i As Integer = 1 To CurrentGameStats.LvlX1
            MainWindowInstance.FindName("ImageLevel"+i.ToString()).Visibility = Visibility.Visible
        Next
        For j As Integer = CurrentGameStats.LvlX1+1 To 5
            MainWindowInstance.FindName("ImageLevel"+j.ToString()).Visibility = Visibility.Hidden
        Next
    End Sub

    ''' <summary>
    ''' ToDo Write MoveEnemiesDown summary
    ''' </summary>
    Public Shared Sub MoveEnemiesDown()
        For Each obj As EntityClasses.EntityEnemyBase in EnemyArray
            If Not IsNothing(obj) Then
                If obj.ObjectTransform_Translate.Y + (30 * Math.Pow(1.25,CurrentGameStats.GameLevel-1)) >= obj.TranslateBoundBottom * -1 Then
                    RaiseCollisionHit(Application.EntityPlayerObject,obj)
                    Else 
                        obj.MoveDown(30 * Math.Pow(1.25,CurrentGameStats.GameLevel-1))
                End If
                
            End If
        Next
    End Sub

    ''' <summary>
    ''' ToDo Write SubtractLife summary
    ''' </summary>
    Private Shared Sub SubtractLife()
        CurrentGameStats.GameLives -= 1
        MainWindowInstance.FindName("Life"+CurrentGameStats.GameLives.ToString()).Visibility = Visibility.Hidden
    End Sub

    ''' <summary>
    ''' ToDo Write AddLife summary
    ''' </summary>
    Private Shared Sub AddLife()
        If CurrentGameStats.GameLives <= MySettings.Default.MaxLives Then
            MainWindowInstance.FindName("Life"+CurrentGameStats.GameLives.ToString()).Visibility = Visibility.Visible
            CurrentGameStats.GameLives += 1
        End If
    End Sub

    ''' <summary>
    ''' ToDo Write GameOver Summary
    ''' </summary>
    Private Shared Sub GameOver()
        GameStatus = 2
        GameTimer.StopTimers()
        MainWindowInstance.LabelCurrentScore.Visibility = Visibility.Hidden
        CurrentGameStats.GameScore = 0
        For Each control As UIElement In MainWindowInstance.GridPlayerLifeIndicator.Children
            control.Visibility=Visibility.Hidden
        Next
        For Each control As UIElement In MainWindowInstance.LevelIndicators.Children
            control.Visibility=Visibility.Hidden
        Next
        MainWindowInstance.CanvasGameOver.Visibility = Visibility.Visible
    End Sub


#End Region

End Class
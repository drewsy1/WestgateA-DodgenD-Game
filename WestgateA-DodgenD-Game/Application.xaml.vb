Imports System.Collections.ObjectModel
Imports WestgateA_DodgenD_Game.Classes
Imports WestgateA_DodgenD_Game.Classes.Entities
Imports WestgateA_DodgenD_Game.Classes.Projectile
' ReSharper disable VBPossibleMistakenCallToGetType.2
Public Class Application
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
    ''' ToDo Write ReleaseFireButton summary
    ''' </summary>
    Public Shared Event ReleaseFireButton()
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
    ''' ToDo Write RaiseReleaseFireButton summary
    ''' </summary>
    Friend Shared Sub RaiseReleaseFireButton()
        RaiseEvent ReleaseFireButton()
    End Sub
#End Region

#Region "Event Methods"

    ''' <summary>
    ''' ToDo Write ProcessProjectileHit summary
    ''' </summary>
    ''' <param name="projectile"></param>
    ''' <param name="entity"></param>
    Public Shared Sub ProcessProjectileHit(ByRef projectile As ProjectileClasses.ProjectileBase, ByRef entity As Object)
        UpdateScore(entity)
        projectile.Remove()
        entity.Remove()
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
                MainWindowInstance.LabelCurrentHighScore.Content = My.Settings.HighScore
            End If
            MainWindowInstance.LabelCurrentScore.Content = CurrentGameStats.GameScore
        End If
    End Sub

#End Region

#Region "Shared Methods"

    ''' <summary>
    ''' Adds a control to canvas
    ''' </summary>
    ''' <param name="localControl">Object representing control</param>
    Public Shared Sub AddToCanvas(localControl)
        If (localControl.GetType().IsSubclassOf(GetType(CanvasObjects).BaseType)) Then
            CanvasGameScreen.Children.Add(localControl.ObjectControl)
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

    Public Shared Sub DefaultEnemyMovement


    End Sub


#End Region

End Class
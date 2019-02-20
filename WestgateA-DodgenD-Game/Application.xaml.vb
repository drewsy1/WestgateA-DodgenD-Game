﻿Imports System.Collections.ObjectModel
Imports WestgateA_DodgenD_Game.Classes
Imports WestgateA_DodgenD_Game.Classes.Entities
Imports WestgateA_DodgenD_Game.Classes.Projectile

Class Application
    ''' <summary>
    ''' TODO Write CanvasGameScreen summary
    ''' </summary>
    Public Shared CanvasGameScreen As Canvas

    ''' <summary>
    ''' Height of canvas in pixels
    ''' </summary>
    Public Const CanvasHeight As Double = 768

    ''' <summary>
    ''' TODO Write GameScore summary
    ''' </summary>
    Public Shared GameScore As Integer

    ''' <summary>
    ''' TODO Write GameLevel summary
    ''' </summary>
    Public Shared GameLevel As Integer

    ''' <summary>
    ''' Gets the MainWindow as an object and provides it as a static object
    ''' </summary>
    Public Shared MainWindowInstance As MainWindow

    ''' <summary>
    ''' Variable for current EntityPlayer object
    ''' </summary>
    Public Shared EntityPlayerObject As EntityClasses.EntityPlayer

    ''' <summary>
    ''' Width of canvas in pixels
    ''' </summary>
    Public Const CanvasWidth As Double = 672

    ''' <summary>
    ''' TODO Write GameLives summary
    ''' </summary>
    Public Shared GameLives As Integer

    ''' <summary>
    ''' TODO Write ObjectCollection summary
    ''' </summary>
    Public Shared ReadOnly EnemyCollection As ObservableCollection(Of EntityClasses.EntityEnemyBase) =
                               New ObservableCollection(Of EntityClasses.EntityEnemyBase)()

#Region "Events"
    ''' <summary>
    ''' ToDo Write PressFireButton summary
    ''' </summary>
    Public Shared Event PressFireButton()

    ''' <summary>
    ''' ToDo Write PlayerProjectileRemove summary
    ''' </summary>
    ''' <param name="projectile"></param>
    Public Shared Event PlayerProjectileRemove(projectile As ProjectileClasses.ProjectilePlayer)

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

    ''' <summary>
    ''' ToDo Write EnemyHit summary
    ''' </summary>
    ''' <param name="enemy"></param>
    Public Shared Event EnemyHit(enemy As EntityClasses.EntityEnemyBase)
    #End Region

    #Region "Event Friend Functions"
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

    ''' <summary>
    ''' ToDo Write RaisePressFireButton summary
    ''' </summary>
    Friend Shared Sub RaisePressFireButton()
        RaiseEvent PressFireButton()
    End Sub

    ''' <summary>
    ''' ToDo Write RaisePlayerProjectileRemove Summary
    ''' </summary>
    ''' <param name="projectile"></param>
    Friend Shared Sub RaisePlayerProjectileRemove(projectile As ProjectileClasses.ProjectilePlayer)
        RaiseEvent PlayerProjectileRemove(projectile)
    End Sub

    ''' <summary>
    ''' ToDo Write RaiseEnemyHit summary
    ''' </summary>
    ''' <param name="enemy"></param>
    Friend Shared Sub RaiseEnemyHit(enemy As EntityClasses.EntityEnemyBase)
        RaiseEvent EnemyHit(enemy)
    End Sub
    #End Region



    ''' <summary>
    ''' ToDo Write ProcessProjectileHit summary
    ''' </summary>
    ''' <param name="projectile"></param>
    ''' <param name="entity"></param>
    Public Shared Sub ProcessProjectileHit(ByRef projectile As ProjectileClasses.ProjectileBase, ByRef entity As Object)
        projectile.Remove()
        entity.Remove()
    End Sub



    #Region "Shared Methods"
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
    ''' Adds a control to canvas
    ''' </summary>
    ''' <param name="localControl">Object representing control</param>
    Public Shared Sub AddToCanvas(localControl)
        If (localControl.GetType().IsSubclassOf(GetType(CanvasObjects).BaseType)) Then
            CanvasGameScreen.Children.Add(localControl.ObjectControl)
        Else

        End If
    End Sub
    #End Region
End Class

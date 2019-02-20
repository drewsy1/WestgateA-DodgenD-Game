﻿Imports WestgateA_DodgenD_Game.Classes.Entities
Imports WestgateA_DodgenD_Game.Enums

Namespace Classes.Projectile
    ' ReSharper disable once ClassNeverInstantiated.Global
    Partial Public Class ProjectileClasses
        ''' <summary>
        ''' Defines properties/methods of a player-fired projectile
        ''' </summary>
        Public Class ProjectilePlayer
            Inherits ProjectileBase

            ''' <summary>
            ''' Direction of projectile
            ''' </summary>
            ''' <returns>ProjectileDirection</returns>
            Protected Overrides ReadOnly Property ProjectileDirection As Double = EProjectileDirection.Up

            ''' <summary>
            ''' Color of ProjectilePlayer projectile
            ''' </summary>
            Protected Overrides Property ProjectileColor As Color = Colors.LawnGreen

            Protected Overrides ReadOnly Property LocationCoordsDefault As Point
                Get
                    Return New Point(Application.CanvasWidth / 2, Application.CanvasHeight)
                End Get
            End Property

            Public Event PlayerProjectileRemove(projectile As ProjectilePlayer)

            ''' <summary>
            ''' Instantiates a ProjectilePlayer object by calling Projectile.New() and
            ''' sets its color to ProjectileColor
            ''' </summary>
            ''' <param name="translateX">X-axis translation (x coordinate +/- pixels)</param>
            ''' <param name="translateY">Y-axis translation (y coordinate +/- pixels)</param>
            ''' <param name="localLocation">Object's starting coordinate</param>
            Sub New(translateX As Double,
                    translateY As Double,
                    Optional localLocation As Point = Nothing)
                MyBase.New(translateX, translateY, localLocation)

                SetColor(ProjectileColor)
                AddHandler EntityClasses.EntityEnemyBase.EnemyHit, AddressOf Remove
            End Sub

            Overrides Sub Remove()
                RemoveHandler EntityClasses.EntityEnemyBase.EnemyHit, AddressOf Remove
                MyBase.Remove()
                RaiseEvent PlayerProjectileRemove(Me)
            End Sub
        End Class
    End Class
End Namespace
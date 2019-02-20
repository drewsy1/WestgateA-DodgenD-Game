Imports WestgateA_DodgenD_Game.Enums

Namespace Classes.Projectile
    ' ReSharper disable once ClassNeverInstantiated.Global
    Partial Public Class ProjectileClasses
        ''' <summary>
        ''' Defines properties/methods of an enemy-fired projectile
        ''' </summary>
        Public Class ProjectileEnemy
            Inherits ProjectileBase

            ''' <summary>
            ''' Sets direction of projectile
            ''' </summary>
            ''' <returns>ProjectileDirection</returns>
            Protected Overrides ReadOnly Property ProjectileDirection As Double = EProjectileDirection.Down

            ''' <summary>
            ''' Color of ProjectileEnemy projectile
            ''' </summary>
            Protected Overrides Property ProjectileColor As Color = Colors.Red

            Protected Overrides ReadOnly Property LocationCoordsDefault As Point
                Get
                    Return New Point(Application.CanvasWidth / 2, 76.5)
                End Get
            End Property

            Public Event EnemyProjectileRemove(projectile As ProjectileEnemy)

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
                'AddHandler EnemyProjectileRemove, AddressOf parent.RemovePlayerProjectileInstance
            End Sub

            Overrides Sub Remove()
                MyBase.Remove()
                RaiseEvent EnemyProjectileRemove(Me)
            End Sub
        End Class
    End Class
End Namespace
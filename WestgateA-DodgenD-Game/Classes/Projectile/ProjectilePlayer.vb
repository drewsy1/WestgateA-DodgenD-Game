Imports WestgateA_DodgenD_Game.Classes.Entities
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
            Protected Overrides Property ProjectileDirection As Double
                Get
                    Return EProjectileDirection.Up
                End Get
                Set(value As Double)

                End Set
            End Property

            ''' <summary>
            ''' Color of ProjectilePlayer projectile
            ''' </summary>
            Protected Overrides Property ProjectileColor As Color = Colors.LawnGreen

            ''' <summary>
            ''' Default starting X-coordinate location for ProjectilePlayer
            ''' </summary>
            Protected Overrides ReadOnly Property LocationXDefault As Double
                Get
                    Return MainWindowWrapper.CanvasWidth / 2
                End Get
            End Property


            ''' <summary>
            ''' Default starting Y-coordinate location for ProjectilePlayer
            ''' </summary>
            Protected Overrides ReadOnly Property LocationYDefault As Double
                Get
                    Return 76.5
                End Get
            End Property

            Public Event PlayerProjectileRemove(projectile As ProjectilePlayer)

            ''' <summary>
            ''' Instantiates a ProjectilePlayer object by calling Projectile.New() and
            ''' sets its color to ProjectileColor
            ''' </summary>
            ''' <param name="translateX">X-axis translation (x coordinate +/- pixels)</param>
            ''' <param name="localLocationX">Object's starting X-coordinate</param>
            ''' <param name="localLocationY">Object's starting Y-coordinate</param>
            Sub New(translateX As Double,
                    translateY As Double,
                    Optional localLocationX As Double = Nothing,
                    Optional localLocationY As Double = Nothing)
                MyBase.New(translateX, translateY, localLocationX, localLocationY)

                SetColor(ProjectileColor)
                AddHandler EntityClasses.EntityEnemyBase.EnemyHit, AddressOf Remove
            End Sub

            Overrides Sub Remove()
                MyBase.Remove()
                RaiseEvent PlayerProjectileRemove(Me)
            End Sub
        End Class
    End Class
End Namespace
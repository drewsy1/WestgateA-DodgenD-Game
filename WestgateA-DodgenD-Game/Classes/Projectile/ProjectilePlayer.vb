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
            Protected Overrides Property ProjectileDirection As Double = EProjectileDirection.Up

            ''' <summary>
            ''' Color of ProjectilePlayer projectile
            ''' </summary>
            Protected Overrides Property ProjectileColor As Color = Colors.LawnGreen

            ''' <summary>
            ''' Default starting X-coordinate location for ProjectilePlayer
            ''' </summary>
            Private Const LocationXDefault As Double = MainWindowWrapper.CanvasWidth / 2

            ''' <summary>
            ''' Default starting Y-coordinate location for ProjectilePlayer
            ''' </summary>
            Private Const LocationYDefault As Double = 76.5

            ''' <summary>
            ''' Instantiates a ProjectilePlayer object by calling Projectile.New() and
            ''' sets its color to ProjectileColor
            ''' </summary>
            ''' <param name="translateX">X-axis translation (x coordinate +/- pixels)</param>
            ''' <param name="locationX">Object's starting X-coordinate</param>
            ''' <param name="locationY">Object's starting Y-coordinate</param>
            Sub New(translateX As Double,
                    Optional locationX As Double = LocationXDefault,
                    Optional locationY As Double = LocationYDefault)
                MyBase.New(translateX, locationX, locationY)

                SetColor(ProjectileColor)
            End Sub
        End Class
    End Class
End Namespace
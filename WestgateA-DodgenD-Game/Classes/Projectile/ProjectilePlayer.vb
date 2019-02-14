Namespace Classes.Projectile
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
            Shared Shadows ReadOnly ProjectileColor As Color = Colors.LawnGreen

            ''' <summary>
            ''' Default starting X-coordinate location for ProjectilePlayer
            ''' </summary>
            Private Const LocationXDefault As Double = 316.5

            ''' <summary>
            ''' Default starting Y-coordinate location for ProjectilePlayer
            ''' </summary>
            Private Const LocationYDefault As Double = 132

            ''' <summary>
            ''' Instantiates a ProjectilePlayer object by calling Projectile.New() and
            ''' sets its color to ProjectileColor
            ''' </summary>
            Sub New()
                MyBase.New()
                SetColor(ProjectileColor)
            End Sub

            ''' <summary>
            ''' Sets location/transform for projectile and adds it to canvas by calling
            ''' base class' AddToCanvas()
            ''' </summary>
            ''' <param name="translateX">X-axis translation (x coordinate +/- pixels)</param>
            ''' <param name="locationX">Object's starting X-coordinate</param>
            ''' <param name="locationY">Object's starting Y-coordinate</param>
            Shadows Sub AddToCanvas(translateX As Double,
                            Optional locationX As Double = LocationXDefault,
                            Optional locationY As Double = LocationYDefault)
                MyBase.AddToCanvas(translateX, locationX, locationY)
            End Sub
        End Class
    End Class
End Namespace
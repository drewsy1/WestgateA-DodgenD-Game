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
            Protected Overrides Property ProjectileDirection As Double = EProjectileDirection.Down

            ''' <summary>
            ''' Color of ProjectileEnemy projectile
            ''' </summary>
            Shared Shadows ReadOnly ProjectileColor As Color = Colors.Red

            ''' <summary>
            ''' Default starting X-coordinate location for ProjectileEnemy
            ''' </summary>
            Private Const TransformXDefault As Double = 316.5

            ''' <summary>
            ''' Default starting Y-coordinate location for ProjectileEnemy
            ''' </summary>
            Private Const TransformYDefault As Double = 768

            ''' <summary>
            ''' Instantiates a ProjectileEnemy object by calling Projectile.New() and
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
            Friend Shadows Sub AddToCanvas(translateX As Double,
                                    Optional locationX As Double = TransformXDefault,
                                    Optional locationY As Double = TransformYDefault)
                MyBase.AddToCanvas(translateX, locationX, locationY)
            End Sub
        End Class
    End Class
End Namespace
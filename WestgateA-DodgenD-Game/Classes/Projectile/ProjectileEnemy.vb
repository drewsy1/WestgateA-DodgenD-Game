﻿Namespace Classes.Projectile
    Partial Public Class ProjectileClasses
        ''' <summary>
        ''' Defines properties/methods of an enemy-fired projectile
        ''' </summary>
        Public Class ProjectileEnemy
            Inherits ProjectileClasses.ProjectileBase

            ' Sets direction of projectile
            Protected Overrides Property ProjectileDirection As Double = EProjectileDirection.Down

            ' Color of ProjectileEnemy projectile
            Shared Shadows ReadOnly ProjectileColor As Color = Colors.Red

            ' Default starting X-coordinate location for ProjectileEnemy
            Private Const TransformXDefault As Double = 316.5

            ' Default starting Y-coordinate location for ProjectileEnemy
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
            Shadows Sub AddToCanvas(translateX As Double,
                                    Optional locationX As Double = TransformXDefault,
                                    Optional locationY As Double = TransformYDefault)
                MyBase.AddToCanvas(translateX, locationX, locationY)
            End Sub
        End Class
    End Class
End Namespace
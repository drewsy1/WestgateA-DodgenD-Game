Imports WestgateA_DodgenD_Game.Classes.Projectile

Namespace Classes.Entities
    ' ReSharper disable once ClassNeverInstantiated.Global
    ''' <summary>
    ''' Class containing Projectile classes and meta-properties
    ''' </summary>
    Partial Public Class EntityClasses
        Public Class EntityEnemy
            Inherits EntityBase
            ''' <summary>
            ''' Default PlayerCursor movement speed
            ''' </summary>
            Protected Overrides Property MovementSpeed As Double = 5

            ''' <summary>
            ''' ProjectilePlayer object for weapon projectile
            ''' </summary>
            Private _enemyProjectileInstance As ProjectileClasses.ProjectileEnemy

#Region "Inherited properties"
            ''' <summary>
            ''' Default player cursor height in pixels
            ''' </summary>
            ''' <returns></returns>
            Protected Overrides Property ObjectHeight As Double = 33

            ''' <summary>
            ''' Default player cursor width in pixels
            ''' </summary>
            ''' <returns></returns>
            Protected Overrides Property ObjectWidth As Double = 33

            ''' <summary>
            ''' Default starting X-coordinate location for PlayerCursor
            ''' </summary>
            ''' <returns></returns>
            Protected Overrides ReadOnly Property LocationXDefault As Double
                Get
                    Return MainWindowWrapper.CanvasWidth / 2
                End Get
            End Property

            ''' <summary>
            ''' Default starting Y-coordinate location for PlayerCursor
            ''' </summary>
            ''' <returns></returns>
            Protected Overrides ReadOnly Property LocationYDefault As Double
                Get
                    Return MainWindowWrapper.CanvasHeight - 106
                End Get
            End Property

            ''' <summary>
            ''' Leftmost x-value of PlayerCursor
            ''' </summary>
            ''' <returns></returns>
            Protected Overrides Property TranslateBoundLeft As Double = GetTranslateBoundLeft()

            ''' <summary>
            ''' Rightmost x-value of PlayerCursor
            ''' </summary>
            ''' <returns></returns>
            Protected Overrides Property TranslateBoundRight As Double = GetTranslateBoundRight()

            ''' <summary>
            ''' Translate transform object for PlayerCursor
            ''' </summary>
            Protected Overrides Property ObjectTransformTranslate As TranslateTransform = New TranslateTransform() With {.X = 0, .Y = 0}

            ''' <summary>
            ''' TransformGroup containing Translate transform to be added to PlayerCursor instance
            ''' </summary>
            Protected Overrides Property ObjectTransformGroup As TransformGroup =
                New TransformGroup() With {
                    .Children = New TransformCollection(
                        New Transform() {ObjectTransformTranslate})
                }

            ''' <summary>
            ''' Image control that serves as PlayerCursor
            ''' </summary>
            Public Overrides Property ObjectControl As Object

            Protected Shadows WithEvents ObjectHitbox As Hitbox
#End Region

            ''' <summary>
            ''' Instantiates a new EntityPlayer object, creates its hitbox, and adds it to ObjectCollection
            ''' </summary>
            Protected Sub New()
                MyBase.New()
            End Sub

            ''' <summary>
            ''' Creates a player projectile that moves upward
            ''' </summary>
            Sub FireWeapon()
                ' If no player projectile currently exists, fire a new one
                If Not ProjectileClasses.ProjectilesCollection.Contains(
                    _enemyProjectileInstance) Then
                    _enemyProjectileInstance =
                        New ProjectileClasses.ProjectileEnemy(
                            (ObjectWidth / 2),
                            0 - ObjectHeight,
                            (LocationX + ObjectTransformTranslate.X),
                            (LocationY)
                            )
                    MainWindowWrapper.AddToCanvas(_enemyProjectileInstance)
                End If
            End Sub
        End Class
    End Class
End Namespace
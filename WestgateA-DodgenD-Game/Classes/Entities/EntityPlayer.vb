Imports WestgateA_DodgenD_Game.Classes.Projectile

Namespace Classes.Entities
    ' ReSharper disable once ClassNeverInstantiated.Global
    ''' <summary>
    ''' Class containing Projectile classes and meta-properties
    ''' </summary>
    Partial Public Class EntityClasses
        Public Class EntityPlayer
            Inherits EntityBase
            ''' <summary>
            ''' Default PlayerCursor movement speed
            ''' </summary>
            Protected Overrides Property MovementSpeed As Double = 5

            ''' <summary>
            ''' Bitmap image object that will contain the PlayerCursor BMP
            ''' </summary>
            Private ReadOnly _playerCursorBitmapImage As BitmapImage = New BitmapImage()

            ''' <summary>
            ''' Default player cursor image source (in URI string format)
            ''' </summary>
            Private Const PlayerCursorImagePath As String = "pack://application:,,,/WestgateA-DodgenD-Game;component/Resources/PlayerCursor.png"

            ''' <summary>
            ''' ProjectilePlayer object for weapon projectile
            ''' </summary>
            Private _playerProjectileInstance As ProjectileClasses.ProjectilePlayer

#Region "Inherited properties"
            ''' <summary>
            ''' Default player cursor height in pixels
            ''' </summary>
            ''' <returns></returns>
            Protected Overrides Property ObjectHeight As Double = 57

            ''' <summary>
            ''' Default player cursor width in pixels
            ''' </summary>
            ''' <returns></returns>
            Protected Overrides Property ObjectWidth As Double = 39

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
                    Return 48
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
            Public Overrides Property ObjectControl As Object = New Image() With {
                .Name = "PlayerCursor",
                .Height = ObjectHeight,
                .Width = ObjectWidth,
                .RenderTransform = ObjectTransformGroup,
                .RenderTransformOrigin = New Point(0, 0),
                .Source = _playerCursorBitmapImage
                }

            Protected Shadows WithEvents ObjectHitbox As Hitbox
#End Region

            ''' <summary>
            ''' Instantiates a new EntityPlayer object, creates its hitbox, and adds it to ObjectCollection
            ''' </summary>
            Sub New()
                MyBase.New()
                _playerCursorBitmapImage.BeginInit()
                _playerCursorBitmapImage.UriSource = New Uri(
                    PlayerCursorImagePath,
                    UriKind.RelativeOrAbsolute
                    )
                _playerCursorBitmapImage.EndInit()

                MainWindowWrapper.SetCanvasLocation(
                    LocationXDefault,
                    LocationYDefault,
                    ObjectControl
                    )
            End Sub

            ''' <summary>
            ''' Creates a player projectile that moves upward
            ''' </summary>
            Sub FireWeapon()
                ' If no player projectile currently exists, fire a new one
                If Not ProjectileClasses.ProjectilesCollection.Contains(
                    _playerProjectileInstance) Then
                    _playerProjectileInstance =
                        New ProjectileClasses.ProjectilePlayer(
                            (ObjectWidth / 2),
                            0 - ObjectHeight,
                            (LocationX + ObjectTransformTranslate.X),
                            (LocationY)
                            )
                    MainWindowWrapper.AddToCanvas(_playerProjectileInstance)
                End If
            End Sub
        End Class
    End Class
End Namespace
Imports WestgateA_DodgenD_Game.Classes.Projectile

Namespace Classes.Entities
    ' ReSharper disable once ClassNeverInstantiated.Global
    ''' <summary>
    ''' Class containing Projectile classes and meta-properties
    ''' </summary>
    Partial Public Class EntityClasses
        Public Class Player
            Inherits EntityBase
            ''' <summary>
            ''' Default PlayerCursor movement speed
            ''' </summary>
            Private Const MovementSpeed As Double = 5

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
            Protected Overrides Property EntityHeight As Double = 57

            ''' <summary>
            ''' Default player cursor width in pixels
            ''' </summary>
            ''' <returns></returns>
            Protected Overrides Property EntityWidth As Double = 39

            ''' <summary>
            ''' Default starting X-coordinate location for PlayerCursor
            ''' </summary>
            ''' <returns></returns>
            Protected Overrides Property LocationXDefault As Double = MainWindowWrapper.CanvasWidth / 2

            ''' <summary>
            ''' Default starting Y-coordinate location for PlayerCursor
            ''' </summary>
            ''' <returns></returns>
            Protected Overrides Property LocationYDefault As Double = 48

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
            Protected Overrides Property EntityTransformTranslate As TranslateTransform = New TranslateTransform() With {.X = 0, .Y = 0}

            ''' <summary>
            ''' TransformGroup containing Translate transform to be added to PlayerCursor instance
            ''' </summary>
            Protected Overrides Property EntityTransformGroup As TransformGroup = New TransformGroup() With {
                .Children = New TransformCollection(New Transform() {EntityTransformTranslate})
                }

            ''' <summary>
            ''' Image control that serves as PlayerCursor
            ''' </summary>
            Protected Overrides Property EntityControl As Object = New Image() With {
                .Name = "PlayerCursor",
                .Height = EntityHeight,
                .Width = EntityWidth,
                .RenderTransform = EntityTransformGroup,
                .RenderTransformOrigin = New Point(0, 0),
                .Source = _playerCursorBitmapImage
                }

            Private WithEvents _entityHitbox As Hitbox
#End Region

            ''' <summary>
            ''' Instantiates a new Player object, creates its hitbox, and adds it to EntitiesCollection
            ''' </summary>
            Sub New()
                _playerCursorBitmapImage.BeginInit()
                _playerCursorBitmapImage.UriSource = New Uri(PlayerCursorImagePath, UriKind.RelativeOrAbsolute)
                _playerCursorBitmapImage.EndInit()

                EntitiesCollection.Add(Me)

                _entityHitbox = New Hitbox(EntityWidth,
                                           EntityHeight,
                                           LocationXDefault,
                                           LocationYDefault)

                AddHandler _entityHitbox.LeavingCanvas, AddressOf MyBase.Remove
            End Sub


            ''' <summary>
            ''' Sets location/transform for player and adds it to canvas
            ''' </summary>
            Overloads Sub AddToCanvas()
                AddToCanvas(LocationXDefault, LocationYDefault, EntityControl)
            End Sub

            ''' <summary>
            ''' Creates a player projectile that moves upward
            ''' </summary>
            Sub FireWeapon()
                ' If no player projectile currently exists, fire a new one
                If Not ProjectileClasses.ProjectilesCollection.Contains(_playerProjectileInstance) Then
                    _playerProjectileInstance = New ProjectileClasses.ProjectilePlayer(EntityTransformTranslate.X + (EntityWidth / 2))
                    _playerProjectileInstance.AddToCanvas()
                End If
            End Sub

            ''' <summary>
            ''' Moves player cursor left if player is within bounds
            ''' </summary>
            ''' <param name="localMovementSpeed">Number of pixels to move left (defaults to MovementSpeed)</param>
            Overloads Sub MoveLeft(Optional localMovementSpeed As Double = MovementSpeed)
                MoveLeft(localMovementSpeed, EntityTransformTranslate)
            End Sub

            ''' <summary>
            ''' Moves player cursor right if player is within bounds
            ''' </summary>
            ''' <param name="localMovementSpeed">Number of pixels to move right (defaults to MovementSpeed)</param>
            Overloads Sub MoveRight(Optional localMovementSpeed As Double = MovementSpeed)
                MoveRight(localMovementSpeed, EntityTransformTranslate)
            End Sub
        End Class
    End Class
End Namespace
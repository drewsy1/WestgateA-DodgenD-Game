Namespace Classes
    Public Class Player
        ''' <summary>
        ''' Default player cursor height in pixels
        ''' </summary>
        Private Const PlayerCursorHeight As Double = 57

        ''' <summary>
        ''' Default player cursor width in pixels
        ''' </summary>
        Private Const PlayerCursorWidth As Double = 39

        ''' <summary>
        ''' Default player cursor image source (in URI string format)
        ''' </summary>
        Private Const PlayerCursorImagePath As String = 
            "pack://application:,,,/WestgateA-DodgenD-Game;component/Resources/PlayerCursor.bmp"

        ''' <summary>
        ''' Default starting X-coordinate location for PlayerCursor
        ''' </summary>
        Private Const LocationXDefault As Double = 316.5
        
        ''' <summary>
        ''' Default starting Y-coordinate location for PlayerCursor
        ''' </summary>
        Private Const LocationYDefault As Double = 48

        ''' <summary>
        ''' Default PlayerCursor movement speed
        ''' </summary>
        Private Const MovementSpeed As Double = 5

        ''' <summary>
        ''' Minimum X-value allowed for translation
        ''' </summary>
        Private Const TranslateBoundLeft As Double = -310

        ''' <summary>
        ''' Maximum X-value allowed for translation
        ''' </summary>
        Private Const TranslateBoundRight As Double = 310

        ''' <summary>
        ''' Bitmap image object that will contain the PlayerCursor BMP
        ''' </summary>
        Private ReadOnly _playerCursorBitmapImage As BitmapImage = New BitmapImage()
   
        ''' <summary>
        ''' Translate transform object for PlayerCursor
        ''' </summary>
        Private ReadOnly _
            _playerCursorBitmapImageTransformTranslate As TranslateTransform = 
                New TranslateTransform() With {.X = 0, .Y = 0}

        ''' <summary>
        ''' TransformGroup containing Translate transform to be added to PlayerCursor instance
        ''' </summary>
        Private ReadOnly _playerCursorBitmapImageTransform As TransformGroup = 
            New TransformGroup() With{
                .Children = New TransformCollection(
                    New Transform() {_playerCursorBitmapImageTransformTranslate}
                )
            }
        ''' <summary>
        ''' ProjectilePlayer object for weapon projectile
        ''' </summary>
        Private _playerProjectileInstance As Projectile.ProjectileClasses.ProjectilePlayer

        ''' <summary>
        ''' Image that serves as PlayerCursor
        ''' </summary>
        Private ReadOnly _playerCursorInstance As Image =  New Image() With {
            .Name = "PlayerCursor",
            .Height = PlayerCursorHeight,
            .Width = PlayerCursorWidth,
            .RenderTransform = _playerCursorBitmapImageTransform,
            .RenderTransformOrigin = New Point(0.5, 0.5),
            .Source = _playerCursorBitmapImage
        }

        ''' <summary>
        ''' Instantiates a new PlayerCursor object
        ''' </summary>
        Sub New()
            _playerCursorBitmapImage.BeginInit()
            _playerCursorBitmapImage.UriSource = New Uri(PlayerCursorImagePath, UriKind.RelativeOrAbsolute)
            _playerCursorBitmapImage.EndInit()
        End Sub

        ''' <summary>
        ''' Sets location/transform for projectile and adds it to canvas
        ''' </summary>
        ''' <param name="locationX">Object's starting X-coordinate</param>
        ''' <param name="locationY">Object's starting Y-coordinate</param>
        Sub AddToCanvas(Optional locationX As Double = LocationXDefault,
                                  Optional locationY As Double = LocationYDefault)
            Canvas.SetLeft(_playerCursorInstance, locationX)
            Canvas.SetBottom(_playerCursorInstance, locationY)
            MainWindowWrapper.MainWindowInstance.CanvasGameScreen.Children.Add(_playerCursorInstance)
        End Sub

        ''' <summary>
        ''' Creates a player projectile that moves upward
        ''' </summary>
        Sub FireWeapon()
            ' If no player projectile currently exists, fire a new one
            If Not Projectile.ProjectileClasses.ProjectilesCollection.Contains(_playerProjectileInstance) Then
                _playerProjectileInstance = New Projectile.ProjectileClasses.ProjectilePlayer()
                _playerProjectileInstance.AddToCanvas(_playerCursorBitmapImageTransformTranslate.X + 17.5)
            End If
        End Sub

        ''' <summary>
        ''' Moves player cursor left if player is within bounds
        ''' </summary>
        ''' <param name="localMovementSpeed">Number of pixels to move left (defaults to MovementSpeed)</param>
        Sub MoveLeft(Optional localMovementSpeed As Double = MovementSpeed)
            If (_playerCursorBitmapImageTransformTranslate.X > TranslateBoundLeft) Then
                _playerCursorBitmapImageTransformTranslate.X -= localMovementSpeed
            End If
        End Sub

        ''' <summary>
        ''' Moves player cursor right if player is within bounds
        ''' </summary>
        ''' <param name="localMovementSpeed">Number of pixels to move right (defaults to MovementSpeed)</param>
        Sub MoveRight(Optional localMovementSpeed As Double = MovementSpeed)
            If (_playerCursorBitmapImageTransformTranslate.X < TranslateBoundRight) Then
                _playerCursorBitmapImageTransformTranslate.X += localMovementSpeed
            End If
        End Sub

    End Class
End Namespace
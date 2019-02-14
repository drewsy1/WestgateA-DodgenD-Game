Namespace Classes
    Public Class Player
        Private Const PlayerCursorHeight As Double = 57
        Private Const PlayerCursorWidth As Double = 39
        Private Const PlayerCursorImagePath As String = "pack://application:,,,/WestgateA-DodgenD-Game;component/Resources/PlayerCursor.bmp"

        Private ReadOnly _playerCursorRenderTransformOrigin = New Point(0.5, 0.5)
        Private ReadOnly _playerCursorBitmapImageUri As Uri = New Uri(PlayerCursorImagePath, UriKind.RelativeOrAbsolute)
        Private ReadOnly _playerCursorBitmapImage As BitmapImage = New BitmapImage()
        Private ReadOnly _playerCursorBitmapImageTransformTranslate As TranslateTransform = New TranslateTransform() With {.X = 0, .Y = 0}
        Private ReadOnly _playerCursorBitmapImageTransform As TransformGroup = New TransformGroup()

        Private Const PlayerCursorDefaultLeft As Double = 316.5
        Private Const PlayerCursorDefaultTop As Double = 663

        Private _playerProjectileInstance As ProjectileClasses.ProjectilePlayer
        Private ReadOnly _playerCursorInstance As Image

        ''' <summary>
        ''' 
        ''' </summary>
        Sub New()
            _playerCursorBitmapImageTransform.Children.Add(_playerCursorBitmapImageTransformTranslate)

            _playerCursorBitmapImage.BeginInit()
            _playerCursorBitmapImage.UriSource = _playerCursorBitmapImageUri
            _playerCursorBitmapImage.EndInit()

            _playerCursorInstance = New Image() With {
                .Name = "PlayerCursor",
                .Height = PlayerCursorHeight,
                .Width = PlayerCursorWidth,
                .RenderTransform = _playerCursorBitmapImageTransform,
                .RenderTransformOrigin = _playerCursorRenderTransformOrigin,
                .Source = _playerCursorBitmapImage
            }
            _playerCursorBitmapImageTransformTranslate.SetValue(FrameworkElement.NameProperty, "PlayerCursorTransform")
            Canvas.SetLeft(_playerCursorInstance, PlayerCursorDefaultLeft)
            Canvas.SetTop(_playerCursorInstance, PlayerCursorDefaultTop)
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        Sub AddToCanvas()
            Dim mw As MainWindow = Application.Current.MainWindow
            mw.CanvasGameScreen.Children.Add(_playerCursorInstance)
        End Sub

        ''' <summary>
        ''' Creates a player projectile that moves upward
        ''' </summary>
        Sub FireWeapon()
            ' If no player projectile currently exists, fire a new one
            If Not ProjectileClasses.ProjectilesCollection.Contains(_playerProjectileInstance) Then
                _playerProjectileInstance = New ProjectileClasses.ProjectilePlayer()
                _playerProjectileInstance.AddToCanvas(_playerCursorBitmapImageTransformTranslate.X + 17.5)
            End If
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        Sub MoveLeft()
            If (_playerCursorBitmapImageTransformTranslate.X > -310) Then _playerCursorBitmapImageTransformTranslate.X -= 10
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        Sub MoveRight()
            If (_playerCursorBitmapImageTransformTranslate.X < 310) Then _playerCursorBitmapImageTransformTranslate.X += 10
        End Sub

    End Class
End Namespace
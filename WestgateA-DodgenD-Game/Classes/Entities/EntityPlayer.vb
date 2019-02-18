Imports WestgateA_DodgenD_Game.Classes.Projectile
Imports WestgateA_DodgenD_Game.Interfaces

Namespace Classes.Entities
    ' ReSharper disable once ClassNeverInstantiated.Global
    ''' <summary>
    ''' Class containing Projectile classes and meta-properties
    ''' </summary>
    Partial Public Class EntityClasses
        Public Class EntityPlayer
            Implements ICanvasObjects
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
            Public Shared PlayerProjectileInstance As ProjectileClasses.ProjectilePlayer

#Region "Inherited properties"
            ''' <summary>
            ''' Default player cursor height in pixels
            ''' </summary>
            ''' <returns></returns>
            Protected Property ObjectHeight As Double = 57 Implements ICanvasObjects.ObjectHeight

            ''' <summary>
            ''' Default player cursor width in pixels
            ''' </summary>
            ''' <returns></returns>
            Protected Shadows Property ObjectWidth As Double = 39 Implements ICanvasObjects.ObjectWidth

            ''' <summary>
            ''' Default starting X-coordinate location for PlayerCursor
            ''' </summary>
            ''' <returns></returns>
            Protected ReadOnly Property LocationXDefault As Double Implements ICanvasObjects.LocationXDefault
                Get
                    Return MainWindowWrapper.CanvasWidth / 2
                End Get
            End Property

            ''' <summary>
            ''' Default starting Y-coordinate location for PlayerCursor
            ''' </summary>
            ''' <returns></returns>
            Protected ReadOnly Property LocationYDefault As Double Implements ICanvasObjects.LocationYDefault
                Get
                    Return 48
                End Get
            End Property

            Protected Property LocationX As Double = LocationXDefault Implements ICanvasObjects.LocationX

            Protected Property LocationY As Double = LocationYDefault Implements ICanvasObjects.LocationY

            ''' <summary>
            ''' Leftmost x-value of PlayerCursor
            ''' </summary>
            ''' <returns></returns>
            Protected Property TranslateBoundLeft As Double = CanvasObjects.GetTranslateBoundLeft(LocationXDefault, ObjectWidth) Implements ICanvasObjects.TranslateBoundLeft

            ''' <summary>
            ''' Rightmost x-value of PlayerCursor
            ''' </summary>
            ''' <returns></returns>
            Protected Property TranslateBoundRight As Double = CanvasObjects.GetTranslateBoundRight(LocationXDefault, ObjectWidth) Implements ICanvasObjects.TranslateBoundRight

            Protected Property TranslateBoundTop As Double Implements ICanvasObjects.TranslateBoundTop

            Protected Property TranslateBoundBottom As Double Implements ICanvasObjects.TranslateBoundBottom

            Protected Property MovementSpeed As Double = 10 Implements ICanvasObjects.MovementSpeed

            Protected Property ObjectTransformTranslate As TranslateTransform = New TranslateTransform() With {.X = 0, .Y = 0} Implements ICanvasObjects.ObjectTransformTranslate

            Protected Property ObjectTransformGroup As TransformGroup =
                New TransformGroup() With {
                    .Children = New TransformCollection(
                        New Transform() {ObjectTransformTranslate})
                } Implements ICanvasObjects.ObjectTransformGroup

            Public Property ObjectControl As Object = New Image() With {
                .Name = "PlayerCursor",
                .Height = ObjectHeight,
                .Width = ObjectWidth,
                .RenderTransform = ObjectTransformGroup,
                .RenderTransformOrigin = New Point(0, 0),
                .Source = _playerCursorBitmapImage
                } Implements ICanvasObjects.ObjectControl

            ''' <summary>
            ''' Moves entity left if entity is within bounds
            ''' </summary>
            ''' <param name="localMovementSpeed">Number of pixels to move left (defaults to 0 unless MovementSpeed is set)</param>
            Public Sub MoveLeft(Optional localMovementSpeed As Double = 0) Implements ICanvasObjects.MoveLeft
                TranslateX(MovementSpeed * -1)
            End Sub

            ''' <summary>
            ''' Moves entity right if entity is within bounds
            ''' </summary>
            ''' <param name="localMovementSpeed">Number of pixels to move right (defaults to 0 unless MovementSpeed is set)</param>
            Public Sub MoveRight(Optional localMovementSpeed As Double = 0) Implements ICanvasObjects.MoveRight
                TranslateX(MovementSpeed)
            End Sub

            ''' <summary>
            ''' Moves entity up if entity is within bounds
            ''' </summary>
            ''' <param name="localMovementSpeed">Number of pixels to move up (defaults to 0 unless MovementSpeed is set)</param>
            Public Sub MoveUp(Optional localMovementSpeed As Double = 0) Implements ICanvasObjects.MoveUp
                If (localMovementSpeed.Equals(0) And MovementSpeed) Then
                    localMovementSpeed = MovementSpeed
                End If
                TranslateY(localMovementSpeed)
            End Sub

            ''' <summary>
            ''' Moves entity down if entity is within bounds
            ''' </summary>
            ''' <param name="localMovementSpeed">Number of pixels to move down (defaults to 0 unless MovementSpeed is set)</param>
            Public Sub MoveDown(Optional localMovementSpeed As Double = 0) Implements ICanvasObjects.MoveDown
                If (localMovementSpeed.Equals(0) And MovementSpeed) Then
                    localMovementSpeed = MovementSpeed
                End If
                TranslateY(localMovementSpeed * -1)
            End Sub

            Public Sub TranslateY(localMovementSpeed As Double) Implements ICanvasObjects.TranslateY
                Dim location As Double = ObjectTransformTranslate.Y - LocationY
                If (location <= TranslateBoundBottom And (localMovementSpeed < 0)) Or
                (location >= TranslateBoundTop And (localMovementSpeed > 0)) Then
                    ObjectTransformTranslate.Y += localMovementSpeed
                    If Not IsNothing(ObjectHitbox) Then
                        ObjectHitbox.MoveY(localMovementSpeed * -1)
                    End If
                End If
            End Sub

            Public Sub TranslateX(localMovementSpeed As Double) Implements ICanvasObjects.TranslateX
                If (ObjectTransformTranslate.X >= TranslateBoundLeft And (localMovementSpeed < 0)) Or
            (ObjectTransformTranslate.X <= TranslateBoundRight And (localMovementSpeed > 0)) Then
                    ObjectTransformTranslate.X += localMovementSpeed
                    ObjectHitbox.MoveX(localMovementSpeed * -1)
                End If
            End Sub

            Public Sub Remove() Implements ICanvasObjects.Remove
                ' Remove rectangle from CanvasGameScreen (make it invisible)
                MainWindowWrapper.MainWindowInstance.CanvasGameScreen.Children.Remove(
                    ObjectControl)

                Hitbox.HitboxCollection.Remove(ObjectHitbox)
                ObjectHitbox = Nothing

                Dim itemIndex As Integer = EntityCollection.IndexOf(Me)
                If itemIndex >= 0 Then
                    EntityCollection(itemIndex) = Nothing
                End If
            End Sub
#End Region

            Public WithEvents ObjectHitbox As Hitbox

            Public Shared Event PlayerHit(enemy As EntityPlayer)

            ''' <summary>
            ''' Instantiates a new EntityPlayer object, creates its hitbox, and adds it to ObjectCollection
            ''' </summary>
            Sub New()
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

                TranslateBoundBottom = CanvasObjects.GetTranslateBoundBottom(LocationY, ObjectHeight)
                TranslateBoundTop = CanvasObjects.GetTranslateBoundTop(LocationY, ObjectHeight)
                TranslateBoundLeft = CanvasObjects.GetTranslateBoundLeft(LocationX, ObjectWidth)
                TranslateBoundRight = CanvasObjects.GetTranslateBoundRight(LocationX, ObjectWidth)

                CanvasObjects.ObjectCollection.Add(Me)

                ObjectHitbox = CanvasObjects.CreateHitbox(ObjectWidth, ObjectHeight, Me, LocationXDefault, LocationYDefault)

                AddHandler PlayerHit, AddressOf Remove
                AddHandler ObjectHitbox.LeavingCanvas, AddressOf Remove

            End Sub

            ''' <summary>
            ''' Creates a player projectile that moves upward
            ''' </summary>
            Sub FireWeapon()
                ' If no player projectile currently exists, fire a new one
                If IsNothing(PlayerProjectileInstance) Then
                    PlayerProjectileInstance =
                        New ProjectileClasses.ProjectilePlayer(
                            (ObjectWidth / 2),
                            0,
                            (LocationX + ObjectTransformTranslate.X),
                            (LocationY)
                            )
                    MainWindowWrapper.AddToCanvas(PlayerProjectileInstance)
                    AddHandler PlayerProjectileInstance.PlayerProjectileRemove, AddressOf RemovePlayerProjectileInstance
                End If
            End Sub

            Private Shared Sub RemovePlayerProjectileInstance(parent As ProjectileClasses.ProjectilePlayer)
                PlayerProjectileInstance = Nothing
            End Sub
        End Class
    End Class
End Namespace
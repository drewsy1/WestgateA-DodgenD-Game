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
            Private _
                Const PlayerCursorImagePath As String =
                "pack://application:,,,/WestgateA-DodgenD-Game;component/Resources/PlayerCursor.png"

            ''' <summary>
            ''' ProjectilePlayer object for weapon projectile
            ''' </summary>
            Public Shared PlayerProjectileInstance As ProjectileClasses.ProjectilePlayer

#Region "Inherited properties"

            Public Property ObjectName As String Implements ICanvasObjects.ObjectName

            Public Property ObjectScoreValue As Integer Implements ICanvasObjects.ObjectScoreValue

            Public Property ObjectHeight As Double = 57 Implements ICanvasObjects.ObjectHeight

            Public Property ObjectWidth As Double = 39 Implements ICanvasObjects.ObjectWidth

            Protected ReadOnly Property ObjectPointLowerLeft As Point Implements ICanvasObjects.ObjectPointLowerLeft
                Get
                    Return LocationCoords
                End Get
            End Property

            Protected ReadOnly Property ObjectPointUpperRight As Point Implements ICanvasObjects.ObjectPointUpperRight
                Get
                    Return New Point(LocationCoords.X + ObjectWidth, LocationCoords.Y + ObjectHeight)
                End Get
            End Property

            Protected ReadOnly Property LocationCoordsDefault As Point Implements ICanvasObjects.LocationCoordsDefault
                Get
                    Return New Point((MainViewModel.CanvasWidth / 2) - (ObjectWidth / 2), 48)
                End Get
            End Property

            Public Property LocationCoords As Point = LocationCoordsDefault Implements ICanvasObjects.LocationCoords

            Public Property TranslateBoundLeft As Double _
                = CanvasObjects.GetTranslateBoundLeft(LocationCoords.X, ObjectWidth) _
                Implements ICanvasObjects.TranslateBoundLeft

            Public Property TranslateBoundRight As Double _
                = CanvasObjects.GetTranslateBoundRight(LocationCoords.X, ObjectWidth) _
                Implements ICanvasObjects.TranslateBoundRight

            Public Property TranslateBoundTop As Double Implements ICanvasObjects.TranslateBoundTop

            Public Property TranslateBoundBottom As Double Implements ICanvasObjects.TranslateBoundBottom

            Public Property MovementSpeed As Double = 10 Implements ICanvasObjects.MovementSpeed

            Public Property ObjectTransformTranslate As TranslateTransform _
                = New TranslateTransform() With {.X = 0, .Y = 0} Implements ICanvasObjects.ObjectTransformTranslate

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

            Public Sub MoveLeft(Optional localMovementSpeed As Double = 0) Implements ICanvasObjects.MoveLeft
                TranslateX(MovementSpeed * -1)
            End Sub

            Public Sub MoveRight(Optional localMovementSpeed As Double = 0) Implements ICanvasObjects.MoveRight
                TranslateX(MovementSpeed)
            End Sub

            Public Sub MoveUp(Optional localMovementSpeed As Double = 0) Implements ICanvasObjects.MoveUp
                If (localMovementSpeed.Equals(0) And MovementSpeed) Then
                    localMovementSpeed = MovementSpeed
                End If
                TranslateY(localMovementSpeed)
            End Sub

            Public Sub MoveDown(Optional localMovementSpeed As Double = 0) Implements ICanvasObjects.MoveDown
                If (localMovementSpeed.Equals(0) And MovementSpeed) Then
                    localMovementSpeed = MovementSpeed
                End If
                TranslateY(localMovementSpeed * -1)
            End Sub

            Public Sub TranslateY(localMovementSpeed As Double) Implements ICanvasObjects.TranslateY
                Dim location As Double = ObjectTransformTranslate.Y - LocationCoords.Y
                If (location <= TranslateBoundBottom And (localMovementSpeed < 0)) Or
                   (location >= TranslateBoundTop And (localMovementSpeed > 0)) Then
                    ObjectTransformTranslate.Y += localMovementSpeed
                    If Not IsNothing(ObjectHitbox) Then
                        ObjectHitbox.MoveY(localMovementSpeed * -1)
                    End If
                End If
            End Sub

            Public Sub TranslateX(localMovementSpeed As Double) Implements ICanvasObjects.TranslateX
                Dim distanceToLeftBound As Double = TranslateBoundLeft - ObjectTransformTranslate.X
                Dim distanceToRightBound As Double = TranslateBoundRight - ObjectTransformTranslate.X

                Select Case localMovementSpeed
                    Case > 0
                        If (distanceToRightBound) >= localMovementSpeed Then
                            ObjectTransformTranslate.X += localMovementSpeed
                            ObjectHitbox.MoveX(localMovementSpeed * -1)
                        ElseIf (distanceToRightBound) > 0 Then
                            ObjectTransformTranslate.X += distanceToRightBound
                            ObjectHitbox.MoveX(distanceToRightBound * -1)
                        End If
                    Case <= 0
                        If (distanceToLeftBound) <= localMovementSpeed Then
                            ObjectTransformTranslate.X += localMovementSpeed
                            ObjectHitbox.MoveX(localMovementSpeed * -1)
                        ElseIf distanceToLeftBound < 0 Then
                            ObjectTransformTranslate.X += distanceToLeftBound
                            ObjectHitbox.MoveX(distanceToLeftBound * -1)
                        End If
                End Select
                'If (ObjectTransformTranslate.X >= TranslateBoundLeft And (localMovementSpeed < 0)) Or
                '   (ObjectTransformTranslate.X <= TranslateBoundRight And (localMovementSpeed > 0)) Then
                '    ObjectTransformTranslate.X += localMovementSpeed
                '    ObjectHitbox.MoveX(localMovementSpeed * -1)
                'End If
            End Sub

            Public Sub Remove() Implements ICanvasObjects.Remove
                ' Remove rectangle from CanvasGameScreen (make it invisible)
                MainViewModel.MainWindowInstance.CanvasGameScreen.Children.Remove(
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

            Public Shared Event PressFireButton()

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

                MainViewModel.SetCanvasLocation(
                    LocationCoords,
                    ObjectControl
                    )

                TranslateBoundBottom = CanvasObjects.GetTranslateBoundBottom(LocationCoords.Y, ObjectHeight)
                TranslateBoundTop = CanvasObjects.GetTranslateBoundTop(LocationCoords.Y, ObjectHeight)
                TranslateBoundLeft = CanvasObjects.GetTranslateBoundLeft(LocationCoords.X, ObjectWidth)
                TranslateBoundRight = CanvasObjects.GetTranslateBoundRight(LocationCoords.X, ObjectWidth)

                CanvasObjects.ObjectCollection.Add(Me)

                ObjectHitbox = CanvasObjects.CreateHitbox(ObjectWidth, ObjectHeight, Me, LocationCoords.X,
                                                          LocationCoords.Y)

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
                            New Point(LocationCoords.X + ObjectTransformTranslate.X, LocationCoords.Y + ObjectHeight)
                            )
                    MainViewModel.AddToCanvas(PlayerProjectileInstance)
                    RaiseEvent PressFireButton
                    AddHandler PlayerProjectileInstance.PlayerProjectileRemove, AddressOf RemovePlayerProjectileInstance
                End If
            End Sub

            Private Shared Sub RemovePlayerProjectileInstance(parent As ProjectileClasses.ProjectilePlayer)
                RemoveHandler PlayerProjectileInstance.PlayerProjectileRemove, AddressOf RemovePlayerProjectileInstance
                PlayerProjectileInstance = Nothing
            End Sub
        End Class
    End Class
End Namespace
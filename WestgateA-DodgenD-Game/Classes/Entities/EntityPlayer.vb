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

            Public ReadOnly Property ObjectPointLowerLeft As Point Implements ICanvasObjects.ObjectPointLowerLeft
                Get
                    Return LocationCoords + New Point(ObjectTransform_Translate.X, 0 - ObjectTransform_Translate.Y)
                End Get
            End Property

            Public ReadOnly Property ObjectPointUpperRight As Point Implements ICanvasObjects.ObjectPointUpperRight
                Get
                    Return New Point(LocationCoords.X + ObjectWidth, LocationCoords.Y + ObjectHeight)
                End Get
            End Property

            Protected ReadOnly Property LocationCoordsDefault As Point Implements ICanvasObjects.LocationCoordsDefault
                Get
                    Return New Point((Application.CanvasWidth / 2) - (ObjectWidth / 2), 48)
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

            Public Property ObjectTransform_Translate As TranslateTransform _
                = New TranslateTransform() With {.X = 0, .Y = 0} Implements ICanvasObjects.ObjectTransform_Translate

            Protected Property ObjectTransformGroup As TransformGroup =
                New TransformGroup() With {
                    .Children = New TransformCollection(
                        New Transform() {ObjectTransform_Translate})
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
                CanvasObjects.TranslateX(Me,MovementSpeed * -1)
            End Sub

            Public Sub MoveRight(Optional localMovementSpeed As Double = 0) Implements ICanvasObjects.MoveRight
                CanvasObjects.TranslateX(Me,MovementSpeed)
            End Sub

            Public Sub MoveUp(Optional localMovementSpeed As Double = 0) Implements ICanvasObjects.MoveUp
                If (localMovementSpeed.Equals(0) And MovementSpeed) Then
                    localMovementSpeed = MovementSpeed
                End If
                CanvasObjects.TranslateY(Me,localMovementSpeed)
            End Sub

            Public Sub MoveDown(Optional localMovementSpeed As Double = 0) Implements ICanvasObjects.MoveDown
                If (localMovementSpeed.Equals(0) And MovementSpeed) Then
                    localMovementSpeed = MovementSpeed
                End If
                CanvasObjects.TranslateY(Me,localMovementSpeed * -1)
            End Sub

            Public Sub Remove() Implements ICanvasObjects.Remove
                Me.ObjectControl.Visibility = Visibility.Hidden

                
            End Sub

#End Region
            ''' <summary>
            ''' TODO Write ObjectPointLowerLeftStart summary
            ''' </summary>
            ''' <returns></returns>
            Public Property ObjectPointLowerLeftStart As Point

            ''' <summary>
            ''' TODO Write ObjectPointUpperRightStart summary
            ''' </summary>
            ''' <returns></returns>
            Public Property ObjectPointUpperRightStart As Point

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

                Application.SetCanvasLocation(
                    LocationCoords,
                    ObjectControl
                    )

                ObjectPointLowerLeftStart = LocationCoordsDefault + New Point(ObjectTransform_Translate.X, 0 - ObjectTransform_Translate.Y)
                ObjectPointUpperRightStart = New Point(LocationCoordsDefault.X + ObjectWidth, LocationCoordsDefault.Y + ObjectHeight)

                TranslateBoundBottom = CanvasObjects.GetTranslateBoundBottom(LocationCoords.Y, ObjectHeight)
                TranslateBoundTop = CanvasObjects.GetTranslateBoundTop(LocationCoords.Y, ObjectHeight)
                TranslateBoundLeft = CanvasObjects.GetTranslateBoundLeft(LocationCoords.X, ObjectWidth)
                TranslateBoundRight = CanvasObjects.GetTranslateBoundRight(LocationCoords.X, ObjectWidth)

                CanvasObjects.ObjectCollection.Add(Me)
                AddHandler PlayerHit, AddressOf Remove
                AddHandler GameTimer.Tick, AddressOf UpdateCollision
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
                            New Point(LocationCoords.X + ObjectTransform_Translate.X, LocationCoords.Y + ObjectHeight)
                            )
                    Application.AddToCanvas(PlayerProjectileInstance)

                    Application.RaisePressFireButton
                    AddHandler Application.PlayerProjectileRemove, AddressOf RemovePlayerProjectileInstance
                End If
            End Sub

            Private Shared Sub RemovePlayerProjectileInstance(parent As ProjectileClasses.ProjectilePlayer)
                RemoveHandler Application.PlayerProjectileRemove, AddressOf RemovePlayerProjectileInstance
                PlayerProjectileInstance = Nothing
            End Sub

            Public Sub UpdateCollision()
                For Each enemy As EntityClasses.EntityEnemyBase In Application.EnemyCollection
                    If CanvasObjects.CheckCollision(enemy, Me) And enemy.ObjectEnabled Then
                        Application.RaiseCollisionHit(Me, enemy)
                    End If
                Next
            End Sub
        End Class
    End Class
End Namespace
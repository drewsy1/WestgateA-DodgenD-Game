Imports WestgateA_DodgenD_Game.Classes.Projectile
Imports WestgateA_DodgenD_Game.Interfaces

Namespace Classes.Entities
    ' ReSharper disable once ClassNeverInstantiated.Global
    ''' <summary>
    ''' Class containing Projectile classes and meta-properties
    ''' </summary>
    Partial Public Class EntityClasses
        Public Class EntityEnemyBase
            Implements ICanvasObjects
#Region "Inherited properties"
            ''' <summary>
            ''' Default player cursor height in pixels
            ''' </summary>
            ''' <returns></returns>
            Protected Property ObjectHeight As Double = 33 Implements ICanvasObjects.ObjectHeight

            ''' <summary>
            ''' Default player cursor width in pixels
            ''' </summary>
            ''' <returns></returns>
            Protected Shadows Property ObjectWidth As Double = 33 Implements ICanvasObjects.ObjectWidth

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
                    Return MainWindowWrapper.CanvasHeight - 106
                End Get
            End Property

            Protected Property LocationX As Double Implements ICanvasObjects.LocationX

            Protected Property LocationY As Double Implements ICanvasObjects.LocationY

            ''' <summary>
            ''' Leftmost x-value of PlayerCursor
            ''' </summary>
            ''' <returns></returns>
            Protected Property TranslateBoundLeft As Double = CanvasObjects.GetTranslateBoundLeft(LocationX, ObjectWidth) Implements ICanvasObjects.TranslateBoundLeft

            ''' <summary>
            ''' Rightmost x-value of PlayerCursor
            ''' </summary>
            ''' <returns></returns>
            Protected Property TranslateBoundRight As Double = CanvasObjects.GetTranslateBoundRight(LocationX, ObjectWidth) Implements ICanvasObjects.TranslateBoundRight

            Protected Property TranslateBoundTop As Double Implements ICanvasObjects.TranslateBoundTop

            Protected Property TranslateBoundBottom As Double Implements ICanvasObjects.TranslateBoundBottom

            Protected Property MovementSpeed As Double = 5 Implements ICanvasObjects.MovementSpeed

            Protected Property ObjectTransformTranslate As TranslateTransform = New TranslateTransform() With {.X = 0, .Y = 0} Implements ICanvasObjects.ObjectTransformTranslate

            Protected Property ObjectTransformGroup As TransformGroup =
                New TransformGroup() With {
                    .Children = New TransformCollection(
                        New Transform() {ObjectTransformTranslate})
                } Implements ICanvasObjects.ObjectTransformGroup

            Public Overridable Property ObjectControl As Object = New UIElement() Implements ICanvasObjects.ObjectControl

            ''' <summary>
            ''' Moves entity left if entity is within bounds
            ''' </summary>
            ''' <param name="localMovementSpeed">Number of pixels to move left (defaults to 0 unless MovementSpeed is set)</param>
            Public Sub MoveLeft(Optional localMovementSpeed As Double = 0) Implements ICanvasObjects.MoveLeft
                If (localMovementSpeed.Equals(0) And MovementSpeed) Then
                    localMovementSpeed = MovementSpeed
                End If
                TranslateX(localMovementSpeed * -1)
            End Sub

            ''' <summary>
            ''' Moves entity right if entity is within bounds
            ''' </summary>
            ''' <param name="localMovementSpeed">Number of pixels to move right (defaults to 0 unless MovementSpeed is set)</param>
            Public Sub MoveRight(Optional localMovementSpeed As Double = 0) Implements ICanvasObjects.MoveRight
                If (localMovementSpeed.Equals(0) And MovementSpeed) Then
                    localMovementSpeed = MovementSpeed
                End If
                TranslateX(localMovementSpeed)
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
                    If Not IsNothing(_objectHitbox) Then
                        _objectHitbox.MoveY(localMovementSpeed * -1)
                    End If
                End If
            End Sub

            Public Sub TranslateX(localMovementSpeed As Double) Implements ICanvasObjects.TranslateX
                If (ObjectTransformTranslate.X >= TranslateBoundLeft And (localMovementSpeed < 0)) Or
            (ObjectTransformTranslate.X <= TranslateBoundRight And (localMovementSpeed > 0)) Then
                    ObjectTransformTranslate.X += localMovementSpeed
                    _objectHitbox.MoveX(localMovementSpeed * -1)
                End If
            End Sub

            Public Overloads Sub Remove() Implements ICanvasObjects.Remove
                ' Remove rectangle from CanvasGameScreen (make it invisible)
                MainWindowWrapper.MainWindowInstance.CanvasGameScreen.Children.Remove(
                    ObjectControl)

                Hitbox.HitboxCollection.Remove(_objectHitbox)
                _objectHitbox = Nothing

                Dim itemIndex As Integer = EntityCollection.IndexOf(Me)
                If itemIndex >= 0 Then
                    EntityCollection(itemIndex) = Nothing
                End If
            End Sub
#End Region
            ''' <summary>
            ''' ProjectilePlayer object for weapon projectile
            ''' </summary>
            Private _enemyProjectileInstance As ProjectileClasses.ProjectileEnemy

            Private WithEvents _objectHitbox As Hitbox

            Public Shared Event EnemyHit(enemy As EntityEnemyBase)

            ''' <summary>
            ''' Instantiates a new Entity object with matching hitbox and adds it to ObjectCollection
            ''' </summary>
            Protected Sub New(Optional localLocationX As Double = Nothing,
                              Optional localLocationY As Double = Nothing)
                If localLocationX.CompareTo(0) = 0 Then localLocationX = LocationXDefault
                If localLocationY.CompareTo(0) = 0 Then localLocationY = LocationYDefault

                LocationX = localLocationX
                LocationY = localLocationY

                TranslateBoundBottom = CanvasObjects.GetTranslateBoundBottom(LocationY, ObjectHeight)
                TranslateBoundTop = CanvasObjects.GetTranslateBoundTop(LocationY, ObjectHeight)
                TranslateBoundLeft = CanvasObjects.GetTranslateBoundLeft(LocationX, ObjectWidth)
                TranslateBoundRight = CanvasObjects.GetTranslateBoundRight(LocationX, ObjectWidth)


                CanvasObjects.ObjectCollection.Add(Me)

                _objectHitbox = CanvasObjects.CreateHitbox(ObjectWidth, ObjectHeight, Me, localLocationX, localLocationY)

                AddHandler GameTimer.LongTick, AddressOf ChangeContent
                AddHandler GameTimer.Tick, AddressOf CheckHitbox
                AddHandler EnemyHit, AddressOf Remove
            End Sub

            ''' <summary>
            ''' Creates a player projectile that moves upward
            ''' </summary>
            Sub FireWeapon()
                _enemyProjectileInstance =
                        New ProjectileClasses.ProjectileEnemy(
                            (ObjectWidth / 2),
                            0 - ObjectHeight,
                            (LocationX + ObjectTransformTranslate.X),
                            (LocationY)
                            )
                MainWindowWrapper.AddToCanvas(_enemyProjectileInstance)
            End Sub

            Overridable Sub ChangeContent()
            End Sub

            Public Overloads Sub Remove(enemyRemoved As EntityEnemyBase)
                enemyRemoved.Remove()
                Debug.WriteLine(enemyRemoved)
            End Sub

            Protected Sub CheckHitbox()
                If Not IsNothing(EntityPlayer.PlayerProjectileInstance) And Not IsNothing(_objectHitbox.HitboxRectangle) Then
                    If _objectHitbox.HitboxRectangle.IntersectsWith(EntityPlayer.PlayerProjectileInstance.ObjectHitbox.HitboxRectangle) Then
                        RaiseEvent EnemyHit(Me)
                    End If
                End If
            End Sub


        End Class
    End Class
End Namespace
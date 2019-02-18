﻿Imports WestgateA_DodgenD_Game.Classes.Projectile
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

            Protected Property ObjectHeight As Double = 33 Implements ICanvasObjects.ObjectHeight

            Protected Property ObjectWidth As Double = 33 Implements ICanvasObjects.ObjectWidth

            Protected ReadOnly Property ObjectPointLowerLeft As Point Implements ICanvasObjects.ObjectPointLowerLeft
                Get
                    Return LocationCoords
                End Get
            End Property

            Protected ReadOnly Property ObjectPointUpperRight As Point Implements ICanvasObjects.ObjectPointUpperRight
                Get
                    Return New Point(ObjectPointLowerLeft.X + ObjectWidth, ObjectPointLowerLeft.Y + ObjectHeight)
                End Get
            End Property

            Protected ReadOnly Property LocationCoordsDefault As Point Implements ICanvasObjects.LocationCoordsDefault
                Get
                    Return New Point(MainWindowWrapper.CanvasWidth / 2, MainWindowWrapper.CanvasHeight - 106)
                End Get
            End Property

            Protected Property LocationCoords As Point Implements ICanvasObjects.LocationCoords

            Protected Property TranslateBoundLeft As Double _
                = CanvasObjects.GetTranslateBoundLeft(LocationCoords.X, ObjectWidth) _
                Implements ICanvasObjects.TranslateBoundLeft

            Protected Property TranslateBoundRight As Double _
                = CanvasObjects.GetTranslateBoundRight(LocationCoords.X, ObjectWidth) _
                Implements ICanvasObjects.TranslateBoundRight

            Protected Property TranslateBoundTop As Double Implements ICanvasObjects.TranslateBoundTop

            Protected Property TranslateBoundBottom As Double Implements ICanvasObjects.TranslateBoundBottom

            Protected Property MovementSpeed As Double = 5 Implements ICanvasObjects.MovementSpeed

            Protected Property ObjectTransformTranslate As TranslateTransform _
                = New TranslateTransform() With {.X = 0, .Y = 0} Implements ICanvasObjects.ObjectTransformTranslate

            Protected Property ObjectTransformGroup As TransformGroup =
                New TransformGroup() With {
                    .Children = New TransformCollection(
                        New Transform() {ObjectTransformTranslate})
                    } Implements ICanvasObjects.ObjectTransformGroup

            Public Overridable Property ObjectControl As Object = New UIElement() _
                Implements ICanvasObjects.ObjectControl

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
                Dim location As Double = ObjectTransformTranslate.Y - LocationCoords.Y
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
            Protected Sub New(Optional localLocationCoords As Point = Nothing)
                If IsNothing(localLocationCoords) Then localLocationCoords = LocationCoordsDefault

                LocationCoords = localLocationCoords

                TranslateBoundBottom = CanvasObjects.GetTranslateBoundBottom(LocationCoords.Y, ObjectHeight)
                TranslateBoundTop = CanvasObjects.GetTranslateBoundTop(LocationCoords.Y, ObjectHeight)
                TranslateBoundLeft = CanvasObjects.GetTranslateBoundLeft(LocationCoords.X, ObjectWidth)
                TranslateBoundRight = CanvasObjects.GetTranslateBoundRight(LocationCoords.X, ObjectWidth)


                CanvasObjects.ObjectCollection.Add(Me)

                _objectHitbox = CanvasObjects.CreateHitbox(ObjectWidth, ObjectHeight, Me, localLocationCoords.X,
                                                           localLocationCoords.Y)

                AddHandler GameTimer.LongTick, AddressOf ChangeContent
                AddHandler GameTimer.Tick, AddressOf CheckHitbox
                AddHandler EnemyHit, AddressOf Remove
            End Sub

            ''' <summary>
            ''' Creates an enemy projectile that moves downward
            ''' </summary>
            Sub FireWeapon()
                _enemyProjectileInstance =
                    New ProjectileClasses.ProjectileEnemy(
                        (ObjectWidth / 2),
                        0 - ObjectHeight,
                        New Point(LocationCoords.X + ObjectTransformTranslate.X, LocationCoords.Y)
                        )
                MainWindowWrapper.AddToCanvas(_enemyProjectileInstance)
            End Sub

            ''' <summary>
            ''' 
            ''' </summary>
            Protected Overridable Sub ChangeContent()
            End Sub

            Private Overloads Shared Sub Remove(enemyRemoved As EntityEnemyBase)
                RemoveHandler EnemyHit, AddressOf Remove
                enemyRemoved.Remove()
                Debug.WriteLine(enemyRemoved)
            End Sub

            ''' <summary>
            ''' 
            ''' </summary>
            Private Sub CheckHitbox()
                If Not IsNothing(EntityPlayer.PlayerProjectileInstance) Then
                    Dim CollisionCheck As Boolean = CanvasObjects.CheckCollision(Me, EntityPlayer.PlayerProjectileInstance)
                    If CollisionCheck Then
                        RaiseEvent EnemyHit(Me)
                    End If
                End If
                'If Not IsNothing(EntityPlayer.PlayerProjectileInstance) And Not IsNothing(_objectHitbox) Then
                '    If _
                '        _objectHitbox.HitboxRectangle.IntersectsWith(
                '            EntityPlayer.PlayerProjectileInstance.ObjectHitbox.HitboxRectangle) Then
                '        RaiseEvent EnemyHit(Me)
                '    End If
                'End If
            End Sub
        End Class
    End Class
End Namespace
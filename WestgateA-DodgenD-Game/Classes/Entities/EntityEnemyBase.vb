Imports System.Collections.ObjectModel
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

            Public Property ObjectName As String Implements ICanvasObjects.ObjectName

            Public Property ObjectScoreValue As Integer Implements ICanvasObjects.ObjectScoreValue

            Public Property ObjectHeight As Double = 33 Implements ICanvasObjects.ObjectHeight

            Public Property ObjectWidth As Double = 33 Implements ICanvasObjects.ObjectWidth

            Public ReadOnly Property ObjectPointLowerLeft As Point Implements ICanvasObjects.ObjectPointLowerLeft
                Get
                    Return LocationCoords
                End Get
            End Property

            Public ReadOnly Property ObjectPointUpperRight As Point Implements ICanvasObjects.ObjectPointUpperRight
                Get
                    Return New Point(ObjectPointLowerLeft.X + ObjectWidth, ObjectPointLowerLeft.Y + ObjectHeight)
                End Get
            End Property

            Protected ReadOnly Property LocationCoordsDefault As Point Implements ICanvasObjects.LocationCoordsDefault
                Get
                    Return New Point(MainViewModel.CanvasWidth / 2, MainViewModel.CanvasHeight - 106)
                End Get
            End Property

            Public Property LocationCoords As Point Implements ICanvasObjects.LocationCoords

            Protected Property TranslateBoundLeft As Double = CanvasObjects.GetTranslateBoundLeft(LocationCoords.X, ObjectWidth) Implements ICanvasObjects.TranslateBoundLeft

            Protected Property TranslateBoundRight As Double = CanvasObjects.GetTranslateBoundRight(LocationCoords.X, ObjectWidth) Implements ICanvasObjects.TranslateBoundRight

            Protected Property TranslateBoundTop As Double Implements ICanvasObjects.TranslateBoundTop

            Protected Property TranslateBoundBottom As Double Implements ICanvasObjects.TranslateBoundBottom

            Protected Property MovementSpeed As Double = 5 Implements ICanvasObjects.MovementSpeed

            Protected Property ObjectTransformTranslate As TranslateTransform = New TranslateTransform() With {.X = 0, .Y = 0} Implements ICanvasObjects.ObjectTransformTranslate

            Protected Property ObjectTransformGroup As TransformGroup =
                New TransformGroup() With {
                    .Children = New TransformCollection(New Transform() {ObjectTransformTranslate})
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
                Dim location As Double = ObjectTransformTranslate.Y - LocationCoords.Y
                If (location <= TranslateBoundBottom And (localMovementSpeed < 0)) Or (location >= TranslateBoundTop And (localMovementSpeed > 0)) Then
                    ObjectTransformTranslate.Y += localMovementSpeed
                End If
            End Sub

            Public Sub TranslateX(localMovementSpeed As Double) Implements ICanvasObjects.TranslateX
                Dim distanceToLeftBound As Double = TranslateBoundLeft - ObjectTransformTranslate.X
                Dim distanceToRightBound As Double = TranslateBoundRight - ObjectTransformTranslate.X

                Select Case localMovementSpeed
                    Case > 0
                        If (distanceToRightBound) >= localMovementSpeed Then
                            ObjectTransformTranslate.X += localMovementSpeed
                        ElseIf (distanceToRightBound) > 0 Then
                            ObjectTransformTranslate.X += distanceToRightBound
                        End If
                    Case <= 0
                        If (distanceToLeftBound) <= localMovementSpeed Then
                            ObjectTransformTranslate.X += localMovementSpeed
                        ElseIf distanceToLeftBound < 0 Then
                            ObjectTransformTranslate.X += distanceToLeftBound
                        End If
                End Select
                'If (ObjectTransformTranslate.X >= TranslateBoundLeft And (localMovementSpeed < 0)) Or
                '   (ObjectTransformTranslate.X <= TranslateBoundRight And (localMovementSpeed > 0)) Then
                '    ObjectTransformTranslate.X += localMovementSpeed
                '    ObjectHitbox.MoveX(localMovementSpeed * -1)
                'End If
            End Sub

            Public Overloads Sub Remove() Implements ICanvasObjects.Remove
                ' Remove rectangle from CanvasGameScreen (make it invisible)
                MainViewModel.MainWindowInstance.CanvasGameScreen.Children.Remove(ObjectControl)


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

            Public Property EnemyType As String = Me.GetType().Name

            Public Shared Event EnemyHit(enemy As EntityEnemyBase)

            ''' <summary>
            ''' Instantiates a new Entity object with matching hitbox and adds it to ObjectCollection
            ''' </summary>
            Sub New(localName As String, localScoreValue As Integer, Optional localLocationCoords As Point = Nothing)
                If IsNothing(localLocationCoords) Then localLocationCoords = LocationCoordsDefault
                ObjectName = localName
                ObjectScoreValue = localScoreValue
                LocationCoords = localLocationCoords

                TranslateBoundBottom = CanvasObjects.GetTranslateBoundBottom(LocationCoords.Y, ObjectHeight)
                TranslateBoundTop = CanvasObjects.GetTranslateBoundTop(LocationCoords.Y, ObjectHeight)
                TranslateBoundLeft = CanvasObjects.GetTranslateBoundLeft(LocationCoords.X, ObjectWidth)
                TranslateBoundRight = CanvasObjects.GetTranslateBoundRight(LocationCoords.X, ObjectWidth)


                CanvasObjects.ObjectCollection.Add(Me)
                MainViewModel.EnemyCollection.Add(Me)


                AddHandler GameTimer.LongTick, AddressOf ChangeContent
                AddHandler GameTimer.Tick, AddressOf CheckHitbox
                AddHandler EnemyHit, AddressOf Remove
            End Sub

            ''' <summary>
            ''' Creates an enemy projectile that moves downward
            ''' </summary>
            Sub FireWeapon()
                _enemyProjectileInstance = New ProjectileClasses.ProjectileEnemy((ObjectWidth / 2), 0 - ObjectHeight, New Point(LocationCoords.X + ObjectTransformTranslate.X, LocationCoords.Y))
                MainViewModel.AddToCanvas(_enemyProjectileInstance)
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
        End Class
    End Class
End Namespace
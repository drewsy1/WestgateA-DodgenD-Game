Imports WestgateA_DodgenD_Game.Interfaces

Namespace Classes.Entities
    ' ReSharper disable once ClassNeverInstantiated.Global
    ''' <summary>
    ''' Class containing entity classes and meta-properties
    ''' </summary>
    Partial Public Class EntityClasses
        Public Class EntityBase
            Implements IEntity

            Protected Overridable Property EntityHeight As Double Implements IEntity.EntityHeight

            Protected Overridable Property EntityWidth As Double Implements IEntity.EntityWidth

            Protected Overridable Property LocationXDefault As Double Implements IEntity.LocationXDefault

            Protected Overridable Property LocationYDefault As Double Implements IEntity.LocationYDefault

            Protected Overridable Property TranslateBoundLeft As Double Implements IEntity.TranslateBoundLeft

            Protected Overridable Property TranslateBoundRight As Double Implements IEntity.TranslateBoundRight

            Protected Overridable Property TranslateBoundTop As Double Implements IEntity.TranslateBoundTop

            Protected Overridable Property TranslateBoundBottom As Double Implements IEntity.TranslateBoundBottom

            Protected Overridable Property EntityTransformTranslate As TranslateTransform Implements IEntity.EntityTransformTranslate

            Protected Overridable Property EntityTransformGroup As TransformGroup Implements IEntity.EntityTransformGroup

            Public Overridable Property EntityControl As Object Implements IEntity.EntityControl

            Protected Overridable Property MovementSpeed As Double Implements IEntity.MovementSpeed

            Protected WithEvents EntityHitbox As Hitbox

            ''' <summary>
            ''' Calculates the minimum X bound to which an entity can travel
            ''' </summary>
            ''' <returns>Min X-bound as Double</returns>
            Protected Function GetTranslateBoundLeft() As Double
                Return -1 * (LocationXDefault - (EntityWidth * 0.5))
            End Function

            ''' <summary>
            ''' Calculates the maximum X bound to which an entity can travel
            ''' </summary>
            ''' <returns>Max X-bound as Double</returns>
            Protected Function GetTranslateBoundRight() As Double
                Return MainWindowWrapper.CanvasWidth - LocationXDefault - (EntityWidth * 1.5)
            End Function

            ''' <summary>
            ''' Calculates the maximum Y bound to which an entity can travel
            ''' </summary>
            ''' <returns>Max Y-bound as Double</returns>
            Protected Function GetTranslateBoundTop() As Double
                Return MainWindowWrapper.CanvasHeight - LocationYDefault - (EntityHeight * 1.5)
            End Function

            ''' <summary>
            ''' Calculates the minimum Y bound to which an entity can travel
            ''' </summary>
            ''' <returns>Min Y-bound as Double</returns>
            Protected Function GetTranslateBoundBottom() As Double
                Return -1 * (LocationYDefault - (EntityHeight * 0.5))
            End Function

            Protected Function CreateHitbox() As Hitbox Implements IEntity.CreateHitbox
                Dim newHitbox As Hitbox = New Hitbox(EntityWidth,
                                                     EntityHeight,
                                                     LocationXDefault,
                                                     LocationYDefault)
                Return newHitbox
            End Function

            ''' <summary>
            ''' Instantiates a new Entity object with matching hitbox and adds it to EntitiesCollection
            ''' </summary>
            Sub New()
                EntitiesCollection.Add(Me)

                EntityHitbox = CreateHitbox()

                AddHandler EntityHitbox.LeavingCanvas, AddressOf Remove
            End Sub

            ''' <summary>
            ''' Moves entity left if entity is within bounds
            ''' </summary>
            ''' <param name="localMovementSpeed">Number of pixels to move left (defaults to 0 unless MovementSpeed is set)</param>
            Public Sub MoveLeft(Optional localMovementSpeed As Double = 0)
                If (localMovementSpeed.Equals(0) And MovementSpeed) Then
                    localMovementSpeed = MovementSpeed
                End If
                TranslateX(localMovementSpeed * -1)
                'If (EntityTransformTranslate.X > TranslateBoundLeft) Then
                '    EntityTransformTranslate.X -= localMovementSpeed
                'End If
            End Sub

            ''' <summary>
            ''' Moves entity right if entity is within bounds
            ''' </summary>
            ''' <param name="localMovementSpeed">Number of pixels to move right (defaults to 0 unless MovementSpeed is set)</param>
            Public Sub MoveRight(Optional localMovementSpeed As Double = 0)
                If (localMovementSpeed.Equals(0) And MovementSpeed) Then
                    localMovementSpeed = MovementSpeed
                End If
                TranslateX(localMovementSpeed)
                'If (EntityTransformTranslate.X < TranslateBoundRight) Then
                '    EntityTransformTranslate.X += localMovementSpeed
                'End If
            End Sub

            ''' <summary>
            ''' Moves entity up if entity is within bounds
            ''' </summary>
            ''' <param name="localMovementSpeed">Number of pixels to move up (defaults to 0 unless MovementSpeed is set)</param>
            Public Sub MoveUp(Optional localMovementSpeed As Double = 0)
                If (localMovementSpeed.Equals(0) And MovementSpeed) Then
                    localMovementSpeed = MovementSpeed
                End If
                TranslateY(localMovementSpeed)
                'If (EntityTransformTranslate.Y < TranslateBoundTop) Then
                '    EntityTransformTranslate.Y += localMovementSpeed
                'End If
            End Sub

            ''' <summary>
            ''' Moves entity down if entity is within bounds
            ''' </summary>
            ''' <param name="localMovementSpeed">Number of pixels to move down (defaults to 0 unless MovementSpeed is set)</param>
            Public Sub MoveDown(Optional localMovementSpeed As Double = 0)
                If (localMovementSpeed.Equals(0) And MovementSpeed) Then
                    localMovementSpeed = MovementSpeed
                End If
                TranslateY(localMovementSpeed * -1)
                'If (EntityTransformTranslate.Y > TranslateBoundBottom) Then
                '    EntityTransformTranslate.Y -= localMovementSpeed
                'End If
            End Sub

            Private Sub TranslateY(localMovementSpeed As Double) Implements IEntity.TranslateY
                If (EntityTransformTranslate.Y > TranslateBoundBottom And
                    EntityTransformTranslate.Y < TranslateBoundTop) Then
                    EntityTransformTranslate.Y += localMovementSpeed
                End If
            End Sub

            Private Sub TranslateX(localMovementSpeed As Double) Implements IEntity.TranslateX
                If (EntityTransformTranslate.X > TranslateBoundLeft And
                    EntityTransformTranslate.X < TranslateBoundRight) Then
                    EntityTransformTranslate.X += localMovementSpeed
                End If
            End Sub

            Public Sub Remove() Implements IEntity.Remove
                ' Remove rectangle from CanvasGameScreen (make it invisible)
                MainWindowWrapper.MainWindowInstance.CanvasGameScreen.Children.Remove(
                    EntityControl)

                EntityHitbox = Nothing

                Dim itemIndex As Integer = EntitiesCollection.IndexOf(Me)
                If itemIndex >= 0 Then
                    EntitiesCollection(itemIndex) = Nothing
                End If
            End Sub
        End Class
    End Class
End Namespace

Namespace Classes.Entities
    ' ReSharper disable once ClassNeverInstantiated.Global
    ''' <summary>
    ''' Class containing entity classes and meta-properties
    ''' </summary>
    Partial Public Class EntityClasses
        Public MustInherit Class EntityBase
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

            Protected Overridable Property EntityControl As Object Implements IEntity.EntityControl

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

            ''' <summary>
            ''' Instantiates a new Entity object with matching hitbox and adds it to EntitiesCollection
            ''' </summary>
            Protected Sub New()
                EntitiesCollection.Add(Me)

                EntityHitbox = CreateHitbox()

                AddHandler EntityHitbox.LeavingCanvas, AddressOf Remove
            End Sub

            Protected Function CreateHitbox() As Hitbox
                Dim newHitbox As Hitbox = New Hitbox(EntityWidth,
                                                     EntityHeight,
                                                     LocationXDefault,
                                                     LocationYDefault)
                Return newHitbox
            End Function

            ''' <summary>
            ''' Sets location/transform for entity and adds it to canvas
            ''' </summary>
            ''' <param name="localEntity">Object representing the entity's element</param>
            Protected Shared Sub AddToCanvas(localEntity As Object)
                MainWindowWrapper.MainWindowInstance.CanvasGameScreen.Children.Add(localEntity)
            End Sub

            ''' <summary>
            ''' Moves entity left if entity is within bounds
            ''' </summary>
            ''' <param name="localMovementSpeed">Number of pixels to move left (defaults to MovementSpeed)</param>
            Public Sub MoveLeft(Optional localMovementSpeed As Double = 0)

                If (EntityTransformTranslate.X > TranslateBoundLeft) Then
                    EntityTransformTranslate.X -= localMovementSpeed
                End If
            End Sub

            ''' <summary>
            ''' Moves entity right if entity is within bounds
            ''' </summary>
            ''' <param name="localMovementSpeed">Number of pixels to move right (defaults to MovementSpeed)</param>
            Public Sub MoveRight(Optional localMovementSpeed As Double = 0)
                If (localMovementSpeed = 0 And MovementSpeed) Then
                    localMovementSpeed = MovementSpeed
                End If
                If (EntityTransformTranslate.X < TranslateBoundRight) Then
                    EntityTransformTranslate.X += localMovementSpeed
                End If
            End Sub

            ''' <summary>
            ''' Removes entity from canvas, clears the hit box object,
            ''' and removes entity from EntitiesCollection
            ''' </summary>
            Protected Sub Remove()
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

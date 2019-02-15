Namespace Classes.Entities
    ' ReSharper disable once ClassNeverInstantiated.Global
    ''' <summary>
    ''' Class containing entity classes and meta-properties
    ''' </summary>
    Partial Public Class EntityClasses
        Public MustInherit Class EntityBase
            ''' <summary>
            ''' Default entity cursor height in pixels
            ''' </summary>
            Protected Overridable Property EntityHeight As Double

            ''' <summary>
            ''' Default entity width in pixels
            ''' </summary>
            Protected Overridable Property EntityWidth As Double

            ''' <summary>
            ''' Default starting X-coordinate location for entity
            ''' </summary>
            Protected Overridable Property LocationXDefault As Double

            ''' <summary>
            ''' Default starting Y-coordinate location for entity
            ''' </summary>
            Protected Overridable Property LocationYDefault As Double

            ''' <summary>
            ''' Leftmost x-value of entity
            ''' </summary>
            ''' <returns></returns>
            Protected Overridable Property TranslateBoundLeft As Double

            ''' <summary>
            ''' Rightmost x-value of entity
            ''' </summary>
            ''' <returns></returns>
            Protected Overridable Property TranslateBoundRight As Double

            Protected Overridable Property TranslateBoundTop As Double

            Protected Overridable Property TranslateBoundBottom As Double

            ''' <summary>
            ''' Translate transform object for entity
            ''' </summary>
            Protected Overridable Property EntityTransformTranslate As TranslateTransform

            ''' <summary>
            ''' TransformGroup containing Translate transform to be added to entity instance
            ''' </summary>
            Protected Overridable Property EntityTransformGroup As TransformGroup

            ''' <summary>
            ''' Image that serves as entity
            ''' </summary>
            Protected Overridable Property EntityControl As Object

            Private WithEvents _entityHitbox As Hitbox

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
            ''' Instantiates a new Entity object and adds it to EntitiesCollection
            ''' </summary>
            ''' <param name="translateX">X-axis translation (x coordinate +/- pixels)</param>
            ''' <param name="locationX">Object's starting X-coordinate</param>
            ''' <param name="locationY">Object's starting Y-coordinate</param>
            Protected Sub New(translateX As Double,
                              translateY As Double,
                              locationX As Double,
                              locationY As Double)

                EntitiesCollection.Add(Me)

                _entityHitbox = New Hitbox(EntityWidth,
                                           EntityHeight,
                                           locationX + translateX,
                                           locationY + translateY)

                AddHandler _entityHitbox.LeavingCanvas, AddressOf Remove
            End Sub

            ''' <summary>
            ''' Instantiates a new Entity object with default location and adds it to EntitiesCollection
            ''' </summary>
            Protected Sub New()
                EntitiesCollection.Add(Me)

                _entityHitbox = New Hitbox(EntityWidth,
                                           EntityHeight,
                                           LocationXDefault,
                                           LocationYDefault)

                AddHandler _entityHitbox.LeavingCanvas, AddressOf Remove
            End Sub

            ''' <summary>
            ''' Sets location/transform for entity and adds it to canvas
            ''' </summary>
            ''' <param name="locationX">Object's starting X-coordinate</param>
            ''' <param name="locationY">Object's starting Y-coordinate</param>
            Protected Shared Sub AddToCanvas(locationX As Double,
                            locationY As Double,
                            localEntity As Object)
                Canvas.SetLeft(localEntity, locationX)
                Canvas.SetBottom(localEntity, locationY)
                MainWindowWrapper.MainWindowInstance.CanvasGameScreen.Children.Add(localEntity)
            End Sub

            ''' <summary>
            ''' Moves entity left if entity is within bounds
            ''' </summary>
            ''' <param name="localMovementSpeed">Number of pixels to move left (defaults to MovementSpeed)</param>
            Protected Sub MoveLeft(localMovementSpeed As Double, localEntity As Object)
                If (localEntity.X > TranslateBoundLeft) Then
                    localEntity.X -= localMovementSpeed
                End If
            End Sub

            ''' <summary>
            ''' Moves entity right if entity is within bounds
            ''' </summary>
            ''' <param name="localMovementSpeed">Number of pixels to move right (defaults to MovementSpeed)</param>
            Protected Sub MoveRight(localMovementSpeed As Double, localEntity As Object)
                If (localEntity.X < TranslateBoundRight) Then
                    localEntity.X += localMovementSpeed
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

                __entityHitbox = Nothing

                Dim itemIndex As Integer = EntitiesCollection.IndexOf(Me)
                If itemIndex >= 0 Then
                    EntitiesCollection(itemIndex) = Nothing
                End If
            End Sub
        End Class
    End Class
End Namespace

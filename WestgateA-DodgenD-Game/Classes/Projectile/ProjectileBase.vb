
Imports WestgateA_DodgenD_Game.Interfaces

Namespace Classes.Projectile
    ' ReSharper disable once ClassNeverInstantiated.Global
    Partial Public Class ProjectileClasses

        ''' <summary>
        ''' Create a static collection of projectiles to keep track of them
        ''' </summary>
        Public Shared ReadOnly ProjectilesCollection As List(Of ProjectileBase) =
                                   New List(Of ProjectileBase)()

        ''' <summary>
        ''' Checks for active projectiles and moves/removes them as needed
        ''' </summary>
        Public Shared Sub UpdateProjectiles()
            'ToDo Is there any better way to do this? Feels clunky
            If ProjectilesCollection.Count > 0 Then
                For Each obj As ProjectileBase In ProjectilesCollection.ToArray()
                    If Not IsNothing(obj) Then
                        obj.UpdateLocation()
                    End If
                Next
            End If
        End Sub

        ''' <summary>
        ''' Base Projectile class, must be inherited by new classes
        ''' </summary>
        Public MustInherit Class ProjectileBase
            Implements ICanvasObjects

            Protected Property ObjectHeight As Double = 27 Implements ICanvasObjects.ObjectHeight
            Protected Property ObjectWidth As Double = 3 Implements ICanvasObjects.ObjectWidth
            ' ReSharper disable UnassignedGetOnlyAutoProperty
            Protected Overridable ReadOnly Property LocationXDefault As Double Implements ICanvasObjects.LocationXDefault
            Protected Overridable ReadOnly Property LocationYDefault As Double Implements ICanvasObjects.LocationYDefault
            Protected Property LocationX As Double Implements ICanvasObjects.LocationX
            Protected Property LocationY As Double Implements ICanvasObjects.LocationY
            Protected Property TranslateBoundLeft As Double Implements ICanvasObjects.TranslateBoundLeft
            Protected Property TranslateBoundRight As Double Implements ICanvasObjects.TranslateBoundRight
            Protected Property TranslateBoundTop As Double Implements ICanvasObjects.TranslateBoundTop
            Protected Property TranslateBoundBottom As Double Implements ICanvasObjects.TranslateBoundBottom
            Protected Property MovementSpeed As Double = 30 Implements ICanvasObjects.MovementSpeed
            Protected Property ObjectTransformTranslate As TranslateTransform =
                New TranslateTransform() With {.X = 0, .Y = 0} Implements ICanvasObjects.ObjectTransformTranslate
            Protected Property ObjectTransformGroup As TransformGroup =
                New TransformGroup() With {
                    .Children = New TransformCollection(
                        New Transform() {ObjectTransformTranslate}
                        )
                    } Implements ICanvasObjects.ObjectTransformGroup
            Public Property ObjectControl As Object = New Rectangle() With {
                .Height = ObjectHeight,
                .Width = ObjectWidth,
                .StrokeThickness = 0,
                .RenderTransform = ObjectTransformGroup,
                .RenderTransformOrigin = New Point(0, 0)
                } Implements ICanvasObjects.ObjectControl

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

            ''' <summary>
            ''' Overrides double representing direction of projectile travel
            ''' </summary>
            ''' <returns>ProjectileDirection</returns>
            Protected Overridable Property ProjectileDirection As Double

            ''' <summary>
            ''' Color of projectile
            ''' </summary>
            ''' <returns>ProjectileColor</returns>
            Protected Overridable Property ProjectileColor As Color


            ''' <summary>
            ''' Instantiates a new Projectile object and adds it to ProjectilesCollection
            ''' </summary>
            ''' <param name="translateX">X-axis translation (x coordinate +/- pixels)</param>
            ''' <param name="localLocationX">Object's starting X-coordinate</param>
            ''' <param name="localLocationY">Object's starting Y-coordinate</param>
            Protected Sub New(translateX As Double,
                              translateY As Double,
                              Optional localLocationX As Double = Nothing,
                              Optional localLocationY As Double = Nothing)
                LocationX = localLocationX
                LocationY = localLocationY

                TranslateBoundBottom = CanvasObjects.GetTranslateBoundBottom(LocationY, ObjectHeight)
                TranslateBoundTop = CanvasObjects.GetTranslateBoundTop(LocationY, ObjectHeight)
                TranslateBoundLeft = CanvasObjects.GetTranslateBoundLeft(LocationX, ObjectWidth)
                TranslateBoundRight = CanvasObjects.GetTranslateBoundRight(LocationX, ObjectWidth)

                CanvasObjects.ObjectCollection.Add(Me)

                ObjectHitbox = CanvasObjects.CreateHitbox(ObjectWidth, ObjectHeight, Me, localLocationX, localLocationY)
                ProjectilesCollection.Add(Me)

                ' Set coordinates on canvas for projectile
                MainWindowWrapper.SetCanvasLocation(localLocationX, localLocationY, ObjectControl)

                ' Increment projectile's X transform value by translateX
                ObjectTransformTranslate.X += translateX
                ObjectTransformTranslate.Y += translateY

                AddHandler ObjectHitbox.LeavingCanvas, AddressOf Remove
                AddHandler GameTimer.Tick, AddressOf UpdateLocation
            End Sub

            Public Overridable Sub Remove() Implements ICanvasObjects.Remove
                RemoveHandler ObjectHitbox.LeavingCanvas, AddressOf Remove
                RemoveHandler GameTimer.Tick, AddressOf UpdateLocation
                ' Remove rectangle from CanvasGameScreen (make it invisible)
                MainWindowWrapper.MainWindowInstance.CanvasGameScreen.Children.Remove(
                    ObjectControl)

                ObjectHitbox = Nothing

                Dim itemIndex As Integer = ProjectilesCollection.IndexOf(Me)
                If itemIndex >= 0 Then
                    ProjectilesCollection.RemoveAt(itemIndex)
                End If
            End Sub

            Public WithEvents ObjectHitbox As Hitbox

            ''' <summary>
            ''' Sets fill color for projectile
            ''' </summary>
            ''' <param name="newProjectileColor">Desired fill color for projectile</param>
            Protected Sub SetColor(newProjectileColor As Color)
                ObjectControl.Fill = New SolidColorBrush(newProjectileColor)
            End Sub

            ''' <summary>
            ''' Moves projectile based on ProjectileDirection and removes it if needed
            ''' </summary>
            Sub UpdateLocation()

                TranslateY(MovementSpeed * ProjectileDirection)
                'ObjectTransformTranslate.Y += (MovementSpeed * ProjectileDirection)
                'ObjectHitbox.MoveY(MovementSpeed * ProjectileDirection * -1)
            End Sub

        End Class


    End Class
End Namespace
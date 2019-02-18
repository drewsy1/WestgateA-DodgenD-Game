Imports WestgateA_DodgenD_Game.Classes.Projectile
Imports WestgateA_DodgenD_Game.Interfaces

Namespace Classes
    Public Class CanvasObjects
        Implements ICanvasObjects

        Public Shared ReadOnly ObjectCollection As List(Of Object) =
                                    New List(Of Object)()

        Protected Overridable Property ObjectHeight As Double Implements ICanvasObjects.ObjectHeight
        Protected Overridable Property ObjectWidth As Double Implements ICanvasObjects.ObjectWidth
        Protected Overridable ReadOnly Property LocationXDefault As Double Implements ICanvasObjects.LocationXDefault
        Protected Overridable ReadOnly Property LocationYDefault As Double Implements ICanvasObjects.LocationYDefault
        Protected Overridable Property LocationX As Double Implements ICanvasObjects.LocationX
        Protected Overridable Property LocationY As Double Implements ICanvasObjects.LocationY
        Protected Overridable Property TranslateBoundLeft As Double Implements ICanvasObjects.TranslateBoundLeft
        Protected Overridable Property TranslateBoundRight As Double Implements ICanvasObjects.TranslateBoundRight
        Protected Overridable Property TranslateBoundTop As Double Implements ICanvasObjects.TranslateBoundTop
        Protected Overridable Property TranslateBoundBottom As Double Implements ICanvasObjects.TranslateBoundBottom
        Protected Overridable Property MovementSpeed As Double Implements ICanvasObjects.MovementSpeed
        Protected Overridable Property ObjectTransformTranslate As TranslateTransform Implements ICanvasObjects.ObjectTransformTranslate
        Protected Overridable Property ObjectTransformGroup As TransformGroup Implements ICanvasObjects.ObjectTransformGroup
        Public Overridable Property ObjectControl As Object Implements ICanvasObjects.ObjectControl
        Public WithEvents ObjectHitbox As Hitbox

        Protected Overridable Function CreateHitbox(localObjectWidth As Double, localObjectHeight As Double, localParent As Object, localLocationX As Double, localLocationY As Double) As Hitbox Implements ICanvasObjects.CreateHitbox
            Dim newHitbox = New Hitbox(localObjectWidth,
                                       localObjectHeight,
                                       localParent,
                                       localLocationX,
                                       localLocationY)
            Return newHitbox
        End Function


        Protected Overridable Function GetTranslateBoundLeft() As Double Implements ICanvasObjects.GetTranslateBoundLeft
            Return -1 * (LocationX - (ObjectWidth * 0.5))
        End Function

        Protected Overridable Function GetTranslateBoundRight() As Double Implements ICanvasObjects.GetTranslateBoundRight
            Return MainWindowWrapper.CanvasWidth - LocationX - (ObjectWidth * 1.5)
        End Function

        Protected Overridable Function GetTranslateBoundTop() As Double Implements ICanvasObjects.GetTranslateBoundTop
            Return MainWindowWrapper.CanvasHeight - LocationY - (ObjectHeight * 1.5)
        End Function

        Protected Overridable Function GetTranslateBoundBottom() As Double Implements ICanvasObjects.GetTranslateBoundBottom
            Return -1 * (LocationY - (ObjectHeight * 0.5))
        End Function

        ''' <summary>
        ''' Moves entity left if entity is within bounds
        ''' </summary>
        ''' <param name="localMovementSpeed">Number of pixels to move left (defaults to 0 unless MovementSpeed is set)</param>
        Public Sub MoveLeft(Optional localMovementSpeed As Double = 0)
            If (localMovementSpeed.Equals(0) And MovementSpeed) Then
                localMovementSpeed = MovementSpeed
            End If
            TranslateX(localMovementSpeed * -1)
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
        End Sub

        Protected Overridable Sub TranslateY(localMovementSpeed As Double) Implements ICanvasObjects.TranslateY
            Dim Location As Double = ObjectTransformTranslate.Y - LocationY
            If (Location <= TranslateBoundBottom And (localMovementSpeed < 0)) Or
                (Location >= TranslateBoundTop And (localMovementSpeed > 0)) Then
                ObjectTransformTranslate.Y += localMovementSpeed
                If Not IsNothing(ObjectHitbox) Then
                    ObjectHitbox.MoveY(localMovementSpeed * -1)
                End If
            End If
        End Sub

        Protected Overridable Sub TranslateX(localMovementSpeed As Double) Implements ICanvasObjects.TranslateX
            If (ObjectTransformTranslate.X >= TranslateBoundLeft And (localMovementSpeed < 0)) Or
            (ObjectTransformTranslate.X <= TranslateBoundRight And (localMovementSpeed > 0)) Then
                ObjectTransformTranslate.X += localMovementSpeed
                ObjectHitbox.MoveX(localMovementSpeed * -1)
            End If
        End Sub

        Public Overridable Sub Remove() Implements ICanvasObjects.Remove
            ' Remove rectangle from CanvasGameScreen (make it invisible)
            MainWindowWrapper.MainWindowInstance.CanvasGameScreen.Children.Remove(
                ObjectControl)

            Hitbox.HitboxCollection.Remove(ObjectHitbox)
            ObjectHitbox = Nothing

            Dim itemIndex As Integer = ObjectCollection.IndexOf(Me)
            If itemIndex >= 0 Then
                ObjectCollection(itemIndex) = Nothing
            End If
        End Sub

        Public Sub New(Optional localObjectWidth As Double = Nothing,
                       Optional localObjectHeight As Double = Nothing,
                       Optional localLocationX As Double = Nothing,
                       Optional localLocationY As Double = Nothing)
            If localLocationX.CompareTo(0) = 0 Then localLocationX = LocationXDefault
            If localLocationY.CompareTo(0) = 0 Then localLocationY = LocationYDefault

            LocationX = localLocationX
            LocationY = localLocationY

            TranslateBoundBottom = GetTranslateBoundBottom()
            TranslateBoundTop = GetTranslateBoundTop()
            TranslateBoundLeft = GetTranslateBoundLeft()
            TranslateBoundRight = GetTranslateBoundRight()


            ObjectCollection.Add(Me)

            ObjectHitbox = CreateHitbox(localObjectWidth, localObjectHeight, Me, localLocationX, localLocationY)

        End Sub
    End Class
End Namespace
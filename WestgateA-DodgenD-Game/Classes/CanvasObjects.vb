Imports WestgateA_DodgenD_Game.Interfaces

Namespace Classes
    Public Class CanvasObjects
        ''' <summary>
        ''' TODO Write ObjectCollection summary
        ''' </summary>
        Public Shared ReadOnly ObjectCollection As List(Of Object) =
                                   New List(Of Object)()

        ''' <summary>
        ''' TODO Write CreateHitbox summary
        ''' </summary>
        ''' <param name="localObjectWidth"></param>
        ''' <param name="localObjectHeight"></param>
        ''' <param name="localParent"></param>
        ''' <param name="localLocationX"></param>
        ''' <param name="localLocationY"></param>
        ''' <returns></returns>
        Public Shared Function CreateHitbox(localObjectWidth As Double, localObjectHeight As Double,
                                            localParent As Object, localLocationX As Double, localLocationY As Double) _
            As Hitbox
            Dim newHitbox = New Hitbox(localObjectWidth, localObjectHeight, localParent, localLocationX, localLocationY)
            Return newHitbox
        End Function

        ''' <summary>
        ''' Calculates the minimum X bound to which an entity can travel
        ''' </summary>
        ''' <param name="localLocationX"></param>
        ''' <param name="localObjectWidth"></param>
        ''' <returns>Min X-bound as Double</returns>
        Public Shared Function GetTranslateBoundLeft(localLocationX As Double, localObjectWidth As Double) As Double
            Return -1 * (localLocationX - (localObjectWidth * 0.5))
        End Function

        ''' <summary>
        ''' Calculates the maximum X bound to which an entity can travel
        ''' </summary>
        ''' <param name="localLocationX"></param>
        ''' <param name="localObjectWidth"></param>
        ''' <returns>Max X-bound as Double</returns>
        Public Shared Function GetTranslateBoundRight(localLocationX As Double, localObjectWidth As Double) As Double
            Return MainViewModel.CanvasWidth - localLocationX - (localObjectWidth * 1.5)
        End Function

        ''' <summary>
        ''' Calculates the maximum Y bound to which an entity can travel
        ''' </summary>
        ''' <param name="localLocationY"></param>
        ''' <param name="localObjectHeight"></param>
        ''' <returns>Max Y-bound as Double</returns>
        Public Shared Function GetTranslateBoundTop(localLocationY As Double, localObjectHeight As Double) As Double
            Return MainViewModel.CanvasHeight - localLocationY - (localObjectHeight * 1.5)
        End Function

        ''' <summary>
        ''' Calculates the minimum Y bound to which an entity can travel
        ''' </summary>
        ''' <param name="localLocationY"></param>
        ''' <param name="localObjectHeight"></param>
        ''' <returns>Min Y-bound as Double</returns>
        Public Shared Function GetTranslateBoundBottom(localLocationY As Double, localObjectHeight As Double) As Double
            Return -1 * (localLocationY - (localObjectHeight * 0.5))
        End Function

        Public Shared Function CheckCollision(entity As ICanvasObjects, projectile As ICanvasObjects) As Boolean
            Dim containsTopBound As Boolean = CheckIfWithinRange(projectile.ObjectPointUpperRight.Y, entity.ObjectPointUpperRight.Y, entity.ObjectPointLowerLeft.Y)
            Dim containsBottomBound As Boolean = CheckIfWithinRange(projectile.ObjectPointLowerLeft.Y, entity.ObjectPointUpperRight.Y, entity.ObjectPointLowerLeft.Y)
            Dim containsLeftBound As Boolean = CheckIfWithinRange(projectile.ObjectPointLowerLeft.X, entity.ObjectPointUpperRight.X, entity.ObjectPointLowerLeft.X)
            Dim containsRightBound As Boolean = CheckIfWithinRange(projectile.ObjectPointUpperRight.X, entity.ObjectPointUpperRight.X, entity.ObjectPointLowerLeft.X)

            Dim containsYAxis = containsTopBound Or containsBottomBound
            Dim containsXAxis = containsLeftBound Or containsRightBound

            Return containsYAxis And containsXAxis
        End Function

        Private Shared Function CheckIfWithinRange(numberToCheck As Double, upperBound As Double, lowerBound As Double) _
            As Boolean
            If (numberToCheck <= upperBound) And (numberToCheck >= lowerBound) Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace
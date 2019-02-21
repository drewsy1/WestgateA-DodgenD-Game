Imports WestgateA_DodgenD_Game.Interfaces

Namespace Classes

    Public Class CanvasObjects

        ''' <summary>
        ''' Translates along the Y axis a given number of pixels while staying within canvas bounds
        ''' </summary>
        ''' <param name="entity"></param>
        ''' <param name="localMovementSpeed"></param>
        Public Shared Sub TranslateY(entity As Object, localMovementSpeed As Double)
            Dim distanceToBottomBound As Double = entity.TranslateBoundBottom - entity.ObjectTransform_Translate.Y
            Dim distanceToTopBound As Double = entity.TranslateBoundTop + entity.ObjectTransform_Translate.Y

            Select Case localMovementSpeed
                Case > 0
                    If (distanceToBottomBound) >= -1 * localMovementSpeed Then
                        entity.ObjectTransform_Translate.Y += localMovementSpeed
                    ElseIf (distanceToBottomBound) > 0 Then
                        entity.ObjectTransform_Translate.Y += -1 * distanceToBottomBound
                    End If
                Case <= 0
                    If (distanceToTopBound) >= -1 * localMovementSpeed Then
                        entity.ObjectTransform_Translate.Y += localMovementSpeed
                    ElseIf distanceToTopBound > 0 Then
                        entity.ObjectTransform_Translate.Y += -1 * distanceToTopBound
                    End If
            End Select
        End Sub

        ''' <summary>
        ''' Translates along the X axis a given number of pixels while staying within canvas bounds
        ''' </summary>
        ''' <param name="entity"></param>
        ''' <param name="localMovementSpeed"></param>
        Public Shared Sub TranslateX(entity As Object, localMovementSpeed As Double)
            Dim distanceToLeftBound As Double = entity.TranslateBoundLeft - entity.ObjectTransform_Translate.X
            Dim distanceToRightBound As Double = entity.TranslateBoundRight - entity.ObjectTransform_Translate.X

            Select Case localMovementSpeed
                Case > 0
                    If (distanceToRightBound) >= localMovementSpeed Then
                        entity.ObjectTransform_Translate.X += localMovementSpeed
                    ElseIf (distanceToRightBound) > 0 Then
                        entity.ObjectTransform_Translate.X += distanceToRightBound
                    End If
                Case <= 0
                    If (distanceToLeftBound) <= localMovementSpeed Then
                        entity.ObjectTransform_Translate.X += localMovementSpeed
                    ElseIf distanceToLeftBound < 0 Then
                        entity.ObjectTransform_Translate.X += distanceToLeftBound
                    End If
            End Select
        End Sub

        ''' <summary>
        ''' TODO Write ObjectCollection summary
        ''' </summary>
        Public Shared ReadOnly ObjectCollection As List(Of Object) =
                                   New List(Of Object)()

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="entity"></param>
        ''' <param name="projectile"></param>
        ''' <returns></returns>
        Public Shared Function CheckCollision(entity As ICanvasObjects, projectile As ICanvasObjects) As Boolean
            Dim containsTopBound As Boolean = CheckIfWithinRange(projectile.ObjectPointUpperRight.Y, entity.ObjectPointUpperRight.Y, entity.ObjectPointLowerLeft.Y)
            Dim containsBottomBound As Boolean = CheckIfWithinRange(projectile.ObjectPointLowerLeft.Y, entity.ObjectPointUpperRight.Y, entity.ObjectPointLowerLeft.Y)
            Dim containsLeftBound As Boolean = CheckIfWithinRange(projectile.ObjectPointLowerLeft.X, entity.ObjectPointUpperRight.X, entity.ObjectPointLowerLeft.X)
            Dim containsRightBound As Boolean = CheckIfWithinRange(projectile.ObjectPointUpperRight.X, entity.ObjectPointUpperRight.X, entity.ObjectPointLowerLeft.X)

            Dim containsYAxis = containsTopBound Or containsBottomBound
            Dim containsXAxis = containsLeftBound Or containsRightBound

            Return containsYAxis And containsXAxis
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
            Return Application.CanvasWidth - localLocationX - (localObjectWidth * 1.5)
        End Function

        ''' <summary>
        ''' Calculates the maximum Y bound to which an entity can travel
        ''' </summary>
        ''' <param name="localLocationY"></param>
        ''' <param name="localObjectHeight"></param>
        ''' <returns>Max Y-bound as Double</returns>
        Public Shared Function GetTranslateBoundTop(localLocationY As Double, localObjectHeight As Double) As Double
            Return Application.CanvasHeight - localLocationY - (localObjectHeight * 1.5)
        End Function

        ''' <summary>
        ''' ToDo Write CheckIfWithinRange summary
        ''' </summary>
        ''' <param name="numberToCheck"></param>
        ''' <param name="upperBound"></param>
        ''' <param name="lowerBound"></param>
        ''' <returns></returns>
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
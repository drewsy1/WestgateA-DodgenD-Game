Imports WestgateA_DodgenD_Game.Classes.Projectile
Imports WestgateA_DodgenD_Game.Interfaces

Namespace Classes
    Public Class CanvasObjects

        Public Shared ReadOnly ObjectCollection As List(Of Object) =
                                    New List(Of Object)()

        Public Shared Function CreateHitbox(localObjectWidth As Double, localObjectHeight As Double, localParent As Object, localLocationX As Double, localLocationY As Double) As Hitbox
            Dim newHitbox = New Hitbox(localObjectWidth, localObjectHeight, localParent, localLocationX, localLocationY)
            Return newHitbox
        End Function

        ''' <summary>
        ''' Calculates the minimum X bound to which an entity can travel
        ''' </summary>
        ''' <returns>Min X-bound as Double</returns>
        Public Shared Function GetTranslateBoundLeft(localLocationX As Double, localObjectWidth As Double) As Double
            Return -1 * (localLocationX - (localObjectWidth * 0.5))
        End Function

        ''' <summary>
        ''' Calculates the maximum X bound to which an entity can travel
        ''' </summary>
        ''' <returns>Max X-bound as Double</returns>
        Public Shared Function GetTranslateBoundRight(localLocationX As Double, localObjectWidth As Double) As Double
            Return MainWindowWrapper.CanvasWidth - localLocationX - (localObjectWidth * 1.5)
        End Function

        ''' <summary>
        ''' Calculates the maximum Y bound to which an entity can travel
        ''' </summary>
        ''' <returns>Max Y-bound as Double</returns>
        Public Shared Function GetTranslateBoundTop(localLocationY As Double, localObjectHeight As Double) As Double
            Return MainWindowWrapper.CanvasHeight - localLocationY - (localObjectHeight * 1.5)
        End Function

        ''' <summary>
        ''' Calculates the minimum Y bound to which an entity can travel
        ''' </summary>
        ''' <returns>Min Y-bound as Double</returns>
        Public Shared Function GetTranslateBoundBottom(localLocationY As Double, localObjectHeight As Double) As Double
            Return -1 * (localLocationY - (localObjectHeight * 0.5))
        End Function
    End Class
End Namespace
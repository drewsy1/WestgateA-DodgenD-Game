Namespace Classes.Projectile
    ''' <summary>
    ''' Class containing Projectile classes and meta-properties
    ''' </summary>
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
    End Class
End Namespace
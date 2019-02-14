Imports JetBrains.Annotations

Namespace Classes
    ''' <summary>
    ''' Class containing Projectile classes and meta-properties
    ''' </summary>
    <UsedImplicitly>
    Partial Public Class ProjectileClasses
        ''' <summary>
        ''' Get the MainWindow as an object
        ''' </summary>
        Private Shared ReadOnly MainWindowInstance As MainWindow =
                                    Windows.Application.Current.MainWindow

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
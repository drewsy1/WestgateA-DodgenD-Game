Imports WestgateA_DodgenD_Game.Classes.Projectile
Namespace Classes.Entities

    Partial Public Class EntityClasses
        Public Class EntityEnemyA
            Inherits EntityEnemy
            Protected Shared ReadOnly EnemyACollection As List(Of Object) =
                                          New List(Of Object)()

            Public Overrides Property ObjectControl As Object = New Button() With {
                                .Name = "EnemyA_" & EnemyACollection.Count,
                                .Height = ObjectHeight,
                                .Width = ObjectWidth,
                                .RenderTransform = ObjectTransformGroup,
                                .RenderTransformOrigin = New Point(0, 0),
                .IsEnabled = False
                }

            Sub New()
                MyBase.New()
                EnemyACollection.Add(Me)

                MainWindowWrapper.SetCanvasLocation(
                    LocationXDefault,
                    LocationYDefault,
                    ObjectControl
                    )
                Dim test As Button = New Button()
                'test.
                'ObjectControl.
            End Sub
        End Class
    End Class
End Namespace
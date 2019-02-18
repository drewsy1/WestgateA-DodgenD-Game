
Namespace Classes.Entities

    Partial Public MustInherit Class EntityClasses
        Public Class EntityEnemyC
            Inherits EntityEnemy

            Private Shared ReadOnly EnemyCCollection As List(Of Object) =
                                          New List(Of Object)()

            Public Overrides Property ObjectControl As Object = New TextBox() With {
                                .Name = "EnemyC_" & EnemyCCollection.Count,
                                .Height = ObjectHeight,
                                .Width = ObjectWidth,
                                .RenderTransform = ObjectTransformGroup,
                                .RenderTransformOrigin = New Point(0, 0),
                                .Text = "☣",
                                .FontFamily = New FontFamily("Segoe UI Symbol"),
                                .FontSize = 32,
                                .VerticalContentAlignment = VerticalAlignment.Center,
                                .HorizontalContentAlignment = HorizontalAlignment.Center,
                                .Padding = New Thickness(-1.5, -10, 0, 0),
                                .Background = New SolidColorBrush(Color.FromRgb(0, 255, 0)),
                                .Foreground = New SolidColorBrush(Color.FromRgb(0, 0, 0)),
                                .IsReadOnly = True
                }

            Sub New(localLocationX As Double, localLocationY As Double)
                MyBase.New()
                MainWindowWrapper.SetCanvasLocation(
                    localLocationX,
                    localLocationY,
                    ObjectControl
                    )
            End Sub

            Overrides Sub ChangeContent()
                'Fires every half-second
                If ObjectControl.Background.Color = Color.FromRgb(0, 255, 0) Then
                    ObjectControl.Background.Color = Color.FromRgb(0, 0, 0)
                    ObjectControl.Foreground.Color = Color.FromRgb(0, 255, 0)
                Else
                    ObjectControl.Background.Color = Color.FromRgb(0, 255, 0)
                    ObjectControl.Foreground.Color = Color.FromRgb(0, 0, 0)
                End If
            End Sub
        End Class
    End Class
End Namespace
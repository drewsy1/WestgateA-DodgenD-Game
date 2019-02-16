
Namespace Classes.Entities

    Partial Public MustInherit Class EntityClasses
        Public Class EntityEnemyC
            Inherits EntityEnemy

            Private Shared ReadOnly EnemyCCollection As List(Of Object) =
                                          New List(Of Object)()

            Public Overrides Property ObjectControl As Object = New Label() With {
                                .Name = "EnemyC_" & EnemyCCollection.Count,
                                .Height = ObjectHeight,
                                .Width = ObjectWidth,
                                .RenderTransform = ObjectTransformGroup,
                                .RenderTransformOrigin = New Point(0, 0),
                                .Content = "⛄",
                                .FontFamily = New FontFamily("Segoe UI Symbol"),
                                .FontSize = 24,
                                .VerticalContentAlignment = VerticalAlignment.Top,
                                .HorizontalContentAlignment = HorizontalAlignment.Center,
                                .Padding = New Thickness(0, 0, 0, 0),
                                .Background = New SolidColorBrush(Color.FromRgb(255, 255, 255)),
                                .Foreground = New SolidColorBrush(Color.FromRgb(0, 0, 0))
                }

            Public Shadows WithEvents ObjectHitbox As Hitbox

            Sub New()
                MyBase.New()
                EnemyCCollection.Add(Me)

                MainWindowWrapper.SetCanvasLocation(
                    LocationXDefault,
                    LocationYDefault,
                    ObjectControl
                    )
                ObjectHitbox = CreateHitbox()
                AddHandler GameTimer.LongTick, AddressOf ChangeContent
            End Sub

            Sub ChangeContent()
                'Fires every half-second
                If ObjectControl.Background.Color = Color.FromRgb(255, 255, 255) Then
                    ObjectControl.Background.Color = Color.FromRgb(64, 255, 255)
                Else
                    ObjectControl.Background.Color = Color.FromRgb(255, 255, 255)
                End If
            End Sub
        End Class
    End Class
End Namespace
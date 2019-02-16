
Namespace Classes.Entities

    Partial Public MustInherit Class EntityClasses
        Public Class EntityEnemyD
            Inherits EntityEnemy

            Private Shared ReadOnly EnemyDCollection As List(Of Object) =
                                          New List(Of Object)()

            Public Overrides Property ObjectControl As Object = New CheckBox() With {
                                .Name = "EnemyD_" & EnemyDCollection.Count,
                                .Height = 11,
                                .Width = 11,
                                .RenderTransform = ObjectTransformGroup,
                                .RenderTransformOrigin = New Point(0, 0),
                                .Content = "",
                                .FontFamily = New FontFamily("Segoe UI Symbol"),
                                .FontSize = 8,
                                .VerticalContentAlignment = VerticalAlignment.Top,
                                .HorizontalContentAlignment = HorizontalAlignment.Center,
                                .Padding = New Thickness(0, 0, 0, 0),
                                .Background = New SolidColorBrush(Color.FromRgb(255, 255, 255)),
                                .Foreground = New SolidColorBrush(Color.FromRgb(0, 0, 0))
                }

            Public Shadows WithEvents ObjectHitbox As Hitbox

            Sub New(localLocationX As Double, localLocationY As Double)
                MyBase.New()
                EnemyDCollection.Add(Me)

                MainWindowWrapper.SetCanvasLocation(
                    localLocationX,
                    localLocationY,
                    ObjectControl
                    )
                ObjectHitbox = CreateHitbox()
                AddHandler GameTimer.LongTick, AddressOf ChangeContent
            End Sub

            Sub ChangeContent()
                'Fires every half-second
                If ObjectControl.IsChecked = False Then
                    ObjectControl.IsChecked = True
                Else
                    ObjectControl.IsChecked = False
                End If
            End Sub
        End Class
    End Class
End Namespace
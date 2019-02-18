
Namespace Classes.Entities

    Partial Public MustInherit Class EntityClasses
        Public Class EntityEnemyD
            Inherits EntityEnemyBase

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
                If ObjectControl.IsChecked = False Then
                    ObjectControl.IsChecked = True
                Else
                    ObjectControl.IsChecked = False
                End If
            End Sub
        End Class
    End Class
End Namespace

Namespace Classes.Entities

    Partial Public MustInherit Class EntityClasses
        Public Class EntityEnemyD
            Inherits EntityEnemyBase

            Private Const ScoreValueConvoy As Integer = 60
            Private Const ScoreValueCharger As Integer = 120

            Private Shared ReadOnly EnemyDCollection As List(Of Object) =
                                          New List(Of Object)()

            Public Overrides Property ObjectControl As Object = New Viewbox() With {
                .Name = "EnemyD_" & EnemyDCollection.Count,
                .RenderTransform = ObjectTransformGroup,
                .RenderTransformOrigin = New Point(0, 0),
            .Height = ObjectHeight,
                .Width = ObjectWidth,
                                .Child = New CheckBox() With {
                                    .Content = "",
                                    .IsChecked = False,
                                    .FontFamily = New FontFamily("Segoe UI Symbol"),
                                    .FontSize = 8,
                                    .VerticalContentAlignment = VerticalAlignment.Top,
                                    .HorizontalContentAlignment = HorizontalAlignment.Center,
                                    .Padding = New Thickness(0, 0, 0, 0),
                                    .Background = New SolidColorBrush(Color.FromRgb(255, 255, 255)),
                                    .Foreground = New SolidColorBrush(Color.FromRgb(0, 0, 0)),
                                    .IsEnabled = False
                                    }
                }


            Sub New(localName As String, localLocation As Point)
                MyBase.New(localName, ScoreValueCharger,ScoreValueConvoy, localLocation)
                Application.SetCanvasLocation(
                    localLocation,
                    ObjectControl
                    )
            End Sub

            Protected Overrides Sub ChangeContent()
                'Fires every half-second
                If ObjectControl.Child.IsChecked = False Then
                    ObjectControl.Child.IsChecked = True
                Else
                    ObjectControl.Child.IsChecked = False
                End If
            End Sub
        End Class
    End Class
End Namespace

Namespace Classes.Entities

    Partial Public MustInherit Class EntityClasses
        Public Class EntityEnemyC
            Inherits EntityEnemyBase

            Private Const ScoreValueConvoy As Integer = 50
            Private Const ScoreValueCharger As Integer = 100

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
                                .Background = New SolidColorBrush(Color.FromRgb(0, 0, 0)),
                                .Foreground = New SolidColorBrush(Color.FromRgb(0, 255, 0)),
                .IsEnabled = False
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
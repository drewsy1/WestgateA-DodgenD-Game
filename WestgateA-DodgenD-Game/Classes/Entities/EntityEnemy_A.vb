
Namespace Classes.Entities

    Partial Public MustInherit Class EntityClasses
        Public Class EntityEnemyA
            Inherits EntityEnemyBase

            Private Const ScoreValueConvoy As Integer = 30
            Private Const ScoreValueCharger As Integer = 60

            Private Shared ReadOnly EnemyACollection As List(Of Object) =
                                          New List(Of Object)()

            Public Overrides Property ObjectControl As Object = New Button() With {
                                .Name = "EnemyA_" & EnemyACollection.Count,
                                .Height = ObjectHeight,
                                .Width = ObjectWidth,
                                .RenderTransform = ObjectTransformGroup,
                                .RenderTransformOrigin = New Point(0, 0),
                                .Content = "😠",
                                .FontFamily = New FontFamily("Segoe UI Symbol"),
                                .FontSize = 20,
                                .VerticalContentAlignment = VerticalAlignment.Center,
                                .Padding = New Thickness(0),
                                .IsEnabled = False,
                                .FontWeight = FontWeights.UltraBold,
                .Foreground = New SolidColorBrush(Color.FromRgb(0, 0, 0))
                }

            'Public Shadows WithEvents ObjectHitbox As Hitbox

            Sub New(localName As String, localLocation As Point)
                MyBase.New(localName, ScoreValueCharger, ScoreValueConvoy, localLocation)
                Application.SetCanvasLocation(
                    localLocation,
                    ObjectControl
                    )
            End Sub

            Protected Overrides Sub ChangeContent()
                If ObjectControl.Content = "😠" Then
                    ObjectControl.Content = "😧"
                    ObjectControl.Foreground = New SolidColorBrush(Color.FromRgb(255, 0, 0))
                Else
                    ObjectControl.Content = "😠"
                    ObjectControl.Foreground = New SolidColorBrush(Color.FromRgb(0, 0, 0))
                End If
            End Sub
        End Class
    End Class
End Namespace
﻿
Namespace Classes.Entities

    Partial Public MustInherit Class EntityClasses
        Public Class EntityEnemyA
            Inherits EntityEnemy

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
                                .FontWeight = FontWeights.UltraBold
                }

            'Public Shadows WithEvents ObjectHitbox As Hitbox

            Sub New(localLocationX As Double, localLocationY As Double)
                MyBase.New()
                MainWindowWrapper.SetCanvasLocation(
                    localLocationX,
                    localLocationY,
                    ObjectControl
                    )
            End Sub

            Overrides Sub ChangeContent()
                If ObjectControl.Content = "😠" Then
                    ObjectControl.Content = "😧"
                Else
                    ObjectControl.Content = "😠"
                End If
            End Sub
        End Class
    End Class
End Namespace
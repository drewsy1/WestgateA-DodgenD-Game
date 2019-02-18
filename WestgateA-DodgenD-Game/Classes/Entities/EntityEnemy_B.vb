﻿
Namespace Classes.Entities

    Partial Public MustInherit Class EntityClasses
        Public Class EntityEnemyB
            Inherits EntityEnemy

            Private Shared ReadOnly EnemyBCollection As List(Of Object) =
                                          New List(Of Object)()

            Public Overrides Property ObjectControl As Object = New TextBox() With {
                                .Name = "EnemyB_" & EnemyBCollection.Count,
                                .Height = ObjectHeight,
                                .Width = ObjectWidth,
                                .RenderTransform = ObjectTransformGroup,
                                .RenderTransformOrigin = New Point(0, 0),
                                .Text = "☢",
                                .FontFamily = New FontFamily("Segoe UI Symbol"),
                                .FontSize = 36,
                                .VerticalContentAlignment = VerticalAlignment.Center,
                                .Padding = New Thickness(-2.5, -11.5, 0, 0),
                                .IsReadOnly = True,
                                .Background = New SolidColorBrush(Color.FromRgb(255, 255, 0)),
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
                If ObjectControl.Background.Color = Color.FromRgb(255, 255, 0) Then
                    ObjectControl.Background.Color = Color.FromRgb(0, 0, 0)
                    ObjectControl.Foreground.Color = Color.FromRgb(255, 255, 0)
                Else
                    ObjectControl.Background.Color = Color.FromRgb(255, 255, 0)
                    ObjectControl.Foreground.Color = Color.FromRgb(0, 0, 0)
                End If
            End Sub
        End Class
    End Class
End Namespace
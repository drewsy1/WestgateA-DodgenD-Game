Imports WestgateA_DodgenD_Game

Namespace Classes
    Public Class Projectile
        Friend Shared ReadOnly ProjectilesCollection As List(Of Projectile) = New List(Of Projectile)()
        Private Const ProjectileHeight As Double = 27
        Private Const ProjectileWidth As Double = 3
        Private Const ProjectileDirection As Double = -1
        Friend ReadOnly _ProjectileTransformTranslate As TranslateTransform = New TranslateTransform() With {.X = 0, .Y = 0}
        Friend ReadOnly _ProjectileTransform As TransformGroup = New TransformGroup()
        Friend ProjectileRectangle As Rectangle = New Rectangle() With {
            .Height = ProjectileHeight,
            .Width = ProjectileWidth,
            .StrokeThickness = 0
        }

        Sub New()

        End Sub

        Sub UpdateLocation()
            _ProjectileTransformTranslate.Y += (10 * ProjectileDirection)
        End Sub
    End Class

    Public Class ProjectilePlayer
        Inherits Projectile
        Private Const ProjectileDirection As Double = -1

        Sub New()
            _ProjectileTransform.Children.Add(_ProjectileTransformTranslate)
            ProjectileRectangle.Fill = New SolidColorBrush(Colors.LawnGreen)
            ProjectileRectangle.RenderTransform = _ProjectileTransform
            ProjectilesCollection.Add(Me)
        End Sub

        Sub AddToCanvas(location As Double)
            Dim mw As MainWindow = Application.Current.MainWindow
            Canvas.SetLeft(ProjectileRectangle, 316.5)
            Canvas.SetTop(ProjectileRectangle, 636)
            _ProjectileTransformTranslate.X += location
            mw.CanvasGameScreen.Children.Add(ProjectileRectangle)
        End Sub
    End Class

    Public Class ProjectileEnemy
        Inherits Projectile
        Private Const ProjectileDirection As Double = 1

        Sub New()
            _ProjectileTransform.Children.Add(_ProjectileTransformTranslate)
            ProjectileRectangle.Fill = New SolidColorBrush(Colors.Red)
            ProjectileRectangle.RenderTransform = _ProjectileTransform
            ProjectilesCollection.Add(Me)
        End Sub

        Sub AddToCanvas(location As Double)
            Dim mw As MainWindow = Application.Current.MainWindow
            Canvas.SetLeft(ProjectileRectangle, 316.5)
            Canvas.SetTop(ProjectileRectangle, 100)
            _ProjectileTransformTranslate.X += location
            mw.CanvasGameScreen.Children.Add(ProjectileRectangle)
        End Sub
    End Class
End Namespace
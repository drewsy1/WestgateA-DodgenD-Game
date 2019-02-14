Imports System.Drawing

Namespace Classes
    Public MustInherit Class Projectile
        Private Shared ReadOnly MainWindowInstance As MainWindow = Application.Current.MainWindow 'Get the MainWindow as an object
        Friend Shared ReadOnly ProjectilesCollection As List(Of Projectile) = New List(Of Projectile)() ' Create a static collection of projectiles to keep track of them
        Private Const ProjectileHeight As Double = 27 ' Default projectile height
        Private Const ProjectileWidth As Double = 3 ' Default projectile width
        Protected Overridable Property ProjectileDirection As Double ' Overridable double representing direction of projectile travel

        Private ReadOnly _projectileTransformTranslate As TranslateTransform = New TranslateTransform() With {.X = 0, .Y = 0} ' Transform object that will be added to the projectile. This object's X and Y properties will move the projectile

        ' TransformGroup containing _projectileTransformTranslate, will be added to ProjectileRectangle
        Private ReadOnly _projectileTransform As TransformGroup = New TransformGroup() With {
            .Children = New TransformCollection(New Transform() {_projectileTransformTranslate})
        }

        ' Rectangle object that serves as projectile
        Private ReadOnly _projectileRectangle As Shapes.Rectangle = New Shapes.Rectangle() With {
            .Height = ProjectileHeight,
            .Width = ProjectileWidth,
            .StrokeThickness = 0,
            .RenderTransform = _projectileTransform,
            .RenderTransformOrigin = New Windows.Point(0, 0)
        }

        Private _projectileRectangleHitBox As Rectangle = New Rectangle() With {
            .Height = ProjectileHeight,
            .Width = ProjectileWidth
        }

        Protected Sub New()
            ProjectilesCollection.Add(Me)
        End Sub

        Protected Sub SetColor(projectileColor As Media.Color)
            _projectileRectangle.Fill = New SolidColorBrush(projectileColor)
        End Sub

        Sub UpdateLocation()
            _projectileTransformTranslate.Y += (10 * ProjectileDirection)
            If _projectileRectangleHitBox.Y <= MainWindowInstance.CanvasGameScreen.Height Then
                Remove()
            End If
        End Sub

        Protected Sub AddToCanvas(location As Double, transformX As Double, transformY As Double)
            _projectileTransformTranslate.X += location
            Canvas.SetLeft(_projectileRectangle, transformX)
            Canvas.SetBottom(_projectileRectangle, transformY)
            _projectileRectangleHitBox.X = location
            _projectileRectangleHitBox.Y = transformY

            MainWindowInstance.CanvasGameScreen.Children.Add(_projectileRectangle)
        End Sub

        Private Sub Remove()
            MainWindowInstance.CanvasGameScreen.Children.Remove(_projectileRectangle)
            Dim itemIndex As Integer = ProjectilesCollection.IndexOf(Me)
            ProjectilesCollection(itemIndex) = Nothing
        End Sub

    End Class

    Public Class ProjectilePlayer
        Inherits Projectile
        Protected Overrides Property ProjectileDirection As Double = -1
        Shared Shadows ReadOnly ProjectileColor As Media.Color = Colors.LawnGreen
        Private Const TransformXDefault As Double = 316.5
        Private Const TransformYDefault As Double = 132

        Sub New()
            MyBase.New()
            SetColor(ProjectileColor)
        End Sub

        Shadows Sub AddToCanvas(location As Double, Optional transformX As Double = TransformXDefault, Optional transformY As Double = TransformYDefault)
            MyBase.AddToCanvas(location, transformX, transformY)
        End Sub
    End Class

    Public Class ProjectileEnemy
        Inherits Projectile
        Protected Overrides Property ProjectileDirection As Double = 1
        Shared Shadows ReadOnly ProjectileColor As Media.Color = Colors.Red
        Private Const TransformXDefault As Double = 316.5
        Private Const TransformYDefault As Double = 768

        Sub New()
            MyBase.New()
            SetColor(ProjectileColor)
        End Sub

        Shadows Sub AddToCanvas(location As Double, Optional transformX As Double = TransformXDefault, Optional transformY As Double = TransformYDefault)
            MyBase.AddToCanvas(location, transformX, transformY)
        End Sub
    End Class
End Namespace
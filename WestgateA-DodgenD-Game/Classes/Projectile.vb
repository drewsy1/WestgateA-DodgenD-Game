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
        Private ReadOnly _projectileRectangle As Rectangle = New Rectangle() With {
            .Height = ProjectileHeight,
            .Width = ProjectileWidth,
            .StrokeThickness = 0,
            .RenderTransform = _projectileTransform
        }

        Protected Sub New()
            ProjectilesCollection.Add(Me)
        End Sub

        Protected Sub SetColor(projectileColor As Color)
            _projectileRectangle.Fill = New SolidColorBrush(projectileColor)
        End Sub

        Sub UpdateLocation()
            _projectileTransformTranslate.Y += (10 * ProjectileDirection)
            'If
        End Sub

        Protected Overridable Sub AddToCanvas(location As Double, transformLeft As Double, transformTop As Double)
            Canvas.SetLeft(_projectileRectangle, transformLeft)
            Canvas.SetTop(_projectileRectangle, transformTop)
            _projectileTransformTranslate.X += location
            MainWindowInstance.CanvasGameScreen.Children.Add(_projectileRectangle)
        End Sub
    End Class

    Public Class ProjectilePlayer
        Inherits Projectile
        Protected Overrides Property ProjectileDirection As Double = -1
        Shared Shadows ReadOnly ProjectileColor As Color = Colors.LawnGreen
        Private Const TransformLeftDefault As Double = 316.5
        Private Const TransformTopDefault As Double = 636

        Sub New()
            MyBase.New()
            SetColor(ProjectileColor)
        End Sub

        Shadows Sub AddToCanvas(location As Double, Optional transformLeft As Double = TransformLeftDefault, Optional transformTop As Double = TransformTopDefault)
            MyBase.AddToCanvas(location, transformLeft, transformTop)
        End Sub
    End Class

    Public Class ProjectileEnemy
        Inherits Projectile
        Protected Overrides Property ProjectileDirection As Double = 1
        Shared Shadows ReadOnly ProjectileColor As Color = Colors.Red
        Private Const TransformLeftDefault As Double = 316.5
        Private Const TransformTopDefault As Double = 0

        Sub New()
            MyBase.New()
            SetColor(ProjectileColor)
        End Sub

        Shadows Sub AddToCanvas(location As Double, Optional transformLeft As Double = TransformLeftDefault, Optional transformTop As Double = TransformTopDefault)
            MyBase.AddToCanvas(location, transformLeft, transformTop)
        End Sub
    End Class
End Namespace
Namespace Classes.Projectile
    ' ReSharper disable once ClassNeverInstantiated.Global
    Partial Public Class ProjectileClasses
        ''' <summary>
        ''' Base Projectile class, must be inherited by new classes
        ''' </summary>
        Public MustInherit Class ProjectileBase

            ''' <summary>
            ''' Default projectile height
            ''' </summary>
            Private Const ProjectileHeight As Double = 27

            ''' <summary>
            ''' Default projectile width
            ''' </summary>
            Private Const ProjectileWidth As Double = 3

            ''' <summary>
            ''' Default projectile speed
            ''' </summary>
            Private Const MovementSpeed As Double = 15

            ''' <summary>
            ''' Overridable double representing direction of projectile travel
            ''' </summary>
            ''' <returns>ProjectileDirection</returns>
            Protected Overridable Property ProjectileDirection As Double

            ''' <summary>
            ''' Color of projectile
            ''' </summary>
            ''' <returns>ProjectileColor</returns>
            Protected Overridable Property ProjectileColor As Color

            ''' <summary>
            ''' Transform object that will be added to the projectile.
            ''' (This object's X and Y properties will move the projectile)
            ''' </summary>
            Private ReadOnly _projectileTransformTranslate As TranslateTransform =
                                 New TranslateTransform() With {.X = 0, .Y = 0}

            ''' <summary>
            ''' TransformGroup containing _projectileTransformTranslate
            ''' (will be added to ProjectileRectangle)
            ''' </summary>
            Private ReadOnly _projectileTransform As TransformGroup =
                New TransformGroup() With {
                    .Children = New TransformCollection(
                        New Transform() {_projectileTransformTranslate}
                        )
                }

            ''' <summary>
            ''' Shapes.Rectangle object that serves as projectile
            ''' </summary>
            Private ReadOnly _projectileRectangle As Rectangle = New Rectangle() With {
                .Height = ProjectileHeight,
                .Width = ProjectileWidth,
                .StrokeThickness = 0,
                .RenderTransform = _projectileTransform,
                .RenderTransformOrigin = New Point(0, 0)
                }

            Private WithEvents _projectileHitbox As Hitbox

            ''' <summary>
            ''' Instantiates a new Projectile object and adds it to ProjectilesCollection
            ''' </summary>
            ''' <param name="translateX">X-axis translation (x coordinate +/- pixels)</param>
            ''' <param name="locationX">Object's starting X-coordinate</param>
            ''' <param name="locationY">Object's starting Y-coordinate</param>
            Protected Sub New(translateX As Double,
                              locationX As Double,
                              locationY As Double)

                ProjectilesCollection.Add(Me)

                ' Increment projectile's X transform value by translateX
                _projectileTransformTranslate.X += translateX

                ' Set coordinates on canvas for projectile
                Canvas.SetLeft(_projectileRectangle, locationX)
                Canvas.SetBottom(_projectileRectangle, locationY)

                _projectileHitbox = New Hitbox(ProjectileWidth,
                                               ProjectileHeight,
                                               locationX + translateX,
                                               locationY)

                AddHandler _projectileHitbox.LeavingCanvas, AddressOf Remove
            End Sub

            ''' <summary>
            ''' Sets fill color for projectile
            ''' </summary>
            ''' <param name="newProjectileColor">Desired fill color for projectile</param>
            Protected Sub SetColor(newProjectileColor As Color)
                _projectileRectangle.Fill = New SolidColorBrush(newProjectileColor)
            End Sub

            ''' <summary>
            ''' Moves projectile based on ProjectileDirection and removes it if needed
            ''' </summary>
            Sub UpdateLocation()
                _projectileTransformTranslate.Y += (MovementSpeed * ProjectileDirection)
                _projectileHitbox.MoveY(MovementSpeed * ProjectileDirection * -1)
            End Sub

            ''' <summary>
            ''' Adds projectile to canvas
            ''' </summary>
            Sub AddToCanvas()
                MainWindowWrapper.MainWindowInstance.CanvasGameScreen.Children.Add(
                    _projectileRectangle)
            End Sub

            ''' <summary>
            ''' Removes projectile from canvas, clears the hit box object,
            ''' and removes projectile from ProjectilesCollection
            ''' </summary>
            Private Sub Remove()
                ' Remove rectangle from CanvasGameScreen (make it invisible)
                MainWindowWrapper.MainWindowInstance.CanvasGameScreen.Children.Remove(
                    _projectileRectangle)

                _projectileHitbox = Nothing

                Dim itemIndex As Integer = ProjectilesCollection.IndexOf(Me)
                If itemIndex >= 0 Then
                    ProjectilesCollection(itemIndex) = Nothing
                End If
            End Sub
        End Class
    End Class
End Namespace
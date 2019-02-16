Imports WestgateA_DodgenD_Game.Interfaces

Namespace Classes.Projectile
    ' ReSharper disable once ClassNeverInstantiated.Global
    Partial Public Class ProjectileClasses
        ''' <summary>
        ''' Base Projectile class, must be inherited by new classes
        ''' </summary>
        Public MustInherit Class ProjectileBase
            Inherits CanvasObjects
            Implements ICanvasObjects

            Protected Overrides Property ObjectHeight As Double = 27 Implements ICanvasObjects.ObjectHeight
            Protected Overrides Property ObjectWidth As Double = 3 Implements ICanvasObjects.ObjectWidth
            ' ReSharper disable UnassignedGetOnlyAutoProperty
            Protected Overrides ReadOnly Property LocationXDefault As Double Implements ICanvasObjects.LocationXDefault
            Protected Overrides ReadOnly Property LocationYDefault As Double Implements ICanvasObjects.LocationYDefault
            Protected Overrides Property LocationX As Double Implements ICanvasObjects.LocationX
            Protected Overrides Property LocationY As Double Implements ICanvasObjects.LocationY
            Protected Overrides Property TranslateBoundLeft As Double Implements ICanvasObjects.TranslateBoundLeft
            Protected Overrides Property TranslateBoundRight As Double Implements ICanvasObjects.TranslateBoundRight
            Protected Overrides Property TranslateBoundTop As Double Implements ICanvasObjects.TranslateBoundTop
            Protected Overrides Property TranslateBoundBottom As Double Implements ICanvasObjects.TranslateBoundBottom
            Protected Overrides Property MovementSpeed As Double = 30 Implements ICanvasObjects.MovementSpeed
            Protected Overrides Property ObjectTransformTranslate As TranslateTransform =
                New TranslateTransform() With {.X = 0, .Y = 0} Implements ICanvasObjects.ObjectTransformTranslate
            Protected Overrides Property ObjectTransformGroup As TransformGroup =
                New TransformGroup() With {
                    .Children = New TransformCollection(
                        New Transform() {ObjectTransformTranslate}
                        )
                    } Implements ICanvasObjects.ObjectTransformGroup
            Public Overrides Property ObjectControl As Object = New Rectangle() With {
                .Height = ObjectHeight,
                .Width = ObjectWidth,
                .StrokeThickness = 0,
                .RenderTransform = ObjectTransformGroup,
                .RenderTransformOrigin = New Point(0, 0)
                } Implements ICanvasObjects.ObjectControl

            ''' <summary>
            ''' Overrides double representing direction of projectile travel
            ''' </summary>
            ''' <returns>ProjectileDirection</returns>
            Protected Overridable Property ProjectileDirection As Double

            ''' <summary>
            ''' Color of projectile
            ''' </summary>
            ''' <returns>ProjectileColor</returns>
            Protected Overridable Property ProjectileColor As Color


            Public Overrides Sub Remove() Implements ICanvasObjects.Remove
                ' Remove rectangle from CanvasGameScreen (make it invisible)
                MainWindowWrapper.MainWindowInstance.CanvasGameScreen.Children.Remove(
                    ObjectControl)

                Hitbox.HitboxCollection.Remove(ObjectHitbox)
                ObjectHitbox = Nothing

                Dim itemIndex As Integer = ProjectilesCollection.IndexOf(Me)
                If itemIndex >= 0 Then
                    ProjectilesCollection(itemIndex) = Nothing
                End If
                Finalize()
            End Sub

            ''' <summary>
            ''' Instantiates a new Projectile object and adds it to ProjectilesCollection
            ''' </summary>
            ''' <param name="translateX">X-axis translation (x coordinate +/- pixels)</param>
            ''' <param name="localLocationX">Object's starting X-coordinate</param>
            ''' <param name="localLocationY">Object's starting Y-coordinate</param>
            Protected Sub New(translateX As Double,
                              translateY As Double,
                              Optional localLocationX As Double = Nothing,
                              Optional localLocationY As Double = Nothing)
                MyBase.New(localLocationX, localLocationY + translateY)
                ProjectilesCollection.Add(Me)

                ObjectHitbox = CreateHitbox()

                ' Set coordinates on canvas for projectile
                MainWindowWrapper.SetCanvasLocation(localLocationX, localLocationY, ObjectControl)

                ' Increment projectile's X transform value by translateX
                ObjectTransformTranslate.X += translateX
                ObjectTransformTranslate.Y += translateY

                AddHandler ObjectHitbox.LeavingCanvas, AddressOf Remove
                AddHandler GameTimer.Tick, AddressOf UpdateLocation
            End Sub

            ''' <summary>
            ''' Sets fill color for projectile
            ''' </summary>
            ''' <param name="newProjectileColor">Desired fill color for projectile</param>
            Protected Sub SetColor(newProjectileColor As Color)
                ObjectControl.Fill = New SolidColorBrush(newProjectileColor)
            End Sub

            ''' <summary>
            ''' Moves projectile based on ProjectileDirection and removes it if needed
            ''' </summary>
            Sub UpdateLocation()

                TranslateY(MovementSpeed * ProjectileDirection)
                'ObjectTransformTranslate.Y += (MovementSpeed * ProjectileDirection)
                'ObjectHitbox.MoveY(MovementSpeed * ProjectileDirection * -1)
            End Sub

        End Class


    End Class
End Namespace
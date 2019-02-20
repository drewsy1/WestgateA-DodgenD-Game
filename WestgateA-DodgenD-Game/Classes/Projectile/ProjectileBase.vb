Imports WestgateA_DodgenD_Game.Classes.Entities
Imports WestgateA_DodgenD_Game.Interfaces

Namespace Classes.Projectile

    ' ReSharper disable once ClassNeverInstantiated.Global
    Partial Public Class ProjectileClasses

        ''' <summary>
        ''' Create a static collection of projectiles to keep track of them
        ''' </summary>
        Public Shared ReadOnly ProjectilesCollection As List(Of ProjectileBase) =
                                   New List(Of ProjectileBase)()

        ''' <summary>
        ''' Checks for active projectiles and moves/removes them as needed
        ''' </summary>
        Public Shared Sub UpdateProjectiles()
            'ToDo Is there any better way to do this? Feels clunky
            If ProjectilesCollection.Count > 0 Then
                For Each obj As ProjectileBase In ProjectilesCollection.ToArray()
                    If Not IsNothing(obj) Then
                        obj.UpdateLocation()
                    End If
                Next
            End If
        End Sub

        ''' <summary>
        ''' Base Projectile class, must be inherited by new classes
        ''' </summary>
        Public MustInherit Class ProjectileBase
            Implements ICanvasObjects

#Region "Implemented From ICanvasObjects"

            Public Property ObjectName As String Implements ICanvasObjects.ObjectName
            Public Property ObjectScoreValue As Integer Implements ICanvasObjects.ObjectScoreValue
            Public Property ObjectHeight As Double = 27 Implements ICanvasObjects.ObjectHeight
            Public Property ObjectWidth As Double = 3 Implements ICanvasObjects.ObjectWidth

            Protected ReadOnly Property ObjectPointLowerLeft As Point Implements ICanvasObjects.ObjectPointLowerLeft
                Get
                    Return LocationCoords + New Point(ObjectTransform_Translate.X, 0 - ObjectTransform_Translate.Y)
                End Get
            End Property

            Protected ReadOnly Property ObjectPointUpperRight As Point Implements ICanvasObjects.ObjectPointUpperRight
                Get
                    Return New Point(ObjectPointLowerLeft.X + ObjectWidth, ObjectPointLowerLeft.Y + ObjectHeight)
                End Get
            End Property

            Public Property ObjectPointLowerLeftStart As Point
            Public Property ObjectPointUpperRightStart As Point

            ' ReSharper disable UnassignedGetOnlyAutoProperty
            Protected Overridable ReadOnly Property LocationCoordsDefault As Point Implements ICanvasObjects.LocationCoordsDefault

            Protected Property LocationCoords As Point Implements ICanvasObjects.LocationCoords
            Public Property TranslateBoundLeft As Double Implements ICanvasObjects.TranslateBoundLeft
            Public Property TranslateBoundRight As Double Implements ICanvasObjects.TranslateBoundRight
            Public Property TranslateBoundTop As Double Implements ICanvasObjects.TranslateBoundTop
            Public Property TranslateBoundBottom As Double Implements ICanvasObjects.TranslateBoundBottom
            Protected Property MovementSpeed As Double = 30 Implements ICanvasObjects.MovementSpeed

            Public Property ObjectTransform_Translate As TranslateTransform =
                New TranslateTransform() With {.X = 0, .Y = 0} Implements ICanvasObjects.ObjectTransform_Translate

            Protected Property ObjectTransformGroup As TransformGroup =
                New TransformGroup() With {
                    .Children = New TransformCollection(
                        New Transform() {ObjectTransform_Translate}
                        )
                    } Implements ICanvasObjects.ObjectTransformGroup

            Public Property ObjectControl As Object = New Rectangle() With {
                .Height = ObjectHeight,
                .Width = ObjectWidth,
                .StrokeThickness = 0,
                .RenderTransform = ObjectTransformGroup,
                .RenderTransformOrigin = New Point(0, 0)
                } Implements ICanvasObjects.ObjectControl

            Public Sub MoveLeft(Optional localMovementSpeed As Double = 0) Implements ICanvasObjects.MoveLeft
                If (localMovementSpeed.Equals(0) And MovementSpeed) Then
                    localMovementSpeed = MovementSpeed
                End If
                CanvasObjects.TranslateX(Me,localMovementSpeed * -1)
            End Sub

            Public Sub MoveRight(Optional localMovementSpeed As Double = 0) Implements ICanvasObjects.MoveRight
                If (localMovementSpeed.Equals(0) And MovementSpeed) Then
                    localMovementSpeed = MovementSpeed
                End If
                CanvasObjects.TranslateX(Me,localMovementSpeed)
            End Sub

            Public Sub MoveUp(Optional localMovementSpeed As Double = 0) Implements ICanvasObjects.MoveUp
                If (localMovementSpeed.Equals(0) And MovementSpeed) Then
                    localMovementSpeed = MovementSpeed
                End If
                CanvasObjects.TranslateY(Me,localMovementSpeed)
            End Sub

            Public Sub MoveDown(Optional localMovementSpeed As Double = 0) Implements ICanvasObjects.MoveDown
                If (localMovementSpeed.Equals(0) And MovementSpeed) Then
                    localMovementSpeed = MovementSpeed
                End If
                CanvasObjects.TranslateY(Me,localMovementSpeed * -1)
            End Sub

#End Region

            ''' <summary>
            ''' Overridable double representing direction of projectile travel
            ''' </summary>
            ''' <returns>ProjectileDirection</returns>
            Protected Overridable ReadOnly Property ProjectileDirection As Double

            ''' <summary>
            ''' Color of projectile
            ''' </summary>
            ''' <returns>ProjectileColor</returns>
            Protected Overridable Property ProjectileColor As Color

            ''' <summary>
            ''' Instantiates a new Projectile object and adds it to ProjectilesCollection
            ''' </summary>
            ''' <param name="translateX">X-axis translation (x coordinate +/- pixels)</param>
            ''' <param name="localLocationCoords">Object's starting point</param>
            Protected Sub New(translateX As Double,
                              translateY As Double,
                              Optional localLocationCoords As Point = Nothing)
                LocationCoords = localLocationCoords
                ObjectPointLowerLeftStart = New Point(ObjectPointLowerLeft.X,ObjectPointLowerLeft.Y)
                ObjectPointUpperRightStart = New Point(ObjectPointUpperRight.X,ObjectPointUpperRight.Y)

                TranslateBoundLeft = CanvasObjects.GetTranslateBoundLeft(LocationCoords.X, ObjectWidth)
                TranslateBoundRight = CanvasObjects.GetTranslateBoundRight(LocationCoords.X, ObjectWidth) 
                TranslateBoundTop = CanvasObjects.GetTranslateBoundTop(LocationCoords.Y, ObjectHeight)
                TranslateBoundBottom = CanvasObjects.GetTranslateBoundBottom(LocationCoords.Y, ObjectHeight)

                CanvasObjects.ObjectCollection.Add(Me)

                ProjectilesCollection.Add(Me)

                ' Set coordinates on canvas for projectile
                Application.SetCanvasLocation(localLocationCoords, ObjectControl)

                ' Increment projectile's X transform value by translateX
                ObjectTransform_Translate.X += translateX
                ObjectTransform_Translate.Y += translateY

                AddHandler GameTimer.Tick, AddressOf UpdateLocation
            End Sub

            Public Overridable Sub Remove() Implements ICanvasObjects.Remove
                RemoveHandler GameTimer.Tick, AddressOf UpdateLocation
                ' Remove rectangle from CanvasGameScreen (make it invisible)
                Application.MainWindowInstance.CanvasGameScreen.Children.Remove(
                    ObjectControl)

                Dim itemIndex As Integer = ProjectilesCollection.IndexOf(Me)
                If itemIndex >= 0 Then
                    ProjectilesCollection.RemoveAt(itemIndex)
                End If
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

                CanvasObjects.TranslateY(Me,MovementSpeed * ProjectileDirection)

                For Each enemy As EntityClasses.EntityEnemyBase In Application.EnemyCollection
                    If CanvasObjects.CheckCollision(enemy, Me) And enemy.ObjectEnabled Then
                        Application.RaiseProjectileHit(Me, enemy)
                    End If
                Next

                If (ObjectPointUpperRight.X >= ObjectPointUpperRightStart.X + TranslateBoundRight) Or (ObjectPointUpperRight.Y >= ObjectPointUpperRightStart.Y + TranslateBoundTop) Or 
                   (ObjectPointLowerLeft.X <= ObjectPointLowerLeftStart.X + TranslateBoundLeft) Or (ObjectPointLowerLeft.Y <= ObjectPointLowerLeftStart.Y + TranslateBoundBottom) Then
                    Remove()
                End If
                'ObjectTransform_Translate.Y += (MovementSpeed * ProjectileDirection)
                'ObjectHitbox.MoveY(MovementSpeed * ProjectileDirection * -1)
            End Sub

        End Class

    End Class

End Namespace
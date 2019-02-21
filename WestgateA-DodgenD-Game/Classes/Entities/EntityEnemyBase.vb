Imports System.ComponentModel
Imports WestgateA_DodgenD_Game.Classes.Projectile
Imports WestgateA_DodgenD_Game.Interfaces

Namespace Classes.Entities

    ' ReSharper disable once ClassNeverInstantiated.Global
    ''' <summary>
    ''' Class containing Projectile classes and meta-properties
    ''' </summary>
    Partial Public Class EntityClasses

        Public Class EntityEnemyBase
            Implements ICanvasObjects

#Region "Inherited properties"

            Public Property LocationCoords As Point Implements ICanvasObjects.LocationCoords

            Protected ReadOnly Property LocationCoordsDefault As Point Implements ICanvasObjects.LocationCoordsDefault
                Get
                    Return New Point(Application.CanvasWidth / 2, Application.CanvasHeight - 106)
                End Get
            End Property

            Protected Property MovementSpeed As Double = 5 Implements ICanvasObjects.MovementSpeed
            Public Overridable Property ObjectControl As Object = New UIElement() Implements ICanvasObjects.ObjectControl
            Public Property ObjectHeight As Double = 33 Implements ICanvasObjects.ObjectHeight
            Public Property ObjectName As String Implements ICanvasObjects.ObjectName

            Public ReadOnly Property ObjectPointLowerLeft As Point Implements ICanvasObjects.ObjectPointLowerLeft
                Get
                    Return LocationCoords
                End Get
            End Property

            Public ReadOnly Property ObjectPointUpperRight As Point Implements ICanvasObjects.ObjectPointUpperRight
                Get
                    Return New Point(ObjectPointLowerLeft.X + ObjectWidth, ObjectPointLowerLeft.Y + ObjectHeight)
                End Get
            End Property

            Public Property ObjectScoreValue As Integer Implements ICanvasObjects.ObjectScoreValue
            Public Property ObjectWidth As Double = 33 Implements ICanvasObjects.ObjectWidth
            Protected Property TranslateBoundBottom As Double Implements ICanvasObjects.TranslateBoundBottom
            Protected Property TranslateBoundLeft As Double = CanvasObjects.GetTranslateBoundLeft(LocationCoords.X, ObjectWidth) Implements ICanvasObjects.TranslateBoundLeft

            Protected Property TranslateBoundRight As Double = CanvasObjects.GetTranslateBoundRight(LocationCoords.X, ObjectWidth) Implements ICanvasObjects.TranslateBoundRight

            Protected Property TranslateBoundTop As Double Implements ICanvasObjects.TranslateBoundTop
            Protected Property ObjectTransform_Translate As TranslateTransform = New TranslateTransform() With {.X = 0, .Y = 0} Implements ICanvasObjects.ObjectTransform_Translate

            Protected Property ObjectTransformGroup As TransformGroup =
                New TransformGroup() With {
                    .Children = New TransformCollection(New Transform() {ObjectTransform_Translate})
                    } Implements ICanvasObjects.ObjectTransformGroup

            ''' <summary>
            ''' Moves entity down if entity is within bounds
            ''' </summary>
            ''' <param name="localMovementSpeed">Number of pixels to move down (defaults to 0 unless MovementSpeed is set)</param>
            Public Sub MoveDown(Optional localMovementSpeed As Double = 0) Implements ICanvasObjects.MoveDown
                If (localMovementSpeed.Equals(0) And MovementSpeed) Then
                    localMovementSpeed = MovementSpeed
                End If
                CanvasObjects.TranslateY(Me, localMovementSpeed * -1)
            End Sub

            ''' <summary>
            ''' Moves entity left if entity is within bounds
            ''' </summary>
            ''' <param name="localMovementSpeed">Number of pixels to move left (defaults to 0 unless MovementSpeed is set)</param>
            Public Sub MoveLeft(Optional localMovementSpeed As Double = 0) Implements ICanvasObjects.MoveLeft
                If (localMovementSpeed.Equals(0) And MovementSpeed) Then
                    localMovementSpeed = MovementSpeed
                End If
                CanvasObjects.TranslateX(Me, localMovementSpeed * -1)
            End Sub

            ''' <summary>
            ''' Moves entity right if entity is within bounds
            ''' </summary>
            ''' <param name="localMovementSpeed">Number of pixels to move right (defaults to 0 unless MovementSpeed is set)</param>
            Public Sub MoveRight(Optional localMovementSpeed As Double = 0) Implements ICanvasObjects.MoveRight
                If (localMovementSpeed.Equals(0) And MovementSpeed) Then
                    localMovementSpeed = MovementSpeed
                End If
                CanvasObjects.TranslateX(Me, localMovementSpeed)
            End Sub

            ''' <summary>
            ''' Moves entity up if entity is within bounds
            ''' </summary>
            ''' <param name="localMovementSpeed">Number of pixels to move up (defaults to 0 unless MovementSpeed is set)</param>
            Public Sub MoveUp(Optional localMovementSpeed As Double = 0) Implements ICanvasObjects.MoveUp
                If (localMovementSpeed.Equals(0) And MovementSpeed) Then
                    localMovementSpeed = MovementSpeed
                End If
                CanvasObjects.TranslateY(Me, localMovementSpeed)
            End Sub

            Public Overloads Sub Remove() Implements ICanvasObjects.Remove
                ' Remove rectangle from CanvasGameScreen (make it invisible)
                Application.MainWindowInstance.CanvasGameScreen.Children.Remove(ObjectControl)
                ObjectEnabled = False

            End Sub

#End Region

            ''' <summary>
            ''' Instantiates a new Entity object with matching hitbox and adds it to ObjectCollection
            ''' </summary>
            ''' <param name="localName"></param>
            ''' <param name="localScoreValue"></param>
            ''' <param name="localLocationCoords"></param>
            Sub New(localName As String, localScoreValue As Integer, Optional localLocationCoords As Point = Nothing)
                If IsNothing(localLocationCoords) Then localLocationCoords = LocationCoordsDefault
                ObjectName = localName
                ObjectScoreValue = localScoreValue
                LocationCoords = localLocationCoords
                ObjectEnabled = True

                TranslateBoundBottom = CanvasObjects.GetTranslateBoundBottom(LocationCoords.Y, ObjectHeight)
                TranslateBoundTop = CanvasObjects.GetTranslateBoundTop(LocationCoords.Y, ObjectHeight)
                TranslateBoundLeft = CanvasObjects.GetTranslateBoundLeft(LocationCoords.X, ObjectWidth)
                TranslateBoundRight = CanvasObjects.GetTranslateBoundRight(LocationCoords.X, ObjectWidth)

                CanvasObjects.ObjectCollection.Add(Me)
                Application.EnemyCollection.Add(Me)

                AddHandler GameTimer.LongTick, AddressOf ChangeContent
            End Sub

            Public Shared Event EnemyHit(enemy As EntityEnemyBase)

            ''' <summary>
            ''' ToDo Write EnemyType Summary
            ''' </summary>
            ''' <returns></returns>
            Public Property EnemyType As String = Me.GetType().Name

            ''' <summary>
            ''' Creates an enemy projectile that moves downward
            ''' </summary>
            Sub FireWeapon()
                EnemyProjectileInstance = New ProjectileClasses.ProjectileEnemy((ObjectWidth / 2), 0 - ObjectHeight, New Point(LocationCoords.X + ObjectTransform_Translate.X, LocationCoords.Y))
                Application.AddToCanvas(EnemyProjectileInstance)
            End Sub

            ''' <summary>
            ''' ToDo Write ChangeContent summary
            ''' </summary>
            Protected Overridable Sub ChangeContent()
            End Sub

            Private Overloads Shared Sub Remove(enemyRemoved As EntityEnemyBase)
                enemyRemoved.Remove()
                Debug.WriteLine(enemyRemoved)
            End Sub

        End Class

    End Class

End Namespace
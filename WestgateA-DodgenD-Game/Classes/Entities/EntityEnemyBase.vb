Imports System.ComponentModel
Imports WestgateA_DodgenD_Game.Classes.Projectile
Imports WestgateA_DodgenD_Game.Interfaces

Namespace Classes.Entities

    ' ReSharper disable once ClassNeverInstantiated.Global
    ''' <summary>
    ''' Class containing Projectile classes and meta-properties
    ''' </summary>
    Partial Public Class EntityClasses
        Implements INotifyPropertyChanged

#Region "Implementations from INotifyPropertyChanged"

        ''' <summary>
        ''' Todo Write PropertyChanged summary
        ''' </summary>
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        ''' <summary>
        ''' ToDo Write OnPropertyChanged summary
        ''' </summary>
        ''' <param name="name"></param>
        Protected Sub OnPropertyChanged(sender As Object, ByVal name As String)
            RaiseEvent PropertyChanged(sender, New PropertyChangedEventArgs(name))
        End Sub

#End Region

        Protected Shared _movementDirection As Integer = 1

        ''' <summary>
        ''' ToDo Write MovementDirection summary
        ''' </summary>
        ''' <returns></returns>
        Public Property MovementDirection As Integer
            Get
                Return _movementDirection
            End Get
            Set(value As Integer)
                _movementDirection = value
                OnPropertyChanged(Me,"MovementDirection")
            End Set
        End Property

        ''' <summary>
        ''' Enum that converts movement mode strings to their numeric representations
        ''' </summary>
        Public Enum EEnemyMovementModeStrings As Integer
            Convoy = 0
            Charger = 1
        End Enum

        Public Enum EEnemyMovementDirections As Integer
            Left = -1
            Right = 1
        End Enum

        ''' <summary>
        ''' 
        ''' </summary>
        Public Class EntityEnemyBase
            Implements ICanvasObjects
            Implements INotifyPropertyChanged

#Region "Implementations from INotifyPropertyChanged"

            ''' <summary>
            ''' Todo Write PropertyChanged summary
            ''' </summary>
            Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

            ''' <summary>
            ''' ToDo Write OnPropertyChanged summary
            ''' </summary>
            ''' <param name="name"></param>
            Protected Sub OnPropertyChanged(ByVal name As String)
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
            End Sub

#End Region

            

#Region "Implementations From ICanvasObjects"

            Public Property LocationCoords As Point Implements ICanvasObjects.LocationCoords

            Protected ReadOnly Property LocationCoordsDefault As Point Implements ICanvasObjects.LocationCoordsDefault
                Get
                    Return New Point(Application.CanvasWidth / 2, Application.CanvasHeight - 106)
                End Get
            End Property

            Protected Property MovementSpeed As Double = 10 Implements ICanvasObjects.MovementSpeed
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

            Public ReadOnly Property ObjectScoreValue As Integer Implements ICanvasObjects.ObjectScoreValue
            Get
                Return MovementModeScoreValues.Item(ObjectMovementMode)
            End Get
            End Property
            Public Property ObjectWidth As Double = 33 Implements ICanvasObjects.ObjectWidth
            Public Property TranslateBoundBottom As Double Implements ICanvasObjects.TranslateBoundBottom
            Public Property TranslateBoundLeft As Double = CanvasObjects.GetTranslateBoundLeft(LocationCoords.X, ObjectWidth) Implements ICanvasObjects.TranslateBoundLeft

            Public Property TranslateBoundRight As Double = CanvasObjects.GetTranslateBoundRight(LocationCoords.X, ObjectWidth) Implements ICanvasObjects.TranslateBoundRight

            Public Property TranslateBoundTop As Double Implements ICanvasObjects.TranslateBoundTop
            Public Property ObjectTransform_Translate As TranslateTransform = New TranslateTransform() With {.X = 0, .Y = 0} Implements ICanvasObjects.ObjectTransform_Translate

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
                Application.ActiveEnemyList.Remove(Me)
            End Sub

#End Region

            ''' <summary>
            ''' ToDo Write _objectEnabled summary
            ''' </summary>
            Private _objectEnabled As Boolean

            ''' <summary>
            ''' ToDo Write _objectEnabled summary
            ''' </summary>
            Private _objectMovementMode As Boolean = EEnemyMovementModeStrings.Convoy

            ''' <summary>
            ''' Instantiates a new Entity object with matching hitbox and adds it to ObjectCollection
            ''' </summary>
            ''' <param name="localName"></param>
            ''' <param name="localScoreValueConvoy"></param>
            ''' <param name="localLocationCoords"></param>
            Sub New(localName As String, localScoreValueCharger As Integer, localScoreValueConvoy As Integer, Optional localLocationCoords As Point = Nothing)
                If IsNothing(localLocationCoords) Then localLocationCoords = LocationCoordsDefault
                ObjectName = localName
                MovementModeScoreValues.Add(EEnemyMovementModeStrings.Convoy,localScoreValueConvoy)
                MovementModeScoreValues.Add(EEnemyMovementModeStrings.Charger,localScoreValueCharger) 
                LocationCoords = localLocationCoords
                ObjectEnabled = True

                TranslateBoundBottom = CanvasObjects.GetTranslateBoundBottom(LocationCoords.Y, ObjectHeight)
                TranslateBoundTop = CanvasObjects.GetTranslateBoundTop(LocationCoords.Y, ObjectHeight)
                TranslateBoundLeft = CanvasObjects.GetTranslateBoundLeft(LocationCoords.X, ObjectWidth)
                TranslateBoundRight = CanvasObjects.GetTranslateBoundRight(LocationCoords.X, ObjectWidth)

                CanvasObjects.ObjectCollection.Add(Me)
                Application.EnemyCollection.Add(Me)
                Application.ActiveEnemyList.Add(Me)

                AddHandler GameTimer.LongTick, AddressOf ChangeContent
            End Sub

            ''' <summary>
            ''' ToDo Write EnemyType Summary
            ''' </summary>
            ''' <returns></returns>
            Public ReadOnly Property EnemyType As String = Me.GetType().Name

            Public Property MovementModeScoreValues As Dictionary(Of Integer, Integer) = New Dictionary(Of Integer,Integer)

            ''' <summary>
            ''' ToDo Write ObjectEnabled summary
            ''' </summary>
            ''' <returns></returns>
            Public Property ObjectEnabled() As Boolean
                Get
                    Return _objectEnabled
                End Get
                Set(value As Boolean)
                    _objectEnabled = value
                    OnPropertyChanged("ObjectEnabled")
                End Set
            End Property

            ''' <summary>
            ''' ToDo Write ObjectEnabled summary
            ''' </summary>
            ''' <returns></returns>
            Public Property ObjectMovementMode() As Integer
                Get
                    Return _objectMovementMode
                End Get
                Set(value As Integer)
                    _objectMovementMode = value
                    OnPropertyChanged("ObjectMovementMode")
                End Set
            End Property

            ''' <summary>
            ''' Projectile object for weapon projectile
            ''' </summary>
            Private Property EnemyProjectileInstance As ProjectileClasses.ProjectileEnemy

            ''' <summary>
            ''' Creates an enemy projectile that moves downward
            ''' </summary>
            Sub FireWeapon()
                EnemyProjectileInstance = New ProjectileClasses.ProjectileEnemy((ObjectWidth / 2), 0 - ObjectHeight, New Point(LocationCoords.X + ObjectTransform_Translate.X, LocationCoords.Y))
                Application.AddToCanvas(EnemyProjectileInstance)
            End Sub

            ''' <summary>
            ''' Returns ObjectName whenever this class' ToString method is called
            ''' </summary>
            ''' <returns></returns>
            Public Overrides Function ToString() As String
                Return ObjectName
            End Function

            ''' <summary>
            ''' ToDo Write ChangeContent summary
            ''' </summary>
            Protected Overridable Sub ChangeContent()
            End Sub

        End Class

    End Class

End Namespace
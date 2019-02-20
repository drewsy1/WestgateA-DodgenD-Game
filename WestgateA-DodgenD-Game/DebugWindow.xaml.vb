Imports System.Collections.ObjectModel
Imports WestgateA_DodgenD_Game.Classes
Imports WestgateA_DodgenD_Game.Classes.Entities

Public Class DebugWindow
    Public Property CanvasWidth As Double = Application.CanvasWidth

    Public Property PlayerEntity As EntityClasses.EntityPlayer = Application.EntityPlayerObject

    Public Property PlayerProjectileValue As Double = PlayerEntity.ObjectTransformTranslate.Y

    Public Property PlayerLeftBound As Double = PlayerEntity.LocationCoords.X + PlayerEntity.TranslateBoundLeft

    Public Property PlayerRightBound As Double = PlayerEntity.LocationCoords.X + PlayerEntity.TranslateBoundRight

    Public Property GameScore As Integer = Application.GameScore
    Public Property GameLevel As Integer = Application.GameLevel
    Public Property GameLives As Integer = Application.GameLives


    Public Property EnemyCollection As ObservableCollection(Of EntityClasses.EntityEnemyBase) = Application.EnemyCollection

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        AddHandler EntityClasses.EntityPlayer.PressFireButton, AddressOf PressFireButton
        AddHandler Application.ReleaseFireButton, AddressOf ReleaseFireButton
        ' Add any initialization after the InitializeComponent() call.


    End Sub


    Public Sub PressFireButton()
        textBoxFiring.Background = New SolidColorBrush(Color.FromRgb(255, 0, 0))
    End Sub
    Public Sub ReleaseFireButton()
        textBoxFiring.Background = New SolidColorBrush(Color.FromRgb(255, 255, 255))
    End Sub

End Class

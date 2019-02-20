Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Windows.Markup
Imports WestgateA_DodgenD_Game.Classes.Entities
Imports WestgateA_DodgenD_Game.Classes.Projectile

Public Class DebugWindow
    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        AddHandler EntityClasses.EntityPlayer.PressFireButton, AddressOf PressFireButton
        AddHandler Application.ReleaseFireButton, AddressOf ReleaseFireButton
        AddHandler Application.ProjectileHit, AddressOf RefreshDataGridEnemies
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Property CanvasWidth As Double = Application.CanvasWidth

    Public Property EnemyCollection As ObservableCollection(Of EntityClasses.EntityEnemyBase) = Application.EnemyCollection
    Public Property GameLevel As Integer = Application.GameLevel
    Public Property GameLives As Integer = Application.GameLives
    Public Property GameScore As Integer = Application.GameScore
    Public Property PlayerEntity As EntityClasses.EntityPlayer = Application.EntityPlayerObject

    Public Property PlayerLeftBound As Double = PlayerEntity.LocationCoords.X + PlayerEntity.TranslateBoundLeft
    Public Property PlayerProjectileValue As Double = PlayerEntity.ObjectTransform_Translate.Y
    Public Property PlayerRightBound As Double = PlayerEntity.LocationCoords.X + PlayerEntity.TranslateBoundRight
    Public Sub PressFireButton()
        TextBoxFiring.Background = New SolidColorBrush(Color.FromRgb(255, 0, 0))
    End Sub

    Public Sub ReleaseFireButton()
        TextBoxFiring.Background = New SolidColorBrush(Color.FromRgb(255, 255, 255))
    End Sub

    Public Sub RefreshDataGridEnemies()
        
    End Sub
    
End Class
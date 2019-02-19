Imports System.Collections.ObjectModel
Imports System.Windows.Automation.Peers
Imports System.Windows.Automation.Provider
Imports System.Windows.Controls.Primitives
Imports WestgateA_DodgenD_Game.Classes
Imports WestgateA_DodgenD_Game.Classes.Entities

Public Class DebugWindow
    Public Property CanvasWidth As Double = MainViewModel.CanvasWidth

    Public Property PlayerEntity As EntityClasses.EntityPlayer = MainViewModel.EntityPlayerObject

    Public Property PlayerLeftBound As Double = PlayerEntity.LocationCoords.X + PlayerEntity.TranslateBoundLeft

    Public Property PlayerRightBound As Double = PlayerEntity.LocationCoords.X + PlayerEntity.TranslateBoundRight

    Public Property GameScore As Integer = MainViewModel.GameScore
    Public Property GameLevel As Integer = MainViewModel.GameLevel
    Public Property GameLives As Integer = MainViewModel.GameLives


    Public Property EnemyCollection As ObservableCollection(Of EntityClasses.EntityEnemyBase) = MainViewModel.EnemyCollection

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        AddHandler MainViewModel.MainWindowInstance.PressFireButton, AddressOf PressFireButton
        AddHandler MainViewModel.MainWindowInstance.ReleaseFireButton, AddressOf ReleaseFireButton
        ' Add any initialization after the InitializeComponent() call.


    End Sub


    Public Sub PressFireButton()
        textBoxFiring.Background = New SolidColorBrush(Color.FromRgb(255, 0, 0))
    End Sub
    Public Sub ReleaseFireButton()
        textBoxFiring.Background = New SolidColorBrush(Color.FromRgb(255, 255, 255))
    End Sub

End Class

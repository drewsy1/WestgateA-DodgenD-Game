Imports System.Collections.ObjectModel
Imports System.Globalization
Imports WestgateA_DodgenD_Game.Classes
Imports WestgateA_DodgenD_Game.Classes.Entities

Public Class DebugWindow
    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        AddHandler Application.PressFireButton, AddressOf PressFireButton
        AddHandler Application.ReleaseFireButton, AddressOf ReleaseFireButton
        AddHandler Application.ProjectileHit, AddressOf RefreshDataGridEnemies

    End Sub


    Public Shared Property ActiveEnemyList As List(Of EntityClasses.EntityEnemyBase) = Application.ActiveEnemyList
    Public Property CanvasWidth As Double = Application.CanvasWidth
    Public Property EnemyCollection As ObservableCollection(Of EntityClasses.EntityEnemyBase) = Application.EnemyCollection
    Public Property CurrentGameStats As GameStats = Application.CurrentGameStats
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

Public Class MovementModeStringConverter
    Implements  IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
        If value=1 Then
            Return "Charger"
        Else
            Return "Convoy"
        End If

    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
        Dim tryParseResult As Integer
        EntityClasses.EEnemyMovementModeStrings.TryParse(value,tryParseResult)
        Return tryParseResult
    End Function
End Class
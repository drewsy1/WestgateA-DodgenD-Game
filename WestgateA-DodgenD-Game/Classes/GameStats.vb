Imports System.ComponentModel

Namespace Classes

    Public Class GameStats
        Implements INotifyPropertyChanged

        Private _gameLevel As Integer = 1
        Private _gameLives As Integer = 3
        Private _gameScore As Integer = 0
        Private _lvlX1 As Integer = 1
        Private _lvlX5 As Integer = 0
        Private _gameGameHighScore As Integer = My.Settings.HighScore

        ''' <summary>
        ''' TODO Write LvlX1 summary
        ''' </summary>
        Public Property LvlX1 As Integer
            Get
                return _lvlX1
            End Get
            Set(value As Integer)
                _lvlX1 = value
                OnPropertyChanged("LvlX1")
            End Set
        End Property

        ''' <summary>
        ''' TODO Write LvlX5 summary
        ''' </summary>
        Public Property LvlX5 As Integer
        Get
            return _lvlX5 * 5
        End Get
            Set(value As Integer)
                _lvlX5 = value
                OnPropertyChanged("LvlX5")
            End Set
        End Property

        ''' <summary>
        ''' TODO Write GameLevel summary
        ''' </summary>
        Public Property GameLevel As Integer
            Get
                Return _gameLevel
            End Get
            Set(value As Integer)
                _gameLevel = value
                OnPropertyChanged("GameLevel")
            End Set
        End Property

        ''' <summary>
        ''' TODO Write GameLives summary
        ''' </summary>
        Public Property GameLives As Integer
            Get
                Return _gameLives
            End Get
            Set(value As Integer)
                _gameLives = value
                OnPropertyChanged("GameLives")
            End Set
        End Property

        ''' <summary>
        ''' TODO Write GameScore summary
        ''' </summary>
        Public Property GameScore As Integer
            Get
                Return _gameScore
            End Get
            Set(value As Integer)
                _gameScore = value
                OnPropertyChanged("GameScore")
            End Set
        End Property

        ''' <summary>
        ''' ToDo Write GameHighScore summary
        ''' </summary>
        ''' <returns></returns>
        Public Property GameHighScore As Integer
            Get
                Return My.Settings.HighScore
            End Get
            Set(value As Integer)
                My.Settings.HighScore = value
                OnPropertyChanged("GameHighScore")
            End Set
        End Property

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

    End Class

End Namespace
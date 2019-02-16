Imports System.Windows.Threading

Namespace Classes
    ''' <summary>
    ''' 
    ''' </summary>
    Public Class GameTimer
        ''' <summary>
        ''' Timer that keeps track of game events/objects
        ''' </summary>
        Private Shared WithEvents _dtTimer As DispatcherTimer = New DispatcherTimer() With {
            .Interval = TimeSpan.FromMilliseconds(1)
        }

        Private Shared WithEvents _LongTimer As DispatcherTimer = New DispatcherTimer() With {
            .Interval = TimeSpan.FromSeconds(0.5)
            }

        ''' <summary>
        ''' 
        ''' </summary>
        Public Shared Event Tick()

        Public Shared Event LongTick()

        ''' <summary>
        ''' 
        ''' </summary>
        Public Shared Sub Start()
            _dtTimer.Start()
            _LongTimer.Start()
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        Private Shared Sub _dtRaiseEvent() Handles _dtTimer.Tick
            RaiseEvent Tick()
        End Sub

        Private Shared Sub _secondRaiseEvent() Handles _LongTimer.Tick
            RaiseEvent LongTick()
        End Sub

    End Class
End Namespace

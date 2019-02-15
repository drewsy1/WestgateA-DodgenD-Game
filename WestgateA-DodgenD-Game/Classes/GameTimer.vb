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

        ''' <summary>
        ''' 
        ''' </summary>
        Public Shared Event Tick()

        ''' <summary>
        ''' 
        ''' </summary>
        Public Shared Sub Start()
            _dtTimer.Start()
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        Private Shared Sub _RaiseEvent() Handles _dtTimer.Tick
            RaiseEvent Tick()
        End Sub

    End Class
End Namespace

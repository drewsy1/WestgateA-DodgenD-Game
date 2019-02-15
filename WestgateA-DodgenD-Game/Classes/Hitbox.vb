Imports System.Drawing

Namespace Classes
    Public Class Hitbox
        Private _hitboxRectangle As Rectangle

        ''' <summary>
        ''' Integer representing the X coordinate of the upper-left corner of the hitbox
        ''' </summary>
        ''' <returns>X coordinate of the upper-left corner of the hitbox</returns>
        Public Property X() As Integer
            Get
                Return _hitboxRectangle.X
            End Get
            Set(value As Integer)
                _hitboxRectangle.X = value
            End Set
        End Property

        ''' <summary>
        ''' Integer representing the Y coordinate of the upper-left corner of the hitbox
        ''' </summary>
        ''' <returns>Y coordinate of the upper-left corner of the hitbox</returns>
        Public Property Y() As Integer
            Get
                Return _hitboxRectangle.Y
            End Get
            Set(value As Integer)
                _hitboxRectangle.Y = value
            End Set
        End Property

        ''' <summary>
        ''' Integer representing the width of the hitbox
        ''' </summary>
        ''' <returns>Width of the hitbox</returns>
        Public Property Width() As Integer
            Get
                Return _hitboxRectangle.Width
            End Get
            Set(value As Integer)
                _hitboxRectangle.Width = value
            End Set
        End Property

        ''' <summary>
        ''' Integer representing the height of the hitbox
        ''' </summary>
        ''' <returns>Height of the hitbox</returns>
        Public Property Height() As Integer
            Get
                Return _hitboxRectangle.Height
            End Get
            Set(value As Integer)
                _hitboxRectangle.Height = value
            End Set
        End Property

        ''' <summary>
        ''' Event raised when hitbox leaves canvas
        ''' </summary>
        ''' <param name="direction">Indicates canvas side being touched by hitbox</param>
        Public Event LeavingCanvas(direction As String)

        Sub New(width As Integer,
                height As Integer,
                Optional x As Integer = 0,
                Optional y As Integer = 0
                )
            Me.X = x
            Me.Y = y
            Me.Width = width
            Me.Height = height

            _hitboxRectangle = New Rectangle() With {
                .X = x,
                .Y = y,
                .Width = width,
                .Height = height
            }

            AddHandler GameTimer.Tick, AddressOf CheckCanvasTopTouch
            AddHandler GameTimer.Tick, AddressOf CheckCanvasBottomTouch
            AddHandler GameTimer.Tick, AddressOf CheckCanvasLeftTouch
            AddHandler GameTimer.Tick, AddressOf CheckCanvasRightTouch
        End Sub

        ''' <summary>
        ''' Checks if hitbox has moved above canvas and raises LeavingCanvasTop() if so
        ''' </summary>
        ''' <returns>Boolean that returns true if Rectangle.Top is above canvas</returns>
        Private Function CheckCanvasTopTouch() As Boolean
            If _hitboxRectangle.Top > MainWindowWrapper.MainWindowInstance.CanvasGameScreen.Height Then
                RaiseEvent LeavingCanvas("Top")
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Checks if hitbox has moved below canvas and raises LeavingCanvasBottom() if so
        ''' </summary>
        ''' <returns>Boolean that returns true if Rectangle.Bottom is below canvas</returns>
        Private Function CheckCanvasBottomTouch() As Boolean
            If _hitboxRectangle.Bottom < 0 Then
                RaiseEvent LeavingCanvas("Bottom")
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Checks if hitbox has moved left of canvas and raises LeavingCanvasLeft() if so
        ''' </summary>
        ''' <returns>Boolean that returns true if Rectangle.Left is left of canvas</returns>
        Private Function CheckCanvasLeftTouch() As Boolean
            If _hitboxRectangle.Left < 0 Then
                RaiseEvent LeavingCanvas("Left")
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Checks if hitbox has moved right of canvas and raises LeavingCanvasRight() if so
        ''' </summary>
        ''' <returns>Boolean that returns true if Rectangle.Right is right of canvas</returns>
        Private Function CheckCanvasRightTouch() As Boolean
            If _hitboxRectangle.Right < MainWindowWrapper.MainWindowInstance.CanvasGameScreen.Width Then
                RaiseEvent LeavingCanvas("Right")
                Return True
            Else
                Return False
            End If
        End Function

        Public Sub MoveX(pixels As Integer)
            X += pixels
        End Sub

        Public Sub MoveY(pixels As Integer)
            Y += pixels
        End Sub
    End Class
End Namespace

Namespace Classes.Canvas
    Public MustInherit Class CanvasMethods
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="locationX"></param>
        ''' <param name="locationY"></param>
        ''' <param name="control"></param>
        Public Shared Sub SetCanvasLocation(locationX As Double,
                                           locationY As Double,
                                           control As Object)
            Controls.Canvas.SetLeft(control, locationX)
            Controls.Canvas.SetBottom(control, locationY)
        End Sub
    End Class
End Namespace
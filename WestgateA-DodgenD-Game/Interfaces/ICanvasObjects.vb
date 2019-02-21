Namespace Interfaces

    Public Interface ICanvasObjects

        ''' <summary>
        '''     Coordinate location of entity
        ''' </summary>
        Property LocationCoords As Point

        ''' <summary>
        '''     Default starting coordinate location for entity
        ''' </summary>
        ReadOnly Property LocationCoordsDefault As Point

        ''' <summary>
        '''     Number of pixels by which the object moves
        ''' </summary>
        Property MovementSpeed As Double

        ''' <summary>
        '''     Image that serves as entity
        ''' </summary>
        Property ObjectControl As Object

        ''' <summary>
        '''     Default entity cursor height in pixels
        ''' </summary>
        Property ObjectHeight As Double

        ''' <summary>
        ''' TODO Write ObjectName summary
        ''' </summary>
        ''' <returns></returns>
        Property ObjectName As String

        ''' <summary>
        ''' TODO Write ObjectPointLowerLeft summary
        ''' </summary>
        ''' <returns></returns>
        ReadOnly Property ObjectPointLowerLeft As Point

        ''' <summary>
        ''' TODO Write ObjectPointUpperRight summary
        ''' </summary>
        ''' <returns></returns>
        ReadOnly Property ObjectPointUpperRight As Point

        ''' <summary>
        ''' TODO Write ObjectScoreValue summary
        ''' </summary>
        ''' <returns></returns>
        ReadOnly Property ObjectScoreValue As Integer

        ''' <summary>
        '''     Translate transform object for entity
        ''' </summary>
        Property ObjectTransform_Translate As TranslateTransform

        ''' <summary>
        '''     TransformGroup containing Translate transform to be added to entity instance
        ''' </summary>
        Property ObjectTransformGroup As TransformGroup
        ''' <summary>
        '''     Default entity width in pixels
        ''' </summary>
        Property ObjectWidth As Double

        ''' <summary>
        '''     Bottommost translatable Y-value of entity
        ''' </summary>
        Property TranslateBoundBottom As Double

        ''' <summary>
        '''     Leftmost translatable X-value of entity
        ''' </summary>
        Property TranslateBoundLeft As Double

        ''' <summary>
        '''     Rightmost translatable X-value of entity
        ''' </summary>
        Property TranslateBoundRight As Double

        ''' <summary>
        '''     Uppermost translatable Y-value of entity
        ''' </summary>
        Property TranslateBoundTop As Double

        ''' <summary>
        ''' TODO Write MoveDown summary
        ''' </summary>
        ''' <param name="localMovementSpeed"></param>
        Sub MoveDown(Optional localMovementSpeed As Double = 0)

        ''' <summary>
        ''' TODO Write MoveLeft summary
        ''' </summary>
        ''' <param name="localMovementSpeed"></param>
        Sub MoveLeft(Optional localMovementSpeed As Double = 0)

        ''' <summary>
        ''' TODO Write MoveRight summary
        ''' </summary>
        ''' <param name="localMovementSpeed"></param>
        Sub MoveRight(Optional localMovementSpeed As Double = 0)

        ''' <summary>
        ''' TODO Write MoveUp summary
        ''' </summary>
        ''' <param name="localMovementSpeed"></param>
        Sub MoveUp(Optional localMovementSpeed As Double = 0)

        ''' <summary>
        '''     Removes entity from canvas, clears the hit box object,
        '''     and removes entity from EntitiesCollection
        ''' </summary>
        Sub Remove()

        ''' <summary>
        '''     Translates along the X axis a given number of pixels while staying within canvas bounds
        ''' </summary>
        ''' <param name="localMovementSpeed">Distance to translate in pixels (Negative moves left)</param>
        'Sub TranslateX(localMovementSpeed As Double)

        ''' <summary>
        '''     Translates along the Y axis a given number of pixels while staying within canvas bounds
        ''' </summary>
        ''' <param name="localMovementSpeed">Distance to translate in pixels (Negative moves down)</param>
        'Sub TranslateY(localMovementSpeed As Double)

    End Interface

End Namespace
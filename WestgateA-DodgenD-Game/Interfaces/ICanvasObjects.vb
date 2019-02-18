Imports WestgateA_DodgenD_Game.Classes

Namespace Interfaces
    Public Interface ICanvasObjects
        ''' <summary>
        ''' Default entity cursor height in pixels
        ''' </summary>
        Property ObjectHeight As Double

        ''' <summary>
        ''' Default entity width in pixels
        ''' </summary>
        Property ObjectWidth As Double

        ''' <summary>
        ''' Default starting X-coordinate location for entity
        ''' </summary>
        ReadOnly Property LocationXDefault As Double

        ''' <summary>
        ''' Default starting Y-coordinate location for entity
        ''' </summary>
        ReadOnly Property LocationYDefault As Double

        ''' <summary>
        ''' X-coordinate location for entity
        ''' </summary>
        Property LocationX As Double

        ''' <summary>
        ''' Y-coordinate location for entity
        ''' </summary>
        Property LocationY As Double

        ''' <summary>
        ''' Leftmost X-value of entity
        ''' </summary>
        Property TranslateBoundLeft As Double

        ''' <summary>
        ''' Rightmost X-value of entity
        ''' </summary>
        Property TranslateBoundRight As Double

        ''' <summary>
        ''' Uppermost Y-value of entity
        ''' </summary>
        Property TranslateBoundTop As Double

        ''' <summary>
        ''' Bottommost Y-value of entity
        ''' </summary>
        Property TranslateBoundBottom As Double

        ''' <summary>
        ''' Number of pixels by which the object moves
        ''' </summary>
        Property MovementSpeed As Double

        ''' <summary>
        ''' Translate transform object for entity
        ''' </summary>
        Property ObjectTransformTranslate As TranslateTransform

        ''' <summary>
        ''' TransformGroup containing Translate transform to be added to entity instance
        ''' </summary>
        Property ObjectTransformGroup As TransformGroup

        ''' <summary>
        ''' Image that serves as entity
        ''' </summary>
        Property ObjectControl As Object

        Sub MoveLeft(Optional localMovementSpeed As Double = 0)

        Sub MoveRight(Optional localMovementSpeed As Double = 0)

        Sub MoveUp(Optional localMovementSpeed As Double = 0)

        Sub MoveDown(Optional localMovementSpeed As Double = 0)

        ''' <summary>
        ''' Translates along the Y axis a given number of pixels while staying within canvas bounds
        ''' </summary>
        ''' <param name="localMovementSpeed">Distance to translate in pixels (Negative moves down)</param>
        Sub TranslateY(localMovementSpeed As Double)

        ''' <summary>
        ''' Translates along the X axis a given number of pixels while staying within canvas bounds
        ''' </summary>
        ''' <param name="localMovementSpeed">Distance to translate in pixels (Negative moves left)</param>
        Sub TranslateX(localMovementSpeed As Double)

        ''' <summary>
        ''' Removes entity from canvas, clears the hit box object,
        ''' and removes entity from EntitiesCollection
        ''' </summary>
        Sub Remove()

    End Interface
End Namespace
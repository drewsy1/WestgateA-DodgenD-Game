Namespace Classes.Entities
    Public Interface IEntity
        ''' <summary>
        ''' Default entity cursor height in pixels
        ''' </summary>
        Property EntityHeight As Double

        ''' <summary>
        ''' Default entity width in pixels
        ''' </summary>
        Property EntityWidth As Double

        ''' <summary>
        ''' Default starting X-coordinate location for entity
        ''' </summary>
        Property LocationXDefault As Double

        ''' <summary>
        ''' Default starting Y-coordinate location for entity
        ''' </summary>
        Property LocationYDefault As Double

        ''' <summary>
        ''' Leftmost x-value of entity
        ''' </summary>
        Property TranslateBoundLeft As Double

        ''' <summary>
        ''' Rightmost x-value of entity
        ''' </summary>
        Property TranslateBoundRight As Double


        Property TranslateBoundTop As Double


        Property TranslateBoundBottom As Double

        Property MovementSpeed As Double

        ''' <summary>
        ''' Translate transform object for entity
        ''' </summary>
        Property EntityTransformTranslate As TranslateTransform

        ''' <summary>
        ''' TransformGroup containing Translate transform to be added to entity instance
        ''' </summary>
        Property EntityTransformGroup As TransformGroup

        ''' <summary>
        ''' Image that serves as entity
        ''' </summary>
        Property EntityControl As Object

        '''' <summary>
        '''' Moves entity left if entity is within bounds
        '''' </summary>
        'Sub MoveLeft(localMovementSpeed As Double)

        '''' <summary>
        '''' Moves entity right if entity is within bounds
        '''' </summary>
        'Sub MoveRight(localMovementSpeed As Double)

        '''' <summary>
        '''' Moves entity up if entity is within bounds
        '''' </summary>
        'Sub MoveUp()

        '''' <summary>
        '''' Moves entity down if entity is within bounds
        '''' </summary>
        'Sub MoveDown()

    End Interface
End Namespace
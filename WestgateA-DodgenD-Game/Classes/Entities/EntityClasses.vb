Namespace Classes.Entities
    ' ReSharper disable once ClassNeverInstantiated.Global
    ''' <summary>
    ''' Class containing Projectile classes and meta-properties
    ''' </summary>
    Partial Public Class EntityClasses
        ''' <summary>
        ''' Create a static collection of projectiles to keep track of them
        ''' </summary>
        Private Shared ReadOnly EntitiesCollection As List(Of EntityBase) =
                               New List(Of EntityBase)()
    End Class
End Namespace
Imports WestgateA_DodgenD_Game.Interfaces

Namespace Classes.Entities
    ' ReSharper disable once ClassNeverInstantiated.Global
    ''' <summary>
    ''' Class containing entity classes and meta-properties
    ''' </summary>
    Partial Public Class EntityClasses
        Public Class EntityBase
            Inherits CanvasObjects
            Implements ICanvasObjects
            Protected Overrides Property ObjectHeight As Double

            Protected Overrides Property ObjectWidth As Double

            Protected Overrides ReadOnly Property LocationXDefault As Double

            Protected Overrides ReadOnly Property LocationYDefault As Double

            Protected Overrides Property LocationX As Double

            Protected Overrides Property LocationY As Double

            Protected Overrides Property TranslateBoundLeft As Double

            Protected Overrides Property TranslateBoundRight As Double

            Protected Overrides Property TranslateBoundTop As Double

            Protected Overrides Property TranslateBoundBottom As Double

            Protected Overrides Property MovementSpeed As Double

            Protected Overrides Property ObjectTransformTranslate As TranslateTransform

            Protected Overrides Property ObjectTransformGroup As TransformGroup

            Public Overrides Property ObjectControl As Object

            ''' <summary>
            ''' Instantiates a new Entity object with matching hitbox and adds it to ObjectCollection
            ''' </summary>
            Sub New(Optional localLocationX As Double = Nothing,
                    Optional localLocationY As Double = Nothing)
                MyBase.New(localLocationX, localLocationY)
                EntityCollection.Add(Me)

                ObjectHitbox = CreateHitbox()

                AddHandler ObjectHitbox.LeavingCanvas, AddressOf Remove
            End Sub

            Public Overrides Sub Remove() Implements ICanvasObjects.Remove
                ' Remove rectangle from CanvasGameScreen (make it invisible)
                MainWindowWrapper.MainWindowInstance.CanvasGameScreen.Children.Remove(
                    ObjectControl)

                Dim itemIndex As Integer = EntityCollection.IndexOf(Me)
                If itemIndex >= 0 Then
                    EntityCollection(itemIndex) = Nothing
                End If
                Finalize()
            End Sub


        End Class
    End Class
End Namespace

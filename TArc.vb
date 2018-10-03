'TArc  class used in solver model and map form

Public Class TArc

    Inherits Arc
    Public Property MultiFlow As New SortedList(Of String, Decimal)

    Public Sub New()

    End Sub

    ' Creates new instance with tail and head node
    Public Sub New(t As Node, h As Node)
        MyBase.New(t, h)
    End Sub



End Class

Public Class frmNodes
    Private Sub NodesBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs)
        Me.Validate()
        Me.NodesBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.DatabaseDataSet)

    End Sub

    Private Sub frmNodes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'DatabaseDataSet.Nodes' table. You can move, or remove it, as needed.
        Me.NodesTableAdapter.Fill(Me.DatabaseDataSet.Nodes)

    End Sub

    Private Sub BindingNavigatorMoveNextItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMoveNextItem.Click

    End Sub
    ' Displays Demand as Absolute value for better readability
    Private Sub Prod1DemTextBox_TextChanged(sender As Object, e As EventArgs) Handles Prod1DemTextBox.TextChanged
        Dim GenDemand As Integer
        GenDemand = Math.Abs(Convert.ToInt32(Prod1DemTextBox.Text))
        Prod1DemTextBox.Text = GenDemand.ToString

    End Sub
    ' Displays Demand as Absolute value for better readability
    Private Sub Prod2DemTextBox_TextChanged(sender As Object, e As EventArgs) Handles Prod2DemTextBox.TextChanged
        Dim GenDemand As Integer
        GenDemand = Math.Abs(Convert.ToInt32(Prod2DemTextBox.Text))
        Prod2DemTextBox.Text = GenDemand.ToString
    End Sub
    ' Displays Demand as Absolute value for better readability
    Private Sub Prod3DemTextBox_TextChanged(sender As Object, e As EventArgs) Handles Prod3DemTextBox.TextChanged
        Dim GenDemand As Integer
        GenDemand = Math.Abs(Convert.ToInt32(Prod3DemTextBox.Text))
        Prod3DemTextBox.Text = GenDemand.ToString
    End Sub
End Class
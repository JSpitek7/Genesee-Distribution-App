Public Class frmMap

    Public Property MapImage As Image = My.Resources.USmap
    Public Property Net As Network
    Public Property Opt As Optimization
    Public Property problemType As String
    Private Property Xscale As Decimal
    Private Property Yscale As Decimal

    Private Sub frmMap_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'populate combo boxes
        For Each p In Net.ProdList.Keys
            cboProduct.Items.Add(p)
        Next
        cboProduct.SelectedIndex = 0
    End Sub

    'draws a node given border, fill, and text color
    Public Sub DrawNode(n As Node, borderColor As Color, fillColor As Color,
                        textColor As Color, e As PaintEventArgs)
        Dim size As Decimal = 30 * Math.Min(Xscale, Yscale)
        Dim x As Integer = Xscale * n.Xcoord - size / 2
        Dim y As Integer = Yscale * n.Ycoord - size / 2
        'creating borders and fill
        Dim borderPen As New Pen(borderColor)
        borderPen.Width = 2
        e.Graphics.DrawEllipse(borderPen, x, y, size, size)
        Dim fillBrush As New SolidBrush(fillColor)
        e.Graphics.FillEllipse(fillBrush, x, y, size, size)
        'creating text
        Dim fontSize As Integer = size / 2
        Dim nodeFont As New System.Drawing.Font("Courier New", fontSize, FontStyle.Bold)
        'text offset
        Dim textWidth As Integer = n.ID.Count * fontSize
        Dim tx As Integer = Xscale * n.Xcoord - textWidth / 2
        Dim ty As Integer = Yscale * n.Ycoord - nodeFont.GetHeight() / 2
        'write text
        Dim textBrush As New SolidBrush(textColor)
        e.Graphics.DrawString(n.ID, nodeFont, textBrush, tx, ty)

    End Sub

    'draws arc with given color, width, and direction
    Public Sub DrawArc(a As Arc, arcColor As Color, arcWidth As Integer,
                       directed As Boolean, e As PaintEventArgs)
        'set pen properties
        Dim arcPen As New Pen(arcColor)
        arcPen.Width = arcWidth
        arcPen.StartCap = Drawing2D.LineCap.Round
        If directed Then
            arcPen.CustomEndCap = New Drawing2D.AdjustableArrowCap(2 * arcWidth,
                                                                   2 * arcWidth)
        Else
            arcPen.EndCap = Drawing2D.LineCap.Round
        End If
        'Draw arc
        e.Graphics.DrawLine(arcPen, Xscale * a.Tail.Xcoord, Yscale * a.Tail.Ycoord,
                            Xscale * a.Head.Xcoord, Yscale * a.Head.Ycoord)

    End Sub

    Private Sub frmMap_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        'scale ratios
        Xscale = Me.ClientRectangle.Width / MapImage.Width
        Yscale = Me.ClientRectangle.Height / MapImage.Height
        'clear form background and draw image
        e.Graphics.Clear(Color.White)
        Dim rect As New Rectangle(0, 0, MapImage.Width, MapImage.Height)
        e.Graphics.DrawImage(MapImage, Me.ClientRectangle, rect, GraphicsUnit.Pixel)
        Me.BackgroundImageLayout = ImageLayout.Stretch
        'draw network
        'draws arcs
        For Each a In Net.ArcList.Values
            Dim arcFlow As Integer = a.MultiFlow(cboProduct.Text)
            'checks if arc is used for selected product
            If arcFlow > 0 Then
                Dim arcColor As Color
                'sets arc color to green if less than 100 units are transported
                If arcFlow <= 100 Then
                    arcColor = Color.Green
                    'sets arc color to yellow if between 100 and 200 units are transported
                ElseIf 100 < arcFlow And arcFlow <= 200 Then
                    arcColor = Color.Yellow
                    'sets arc color to red if more than 200 units are transported
                Else
                    arcColor = Color.Red
                End If
                DrawArc(a, arcColor, 2, False, e)
            End If
        Next

        'draws nodes
        'checks to see if node demand exists in problem type
        If problemType = "All" Or problemType = cboProduct.SelectedItem Then
            For Each n In Net.NodeList.Values
                Dim fillColor As Color
                Dim demanded As Integer = Net.NodeList(n.ID).Demand(cboProduct.Text)
                Dim received As Integer = Opt.SatisfiedNodeDem(n.ID & cboProduct.Text)

                If received <> demanded Then
                    fillColor = Color.Red
                Else
                    fillColor = Color.Green
                End If
                DrawNode(n, Color.Black, fillColor, Color.Black, e)
            Next
        End If

    End Sub

    'handles change in cboProduct
    Private Sub cboProduct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProduct.SelectedIndexChanged
        Me.Refresh()
    End Sub
End Class
Public Class ComboControl
    Inherits System.Windows.Forms.UserControl

    Dim MyBg As New Bitmap(8, 88)

    Dim MouseX, MouseY As Integer
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        picBG.Image = MyBg
    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents picBG As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.picBG = New System.Windows.Forms.PictureBox
        Me.SuspendLayout()
        '
        'picBG
        '
        Me.picBG.BackColor = System.Drawing.Color.Black
        Me.picBG.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picBG.Location = New System.Drawing.Point(0, 0)
        Me.picBG.Name = "picBG"
        Me.picBG.Size = New System.Drawing.Size(8, 88)
        Me.picBG.TabIndex = 0
        Me.picBG.TabStop = False
        '
        'ComboControl
        '
        Me.Controls.Add(Me.picBG)
        Me.Name = "ComboControl"
        Me.Size = New System.Drawing.Size(8, 88)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub ComboControl_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picBG.MouseDown
        If e.Button = MouseButtons.Left Then
            MouseX = e.X \ 8
            MouseY = e.Y \ 8
        End If
        If (e.Button = MouseButtons.Right) Then
            If Not Me.Tag Is Nothing Then
                MessageBox.Show(Hex(DirectCast(Me.Tag, Integer)))
            End If

        End If
    End Sub

    Private Sub ComboControl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picBG.MouseMove
        If e.Button = MouseButtons.Left Then
            Dim SnapX, SnapY As Integer
            SnapX = e.X \ 8
            SnapY = e.Y \ 8
            Me.Left -= (MouseX - SnapX) * 8
            Me.Top -= (MouseY - SnapY) * 8
        End If
    End Sub

    Public ReadOnly Property BGImage() As Bitmap
        Get
            Return MyBg
        End Get
    End Property

    Private Sub picBG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picBG.Click
    End Sub
End Class

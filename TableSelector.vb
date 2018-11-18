Public Class TableSelector
    Inherits System.Windows.Forms.Form
    Dim ReturnResult As Integer = -1
    Dim AllowFirstTable As Boolean
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.Icon = Nothing
    End Sub

    'Form overrides dispose to clean up the component list.
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
    Friend WithEvents imlTable As System.Windows.Forms.ImageList
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents sbpTile As System.Windows.Forms.StatusBarPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lstTables As System.Windows.Forms.ListView
    Friend WithEvents chTable As System.Windows.Forms.ColumnHeader
    Friend WithEvents chTileOffset As System.Windows.Forms.ColumnHeader
    Friend WithEvents chROMOffset As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmdDone As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(TableSelector))
        Me.imlTable = New System.Windows.Forms.ImageList(Me.components)
        Me.StatusBar1 = New System.Windows.Forms.StatusBar
        Me.sbpTile = New System.Windows.Forms.StatusBarPanel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdDone = New System.Windows.Forms.Button
        Me.lstTables = New System.Windows.Forms.ListView
        Me.chTable = New System.Windows.Forms.ColumnHeader
        Me.chTileOffset = New System.Windows.Forms.ColumnHeader
        Me.chROMOffset = New System.Windows.Forms.ColumnHeader
        CType(Me.sbpTile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'imlTable
        '
        Me.imlTable.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit
        Me.imlTable.ImageSize = New System.Drawing.Size(16, 16)
        Me.imlTable.ImageStream = CType(resources.GetObject("imlTable.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlTable.TransparentColor = System.Drawing.Color.Transparent
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 335)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.sbpTile})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(292, 22)
        Me.StatusBar1.SizingGrip = False
        Me.StatusBar1.TabIndex = 1
        Me.StatusBar1.Text = "Current Tile: "
        '
        'sbpTile
        '
        Me.sbpTile.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.sbpTile.Icon = CType(resources.GetObject("sbpTile.Icon"), System.Drawing.Icon)
        Me.sbpTile.Text = "Current Tile: "
        Me.sbpTile.Width = 292
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmdCancel)
        Me.Panel1.Controls.Add(Me.cmdDone)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 311)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(292, 24)
        Me.Panel1.TabIndex = 3
        '
        'cmdCancel
        '
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdCancel.Location = New System.Drawing.Point(75, 0)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 24)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "Cancel"
        '
        'cmdDone
        '
        Me.cmdDone.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdDone.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdDone.Location = New System.Drawing.Point(0, 0)
        Me.cmdDone.Name = "cmdDone"
        Me.cmdDone.Size = New System.Drawing.Size(75, 24)
        Me.cmdDone.TabIndex = 0
        Me.cmdDone.Text = "OK"
        '
        'lstTables
        '
        Me.lstTables.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chTable, Me.chTileOffset, Me.chROMOffset})
        Me.lstTables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstTables.Location = New System.Drawing.Point(0, 0)
        Me.lstTables.Name = "lstTables"
        Me.lstTables.Size = New System.Drawing.Size(292, 311)
        Me.lstTables.SmallImageList = Me.imlTable
        Me.lstTables.TabIndex = 4
        Me.lstTables.View = System.Windows.Forms.View.Details
        '
        'chTable
        '
        Me.chTable.Text = "Table"
        Me.chTable.Width = 84
        '
        'chTileOffset
        '
        Me.chTileOffset.Text = "Tile offset"
        Me.chTileOffset.Width = 71
        '
        'chROMOffset
        '
        Me.chROMOffset.Text = "ROM Offset"
        Me.chROMOffset.Width = 116
        '
        'TableSelector
        '
        Me.AcceptButton = Me.cmdDone
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(292, 357)
        Me.Controls.Add(Me.lstTables)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.StatusBar1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TableSelector"
        Me.ShowInTaskbar = False
        Me.Text = "TableSelector"
        CType(Me.sbpTile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Shadows Function ShowDialog(ByVal ComboData As RomInfo, ByVal SelectedTile As Integer, Optional ByVal AllowFirstTable As Boolean = False) As Integer
        Dim NewItem As ListViewItem
        For i As Integer = 0 To 15
            NewItem = lstTables.Items.Add("Table " & i.ToString, 0)
            NewItem.SubItems.Add(combodata.TableLocation(i).ToString)
            NewItem.SubItems.Add("$" & Hex(combodata.RomTableValue(i)))
        Next
        Try
            lstTables.Items.Item(ComboData.NextTableInclusive(SelectedTile)).Selected = True
        Catch
            lstTables.Items.Item(0).Selected = True
        End Try
        sbpTile.Text &= SelectedTile.ToString
        lstTables.Select()
        Me.AllowFirstTable = AllowFirstTable
        MyBase.ShowDialog()
        Return ReturnResult
    End Function

    Private Sub cmdDone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDone.Click
        If CheckItem() Then
            Me.ReturnResult = lstTables.SelectedIndices.Item(0)
            Me.Hide()
        End If
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Hide()
    End Sub

    Private Sub lstTables_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstTables.DoubleClick
        If CheckItem() Then
            Me.ReturnResult = lstTables.SelectedIndices.Item(0)
            Me.Hide()
        End If
    End Sub

    Private Function CheckItem() As Boolean
        If (Not AllowFirstTable) AndAlso lstTables.SelectedIndices.Item(0) = 0 Then
            MessageBox.Show("This table pointer cannot be moved.")
            Return False
        End If
        Return True
    End Function
End Class

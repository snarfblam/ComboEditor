Public Class frmMain
    Inherits System.Windows.Forms.Form
    Dim bmpTiles As Bitmap
    Dim gTiles As Graphics

    Dim bmpSelection As New Bitmap(16, 16)
    Dim gSelection As Graphics = Graphics.FromImage(bmpSelection)
    Dim SelectedTile As Byte
    Dim ClickedTile As Integer
    Dim ComboData As RomInfo

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        picSelection.Image = bmpSelection
    End Sub
#Region " Windows Form Designer generated code "

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
    Friend WithEvents odfOpenRom As System.Windows.Forms.OpenFileDialog
    Friend WithEvents mnu As System.Windows.Forms.MainMenu
    Friend WithEvents mnuFile As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileOpen As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileSave As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileExit As System.Windows.Forms.MenuItem
    Friend WithEvents picTiles As System.Windows.Forms.Panel
    Friend WithEvents scrTiles As System.Windows.Forms.VScrollBar
    Friend WithEvents picSource As System.Windows.Forms.PictureBox
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuViewExpanded As System.Windows.Forms.MenuItem
    Friend WithEvents picSelection As System.Windows.Forms.PictureBox
    Friend WithEvents mnuViewTableLocations As System.Windows.Forms.MenuItem
    Friend WithEvents mnuViewCombos As System.Windows.Forms.MenuItem
    Friend WithEvents mnuViewTables As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents cmnTileDelete As System.Windows.Forms.MenuItem
    Friend WithEvents cmnTileInsert As System.Windows.Forms.MenuItem
    Friend WithEvents cmnTileTable As System.Windows.Forms.MenuItem
    Friend WithEvents cmnTile As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MnuOptionsAutopointers As System.Windows.Forms.MenuItem
    Friend WithEvents mnuViewJump As System.Windows.Forms.MenuItem
    Friend WithEvents cmnTilesJump As System.Windows.Forms.MenuItem
    Friend WithEvents cmnTileInfo As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileConvert As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents ComboHighlight As System.Windows.Forms.PictureBox
    Friend WithEvents mnuOptionsAutotable As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOptionErrorcheck As System.Windows.Forms.MenuItem
    Friend WithEvents picSrc As System.Windows.Forms.PictureBox
    Friend WithEvents picComboPreview As System.Windows.Forms.PictureBox
    Friend WithEvents mnuOptionsCreateconversion As System.Windows.Forms.MenuItem
    Friend WithEvents conversionTableSaver As System.Windows.Forms.SaveFileDialog
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.odfOpenRom = New System.Windows.Forms.OpenFileDialog
        Me.mnu = New System.Windows.Forms.MainMenu(Me.components)
        Me.mnuFile = New System.Windows.Forms.MenuItem
        Me.mnuFileOpen = New System.Windows.Forms.MenuItem
        Me.mnuFileSave = New System.Windows.Forms.MenuItem
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.mnuFileConvert = New System.Windows.Forms.MenuItem
        Me.MenuItem6 = New System.Windows.Forms.MenuItem
        Me.mnuFileExit = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.mnuViewCombos = New System.Windows.Forms.MenuItem
        Me.mnuViewExpanded = New System.Windows.Forms.MenuItem
        Me.mnuViewTables = New System.Windows.Forms.MenuItem
        Me.mnuViewTableLocations = New System.Windows.Forms.MenuItem
        Me.mnuViewJump = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.MnuOptionsAutopointers = New System.Windows.Forms.MenuItem
        Me.mnuOptionsAutotable = New System.Windows.Forms.MenuItem
        Me.mnuOptionErrorcheck = New System.Windows.Forms.MenuItem
        Me.mnuOptionsCreateconversion = New System.Windows.Forms.MenuItem
        Me.picTiles = New System.Windows.Forms.Panel
        Me.ComboHighlight = New System.Windows.Forms.PictureBox
        Me.scrTiles = New System.Windows.Forms.VScrollBar
        Me.picSource = New System.Windows.Forms.PictureBox
        Me.picSelection = New System.Windows.Forms.PictureBox
        Me.cmnTile = New System.Windows.Forms.ContextMenu
        Me.cmnTileInfo = New System.Windows.Forms.MenuItem
        Me.cmnTileDelete = New System.Windows.Forms.MenuItem
        Me.cmnTileInsert = New System.Windows.Forms.MenuItem
        Me.MenuItem5 = New System.Windows.Forms.MenuItem
        Me.cmnTileTable = New System.Windows.Forms.MenuItem
        Me.cmnTilesJump = New System.Windows.Forms.MenuItem
        Me.picSrc = New System.Windows.Forms.PictureBox
        Me.picComboPreview = New System.Windows.Forms.PictureBox
        Me.conversionTableSaver = New System.Windows.Forms.SaveFileDialog
        Me.picTiles.SuspendLayout()
        CType(Me.ComboHighlight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picSelection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picSrc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picComboPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'odfOpenRom
        '
        Me.odfOpenRom.FileName = "Zelda.nes"
        Me.odfOpenRom.Filter = "Zelda Rom (*.nes)|*zeld*.nes|All NES Roms (*.nes)|*.nes|All files (*.*)|*.*"
        Me.odfOpenRom.Title = "Open Zelda Rom"
        '
        'mnu
        '
        Me.mnu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFile, Me.MenuItem1, Me.MenuItem2})
        '
        'mnuFile
        '
        Me.mnuFile.Index = 0
        Me.mnuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFileOpen, Me.mnuFileSave, Me.MenuItem4, Me.mnuFileConvert, Me.MenuItem6, Me.mnuFileExit, Me.MenuItem3})
        Me.mnuFile.Text = "&File"
        '
        'mnuFileOpen
        '
        Me.mnuFileOpen.Index = 0
        Me.mnuFileOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO
        Me.mnuFileOpen.Text = "&Open"
        '
        'mnuFileSave
        '
        Me.mnuFileSave.Index = 1
        Me.mnuFileSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS
        Me.mnuFileSave.Text = "&Save"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 2
        Me.MenuItem4.Text = "-"
        '
        'mnuFileConvert
        '
        Me.mnuFileConvert.Index = 3
        Me.mnuFileConvert.Text = "Create Conversion Table..."
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 4
        Me.MenuItem6.Text = "-"
        '
        'mnuFileExit
        '
        Me.mnuFileExit.Index = 5
        Me.mnuFileExit.Shortcut = System.Windows.Forms.Shortcut.AltF4
        Me.mnuFileExit.Text = "&Exit"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 6
        Me.MenuItem3.Text = "Temp"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 1
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuViewCombos, Me.mnuViewTableLocations, Me.mnuViewJump})
        Me.MenuItem1.Text = "&View"
        '
        'mnuViewCombos
        '
        Me.mnuViewCombos.Index = 0
        Me.mnuViewCombos.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuViewExpanded, Me.mnuViewTables})
        Me.mnuViewCombos.Text = "&Combos"
        '
        'mnuViewExpanded
        '
        Me.mnuViewExpanded.Index = 0
        Me.mnuViewExpanded.Text = "&Expanded Combos"
        '
        'mnuViewTables
        '
        Me.mnuViewTables.Index = 1
        Me.mnuViewTables.Text = "Combos By &Table"
        '
        'mnuViewTableLocations
        '
        Me.mnuViewTableLocations.Index = 1
        Me.mnuViewTableLocations.Text = "Combo Table &Locations"
        '
        'mnuViewJump
        '
        Me.mnuViewJump.Index = 2
        Me.mnuViewJump.Text = "Jump to table..."
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 2
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MnuOptionsAutopointers, Me.mnuOptionsAutotable, Me.mnuOptionErrorcheck, Me.mnuOptionsCreateconversion})
        Me.MenuItem2.Text = "Tools"
        '
        'MnuOptionsAutopointers
        '
        Me.MnuOptionsAutopointers.Checked = True
        Me.MnuOptionsAutopointers.Index = 0
        Me.MnuOptionsAutopointers.Text = "Modify Pointers for Delete/Insert"
        '
        'mnuOptionsAutotable
        '
        Me.mnuOptionsAutotable.Index = 1
        Me.mnuOptionsAutotable.Text = "Autotable"
        '
        'mnuOptionErrorcheck
        '
        Me.mnuOptionErrorcheck.Index = 2
        Me.mnuOptionErrorcheck.Text = "Check for errors"
        '
        'mnuOptionsCreateconversion
        '
        Me.mnuOptionsCreateconversion.Index = 3
        Me.mnuOptionsCreateconversion.Text = "Create Conversion Table"
        '
        'picTiles
        '
        Me.picTiles.Controls.Add(Me.ComboHighlight)
        Me.picTiles.Dock = System.Windows.Forms.DockStyle.Left
        Me.picTiles.Location = New System.Drawing.Point(0, 0)
        Me.picTiles.Name = "picTiles"
        Me.picTiles.Size = New System.Drawing.Size(22, 407)
        Me.picTiles.TabIndex = 0
        '
        'ComboHighlight
        '
        Me.ComboHighlight.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ComboHighlight.Location = New System.Drawing.Point(17, 0)
        Me.ComboHighlight.Name = "ComboHighlight"
        Me.ComboHighlight.Size = New System.Drawing.Size(5, 176)
        Me.ComboHighlight.TabIndex = 30
        Me.ComboHighlight.TabStop = False
        Me.ComboHighlight.Visible = False
        '
        'scrTiles
        '
        Me.scrTiles.Dock = System.Windows.Forms.DockStyle.Left
        Me.scrTiles.LargeChange = 11
        Me.scrTiles.Location = New System.Drawing.Point(22, 0)
        Me.scrTiles.Maximum = 11
        Me.scrTiles.Name = "scrTiles"
        Me.scrTiles.Size = New System.Drawing.Size(17, 407)
        Me.scrTiles.TabIndex = 1
        '
        'picSource
        '
        Me.picSource.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picSource.Image = CType(resources.GetObject("picSource.Image"), System.Drawing.Image)
        Me.picSource.Location = New System.Drawing.Point(40, 24)
        Me.picSource.Name = "picSource"
        Me.picSource.Size = New System.Drawing.Size(132, 132)
        Me.picSource.TabIndex = 28
        Me.picSource.TabStop = False
        '
        'picSelection
        '
        Me.picSelection.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picSelection.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picSelection.Location = New System.Drawing.Point(40, 0)
        Me.picSelection.Name = "picSelection"
        Me.picSelection.Size = New System.Drawing.Size(20, 20)
        Me.picSelection.TabIndex = 29
        Me.picSelection.TabStop = False
        '
        'cmnTile
        '
        Me.cmnTile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.cmnTileInfo, Me.cmnTileDelete, Me.cmnTileInsert, Me.MenuItem5, Me.cmnTileTable, Me.cmnTilesJump})
        '
        'cmnTileInfo
        '
        Me.cmnTileInfo.Index = 0
        Me.cmnTileInfo.Text = "Info..."
        '
        'cmnTileDelete
        '
        Me.cmnTileDelete.Index = 1
        Me.cmnTileDelete.Text = "Delete Tile"
        '
        'cmnTileInsert
        '
        Me.cmnTileInsert.Index = 2
        Me.cmnTileInsert.Text = "Insert Tile"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 3
        Me.MenuItem5.Text = "-"
        '
        'cmnTileTable
        '
        Me.cmnTileTable.Index = 4
        Me.cmnTileTable.Text = "Start Table Here..."
        '
        'cmnTilesJump
        '
        Me.cmnTilesJump.Index = 5
        Me.cmnTilesJump.Text = "Jump to table..."
        '
        'picSrc
        '
        Me.picSrc.Image = CType(resources.GetObject("picSrc.Image"), System.Drawing.Image)
        Me.picSrc.Location = New System.Drawing.Point(80, 48)
        Me.picSrc.Name = "picSrc"
        Me.picSrc.Size = New System.Drawing.Size(64, 64)
        Me.picSrc.TabIndex = 30
        Me.picSrc.TabStop = False
        Me.picSrc.Visible = False
        '
        'picComboPreview
        '
        Me.picComboPreview.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picComboPreview.Location = New System.Drawing.Point(48, 160)
        Me.picComboPreview.Name = "picComboPreview"
        Me.picComboPreview.Size = New System.Drawing.Size(120, 96)
        Me.picComboPreview.TabIndex = 31
        Me.picComboPreview.TabStop = False
        '
        'conversionTableSaver
        '
        Me.conversionTableSaver.Filter = "Conversion Table (*.ct)|*.ct|All Files (*.*)|*.*"
        Me.conversionTableSaver.Title = "Save Table"
        '
        'frmMain
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(176, 407)
        Me.Controls.Add(Me.picComboPreview)
        Me.Controls.Add(Me.picSrc)
        Me.Controls.Add(Me.picSelection)
        Me.Controls.Add(Me.picSource)
        Me.Controls.Add(Me.scrTiles)
        Me.Controls.Add(Me.picTiles)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Menu = Me.mnu
        Me.Name = "frmMain"
        Me.Text = "Combulator"
        Me.picTiles.ResumeLayout(False)
        CType(Me.ComboHighlight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picSelection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picSrc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picComboPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Shared Sub Main()
        Application.EnableVisualStyles()
        Application.DoEvents()
        Application.Run(New frmMain)
    End Sub

    Private Sub mnuFileOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileOpen.Click
        odfOpenRom.ShowDialog()
        Dim FileNumber As Integer = FreeFile()
        FileOpen(FileNumber, odfOpenRom.FileName, OpenMode.Binary, OpenAccess.Read)
        ComboData = New RomInfo(FileNumber)
        FileClose(FileNumber)
        bmpTiles = New Bitmap(picTiles.Width, Screen.PrimaryScreen.Bounds.Height, Imaging.PixelFormat.Format24bppRgb)
        gTiles = Graphics.FromImage(bmpTiles)
        gTiles.Clear(SystemColors.Control)
        picTiles.BackgroundImage = bmpTiles
        scrTiles.Maximum = RomInfo.ComboLen
        DrawCombos()
        'picTiles.Image = bmpTiles
        picTiles.Invalidate()
        'MessageBox.Show(Combos.TableLocation(1).ToString)
        'Combos.TableLocation(1) = Combos.TableLocation(1)
        'MessageBox.Show(Combos.TableLocation(1).ToString)
    End Sub

    Private Sub mnuFileExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileExit.Click
        Me.Close()
    End Sub

    Private Sub scrTiles_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles scrTiles.Scroll
        DrawCombos()
        picTiles.Invalidate()
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub picSource_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picSource.MouseDown
        Const TileSize As Integer = 16
        Const TableWidth As Integer = 8
        Try
            SelectedTile = CByte((e.X \ TileSize) + (e.Y \ TileSize) * TableWidth)
        Catch
            SelectedTile = 0
        End Try
        Dim srcRect As New RectangleF((SelectedTile Mod TableWidth) * TileSize, (SelectedTile \ TableWidth) * TileSize, TileSize, TileSize)
        gSelection.DrawImage(picSource.Image, 0, 0, srcRect, GraphicsUnit.Pixel)
        picSelection.Invalidate()
    End Sub

    'Gets the byte offset in the tile data for the specified pixel location
    Public Function TileAt(ByVal Y As Integer) As Integer
        Const TileSize As Integer = 16
        Y = Y \ TileSize    'Convert from pixel # to tile #
        Dim DataPos As Integer = scrTiles.Value
        Dim ScreenPos As Integer = 0

        '        Dim TileNumber As Integer = scrTiles.Value
        If CBool(ComboData.ComboBytes(DataPos) And ComboData.Combo.Doubled) Then ScreenPos += 1
        Do Until ScreenPos >= Y
            ScreenPos += 1
            DataPos += 1
            If (DataPos >= ComboData.ComboBytes.Length) Then Return -1

            If CBool(ComboData.ComboBytes(DataPos) And ComboData.Combo.Doubled) Then ScreenPos += 1
        Loop
        Return DataPos
    End Function

    Private Sub picTiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picTiles.Click

    End Sub

    Private Sub picTiles_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picTiles.MouseDown
        If (ComboData Is Nothing) Then Return
        'MessageBox.Show(TileAt(e.Y).ToString)
        ClickedTile = TileAt(e.Y)
        If ClickedTile < 0 Then Return

        If e.Button = MouseButtons.Left Then
            If CBool(ModifierKeys And Keys.Shift) Then
                ComboData.ComboBytes(ClickedTile) = ComboData.ComboBytes(ClickedTile) Xor ComboData.Combo.Start
            ElseIf CBool(ModifierKeys And Keys.Control) Then
                ComboData.ComboBytes(ClickedTile) = ComboData.ComboBytes(ClickedTile) Xor ComboData.Combo.Doubled
            Else
                ComboData.ComboBytes(ClickedTile) = (ComboData.ComboBytes(ClickedTile) And Not CByte(ComboData.Combo.TileValue)) Or SelectedTile
            End If
        ElseIf e.Button = MouseButtons.Right Then
            If CBool(ModifierKeys And Keys.Shift) Then
                ComboData.DeleteCombo(ClickedTile, MnuOptionsAutopointers.Checked)
            ElseIf CBool(ModifierKeys And Keys.Control) Then
                ComboData.DuplicateCombo(ClickedTile, MnuOptionsAutopointers.Checked)
            Else
                cmnTile.Show(picTiles, New Point(e.X, e.Y))
            End If
        End If
        DrawCombos()
        'picTiles.Image = bmpTiles


    End Sub

    Private Sub mnuFileSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileSave.Click
        Dim FileNumber As Integer = FreeFile()
        FileOpen(FileNumber, odfOpenRom.FileName, OpenMode.Binary, OpenAccess.ReadWrite)
        ComboData.WriteCombosToFile(FileNumber)
        FileClose(FileNumber)
    End Sub

    Private Sub mnuViewTableLocations_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuViewTableLocations.Click
        Dim NewForm As New Form
        Dim NewListBox As New ListBox
        NewListBox.Dock = DockStyle.Fill
        NewForm.Controls.Add(NewListBox)
        NewForm.ShowInTaskbar = False
        NewForm.Text = "Table locations"
        NewListBox.IntegralHeight = False
        For i As Integer = 0 To 15
            NewListBox.Items.Add(ComboData.TableLocation(i).ToString & " $" & Hex(ComboData.TableLocation(i)) & "  Tile#-" & ComboData.RomTableValue(i))
        Next
        NewListBox.Font = New Font("Courier New", 10, FontStyle.Regular, GraphicsUnit.Point, 0)
        NewForm.Show()
    End Sub

    Private Sub mnuViewExpanded_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuViewExpanded.Click
        Dim NewOrganizer As New Organizer(ComboData)
        NewOrganizer.Show()
        NewOrganizer.AddCombos()
    End Sub

    Private Sub mnuViewTables_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuViewTables.Click
        Dim NewOrganizer As New Organizer(ComboData)
        NewOrganizer.Show()
        NewOrganizer.AddCombosByTable()
    End Sub

    Private Sub cmnTileDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnTileDelete.Click
        ComboData.DeleteCombo(ClickedTile, MnuOptionsAutopointers.Checked)
        DrawCombos()
    End Sub

    Private Sub MnuOptionsAutopointers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuOptionsAutopointers.Click
        MnuOptionsAutopointers.Checked = Not MnuOptionsAutopointers.Checked
    End Sub

    Private Sub cmnTileInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnTileInsert.Click
        ComboData.DuplicateCombo(ClickedTile, MnuOptionsAutopointers.Checked)
        DrawCombos()
    End Sub

    Private Sub cmnTileTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnTileTable.Click
        Dim TableSelector As New TableSelector
        Dim TableNumber As Integer = TableSelector.ShowDialog(ComboData, ClickedTile)
        TableSelector.Dispose()
        If TableNumber > -1 Then ComboData.TableLocation(TableNumber) = ClickedTile
    End Sub

    Private Sub DrawCombos()
        ComboData.DrawCombos(scrTiles.Value, bmpTiles.Height \ 16 + 1, Graphics.FromImage(bmpTiles), DirectCast(picSource.Image, Bitmap))
        picTiles.Invalidate()
        picComboPreview.Invalidate()
    End Sub

    Private Sub mnuViewJump_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuViewJump.Click
        Dim TableSelector As New TableSelector
        Dim TableNumber As Integer = TableSelector.ShowDialog(ComboData, ClickedTile, True)
        TableSelector.Dispose()
        If TableNumber > -1 Then scrTiles.Value = ComboData.TableLocation(TableNumber)
        DrawCombos()
    End Sub

    Private Sub cmnTilesJump_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnTilesJump.Click
        Dim TableSelector As New TableSelector
        Dim TableNumber As Integer = TableSelector.ShowDialog(ComboData, ClickedTile, True)
        TableSelector.Dispose()
        If TableNumber > -1 Then scrTiles.Value = ComboData.TableLocation(TableNumber)
        DrawCombos()
    End Sub

    Private Sub cmnTileInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnTileInfo.Click
        Dim MessageText As String = "Tile " & (ComboData.ComboBytes(ClickedTile) And ComboData.Combo.TileValue).ToString & " - " & ComboData.Descriptions((ComboData.ComboBytes(ClickedTile) And ComboData.Combo.TileValue))
        If CBool(ComboData.ComboBytes(ClickedTile) And ComboData.Combo.Doubled) Then MessageText &= ControlChars.CrLf & "(Doubled)"
        If CBool(ComboData.ComboBytes(ClickedTile) And ComboData.Combo.Start) Then MessageText &= ControlChars.CrLf & "(Combo Start)"
        MessageText &= ControlChars.CrLf & "Offset - 0x" & ClickedTile.ToString("X")

        MessageBox.Show(MessageText, "Combo info")
    End Sub

    Private Sub mnuFileConvert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileConvert.Click
        If Me.odfOpenRom.ShowDialog() = DialogResult.OK Then
            Dim FileNumber As Integer = FreeFile()
            FileOpen(FileNumber, odfOpenRom.FileName, OpenMode.Binary)
            Dim Converter As New ComboConverter(Me.ComboData, FileNumber)
            FileClose()
        End If
    End Sub

    Private Sub picTiles_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles picTiles.MouseEnter
        ComboHighlight.Visible = True
    End Sub

    Private Sub picTiles_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles picTiles.MouseLeave
        ComboHighlight.Visible = False
    End Sub

    Private Sub picTiles_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picTiles.MouseMove
        If (ComboData Is Nothing) Then Return

        Dim y As Integer = e.Y
        If y > 16 Then
            If TileAt(y) = TileAt(y - 16) Then
                y -= 16
            End If
        End If

        ComboHighlight.Top = y - y Mod 16
    End Sub

    Private Sub ComboHighlight_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ComboHighlight.MouseMove
        picTiles_MouseMove(sender, New MouseEventArgs(e.Button, e.Clicks, e.X, e.Y + ComboHighlight.Top, e.Delta))
    End Sub

    Private Sub ComboHighlight_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ComboHighlight.MouseDown
        picTiles_MouseDown(sender, New MouseEventArgs(e.Button, e.Clicks, e.X, e.Y + ComboHighlight.Top, e.Delta))
    End Sub

    Private Sub frmMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.C Then
            ComboHighlight.Visible = True
        End If
    End Sub

    Private Sub frmMain_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.C Then
            ComboHighlight.Visible = False
        End If
    End Sub

    Private Sub mnuOptionsAutotable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOptionsAutotable.Click
        ' These values will be initialized to (zero based) tile # 1, table # 1, and combo # 1 because the first table
        ' can not be moved and the first tile must be assumed to be the first combo and table
        Dim comboCount As Integer = 1
        Dim tableIndex As Integer = 1
        Dim tileIndex As Integer = 1

        While tileIndex < ComboData.ComboBytes.Length
            If (ComboData.ComboBytes(tileIndex) And ComboData.Combo.Start) = RomInfo.Combo.Start Then
                If comboCount = 0 Then
                    If (tableIndex = 16) Then
                        MessageBox.Show("The number of combos exceeded 256 and can not all be placed into tables.")
                        Return
                    End If
                    ComboData.TableLocation(tableIndex) = tileIndex
                    tableIndex += 1
                End If
                comboCount = (comboCount + 1) Mod 16
            End If

            tileIndex += 1
        End While
    End Sub


    Private Sub mnuOptionErrorcheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOptionErrorcheck.Click
        PerformErrorCheck()
    End Sub

    ' Checks for extraneous combos between tables, extraneous tiles between combos, and combos that end in the middle of a doubled tile
    Sub PerformErrorCheck()
        Dim errors As New System.Text.StringBuilder

        Dim tileInComboIndex As Integer = 0
        Dim comboInTableIndex As Integer = 0

        For i As Integer = 0 To ComboData.ComboBytes.Length - 1
            tileInComboIndex += 1
            If (ComboData.ComboBytes(i) And ComboData.Combo.Start) = ComboData.Combo.Start Then
                tileInComboIndex = 0
                comboInTableIndex += 1

                If TestForDoubleEnd(i) Then errors.Append("Last tile doubled at offset 0x" + i.ToString("X") + Environment.NewLine)
                If comboInTableIndex > 16 Then errors.Append("Extraneous combo at offset 0x" + i.ToString("X") + Environment.NewLine)
            End If

            For table As Integer = 0 To 15
                If (ComboData.TableLocation(table) = i) Then
                    comboInTableIndex = 0
                    tileInComboIndex = 0
                End If
            Next

            If tileInComboIndex > 11 Then errors.Append("Extraneous tile at offset 0x" + i.ToString("X") + Environment.NewLine)

            If (ComboData.ComboBytes(i) And ComboData.Combo.Doubled) = ComboData.Combo.Doubled Then tileInComboIndex += 1
        Next
        If (errors.Length = 0) Then
            MessageBox.Show("No errors.")
        Else
            MessageBox.Show(errors.ToString())
        End If

    End Sub

    Private Function TestForDoubleEnd(ByVal i As Integer) As Boolean
        Dim tiley As Integer = 0
        While tiley < 11
            If (tiley >= ComboData.ComboBytes.Length) Then Return False
            If (ComboData.ComboBytes(i) And ComboData.Combo.Doubled) = ComboData.Combo.Doubled Then
                tiley += 1
                If (tiley = 11) Then Return True
            End If

            tiley += 1

            i += 1
        End While

        Return False

    End Function


    Private Sub picComboPreview_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles picComboPreview.Paint
        If (ComboData Is Nothing) Then Return

        'Get the first visible tile
        Dim tileIndex As Integer = TileAt(0)
        Dim screenY As Integer = 0
        Dim tileCount As Integer = (ClientSize.Height + 15) \ 16 + 1
        Dim drawnComboX As Integer = 0

        While tileIndex < ComboData.ComboBytes.Length And screenY < tileCount
            If (ComboData.ComboBytes(tileIndex) And ComboData.Combo.Start) = ComboData.Combo.Start Then
                DrawPreviewCombo(e.Graphics, tileIndex, drawnComboX)
                drawnComboX += 1
            End If

            screenY += 1
            If (ComboData.ComboBytes(tileIndex) And ComboData.Combo.Doubled) = ComboData.Combo.Doubled Then
                screenY += 1
            End If

            tileIndex += 1

        End While
    End Sub

    Sub DrawPreviewCombo(ByVal g As Graphics, ByVal tileIndex As Integer, ByVal screenX As Integer)
        Dim srcRect As New Rectangle(0, 0, 8, 8)
        Dim dstRect As New Rectangle(screenX * 8, 0, 8, 8)

        Dim y As Integer
        While y < 11
            If (tileIndex >= ComboData.ComboBytes.Length) Then Return
            Dim tile As Integer = ComboData.ComboBytes(tileIndex) And ComboData.Combo.TileValue
            Dim srcX As Integer = tile Mod 8
            Dim srcY As Integer = tile \ 8
            srcRect.X = srcX * 8
            srcRect.Y = srcY * 8
            g.DrawImage(picSrc.Image, dstRect, srcRect, GraphicsUnit.Pixel)
            If (ComboData.ComboBytes(tileIndex) And ComboData.Combo.Doubled) = ComboData.Combo.Doubled Then
                dstRect.Y += 8
                y = y + 1
                g.DrawImage(picSrc.Image, dstRect, srcRect, GraphicsUnit.Pixel)
            End If

            dstRect.Y += 8
            y = y + 1
            tileIndex += 1
        End While
    End Sub

    Private Sub mnuOptionsCreateconversion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOptionsCreateconversion.Click
        If (odfOpenRom.ShowDialog() = DialogResult.OK) Then
            Dim filenum As Integer = FreeFile()
            FileOpen(filenum, odfOpenRom.FileName, OpenMode.Binary, OpenAccess.Read)
            Dim convertFrom As New RomInfo(filenum)
            FileClose(filenum)

            Dim currentSet As ComboTiles() = ExtractCombos(ComboData) ' New ComboTiles(256) {}
            Dim oldSet As ComboTiles() = ExtractCombos(convertFrom) 'New ComboTiles(256) {}

            Dim ConversionTable As Byte() = New Byte(256) {}
            Dim MissingCombos As New ArrayList
            For i As Integer = 0 To 255
                If (oldSet(i).Bytes Is Nothing) Then
                    MissingCombos.Add("Combo 0x" & i.ToString("x") & " not defined")
                Else
                    Dim mappedIndex As Integer = FindComboMatch(oldSet(i), currentSet)
                    If (mappedIndex = -1) Then
                        MissingCombos.Add("Could not map combo 0x" & i.ToString("x") & ".")
                    Else
                        ConversionTable(i) = CByte(mappedIndex)
                    End If
                End If
            Next
            MessageBox.Show( _
                String.Join(Environment.NewLine, DirectCast(MissingCombos.ToArray(GetType(String)), String())), _
                "Results")

            If conversionTableSaver.ShowDialog() = DialogResult.OK Then
                Dim fs As New System.IO.FileStream(conversionTableSaver.FileName, IO.FileMode.Create)
                fs.Write(ConversionTable, 0, 256)
                fs.Close()
            End If



        End If
    End Sub

    'Returns a number greater than or equal to zero as the index of a match, -1 if a match was not found.
    Private Function FindComboMatch(ByVal combo As ComboTiles, ByVal SearchArray As ComboTiles()) As Integer
        If combo.Bytes Is Nothing Then Throw New ArgumentException("Uninitialized/empty combo is not valid.", "combo")
        For i As Integer = 0 To SearchArray.Length - 1
            If combo.CompareEquality(combo, SearchArray(i)) Then Return i
        Next

        Return -1
    End Function

    Private Function ExtractCombos(ByVal rom As RomInfo) As ComboTiles()
        Dim Result As ComboTiles() = New ComboTiles(256) {}
        For Table As Integer = 0 To 15
            Dim byteOffset As Integer = rom.TableLocation(Table)
            For ComboCount As Integer = 0 To 15

                ' Seek to next combo
                While byteOffset < rom.ComboBytes.Length AndAlso (rom.ComboBytes(byteOffset) And RomInfo.Combo.Start) <> RomInfo.Combo.Start
                    byteOffset += 1
                End While

                ' Extract combo and store to result
                Result(Table * 16 + ComboCount) = New ComboTiles(rom.ComboBytes, byteOffset)

                ' Seek past extracted combo
                byteOffset += 1
            Next
        Next

        Return Result
    End Function

End Class

Public Class Organizer
    Inherits System.Windows.Forms.Form
    Dim ComboCount As Integer
    Dim Combodata As RomInfo
#Region " Windows Form Designer generated code "

    Public Sub New(ByVal ROM As RomInfo)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.ComboData = ROM
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
    Friend WithEvents picSrc As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Organizer))
        Me.picSrc = New System.Windows.Forms.PictureBox
        Me.SuspendLayout()
        '
        'picSrc
        '
        Me.picSrc.Image = CType(resources.GetObject("picSrc.Image"), System.Drawing.Image)
        Me.picSrc.Location = New System.Drawing.Point(0, 0)
        Me.picSrc.Name = "picSrc"
        Me.picSrc.Size = New System.Drawing.Size(64, 64)
        Me.picSrc.TabIndex = 0
        Me.picSrc.TabStop = False
        Me.picSrc.Visible = False
        '
        'Organizer
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(512, 352)
        Me.Controls.Add(Me.picSrc)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "Organizer"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.Text = "Organizer"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub AddCombos()
        Me.ClientSize = New System.Drawing.Size(512, 352)
        Dim i As Integer
        Dim NewLoc As New Point
        For i = 0 To RomInfo.ComboLen
            If CBool(ComboData.ComboBytes(i) And RomInfo.Combo.Start) Then
                Dim NewCombo As New ComboControl
                NewCombo.Location = NewLoc
                DrawCombo(i, NewCombo)
                Me.Controls.Add(NewCombo)
                ComboCount += 1
                NewLoc.X += 8
                If NewLoc.X = 512 Then
                    NewLoc.X = 0
                    NewLoc.Y += 88
                End If
            End If
        Next i
    End Sub
    Public Sub AddCombosByTable()
        Me.ClientSize = New System.Drawing.Size(536, 472)
        Dim NewLoc As New Point
        For i As Integer = 0 To 15
            Dim Pointer As Integer = Combodata.TableLocation(i)
            For j As Integer = 0 To 15
                Do Until CBool(combodata.ComboBytes(Pointer) And RomInfo.Combo.Start)
                    Pointer += 1
                    If Pointer >= RomInfo.ComboLen Then Return
                Loop
                Dim NewCombo As New ComboControl
                NewCombo.Location = NewLoc
                NewCombo.Tag = i * 16 + j
                DrawCombo(Pointer, NewCombo)
                Me.Controls.Add(NewCombo)
                ComboCount += 1
                NewLoc.X += 8
                Pointer += 1
                If Pointer >= RomInfo.ComboLen Then Return
            Next j
            NewLoc.X += 8
            If NewLoc.X = 544 Then
                NewLoc.X = 0
                NewLoc.Y += 96
            End If
        Next i
    End Sub



    Private Sub DrawCombo(ByVal ComboLoc As Integer, ByVal Control As ComboControl)
        Const TileSize As Integer = 8
        Dim tilecount As Integer = 0
        Dim G As Graphics = Graphics.FromImage(Control.BGImage())
        Dim rectSource As New Rectangle(0, 0, 16, 16)
        Do
            rectSource.Location = New Point(((Combodata.ComboBytes(ComboLoc) And 63) Mod 8) * TileSize, ((Combodata.ComboBytes(ComboLoc) And 63) \ 8) * TileSize)
            G.DrawImage(picSrc.Image, 0, tilecount * TileSize, rectSource, GraphicsUnit.Pixel)
            tilecount += 1
            If CBool(combodata.ComboBytes(ComboLoc) And RomInfo.Combo.Doubled) Then
                G.DrawImage(picSrc.Image, 0, tilecount * TileSize, rectSource, GraphicsUnit.Pixel)
                tilecount += 1
            End If
            ComboLoc += 1
        Loop While tilecount < 11 And ComboLoc < RomInfo.ComboLen
    End Sub
End Class

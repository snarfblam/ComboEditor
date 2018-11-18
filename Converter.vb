
Public Class Converter
    Dim Dialog As New OpenFileDialog
    Dim DisplayForm As Form, Label As Label
    Dim ComboTiles()(,) As Byte = {New Byte(255, 10) {}, New Byte(255, 10) {}} 'Two rectangular arrays: one for each ROM
    Dim Checksum()() As Integer = {New Integer(255) {}, New Integer(255) {}} 'Checksum array
    Dim OldROM As String
    Dim NewROM As String

    Dim TableLocations As Byte()(,) = {New Byte(15, 1) {}, New Byte(15, 1) {}}
    Const ComboDataStart As Integer = 89048 + 1
    Const TablePointersOffset As Integer = &H19D1F + 1

    Public Sub New()
        Dialog.CheckFileExists = True
        Dialog.FileName = "Zelda.nes"
        Dialog.Filter = "NES ROM (*.nes)|*.nes|All files (*.*)|*.*"

        DisplayForm = New Form
        Label = New Label
        DisplayForm.FormBorderStyle = FormBorderStyle.FixedToolWindow
        DisplayForm.Text = "Table converter"
        Label.Text = "Please select your ROMS."
        Label.Dock = DockStyle.Fill
        Label.Location = New Point(0, 0)
        DisplayForm.Controls.Add(Label)
        DisplayForm.Show	

        'Original first, THEN NEW ROM
        Dialog.Title = "Origional ROM"
        If Dialog.ShowDialog = DialogResult.OK Then OldROM = Dialog.FileName
        Dialog.Title = "New ROM"
        If Dialog.ShowDialog = DialogResult.OK Then NewROM = Dialog.FileName
        Dim OldFile As Integer = FreeFile()
        FileOpen(OldFile, OldROM, OpenMode.Binary, OpenAccess.ReadWrite)
        Dim NewFile As Integer = FreeFile()
        FileOpen(NewFile, NewROM, OpenMode.Binary, OpenAccess.ReadWrite)

        Label.Text = "Reading Tables"
        Application.DoEvents()
        TableLocations(0) = ReadTable(OldFile)
        TableLocations(1) = ReadTable(NewFile)

        Label.Text = "Reading Combos"
        Application.DoEvents()
        ReadCombos(OldFile, 0)
        ReadCombos(NewFile, 1)
        FileClose(New Integer() {OldFile, NewFile})
    End Sub

    Private Function ReadTable(ByVal FileNumber As Integer) As Byte(,)
        Dim Table(15, 1) As Byte
        Seek(FileNumber, TablePointersOffset)
        For i As Integer = 0 To 15
            FileGet(FileNumber, Table(i, 0))
            FileGet(FileNumber, Table(i, 1))
        Next
        Return Table
    End Function

    Private Sub ReadCombos(ByVal FileNumber As Integer, ByVal ROM As Integer)
        Dim FilePosition As Integer
        Dim ComboByte As Byte
        For Tables As Integer = 0 To 15
            FilePosition = Me.TableLocationOffset(Tables, ROM)

            'This loop searches the ROM and records the locations of combos
            FileGet(FileNumber, ComboByte, FilePosition)
            For Combos As Integer = 0 To 15
                Do Until CBool(ComboByte And RomInfo.Combo.Start)
                    FilePosition += 1                     '   > This part is tested and works in a good rom!
                    FileGet(FileNumber, ComboByte, FilePosition)
                    If FilePosition > 140000 Then
                        Throw New Exception("The ROM being accessed appears to be corrupt and could not be loaded.")
                    End If
                Loop
                ExtractCombo(FileNumber, FilePosition, Tables, Combos, ROM)
            Next Combos
        Next Tables

        '    'This loop goes to the locations found and loads the combos
        '    For Combos = 0 To 15
        '        FilePosition = ComboStart(Combos)
        '        SizeSoFar = 0
        '        Do
        '            FileGet(FileNumber, DataIn, FilePosition + Adjust)
        '            FilePosition += 1
        '            ComboTable(Tables * 16 + Combos, SizeSoFar) = DataIn And ComboNumberMask
        '            If (SizeSoFar < 10) AndAlso (DataIn And DoubleFlag) Then   'and this tile had the double flag set then
        '                SizeSoFar += 1
        '                ComboTable(Tables * 16 + Combos, SizeSoFar) = DataIn And ComboNumberMask
        '            End If
        '            SizeSoFar += 1                    'Add 1 to the size for this tile
        '        Loop While SizeSoFar < 11
        '    Next Combos
        'Next Tables
    End Sub

    Sub ExtractCombo(ByVal FileNumber As Integer, ByVal FilePosition As Integer, ByVal Table As Integer, ByVal ComboNumber As Integer, ByVal ROM As Integer)
        Dim ComboByte As Byte, TileNumber As Integer
        Do Until TileNumber >= 11
            FileGet(FileNumber, ComboByte, FilePosition)
            ComboTiles(ROM)(Table, ComboNumber) = (ComboByte And RomInfo.Combo.TileValue)
            MessageBox.Show((ComboByte And RomInfo.Combo.TileValue).ToString)
            TileNumber += 1
            FilePosition += 1
            If TileNumber < 11 Then
                If CBool(ComboByte And RomInfo.Combo.Doubled) Then
                    ComboTiles(ROM)(Table, ComboNumber) = (ComboByte And RomInfo.Combo.TileValue)
                    TileNumber += 1
                    FilePosition += 1
                End If
            End If
        Loop
    End Sub


    Public Property TableLocationTile(ByVal Index As Integer, ByVal Table As Integer) As Integer
        Get
            Return TableLocationRaw(Index, Table) - TableLocationRaw(0, Table)
        End Get
        Set(ByVal Value As Integer)
            If Index = 0 Then
                Throw New Exception("Base table address [TableLocation(0)] can not be modified")
            Else
                TableLocationRaw(Index, Table) = Value + TableLocationRaw(0, Table)
            End If
        End Set
    End Property

    Public Property TableLocationRaw(ByVal Index As Integer, ByVal Table As Integer) As Integer
        Get
            Return CInt(TableLocations(Table)(Index, 1)) * 256 + TableLocations(Table)(Index, 0)
        End Get
        Set(ByVal Value As Integer)
            TableLocations(Table)(Index, 1) = CByte(Value \ 256)
            TableLocations(Table)(Index, 0) = CByte(Value Mod 256)
        End Set
    End Property

    Public Function TableLocationOffset(ByVal Index As Integer, ByVal Table As Integer) As Integer
        Return TableLocationTile(Index, Table) + ComboDataStart
    End Function
End Class

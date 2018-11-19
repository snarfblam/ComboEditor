
Public Class RomInfo
    Dim TableLocations As Byte(,) ' = {89048, 89101, 89150, 89216, 89284, 89334, 89394, 89453, 89512, 89574, 89639, 89708, 89769, 89823, 89889, 89941}
    Public Const ComboDataStart As Integer = 89048
    Public Const ComboLen As Integer = 963
    Public ComboBytes(ComboLen) As Byte
    Dim ComboStart(256) As Integer
    Public ComboCount As Integer
    Const Adjust As Integer = 16     'Offset of the data in the rom, due to the header
    'Const TablePointersOffset As Integer = 105743 + Adjust + 1
    Const TablePointersOffset As Integer = &H19D1F + 1

    Public Enum Combo As Byte
        TileValue = 63
        Doubled = 64
        Start = 128
    End Enum
    Public Sub New(ByVal FileNumber As Integer)
        Dim DataIn As Byte          'Binary file access data buffer
        Dim Fileposition As Integer
        Dim SizeSoFar As Integer    'Number of tiles of each combo loaded so far


        TableLocations = New Byte(15, 1) {}
        Seek(FileNumber, TablePointersOffset)
        For i As Integer = 0 To 15
            FileGet(FileNumber, TableLocations(i, 0))
            FileGet(FileNumber, TableLocations(i, 1))
        Next

        Seek(FileNumber, ComboDataStart + Adjust + 1)
        For i As Integer = 0 To ComboLen
            FileGet(FileNumber, ComboBytes(i))
        Next
    End Sub

    Public Sub WriteCombosToFile(ByVal FileNumber As Integer)
        Const TableEnd As Integer = 90025
        Dim DataIn As Byte          'Binary file access data buffer
        Dim Fileposition As Integer
        Dim SizeSoFar As Integer    'Number of tiles of each combo loaded so far


        Seek(FileNumber, TablePointersOffset)
        For i As Integer = 0 To 15
            FilePut(FileNumber, TableLocations(i, 0))
            FilePut(FileNumber, TableLocations(i, 1))
        Next

        Seek(FileNumber, ComboDataStart + Adjust + 1)
        For i As Integer = 0 To ComboLen
            FilePut(FileNumber, ComboBytes(i))
        Next

    End Sub

    Public Property RomTableValue(ByVal Index As Integer) As Integer
        Get
            Return CInt(TableLocations(Index, 1)) * 256 + TableLocations(Index, 0)
        End Get
        Set(ByVal Value As Integer)
            TableLocations(Index, 1) = CByte(Value \ 256)
            TableLocations(Index, 0) = CByte(Value Mod 256)
        End Set
    End Property

    Public Property TableLocation(ByVal Index As Integer) As Integer
        Get
            Return RomTableValue(Index) - RomTableValue(0)
        End Get
        Set(ByVal Value As Integer)
            If Index = 0 Then
                Throw New Exception("Base table address [TableLocation(0)] can not be modified")
            Else
                RomTableValue(Index) = Value + RomTableValue(0)
            End If
        End Set
    End Property

    Public Function NextTable(ByVal AfterTile As Integer) As Integer
        Dim i As Integer
        Do Until TableLocation(i) > AfterTile
            i += 1
            If i > 15 Then Return -1
        Loop
        Return i
    End Function

    Public Function NextTableInclusive(ByVal AfterTile As Integer) As Integer
        Dim i As Integer
        Do Until TableLocation(i) > AfterTile
            i += 1
            If i > 15 Then Return -1
        Loop
        Return i
    End Function

    Public Function OnTable(ByVal Tile As Integer) As Integer
        Dim i As Integer = NextTable(Tile)
        If i > 0 Then
            Return i - 1
        Else
            Return 15
        End If
    End Function

    ' Determines if the specified combo is the first in a combo table.
    Public Function OnPageBoundary(ByVal TileNumber As Integer) As Boolean
        For i As Integer = 0 To 15
            If TableLocation(i) = TileNumber Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Sub DrawCombos(ByVal StartTile As Integer, ByVal Height As Integer, ByVal G As Graphics, ByVal Source As Bitmap)
        Const TileSize As Integer = 16
        Dim StartAt As Integer = -11
        Dim rectSource As New Rectangle(0, 0, 16, 16)
        Dim rectDest As Rectangle = rectSource
        Dim Y As Integer
        Dim TilePen As Pen = New Pen(Color.Black, 2)
        Dim StartPen As Pen = New Pen(Color.Red, 2)
        Dim ClearPen As Pen = New Pen(SystemColors.Control, 2)
        Dim PenToUse As Pen '= TilePen
        Dim FinishPen As Pen = New Pen(Color.Green, 2)
        G.DrawLine(ClearPen, 18, 0, 18, Height * TileSize)
        G.DrawLine(ClearPen, 21, 0, 21, Height * TileSize)

        'Incase we are at the bottom of the combos, adjust height to be the limit of the array
        If Height + StartTile > ComboLen Then
            Height = ComboLen - StartTile
            G.Clear(ClearPen.Color)
        End If

        For i As Integer = 0 To Height
            PenToUse = TilePen
            rectSource.Location = New Point(((ComboBytes(i + StartTile) And Combo.TileValue) Mod 8) * TileSize, ((ComboBytes(i + StartTile) And Combo.TileValue) \ 8) * TileSize)
            'rectSource.Location = New Point(16, 16)
            rectDest.Y = Y * TileSize
            G.DrawImage(Source, rectDest, rectSource, GraphicsUnit.Pixel)
            If CBool(ComboBytes(i + StartTile) And Combo.Start) Then
                PenToUse = StartPen
                G.DrawLine(FinishPen, 21, (Y + 11) * TileSize - 2, 21, (Y + 11) * TileSize)
            End If
            If CBool(ComboBytes(i + StartTile) And Combo.Doubled) Then
                G.DrawLine(PenToUse, 18, rectDest.Y, 18, rectDest.Y + TileSize)
                'G.DrawLine(PenToUse, 19, rectDest.Y, 19, rectDest.Y + TileSize)
                Y += 1
                rectDest.Y = Y * TileSize
                G.DrawImage(Source, rectDest, rectSource, GraphicsUnit.Pixel)
            End If
            G.DrawLine(PenToUse, 18, rectDest.Y, 18, rectDest.Y + TileSize - 2)
            'G.DrawLine(PenToUse, 19, rectDest.Y, 19, rectDest.Y + TileSize - 2)
            Y += 1
        Next
        Y = 0
        For i As Integer = 0 To Math.Max(1, StartTile - 11) - StartTile Step -1
            If CBool(ComboBytes(i + StartTile) And Combo.Start) Then
                G.DrawLine(FinishPen, 21, (Y + 11) * TileSize - 2, 21, (Y + 11) * TileSize)
            End If
            If CBool(ComboBytes(i + StartTile - 1) And Combo.Doubled) Then
                Y -= 1
            End If
            Y -= 1
        Next

    End Sub

    Public Sub DeleteCombo(ByVal TileNumber As Integer, Optional ByVal FixTableBounds As Boolean = True)
        Array.Copy(ComboBytes, TileNumber + 1, ComboBytes, TileNumber, ComboBytes.GetUpperBound(0) - TileNumber)
        If FixTableBounds Then
            If OnPageBoundary(TileNumber) Then MessageBox.Show("The deleted tile was the first in a combo table.")
            Dim i As Integer = NextTable(TileNumber)
            If i <> -1 Then
                For i = i To 15
                    TableLocation(i) -= 1
                Next
            End If
        End If
    End Sub
    Public Sub DuplicateCombo(ByVal TileNumber As Integer, Optional ByVal FixTableBounds As Boolean = True)
        Array.Copy(ComboBytes, TileNumber, ComboBytes, TileNumber + 1, ComboBytes.GetUpperBound(0) - TileNumber - 1)
        If FixTableBounds Then
            If OnPageBoundary(TileNumber) Then MessageBox.Show("The deleted tile was on a page boundary. The tile that has moved into its place should be marked as the beginning of a combo.")
            Dim i As Integer = NextTableInclusive(TileNumber)
            If i <> -1 Then
                For i = i To 15
                    TableLocation(i) += 1
                Next
            End If
        End If
    End Sub

    Public Shared Descriptions As String() = { _
    "Black", _
    "Lines", _
    "Black 2", _
    "Bricks", _
    "Unused", _
    "Water", _
    "Water (Top)", _
    "Water (Bottom)", _
    "Water (Left)", _
    "Water (Right)", _
    "Steps", _
    "Bridge/Dock", _
    "Cave", _
    "Ground (Cave)", _
    "Ground", _
    "Waterfall (Cave)", _
    "Numbers 3547", _
    "Numbers 4657", _
    "Staircase", _
    "Rock", _
    "Grave", _
    "Water (Top Left)", _
    "Water (Top Right)", _
    "Water (Bottom Left)", _
    "Water (Bottom Right)", _
    "Bush", _
    "Mountain (Top)", _
    "Mountain", _
    "Tree (Top Left)", _
    "Tree (Top)", _
    "Tree (Top Right)", _
    "Tree (Bottom Left)", _
    "Tree (Bottom Right)", _
    "Building (Top Left)", _
    "Building (Top)", _
    "Building (Top Right)", _
    "Building (Bottom Left)", _
    "Building (Bottom Right)", _
    "Pushable Rock", _
    "Bombable Cave", _
    "Burnable Bush", _
    "Pushable Grave", _
    "Armos (A)", _
    "Armos (B)", _
    "Armos (C)", _
    "Building (Alternate Top)", _
    "Water (Top Left Ground)", _
    "Water (Top Right Ground)", _
    "Water (Bottom Left Ground)", _
    "Water (Bottom Right Ground)", _
    "Mountain Top Left", _
    "Mountain Top Right", _
    "Mountain Bottom Left", _
    "Mountain Bottom Right", _
    "Waterfall", _
    "Sand", _
    "unused", _
    "unused", _
    "unused", _
    "unused", _
    "unused", _
    "unused", _
    "unused", _
    "unused", _
    "unused"}
End Class

Public Class ComboConverter
    Dim MReplacer() As Byte = New Byte() {189, 110, 78, 33, 118, 12, 190, 120, 186, 30, 73, 139, 125, 153, 154, 96, 30, 73, 139, 125, 153, 154, 96, 65, 121, 94, 87, 234, 13, 92, 11, 57, 87, 234, 13, 92, 11, 57, 56, 55, 61, 63, 59, 58, 26, 25, 14, 176, 59, 58, 26, 25, 14, 176, 227, 69, 72, 175, 222, 209, 98, 143, 144, 225, 222, 209, 98, 143, 144, 225, 185, 152, 153, 54, 71, 129, 67, 19, 160, 139, 71, 129, 67, 19, 160, 139, 38, 60, 124, 159, 66, 101, 156, 138, 137, 40, 159, 66, 101, 156, 138, 137, 40, 41, 149, 37, 31, 115, 42, 62, 53, 89, 37, 31, 115, 42, 62, 53, 89, 32, 224, 223, 105, 75, 150, 216, 51, 50, 223, 105, 75, 150, 216, 51, 50, 149, 77, 39, 4, 126, 76, 230, 23, 131, 39, 4, 126, 76, 230, 23, 131, 49, 123, 162, 171, 181, 178, 28, 29, 194, 162, 171, 181, 178, 28, 29, 194, 199, 79, 82, 232, 47, 48, 71, 221, 36, 232, 47, 48, 71, 221, 36, 132, 45, 74, 27, 1, 24, 68, 164, 165, 167, 27, 1, 24, 68, 164, 165, 167, 168, 134, 7, 228, 43, 183, 147, 9, 184, 7, 228, 43, 183, 147, 9, 184, 16, 17, 226, 225, 85, 186, 34, 15, 212, 226, 225, 85, 186, 34, 15, 212, 208, 18, 203, 206, 21, 20, 16, 35, 122, 203, 206, 21, 20, 16, 35, 122, 52, 22, 240, 0, 0, 0, 0, 0, 0}
    Dim ReplacerArray(255) As Byte
    Dim CheckSum(1, 255) As Integer
    Dim Combos(1, 255, 10) As Byte
    Dim RomData(1) As RomInfo
    Dim Unmatched As String = ""
    Public Event StatusChanged(ByVal e As StatusEventArgs)

    Public Sub New(ByVal FileNumber1 As Integer, ByVal FileNumber2 As Integer)
        RaiseEvent StatusChanged(New StatusEventArgs("Loading ROM 1"))
        RomData(0) = New RomInfo(FileNumber1)
        RaiseEvent StatusChanged(New StatusEventArgs("Loading ROM 2"))
        RomData(1) = New RomInfo(FileNumber2)
        ExtractCombos()
        CreateReplacer()
    End Sub
    Public Sub New(ByVal RomData As RomInfo, ByVal FileNumber As Integer)
        RaiseEvent StatusChanged(New StatusEventArgs("Loading ROM 1"))
        Me.RomData(0) = RomData
        RaiseEvent StatusChanged(New StatusEventArgs("Loading ROM 2"))
        Me.RomData(1) = New RomInfo(FileNumber)
        ExtractCombos()
        CreateSums()
    End Sub
    Private Sub ExtractCombos()
        For ROM As Integer = 0 To 1
            RaiseEvent StatusChanged(New StatusEventArgs("Extracting combos from ROM " & ROM.ToString))
            For Table As Integer = 0 To 15
                Dim TileNumber As Integer = RomData(ROM).TableLocation(Table)
                Dim ComboNumber As Integer = 0
                Do While (TileNumber <= RomInfo.ComboLen) And (ComboNumber < 16)
                    If CBool(RomData(ROM).ComboBytes(TileNumber) And RomInfo.Combo.Start) Then
                        GrabCombo(ROM, Table * 16 + ComboNumber, TileNumber)
                        ComboNumber += 1
                    End If
                    TileNumber += 1
                Loop
            Next
        Next
    End Sub

    Private Sub CreateSums()
        For ROM As Integer = 0 To 1
            RaiseEvent StatusChanged(New StatusEventArgs("Calculating Checksums " & ROM.ToString))
            For Combo As Integer = 0 To 255
                CheckSum(ROM, Combo) = BitConverter.ToInt32(New Byte() { _
                    Combos(ROM, Combo, 0) Xor Combos(ROM, Combo, 4) Xor Combos(ROM, Combo, 8), _
                    Combos(ROM, Combo, 1) Xor Combos(ROM, Combo, 5) Xor Combos(ROM, Combo, 9), _
                    Combos(ROM, Combo, 2) Xor Combos(ROM, Combo, 6) Xor Combos(ROM, Combo, 10), _
                    Combos(ROM, Combo, 3) Xor Combos(ROM, Combo, 7)}, 0)
            Next
        Next
    End Sub

    Private Sub GrabCombo(ByVal ROM As Integer, ByVal Combo As Integer, ByVal Tile As Integer)
        Dim i As Integer
        Dim TileNumber As Integer = Tile
        Do While i < 11
            Combos(ROM, Combo, i) = RomData(i).ComboBytes(TileNumber) And RomInfo.Combo.TileValue
            If CBool(RomData(i).ComboBytes(TileNumber) And RomInfo.Combo.Doubled) And i < 11 Then
                Combos(ROM, Combo, i + 1) = RomData(i).ComboBytes(TileNumber) And RomInfo.Combo.TileValue
                i += 1
                TileNumber += 1
            End If
            i += 1
            TileNumber += 1
        Loop
    End Sub

    Private Sub CreateReplacer()
        RaiseEvent StatusChanged(New StatusEventArgs("Comparing ROMS"))
        For OCombo As Integer = 0 To 255    'Original Combo
            Dim NCombo As Integer = -1 'New combo
            Dim Match As Boolean = False
            Do Until Match
                NCombo += 1
                If NCombo > 255 Then
                    Match = True
                    NCombo = 0
                    Unmatched &= OCombo.ToString & " "
                End If
                If CheckSum(0, OCombo) = CheckSum(1, NCombo) Then
                    Match = Matching(OCombo, NCombo)
                End If
            Loop
            ReplacerArray(OCombo) = CByte(NCombo)
        Next
    End Sub


    Private Function Matching(ByVal i As Integer, ByVal j As Integer) As Boolean
        For Tile As Integer = 0 To 10
            If Combos(0, i, Tile) = Combos(1, j, Tile) Then
                Return False
            End If
        Next
        Return True
    End Function
    Public Class StatusEventArgs
        Inherits EventArgs
        Private m_Status As String

        Public Sub New(ByVal Status As String)
            MyBase.New()

            m_Status = Status
        End Sub

        Public ReadOnly Property Status() As String
            Get
                Return m_Status
            End Get
        End Property
    End Class

    Public Sub Apply(ByVal FileNumber As Integer)
        RaiseEvent StatusChanged(New StatusEventArgs("Loading overworld data"))
        Dim LayoutData As New LayoutData(FileNumber)
        RaiseEvent StatusChanged(New StatusEventArgs("Converting overworld"))
        For i As Integer = 0 To LayoutData.ScreenMax
            LayoutData.Columns(i) = ReplacerArray(LayoutData.Columns(i))
        Next
        RaiseEvent StatusChanged(New StatusEventArgs("Saving overworld data"))
        LayoutData.Save(FileNumber)
    End Sub

    Private Class LayoutData
        Public Columns As Byte()
        Public Const ScreenCount As Integer = 124
        Public Const ScreenMax As Integer = 123
        Const FileOffset As Integer = &H15428
        Public Sub New(ByVal FileNumber As Integer)
            'Dim iScreen As Integer, iColumn As Integer
            Columns = New Byte((ScreenCount * 16) - 1) {}
            FileGet(FileNumber, CType(Columns, Array), FileOffset)
            For i As Integer = 0 To 15
                MsgBox(Columns(i))
            Next
            'For iScreen = 0 To ScreenMax
            '    For iColumn = 0 To 15
            '        'FileGet(1, RomData(iScreen, iColumn), &H15428 + iColumn + iScreen * 16 + 1)
            '    Next
            'Next
        End Sub

        Public Sub Save(ByVal FileNumber As Integer)
            FilePut(FileNumber, CType(Columns, Array), FileOffset)
        End Sub
    End Class
End Class

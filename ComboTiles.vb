Public Structure ComboTiles
    Dim Bytes As Byte()
    Dim chex As Integer

    Public Sub New(ByVal tiles As Byte(), ByVal offset As Integer)
        If (offset >= tiles.Length) Then Return

        Bytes = New Byte(11) {}
        Dim tile As Integer

        While tile < 11
            ' Copy tile from rom data to combo data
            Bytes(tile) = tiles(offset) And RomInfo.Combo.TileValue
            tile = tile + 1

            ' If tile is doubled
            If (tiles(offset) And RomInfo.Combo.Doubled) = RomInfo.Combo.Doubled And tile < 11 Then
                ' Copy tile again
                Bytes(tile) = tiles(offset) And RomInfo.Combo.TileValue
                tile = tile + 1
            End If

            ' Seek to next tile in rom data
            offset = offset + 1

            ' If we haven't found a complete combo and we've run out of data
            If tile < 11 And (offset >= tiles.Length) Then
                'Reset this object to an empty value
                Bytes = Nothing
                Return
            End If

            ' Calculate a checksum
            chex = BitConverter.ToInt32(Bytes, 0) Or BitConverter.ToInt32(Bytes, 4) Or BitConverter.ToInt32(Bytes, 7)
        End While
    End Sub

    ' Compares two ComboTiles objects for equality. If either or both objects are empty (uninitialized or invalid) the result will
    ' be false
    Public Shared Function CompareEquality(ByVal a As ComboTiles, ByVal b As ComboTiles) As Boolean
        If a.Bytes Is Nothing OrElse b.Bytes Is Nothing Then Return False
        If a.chex <> b.chex Then Return False
        If a.Bytes(0) = b.Bytes(0) AndAlso _
            a.Bytes(1) = b.Bytes(1) AndAlso _
            a.Bytes(2) = b.Bytes(2) AndAlso _
            a.Bytes(3) = b.Bytes(3) AndAlso _
            a.Bytes(4) = b.Bytes(4) AndAlso _
            a.Bytes(5) = b.Bytes(5) AndAlso _
            a.Bytes(6) = b.Bytes(6) AndAlso _
            a.Bytes(7) = b.Bytes(7) AndAlso _
            a.Bytes(8) = b.Bytes(8) AndAlso _
            a.Bytes(9) = b.Bytes(9) AndAlso _
            a.Bytes(10) = b.Bytes(10) Then _
            Return True
        Return False
    End Function
End Structure

Imports Microsoft.VisualBasic

Public Class DataAccess
    Public Function UnMaskString(ByVal pv_sIn As String, ByVal pv_iSeed As Int32, ByVal pv_iMask1 As Int32, ByVal pv_iMask2 As Int32, ByVal pv_iMask3 As Int32, ByVal pv_iMask4 As Int32) As String
        Dim sbOut As System.Text.StringBuilder = New System.Text.StringBuilder("")
        Dim s As Int32 = pv_iSeed

        Dim ts As String = pv_sIn

        Dim j As Int32 = 0

        Dim c As Char() = pv_sIn.ToCharArray()

        For k As Int32 = 1 To pv_sIn.Length Step 2
            j += 1

            Dim i1 As Int32 = Microsoft.VisualBasic.AscW(c(k - 1))
            Dim i2 As Int32 = Microsoft.VisualBasic.AscW(c(k))

            If i1 > 57 Then
                i1 -= 55
            Else
                i1 -= 48
            End If

            If i2 > 57 Then
                i2 -= 55
            Else
                i2 -= 48
            End If

            Dim i As Int32 = (i1 * 16) Or i2
            Dim r As Int32 = (i * 119) Mod 256
            Dim x As Int32 = r - j - s

            Do While x < 0
                x += 256
            Loop

            Dim iMask As Int32 = (j Mod 4) + 1

            If iMask = 4 Then
                x = x Xor pv_iMask4
            End If
            If iMask = 3 Then
                x = x Xor pv_iMask3
            End If
            If iMask = 2 Then
                x = x Xor pv_iMask2
            End If
            If iMask = 1 Then
                x = x Xor pv_iMask1
            End If

            sbOut.Append(Microsoft.VisualBasic.Chr(x))

            s = i

        Next

        Return sbOut.ToString()

    End Function

End Class

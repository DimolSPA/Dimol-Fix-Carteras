Public Class Ucode
    Public Function Encripta(ByVal password As String) As String
        Dim encrip As String, count As Integer
        encrip = ""

        password = Trim$(password)

        For count = 1 To Len(Password)
            encrip = encrip & Chr(Asc(Mid$(password, count, 1)) + 73 + count)
        Next

        Encripta = encrip

    End Function

    Public Function Desencripta(ByVal psw_encriptada As String) As String
        Dim result As String, count As Integer
        Dim PasLet As String
        Dim AscPas As String

        result = ""
        psw_encriptada = Trim$(psw_encriptada)

        For count = 1 To Len(psw_encriptada)
            AscPas = Mid$(psw_encriptada, count, 1)
            PasLet = Chr(Asc(AscPas) - 73 - count)
            result = result & PasLet
        Next

        Desencripta = result

    End Function
End Class

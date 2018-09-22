'
' Multiple File Renamer -> Class FORM1
' Author: Bobby Georgiou
' Date: 2017
'
Imports System.IO

Public Class Form1
    Public DirFileCount As Integer
    Public DirFiles As System.Collections.ObjectModel.ReadOnlyCollection(Of String)
    Public AddMode As String
    Public MatchCase As Boolean

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label3.Visible = False
        Button2.Enabled = False
        Button3.Enabled = False
        GroupBox2.Enabled = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FolderBrowserDialog1.ShowDialog()
        If FolderBrowserDialog1.SelectedPath <> "" Then
            TextBox1.Text = FolderBrowserDialog1.SelectedPath
        Else
            Exit Sub
        End If
        Label3.Visible = True
        GroupBox2.Enabled = True
        GetFiles()
    End Sub

    Sub GetFiles()
        DirFiles = My.Computer.FileSystem.GetFiles(TextBox1.Text, FileIO.SearchOption.SearchTopLevelOnly)
        DirFileCount = DirFiles.Count
        If DirFileCount = 1 Then
            Label3.Text = CStr(DirFileCount) & " file"
        Else
            Label3.Text = CStr(DirFileCount) & " files"
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        '
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If DirFileCount = 0 Then
            MsgBox("Cannot continue as there are zero files in the selected directory.", MsgBoxStyle.Exclamation, "Multiple File Renamer")
        Else
            If RadioButton1.Checked = True Then
                AddMode = "Before"
            ElseIf RadioButton2.Checked = True Then
                AddMode = "After"
            Else
                AddMode = "Start"
            End If
            If CheckBox1.Checked = True Then
                MatchCase = True
            Else
                MatchCase = False
            End If
            Dim Matchcnt As Integer = 0

            If AddMode = "Start" Then
                For i = 1 To DirFileCount
                    Dim Ext As String = Path.GetExtension(DirFiles.Item(i - 1))
                    Dim NewFilename As String
                    NewFilename = Path.GetFileNameWithoutExtension(DirFiles.Item(i - 1)).Insert(0, TextBox3.Text) & Ext
                    My.Computer.FileSystem.RenameFile(DirFiles.Item(i - 1), NewFilename)
                    Matchcnt = Matchcnt + 1
                Next
            End If

            If AddMode <> "Start" Then
                For i = 1 To DirFileCount
                    If MatchCase = True Then
                        If Path.GetFileNameWithoutExtension(DirFiles.Item(i - 1)).Contains(TextBox2.Text) Then
                            Dim Index As Integer = Path.GetFileNameWithoutExtension(DirFiles.Item(i - 1)).IndexOf(TextBox2.Text)
                            Dim Ext As String = Path.GetExtension(DirFiles.Item(i - 1))
                            Dim NewFilename As String
                            If AddMode = "Before" Then
                                NewFilename = Path.GetFileNameWithoutExtension(DirFiles.Item(i - 1)).Insert(Index, TextBox3.Text) & Ext
                            Else
                                NewFilename = Path.GetFileNameWithoutExtension(DirFiles.Item(i - 1)).Insert(CInt(Index + TextBox2.Text.Length), TextBox3.Text) & Ext
                            End If
                            My.Computer.FileSystem.RenameFile(DirFiles.Item(i - 1), NewFilename)
                            Matchcnt = Matchcnt + 1
                        End If
                    Else
                        If Path.GetFileNameWithoutExtension(DirFiles.Item(i - 1)).ToLower.Contains(TextBox2.Text.ToLower) Then
                            Dim Index As Integer = Path.GetFileNameWithoutExtension(DirFiles.Item(i - 1)).IndexOf(TextBox2.Text)
                            Dim Ext As String = Path.GetExtension(DirFiles.Item(i - 1))
                            Dim NewFilename As String
                            If AddMode = "Before" Then
                                NewFilename = Path.GetFileNameWithoutExtension(DirFiles.Item(i - 1)).Insert(Index, TextBox3.Text) & Ext
                            Else
                                NewFilename = Path.GetFileNameWithoutExtension(DirFiles.Item(i - 1)).Insert(CInt(Index + TextBox2.Text.Length), TextBox3.Text) & Ext
                            End If
                            My.Computer.FileSystem.RenameFile(DirFiles.Item(i - 1), NewFilename)
                            Matchcnt = Matchcnt + 1
                        End If
                    End If
                Next
            End If

            TextBox2.Text = ""
            TextBox3.Text = ""
            GetFiles()
            If Matchcnt = 0 Then
                MsgBox("No matches found in the specified directory.", MsgBoxStyle.Information, "Multiple File Renamer")
            ElseIf Matchcnt = 1 Then
                MsgBox("Done with " & Matchcnt & " instance replaced.", MsgBoxStyle.Information, "Multiple File Renamer")
            Else
                MsgBox("Done with " & Matchcnt & " instances replaced.", MsgBoxStyle.Information, "Multiple File Renamer")
            End If
            End If
    End Sub

    Sub CheckTextBoxes()
        If TextBox2.Text <> "" And TextBox3.Text <> "" Then
            Button2.Enabled = True
        Else
            Button2.Enabled = False
        End If

        If TextBox3.Text <> "" And TextBox2.Text = "" And RadioButton3.Checked = True Then
            Button2.Enabled = True
        End If

        If TextBox5.Text <> "" Then
            Button3.Enabled = True
        Else
            Button3.Enabled = False
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        CheckTextBoxes()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        CheckTextBoxes()
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        CheckTextBoxes()
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        CheckTextBoxes()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If DirFileCount = 0 Then
            MsgBox("Cannot continue as there are zero files in the selected directory.", MsgBoxStyle.Exclamation, "Multiple File Renamer")
        Else
            If CheckBox2.Checked = True Then
                MatchCase = True
            Else
                MatchCase = False
            End If
            Dim Matchcnt As Integer = 0
            For i = 1 To DirFileCount
                If MatchCase = True Then
                    If Path.GetFileNameWithoutExtension(DirFiles.Item(i - 1)).Contains(TextBox5.Text) Then
                        Dim Ext As String = Path.GetExtension(DirFiles.Item(i - 1))
                        Dim NewFilename As String = Path.GetFileNameWithoutExtension(DirFiles.Item(i - 1)).Replace(TextBox5.Text, TextBox4.Text) & Ext
                        Try
                            My.Computer.FileSystem.RenameFile(DirFiles.Item(i - 1), NewFilename)
                        Catch ex As System.IO.IOException
                            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Error")
                            Exit Sub
                        End Try
                    End If
                    Matchcnt = Matchcnt + 1
                Else
                    If Path.GetFileNameWithoutExtension(DirFiles.Item(i - 1)).ToLower.Contains(TextBox5.Text.ToLower) Then
                        Dim Ext As String = Path.GetExtension(DirFiles.Item(i - 1))
                        Dim NewFilename As String = Path.GetFileNameWithoutExtension(DirFiles.Item(i - 1)).Replace(TextBox5.Text, TextBox4.Text) & Ext
                        Try
                            My.Computer.FileSystem.RenameFile(DirFiles.Item(i - 1), NewFilename)
                        Catch ex As System.IO.IOException
                            MsgBox("Cannot complete the action since the specified filename already exists in the directory.", MsgBoxStyle.Exclamation, "Error")
                            Exit Sub
                        End Try
                    End If
                    Matchcnt = Matchcnt + 1
                End If
            Next
            TextBox5.Text = ""
            TextBox4.Text = ""
            GetFiles()
            If Matchcnt = 0 Then
                MsgBox("No matches found in the specified directory.", MsgBoxStyle.Information, "Multiple File Renamer")
            ElseIf Matchcnt = 1 Then
                MsgBox("Done with " & Matchcnt & " instance replaced.", MsgBoxStyle.Information, "Multiple File Renamer")
            Else
                MsgBox("Done with " & Matchcnt & " instances replaced.", MsgBoxStyle.Information, "Multiple File Renamer")
            End If
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            TextBox2.Enabled = False
            CheckBox1.Enabled = False
        Else
            TextBox2.Enabled = True
            CheckBox1.Enabled = True
        End If
        CheckTextBoxes()
    End Sub
End Class

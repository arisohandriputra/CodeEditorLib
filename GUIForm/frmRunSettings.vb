Imports System.IO

Public Class frmRunSettings

    Private Sub frmRunSettings_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        txtPython.Text = My.Settings.PythonPath
        txtCpp.Text = My.Settings.CppPath
        txtNode.Text = My.Settings.NodePath
        txtRuby.Text = My.Settings.RubyPath
        txtJava.Text = My.Settings.JavaPath
        ApplyLanguage()
    End Sub

    Private Sub ApplyLanguage()
        Dim L = LanguageManager.Instance
        Me.Text = L.GetString("RunSettings", "FormTitle")
        lblTitle.Text = L.GetString("RunSettings", "FormTitle")
        lblSub.Text = L.GetString("RunSettings", "SubTitle")
        grpHtml.Text = L.GetString("RunSettings", "HtmlGroupTitle")
        lblHtmlInfo.Text = L.GetString("RunSettings", "HtmlAutoInfo")
        grpPython.Text = L.GetString("RunSettings", "PythonGroupTitle")
        lblPythonHint.Text = L.GetString("RunSettings", "PythonHint")
        grpCpp.Text = L.GetString("RunSettings", "CppGroupTitle")
        lblCppHint.Text = L.GetString("RunSettings", "CppHint")
        grpNode.Text = L.GetString("RunSettings", "NodeGroupTitle")
        lblNodeHint.Text = L.GetString("RunSettings", "NodeHint")
        grpRuby.Text = L.GetString("RunSettings", "RubyGroupTitle")
        lblRubyHint.Text = L.GetString("RunSettings", "RubyHint")
        grpJava.Text = L.GetString("RunSettings", "JavaGroupTitle")
        lblJavaHint.Text = L.GetString("RunSettings", "JavaHint")
        btnBrowsePython.Text = L.GetString("RunSettings", "BrowseButton")
        btnBrowseCpp.Text = L.GetString("RunSettings", "BrowseButton")
        btnBrowseNode.Text = L.GetString("RunSettings", "BrowseButton")
        btnBrowseRuby.Text = L.GetString("RunSettings", "BrowseButton")
        btnBrowseJava.Text = L.GetString("RunSettings", "BrowseButton")
        btnDetectPython.Text = L.GetString("RunSettings", "AutoDetectButton")
        btnDetectCpp.Text = L.GetString("RunSettings", "AutoDetectButton")
        btnDetectNode.Text = L.GetString("RunSettings", "AutoDetectButton")
        btnDetectJava.Text = L.GetString("RunSettings", "AutoDetectButton")
        btnSave.Text = L.GetString("RunSettings", "SaveButton")
        btnCancel.Text = L.GetString("RunSettings", "CancelButton")
    End Sub

    Private Sub BrowseExe(ByVal txt As System.Windows.Forms.TextBox)
        Dim L = LanguageManager.Instance
        Using dlg As New OpenFileDialog()
            dlg.Filter = L.GetString("RunSettings", "BrowseExeFilter")
            dlg.Title = L.GetString("RunSettings", "BrowseExeTitle")
            If txt.Text <> "" AndAlso Directory.Exists(Path.GetDirectoryName(txt.Text)) Then
                dlg.InitialDirectory = Path.GetDirectoryName(txt.Text)
            End If
            If dlg.ShowDialog() = DialogResult.OK Then
                txt.Text = dlg.FileName
            End If
        End Using
    End Sub

    Private Sub btnBrowsePython_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBrowsePython.Click
        BrowseExe(txtPython)
    End Sub

    Private Sub btnBrowseCpp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBrowseCpp.Click
        BrowseExe(txtCpp)
    End Sub

    Private Sub btnBrowseNode_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBrowseNode.Click
        BrowseExe(txtNode)
    End Sub

    Private Sub btnBrowseRuby_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBrowseRuby.Click
        BrowseExe(txtRuby)
    End Sub

    Private Sub btnBrowseJava_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBrowseJava.Click
        BrowseExe(txtJava)
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        My.Settings.PythonPath = txtPython.Text.Trim()
        My.Settings.CppPath = txtCpp.Text.Trim()
        My.Settings.NodePath = txtNode.Text.Trim()
        My.Settings.RubyPath = txtRuby.Text.Trim()
        My.Settings.JavaPath = txtJava.Text.Trim()
        My.Settings.Save()
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnDetectPython_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDetectPython.Click
        Dim L = LanguageManager.Instance
        Dim found = DetectExe(New String() {"python", "python3", "python.exe"})
        Dim langName = "Python"
        If found <> "" Then
            txtPython.Text = found
            MsgBox(L.GetString("RunSettings", "DetectFoundMsg", found), MsgBoxStyle.Information, L.GetString("RunSettings", "DetectFoundTitle", langName))
        Else
            MsgBox(L.GetString("RunSettings", "DetectNotFoundMsg", langName), MsgBoxStyle.Exclamation, L.GetString("RunSettings", "DetectNotFoundTitle", langName))
        End If
    End Sub

    Private Sub btnDetectCpp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDetectCpp.Click
        Dim L = LanguageManager.Instance
        Dim found = DetectExe(New String() {"g++", "g++.exe", "gcc", "cl"})
        Dim langName = "C++ Compiler"
        If found <> "" Then
            txtCpp.Text = found
            MsgBox(L.GetString("RunSettings", "DetectFoundMsg", found), MsgBoxStyle.Information, L.GetString("RunSettings", "DetectFoundTitle", langName))
        Else
            MsgBox(L.GetString("RunSettings", "DetectNotFoundMsg", langName), MsgBoxStyle.Exclamation, L.GetString("RunSettings", "DetectNotFoundTitle", langName))
        End If
    End Sub

    Private Sub btnDetectNode_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDetectNode.Click
        Dim L = LanguageManager.Instance
        Dim found = DetectExe(New String() {"node", "node.exe"})
        Dim langName = "Node.js"
        If found <> "" Then
            txtNode.Text = found
            MsgBox(L.GetString("RunSettings", "DetectFoundMsg", found), MsgBoxStyle.Information, L.GetString("RunSettings", "DetectFoundTitle", langName))
        Else
            MsgBox(L.GetString("RunSettings", "DetectNotFoundMsg", langName), MsgBoxStyle.Exclamation, L.GetString("RunSettings", "DetectNotFoundTitle", langName))
        End If
    End Sub

    Private Sub btnDetectJava_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDetectJava.Click
        Dim L = LanguageManager.Instance
        Dim found = DetectExe(New String() {"java", "java.exe"})
        Dim langName = "Java"
        If found <> "" Then
            txtJava.Text = found
            MsgBox(L.GetString("RunSettings", "DetectFoundMsg", found), MsgBoxStyle.Information, L.GetString("RunSettings", "DetectFoundTitle", langName))
        Else
            MsgBox(L.GetString("RunSettings", "DetectNotFoundMsg", langName), MsgBoxStyle.Exclamation, L.GetString("RunSettings", "DetectNotFoundTitle", langName))
        End If
    End Sub

    Private Function DetectExe(ByVal names As String()) As String
        For Each name As String In names
            Try
                Dim psi As New System.Diagnostics.ProcessStartInfo(name, "--version")
                psi.UseShellExecute = False
                psi.RedirectStandardOutput = True
                psi.RedirectStandardError = True
                psi.CreateNoWindow = True
                Dim p = System.Diagnostics.Process.Start(psi)
                p.WaitForExit(3000)
                Dim wherePsi As New System.Diagnostics.ProcessStartInfo("where", name)
                wherePsi.UseShellExecute = False
                wherePsi.RedirectStandardOutput = True
                wherePsi.CreateNoWindow = True
                Dim whereP = System.Diagnostics.Process.Start(wherePsi)
                Dim result = whereP.StandardOutput.ReadLine()
                whereP.WaitForExit(2000)
                If result IsNot Nothing AndAlso result.Trim() <> "" Then
                    Return result.Trim()
                End If
                Return name
            Catch

            End Try
        Next
        Return ""
    End Function

End Class

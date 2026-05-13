Imports System.IO
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Threading

Public Class LanguageManager

    Private Shared _instance As LanguageManager
    Public Shared ReadOnly Property Instance As LanguageManager
        Get
            If _instance Is Nothing Then _instance = New LanguageManager()
            Return _instance
        End Get
    End Property

  
    Private _strings As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
    Private _currentCode As String = "en"

    Public ReadOnly Property LanguagesFolder As String
        Get
            Return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Languages")
        End Get
    End Property

    Public ReadOnly Property CurrentCode As String
        Get
            Return _currentCode
        End Get
    End Property

    Private Sub New()

        Dim lang As String = My.Settings.lang

        If String.IsNullOrEmpty(lang) Then
            lang = "en"
        End If

        If Not Load(lang) Then
            Load("en")
        End If

    End Sub

    Public Function Load(ByVal code As String) As Boolean
        Dim filePath As String = Path.Combine(LanguagesFolder, code.ToLower() & ".ini")
        If Not File.Exists(filePath) Then Return False

        _strings.Clear()
        _currentCode = code.ToLower()

        Dim currentSection As String = ""
        For Each line As String In File.ReadAllLines(filePath, System.Text.Encoding.UTF8)
            Dim trimmed = line.Trim()
            If trimmed.Length = 0 OrElse trimmed.StartsWith(";") OrElse trimmed.StartsWith("#") Then Continue For

            If trimmed.StartsWith("[") AndAlso trimmed.EndsWith("]") Then
                currentSection = trimmed.Substring(1, trimmed.Length - 2).Trim()
                Continue For
            End If

            Dim eqIndex = trimmed.IndexOf("="c)
            If eqIndex > 0 Then
                Dim key = currentSection & "." & trimmed.Substring(0, eqIndex).Trim()
                Dim value = trimmed.Substring(eqIndex + 1)

                _strings(key) = value
            End If
        Next
        Return True
    End Function

  
    Public Function GetString(ByVal section As String, ByVal key As String) As String
        Dim fullKey = section & "." & key
        Dim value As String = Nothing
        If _strings.TryGetValue(fullKey, value) Then Return value
        Return "[" & fullKey & "]"
    End Function

    Public Function GetString(ByVal section As String, ByVal key As String,
                              ByVal ParamArray args() As Object) As String
        Dim raw = GetString(section, key)
        Try
            Return String.Format(raw, args)
        Catch
            Return raw
        End Try
    End Function

    Public Function Menu(ByVal key As String) As String
        Return GetString("Menu", key)
    End Function
    Public Function Toolbar(ByVal key As String) As String
        Return GetString("Toolbar", key)
    End Function
    Public Function FindPanel(ByVal key As String) As String
        Return GetString("FindPanel", key)
    End Function
    Public Function StatusBar(ByVal key As String) As String
        Return GetString("StatusBar", key)
    End Function
    Public Function Tab(ByVal key As String) As String
        Return GetString("Tab", key)
    End Function
    Public Function Dlg(ByVal key As String, ByVal ParamArray args() As Object) As String
        Return GetString("Dialog", key, args)
    End Function
    Public Function Status(ByVal key As String, ByVal ParamArray args() As Object) As String
        Return GetString("Status", key, args)
    End Function
    Public Function FileFilter(ByVal key As String) As String
        Return GetString("FileFilter", key)
    End Function
    Public Function KbShortcut(ByVal key As String) As String
        Return GetString("KeyboardShortcuts", key)
    End Function
    Public Function LangMenu(ByVal key As String) As String
        Return GetString("LanguageMenu", key)
    End Function
    Public Function About(ByVal key As String, ByVal ParamArray args() As Object) As String
        Return GetString("About", key, args)
    End Function

    Public Function GetAvailableLanguages() As List(Of LanguageInfo)
        Dim result As New List(Of LanguageInfo)
        If Not Directory.Exists(LanguagesFolder) Then Return result

        For Each filePath As String In Directory.GetFiles(LanguagesFolder, "*.ini")
            Dim code = Path.GetFileNameWithoutExtension(filePath).ToLower()
            Dim name = ReadMetaValue(filePath, "LanguageName")
            If name = "" Then name = code.ToUpper()
            result.Add(New LanguageInfo(code, name))
        Next
        result.Sort(New LanguageInfoComparer())
        Return result
    End Function

    Private Function ReadMetaValue(ByVal filePath As String, ByVal metaKey As String) As String
        Dim inMeta As Boolean = False
        For Each line As String In File.ReadAllLines(filePath, System.Text.Encoding.UTF8)
            Dim t = line.Trim()
            If t = "[Meta]" Then inMeta = True : Continue For
            If t.StartsWith("[") Then inMeta = False : Continue For
            If Not inMeta Then Continue For

            Dim eq = t.IndexOf("="c)
            If eq > 0 AndAlso t.Substring(0, eq).Trim().Equals(metaKey, StringComparison.OrdinalIgnoreCase) Then
                Return t.Substring(eq + 1).Trim()
            End If
        Next
        Return ""
    End Function

    Public Sub BuildLanguageMenu(ByVal parentItem As ToolStripMenuItem,
                                 ByVal onSelected As Action(Of String))
        parentItem.Text = LangMenu("MenuTitle")
        parentItem.DropDownItems.Clear()

        For Each lang In GetAvailableLanguages()
            Dim langCopy = lang
            Dim item As New ToolStripMenuItem(lang.DisplayName) With {
                .Tag = lang.Code,
                .Checked = (lang.Code = _currentCode)
            }
            AddHandler item.Click, Sub(s, e)

                                       If langCopy.Code.ToLower() = _currentCode.ToLower() Then
                                           Return
                                       End If

                                       My.Settings.lang = langCopy.Code.ToLower()
                                       My.Settings.Save()

                                       Thread.Sleep(300)

                                       Process.Start(Application.ExecutablePath)

                                       Application.Exit()

                                   End Sub
            parentItem.DropDownItems.Add(item)


        Next
    End Sub

    Public Sub SyncLanguageMenuChecks(ByVal parentItem As ToolStripMenuItem)
        For Each item As ToolStripItem In parentItem.DropDownItems
            If TypeOf item Is ToolStripMenuItem Then
                Dim mi = CType(item, ToolStripMenuItem)
                mi.Checked = (TryCast(mi.Tag, String) = _currentCode)
            End If
        Next
    End Sub

    Public Function BuildFileFilter() As String
        Return FileFilter("AllSupported") & "|" &
               FileFilter("CSharpFiles") & "|" &
               FileFilter("VBFiles") & "|" &
               FileFilter("JavaScriptFiles") & "|" &
               FileFilter("PythonFiles") & "|" &
               FileFilter("HTMLFiles") & "|" &
               FileFilter("CppFiles") & "|" &
               FileFilter("GoFiles") & "|" &
               FileFilter("JavaFiles") & "|" &
               FileFilter("RustFiles") & "|" &
               FileFilter("SQLFiles") & "|" &
               FileFilter("PHPFiles") & "|" &
               FileFilter("CSSFiles") & "|" &
               FileFilter("XMLFiles") & "|" &
               FileFilter("JSONFiles") & "|" &
               FileFilter("TextFiles") & "|" &
               FileFilter("AllFiles")
    End Function

End Class

Public Class LanguageInfo
    Private _code As String
    Private _displayName As String

    Public ReadOnly Property Code() As String
        Get
            Return _code
        End Get
    End Property

    Public ReadOnly Property DisplayName() As String
        Get
            Return _displayName
        End Get
    End Property

    Public Sub New(ByVal code As String, ByVal displayName As String)
        _code = code
        _displayName = displayName
    End Sub
End Class

Public Class LanguageInfoComparer
    Implements IComparer(Of LanguageInfo)

    Public Function Compare(ByVal x As LanguageInfo, ByVal y As LanguageInfo) As Integer _
        Implements IComparer(Of LanguageInfo).Compare
        If x.Code = "en" AndAlso y.Code <> "en" Then Return -1
        If y.Code = "en" AndAlso x.Code <> "en" Then Return 1
        Return String.Compare(x.DisplayName, y.DisplayName, StringComparison.OrdinalIgnoreCase)
    End Function
End Class

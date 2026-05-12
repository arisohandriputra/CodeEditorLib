Imports System.IO
Imports System.Text
Imports CodeEditorLib.Controls
Imports CodeEditorLib.Languages
Imports CodeEditorLib.Themes

Public Class Form1

    ' ─────────────────────────────────────────────
    '  Data structures
    ' ─────────────────────────────────────────────
    Private Structure EditorTab
        Dim FilePath As String
        Dim IsModified As Boolean
        Dim Editor As CodeEditorControl
        Dim TabButton As TabButtonControl
        Dim UntitledIndex As Integer
        Dim SavedCode As String  ' Snapshot konten saat terakhir disimpan/dibuka
    End Structure

    Private _tabs As New List(Of EditorTab)
    Private _activeIndex As Integer = -1
    Private _untitledCounter As Integer = 0
    Private _isDark As Boolean = True
    Private _recentFiles As New List(Of String)
    Private _isLoadingFile As Boolean = False  ' Flag: sedang load file, abaikan TextChanged

    ' ─────────────────────────────────────────────
    '  Custom tab button control
    ' ─────────────────────────────────────────────
    Public Class TabButtonControl
        Inherits Control

        Public Property TabTitle As String = "Untitled"
        Public Property IsActive As Boolean = False
        Public Property IsModified As Boolean = False
        Public Event CloseClicked As EventHandler

        Private _closeHovered As Boolean = False
        Private _closeRect As New Rectangle(0, 0, 16, 16)

        Public Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or
                     ControlStyles.UserPaint Or
                     ControlStyles.OptimizedDoubleBuffer, True)
            Height = 32
            Width = 160
            Cursor = Cursors.Hand
            Font = New Font("Segoe UI", 9, FontStyle.Regular)
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            Dim g = e.Graphics
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

            Dim bg As Color
            Dim fg As Color
            Dim accentColor As Color = Color.FromArgb(0, 120, 212)

            If IsActive Then
                bg = Color.FromArgb(255, 255, 255)
                fg = Color.FromArgb(30, 30, 30)
            Else
                bg = Color.FromArgb(213, 213, 213)
                fg = Color.FromArgb(90, 90, 90)
            End If

            Using br = New SolidBrush(bg)
                g.FillRectangle(br, 0, 0, Width, Height)
            End Using

            If IsActive Then
                Using accentBr = New SolidBrush(accentColor)
                    g.FillRectangle(accentBr, 0, Height - 2, Width, 2)
                End Using
            End If

            Using pen = New Pen(Color.FromArgb(60, 60, 60), 1)
                g.DrawLine(pen, Width - 1, 4, Width - 1, Height - 4)
            End Using

            Dim xOffset As Integer = 8
            Dim displayTitle As String = TabTitle
            If IsModified Then
                displayTitle = "* " & TabTitle
            End If

            Dim cx = Width - 20
            Dim cy = (Height - 16) \ 2
            _closeRect = New Rectangle(cx, cy, 16, 16)

            Dim maxTextWidth = Width - xOffset - 26
            Using fgBr = New SolidBrush(fg)
                Dim sf = New StringFormat()
                sf.LineAlignment = StringAlignment.Center
                sf.Trimming = StringTrimming.EllipsisCharacter
                g.DrawString(displayTitle, Font, fgBr,
                             New RectangleF(xOffset, 0, maxTextWidth, Height), sf)
            End Using

            If _closeHovered Then
                Using closeBg = New SolidBrush(Color.FromArgb(200, 50, 50))
                    g.FillRectangle(closeBg, cx, cy, 16, 16)
                End Using
                Using closeFg = New SolidBrush(Color.White)
                    Dim sf As New StringFormat()
                    sf.Alignment = StringAlignment.Center
                    sf.LineAlignment = StringAlignment.Center
                    g.DrawString("x", New Font("Segoe UI", 8, FontStyle.Bold), closeFg,
                                 New RectangleF(cx, cy, 16, 16), sf)
                End Using
            Else
                Using closeFg = New SolidBrush(If(IsActive, Color.FromArgb(180, 180, 180), Color.FromArgb(100, 100, 100)))
                    Dim sf As New StringFormat()
                    sf.Alignment = StringAlignment.Center
                    sf.LineAlignment = StringAlignment.Center
                    g.DrawString("x", New Font("Segoe UI", 8), closeFg,
                                 New RectangleF(cx, cy, 16, 16), sf)
                End Using
            End If
        End Sub

        Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
            Dim was = _closeHovered
            _closeHovered = _closeRect.Contains(e.Location)
            If was <> _closeHovered Then Invalidate()
        End Sub

        Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
            _closeHovered = False
            Invalidate()
        End Sub

        Protected Overrides Sub OnMouseClick(ByVal e As MouseEventArgs)
            If _closeRect.Contains(e.Location) Then
                RaiseEvent CloseClicked(Me, EventArgs.Empty)
            End If
        End Sub
    End Class

    ' ─────────────────────────────────────────────
    '  Init
    ' ─────────────────────────────────────────────
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.favicon
        ' ── Localisation: build language menu & apply default (English) ──
        LanguageManager.Instance.BuildLanguageMenu(UILanguageToolStripMenuItem,
            Sub(code As String)
                ApplyLanguage()
                LanguageManager.Instance.SyncLanguageMenuChecks(UILanguageToolStripMenuItem)
            End Sub)
        ApplyLanguage()

        ApplyTheme()
        LoadRecentFiles()
        NewTab()
        HandleCommandLineArgs()   ' Buka file dari double-klik Explorer
        Me.AllowDrop = True
        TabsPanel.AllowDrop = True
        EditorHostPanel.AllowDrop = True
        ShowLineNumbersToolStripMenuItem.Checked = True
        LightToolStripMenuItem.Checked = False
        DarkToolStripMenuItem.Checked = True
        AddHandler Me.Resize, AddressOf Form1_Resize
        LayoutPanels()
    End Sub

    ' ─────────────────────────────────────────────
    '  LOCALISATION
    ' ─────────────────────────────────────────────
    Private Sub ApplyLanguage()
        Dim L = LanguageManager.Instance

        ' ── Menu: File ──
        FileToolStripMenuItem.Text = L.Menu("File")
        NewFileToolStripMenuItem.Text = L.Menu("New")
        OpenFileToolStripMenuItem.Text = L.Menu("OpenFile")
        OpenRecentToolStripMenuItem.Text = L.Menu("OpenRecent")
        SaveToolStripMenuItem.Text = L.Menu("Save")
        SaveAsToolStripMenuItem.Text = L.Menu("SaveAs")
        SaveAllToolStripMenuItem.Text = L.Menu("SaveAll")
        CloseTabToolStripMenuItem.Text = L.Menu("CloseTab")
        CloseAllToolStripMenuItem.Text = L.Menu("CloseAllTabs")
        ExitToolStripMenuItem.Text = L.Menu("Exit")

        ' ── Menu: Edit ──
        EditToolStripMenuItem.Text = L.Menu("Edit")
        UndoToolStripMenuItem.Text = L.Menu("Undo")
        RedoToolStripMenuItem.Text = L.Menu("Redo")
        CutToolStripMenuItem.Text = L.Menu("Cut")
        CopyToolStripMenuItem.Text = L.Menu("Copy")
        PasteToolStripMenuItem.Text = L.Menu("Paste")
        SelectAllToolStripMenuItem.Text = L.Menu("SelectAll")
        FindToolStripMenuItem.Text = L.Menu("FindReplace")
        GoToLineToolStripMenuItem.Text = L.Menu("GoToLine")
        ToggleCommentToolStripMenuItem.Text = L.Menu("ToggleComment")

        ' ── Menu: View ──
        ViewToolStripMenuItem.Text = L.Menu("View")
        WordWrapToolStripMenuItem.Text = L.Menu("WordWrap")
        ShowLineNumbersToolStripMenuItem.Text = L.Menu("ShowLineNumbers")

        ' ── Menu: Language (code language, not UI language) ──
        LanguageToolStripMenuItem.Text = L.Menu("Language")
        PlainTextToolStripMenuItem.Text = L.Menu("PlainText")

        ' ── Menu: Themes ──
        ThemesToolStripMenuItem.Text = L.Menu("Themes")
        DarkToolStripMenuItem.Text = L.Menu("Dark")
        LightToolStripMenuItem.Text = L.Menu("Light")

        ' ── Menu: Help ──
        HelpToolStripMenuItem.Text = L.Menu("Help")
        KeyboardShortcutsToolStripMenuItem.Text = L.Menu("KeyboardShortcuts")
        AboutToolStripMenuItem.Text = L.Menu("About")

        ' ── Toolbar ──
        bNew.Text = L.Toolbar("New")
        bOpen.Text = L.Toolbar("Open")
        bSave.Text = L.Toolbar("Save")
        bFind.Text = L.Toolbar("Find")

        ' ── Find panel ──
        lblFindFind.Text = L.FindPanel("FindLabel")
        lblFindReplace.Text = L.FindPanel("ReplaceLabel")
        btnFindPrev.Text = L.FindPanel("PrevButton")
        btnFindNext.Text = L.FindPanel("NextButton")
        btnReplace.Text = L.FindPanel("ReplaceButton")
        btnReplaceAll.Text = L.FindPanel("ReplaceAllButton")
        chkCaseSensitive.Text = L.FindPanel("CaseButton")
        btnCloseFindPanel.Text = L.FindPanel("CloseButton")

        ' ── Status bar (static labels) ──
        lblEncoding.Text = L.StatusBar("Encoding")
        lblCRLF.Text = L.StatusBar("LineEnding")
        ToolStripStatusLabel1.Text = L.StatusBar("ZoomHint")

        ' Rebuild Recent menu & re-refresh caret/language status
        LoadRecentFiles()
        UpdateStatusBar()
    End Sub

    ' ─────────────────────────────────────────────
    '  TAB MANAGEMENT
    ' ─────────────────────────────────────────────
    Private Sub NewTab(Optional ByVal filePath As String = "", Optional ByVal content As String = "")
        _untitledCounter += 1

        Dim editor As New CodeEditorControl()
        editor.Dock = DockStyle.Fill
        editor.AutoBrackets = True
        editor.AutoIndent = True
        editor.ShowLineNumbers = True
        editor.HighlightCurrentLine = True
        editor.TabSize = 4
        _isLoadingFile = True
        editor.Code = content
        _isLoadingFile = False
        editor.Theme = If(_isDark, CType(New DarkTheme(), Object), New LightTheme())
        editor.AllowDrop = True
        AddHandler editor.DragEnter, AddressOf Editor_DragEnter
        AddHandler editor.DragDrop, AddressOf Editor_DragDrop
        AddHandler editor.CaretPositionChanged, AddressOf Editor_CaretPositionChanged
        AddHandler editor.TextChanged, AddressOf Editor_TextChanged

        Dim tabBtn As New TabButtonControl()
        tabBtn.Height = 32
        tabBtn.Width = 160
        tabBtn.Dock = DockStyle.None
        AddHandler tabBtn.Click, AddressOf TabButton_Click
        AddHandler tabBtn.CloseClicked, AddressOf TabButton_Close
        TabsPanel.Controls.Add(tabBtn)
        Dim t As New EditorTab()
        t.FilePath = filePath
        t.IsModified = False
        t.Editor = editor
        t.TabButton = tabBtn
        t.UntitledIndex = _untitledCounter

        If filePath <> "" AndAlso File.Exists(filePath) Then
            editor.Language = GetLanguageFromExtension(Path.GetExtension(filePath).ToLower())
        End If

        _tabs.Add(t)
        ' Simpan snapshot dari editor.Code SETELAH di-set (bukan dari content mentah)
        ' karena editor mungkin normalisasi line ending, sehingga perbandingan tetap akurat
        Dim newIdx = _tabs.Count - 1
        Dim tSnap = _tabs(newIdx)
        tSnap.SavedCode = editor.Code  ' ambil dari editor, bukan dari content
        _tabs(newIdx) = tSnap
        SwitchToTab(newIdx)
        RepositionTabs()
        UpdateTabTitles()
    End Sub

    Private _tabScrollOffset As Integer = 0

    Private Sub RepositionTabs()
        Dim totalWidth As Integer = _tabs.Count * 160
        Dim panelWidth As Integer = TabsPanel.Width
        Dim needsScroll As Boolean = totalWidth > panelWidth

        ' Clamp scroll offset
        If needsScroll Then
            _tabScrollOffset = Math.Max(0, Math.Min(_tabScrollOffset, totalWidth - panelWidth))
        Else
            _tabScrollOffset = 0
        End If

        ' Position each tab button
        Dim x As Integer = -_tabScrollOffset
        For i = 0 To _tabs.Count - 1
            _tabs(i).TabButton.Location = New Point(x, 0)
            _tabs(i).TabButton.Height = 32
            x += _tabs(i).TabButton.Width
        Next

        ' Show/hide scroll buttons
        btnScrollLeft.Visible = needsScroll
        btnScrollRight.Visible = needsScroll
        btnScrollLeft.Enabled = (_tabScrollOffset > 0)
        btnScrollRight.Enabled = (_tabScrollOffset < totalWidth - panelWidth)
    End Sub

    Private Sub SwitchToTab(ByVal index As Integer)
        If index < 0 OrElse index >= _tabs.Count Then Return

        If _activeIndex >= 0 AndAlso _activeIndex < _tabs.Count Then
            _tabs(_activeIndex).Editor.Visible = False
            _tabs(_activeIndex).TabButton.IsActive = False
            _tabs(_activeIndex).TabButton.Invalidate()
        End If

        _activeIndex = index
        Dim t = _tabs(index)

        If Not EditorHostPanel.Controls.Contains(t.Editor) Then
            EditorHostPanel.Controls.Add(t.Editor)
        End If
        t.Editor.Visible = True
        t.Editor.BringToFront()
        t.TabButton.IsActive = True
        t.TabButton.Invalidate()

        ' Scroll agar tab aktif terlihat
        Dim tabLeft = index * 160
        Dim tabRight = tabLeft + 160
        If tabLeft < _tabScrollOffset Then
            _tabScrollOffset = tabLeft
            RepositionTabs()
        ElseIf tabRight > _tabScrollOffset + TabsPanel.Width Then
            _tabScrollOffset = tabRight - TabsPanel.Width
            RepositionTabs()
        End If

        UpdateStatusBar()
        UpdateTitleBar()
        UpdateTabTitles()
    End Sub

    Private Sub CloseTab(ByVal index As Integer)
        If index < 0 OrElse index >= _tabs.Count Then Return
        Dim t = _tabs(index)

        If t.IsModified Then
            Dim fn = GetTabDisplayName(t)
            Dim result As DialogResult = MessageBox.Show(
                LanguageManager.Instance.Dlg("UnsavedChanges", fn, vbCrLf),
                LanguageManager.Instance.Dlg("UnsavedChangesTitle"),
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning)
            If result = DialogResult.Yes Then
                If Not SaveTab(index) Then Return
            ElseIf result = DialogResult.Cancel Then
                Return
            End If
            ' No = tutup tanpa simpan
        End If

        TabsPanel.Controls.Remove(t.TabButton)
        EditorHostPanel.Controls.Remove(t.Editor)
        t.Editor.Dispose()
        _tabs.RemoveAt(index)

        If _tabs.Count = 0 Then
            ' Satu-satunya tab ditutup → tutup program
            Me.Close()
        Else
            _activeIndex = Math.Max(0, Math.Min(index, _tabs.Count - 1))
            SwitchToTab(_activeIndex)
            RepositionTabs()
        End If
    End Sub

    Private Sub TabButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn = CType(sender, TabButtonControl)
        For i = 0 To _tabs.Count - 1
            If _tabs(i).TabButton Is btn Then
                SwitchToTab(i)
                Return
            End If
        Next
    End Sub

    Private Sub TabButton_Close(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn = CType(sender, TabButtonControl)
        For i = 0 To _tabs.Count - 1
            If _tabs(i).TabButton Is btn Then
                CloseTab(i)
                Return
            End If
        Next
    End Sub

    Private Sub UpdateTabTitles()
        For i = 0 To _tabs.Count - 1
            Dim t = _tabs(i)
            t.TabButton.TabTitle = GetTabDisplayName(t)
            t.TabButton.IsModified = t.IsModified
            t.TabButton.IsActive = (i = _activeIndex)
            t.TabButton.Invalidate()
        Next
    End Sub

    Private Function GetTabDisplayName(ByVal t As EditorTab) As String
        If t.FilePath = "" Then
            Return LanguageManager.Instance.Tab("Untitled") & t.UntitledIndex
        Else
            Return Path.GetFileName(t.FilePath)
        End If
    End Function

    ' ─────────────────────────────────────────────
    '  EDITOR EVENTS
    ' ─────────────────────────────────────────────
    Private Sub Editor_CaretPositionChanged(ByVal sender As Object, ByVal e As CodeEditorLib.Events.CaretPositionEventArgs)
        UpdateStatusBar()
    End Sub

    Private Sub Editor_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        ' Abaikan TextChanged saat sedang memuat file agar IsModified tidak salah set
        If _isLoadingFile Then Return

        ' Cari tab yang sesuai dengan editor yang mengirim event (bukan pakai _activeIndex!)
        For i As Integer = 0 To _tabs.Count - 1
            If _tabs(i).Editor Is sender Then
                If Not _tabs(i).IsModified Then
                    Dim t = _tabs(i)
                    t.IsModified = True
                    _tabs(i) = t
                    ' Tandai tab button dengan *
                    t.TabButton.IsModified = True
                    t.TabButton.Invalidate()
                    ' Update title bar hanya jika ini tab yang aktif
                    If i = _activeIndex Then UpdateTitleBar()
                End If
                Return
            End If
        Next
    End Sub

    ' ─────────────────────────────────────────────
    '  TIMER — polling deteksi perubahan konten editor
    '  Sebagai fallback jika TextChanged tidak di-raise oleh CodeEditorControl
    ' ─────────────────────────────────────────────
    Private Sub tmrModifiedCheck_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrModifiedCheck.Tick
        If _isLoadingFile Then Return
        For i As Integer = 0 To _tabs.Count - 1
            Dim t = _tabs(i)
            If t.IsModified Then Continue For ' sudah ditandai, skip

            Dim currentCode As String = ""
            Try
                currentCode = t.Editor.Code
            Catch
                Continue For
            End Try

            If currentCode <> t.SavedCode Then
                ' Ada perubahan dari snapshot terakhir — tandai modified
                t.IsModified = True
                _tabs(i) = t
                t.TabButton.IsModified = True
                t.TabButton.Invalidate()
                If i = _activeIndex Then UpdateTitleBar()
            End If
        Next
    End Sub

    ' ─────────────────────────────────────────────
    '  DRAG & DROP
    ' ─────────────────────────────────────────────
    Private Sub Form1_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles MyBase.DragEnter,
                                                                                TabsPanel.DragEnter,
                                                                                EditorHostPanel.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub Form1_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles MyBase.DragDrop,
                                                                              TabsPanel.DragDrop,
                                                                              EditorHostPanel.DragDrop
        For Each f As String In CType(e.Data.GetData(DataFormats.FileDrop), String())
            OpenFileInTab(f)
        Next
    End Sub

    Private Sub Editor_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs)
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub Editor_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs)
        For Each f As String In CType(e.Data.GetData(DataFormats.FileDrop), String())
            OpenFileInTab(f)
        Next
    End Sub

    ' ─────────────────────────────────────────────
    '  FILE OPERATIONS
    ' ─────────────────────────────────────────────
    Private Sub OpenFileInTab(ByVal filePath As String)
        For i = 0 To _tabs.Count - 1
            If String.Compare(_tabs(i).FilePath, filePath, True) = 0 Then
                SwitchToTab(i)
                Return
            End If
        Next

        Dim ext = Path.GetExtension(filePath).ToLower()
        Dim supported = {".cs", ".vb", ".vbs", ".js", ".ts", ".py", ".html", ".htm",
                         ".cpp", ".c", ".h", ".java", ".sql", ".php", ".css",
                         ".xml", ".json", ".txt", ".md", ".bat", ".sh", ".yaml",
                         ".yml", ".rb", ".go", ".rs", ".swift", ".kt"}

        If Array.IndexOf(supported, ext) < 0 AndAlso ext <> "" Then
            Dim ans As DialogResult = MessageBox.Show(LanguageManager.Instance.Dlg("UnsupportedFile", ext),
                                      LanguageManager.Instance.Dlg("UnsupportedFileTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If ans <> DialogResult.Yes Then Return
        End If

        Try
            Dim content = File.ReadAllText(filePath, DetectEncoding(filePath))
            AddRecentFile(filePath)

            _isLoadingFile = True  ' Mulai load — tandai agar TextChanged tidak trigger IsModified
            Try
                If _activeIndex >= 0 Then
                    Dim cur = _tabs(_activeIndex)
                    If cur.FilePath = "" AndAlso cur.Editor.Code = "" AndAlso Not cur.IsModified Then
                        cur.FilePath = filePath
                        cur.Editor.Code = content
                        cur.Editor.Language = GetLanguageFromExtension(ext)
                        cur.IsModified = False
                        cur.SavedCode = cur.Editor.Code  ' ambil dari editor setelah di-set
                        _tabs(_activeIndex) = cur
                        UpdateTabTitles()
                        UpdateTitleBar()
                        Return
                    End If
                End If

                NewTab(filePath, content)
            Finally
                _isLoadingFile = False  ' Selesai load — kembalikan flag
            End Try
        Catch ex As Exception
            _isLoadingFile = False
            MessageBox.Show(LanguageManager.Instance.Dlg("CouldNotOpen", vbCrLf, ex.Message),
                            LanguageManager.Instance.Dlg("CouldNotOpenTitle"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function SaveTab(ByVal index As Integer) As Boolean
        If index < 0 OrElse index >= _tabs.Count Then Return False
        Dim t = _tabs(index)
        If t.FilePath = "" Then Return SaveTabAs(index)
        Try
            File.WriteAllText(t.FilePath, t.Editor.Code, New UTF8Encoding(False))
            t.IsModified = False
            t.SavedCode = t.Editor.Code  ' Update snapshot setelah simpan
            _tabs(index) = t
            UpdateTabTitles()
            UpdateTitleBar()
            Return True
        Catch ex As Exception
            MessageBox.Show(LanguageManager.Instance.Dlg("SaveFailed", vbCrLf, ex.Message),
                            LanguageManager.Instance.Dlg("SaveFailedTitle"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Function SaveTabAs(ByVal index As Integer) As Boolean
        If index < 0 OrElse index >= _tabs.Count Then Return False
        Dim t = _tabs(index)
        Using dlg As New SaveFileDialog()
            dlg.Title = LanguageManager.Instance.Dlg("SaveAsTitle")
            dlg.Filter = GetFileFilter()
            dlg.FileName = GetTabDisplayName(t)
            If t.FilePath <> "" Then dlg.InitialDirectory = Path.GetDirectoryName(t.FilePath)
            If dlg.ShowDialog() = DialogResult.OK Then
                t.FilePath = dlg.FileName
                _tabs(index) = t
                Try
                    File.WriteAllText(t.FilePath, t.Editor.Code, New UTF8Encoding(False))
                    t.IsModified = False
                    t.SavedCode = t.Editor.Code  ' Update snapshot setelah simpan
                    _tabs(index) = t
                    AddRecentFile(t.FilePath)
                    UpdateTabTitles()
                    UpdateTitleBar()
                    Return True
                Catch ex As Exception
                    MessageBox.Show(LanguageManager.Instance.Dlg("SaveFailed", vbCrLf, ex.Message),
                                    LanguageManager.Instance.Dlg("SaveFailedTitle"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
        Return False
    End Function

    Private Function DetectEncoding(ByVal path As String) As Encoding
        Try
            Dim bom(3) As Byte
            Using fs = New FileStream(path, FileMode.Open, FileAccess.Read)
                fs.Read(bom, 0, 4)
            End Using
            If bom(0) = &HEF AndAlso bom(1) = &HBB AndAlso bom(2) = &HBF Then Return New UTF8Encoding(True)
            If bom(0) = &HFF AndAlso bom(1) = &HFE Then Return Encoding.Unicode
            If bom(0) = &HFE AndAlso bom(1) = &HFF Then Return Encoding.BigEndianUnicode
        Catch
        End Try
        Return New UTF8Encoding(False)
    End Function

    Private Function GetFileFilter() As String
        Return LanguageManager.Instance.BuildFileFilter()
    End Function

    Private Function GetLanguageFromExtension(ByVal ext As String) As Object
        Select Case ext.ToLower()
            Case ".cpp", ".c", ".h", ".cc" : Return New CppLanguage()
            Case ".cs" : Return New CSharpLanguage()
            Case ".css" : Return New CssLanguage()
            Case ".go" : Return New GoLanguage()
            Case ".html", ".htm" : Return New HtmlLanguage()
            Case ".java" : Return New JavaLanguage()
            Case ".js", ".ts" : Return New JavaScriptLanguage()
            Case ".php" : Return New PhpLanguage()
            Case ".py" : Return New PythonLanguage()
            Case ".rs" : Return New RustLanguage()
            Case ".sql" : Return New SqlLanguage()
            Case ".vb", ".vbs" : Return New VbLanguage()
            Case ".xml", ".xaml", ".config" : Return New XmlLanguage()
            Case Else : Return Nothing
        End Select
    End Function

    ' ─────────────────────────────────────────────
    '  THEME
    ' ─────────────────────────────────────────────
    Private Sub SetMenuColors(ByVal items As ToolStripItemCollection, ByVal fg As Color)
        For Each item As ToolStripItem In items
            item.ForeColor = fg
            If TypeOf item Is ToolStripMenuItem Then
                SetMenuColors(CType(item, ToolStripMenuItem).DropDownItems, fg)
            End If
        Next
    End Sub

    Private Sub ApplyTheme()
        If _isDark Then
            Me.BackColor = Color.FromArgb(30, 30, 30)
            TabsPanel.BackColor = Color.FromArgb(37, 37, 38)
            EditorHostPanel.BackColor = Color.FromArgb(30, 30, 30)
            MenuStrip1.BackColor = Color.FromArgb(45, 45, 48)
            MenuStrip1.ForeColor = Color.FromArgb(220, 220, 220)
            MenuStrip1.Renderer = New DarkMenuRenderer()
            SetMenuColors(MenuStrip1.Items, Color.FromArgb(220, 220, 220))
            StatusStrip1.BackColor = Color.FromArgb(0, 122, 204)
            StatusStrip1.ForeColor = Color.White
            ToolBarPanel.BackColor = Color.FromArgb(37, 37, 38)
            btnScrollLeft.BackColor = Color.FromArgb(50, 50, 52)
            btnScrollLeft.ForeColor = Color.FromArgb(200, 200, 200)
            btnScrollRight.BackColor = Color.FromArgb(50, 50, 52)
            btnScrollRight.ForeColor = Color.FromArgb(200, 200, 200)
            bNew.ForeColor = Color.FromArgb(200, 200, 200)
            bOpen.ForeColor = Color.FromArgb(200, 200, 200)
            bSave.ForeColor = Color.FromArgb(200, 200, 200)
            bFind.ForeColor = Color.FromArgb(200, 200, 200)
        Else
            Me.BackColor = Color.FromArgb(240, 240, 240)
            TabsPanel.BackColor = Color.FromArgb(213, 213, 213)
            EditorHostPanel.BackColor = Color.White
            MenuStrip1.BackColor = Color.FromArgb(240, 240, 240)
            MenuStrip1.ForeColor = Color.Black
            MenuStrip1.Renderer = New ToolStripProfessionalRenderer()
            SetMenuColors(MenuStrip1.Items, Color.Black)
            StatusStrip1.BackColor = Color.FromArgb(0, 99, 177)
            StatusStrip1.ForeColor = Color.White
            ToolBarPanel.BackColor = Color.FromArgb(238, 238, 242)
            btnScrollLeft.BackColor = Color.FromArgb(195, 195, 198)
            btnScrollLeft.ForeColor = Color.FromArgb(50, 50, 50)
            btnScrollRight.BackColor = Color.FromArgb(195, 195, 198)
            btnScrollRight.ForeColor = Color.FromArgb(50, 50, 50)
            bNew.ForeColor = Color.FromArgb(50, 50, 50)
            bOpen.ForeColor = Color.FromArgb(50, 50, 50)
            bSave.ForeColor = Color.FromArgb(50, 50, 50)
            bFind.ForeColor = Color.FromArgb(50, 50, 50)
        End If
        For i = 0 To _tabs.Count - 1
            _tabs(i).Editor.Theme = If(_isDark, CType(New DarkTheme(), Object), New LightTheme())
        Next
        UpdateTabTitles()
    End Sub

    ' ─────────────────────────────────────────────
    '  STATUS & TITLE
    ' ─────────────────────────────────────────────
    Private Sub UpdateStatusBar()
        Dim L = LanguageManager.Instance
        If _activeIndex < 0 OrElse _activeIndex >= _tabs.Count Then
            lblCaret.Text = L.StatusBar("DefaultCaret")
            lblLanguage.Text = L.StatusBar("DefaultLanguage")
            Return
        End If
        Dim t = _tabs(_activeIndex)
        lblCaret.Text = "Ln " & t.Editor.CurrentLine & ",  Col " & t.Editor.CurrentColumn
        lblLanguage.Text = If(t.Editor.LanguageName = "", L.StatusBar("DefaultLanguage"), t.Editor.LanguageName)
        lblEncoding.Text = L.StatusBar("Encoding")
        lblCRLF.Text = L.StatusBar("LineEnding")
    End Sub

    Private Sub UpdateTitleBar()
        If _activeIndex < 0 OrElse _activeIndex >= _tabs.Count Then
            Me.Text = "CE++"
            Return
        End If
        Dim t = _tabs(_activeIndex)
        Dim modified = If(t.IsModified, "* ", "")
        Dim name = If(t.FilePath <> "", t.FilePath, GetTabDisplayName(t))
        Me.Text = modified & name & " - CE++"
    End Sub

    ' ─────────────────────────────────────────────
    '  RECENT FILES
    ' ─────────────────────────────────────────────
    Private ReadOnly _recentFilesPath As String =
        Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CE++"), "recent.txt")

    Private Sub LoadRecentFiles()
        Try
            If File.Exists(_recentFilesPath) Then
                _recentFiles = New List(Of String)(File.ReadAllLines(_recentFilesPath))
            End If
        Catch
        End Try
        RefreshRecentMenu()
    End Sub

    Private Sub AddRecentFile(ByVal path As String)
        _recentFiles.Remove(path)
        _recentFiles.Insert(0, path)
        If _recentFiles.Count > 15 Then _recentFiles.RemoveAt(_recentFiles.Count - 1)
        Try
            Dim dir As String = IO.Path.GetDirectoryName(_recentFilesPath)
            If Not Directory.Exists(dir) Then Directory.CreateDirectory(dir)
            File.WriteAllLines(_recentFilesPath, _recentFiles.ToArray())
        Catch
        End Try
        RefreshRecentMenu()
    End Sub

    Private Sub RefreshRecentMenu()
        Dim L = LanguageManager.Instance
        OpenRecentToolStripMenuItem.DropDownItems.Clear()
        If _recentFiles.Count = 0 Then
            Dim empty = New ToolStripMenuItem(L.Dlg("NoRecentFiles")) With {.Enabled = False}
            OpenRecentToolStripMenuItem.DropDownItems.Add(empty)
        Else
            For Each f In _recentFiles
                Dim item = New ToolStripMenuItem(f) With {.Tag = f}
                AddHandler item.Click, AddressOf RecentFile_Click
                OpenRecentToolStripMenuItem.DropDownItems.Add(item)
            Next
            OpenRecentToolStripMenuItem.DropDownItems.Add(New ToolStripSeparator())
            Dim clear = New ToolStripMenuItem(L.Dlg("ClearRecentFiles"))
            AddHandler clear.Click, Sub(s, a)
                                        _recentFiles.Clear()
                                        Try
                                            If File.Exists(_recentFilesPath) Then File.Delete(_recentFilesPath)
                                        Catch
                                        End Try
                                        RefreshRecentMenu()
                                    End Sub
            OpenRecentToolStripMenuItem.DropDownItems.Add(clear)
        End If
    End Sub

    Private Sub RecentFile_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim path = CStr(CType(sender, ToolStripMenuItem).Tag)
        If File.Exists(path) Then
            OpenFileInTab(path)
        Else
            MessageBox.Show(LanguageManager.Instance.Dlg("FileNotFound", vbCrLf, path),
                            LanguageManager.Instance.Dlg("FileNotFoundTitle"),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            _recentFiles.Remove(path)
            RefreshRecentMenu()
        End If
    End Sub

    ' ─────────────────────────────────────────────
    '  FIND & REPLACE
    ' ─────────────────────────────────────────────
    Private Sub ToggleFindPanel()
        FindPanel.Visible = Not FindPanel.Visible
        If FindPanel.Visible Then
            txtFind.Focus()
            If _activeIndex >= 0 Then
                ' SelectedText not available; skip pre-fill''
                ' If sel <> "" Then txtFind.Text = sel
            End If
        End If
    End Sub

    Private Sub btnFindNext_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindNext.Click
        FindNext(False)
    End Sub

    Private Sub btnFindPrev_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindPrev.Click
        FindNext(True)
    End Sub

    Private _findPos As Integer = 0

    Private Sub FindNext(ByVal backward As Boolean)
        If _activeIndex < 0 OrElse txtFind.Text = "" Then Return
        Dim editor = _tabs(_activeIndex).Editor
        Dim code = editor.Code
        Dim searchStr = txtFind.Text
        If Not chkCaseSensitive.Checked Then
            code = code.ToLower()
            searchStr = searchStr.ToLower()
        End If
        Dim startPos As Integer = _findPos
        Dim idx As Integer
        If backward Then
            idx = code.LastIndexOf(searchStr, Math.Max(0, startPos - 1))
        Else
            idx = code.IndexOf(searchStr, startPos + 1)
            If idx < 0 Then idx = code.IndexOf(searchStr, 0)
        End If
        If idx >= 0 Then
            _findPos = idx
            ' Navigate to found line
            Dim lineNum As Integer = code.Substring(0, idx).Split(vbLf.ToCharArray())(0).Split(vbLf.ToCharArray()).Length
            Dim newlineCount As Integer = 0
            For Each ch As Char In code.Substring(0, idx)
                If ch = vbLf Then newlineCount += 1
            Next
            Try
                editor.GoToLine(newlineCount + 1)
            Catch
            End Try
            lblFindStatus.Text = LanguageManager.Instance.Status("FoundAt", idx)
        Else
            lblFindStatus.Text = LanguageManager.Instance.Status("NotFound")
        End If
    End Sub

    Private Sub btnReplace_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReplace.Click
        If _activeIndex < 0 OrElse txtFind.Text = "" Then Return
        Dim editor = _tabs(_activeIndex).Editor
        Dim code = editor.Code
        Dim comp = If(chkCaseSensitive.Checked, StringComparison.Ordinal, StringComparison.OrdinalIgnoreCase)
        Dim idx = code.IndexOf(txtFind.Text, _findPos, comp)
        If idx >= 0 Then
            editor.Code = code.Substring(0, idx) & txtReplace.Text & code.Substring(idx + txtFind.Text.Length)
            _findPos = idx
        End If
        FindNext(False)
    End Sub

    Private Sub btnReplaceAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReplaceAll.Click
        If _activeIndex < 0 OrElse txtFind.Text = "" Then Return
        Dim editor = _tabs(_activeIndex).Editor
        Dim orig = editor.Code
        Dim comp = If(chkCaseSensitive.Checked, StringComparison.Ordinal, StringComparison.OrdinalIgnoreCase)
        Dim sb As New StringBuilder()
        Dim pos As Integer = 0
        Dim count As Integer = 0
        Do
            Dim idx = orig.IndexOf(txtFind.Text, pos, comp)
            If idx < 0 Then
                sb.Append(orig.Substring(pos))
                Exit Do
            End If
            sb.Append(orig.Substring(pos, idx - pos))
            sb.Append(txtReplace.Text)
            pos = idx + txtFind.Text.Length
            count += 1
        Loop
        If count > 0 Then
            editor.Code = sb.ToString()
            lblFindStatus.Text = LanguageManager.Instance.Status("Replaced", count)
        Else
            lblFindStatus.Text = LanguageManager.Instance.Status("NoMatches")
        End If
    End Sub

    Private Sub btnCloseFindPanel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCloseFindPanel.Click
        FindPanel.Visible = False
    End Sub

    Private Sub txtFind_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtFind.KeyDown
        If e.KeyCode = Keys.Return Then FindNext(e.Shift)
        If e.KeyCode = Keys.Escape Then FindPanel.Visible = False
    End Sub

    ' ─────────────────────────────────────────────
    '  GOTO LINE
    ' ─────────────────────────────────────────────
    Private Sub GoToLine()
        Dim L = LanguageManager.Instance
        Dim input = InputBox(L.Dlg("GoToLinePrompt"), L.Dlg("GoToLineTitle"), "1")
        Dim lineNum As Integer
        If Integer.TryParse(input, lineNum) AndAlso _activeIndex >= 0 Then
            Try
                _tabs(_activeIndex).Editor.GoToLine(lineNum)
            Catch
            End Try
        End If
    End Sub

    ' ─────────────────────────────────────────────
    '  KEYBOARD SHORTCUTS
    ' ─────────────────────────────────────────────
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        Select Case keyData
            Case Keys.Control Or Keys.N
                NewTab() : Return True
            Case Keys.Control Or Keys.O
                OpenFileDialog_Open() : Return True
            Case Keys.Control Or Keys.S
                If _activeIndex >= 0 Then SaveTab(_activeIndex) : Return True
            Case Keys.Control Or Keys.Shift Or Keys.S
                If _activeIndex >= 0 Then SaveTabAs(_activeIndex) : Return True
            Case Keys.Control Or Keys.W
                If _activeIndex >= 0 Then CloseTab(_activeIndex) : Return True
            Case Keys.Control Or Keys.F
                ToggleFindPanel() : Return True
            Case Keys.Control Or Keys.G
                GoToLine() : Return True
            Case Keys.Control Or Keys.Tab
                If _tabs.Count > 1 Then SwitchToTab((_activeIndex + 1) Mod _tabs.Count) : Return True
            Case Keys.Control Or Keys.Shift Or Keys.Tab
                If _tabs.Count > 1 Then
                    SwitchToTab((_activeIndex - 1 + _tabs.Count) Mod _tabs.Count)
                    Return True
                End If
        End Select
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    ' ─────────────────────────────────────────────
    '  MENU — FILE
    ' ─────────────────────────────────────────────
    Private Sub NewFileToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles NewFileToolStripMenuItem.Click
        NewTab()
    End Sub

    Private Sub OpenFileToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles OpenFileToolStripMenuItem.Click
        OpenFileDialog_Open()
    End Sub

    Private Sub OpenFileDialog_Open()
        Using dlg As New OpenFileDialog()
            dlg.Title = LanguageManager.Instance.Dlg("OpenFileTitle")
            dlg.Filter = GetFileFilter()
            dlg.Multiselect = True
            If dlg.ShowDialog() = DialogResult.OK Then
                For Each f In dlg.FileNames
                    OpenFileInTab(f)
                Next
            End If
        End Using
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles SaveToolStripMenuItem.Click
        If _activeIndex >= 0 Then SaveTab(_activeIndex)
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        If _activeIndex >= 0 Then SaveTabAs(_activeIndex)
    End Sub

    Private Sub SaveAllToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles SaveAllToolStripMenuItem.Click
        For i = 0 To _tabs.Count - 1
            If _tabs(i).IsModified Then SaveTab(i)
        Next
    End Sub

    Private Sub CloseTabToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles CloseTabToolStripMenuItem.Click
        If _activeIndex >= 0 Then CloseTab(_activeIndex)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles CloseAllToolStripMenuItem.Click
        ' Selalu tutup tab pertama karena index berubah setelah setiap close
        Do While _tabs.Count > 0
            Dim countBefore = _tabs.Count
            CloseTab(0)
            ' Jika user tekan Cancel pada dialog simpan, hentikan loop
            If _tabs.Count = countBefore Then Return
        Loop
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    ' ─────────────────────────────────────────────
    '  MENU — EDIT
    ' ─────────────────────────────────────────────
    Private Sub UndoToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles UndoToolStripMenuItem.Click
        If _activeIndex >= 0 Then _tabs(_activeIndex).Editor.Undo()
    End Sub
    Private Sub RedoToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles RedoToolStripMenuItem.Click
        If _activeIndex >= 0 Then _tabs(_activeIndex).Editor.Redo()
    End Sub
    Private Sub CutToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles CutToolStripMenuItem.Click
        If _activeIndex >= 0 Then _tabs(_activeIndex).Editor.Cut()
    End Sub
    Private Sub CopyToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles CopyToolStripMenuItem.Click
        If _activeIndex >= 0 Then _tabs(_activeIndex).Editor.Copy()
    End Sub
    Private Sub PasteToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles PasteToolStripMenuItem.Click
        If _activeIndex >= 0 Then _tabs(_activeIndex).Editor.Paste()
    End Sub
    Private Sub SelectAllToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles SelectAllToolStripMenuItem.Click
        If _activeIndex >= 0 Then _tabs(_activeIndex).Editor.SelectAll()
    End Sub
    Private Sub ToggleCommentToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles ToggleCommentToolStripMenuItem.Click
        If _activeIndex >= 0 Then _tabs(_activeIndex).Editor.ToggleComment()
    End Sub
    Private Sub FindToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles FindToolStripMenuItem.Click
        ToggleFindPanel()
    End Sub
    Private Sub GoToLineToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles GoToLineToolStripMenuItem.Click
        GoToLine()
    End Sub

    ' ─────────────────────────────────────────────
    '  MENU — VIEW
    ' ─────────────────────────────────────────────
    Private Sub WordWrapToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles WordWrapToolStripMenuItem.Click
        If _activeIndex >= 0 Then
            Dim ww = Not _tabs(_activeIndex).Editor.WordWrap
            _tabs(_activeIndex).Editor.WordWrap = ww
            WordWrapToolStripMenuItem.Checked = ww
        End If
    End Sub
    Private Sub ShowLineNumbersToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles ShowLineNumbersToolStripMenuItem.Click
        Dim show = Not ShowLineNumbersToolStripMenuItem.Checked
        For i = 0 To _tabs.Count - 1
            _tabs(i).Editor.ShowLineNumbers = show
        Next
        ShowLineNumbersToolStripMenuItem.Checked = show
    End Sub

    ' ─────────────────────────────────────────────
    '  MENU — LANGUAGE
    ' ─────────────────────────────────────────────
    Private Sub SetLanguage(ByVal lang As Object)
        If _activeIndex >= 0 Then _tabs(_activeIndex).Editor.Language = lang
    End Sub
    Private Sub CppToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles CppToolStripMenuItem.Click
        SetLanguage(New CppLanguage())
    End Sub
    Private Sub CSharpToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles CSharpToolStripMenuItem.Click
        SetLanguage(New CSharpLanguage())
    End Sub
    Private Sub CSSToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles CSSToolStripMenuItem.Click
        SetLanguage(New CssLanguage())
    End Sub
    Private Sub GoToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles GoToolStripMenuItem.Click
        SetLanguage(New GoLanguage())
    End Sub
    Private Sub HTMLToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles HTMLToolStripMenuItem.Click
        SetLanguage(New HtmlLanguage())
    End Sub
    Private Sub JavaToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles JavaToolStripMenuItem.Click
        SetLanguage(New JavaLanguage())
    End Sub
    Private Sub JavaScriptToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles JavaScriptToolStripMenuItem.Click
        SetLanguage(New JavaScriptLanguage())
    End Sub
    Private Sub PHPToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles PHPToolStripMenuItem.Click
        SetLanguage(New PhpLanguage())
    End Sub
    Private Sub PythonToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles PythonToolStripMenuItem.Click
        SetLanguage(New PythonLanguage())
    End Sub
    Private Sub RustToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles RustToolStripMenuItem.Click
        SetLanguage(New RustLanguage())
    End Sub
    Private Sub SQLToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles SQLToolStripMenuItem.Click
        SetLanguage(New SqlLanguage())
    End Sub
    Private Sub VBToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles VBToolStripMenuItem.Click
        SetLanguage(New VbLanguage())
    End Sub
    Private Sub XMLToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles XMLToolStripMenuItem.Click
        SetLanguage(New XmlLanguage())
    End Sub
    Private Sub PlainTextToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles PlainTextToolStripMenuItem.Click
        SetLanguage(Nothing)
    End Sub

    ' ─────────────────────────────────────────────
    '  MENU — THEMES
    ' ─────────────────────────────────────────────
    Private Sub DarkToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles DarkToolStripMenuItem.Click
        _isDark = True
        ApplyTheme()
        LightToolStripMenuItem.Checked = False
        DarkToolStripMenuItem.Checked = True
    End Sub
    Private Sub LightToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles LightToolStripMenuItem.Click
        _isDark = False
        ApplyTheme()
        LightToolStripMenuItem.Checked = True
        DarkToolStripMenuItem.Checked = False
    End Sub

    ' ─────────────────────────────────────────────
    '  MENU — HELP
    ' ─────────────────────────────────────────────
    Private Sub AboutToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles AboutToolStripMenuItem.Click
        frmAbout.ShowDialog()
    End Sub

    Private Sub KeyboardShortcutsToolStripMenuItem_Click(ByVal s As Object, ByVal e As EventArgs) Handles KeyboardShortcutsToolStripMenuItem.Click
        Dim L = LanguageManager.Instance
        frmKeyboard.Text = L.KbShortcut("Title")

        Dim content As String = L.KbShortcut("Content")

        content = content.Replace("{0}", Environment.NewLine)

        frmKeyboard.TextBox1.Font = New Font("Consolas", 10)
        frmKeyboard.TextBox1.Text = content

        frmKeyboard.ShowDialog()
    End Sub

    Private Sub btnScrollLeft_Click(ByVal s As Object, ByVal e As EventArgs) Handles btnScrollLeft.Click
        _tabScrollOffset = Math.Max(0, _tabScrollOffset - 160)
        RepositionTabs()
    End Sub

    Private Sub btnScrollRight_Click(ByVal s As Object, ByVal e As EventArgs) Handles btnScrollRight.Click
        Dim totalWidth As Integer = _tabs.Count * 160
        _tabScrollOffset = Math.Min(_tabScrollOffset + 160, Math.Max(0, totalWidth - TabsPanel.Width))
        RepositionTabs()
    End Sub

    ' ─────────────────────────────────────────────
    '  TOOLBAR
    ' ─────────────────────────────────────────────
    Private Sub btnNew_Click(ByVal s As Object, ByVal e As EventArgs)

    End Sub
    Private Sub btnOpen_Click(ByVal s As Object, ByVal e As EventArgs)

    End Sub
    Private Sub btnSave_Click(ByVal s As Object, ByVal e As EventArgs)

    End Sub
    Private Sub btnFind_Click(ByVal s As Object, ByVal e As EventArgs)

    End Sub

    ' ─────────────────────────────────────────────
    '  FORM CLOSING
    ' ─────────────────────────────────────────────
    Private Sub Form1_FormClosing(ByVal s As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Kumpulkan semua tab yang belum disimpan
        Dim unsavedNames As New List(Of String)
        For Each tc As EditorTab In _tabs
            If tc.IsModified Then
                unsavedNames.Add("  * " & GetTabDisplayName(tc))
            End If
        Next

        If unsavedNames.Count = 0 Then Return ' Tidak ada yang belum disimpan, langsung keluar

        ' Buat pesan daftar file
        Dim L = LanguageManager.Instance
        Dim fileList As String = String.Join(vbCrLf, unsavedNames.ToArray())
        Dim msg As String = L.Dlg("UnsavedOnExit", vbCrLf, fileList)

        Dim result As DialogResult = MessageBox.Show(
            msg,
            L.Dlg("UnsavedOnExitTitle"),
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            ' Simpan semua yang belum disimpan
            For i As Integer = 0 To _tabs.Count - 1
                If _tabs(i).IsModified Then
                    If Not SaveTab(i) Then
                        ' Jika save gagal (misal user cancel SaveAs), batalkan keluar
                        e.Cancel = True
                        Return
                    End If
                End If
            Next
        ElseIf result = DialogResult.No Then
            ' Lewati / keluar tanpa simpan
            ' Biarkan program keluar
        Else
            ' Cancel → batalkan keluar
            e.Cancel = True
        End If
    End Sub


    Private Sub bNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bNew.Click
        Me.ActiveControl = Nothing
        NewTab()
    End Sub

    Private Sub bOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bOpen.Click
        Me.ActiveControl = Nothing
        OpenFileDialog_Open()
    End Sub

    Private Sub bSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSave.Click
        Me.ActiveControl = Nothing
        If _activeIndex >= 0 Then SaveTab(_activeIndex)
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bFind.Click
        Me.ActiveControl = Nothing
        ToggleFindPanel()
    End Sub

    Private Sub bOpen_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles bOpen.MouseEnter
        bOpen.Image = My.Resources.file
    End Sub

    Private Sub bOpen_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles bOpen.MouseLeave
        bOpen.Image = My.Resources.file1
    End Sub

    ' ─────────────────────────────────────────────
    '  COMMAND LINE / SINGLE INSTANCE
    ' ─────────────────────────────────────────────

    ' Dipanggil saat startup pertama: buka file yang dikirim lewat command line.
    ' Contoh: user double-klik main.py di Explorer → CE++.exe "main.py"
    Private Sub HandleCommandLineArgs()
        Dim args() As String = Environment.GetCommandLineArgs()
        ' args(0) adalah path EXE itu sendiri, lewati
        For i As Integer = 1 To args.Length - 1
            Dim path As String = args(i)
            If IO.File.Exists(path) Then
                OpenFileFromArgs(path)
            End If
        Next
    End Sub

    ' Dipanggil dari Application.Designer.vb (OnStartupNextInstance) ketika
    ' program sudah berjalan dan user double-klik file lain di Explorer.
    ' File dibuka di tab baru; jika sudah terbuka, aktifkan tab-nya saja.
    Public Sub OpenFileFromArgs(ByVal filePath As String)
        If Me.InvokeRequired Then
            ' OnStartupNextInstance bisa berjalan di thread berbeda —
            ' pastikan semua operasi UI dilakukan di UI thread.
            Me.Invoke(New Action(Of String)(AddressOf OpenFileFromArgs), filePath)
            Return
        End If
        OpenFileInTab(filePath)
    End Sub

End Class

Public Class frmKeyboard

    Private Sub frmKeyboard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PopulateShortcuts()
    End Sub

    Private Sub PopulateShortcuts()
        Dim L = LanguageManager.Instance
        lvShortcuts.Items.Clear()
        lblTitle.Text = L.KbShortcut("Title")
        lblSubtitle.Text = L.KbShortcut("Subtitle")

        AddGroup("  📁  " & L.KbShortcut("GroupFile"))
        AddItem(L.KbShortcut("NewFileKey"), L.KbShortcut("NewFile"))
        AddItem(L.KbShortcut("OpenFileKey"), L.KbShortcut("OpenFile"))
        AddItem(L.KbShortcut("SaveKey"), L.KbShortcut("Save"))
        AddItem(L.KbShortcut("SaveAsKey"), L.KbShortcut("SaveAs"))
        AddItem(L.KbShortcut("CloseTabKey"), L.KbShortcut("CloseTab"))
        AddItem(L.KbShortcut("NextTabKey"), L.KbShortcut("NextTab"))
        AddItem(L.KbShortcut("PreviousTabKey"), L.KbShortcut("PreviousTab"))

        AddGroup("  ✏️  " & L.KbShortcut("GroupEdit"))
        AddItem(L.KbShortcut("UndoKey"), L.KbShortcut("Undo"))
        AddItem(L.KbShortcut("RedoKey"), L.KbShortcut("Redo"))
        AddItem(L.KbShortcut("CutKey"), L.KbShortcut("Cut"))
        AddItem(L.KbShortcut("CopyKey"), L.KbShortcut("Copy"))
        AddItem(L.KbShortcut("PasteKey"), L.KbShortcut("Paste"))
        AddItem(L.KbShortcut("SelectAllKey"), L.KbShortcut("SelectAll"))
        AddItem(L.KbShortcut("CommentKey"), L.KbShortcut("Comment"))

        AddGroup("  🔍  " & L.KbShortcut("GroupSearch"))
        AddItem(L.KbShortcut("FindReplaceKey"), L.KbShortcut("FindReplace"))
        AddItem(L.KbShortcut("GoToLineKey"), L.KbShortcut("GoToLine"))
        AddItem(L.KbShortcut("FindNextKey"), L.KbShortcut("FindNext"))
        AddItem(L.KbShortcut("FindPreviousKey"), L.KbShortcut("FindPrevious"))
        AddItem(L.KbShortcut("EscapeKey"), L.KbShortcut("Escape"))

        AddGroup("  ▶  " & L.KbShortcut("GroupRun"))
        AddItem(L.KbShortcut("RunFileKey"), L.KbShortcut("RunFile"))
        AddItem(L.KbShortcut("KillProcKey"), L.KbShortcut("KillProc"))

        AddGroup("  🖥  " & L.KbShortcut("GroupView"))
        AddItem(L.KbShortcut("ZoomInKey"), L.KbShortcut("ZoomIn"))
        AddItem(L.KbShortcut("ZoomOutKey"), L.KbShortcut("ZoomOut"))
        lvShortcuts.Columns(0).Text = L.KbShortcut("Shortcut")
        lvShortcuts.Columns(1).Text = L.KbShortcut("Description")
        lvShortcuts.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
        lvShortcuts.Columns(0).Width = 220
        lvShortcuts.Columns(1).Width = lvShortcuts.ClientSize.Width - 220 - 4
    End Sub

    Private Sub AddGroup(title As String)
        Dim item As New ListViewItem(title)
        item.UseItemStyleForSubItems = True
        item.BackColor = System.Drawing.Color.FromArgb(45, 45, 48)
        item.ForeColor = System.Drawing.Color.FromArgb(100, 180, 255)
        item.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
        item.SubItems.Add("")
        item.Tag = "group"
        lvShortcuts.Items.Add(item)
    End Sub

    Private Sub AddItem(shortcut As String, description As String)
        Dim item As New ListViewItem(shortcut)
        item.UseItemStyleForSubItems = False
        item.ForeColor = System.Drawing.Color.FromArgb(255, 215, 0)
        item.Font = New System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold)
        Dim sub1 As New ListViewItem.ListViewSubItem()
        sub1.Text = description
        sub1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220)
        sub1.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular)
        item.SubItems.Add(sub1)
        item.Tag = "item"
        lvShortcuts.Items.Add(item)
    End Sub

    Private Sub lvShortcuts_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvShortcuts.MouseDown
        Dim hit = lvShortcuts.HitTest(e.Location)
        If hit.Item IsNot Nothing AndAlso hit.Item.Tag.ToString() = "group" Then
            lvShortcuts.SelectedItems.Clear()
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmKeyboard_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

End Class

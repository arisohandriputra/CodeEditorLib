<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmRunSettings
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblSub = New System.Windows.Forms.Label()
        Me.grpPython = New System.Windows.Forms.GroupBox()
        Me.btnDetectPython = New System.Windows.Forms.Button()
        Me.txtPython = New System.Windows.Forms.TextBox()
        Me.btnBrowsePython = New System.Windows.Forms.Button()
        Me.lblPythonHint = New System.Windows.Forms.Label()
        Me.grpCpp = New System.Windows.Forms.GroupBox()
        Me.btnDetectCpp = New System.Windows.Forms.Button()
        Me.txtCpp = New System.Windows.Forms.TextBox()
        Me.btnBrowseCpp = New System.Windows.Forms.Button()
        Me.lblCppHint = New System.Windows.Forms.Label()
        Me.grpNode = New System.Windows.Forms.GroupBox()
        Me.btnDetectNode = New System.Windows.Forms.Button()
        Me.txtNode = New System.Windows.Forms.TextBox()
        Me.btnBrowseNode = New System.Windows.Forms.Button()
        Me.lblNodeHint = New System.Windows.Forms.Label()
        Me.grpRuby = New System.Windows.Forms.GroupBox()
        Me.txtRuby = New System.Windows.Forms.TextBox()
        Me.btnBrowseRuby = New System.Windows.Forms.Button()
        Me.lblRubyHint = New System.Windows.Forms.Label()
        Me.grpJava = New System.Windows.Forms.GroupBox()
        Me.btnDetectJava = New System.Windows.Forms.Button()
        Me.txtJava = New System.Windows.Forms.TextBox()
        Me.btnBrowseJava = New System.Windows.Forms.Button()
        Me.lblJavaHint = New System.Windows.Forms.Label()
        Me.grpHtml = New System.Windows.Forms.GroupBox()
        Me.lblHtmlInfo = New System.Windows.Forms.Label()
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.grpPython.SuspendLayout()
        Me.grpCpp.SuspendLayout()
        Me.grpNode.SuspendLayout()
        Me.grpRuby.SuspendLayout()
        Me.grpJava.SuspendLayout()
        Me.grpHtml.SuspendLayout()
        Me.pnlButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30)
        Me.lblTitle.Location = New System.Drawing.Point(12, 12)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(120, 25)
        Me.lblTitle.Text = ""
        '
        'lblSub
        '
        Me.lblSub.AutoSize = True
        Me.lblSub.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblSub.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100)
        Me.lblSub.Location = New System.Drawing.Point(13, 40)
        Me.lblSub.Name = "lblSub"
        Me.lblSub.Size = New System.Drawing.Size(380, 13)
        Me.lblSub.Text = ""
        '
        'grpHtml
        '
        Me.grpHtml.Controls.Add(Me.lblHtmlInfo)
        Me.grpHtml.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpHtml.Location = New System.Drawing.Point(12, 62)
        Me.grpHtml.Name = "grpHtml"
        Me.grpHtml.Size = New System.Drawing.Size(500, 52)
        Me.grpHtml.TabIndex = 0
        Me.grpHtml.TabStop = False
        Me.grpHtml.Text = ""
        '
        'lblHtmlInfo
        '
        Me.lblHtmlInfo.AutoSize = True
        Me.lblHtmlInfo.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblHtmlInfo.ForeColor = System.Drawing.Color.FromArgb(0, 120, 0)
        Me.lblHtmlInfo.Location = New System.Drawing.Point(8, 22)
        Me.lblHtmlInfo.Name = "lblHtmlInfo"
        Me.lblHtmlInfo.Size = New System.Drawing.Size(350, 13)
        Me.lblHtmlInfo.Text = ""
        '
        'grpPython
        '
        Me.grpPython.Controls.Add(Me.lblPythonHint)
        Me.grpPython.Controls.Add(Me.txtPython)
        Me.grpPython.Controls.Add(Me.btnBrowsePython)
        Me.grpPython.Controls.Add(Me.btnDetectPython)
        Me.grpPython.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpPython.Location = New System.Drawing.Point(12, 124)
        Me.grpPython.Name = "grpPython"
        Me.grpPython.Size = New System.Drawing.Size(500, 68)
        Me.grpPython.TabIndex = 1
        Me.grpPython.TabStop = False
        Me.grpPython.Text = ""
        '
        'lblPythonHint
        '
        Me.lblPythonHint.AutoSize = True
        Me.lblPythonHint.Font = New System.Drawing.Font("Segoe UI", 7.5!)
        Me.lblPythonHint.ForeColor = System.Drawing.Color.Gray
        Me.lblPythonHint.Location = New System.Drawing.Point(8, 20)
        Me.lblPythonHint.Name = "lblPythonHint"
        Me.lblPythonHint.Text = ""
        '
        'txtPython
        '
        Me.txtPython.Font = New System.Drawing.Font("Consolas", 9.0!)
        Me.txtPython.Location = New System.Drawing.Point(8, 38)
        Me.txtPython.Name = "txtPython"
        Me.txtPython.Size = New System.Drawing.Size(310, 22)
        Me.txtPython.TabIndex = 0
        '
        'btnBrowsePython
        '
        Me.btnBrowsePython.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular)
        Me.btnBrowsePython.Location = New System.Drawing.Point(325, 37)
        Me.btnBrowsePython.Name = "btnBrowsePython"
        Me.btnBrowsePython.Size = New System.Drawing.Size(70, 24)
        Me.btnBrowsePython.TabIndex = 1
        Me.btnBrowsePython.Text = ""
        '
        'btnDetectPython
        '
        Me.btnDetectPython.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular)
        Me.btnDetectPython.Location = New System.Drawing.Point(400, 37)
        Me.btnDetectPython.Name = "btnDetectPython"
        Me.btnDetectPython.Size = New System.Drawing.Size(90, 24)
        Me.btnDetectPython.TabIndex = 2
        Me.btnDetectPython.Text = ""
        '
        'grpCpp
        '
        Me.grpCpp.Controls.Add(Me.lblCppHint)
        Me.grpCpp.Controls.Add(Me.txtCpp)
        Me.grpCpp.Controls.Add(Me.btnBrowseCpp)
        Me.grpCpp.Controls.Add(Me.btnDetectCpp)
        Me.grpCpp.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpCpp.Location = New System.Drawing.Point(12, 202)
        Me.grpCpp.Name = "grpCpp"
        Me.grpCpp.Size = New System.Drawing.Size(500, 68)
        Me.grpCpp.TabIndex = 2
        Me.grpCpp.TabStop = False
        Me.grpCpp.Text = ""
        '
        'lblCppHint
        '
        Me.lblCppHint.AutoSize = True
        Me.lblCppHint.Font = New System.Drawing.Font("Segoe UI", 7.5!)
        Me.lblCppHint.ForeColor = System.Drawing.Color.Gray
        Me.lblCppHint.Location = New System.Drawing.Point(8, 20)
        Me.lblCppHint.Name = "lblCppHint"
        Me.lblCppHint.Text = ""
        '
        'txtCpp
        '
        Me.txtCpp.Font = New System.Drawing.Font("Consolas", 9.0!)
        Me.txtCpp.Location = New System.Drawing.Point(8, 38)
        Me.txtCpp.Name = "txtCpp"
        Me.txtCpp.Size = New System.Drawing.Size(310, 22)
        Me.txtCpp.TabIndex = 0
        '
        'btnBrowseCpp
        '
        Me.btnBrowseCpp.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular)
        Me.btnBrowseCpp.Location = New System.Drawing.Point(325, 37)
        Me.btnBrowseCpp.Name = "btnBrowseCpp"
        Me.btnBrowseCpp.Size = New System.Drawing.Size(70, 24)
        Me.btnBrowseCpp.TabIndex = 1
        Me.btnBrowseCpp.Text = ""
        '
        'btnDetectCpp
        '
        Me.btnDetectCpp.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular)
        Me.btnDetectCpp.Location = New System.Drawing.Point(400, 37)
        Me.btnDetectCpp.Name = "btnDetectCpp"
        Me.btnDetectCpp.Size = New System.Drawing.Size(90, 24)
        Me.btnDetectCpp.TabIndex = 2
        Me.btnDetectCpp.Text = ""
        '
        'grpNode
        '
        Me.grpNode.Controls.Add(Me.lblNodeHint)
        Me.grpNode.Controls.Add(Me.txtNode)
        Me.grpNode.Controls.Add(Me.btnBrowseNode)
        Me.grpNode.Controls.Add(Me.btnDetectNode)
        Me.grpNode.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpNode.Location = New System.Drawing.Point(12, 280)
        Me.grpNode.Name = "grpNode"
        Me.grpNode.Size = New System.Drawing.Size(500, 68)
        Me.grpNode.TabIndex = 3
        Me.grpNode.TabStop = False
        Me.grpNode.Text = ""
        '
        'lblNodeHint
        '
        Me.lblNodeHint.AutoSize = True
        Me.lblNodeHint.Font = New System.Drawing.Font("Segoe UI", 7.5!)
        Me.lblNodeHint.ForeColor = System.Drawing.Color.Gray
        Me.lblNodeHint.Location = New System.Drawing.Point(8, 20)
        Me.lblNodeHint.Name = "lblNodeHint"
        Me.lblNodeHint.Text = ""
        '
        'txtNode
        '
        Me.txtNode.Font = New System.Drawing.Font("Consolas", 9.0!)
        Me.txtNode.Location = New System.Drawing.Point(8, 38)
        Me.txtNode.Name = "txtNode"
        Me.txtNode.Size = New System.Drawing.Size(310, 22)
        Me.txtNode.TabIndex = 0
        '
        'btnBrowseNode
        '
        Me.btnBrowseNode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular)
        Me.btnBrowseNode.Location = New System.Drawing.Point(325, 37)
        Me.btnBrowseNode.Name = "btnBrowseNode"
        Me.btnBrowseNode.Size = New System.Drawing.Size(70, 24)
        Me.btnBrowseNode.TabIndex = 1
        Me.btnBrowseNode.Text = ""
        '
        'btnDetectNode
        '
        Me.btnDetectNode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular)
        Me.btnDetectNode.Location = New System.Drawing.Point(400, 37)
        Me.btnDetectNode.Name = "btnDetectNode"
        Me.btnDetectNode.Size = New System.Drawing.Size(90, 24)
        Me.btnDetectNode.TabIndex = 2
        Me.btnDetectNode.Text = ""
        '
        'grpRuby
        '
        Me.grpRuby.Controls.Add(Me.lblRubyHint)
        Me.grpRuby.Controls.Add(Me.txtRuby)
        Me.grpRuby.Controls.Add(Me.btnBrowseRuby)
        Me.grpRuby.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpRuby.Location = New System.Drawing.Point(12, 358)
        Me.grpRuby.Name = "grpRuby"
        Me.grpRuby.Size = New System.Drawing.Size(500, 68)
        Me.grpRuby.TabIndex = 4
        Me.grpRuby.TabStop = False
        Me.grpRuby.Text = ""
        '
        'lblRubyHint
        '
        Me.lblRubyHint.AutoSize = True
        Me.lblRubyHint.Font = New System.Drawing.Font("Segoe UI", 7.5!)
        Me.lblRubyHint.ForeColor = System.Drawing.Color.Gray
        Me.lblRubyHint.Location = New System.Drawing.Point(8, 20)
        Me.lblRubyHint.Name = "lblRubyHint"
        Me.lblRubyHint.Text = ""
        '
        'txtRuby
        '
        Me.txtRuby.Font = New System.Drawing.Font("Consolas", 9.0!)
        Me.txtRuby.Location = New System.Drawing.Point(8, 38)
        Me.txtRuby.Name = "txtRuby"
        Me.txtRuby.Size = New System.Drawing.Size(310, 22)
        Me.txtRuby.TabIndex = 0
        '
        'btnBrowseRuby
        '
        Me.btnBrowseRuby.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular)
        Me.btnBrowseRuby.Location = New System.Drawing.Point(325, 37)
        Me.btnBrowseRuby.Name = "btnBrowseRuby"
        Me.btnBrowseRuby.Size = New System.Drawing.Size(70, 24)
        Me.btnBrowseRuby.TabIndex = 1
        Me.btnBrowseRuby.Text = ""
        '
        'grpJava
        '
        Me.grpJava.Controls.Add(Me.lblJavaHint)
        Me.grpJava.Controls.Add(Me.txtJava)
        Me.grpJava.Controls.Add(Me.btnBrowseJava)
        Me.grpJava.Controls.Add(Me.btnDetectJava)
        Me.grpJava.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpJava.Location = New System.Drawing.Point(12, 436)
        Me.grpJava.Name = "grpJava"
        Me.grpJava.Size = New System.Drawing.Size(500, 68)
        Me.grpJava.TabIndex = 5
        Me.grpJava.TabStop = False
        Me.grpJava.Text = ""
        '
        'lblJavaHint
        '
        Me.lblJavaHint.AutoSize = True
        Me.lblJavaHint.Font = New System.Drawing.Font("Segoe UI", 7.5!)
        Me.lblJavaHint.ForeColor = System.Drawing.Color.Gray
        Me.lblJavaHint.Location = New System.Drawing.Point(8, 20)
        Me.lblJavaHint.Name = "lblJavaHint"
        Me.lblJavaHint.Text = ""
        '
        'txtJava
        '
        Me.txtJava.Font = New System.Drawing.Font("Consolas", 9.0!)
        Me.txtJava.Location = New System.Drawing.Point(8, 38)
        Me.txtJava.Name = "txtJava"
        Me.txtJava.Size = New System.Drawing.Size(310, 22)
        Me.txtJava.TabIndex = 0
        '
        'btnBrowseJava
        '
        Me.btnBrowseJava.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular)
        Me.btnBrowseJava.Location = New System.Drawing.Point(325, 37)
        Me.btnBrowseJava.Name = "btnBrowseJava"
        Me.btnBrowseJava.Size = New System.Drawing.Size(70, 24)
        Me.btnBrowseJava.TabIndex = 1
        Me.btnBrowseJava.Text = ""
        '
        'btnDetectJava
        '
        Me.btnDetectJava.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular)
        Me.btnDetectJava.Location = New System.Drawing.Point(400, 37)
        Me.btnDetectJava.Name = "btnDetectJava"
        Me.btnDetectJava.Size = New System.Drawing.Size(90, 24)
        Me.btnDetectJava.TabIndex = 2
        Me.btnDetectJava.Text = ""
        '
        'pnlButtons
        '
        Me.pnlButtons.Controls.Add(Me.btnSave)
        Me.pnlButtons.Controls.Add(Me.btnCancel)
        Me.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlButtons.Location = New System.Drawing.Point(0, 520)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(528, 44)
        Me.pnlButtons.TabIndex = 6
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(0, 120, 212)
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.Location = New System.Drawing.Point(330, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(90, 28)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = ""
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnCancel.Location = New System.Drawing.Point(428, 8)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 28)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = ""
        '
        'frmRunSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(245, 245, 248)
        Me.ClientSize = New System.Drawing.Size(528, 564)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.lblSub)
        Me.Controls.Add(Me.grpHtml)
        Me.Controls.Add(Me.grpPython)
        Me.Controls.Add(Me.grpCpp)
        Me.Controls.Add(Me.grpNode)
        Me.Controls.Add(Me.grpRuby)
        Me.Controls.Add(Me.grpJava)
        Me.Controls.Add(Me.pnlButtons)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRunSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = ""
        Me.grpPython.ResumeLayout(False)
        Me.grpPython.PerformLayout()
        Me.grpCpp.ResumeLayout(False)
        Me.grpCpp.PerformLayout()
        Me.grpNode.ResumeLayout(False)
        Me.grpNode.PerformLayout()
        Me.grpRuby.ResumeLayout(False)
        Me.grpRuby.PerformLayout()
        Me.grpJava.ResumeLayout(False)
        Me.grpJava.PerformLayout()
        Me.grpHtml.ResumeLayout(False)
        Me.grpHtml.PerformLayout()
        Me.pnlButtons.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblSub As System.Windows.Forms.Label
    Friend WithEvents grpHtml As System.Windows.Forms.GroupBox
    Friend WithEvents lblHtmlInfo As System.Windows.Forms.Label
    Friend WithEvents grpPython As System.Windows.Forms.GroupBox
    Friend WithEvents lblPythonHint As System.Windows.Forms.Label
    Friend WithEvents txtPython As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowsePython As System.Windows.Forms.Button
    Friend WithEvents btnDetectPython As System.Windows.Forms.Button
    Friend WithEvents grpCpp As System.Windows.Forms.GroupBox
    Friend WithEvents lblCppHint As System.Windows.Forms.Label
    Friend WithEvents txtCpp As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowseCpp As System.Windows.Forms.Button
    Friend WithEvents btnDetectCpp As System.Windows.Forms.Button
    Friend WithEvents grpNode As System.Windows.Forms.GroupBox
    Friend WithEvents lblNodeHint As System.Windows.Forms.Label
    Friend WithEvents txtNode As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowseNode As System.Windows.Forms.Button
    Friend WithEvents btnDetectNode As System.Windows.Forms.Button
    Friend WithEvents grpRuby As System.Windows.Forms.GroupBox
    Friend WithEvents lblRubyHint As System.Windows.Forms.Label
    Friend WithEvents txtRuby As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowseRuby As System.Windows.Forms.Button
    Friend WithEvents grpJava As System.Windows.Forms.GroupBox
    Friend WithEvents lblJavaHint As System.Windows.Forms.Label
    Friend WithEvents txtJava As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowseJava As System.Windows.Forms.Button
    Friend WithEvents btnDetectJava As System.Windows.Forms.Button
    Friend WithEvents pnlButtons As System.Windows.Forms.Panel
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button

End Class

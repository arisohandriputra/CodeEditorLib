<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAbout
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
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.picLogo = New System.Windows.Forms.PictureBox()
        Me.lblTagline = New System.Windows.Forms.Label()
        Me.pnlBody = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblAuthor = New System.Windows.Forms.Label()
        Me.pnlDivider1 = New System.Windows.Forms.Panel()
        Me.lblTranslatorsHeader = New System.Windows.Forms.Label()
        Me.lblTranslatorsList = New System.Windows.Forms.Label()
        Me.pnlDivider2 = New System.Windows.Forms.Panel()
        Me.lbSupport = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pnlDivider3 = New System.Windows.Forms.Panel()
        Me.lblLove = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lnkSourceCode = New System.Windows.Forms.LinkLabel()
        Me.pnlFooter = New System.Windows.Forms.Panel()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.lnkLicense = New System.Windows.Forms.LinkLabel()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pnlHeader.SuspendLayout()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBody.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFooter.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.White
        Me.pnlHeader.Controls.Add(Me.Label2)
        Me.pnlHeader.Controls.Add(Me.PictureBox2)
        Me.pnlHeader.Controls.Add(Me.lblVersion)
        Me.pnlHeader.Controls.Add(Me.picLogo)
        Me.pnlHeader.Controls.Add(Me.lblTagline)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(340, 122)
        Me.pnlHeader.TabIndex = 0
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.lblVersion.Location = New System.Drawing.Point(10, 61)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(28, 15)
        Me.lblVersion.TabIndex = 2
        Me.lblVersion.Text = "v1.1"
        '
        'picLogo
        '
        Me.picLogo.Image = Global.CE__.My.Resources.Resources.CE___Logo
        Me.picLogo.Location = New System.Drawing.Point(4, 4)
        Me.picLogo.Name = "picLogo"
        Me.picLogo.Size = New System.Drawing.Size(90, 63)
        Me.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picLogo.TabIndex = 0
        Me.picLogo.TabStop = False
        '
        'lblTagline
        '
        Me.lblTagline.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTagline.ForeColor = System.Drawing.Color.Gray
        Me.lblTagline.Location = New System.Drawing.Point(4, 76)
        Me.lblTagline.Name = "lblTagline"
        Me.lblTagline.Size = New System.Drawing.Size(210, 43)
        Me.lblTagline.TabIndex = 3
        Me.lblTagline.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlBody
        '
        Me.pnlBody.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlBody.Controls.Add(Me.Label1)
        Me.pnlBody.Controls.Add(Me.lblAuthor)
        Me.pnlBody.Controls.Add(Me.pnlDivider1)
        Me.pnlBody.Controls.Add(Me.lblTranslatorsHeader)
        Me.pnlBody.Controls.Add(Me.lblTranslatorsList)
        Me.pnlBody.Controls.Add(Me.pnlDivider2)
        Me.pnlBody.Controls.Add(Me.lbSupport)
        Me.pnlBody.Controls.Add(Me.PictureBox1)
        Me.pnlBody.Controls.Add(Me.pnlDivider3)
        Me.pnlBody.Controls.Add(Me.lblLove)
        Me.pnlBody.Controls.Add(Me.Label4)
        Me.pnlBody.Controls.Add(Me.lnkSourceCode)
        Me.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBody.Location = New System.Drawing.Point(0, 122)
        Me.pnlBody.Name = "pnlBody"
        Me.pnlBody.Padding = New System.Windows.Forms.Padding(20, 14, 20, 0)
        Me.pnlBody.Size = New System.Drawing.Size(340, 250)
        Me.pnlBody.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(100, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Ari Sohandri Putra"
        '
        'lblAuthor
        '
        Me.lblAuthor.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAuthor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblAuthor.Location = New System.Drawing.Point(20, 14)
        Me.lblAuthor.Name = "lblAuthor"
        Me.lblAuthor.Size = New System.Drawing.Size(74, 18)
        Me.lblAuthor.TabIndex = 0
        '
        'pnlDivider1
        '
        Me.pnlDivider1.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.pnlDivider1.Location = New System.Drawing.Point(20, 38)
        Me.pnlDivider1.Name = "pnlDivider1"
        Me.pnlDivider1.Size = New System.Drawing.Size(300, 1)
        Me.pnlDivider1.TabIndex = 10
        '
        'lblTranslatorsHeader
        '
        Me.lblTranslatorsHeader.AutoSize = True
        Me.lblTranslatorsHeader.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblTranslatorsHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.lblTranslatorsHeader.Location = New System.Drawing.Point(20, 48)
        Me.lblTranslatorsHeader.Name = "lblTranslatorsHeader"
        Me.lblTranslatorsHeader.Size = New System.Drawing.Size(0, 13)
        Me.lblTranslatorsHeader.TabIndex = 1
        '
        'lblTranslatorsList
        '
        Me.lblTranslatorsList.AutoSize = True
        Me.lblTranslatorsList.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblTranslatorsList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblTranslatorsList.Location = New System.Drawing.Point(20, 64)
        Me.lblTranslatorsList.Name = "lblTranslatorsList"
        Me.lblTranslatorsList.Size = New System.Drawing.Size(0, 15)
        Me.lblTranslatorsList.TabIndex = 2
        '
        'pnlDivider2
        '
        Me.pnlDivider2.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.pnlDivider2.Location = New System.Drawing.Point(20, 88)
        Me.pnlDivider2.Name = "pnlDivider2"
        Me.pnlDivider2.Size = New System.Drawing.Size(300, 1)
        Me.pnlDivider2.TabIndex = 11
        '
        'lbSupport
        '
        Me.lbSupport.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lbSupport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.lbSupport.Location = New System.Drawing.Point(20, 98)
        Me.lbSupport.Name = "lbSupport"
        Me.lbSupport.Size = New System.Drawing.Size(300, 16)
        Me.lbSupport.TabIndex = 3
        '
        'PictureBox1
        '
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Image = Global.CE__.My.Resources.Resources.github_sponsors
        Me.PictureBox1.Location = New System.Drawing.Point(91, 118)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(157, 49)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 4
        Me.PictureBox1.TabStop = False
        '
        'pnlDivider3
        '
        Me.pnlDivider3.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.pnlDivider3.Location = New System.Drawing.Point(20, 176)
        Me.pnlDivider3.Name = "pnlDivider3"
        Me.pnlDivider3.Size = New System.Drawing.Size(300, 1)
        Me.pnlDivider3.TabIndex = 12
        '
        'lblLove
        '
        Me.lblLove.AutoSize = True
        Me.lblLove.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblLove.ForeColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblLove.Location = New System.Drawing.Point(20, 186)
        Me.lblLove.Name = "lblLove"
        Me.lblLove.Size = New System.Drawing.Size(0, 15)
        Me.lblLove.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(20, 202)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(111, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Lombok, Indonesia"
        '
        'lnkSourceCode
        '
        Me.lnkSourceCode.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lnkSourceCode.LinkColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lnkSourceCode.Location = New System.Drawing.Point(20, 228)
        Me.lnkSourceCode.Name = "lnkSourceCode"
        Me.lnkSourceCode.Size = New System.Drawing.Size(300, 18)
        Me.lnkSourceCode.TabIndex = 7
        Me.lnkSourceCode.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'pnlFooter
        '
        Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(18, Byte), Integer), CType(CType(18, Byte), Integer), CType(CType(24, Byte), Integer))
        Me.pnlFooter.Controls.Add(Me.btnOK)
        Me.pnlFooter.Controls.Add(Me.lnkLicense)
        Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFooter.Location = New System.Drawing.Point(0, 372)
        Me.pnlFooter.Name = "pnlFooter"
        Me.pnlFooter.Padding = New System.Windows.Forms.Padding(16, 7, 16, 7)
        Me.pnlFooter.Size = New System.Drawing.Size(340, 38)
        Me.pnlFooter.TabIndex = 2
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.btnOK.FlatAppearance.BorderSize = 0
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.btnOK.Location = New System.Drawing.Point(256, 6)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(68, 26)
        Me.btnOK.TabIndex = 0
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'lnkLicense
        '
        Me.lnkLicense.AutoSize = True
        Me.lnkLicense.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lnkLicense.LinkColor = System.Drawing.Color.FromArgb(CType(CType(130, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(155, Byte), Integer))
        Me.lnkLicense.Location = New System.Drawing.Point(16, 12)
        Me.lnkLicense.Name = "lnkLicense"
        Me.lnkLicense.Size = New System.Drawing.Size(0, 15)
        Me.lnkLicense.TabIndex = 1
        Me.lnkLicense.TabStop = True
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.CE__.My.Resources.Resources.aboutpongo
        Me.PictureBox2.Location = New System.Drawing.Point(243, 4)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(81, 81)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 4
        Me.PictureBox2.TabStop = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label2.Location = New System.Drawing.Point(227, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(110, 40)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Protect the Wild, Secure the Future."
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmAbout
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(340, 410)
        Me.Controls.Add(Me.pnlBody)
        Me.Controls.Add(Me.pnlFooter)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAbout"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.TopMost = True
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBody.ResumeLayout(False)
        Me.pnlBody.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFooter.ResumeLayout(False)
        Me.pnlFooter.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents picLogo As System.Windows.Forms.PictureBox
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblTagline As System.Windows.Forms.Label
    Friend WithEvents pnlBody As System.Windows.Forms.Panel
    Friend WithEvents lblAuthor As System.Windows.Forms.Label
    Friend WithEvents lblTranslatorsHeader As System.Windows.Forms.Label
    Friend WithEvents lblTranslatorsList As System.Windows.Forms.Label
    Friend WithEvents lbSupport As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblLove As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lnkSourceCode As System.Windows.Forms.LinkLabel
    Friend WithEvents pnlDivider1 As System.Windows.Forms.Panel
    Friend WithEvents pnlDivider2 As System.Windows.Forms.Panel
    Friend WithEvents pnlDivider3 As System.Windows.Forms.Panel
    Friend WithEvents pnlFooter As System.Windows.Forms.Panel
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lnkLicense As System.Windows.Forms.LinkLabel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class

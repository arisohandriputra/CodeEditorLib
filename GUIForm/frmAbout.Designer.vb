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
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.lblTagline = New System.Windows.Forms.Label()
        Me.pnlBody = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbSupport = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblTranslatorsList = New System.Windows.Forms.Label()
        Me.lblTranslatorsHeader = New System.Windows.Forms.Label()
        Me.lblSep1 = New System.Windows.Forms.Label()
        Me.lblAuthor = New System.Windows.Forms.Label()
        Me.pnlFooter = New System.Windows.Forms.Panel()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.lnkLicense = New System.Windows.Forms.LinkLabel()
        Me.lbBanner = New System.Windows.Forms.Label()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.pnlHeader.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBody.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFooter.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.White
        Me.pnlHeader.Controls.Add(Me.lblVersion)
        Me.pnlHeader.Controls.Add(Me.PictureBox2)
        Me.pnlHeader.Controls.Add(Me.lblTagline)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Padding = New System.Windows.Forms.Padding(14, 9, 14, 9)
        Me.pnlHeader.Size = New System.Drawing.Size(307, 90)
        Me.pnlHeader.TabIndex = 2
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblVersion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.lblVersion.Location = New System.Drawing.Point(11, 4)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(28, 15)
        Me.lblVersion.TabIndex = 1
        Me.lblVersion.Text = "v1.0"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.CE__.My.Resources.Resources.CE___Logo
        Me.PictureBox2.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(146, 87)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 2
        Me.PictureBox2.TabStop = False
        '
        'lblTagline
        '
        Me.lblTagline.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTagline.ForeColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblTagline.Location = New System.Drawing.Point(152, 9)
        Me.lblTagline.Name = "lblTagline"
        Me.lblTagline.Size = New System.Drawing.Size(143, 72)
        Me.lblTagline.TabIndex = 0
        Me.lblTagline.Text = "the fast, free, open source Code Editor for Windows"
        Me.lblTagline.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlBody
        '
        Me.pnlBody.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlBody.Controls.Add(Me.LinkLabel2)
        Me.pnlBody.Controls.Add(Me.PictureBox3)
        Me.pnlBody.Controls.Add(Me.lbBanner)
        Me.pnlBody.Controls.Add(Me.Label2)
        Me.pnlBody.Controls.Add(Me.LinkLabel1)
        Me.pnlBody.Controls.Add(Me.Label1)
        Me.pnlBody.Controls.Add(Me.lbSupport)
        Me.pnlBody.Controls.Add(Me.PictureBox1)
        Me.pnlBody.Controls.Add(Me.lblTranslatorsList)
        Me.pnlBody.Controls.Add(Me.lblTranslatorsHeader)
        Me.pnlBody.Controls.Add(Me.lblSep1)
        Me.pnlBody.Controls.Add(Me.lblAuthor)
        Me.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBody.Location = New System.Drawing.Point(0, 90)
        Me.pnlBody.Name = "pnlBody"
        Me.pnlBody.Padding = New System.Windows.Forms.Padding(14, 9, 14, 3)
        Me.pnlBody.Size = New System.Drawing.Size(307, 273)
        Me.pnlBody.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Location = New System.Drawing.Point(12, 156)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(283, 2)
        Me.Label2.TabIndex = 10
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.LinkLabel1.Location = New System.Drawing.Point(14, 245)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(276, 15)
        Me.LinkLabel1.TabIndex = 3
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "CodeEditorLib Source Code"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Location = New System.Drawing.Point(12, 74)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(283, 2)
        Me.Label1.TabIndex = 9
        '
        'lbSupport
        '
        Me.lbSupport.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lbSupport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.lbSupport.Location = New System.Drawing.Point(12, 161)
        Me.lbSupport.Name = "lbSupport"
        Me.lbSupport.Size = New System.Drawing.Size(283, 15)
        Me.lbSupport.TabIndex = 8
        Me.lbSupport.Text = "Support Ari Sohandri Putra:"
        Me.lbSupport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Image = Global.CE__.My.Resources.Resources.github_sponsors
        Me.PictureBox1.Location = New System.Drawing.Point(71, 181)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(157, 49)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'lblTranslatorsList
        '
        Me.lblTranslatorsList.AutoSize = True
        Me.lblTranslatorsList.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblTranslatorsList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.lblTranslatorsList.Location = New System.Drawing.Point(12, 50)
        Me.lblTranslatorsList.Name = "lblTranslatorsList"
        Me.lblTranslatorsList.Size = New System.Drawing.Size(68, 15)
        Me.lblTranslatorsList.TabIndex = 0
        Me.lblTranslatorsList.Text = "CE++ Team"
        '
        'lblTranslatorsHeader
        '
        Me.lblTranslatorsHeader.AutoSize = True
        Me.lblTranslatorsHeader.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblTranslatorsHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.lblTranslatorsHeader.Location = New System.Drawing.Point(12, 33)
        Me.lblTranslatorsHeader.Name = "lblTranslatorsHeader"
        Me.lblTranslatorsHeader.Size = New System.Drawing.Size(73, 15)
        Me.lblTranslatorsHeader.TabIndex = 1
        Me.lblTranslatorsHeader.Text = "Translators :"
        '
        'lblSep1
        '
        Me.lblSep1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSep1.Location = New System.Drawing.Point(12, 25)
        Me.lblSep1.Name = "lblSep1"
        Me.lblSep1.Size = New System.Drawing.Size(283, 2)
        Me.lblSep1.TabIndex = 5
        '
        'lblAuthor
        '
        Me.lblAuthor.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAuthor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.lblAuthor.Location = New System.Drawing.Point(12, 5)
        Me.lblAuthor.Name = "lblAuthor"
        Me.lblAuthor.Size = New System.Drawing.Size(283, 15)
        Me.lblAuthor.TabIndex = 6
        Me.lblAuthor.Text = "Author : Ari Sohandri Putra"
        '
        'pnlFooter
        '
        Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.pnlFooter.Controls.Add(Me.btnOK)
        Me.pnlFooter.Controls.Add(Me.lnkLicense)
        Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFooter.Location = New System.Drawing.Point(0, 363)
        Me.pnlFooter.Name = "pnlFooter"
        Me.pnlFooter.Padding = New System.Windows.Forms.Padding(9, 5, 9, 5)
        Me.pnlFooter.Size = New System.Drawing.Size(307, 35)
        Me.pnlFooter.TabIndex = 1
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.BackColor = System.Drawing.SystemColors.Control
        Me.btnOK.FlatAppearance.BorderSize = 0
        Me.btnOK.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.ForeColor = System.Drawing.Color.Black
        Me.btnOK.Location = New System.Drawing.Point(232, 6)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(63, 24)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "&OK"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'lnkLicense
        '
        Me.lnkLicense.AutoSize = True
        Me.lnkLicense.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lnkLicense.Location = New System.Drawing.Point(10, 10)
        Me.lnkLicense.Name = "lnkLicense"
        Me.lnkLicense.Size = New System.Drawing.Size(69, 15)
        Me.lnkLicense.TabIndex = 2
        Me.lnkLicense.TabStop = True
        Me.lnkLicense.Text = "MIT License"
        '
        'lbBanner
        '
        Me.lbBanner.Font = New System.Drawing.Font("Segoe UI", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbBanner.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lbBanner.Location = New System.Drawing.Point(143, 79)
        Me.lbBanner.Name = "lbBanner"
        Me.lbBanner.Size = New System.Drawing.Size(152, 55)
        Me.lbBanner.TabIndex = 11
        Me.lbBanner.Text = "Stand Against Deforestation"
        Me.lbBanner.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.CE__.My.Resources.Resources.kalaweit
        Me.PictureBox3.Location = New System.Drawing.Point(12, 79)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(125, 74)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox3.TabIndex = 12
        Me.PictureBox3.TabStop = False
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel2.Location = New System.Drawing.Point(143, 134)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(116, 15)
        Me.LinkLabel2.TabIndex = 13
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "https://kalaweit.org/"
        '
        'frmAbout
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(307, 398)
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
        Me.Text = "About - CE++"
        Me.TopMost = True
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBody.ResumeLayout(False)
        Me.pnlBody.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFooter.ResumeLayout(False)
        Me.pnlFooter.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblTagline As System.Windows.Forms.Label
    Friend WithEvents pnlBody As System.Windows.Forms.Panel
    Friend WithEvents lblAuthor As System.Windows.Forms.Label
    Friend WithEvents lblSep1 As System.Windows.Forms.Label
    Friend WithEvents lblTranslatorsHeader As System.Windows.Forms.Label
    Friend WithEvents lblTranslatorsList As System.Windows.Forms.Label
    Friend WithEvents pnlFooter As System.Windows.Forms.Panel
    Friend WithEvents lnkLicense As System.Windows.Forms.LinkLabel
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lbSupport As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents lbBanner As System.Windows.Forms.Label

End Class

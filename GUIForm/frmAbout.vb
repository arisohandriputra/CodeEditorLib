Public Class frmAbout

    Private Sub frmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ApplyLanguage()
        PaintHeaderAccent()
    End Sub

    Private Sub ApplyLanguage()
        Dim L = LanguageManager.Instance
        Me.Text = L.About("FormTitle")
        lblTagline.Text = L.About("Tagline", vbCrLf)
        lblAuthor.Text = L.About("Author")
        lblTranslatorsHeader.Text = L.About("Translators")
        lblTranslatorsList.Text = L.About("TranslatorsName", vbCrLf)
        btnOK.Text = L.About("OKButton")
        lnkLicense.Text = L.About("LicenseLink")
        lbSupport.Text = L.About("LabelSupport")
        lnkSourceCode.Text = L.About("SourceCode")
        lblLove.Text = L.About("BannerText")
    End Sub

    '  Draw a subtle green accent bar at the bottom of the header
    Private Sub PaintHeaderAccent()
        AddHandler pnlHeader.Paint, AddressOf Header_Paint
        pnlHeader.Invalidate()
    End Sub

    Private Sub Header_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim w As Integer = pnlHeader.Width
        Dim h As Integer = pnlHeader.Height
        Using br As New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0, 122, 204))
            e.Graphics.FillRectangle(br, 0, h - 3, w, 3)
        End Using
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub lnkLicense_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkLicense.LinkClicked
        Try
            Process.Start("https://raw.githubusercontent.com/arisohandriputra/CodeEditorLib/refs/heads/main/LICENSE")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Try
            Process.Start("https://github.com/sponsors/arisohandriputra")
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PictureBox1_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.MouseEnter
        PictureBox1.Image = My.Resources.github_sponsors2
    End Sub

    Private Sub PictureBox1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.MouseLeave
        PictureBox1.Image = My.Resources.github_sponsors
    End Sub

    Private Sub lnkSourceCode_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSourceCode.LinkClicked
        Try
            Process.Start("https://github.com/arisohandriputra/CodeEditorLib")
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

End Class

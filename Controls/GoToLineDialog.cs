// =============================================================================
//  CodeEditorLib - GoToLineDialog
//  "Go To Line" dialog for the code editor component
// =============================================================================
//
//  Description :
//  GoToLineDialog provides a clean and modern interface for quickly
//  navigating to a specific line inside the editor.
//
//  Features:
//  - Modern borderless UI
//  - Theme-aware appearance
//  - Live line preview
//  - Interactive slider navigation
//  - Smooth drag movement
//  - Keyboard-friendly controls
//  - Compatible with dark and light themes
//
// -----------------------------------------------------------------------------
//
//  Author      : Ari Sohandri Putra
//  Repository  : https://github.com/arisohandriputra/CodeEditorLib
//  License     : MIT License
//  Copyright   : Copyright (c) 2026 Ari Sohandri Putra
//
// -----------------------------------------------------------------------------
//
//  Permission is hereby granted, free of charge, to any person obtaining a
//  copy of this software and associated documentation files (the "Software"),
//  to deal in the Software without restriction, including without limitation
//  the rights to use, copy, modify, merge, publish, distribute, sublicense,
//  and/or sell copies of the Software, and to permit persons to whom the
//  Software is furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//
//  IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
//  CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
//  TORT OR OTHERWISE, ARISING FROM, OUT OF, OR IN CONNECTION WITH THE
//  SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// =============================================================================

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CodeEditorLib.Themes;

namespace CodeEditorLib.Controls
{
    internal class GoToLineDialog : Form
    {
        private EditorTheme _theme;
        private RichTextBox _editor;
        private int _currentLine;
        private int _totalLines;

        private Panel _headerPanel;
        private Label _titleLabel;
        private Label _subtitleLabel;
        private Panel _bodyPanel;
        private Label _inputLabel;
        private NumericUpDown _lineInput;
        private Label _previewLabel;
        private Panel _previewPanel;
        private Label _previewLineLabel;
        private Panel _buttonPanel;
        private Button _goButton;
        private Button _cancelButton;
        private Panel _sliderPanel;

        private bool _dragging;
        private int _dragStartX;

        public int SelectedLine { get; private set; }

        public GoToLineDialog(EditorTheme theme, RichTextBox editor, int currentLine, int totalLines)
        {
            _theme       = theme;
            _editor      = editor;
            _currentLine = currentLine;
            _totalLines  = totalLines;
            SelectedLine = currentLine;

            BuildUI();
            ApplyTheme();
            UpdatePreview((int)_lineInput.Value);
        }

        private void BuildUI()
        {
            this.Text             = "Go To Line";
            this.FormBorderStyle  = FormBorderStyle.None;
            this.Size             = new Size(420, 290);
            this.StartPosition    = FormStartPosition.CenterParent;
            this.ShowInTaskbar    = false;
            this.TopMost          = true;

           
            _headerPanel          = new Panel();
            _headerPanel.Dock     = DockStyle.Top;
            _headerPanel.Height   = 56;
            _headerPanel.Padding  = new Padding(16, 0, 8, 0);

            _titleLabel           = new Label();
            _titleLabel.Text      = "Go To Line";
            _titleLabel.Font      = new Font("Segoe UI", 12f, FontStyle.Bold);
            _titleLabel.AutoSize  = true;
            _titleLabel.Location  = new Point(16, 8);

            _subtitleLabel        = new Label();
            _subtitleLabel.Font   = new Font("Segoe UI", 8f);
            _subtitleLabel.AutoSize = true;
            _subtitleLabel.Location = new Point(16, 32);

            Button closeBtn       = new Button();
            closeBtn.Text         = "×";
            closeBtn.Font         = new Font("Segoe UI", 14f);
            closeBtn.FlatStyle    = FlatStyle.Flat;
            closeBtn.FlatAppearance.BorderSize = 0;
            closeBtn.Size         = new Size(32, 32);
            closeBtn.Anchor       = AnchorStyles.Top | AnchorStyles.Right;
            closeBtn.Cursor       = Cursors.Hand;
            closeBtn.DialogResult = DialogResult.Cancel;
            closeBtn.Click       += delegate { this.DialogResult = DialogResult.Cancel; this.Close(); };

            _headerPanel.Controls.Add(_titleLabel);
            _headerPanel.Controls.Add(_subtitleLabel);
            _headerPanel.Controls.Add(closeBtn);

            _headerPanel.Layout += delegate
            {
                closeBtn.Location = new Point(_headerPanel.Width - closeBtn.Width - 6, 12);
            };

            _bodyPanel          = new Panel();
            _bodyPanel.Dock     = DockStyle.Fill;
            _bodyPanel.Padding  = new Padding(16, 8, 16, 0);

            _inputLabel         = new Label();
            _inputLabel.Text    = "Line number:";
            _inputLabel.Font    = new Font("Segoe UI", 9f);
            _inputLabel.AutoSize = true;
            _inputLabel.Location = new Point(16, 8);

            _lineInput          = new NumericUpDown();
            _lineInput.Font     = new Font("Consolas", 13f, FontStyle.Bold);
            _lineInput.Minimum  = 1;
            _lineInput.Maximum  = _totalLines;
            _lineInput.Value    = _currentLine;
            _lineInput.Width    = 120;
            _lineInput.Height   = 32;
            _lineInput.Location = new Point(16, 28);
            _lineInput.BorderStyle = BorderStyle.FixedSingle;
            _lineInput.ValueChanged += delegate { UpdatePreview((int)_lineInput.Value); };

            Label ofLabel       = new Label();
            ofLabel.Font        = new Font("Segoe UI", 9f);
            ofLabel.Text        = string.Format("of {0} lines", _totalLines);
            ofLabel.AutoSize    = true;
            ofLabel.Location    = new Point(146, 38);

            _sliderPanel        = new Panel();
            _sliderPanel.Height = 22;
            _sliderPanel.Location = new Point(16, 70);
            _sliderPanel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            _sliderPanel.Cursor = Cursors.Hand;
            _sliderPanel.Paint += OnSliderPaint;
            _sliderPanel.MouseDown += OnSliderMouseDown;
            _sliderPanel.MouseMove += OnSliderMouseMove;
            _sliderPanel.MouseUp   += delegate { _dragging = false; };

            _bodyPanel.Layout += delegate
            {
                _sliderPanel.Width = _bodyPanel.ClientSize.Width - 32;
                _previewPanel.Width = _bodyPanel.ClientSize.Width - 32;
            };

            _previewLabel       = new Label();
            _previewLabel.Font  = new Font("Segoe UI", 8f);
            _previewLabel.Text  = "Preview:";
            _previewLabel.AutoSize = true;
            _previewLabel.Location = new Point(16, 100);

            _previewPanel       = new Panel();
            _previewPanel.Location = new Point(16, 118);
            _previewPanel.Height = 36;
            _previewPanel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            _previewPanel.BorderStyle = BorderStyle.None;
            _previewPanel.Paint += OnPreviewPanelPaint;

            _previewLineLabel   = new Label();
            _previewLineLabel.Font = new Font("Consolas", 9f);
            _previewLineLabel.AutoSize = false;
            _previewLineLabel.Dock = DockStyle.Fill;
            _previewLineLabel.TextAlign = ContentAlignment.MiddleLeft;
            _previewLineLabel.Padding = new Padding(10, 0, 4, 0);
            _previewPanel.Controls.Add(_previewLineLabel);

            _bodyPanel.Controls.Add(_inputLabel);
            _bodyPanel.Controls.Add(_lineInput);
            _bodyPanel.Controls.Add(ofLabel);
            _bodyPanel.Controls.Add(_sliderPanel);
            _bodyPanel.Controls.Add(_previewLabel);
            _bodyPanel.Controls.Add(_previewPanel);

            _buttonPanel        = new Panel();
            _buttonPanel.Dock   = DockStyle.Bottom;
            _buttonPanel.Height = 60;

            _goButton           = new Button();
            _goButton.Text      = "Go";
            _goButton.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);
            _goButton.Size      = new Size(100, 36);
            _goButton.FlatStyle = FlatStyle.Flat;
            _goButton.FlatAppearance.BorderSize = 0;
            _goButton.Anchor    = AnchorStyles.Right | AnchorStyles.Top;
            _goButton.Cursor    = Cursors.Hand;
            _goButton.DialogResult = DialogResult.OK;
            _goButton.Click    += OnGoClicked;

            _cancelButton       = new Button();
            _cancelButton.Text  = "Cancel";
            _cancelButton.Font  = new Font("Segoe UI", 9f);
            _cancelButton.Size  = new Size(100, 36);
            _cancelButton.FlatStyle = FlatStyle.Flat;
            _cancelButton.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            _cancelButton.Cursor = Cursors.Hand;
            _cancelButton.DialogResult = DialogResult.Cancel;

            _buttonPanel.Controls.Add(_goButton);
            _buttonPanel.Controls.Add(_cancelButton);

            _buttonPanel.Layout += delegate
            {
                int panelW = _buttonPanel.ClientSize.Width;
                int btnY   = (_buttonPanel.Height - _goButton.Height) / 2;
                _cancelButton.Location = new Point(panelW - _cancelButton.Width - 16, btnY);
                _goButton.Location     = new Point(panelW - _cancelButton.Width - _goButton.Width - 24, btnY);
            };

            this.Controls.Add(_bodyPanel);
            this.Controls.Add(_buttonPanel);
            this.Controls.Add(_headerPanel);

            this.AcceptButton = _goButton;
            this.CancelButton = _cancelButton;

            _headerPanel.MouseDown += delegate(object s, MouseEventArgs ev)
            {
                if (ev.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(this.Handle, 0xA1, new IntPtr(0x2), IntPtr.Zero);
                }
            };
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        private void ApplyTheme()
        {
            if (_theme == null) return;

            Color bg      = _theme.BackgroundColor;
            Color fg      = _theme.DefaultTextColor;
            Color lineBg  = _theme.LineNumberBackColor;
            Color lineFg  = _theme.LineNumberForeColor;
            Color selBg   = _theme.SelectionColor;

            Color accent  = Color.FromArgb(86, 156, 214); 

            this.BackColor = bg;

            _headerPanel.BackColor = lineBg;
            _titleLabel.ForeColor  = fg;
            _titleLabel.BackColor  = Color.Transparent;
            _subtitleLabel.Text    = string.Format("Current line: {0}  •  Total: {1}", _currentLine, _totalLines);
            _subtitleLabel.ForeColor = lineFg;
            _subtitleLabel.BackColor = Color.Transparent;

            _bodyPanel.BackColor   = bg;
            _inputLabel.ForeColor  = lineFg;
            _inputLabel.BackColor  = Color.Transparent;

            _lineInput.BackColor   = lineBg;
            _lineInput.ForeColor   = fg;

            foreach (Control c in _bodyPanel.Controls)
            {
                Label l = c as Label;
                if (l != null && l != _inputLabel)
                {
                    l.ForeColor = lineFg;
                    l.BackColor = Color.Transparent;
                }
            }

            _previewPanel.BackColor = lineBg;
            _previewLineLabel.ForeColor = fg;
            _previewLineLabel.BackColor = Color.Transparent;

            _buttonPanel.BackColor = lineBg;

            _goButton.BackColor         = accent;
            _goButton.ForeColor         = Color.White;
            _goButton.FlatAppearance.BorderColor = accent;

            _cancelButton.BackColor     = bg;
            _cancelButton.ForeColor     = fg;
            _cancelButton.FlatAppearance.BorderColor = lineFg;
            _cancelButton.FlatAppearance.BorderSize  = 1;

            foreach (Control c in _headerPanel.Controls)
            {
                Button b = c as Button;
                if (b != null && b.Text == "×")
                {
                    b.BackColor = Color.Transparent;
                    b.ForeColor = lineFg;
                    b.FlatAppearance.MouseOverBackColor = Color.FromArgb(
                        Math.Min(lineBg.R + 30, 255),
                        Math.Min(lineBg.G + 30, 255),
                        Math.Min(lineBg.B + 30, 255));
                }
            }

            this.Invalidate(true);
        }

        private void UpdatePreview(int lineNum)
        {
            SelectedLine = lineNum;

            if (_editor == null) return;
            int idx = lineNum - 1;
            string[] lines = _editor.Lines;
            if (idx >= 0 && idx < lines.Length)
            {
                string text = lines[idx].Trim();
                if (text.Length > 60) text = text.Substring(0, 57) + "...";
                _previewLineLabel.Text = string.IsNullOrEmpty(text) ? "(empty line)" : text;
            }
            else
            {
                _previewLineLabel.Text = "(empty line)";
            }

            if (_sliderPanel != null) _sliderPanel.Invalidate();
        }

        private void OnGoClicked(object sender, EventArgs e)
        {
            SelectedLine = (int)_lineInput.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void OnPreviewPanelPaint(object sender, PaintEventArgs e)
        {
            Color accent = Color.FromArgb(86, 156, 214);
            using (SolidBrush ab = new SolidBrush(accent))
                e.Graphics.FillRectangle(ab, 0, 0, 4, _previewPanel.Height);
        }

        private void OnSliderPaint(object sender, PaintEventArgs e)
        {
            if (_theme == null) return;
            Graphics g = e.Graphics;
            int w = _sliderPanel.Width;
            int h = _sliderPanel.Height;
            int cy = h / 2;

            Color trackColor = _theme.LineNumberBorderColor;
            Color thumbColor = Color.FromArgb(86, 156, 214);

            using (SolidBrush tb = new SolidBrush(trackColor))
                g.FillRectangle(tb, 0, cy - 2, w, 4);

            float pct = (_totalLines > 1) ? (float)(_lineInput.Value - 1) / (_totalLines - 1) : 0f;
            int tx = (int)(pct * (w - 10));

            using (SolidBrush fb = new SolidBrush(thumbColor))
                g.FillRectangle(fb, 0, cy - 2, tx + 5, 4);

            int r = 7;
            using (SolidBrush cb = new SolidBrush(thumbColor))
                g.FillEllipse(cb, tx, cy - r, r * 2, r * 2);
        }

        private void OnSliderMouseDown(object sender, MouseEventArgs e)
        {
            _dragging   = true;
            _dragStartX = e.X;
            UpdateSliderFromX(e.X);
        }

        private void OnSliderMouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging) UpdateSliderFromX(e.X);
        }

        private void UpdateSliderFromX(int x)
        {
            float pct = (float)x / _sliderPanel.Width;
            pct = Math.Max(0f, Math.Min(1f, pct));
            int val = 1 + (int)(pct * (_totalLines - 1));
            _lineInput.Value = val;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Color border = _theme != null ? _theme.LineNumberBorderColor : Color.Gray;
            using (Pen p = new Pen(border))
                e.Graphics.DrawRectangle(p, 0, 0, this.Width - 1, this.Height - 1);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "GoToLineDialog";
            this.Load += new System.EventHandler(this.GoToLineDialog_Load);
            this.ResumeLayout(false);

        }

        private void GoToLineDialog_Load(object sender, EventArgs e)
        {

        }
    }
}

// =============================================================================
//  Minimap Panel for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides a visual overview of the editor content using a compact
//  minimap representation similar to modern code editors.
//
//  Features:
//  - Real-time code overview rendering
//  - Syntax-inspired minimap coloring
//  - Viewport highlight indicator
//  - Click and drag navigation
//  - Cached rendering for better performance
//  - Theme-aware appearance
//  - Automatic editor synchronization
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
//  TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
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
    internal class MinimapPanel : Panel
    {
      
        private RichTextBox _editor;
        private EditorTheme _theme;
        private Bitmap   _cache;
        private bool     _cacheDirty = true;
        private string   _cachedText = null;
        private int      _cachedThemeHash;
        private const int LineHeight  = 3;  
        private const int CharWidth   = 1;  
        private const int PaddingLeft = 4;
        private const int MaxChars    = 120; 
        private bool _dragging;
        private int  _dragStartY;
        private int  _dragStartScrollLine;

        public RichTextBox Editor
        {
            get { return _editor; }
            set
            {
                _editor = value;
                if (_editor != null)
                {
                    _editor.VScroll     += delegate { Invalidate(); };
                    _editor.TextChanged += delegate { _cacheDirty = true; Invalidate(); };
                }
            }
        }

        public EditorTheme Theme
        {
            get { return _theme; }
            set { _theme = value; _cacheDirty = true; Invalidate(); }
        }

        public MinimapPanel()
        {
            this.DoubleBuffered = true;
            this.Cursor         = Cursors.Hand;
            this.Width          = 140;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_editor == null || _theme == null)
            {
                e.Graphics.Clear(this.BackColor);
                return;
            }

            RebuildCacheIfNeeded();

            if (_cache != null)
                e.Graphics.DrawImage(_cache, 0, 0);

            DrawViewportHighlight(e.Graphics);
            DrawBorderLine(e.Graphics);
        }

        private void RebuildCacheIfNeeded()
        {
            string currentText  = _editor != null ? _editor.Text : string.Empty;
            int    themeHash    = _theme  != null ? _theme.BackgroundColor.ToArgb() ^ _theme.DefaultTextColor.ToArgb() : 0;

            if (!_cacheDirty && _cache != null
                && currentText == _cachedText
                && themeHash == _cachedThemeHash)
                return;

            _cachedText      = currentText;
            _cachedThemeHash = themeHash;
            _cacheDirty      = false;

            if (_cache != null) { _cache.Dispose(); _cache = null; }
            if (string.IsNullOrEmpty(currentText)) return;

            int w = this.Width;
            int h = this.Height;
            if (w <= 0 || h <= 0) return;

            _cache = new Bitmap(w, h);
            using (Graphics g = Graphics.FromImage(_cache))
            {
                Color bg   = _theme.BackgroundColor;
                Color fg   = _theme.DefaultTextColor;

                Color mapBg = DarkenOrLightenBy(bg, 12);
                g.Clear(mapBg);

                string[] lines = currentText.Split('\n');
                int totalLines = lines.Length;

                for (int i = 0; i < totalLines; i++)
                {
                    int y = i * LineHeight;
                    if (y + LineHeight > h) break;

                    string line = lines[i].TrimEnd('\r');
                    if (line.Length == 0) continue;

                    int indent = 0;
                    foreach (char c in line)
                    {
                        if (c == ' ')       indent++;
                        else if (c == '\t') indent += 4;
                        else break;
                    }

                    int contentStart = PaddingLeft + (indent * CharWidth / 4);
                    int contentLen   = Math.Min(line.TrimStart().Length, MaxChars);

                    if (contentLen <= 0) continue;

                    int barW = Math.Min(contentLen * CharWidth, w - contentStart - PaddingLeft);
                    if (barW <= 0) continue;

                    string trimmed = line.TrimStart();

                   
                    Color lineColor = Color.FromArgb(130, fg.R, fg.G, fg.B);
                   

                    if (trimmed.StartsWith("//") || trimmed.StartsWith("#!")
                        || trimmed.StartsWith("/*") || trimmed.StartsWith("*")
                        || trimmed.StartsWith("--") || trimmed.StartsWith("'"))
                        lineColor = Color.FromArgb(120, 106, 153, 106);  

                    
                    else if (trimmed.StartsWith("using ") || trimmed.StartsWith("import ")
                             || trimmed.StartsWith("#include") || trimmed.StartsWith("#define")
                             || trimmed.StartsWith("#pragma") || trimmed.StartsWith("require")
                             || trimmed.StartsWith("from "))
                        lineColor = Color.FromArgb(180, 86, 182, 194);  

                    else if (trimmed.StartsWith("class ") || trimmed.StartsWith("interface ")
                             || trimmed.StartsWith("struct ") || trimmed.StartsWith("enum ")
                             || trimmed.StartsWith("trait ") || trimmed.StartsWith("type "))
                        lineColor = Color.FromArgb(200, 78, 201, 176); 

                    else if (trimmed.StartsWith("public ") || trimmed.StartsWith("private ")
                             || trimmed.StartsWith("protected ") || trimmed.StartsWith("internal ")
                             || trimmed.StartsWith("static ") || trimmed.StartsWith("abstract ")
                             || trimmed.StartsWith("override ") || trimmed.StartsWith("virtual "))
                        lineColor = Color.FromArgb(190, 86, 156, 214);   

                    else if (trimmed.StartsWith("if ") || trimmed.StartsWith("if(")
                             || trimmed.StartsWith("else") || trimmed.StartsWith("for ")
                             || trimmed.StartsWith("for(") || trimmed.StartsWith("foreach")
                             || trimmed.StartsWith("while") || trimmed.StartsWith("switch")
                             || trimmed.StartsWith("return") || trimmed.StartsWith("try")
                             || trimmed.StartsWith("catch") || trimmed.StartsWith("finally")
                             || trimmed.StartsWith("throw") || trimmed.StartsWith("break")
                             || trimmed.StartsWith("continue"))
                        lineColor = Color.FromArgb(190, 216, 160, 223);  

                    else if (trimmed.StartsWith("\"") || trimmed.StartsWith("'")
                             || trimmed.StartsWith("@\"") || trimmed.StartsWith("$\""))
                        lineColor = Color.FromArgb(170, 214, 157, 133);  

                    else if (trimmed.StartsWith("[") || trimmed.StartsWith("@"))
                        lineColor = Color.FromArgb(160, 220, 220, 170);  

                    else if (trimmed.StartsWith("}") || trimmed.StartsWith("{"))
                        lineColor = Color.FromArgb(100, fg.R, fg.G, fg.B);  

                    else if (indent >= 12)
                        lineColor = Color.FromArgb(140, fg.R, fg.G, fg.B);

                    using (SolidBrush b = new SolidBrush(lineColor))
                        g.FillRectangle(b, contentStart, y, barW, Math.Max(1, LineHeight - 1));
                }
            }
        }

        private void DrawViewportHighlight(Graphics g)
        {
            if (_editor == null) return;

            int totalLines = Math.Max(1, _editor.Lines.Length);
            int h          = this.Height;
            int w          = this.Width;

            int firstVisible  = GetFirstVisibleLine();
            int visibleCount  = GetVisibleLineCount();

            float scale = (float)(totalLines * LineHeight);
            if (scale <= 0) return;

            int vpTop    = (int)((float)firstVisible / totalLines * h);
            int vpHeight = Math.Max(6, (int)((float)visibleCount / totalLines * h));

            vpTop = Math.Max(0, Math.Min(vpTop, h - vpHeight));

            using (SolidBrush vb = new SolidBrush(Color.FromArgb(55, 255, 255, 255)))
                g.FillRectangle(vb, 0, vpTop, w, vpHeight);

            using (Pen vp = new Pen(Color.FromArgb(160, 255, 255, 255), 1))
            {
                g.DrawLine(vp, 0, vpTop,              w, vpTop);
                g.DrawLine(vp, 0, vpTop + vpHeight,   w, vpTop + vpHeight);
            }

            using (Pen lp = new Pen(Color.FromArgb(200, 86, 156, 214), 2))
            {
                g.DrawLine(lp, 1, vpTop, 1, vpTop + vpHeight);
            }
        }

        private void DrawBorderLine(Graphics g)
        {
            if (_theme == null) return;
            Color borderColor = _theme.LineNumberBorderColor;
            using (Pen p = new Pen(borderColor, 1))
                g.DrawLine(p, 0, 0, 0, this.Height);
        }

        private int GetFirstVisibleLine()
        {
            if (_editor == null) return 0;
            try
            {
                Point topLeft = new Point(0, 0);
                int charIdx = _editor.GetCharIndexFromPosition(topLeft);
                return _editor.GetLineFromCharIndex(charIdx);
            }
            catch { return 0; }
        }

        private int GetVisibleLineCount()
        {
            if (_editor == null) return 0;
            try
            {
                int fontH = _editor.Font.Height;
                if (fontH <= 0) return 0;
                return _editor.ClientSize.Height / fontH + 1;
            }
            catch { return 0; }
        }

        private void ScrollEditorToLine(int targetLine)
        {
            if (_editor == null) return;
            int totalLines = Math.Max(1, _editor.Lines.Length);
            targetLine = Math.Max(0, Math.Min(targetLine, totalLines - 1));

            int charIdx = _editor.GetFirstCharIndexFromLine(targetLine);
            if (charIdx < 0) charIdx = 0;

            _editor.SelectionStart  = charIdx;
            _editor.SelectionLength = 0;
            _editor.ScrollToCaret();
            _editor.Focus();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                _dragging            = true;
                _dragStartY          = e.Y;
                _dragStartScrollLine = GetFirstVisibleLine();
                NavigateToY(e.Y);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_dragging && e.Button == MouseButtons.Left)
                NavigateToY(e.Y);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _dragging = false;
        }

        private void NavigateToY(int y)
        {
            if (_editor == null) return;
            int totalLines = Math.Max(1, _editor.Lines.Length);
            int h          = Math.Max(1, this.Height);
            float pct      = (float)y / h;
            pct = Math.Max(0f, Math.Min(1f, pct));
            int targetLine = (int)(pct * totalLines);
            ScrollEditorToLine(targetLine);
        }

        public void InvalidateCache()
        {
            _cacheDirty = true;
            Invalidate();
        }

        private static Color DarkenOrLightenBy(Color c, int amount)
        {
            float brightness = c.GetBrightness();
            if (brightness < 0.5f)
            {
                return Color.FromArgb(
                    c.A,
                    Math.Min(255, c.R + amount),
                    Math.Min(255, c.G + amount),
                    Math.Min(255, c.B + amount));
            }
            else
            {
                return Color.FromArgb(
                    c.A,
                    Math.Max(0, c.R - amount),
                    Math.Max(0, c.G - amount),
                    Math.Max(0, c.B - amount));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _cache != null)
            {
                _cache.Dispose();
                _cache = null;
            }
            base.Dispose(disposing);
        }
    }
}

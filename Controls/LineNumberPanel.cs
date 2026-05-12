using System;
using System.Drawing;
using System.Windows.Forms;
using CodeEditorLib.Themes;

namespace CodeEditorLib.Controls
{
    public class LineNumberPanel : Control
    {
        private RichTextBox _editor;
        private EditorTheme _theme;
        private Font _lineFont;
        private int _currentLine;
        private bool _showLineNumbers;
        private int _padRight;

        public LineNumberPanel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            _showLineNumbers = true;
            _padRight = 6;
            _currentLine = 1;
        }

        public RichTextBox Editor
        {
            get { return _editor; }
            set { _editor = value; Invalidate(); }
        }

        public EditorTheme Theme
        {
            get { return _theme; }
            set { _theme = value; Invalidate(); }
        }

        public Font LineFont
        {
            get { return _lineFont; }
            set { _lineFont = value; Invalidate(); }
        }

        public bool ShowLineNumbers
        {
            get { return _showLineNumbers; }
            set { _showLineNumbers = value; Invalidate(); }
        }

        public int CurrentLine
        {
            get { return _currentLine; }
            set
            {
                if (_currentLine != value)
                {
                    _currentLine = value;
                    Invalidate();
                }
            }
        }

        public int ComputeWidth(int lineCount)
        {
            if (!_showLineNumbers) return 0;
            Font f = _lineFont ?? this.Font;
            using (Graphics g = CreateGraphics())
            {
                string sample = lineCount.ToString() + "0";
                SizeF sz = g.MeasureString(sample, f);
                return (int)sz.Width + _padRight * 2;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Color backColor = _theme != null ? _theme.LineNumberBackColor : SystemColors.Control;
            Color foreColor = _theme != null ? _theme.LineNumberForeColor : SystemColors.ControlText;
            Color borderColor = _theme != null ? _theme.LineNumberBorderColor : SystemColors.ControlDark;
            Color currentBack = _theme != null ? _theme.CurrentLineColor : Color.LightYellow;

            g.Clear(backColor);

            using (Pen borderPen = new Pen(borderColor))
                g.DrawLine(borderPen, Width - 1, 0, Width - 1, Height);

            if (!_showLineNumbers || _editor == null) return;

            Font font = _lineFont ?? this.Font;
            float lineHeight = font.GetHeight(g);

            int firstChar = _editor.GetCharIndexFromPosition(new Point(0, 0));
            int firstLine = _editor.GetLineFromCharIndex(firstChar);
            int lastChar = _editor.GetCharIndexFromPosition(new Point(0, _editor.Height - 1));
            int lastLine = _editor.GetLineFromCharIndex(lastChar);

            int totalLines = _editor.Lines.Length;

            for (int lineNum = firstLine; lineNum <= lastLine && lineNum < totalLines; lineNum++)
            {
                int charIndex = _editor.GetFirstCharIndexFromLine(lineNum);
                if (charIndex < 0) continue;
                Point pos = _editor.GetPositionFromCharIndex(charIndex);

                float y = pos.Y;

                if (lineNum + 1 == _currentLine)
                {
                    using (SolidBrush hlBrush = new SolidBrush(currentBack))
                        g.FillRectangle(hlBrush, 0, y, Width - 1, lineHeight);
                }

                string lineStr = (lineNum + 1).ToString();
                SizeF textSize = g.MeasureString(lineStr, font);
                float x = Width - textSize.Width - _padRight;

                using (SolidBrush textBrush = new SolidBrush(foreColor))
                    g.DrawString(lineStr, font, textBrush, x, y);
            }
        }
    }
}

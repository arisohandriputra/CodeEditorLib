using System;
using System.Drawing;

namespace CodeEditorLib.Highlighting
{
    public class HighlightStyle
    {
        private Color _foreColor;
        private Color _backColor;
        private bool _bold;
        private bool _italic;
        private bool _underline;

        public HighlightStyle(Color foreColor)
            : this(foreColor, Color.Empty, false, false, false) { }

        public HighlightStyle(Color foreColor, bool bold)
            : this(foreColor, Color.Empty, bold, false, false) { }

        public HighlightStyle(Color foreColor, Color backColor, bool bold, bool italic, bool underline)
        {
            _foreColor = foreColor;
            _backColor = backColor;
            _bold = bold;
            _italic = italic;
            _underline = underline;
        }

        public Color ForeColor { get { return _foreColor; } set { _foreColor = value; } }

        public Color BackColor { get { return _backColor; } set { _backColor = value; } }

        public bool Bold { get { return _bold; } set { _bold = value; } }

        public bool Italic { get { return _italic; } set { _italic = value; } }

        public bool Underline { get { return _underline; } set { _underline = value; } }

        public Font CreateFont(Font baseFont)
        {
            FontStyle style = FontStyle.Regular;
            if (_bold) style |= FontStyle.Bold;
            if (_italic) style |= FontStyle.Italic;
            if (_underline) style |= FontStyle.Underline;
            return new Font(baseFont, style);
        }

        public static readonly HighlightStyle Default = new HighlightStyle(SystemColors.WindowText);
    }
}

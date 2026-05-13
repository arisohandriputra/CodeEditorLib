// =============================================================================
//  Syntax Highlight Style System for CodeEditorLib
// =============================================================================
//
//  Description :
//  Defines visual styling information used by the syntax
//  highlighting engine for rendering editor tokens.
//
//  This file is responsible for:
//  - Managing token foreground colors
//  - Managing token background colors
//  - Supporting bold text rendering
//  - Supporting italic text rendering
//  - Supporting underline text rendering
//  - Creating styled fonts dynamically
//
// -----------------------------------------------------------------------------
//
//  HighlightStyle Class :
//  Represents the visual appearance of a syntax token
//  inside the editor rendering system.
//
//  A style can define:
//  - Text color
//  - Background color
//  - Bold formatting
//  - Italic formatting
//  - Underline formatting
//
// -----------------------------------------------------------------------------
//
//  Example Styling:
//  - Keywords       -> Blue + Bold
//  - Strings        -> Green
//  - Comments       -> Gray + Italic
//  - Errors         -> Red + Underline
//  - Selected Text  -> Custom background color
//
// -----------------------------------------------------------------------------
//
//  Rendering Features:
//  - Dynamic font style generation
//  - Lightweight visual formatting
//  - Compatible with WinForms rendering
//  - Supports custom editor themes
//  - Reusable style definitions
//
// -----------------------------------------------------------------------------
//
//  Internal Logic:
//  - Uses System.Drawing.Color for color management
//  - Uses FontStyle flags for text formatting
//  - Combines multiple styles into a single font
//
// -----------------------------------------------------------------------------
//
//  Default Style:
//  Provides a fallback editor style using the system's
//  default window text color when no custom style exists.
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - Syntax highlighting engine
//  - Editor theme systems
//  - Token rendering pipeline
//  - Custom language formatting
//  - Dark/Light mode support
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

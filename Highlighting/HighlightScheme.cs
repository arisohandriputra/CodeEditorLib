// =============================================================================
//  Syntax Highlight Scheme System for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides syntax highlight theme management used by the
//  editor for controlling token appearance and styling.
//
//  This file is responsible for:
//  - Mapping token types to visual styles
//  - Managing syntax color themes
//  - Providing style lookup functionality
//  - Supporting customizable editor highlighting
//  - Handling default style fallbacks
//
// -----------------------------------------------------------------------------
//
//  HighlightScheme Class :
//  Represents a complete syntax highlighting scheme
//  used by the editor rendering engine.
//
//  A highlight scheme defines how different token
//  types should visually appear inside the editor.
//
// -----------------------------------------------------------------------------
//
//  Supported Styling Examples:
//  - Keywords       -> Blue bold text
//  - Strings        -> Green text
//  - Comments       -> Gray italic text
//  - Numbers        -> Orange text
//  - Operators      -> White text
//  - HTML Tags      -> Cyan text
//  - SQL Keywords   -> Purple bold text
//
// -----------------------------------------------------------------------------
//
//  Internal Structure:
//  - Uses a dictionary-based token/style mapping
//  - TokenType acts as the lookup key
//  - HighlightStyle stores visual formatting data
//
// -----------------------------------------------------------------------------
//
//  Features:
//  - Fast token style lookup
//  - Dynamic style assignment
//  - Custom editor theme support
//  - Default fallback style handling
//  - Lightweight rendering integration
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - Syntax highlighting engine
//  - Theme switching systems
//  - Dark/Light editor modes
//  - Custom language styling
//  - User-defined color schemes
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
using System.Collections.Generic;
using CodeEditorLib.Core;

namespace CodeEditorLib.Highlighting
{
    public class HighlightScheme
    {
        private Dictionary<TokenType, HighlightStyle> _styles;
        private string _name;

        public HighlightScheme(string name)
        {
            _name = name;
            _styles = new Dictionary<TokenType, HighlightStyle>();
        }

        public string Name { get { return _name; } }

        public void SetStyle(TokenType tokenType, HighlightStyle style)
        {
            _styles[tokenType] = style;
        }

        public HighlightStyle GetStyle(TokenType tokenType)
        {
            HighlightStyle style;
            if (_styles.TryGetValue(tokenType, out style))
                return style;
            return HighlightStyle.Default;
        }

        public bool HasStyle(TokenType tokenType)
        {
            return _styles.ContainsKey(tokenType);
        }
    }
}

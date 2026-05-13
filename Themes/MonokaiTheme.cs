// =============================================================================
//  Monokai Theme Definition for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides a Monokai-style dark theme configuration used by the editor
//  syntax highlighting and UI rendering system.
//
//  This file is responsible for:
//  - Defining Monokai theme metadata
//  - Configuring editor UI colors (background, selection, caret, etc.)
//  - Styling line numbers and active line highlighting
//  - Defining syntax highlighting color scheme
//  - Providing consistent visual styling across supported languages
//  - Mapping token types to Monokai color palette
//
// -----------------------------------------------------------------------------
//
//  Theme Characteristics:
//  - Dark background optimized for long coding sessions
//  - High contrast syntax highlighting
//  - Vibrant keyword and string colors
//  - Soft muted comment styling
//  - Distinct color separation for code elements
//
// -----------------------------------------------------------------------------
//
//  UI Color Configuration:
//  - Background color        -> Dark gray (#272822)
//  - Default text color      -> Light gray (#F8F8F2)
//  - Selection color         -> Subtle highlight (#49483E)
//  - Current line highlight  -> Slightly lighter dark tone
//  - Line number styling     -> Muted gray tones
//  - Caret color             -> Bright white
//  - Bracket match highlight -> Dark yellow tint
//  - Autocomplete popup      -> Dark themed panel
//
// -----------------------------------------------------------------------------
//
//  Syntax Highlighting Features:
//  - Keywords        -> Bright pink/red (Monokai signature)
//  - Strings         -> Soft yellow
//  - Comments        -> Muted gray-green
//  - Numbers         -> Purple tones
//  - Types           -> Cyan/light blue
//  - Identifiers     -> Green accents
//  - Operators       -> Light text color
//  - Preprocessors   -> Muted gray
//
// -----------------------------------------------------------------------------
//
//  Supported Token Categories:
//  - Language keywords (if, class, return, etc.)
//  - String literals
//  - Comments (single and multi-line)
//  - Numeric values
//  - Data types
//  - Operators and expressions
//  - XML/HTML tags and attributes
//  - CSS selectors and properties
//  - SQL keywords and functions
//
// -----------------------------------------------------------------------------
//
//  Design Philosophy:
//  Monokai theme prioritizes readability, contrast, and reduced eye strain,
//  while maintaining a visually rich coding experience.
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - CodeEditorLib theme system
//  - Code editor UI customization
//  - IDE-like syntax highlighting
//  - Developer-focused applications
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
//  and/or sell copies of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//
//  IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
//  CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
//  TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
//  SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// =============================================================================

using System.Drawing;
using CodeEditorLib.Core;
using CodeEditorLib.Highlighting;

namespace CodeEditorLib.Themes
{
    public class MonokaiTheme : EditorTheme
    {
        public MonokaiTheme() : base("Monokai") { }

        public override Color BackgroundColor       { get { return Color.FromArgb(39, 40, 34); } }
        public override Color DefaultTextColor      { get { return Color.FromArgb(248, 248, 242); } }
        public override Color SelectionColor        { get { return Color.FromArgb(73, 72, 62); } }
        public override Color CurrentLineColor      { get { return Color.FromArgb(50, 50, 45); } }
        public override Color LineNumberBackColor   { get { return Color.FromArgb(30, 31, 26); } }
        public override Color LineNumberForeColor   { get { return Color.FromArgb(117, 113, 94); } }
        public override Color LineNumberBorderColor { get { return Color.FromArgb(60, 60, 52); } }
        public override Color CaretColor            { get { return Color.FromArgb(248, 248, 240); } }
        public override Color BracketMatchColor     { get { return Color.FromArgb(80, 75, 20); } }
        public override Color AutoCompleteBackColor { get { return Color.FromArgb(39, 40, 34); } }
        public override Color AutoCompleteForeColor { get { return Color.FromArgb(248, 248, 242); } }
        public override Color AutoCompleteSelectionColor { get { return Color.FromArgb(102, 217, 239); } }

        protected override void BuildScheme(HighlightScheme scheme)
        {
            scheme.SetStyle(TokenType.Keyword,      new HighlightStyle(Color.FromArgb(249, 38, 114), true));
            scheme.SetStyle(TokenType.String,       new HighlightStyle(Color.FromArgb(230, 219, 116)));
            scheme.SetStyle(TokenType.Comment,      new HighlightStyle(Color.FromArgb(117, 113, 94), Color.Empty, false, true, false));
            scheme.SetStyle(TokenType.Number,       new HighlightStyle(Color.FromArgb(174, 129, 255)));
            scheme.SetStyle(TokenType.Type,         new HighlightStyle(Color.FromArgb(102, 217, 239)));
            scheme.SetStyle(TokenType.Operator,     new HighlightStyle(Color.FromArgb(248, 248, 242)));
            scheme.SetStyle(TokenType.Preprocessor, new HighlightStyle(Color.FromArgb(117, 113, 94)));
            scheme.SetStyle(TokenType.Identifier,   new HighlightStyle(Color.FromArgb(166, 226, 46)));
            scheme.SetStyle(TokenType.Attribute,    new HighlightStyle(Color.FromArgb(102, 217, 239)));
            scheme.SetStyle(TokenType.XmlTag,       new HighlightStyle(Color.FromArgb(249, 38, 114)));
            scheme.SetStyle(TokenType.XmlAttribute, new HighlightStyle(Color.FromArgb(166, 226, 46)));
            scheme.SetStyle(TokenType.XmlValue,     new HighlightStyle(Color.FromArgb(230, 219, 116)));
            scheme.SetStyle(TokenType.HtmlTag,      new HighlightStyle(Color.FromArgb(249, 38, 114)));
            scheme.SetStyle(TokenType.HtmlAttribute,new HighlightStyle(Color.FromArgb(166, 226, 46)));
            scheme.SetStyle(TokenType.HtmlValue,    new HighlightStyle(Color.FromArgb(230, 219, 116)));
            scheme.SetStyle(TokenType.CssSelector,  new HighlightStyle(Color.FromArgb(249, 38, 114)));
            scheme.SetStyle(TokenType.CssProperty,  new HighlightStyle(Color.FromArgb(102, 217, 239)));
            scheme.SetStyle(TokenType.CssValue,     new HighlightStyle(Color.FromArgb(230, 219, 116)));
            scheme.SetStyle(TokenType.SqlKeyword,   new HighlightStyle(Color.FromArgb(249, 38, 114), true));
            scheme.SetStyle(TokenType.SqlFunction,  new HighlightStyle(Color.FromArgb(166, 226, 46)));
        }
    }
}

// =============================================================================
//  Solarized Dark Theme Definition for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides a Solarized Dark theme configuration used by the editor
//  syntax highlighting and UI rendering system.
//
//  This file is responsible for:
//  - Defining Solarized Dark theme metadata
//  - Configuring editor UI colors (background, selection, caret, etc.)
//  - Styling line numbers, gutter, and active line highlight
//  - Defining syntax highlighting color scheme
//  - Mapping token types to Solarized color palette
//  - Ensuring consistent low-contrast dark mode experience
//
// -----------------------------------------------------------------------------
//
//  Theme Characteristics:
//  - Low contrast, eye-friendly dark background
//  - Carefully balanced color palette (Solarized system)
//  - Reduced visual fatigue for long coding sessions
//  - Soft cyan/green/yellow accents
//  - Stable readability across different lighting conditions
//
// -----------------------------------------------------------------------------
//
//  UI Color Configuration:
//  - Background color        -> Deep navy teal (0, 43, 54)
//  - Default text color      -> Muted gray-blue (131, 148, 150)
//  - Selection color         -> Subtle deep highlight (7, 54, 66)
//  - Current line highlight  -> Same tone as selection for consistency
//  - Line number styling     -> Muted slate tones
//  - Caret color             -> Soft light gray
//  - Bracket match highlight -> Subtle background emphasis
//  - Autocomplete popup      -> Matching dark Solarized surface
//
// -----------------------------------------------------------------------------
//
//  Syntax Highlighting Features:
//  - Keywords        -> Green accent (Solarized green)
//  - Strings         -> Cyan/teal tone
//  - Comments        -> Desaturated gray (low emphasis)
//  - Numbers         -> Teal/cyan accent
//  - Types           -> Yellow/gold tone
//  - Identifiers     -> Blue accent
//  - Operators       -> Neutral foreground color
//  - Preprocessors   -> Orange/red accent
//
// -----------------------------------------------------------------------------
//
//  Supported Token Categories:
//  - Language keywords
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
//  The Solarized Dark theme is designed to reduce eye strain by using a
//  scientifically balanced color palette that minimizes harsh contrast
//  while maintaining clear syntax separation.
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - CodeEditorLib theme system
//  - Dark mode IDE environments
//  - Long-duration coding workflows
//  - Developer tooling and editors
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
//  or sell copies of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//
//  IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
//  CLAIM, DAMAGES OR OTHER LIABILITY,
//  WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// =============================================================================

using System.Drawing;
using CodeEditorLib.Core;
using CodeEditorLib.Highlighting;

namespace CodeEditorLib.Themes
{
    public class SolarizedDarkTheme : EditorTheme
    {
        public SolarizedDarkTheme() : base("Solarized Dark") { }

        public override Color BackgroundColor       { get { return Color.FromArgb(0, 43, 54); } }
        public override Color DefaultTextColor      { get { return Color.FromArgb(131, 148, 150); } }
        public override Color SelectionColor        { get { return Color.FromArgb(7, 54, 66); } }
        public override Color CurrentLineColor      { get { return Color.FromArgb(7, 54, 66); } }
        public override Color LineNumberBackColor   { get { return Color.FromArgb(0, 36, 46); } }
        public override Color LineNumberForeColor   { get { return Color.FromArgb(88, 110, 117); } }
        public override Color LineNumberBorderColor { get { return Color.FromArgb(7, 54, 66); } }
        public override Color CaretColor            { get { return Color.FromArgb(131, 148, 150); } }
        public override Color BracketMatchColor     { get { return Color.FromArgb(7, 54, 66); } }
        public override Color AutoCompleteBackColor { get { return Color.FromArgb(0, 43, 54); } }
        public override Color AutoCompleteForeColor { get { return Color.FromArgb(147, 161, 161); } }
        public override Color AutoCompleteSelectionColor { get { return Color.FromArgb(38, 139, 210); } }

        protected override void BuildScheme(HighlightScheme scheme)
        {
            scheme.SetStyle(TokenType.Keyword,      new HighlightStyle(Color.FromArgb(133, 153, 0), true));
            scheme.SetStyle(TokenType.String,       new HighlightStyle(Color.FromArgb(42, 161, 152)));
            scheme.SetStyle(TokenType.Comment,      new HighlightStyle(Color.FromArgb(88, 110, 117), Color.Empty, false, true, false));
            scheme.SetStyle(TokenType.Number,       new HighlightStyle(Color.FromArgb(42, 161, 152)));
            scheme.SetStyle(TokenType.Type,         new HighlightStyle(Color.FromArgb(181, 137, 0)));
            scheme.SetStyle(TokenType.Operator,     new HighlightStyle(Color.FromArgb(147, 161, 161)));
            scheme.SetStyle(TokenType.Preprocessor, new HighlightStyle(Color.FromArgb(203, 75, 22)));
            scheme.SetStyle(TokenType.Identifier,   new HighlightStyle(Color.FromArgb(38, 139, 210)));
            scheme.SetStyle(TokenType.Attribute,    new HighlightStyle(Color.FromArgb(108, 113, 196)));
            scheme.SetStyle(TokenType.XmlTag,       new HighlightStyle(Color.FromArgb(38, 139, 210)));
            scheme.SetStyle(TokenType.XmlAttribute, new HighlightStyle(Color.FromArgb(147, 161, 161)));
            scheme.SetStyle(TokenType.XmlValue,     new HighlightStyle(Color.FromArgb(42, 161, 152)));
            scheme.SetStyle(TokenType.HtmlTag,      new HighlightStyle(Color.FromArgb(38, 139, 210)));
            scheme.SetStyle(TokenType.HtmlAttribute,new HighlightStyle(Color.FromArgb(147, 161, 161)));
            scheme.SetStyle(TokenType.HtmlValue,    new HighlightStyle(Color.FromArgb(42, 161, 152)));
            scheme.SetStyle(TokenType.CssSelector,  new HighlightStyle(Color.FromArgb(203, 75, 22)));
            scheme.SetStyle(TokenType.CssProperty,  new HighlightStyle(Color.FromArgb(38, 139, 210)));
            scheme.SetStyle(TokenType.CssValue,     new HighlightStyle(Color.FromArgb(42, 161, 152)));
            scheme.SetStyle(TokenType.SqlKeyword,   new HighlightStyle(Color.FromArgb(133, 153, 0), true));
            scheme.SetStyle(TokenType.SqlFunction,  new HighlightStyle(Color.FromArgb(108, 113, 196)));
        }
    }
}

// =============================================================================
//  Solarized Light Theme Definition for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides a Solarized Light theme configuration used by the editor
//  syntax highlighting and UI rendering system.
//
//  This file is responsible for:
//  - Defining Solarized Light theme metadata
//  - Configuring editor UI colors (background, selection, caret, etc.)
//  - Styling line numbers, gutter, and active line highlighting
//  - Defining syntax highlighting color scheme
//  - Mapping token types to Solarized palette colors
//  - Ensuring consistent light-mode readability across all languages
//
// -----------------------------------------------------------------------------
//
//  Theme Characteristics:
//  - Soft warm light background for reduced eye strain
//  - Balanced contrast between text and background
//  - Carefully tuned Solarized color system
//  - Reduced harsh whites for comfortable reading
//  - Stable readability in bright environments
//
// -----------------------------------------------------------------------------
//
//  UI Color Configuration:
//  - Background color        -> Warm light base (253, 246, 227)
//  - Default text color      -> Muted dark teal-gray (101, 123, 131)
//  - Selection color         -> Soft beige highlight (238, 232, 213)
//  - Current line highlight  -> Subtle warm overlay
//  - Line number styling     -> Muted gray-green tones
//  - Caret color             -> Dark readable tone
//  - Bracket match highlight -> Soft background emphasis
//  - Autocomplete popup      -> Light Solarized surface
//
// -----------------------------------------------------------------------------
//
//  Syntax Highlighting Features:
//  - Keywords        -> Green accent tone
//  - Strings         -> Cyan/teal tone
//  - Comments        -> Desaturated gray (low emphasis)
//  - Numbers         -> Cyan/teal accent
//  - Types           -> Yellow/gold tone
//  - Identifiers     -> Blue accent
//  - Operators       -> Muted foreground tone
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
//  The Solarized Light theme is designed to provide a soft, eye-friendly
//  light mode experience using a scientifically balanced palette that avoids
//  pure white backgrounds and overly harsh contrasts.
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - CodeEditorLib theme system
//  - Light mode IDE environments
//  - Documentation and editing workflows
//  - Developer tooling and UI editors
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
//  WHETHER IN AN ACTION OF CONTRACT,
//  TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// =============================================================================

using System.Drawing;
using CodeEditorLib.Core;
using CodeEditorLib.Highlighting;

namespace CodeEditorLib.Themes
{
    public class SolarizedLightTheme : EditorTheme
    {
        public SolarizedLightTheme() : base("Solarized Light") { }

        public override Color BackgroundColor       { get { return Color.FromArgb(253, 246, 227); } }
        public override Color DefaultTextColor      { get { return Color.FromArgb(101, 123, 131); } }
        public override Color SelectionColor        { get { return Color.FromArgb(238, 232, 213); } }
        public override Color CurrentLineColor      { get { return Color.FromArgb(238, 232, 213); } }
        public override Color LineNumberBackColor   { get { return Color.FromArgb(238, 232, 213); } }
        public override Color LineNumberForeColor   { get { return Color.FromArgb(147, 161, 161); } }
        public override Color LineNumberBorderColor { get { return Color.FromArgb(210, 203, 185); } }
        public override Color CaretColor            { get { return Color.FromArgb(101, 123, 131); } }
        public override Color BracketMatchColor     { get { return Color.FromArgb(238, 232, 213); } }
        public override Color AutoCompleteBackColor { get { return Color.FromArgb(253, 246, 227); } }
        public override Color AutoCompleteForeColor { get { return Color.FromArgb(101, 123, 131); } }
        public override Color AutoCompleteSelectionColor { get { return Color.FromArgb(38, 139, 210); } }

        protected override void BuildScheme(HighlightScheme scheme)
        {
            scheme.SetStyle(TokenType.Keyword,      new HighlightStyle(Color.FromArgb(133, 153, 0), true));
            scheme.SetStyle(TokenType.String,       new HighlightStyle(Color.FromArgb(42, 161, 152)));
            scheme.SetStyle(TokenType.Comment,      new HighlightStyle(Color.FromArgb(147, 161, 161), Color.Empty, false, true, false));
            scheme.SetStyle(TokenType.Number,       new HighlightStyle(Color.FromArgb(42, 161, 152)));
            scheme.SetStyle(TokenType.Type,         new HighlightStyle(Color.FromArgb(181, 137, 0)));
            scheme.SetStyle(TokenType.Operator,     new HighlightStyle(Color.FromArgb(101, 123, 131)));
            scheme.SetStyle(TokenType.Preprocessor, new HighlightStyle(Color.FromArgb(203, 75, 22)));
            scheme.SetStyle(TokenType.Identifier,   new HighlightStyle(Color.FromArgb(38, 139, 210)));
            scheme.SetStyle(TokenType.Attribute,    new HighlightStyle(Color.FromArgb(108, 113, 196)));
            scheme.SetStyle(TokenType.XmlTag,       new HighlightStyle(Color.FromArgb(38, 139, 210)));
            scheme.SetStyle(TokenType.XmlAttribute, new HighlightStyle(Color.FromArgb(101, 123, 131)));
            scheme.SetStyle(TokenType.XmlValue,     new HighlightStyle(Color.FromArgb(42, 161, 152)));
            scheme.SetStyle(TokenType.HtmlTag,      new HighlightStyle(Color.FromArgb(38, 139, 210)));
            scheme.SetStyle(TokenType.HtmlAttribute,new HighlightStyle(Color.FromArgb(101, 123, 131)));
            scheme.SetStyle(TokenType.HtmlValue,    new HighlightStyle(Color.FromArgb(42, 161, 152)));
            scheme.SetStyle(TokenType.CssSelector,  new HighlightStyle(Color.FromArgb(203, 75, 22)));
            scheme.SetStyle(TokenType.CssProperty,  new HighlightStyle(Color.FromArgb(38, 139, 210)));
            scheme.SetStyle(TokenType.CssValue,     new HighlightStyle(Color.FromArgb(42, 161, 152)));
            scheme.SetStyle(TokenType.SqlKeyword,   new HighlightStyle(Color.FromArgb(133, 153, 0), true));
            scheme.SetStyle(TokenType.SqlFunction,  new HighlightStyle(Color.FromArgb(108, 113, 196)));
        }
    }
}

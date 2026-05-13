// =============================================================================
//  Nord Theme Definition for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides a Nord-inspired dark theme configuration used by the editor
//  syntax highlighting and UI rendering system.
//
//  This file is responsible for:
//  - Defining Nord theme metadata
//  - Configuring editor UI colors (background, selection, caret, etc.)
//  - Styling line numbers, gutter, and active line highlighting
//  - Defining syntax highlighting color scheme
//  - Mapping token types to Nord palette colors
//  - Ensuring consistent dark UI experience across languages
//
// -----------------------------------------------------------------------------
//
//  Theme Characteristics:
//  - Soft Arctic-inspired dark background
//  - Low eye strain color palette
//  - Cool blue/gray dominant tones
//  - Muted contrast for long coding sessions
//  - Balanced syntax highlighting without harsh colors
//
// -----------------------------------------------------------------------------
//
//  UI Color Configuration:
//  - Background color        -> Deep nordic blue-gray (46, 52, 64)
//  - Default text color      -> Light icy gray (216, 222, 233)
//  - Selection color         -> Subtle steel highlight (67, 76, 94)
//  - Current line highlight  -> Slightly lighter background tone
//  - Line number styling     -> Muted slate gray
//  - Caret color             -> Bright icy white
//  - Bracket match highlight -> Soft overlay highlight
//  - Autocomplete popup      -> Dark blended panel with nord tones
//
// -----------------------------------------------------------------------------
//
//  Syntax Highlighting Features:
//  - Keywords        -> Muted blue tones
//  - Strings         -> Soft greenish pastel
//  - Comments        -> Dimmed gray-blue (low emphasis)
//  - Numbers         -> Warm contrast tone
//  - Types           -> Cyan/ice blue accents
//  - Identifiers     -> Soft teal/green tones
//  - Operators       -> Light neutral text color
//  - Preprocessors   -> Warm yellow accent
//
// -----------------------------------------------------------------------------
//
//  Supported Token Categories:
//  - Programming language keywords
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
//  The Nord theme is designed to mimic Arctic aesthetics, focusing on calm
//  colors, reduced visual noise, and improved long-term readability.
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - CodeEditorLib theme system
//  - IDE-style dark mode UI
//  - Developer tools and editors
//  - Cross-language syntax highlighting
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
  
    public class NordTheme : EditorTheme
    {
        public NordTheme() : base("Nord") { }

        public override Color BackgroundColor       { get { return Color.FromArgb(46, 52, 64); } }
        public override Color DefaultTextColor      { get { return Color.FromArgb(216, 222, 233); } }
        public override Color SelectionColor        { get { return Color.FromArgb(67, 76, 94); } }
        public override Color CurrentLineColor      { get { return Color.FromArgb(59, 66, 82); } }
        public override Color LineNumberBackColor   { get { return Color.FromArgb(36, 41, 51); } }
        public override Color LineNumberForeColor   { get { return Color.FromArgb(76, 86, 106); } }
        public override Color LineNumberBorderColor { get { return Color.FromArgb(59, 66, 82); } }
        public override Color CaretColor            { get { return Color.FromArgb(216, 222, 233); } }
        public override Color BracketMatchColor     { get { return Color.FromArgb(67, 76, 94); } }
        public override Color AutoCompleteBackColor { get { return Color.FromArgb(59, 66, 82); } }
        public override Color AutoCompleteForeColor { get { return Color.FromArgb(216, 222, 233); } }
        public override Color AutoCompleteSelectionColor { get { return Color.FromArgb(136, 192, 208); } }

        protected override void BuildScheme(HighlightScheme scheme)
        {
            scheme.SetStyle(TokenType.Keyword,      new HighlightStyle(Color.FromArgb(129, 161, 193), true));
            scheme.SetStyle(TokenType.String,       new HighlightStyle(Color.FromArgb(163, 190, 140)));
            scheme.SetStyle(TokenType.Comment,      new HighlightStyle(Color.FromArgb(76, 86, 106), Color.Empty, false, true, false));
            scheme.SetStyle(TokenType.Number,       new HighlightStyle(Color.FromArgb(208, 135, 112)));
            scheme.SetStyle(TokenType.Type,         new HighlightStyle(Color.FromArgb(136, 192, 208)));
            scheme.SetStyle(TokenType.Operator,     new HighlightStyle(Color.FromArgb(216, 222, 233)));
            scheme.SetStyle(TokenType.Preprocessor, new HighlightStyle(Color.FromArgb(235, 203, 139)));
            scheme.SetStyle(TokenType.Identifier,   new HighlightStyle(Color.FromArgb(143, 188, 187)));
            scheme.SetStyle(TokenType.Attribute,    new HighlightStyle(Color.FromArgb(136, 192, 208)));
            scheme.SetStyle(TokenType.XmlTag,       new HighlightStyle(Color.FromArgb(129, 161, 193)));
            scheme.SetStyle(TokenType.XmlAttribute, new HighlightStyle(Color.FromArgb(143, 188, 187)));
            scheme.SetStyle(TokenType.XmlValue,     new HighlightStyle(Color.FromArgb(163, 190, 140)));
            scheme.SetStyle(TokenType.HtmlTag,      new HighlightStyle(Color.FromArgb(129, 161, 193)));
            scheme.SetStyle(TokenType.HtmlAttribute,new HighlightStyle(Color.FromArgb(143, 188, 187)));
            scheme.SetStyle(TokenType.HtmlValue,    new HighlightStyle(Color.FromArgb(163, 190, 140)));
            scheme.SetStyle(TokenType.CssSelector,  new HighlightStyle(Color.FromArgb(208, 135, 112)));
            scheme.SetStyle(TokenType.CssProperty,  new HighlightStyle(Color.FromArgb(129, 161, 193)));
            scheme.SetStyle(TokenType.CssValue,     new HighlightStyle(Color.FromArgb(163, 190, 140)));
            scheme.SetStyle(TokenType.SqlKeyword,   new HighlightStyle(Color.FromArgb(129, 161, 193), true));
            scheme.SetStyle(TokenType.SqlFunction,  new HighlightStyle(Color.FromArgb(136, 192, 208)));
        }
    }
}

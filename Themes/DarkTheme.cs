// =============================================================================
//  Dark Theme for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides a dark visual theme configuration for the CodeEditorLib editor.
//
//  This theme is responsible for:
//  - Defining editor background and foreground colors
//  - Styling selection, caret, and current line highlight
//  - Configuring line number appearance
//  - Setting bracket match highlighting
//  - Providing autocomplete UI color scheme
//  - Mapping syntax token colors for all supported languages
//
// -----------------------------------------------------------------------------
//
//  Theme Appearance:
//  - Dark gray editor background for reduced eye strain
//  - Soft light-gray text for readability
//  - Blue-based selection highlight
//  - Subtle current-line highlighting
//  - Dimmed line numbers for minimal distraction
//
// -----------------------------------------------------------------------------
//
//  UI Components Styled:
//  - Editor background
//  - Text rendering color
//  - Selection highlight
//  - Caret (cursor)
//  - Line numbers panel
//  - Active line highlight
//  - Bracket matching indicator
//  - Autocomplete popup
//
// -----------------------------------------------------------------------------
//
//  Syntax Highlighting Scheme:
//  Defines color mapping for token types across languages:
//
//  - Keywords      -> Blue (emphasized, bold)
//  - Strings       -> Orange-brown
//  - Comments      -> Green (muted)
//  - Numbers       -> Light green
//  - Types         -> Cyan-green
//  - Operators     -> Light gray
//  - Preprocessor  -> Gray
//  - Identifiers   -> Light cyan
//  - Attributes    -> Cyan-green
//
//  Language-specific tokens supported:
//  - XML tags, attributes, values
//  - HTML tags, attributes, values
//  - CSS selectors, properties, values
//  - SQL keywords and functions
//
// -----------------------------------------------------------------------------
//
//  Rendering System:
//  Uses a token-based highlighting engine (HighlightScheme)
//  where each TokenType is mapped to a HighlightStyle.
//
//  Priority rules ensure correct rendering:
//  - Keywords override identifiers
//  - Strings override keyword parsing
//  - Comments override all other token types
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - Dark mode IDE/editor environments
//  - Code editing in low-light conditions
//  - Developer-focused Windows Forms applications
//  - Embedded code editor components
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
    
    public class DarkTheme : EditorTheme
    {
        public DarkTheme() : base("Dark") { }

        public override Color BackgroundColor      { get { return Color.FromArgb(30, 30, 30); } }
        public override Color DefaultTextColor     { get { return Color.FromArgb(212, 212, 212); } }
        public override Color SelectionColor       { get { return Color.FromArgb(38, 79, 120); } }
        public override Color CurrentLineColor     { get { return Color.FromArgb(40, 40, 40); } }
        public override Color LineNumberBackColor  { get { return Color.FromArgb(24, 24, 24); } }
        public override Color LineNumberForeColor  { get { return Color.FromArgb(133, 133, 133); } }
        public override Color LineNumberBorderColor{ get { return Color.FromArgb(50, 50, 50); } }
        public override Color CaretColor           { get { return Color.White; } }
        public override Color BracketMatchColor    { get { return Color.FromArgb(80, 80, 0); } }
        public override Color AutoCompleteBackColor{ get { return Color.FromArgb(37, 37, 38); } }
        public override Color AutoCompleteForeColor{ get { return Color.FromArgb(212, 212, 212); } }
        public override Color AutoCompleteSelectionColor { get { return Color.FromArgb(0, 120, 215); } }

        protected override void BuildScheme(HighlightScheme scheme)
        {
            scheme.SetStyle(TokenType.Keyword,      new HighlightStyle(Color.FromArgb(86, 156, 214), true));
            scheme.SetStyle(TokenType.String,       new HighlightStyle(Color.FromArgb(206, 145, 120)));
            scheme.SetStyle(TokenType.Comment,      new HighlightStyle(Color.FromArgb(106, 153, 85), false));
            scheme.SetStyle(TokenType.Number,       new HighlightStyle(Color.FromArgb(181, 206, 168)));
            scheme.SetStyle(TokenType.Type,         new HighlightStyle(Color.FromArgb(78, 201, 176)));
            scheme.SetStyle(TokenType.Operator,     new HighlightStyle(Color.FromArgb(212, 212, 212)));
            scheme.SetStyle(TokenType.Preprocessor, new HighlightStyle(Color.FromArgb(155, 155, 155)));
            scheme.SetStyle(TokenType.Identifier,   new HighlightStyle(Color.FromArgb(156, 220, 254)));
            scheme.SetStyle(TokenType.Attribute,    new HighlightStyle(Color.FromArgb(78, 201, 176)));
            scheme.SetStyle(TokenType.XmlTag,       new HighlightStyle(Color.FromArgb(86, 156, 214)));
            scheme.SetStyle(TokenType.XmlAttribute, new HighlightStyle(Color.FromArgb(156, 220, 254)));
            scheme.SetStyle(TokenType.XmlValue,     new HighlightStyle(Color.FromArgb(206, 145, 120)));
            scheme.SetStyle(TokenType.HtmlTag,      new HighlightStyle(Color.FromArgb(86, 156, 214)));
            scheme.SetStyle(TokenType.HtmlAttribute,new HighlightStyle(Color.FromArgb(156, 220, 254)));
            scheme.SetStyle(TokenType.HtmlValue,    new HighlightStyle(Color.FromArgb(206, 145, 120)));
            scheme.SetStyle(TokenType.CssSelector,  new HighlightStyle(Color.FromArgb(215, 186, 125)));
            scheme.SetStyle(TokenType.CssProperty,  new HighlightStyle(Color.FromArgb(156, 220, 254)));
            scheme.SetStyle(TokenType.CssValue,     new HighlightStyle(Color.FromArgb(206, 145, 120)));
            scheme.SetStyle(TokenType.SqlKeyword,   new HighlightStyle(Color.FromArgb(86, 156, 214), true));
            scheme.SetStyle(TokenType.SqlFunction,  new HighlightStyle(Color.FromArgb(220, 220, 170)));
        }
    }
}

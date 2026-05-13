// =============================================================================
//  Light Theme for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides a light-mode visual theme configuration for the CodeEditorLib
//  code editor control.
//
//  This theme is responsible for:
//  - Defining a clean light UI color palette
//  - Styling editor background and text for readability
//  - Configuring selection, caret, and current line appearance
//  - Styling line number panel and borders
//  - Defining autocomplete popup colors
//  - Mapping syntax highlighting colors for all supported token types
//
// -----------------------------------------------------------------------------
//
//  Theme Design Philosophy:
//  - Bright, high-contrast interface for daytime usage
//  - Minimal visual noise for readability
//  - Traditional IDE-like color mapping
//  - Clear separation between UI elements and code content
//
// -----------------------------------------------------------------------------
//
//  UI Components Styled:
//  - Editor background (white canvas)
//  - Default text (black)
//  - Selection highlight (light blue)
//  - Current line highlight (subtle blue tint)
//  - Line number panel (light gray background)
//  - Caret (black cursor)
//  - Bracket match highlight (soft yellow)
//  - Autocomplete dropdown styling
//
// -----------------------------------------------------------------------------
//
//  Syntax Highlighting Scheme:
//  Defines token-to-color mapping for all supported languages:
//
//  - Keywords      -> Blue (bold emphasis)
//  - Strings       -> Dark red
//  - Comments      -> Green
//  - Numbers       -> Greenish tone
//  - Types         -> Teal/blue
//  - Operators     -> Black
//  - Preprocessor  -> Gray
//  - Identifiers   -> Black
//
//  Multi-language token support:
//  - XML / HTML tags, attributes, values
//  - CSS selectors, properties, values
//  - SQL keywords and functions
//
// -----------------------------------------------------------------------------
//
//  Rendering System:
//  Uses HighlightScheme-based token mapping where each TokenType is
//  associated with a HighlightStyle defining color and formatting.
//
//  Rendering priority rules:
//  - Comments override all syntax elements
//  - Strings override keywords and identifiers
//  - Keywords override identifiers
//  - Structured language tokens (XML/HTML/CSS/SQL) are handled separately
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - Light-mode IDE/editor environments
//  - Business applications with embedded code editors
//  - Documentation or configuration editing tools
//  - Windows Forms developer tools
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
//  THE SOFTWARE IS PROVIDED WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
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
    public class LightTheme : EditorTheme
    {
        public LightTheme() : base("Light") { }

        public override Color BackgroundColor      { get { return Color.White; } }
        public override Color DefaultTextColor     { get { return Color.Black; } }
        public override Color SelectionColor       { get { return Color.FromArgb(173, 214, 255); } }
        public override Color CurrentLineColor     { get { return Color.FromArgb(240, 248, 255); } }
        public override Color LineNumberBackColor  { get { return Color.FromArgb(238, 238, 238); } }
        public override Color LineNumberForeColor  { get { return Color.FromArgb(120, 120, 120); } }
        public override Color LineNumberBorderColor{ get { return Color.FromArgb(180, 180, 180); } }
        public override Color CaretColor           { get { return Color.Black; } }
        public override Color BracketMatchColor    { get { return Color.FromArgb(230, 230, 150); } }
        public override Color AutoCompleteBackColor{ get { return Color.White; } }
        public override Color AutoCompleteForeColor{ get { return Color.Black; } }
        public override Color AutoCompleteSelectionColor { get { return Color.FromArgb(0, 120, 215); } }

        protected override void BuildScheme(HighlightScheme scheme)
        {
            scheme.SetStyle(TokenType.Keyword,      new HighlightStyle(Color.FromArgb(0, 0, 255), true));
            scheme.SetStyle(TokenType.String,       new HighlightStyle(Color.FromArgb(163, 21, 21)));
            scheme.SetStyle(TokenType.Comment,      new HighlightStyle(Color.FromArgb(0, 128, 0)));
            scheme.SetStyle(TokenType.Number,       new HighlightStyle(Color.FromArgb(9, 134, 88)));
            scheme.SetStyle(TokenType.Type,         new HighlightStyle(Color.FromArgb(43, 145, 175)));
            scheme.SetStyle(TokenType.Operator,     new HighlightStyle(Color.Black));
            scheme.SetStyle(TokenType.Preprocessor, new HighlightStyle(Color.FromArgb(128, 128, 128)));
            scheme.SetStyle(TokenType.Identifier,   new HighlightStyle(Color.Black));
            scheme.SetStyle(TokenType.Attribute,    new HighlightStyle(Color.FromArgb(43, 145, 175)));
            scheme.SetStyle(TokenType.XmlTag,       new HighlightStyle(Color.FromArgb(128, 0, 0)));
            scheme.SetStyle(TokenType.XmlAttribute, new HighlightStyle(Color.FromArgb(255, 0, 0)));
            scheme.SetStyle(TokenType.XmlValue,     new HighlightStyle(Color.FromArgb(0, 0, 255)));
            scheme.SetStyle(TokenType.HtmlTag,      new HighlightStyle(Color.FromArgb(128, 0, 0)));
            scheme.SetStyle(TokenType.HtmlAttribute,new HighlightStyle(Color.FromArgb(255, 0, 0)));
            scheme.SetStyle(TokenType.HtmlValue,    new HighlightStyle(Color.FromArgb(0, 0, 255)));
            scheme.SetStyle(TokenType.CssSelector,  new HighlightStyle(Color.FromArgb(128, 0, 0)));
            scheme.SetStyle(TokenType.CssProperty,  new HighlightStyle(Color.FromArgb(255, 0, 0)));
            scheme.SetStyle(TokenType.CssValue,     new HighlightStyle(Color.FromArgb(0, 0, 255)));
            scheme.SetStyle(TokenType.SqlKeyword,   new HighlightStyle(Color.FromArgb(0, 0, 255), true));
            scheme.SetStyle(TokenType.SqlFunction,  new HighlightStyle(Color.FromArgb(128, 0, 128)));
        }
    }
}

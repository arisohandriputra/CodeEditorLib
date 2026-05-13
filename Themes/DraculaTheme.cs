// =============================================================================
//  Dracula Theme for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides a Dracula-inspired dark theme configuration for the CodeEditorLib
//  code editor control.
//
//  This theme is responsible for:
//  - Defining Dracula-style editor color palette
//  - Styling text, background, selection, and caret appearance
//  - Configuring line number panel styling
//  - Highlighting current line and bracket matching
//  - Styling autocomplete dropdown UI
//  - Mapping syntax highlighting colors for all supported token types
//
// -----------------------------------------------------------------------------
//
//  Theme Design Philosophy:
//  - Dark, low-contrast background for reduced eye strain
//  - Bright accent colors for syntax visibility
//  - Consistent color harmony across all UI elements
//  - High readability for long coding sessions
//
// -----------------------------------------------------------------------------
//
//  UI Components Styled:
//  - Editor background (deep Dracula purple tone)
//  - Default text (soft white)
//  - Selection highlight (muted gray-blue)
//  - Caret color (bright white)
//  - Line number panel (dark overlay style)
//  - Current line highlight
//  - Bracket matching indicator
//  - Autocomplete popup styling
//
// -----------------------------------------------------------------------------
//
//  Syntax Highlighting Scheme:
//  Defines token-to-color mapping based on Dracula palette:
//
//  - Keywords      -> Pink / magenta accent (bold emphasis)
//  - Strings       -> Yellow accent
//  - Comments      -> Muted blue-gray (italic style)
//  - Numbers       -> Purple accent
//  - Types         -> Cyan accent
//  - Operators     -> Pink accent
//  - Preprocessor  -> Orange accent
//  - Identifiers   -> Green accent
//
//  Multi-language support includes:
//  - XML / HTML tags and attributes
//  - CSS selectors, properties, values
//  - SQL keywords and functions
//
// -----------------------------------------------------------------------------
//
//  Rendering Engine:
//  Uses HighlightScheme-based token mapping system.
//  Each TokenType is assigned a HighlightStyle defining color and font style.
//
//  Rendering priority rules:
//  - Comments override all token types
//  - Strings override keywords and identifiers
//  - Keywords override identifiers
//  - Structural tokens (XML/HTML) are parsed separately
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - Dark-mode IDE and code editors
//  - Developer tools and embedded editors
//  - Windows Forms code editor applications
//  - Multi-language syntax highlighting environments
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
   
    public class DraculaTheme : EditorTheme
    {
        public DraculaTheme() : base("Dracula") { }

        public override Color BackgroundColor       { get { return Color.FromArgb(40, 42, 54); } }
        public override Color DefaultTextColor      { get { return Color.FromArgb(248, 248, 242); } }
        public override Color SelectionColor        { get { return Color.FromArgb(68, 71, 90); } }
        public override Color CurrentLineColor      { get { return Color.FromArgb(68, 71, 90); } }
        public override Color LineNumberBackColor   { get { return Color.FromArgb(33, 34, 44); } }
        public override Color LineNumberForeColor   { get { return Color.FromArgb(100, 102, 120); } }
        public override Color LineNumberBorderColor { get { return Color.FromArgb(68, 71, 90); } }
        public override Color CaretColor            { get { return Color.FromArgb(248, 248, 242); } }
        public override Color BracketMatchColor     { get { return Color.FromArgb(80, 90, 50); } }
        public override Color AutoCompleteBackColor { get { return Color.FromArgb(40, 42, 54); } }
        public override Color AutoCompleteForeColor { get { return Color.FromArgb(248, 248, 242); } }
        public override Color AutoCompleteSelectionColor { get { return Color.FromArgb(189, 147, 249); } }

        protected override void BuildScheme(HighlightScheme scheme)
        {
            scheme.SetStyle(TokenType.Keyword,      new HighlightStyle(Color.FromArgb(255, 121, 198), true));
            scheme.SetStyle(TokenType.String,       new HighlightStyle(Color.FromArgb(241, 250, 140)));
            scheme.SetStyle(TokenType.Comment,      new HighlightStyle(Color.FromArgb(98, 114, 164), Color.Empty, false, true, false));
            scheme.SetStyle(TokenType.Number,       new HighlightStyle(Color.FromArgb(189, 147, 249)));
            scheme.SetStyle(TokenType.Type,         new HighlightStyle(Color.FromArgb(139, 233, 253)));
            scheme.SetStyle(TokenType.Operator,     new HighlightStyle(Color.FromArgb(255, 121, 198)));
            scheme.SetStyle(TokenType.Preprocessor, new HighlightStyle(Color.FromArgb(255, 184, 108)));
            scheme.SetStyle(TokenType.Identifier,   new HighlightStyle(Color.FromArgb(80, 250, 123)));
            scheme.SetStyle(TokenType.Attribute,    new HighlightStyle(Color.FromArgb(80, 250, 123)));
            scheme.SetStyle(TokenType.XmlTag,       new HighlightStyle(Color.FromArgb(255, 121, 198)));
            scheme.SetStyle(TokenType.XmlAttribute, new HighlightStyle(Color.FromArgb(80, 250, 123)));
            scheme.SetStyle(TokenType.XmlValue,     new HighlightStyle(Color.FromArgb(241, 250, 140)));
            scheme.SetStyle(TokenType.HtmlTag,      new HighlightStyle(Color.FromArgb(255, 121, 198)));
            scheme.SetStyle(TokenType.HtmlAttribute,new HighlightStyle(Color.FromArgb(80, 250, 123)));
            scheme.SetStyle(TokenType.HtmlValue,    new HighlightStyle(Color.FromArgb(241, 250, 140)));
            scheme.SetStyle(TokenType.CssSelector,  new HighlightStyle(Color.FromArgb(255, 121, 198)));
            scheme.SetStyle(TokenType.CssProperty,  new HighlightStyle(Color.FromArgb(139, 233, 253)));
            scheme.SetStyle(TokenType.CssValue,     new HighlightStyle(Color.FromArgb(241, 250, 140)));
            scheme.SetStyle(TokenType.SqlKeyword,   new HighlightStyle(Color.FromArgb(255, 121, 198), true));
            scheme.SetStyle(TokenType.SqlFunction,  new HighlightStyle(Color.FromArgb(80, 250, 123)));
        }
    }
}

// =============================================================================
//  CSS Language Definition for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides CSS language configuration used by the editor
//  syntax highlighting and parsing engine.
//
//  This file is responsible for:
//  - Defining CSS language metadata
//  - Registering supported stylesheet file extensions
//  - Configuring syntax highlighting rules
//  - Detecting selectors, properties, and values
//  - Detecting comments, strings, and numbers
//  - Supporting automatic indentation behavior
//
// -----------------------------------------------------------------------------
//
//  Supported File Extensions:
//  - .css
//  - .scss
//  - .less
//  - .sass
//
// -----------------------------------------------------------------------------
//
//  Syntax Highlighting Features:
//  - Multi-line comments       -> /* comment */
//  - CSS selectors             -> body, .class, #id
//  - CSS properties            -> color, margin
//  - CSS values                -> red, 10px
//  - Color values              -> #ffffff
//  - Numeric values            -> 12px, 50%
//  - Strings                   -> "text"
//  - At-rules                  -> @media, @import
//
// -----------------------------------------------------------------------------
//
//  Supported Stylesheet Syntax:
//  - Standard CSS
//  - SCSS
//  - LESS
//  - SASS
//
// -----------------------------------------------------------------------------
//
//  Auto Indentation:
//  Automatically increases indentation after lines
//  ending with opening braces '{'.
//
//  Example:
//      .container
//      {
//          |
//      }
//
// -----------------------------------------------------------------------------
//
//  Internal Parsing System:
//  Uses regex-based syntax rules with priority ordering.
//
//  Rule priorities ensure:
//  - Comments override other tokens
//  - Selectors are detected correctly
//  - Property/value parsing remains accurate
//  - String literals are isolated properly
//
// -----------------------------------------------------------------------------
//
//  CSS Parsing Support:
//  Recognizes common CSS structures such as:
//
//      body {
//          color: #ffffff;
//          margin: 10px;
//      }
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - CSS stylesheet editing
//  - Web development
//  - Frontend code highlighting
//  - Theme and UI styling editors
//  - IDE/editor integration
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
using CodeEditorLib.Core;

namespace CodeEditorLib.Languages
{
    public class CssLanguage : LanguageDefinition
    {
        public CssLanguage()
        {
            Name = "CSS";
            CommentMultiLineStart = "/*";
            CommentMultiLineEnd = "*/";
            IndentChars = "  ";
            AddExtension(".css");
            AddExtension(".scss");
            AddExtension(".less");
            AddExtension(".sass");

            AddRule(new SyntaxRule("Comment",    @"/\*[\s\S]*?\*/",                    TokenType.Comment, 10));
            AddRule(new SyntaxRule("AtRule",     @"@[\w\-]+",                           TokenType.Preprocessor, 9));
            AddRule(new SyntaxRule("Selector",   @"(?:^|\})\s*([^{]+)(?=\s*\{)",       TokenType.CssSelector, 8));
            AddRule(new SyntaxRule("CssValue",   @"(?<=:)\s*[^;{}]+",                  TokenType.CssValue, 7));
            AddRule(new SyntaxRule("CssProp",    @"[\w\-]+(?=\s*:)",                   TokenType.CssProperty, 6));
            AddRule(new SyntaxRule("Color",      @"#(?:[0-9a-fA-F]{3}|[0-9a-fA-F]{6})\b", TokenType.Number, 5));
            AddRule(new SyntaxRule("Number",     @"\b\d+\.?\d*(?:px|em|rem|%|vh|vw|pt|cm|mm|s|ms)?\b", TokenType.Number, 4));
            AddRule(new SyntaxRule("String",     @"""[^""]*""|'[^']*'",                 TokenType.String, 3));
        }

        public override bool ShouldAutoIndent(string lineText) { return lineText.TrimEnd().EndsWith("{"); }
    }
}

// =============================================================================
//  JavaScript Language Definition for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides JavaScript language configuration used by the
//  editor syntax highlighting and parsing engine.
//
//  This file is responsible for:
//  - Defining JavaScript language metadata
//  - Registering JavaScript file extensions
//  - Configuring syntax highlighting rules
//  - Detecting comments and string literals
//  - Detecting keywords, built-in objects, and operators
//  - Supporting automatic indentation behavior
//
// -----------------------------------------------------------------------------
//
//  Supported File Extensions:
//  - .js
//  - .jsx
//  - .mjs
//
// -----------------------------------------------------------------------------
//
//  Syntax Highlighting Features:
//  - Single-line comments      -> // comment
//  - Multi-line comments       -> /* comment */
//  - Standard string literals  -> "text"
//  - Single-quoted strings     -> 'text'
//  - Template literals         -> `text`
//  - JavaScript keywords       -> function, class, async
//  - Built-in objects/functions
//  - Numeric literals
//  - Operators and expressions
//
// -----------------------------------------------------------------------------
//
//  Keyword Support:
//  Includes modern JavaScript keywords such as:
//  - let
//  - const
//  - class
//  - async
//  - await
//  - import
//  - export
//  - yield
//
//  Compatible with modern ECMAScript syntax.
//
// -----------------------------------------------------------------------------
//
//  Built-in Object Recognition:
//  Detects common JavaScript built-in objects
//  and browser APIs.
//
//  Examples:
//  - console
//  - document
//  - window
//  - Array
//  - Promise
//  - JSON
//  - fetch
//
// -----------------------------------------------------------------------------
//
//  Operator Support:
//  Recognizes modern JavaScript operators such as:
//
//  Examples:
//  - Arrow function     -> =>
//  - Optional chaining  -> ?.
//  - Arithmetic         -> + - * /
//  - Comparison         -> == === !=
//
// -----------------------------------------------------------------------------
//
//  Auto Indentation:
//  Automatically increases indentation after lines
//  ending with opening braces '{'.
//
//  Example:
//      function test()
//      {
//          |
//      }
//
// -----------------------------------------------------------------------------
//
//  Internal Parsing System:
//  Uses regex-based syntax rules with priority ordering.
//
//  Priority handling ensures:
//  - Comments override keywords
//  - Template literals are parsed correctly
//  - Built-ins are highlighted separately
//  - Accurate token classification
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - JavaScript source editing
//  - Web application development
//  - Frontend scripting
//  - Syntax highlighting
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
using System.Collections.Generic;
using CodeEditorLib.Core;

namespace CodeEditorLib.Languages
{
    public class JavaScriptLanguage : LanguageDefinition
    {
        private static readonly string[] Keywords = {
            "break","case","catch","class","const","continue","debugger",
            "default","delete","do","else","export","extends","false",
            "finally","for","function","if","import","in","instanceof",
            "let","new","null","of","return","static","super","switch",
            "this","throw","true","try","typeof","undefined","var","void",
            "while","with","yield","async","await","from","get","set"
        };

        private static readonly string[] Builtins = {
            "console","document","window","Array","Object","String",
            "Number","Boolean","Math","Date","JSON","Promise","Error",
            "Map","Set","RegExp","Symbol","parseInt","parseFloat",
            "isNaN","isFinite","setTimeout","setInterval","fetch"
        };

        public JavaScriptLanguage()
        {
            Name = "JavaScript";
            CommentSingleLine     = "//";
            CommentMultiLineStart = "/*";
            CommentMultiLineEnd   = "*/";
            IndentChars = "  ";
            AddExtension(".js");
            AddExtension(".jsx");
            AddExtension(".mjs");

            AddRule(new SyntaxRule("BlockComment", @"/\*[\s\S]*?\*/",                          TokenType.Comment,  10));
            AddRule(new SyntaxRule("LineComment",  @"//[^\n]*",                                 TokenType.Comment,   9));
            AddRule(new SyntaxRule("TemplateLit",  @"`(?:\\.|[^`\\])*`",                       TokenType.String,    8));
            AddRule(new SyntaxRule("String",       @"""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])*'",  TokenType.String,    7));
            AddRule(new SyntaxRule("Keywords",     @"\b(" + string.Join("|", Keywords) + @")\b", TokenType.Keyword, 5));
            AddRule(new SyntaxRule("Builtins",     @"\b(" + string.Join("|", Builtins) + @")\b", TokenType.Type,   4));
            AddRule(new SyntaxRule("Number",       @"\b\d+\.?\d*(?:[eE][+-]?\d+)?\b",          TokenType.Number,    3));
            AddRule(new SyntaxRule("Operator",     @"[+\-*/%&|^~!=<>?:]+|=>|\?\.",             TokenType.Operator,  1));
        }

        public override bool ShouldAutoIndent(string lineText)
        {
            return lineText.TrimEnd().EndsWith("{");
        }
    }
}

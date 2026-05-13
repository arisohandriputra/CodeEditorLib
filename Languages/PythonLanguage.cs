// =============================================================================
//  Python Language Definition for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides Python language configuration used by the
//  editor syntax highlighting and parsing engine.
//
//  This file is responsible for:
//  - Defining Python language metadata
//  - Registering Python source file extensions
//  - Configuring syntax highlighting rules
//  - Detecting comments, decorators, and strings
//  - Detecting Python keywords and built-in functions
//  - Supporting Python-style automatic indentation
//
// -----------------------------------------------------------------------------
//
//  Supported File Extensions:
//  - .py
//  - .pyw
//
// -----------------------------------------------------------------------------
//
//  Syntax Highlighting Features:
//  - Single-line comments      -> # comment
//  - Triple-quoted strings
//  - Standard string literals  -> "text"
//  - Single-quoted strings     -> 'text'
//  - Decorators                -> @staticmethod
//  - Python keywords           -> def, class, async
//  - Built-in functions/types
//  - Numeric literals
//  - Operators and expressions
//
// -----------------------------------------------------------------------------
//
//  Keyword Support:
//  Includes modern Python keywords such as:
//  - async
//  - await
//  - lambda
//  - yield
//  - nonlocal
//  - with
//
//  Compatible with modern Python syntax.
//
// -----------------------------------------------------------------------------
//
//  Built-in Function Recognition:
//  Detects common Python built-in functions
//  and standard runtime objects.
//
//  Examples:
//  - print
//  - len
//  - range
//  - open
//  - isinstance
//  - Exception
//
// -----------------------------------------------------------------------------
//
//  Decorator Support:
//  Detects Python decorators used for functions
//  and classes.
//
//  Examples:
//  - @property
//  - @staticmethod
//  - @classmethod
//
// -----------------------------------------------------------------------------
//
//  String Handling:
//  Supports multiple Python string styles:
//
//  Examples:
//  - "Hello"
//  - 'Hello'
//  - """Multi-line"""
//  - '''Multi-line'''
//
// -----------------------------------------------------------------------------
//
//  Auto Indentation:
//  Automatically increases indentation after lines
//  ending with ':'.
//
//  Example:
//      if ready:
//          |
//
// -----------------------------------------------------------------------------
//
//  Internal Parsing System:
//  Uses regex-based syntax rules with priority ordering.
//
//  Priority handling ensures:
//  - Comments override keywords
//  - Triple-quoted strings are parsed correctly
//  - Decorators are highlighted separately
//  - Accurate token classification
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - Python source code editing
//  - Scripting and automation
//  - Data science development
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
    public class PythonLanguage : LanguageDefinition
    {
        private static readonly string[] Keywords = {
            "False","None","True","and","as","assert","async","await",
            "break","class","continue","def","del","elif","else","except",
            "finally","for","from","global","if","import","in","is",
            "lambda","nonlocal","not","or","pass","raise","return",
            "try","while","with","yield"
        };

        private static readonly string[] Builtins = {
            "print","len","range","type","str","int","float","bool",
            "list","dict","set","tuple","input","open","enumerate",
            "zip","map","filter","sorted","reversed","sum","min","max",
            "abs","round","isinstance","issubclass","hasattr","getattr",
            "setattr","dir","vars","repr","format","super","property",
            "staticmethod","classmethod","Exception","ValueError","TypeError"
        };

        public PythonLanguage()
        {
            Name = "Python";
            CommentSingleLine = "#";
            IndentChars = "    ";
            AddExtension(".py");
            AddExtension(".pyw");

            AddRule(new SyntaxRule("Comment",      @"#[^\n]*",                                           TokenType.Comment,   9));
            AddRule(new SyntaxRule("TripleDouble", "\"\"\"[\\s\\S]*?\"\"\"",                             TokenType.Comment,   8));
            AddRule(new SyntaxRule("TripleSingle", @"'''[\s\S]*?'''",                                    TokenType.Comment,   8));
            AddRule(new SyntaxRule("String",       @"""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])*'",            TokenType.String,    7));
            AddRule(new SyntaxRule("Keywords",     @"\b(" + string.Join("|", Keywords) + @")\b",         TokenType.Keyword,   5));
            AddRule(new SyntaxRule("Builtins",     @"\b(" + string.Join("|", Builtins) + @")\b",         TokenType.Type,      4));
            AddRule(new SyntaxRule("Decorator",    @"@\w+",                                               TokenType.Attribute, 3));
            AddRule(new SyntaxRule("Number",       @"\b\d+\.?\d*(?:[eE][+-]?\d+)?\b",                   TokenType.Number,    2));
            AddRule(new SyntaxRule("Operator",     @"[+\-*/%&|^~!=<>]+|//|\*\*",                        TokenType.Operator,  1));
        }

        public override bool ShouldAutoIndent(string lineText)
        {
            return lineText.TrimEnd().EndsWith(":");
        }
    }
}

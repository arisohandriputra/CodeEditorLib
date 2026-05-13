// =============================================================================
//  Go Language Definition for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides Go programming language configuration used by
//  the editor syntax highlighting and parsing engine.
//
//  This file is responsible for:
//  - Defining Go language metadata
//  - Registering Go source file extensions
//  - Configuring syntax highlighting rules
//  - Detecting comments and string literals
//  - Detecting keywords, built-in types, and operators
//  - Supporting automatic indentation behavior
//
// -----------------------------------------------------------------------------
//
//  Supported File Extensions:
//  - .go
//
// -----------------------------------------------------------------------------
//
//  Syntax Highlighting Features:
//  - Single-line comments      -> // comment
//  - Multi-line comments       -> /* comment */
//  - Raw string literals       -> `text`
//  - Standard string literals  -> "text"
//  - Character literals        -> 'A'
//  - Build tags                -> //go:build
//  - Go keywords               -> func, package, interface
//  - Built-in types/functions
//  - Numeric literals
//  - Operators and expressions
//
// -----------------------------------------------------------------------------
//
//  Keyword Support:
//  Includes Go language keywords such as:
//  - func
//  - package
//  - interface
//  - defer
//  - select
//  - range
//  - chan
//
// -----------------------------------------------------------------------------
//
//  Built-in Type Recognition:
//  Includes common Go types and functions:
//
//  Examples:
//  - int
//  - string
//  - error
//  - append
//  - make
//  - panic
//  - recover
//
//  Also includes common standard library structures such as:
//  - Context
//  - WaitGroup
//  - Mutex
//  - Request
//  - Response
//  - Client
//
// -----------------------------------------------------------------------------
//
//  Numeric Literal Support:
//  Detects multiple Go number formats:
//
//  Examples:
//  - Decimal      -> 123
//  - Hexadecimal  -> 0xFF
//  - Binary       -> 0b1010
//  - Octal        -> 0o755
//  - Floating     -> 3.14
//  - Imaginary    -> 5i
//
// -----------------------------------------------------------------------------
//
//  Auto Indentation:
//  Automatically increases indentation after lines
//  ending with opening braces '{'.
//
//  Uses tab indentation style to match common Go formatting.
//
// -----------------------------------------------------------------------------
//
//  Internal Parsing System:
//  Uses regex-based syntax rules with priority ordering.
//
//  Priority handling ensures:
//  - Comments override keywords
//  - Raw strings are parsed correctly
//  - Build tags are detected separately
//  - Accurate token classification
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - Go source code editing
//  - Backend development
//  - Syntax highlighting
//  - IDE/editor integration
//  - Code parsing systems
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
    public class GoLanguage : LanguageDefinition
    {
        private static readonly string[] Keywords = {
            "break","case","chan","const","continue","default","defer","else",
            "fallthrough","for","func","go","goto","if","import","interface",
            "map","package","range","return","select","struct","switch","type",
            "var"
        };

        private static readonly string[] Types = {
            "bool","byte","complex64","complex128","error","float32","float64",
            "int","int8","int16","int32","int64","rune","string","uint","uint8",
            "uint16","uint32","uint64","uintptr","any","comparable",
            "append","cap","clear","close","complex","copy","delete","imag",
            "len","make","max","min","new","panic","print","println","real","recover",
            "Context","WaitGroup","Mutex","RWMutex","Once","Reader","Writer",
            "ReadWriter","Closer","Handler","Request","Response","Server","Client",
            "Buffer","Scanner","Map","Slice","Chan","Func"
        };

        public GoLanguage()
        {
            Name = "Go";
            CommentSingleLine     = "//";
            CommentMultiLineStart = "/*";
            CommentMultiLineEnd   = "*/";
            IndentChars = "\t";
            AddExtension(".go");

            AddRule(new SyntaxRule("BlockComment",  @"/\*[\s\S]*?\*/",                                        TokenType.Comment,     10));
            AddRule(new SyntaxRule("LineComment",   @"//[^\n]*",                                              TokenType.Comment,      9));
            AddRule(new SyntaxRule("RawString",     @"`[^`]*`",                                               TokenType.String,       8));
            AddRule(new SyntaxRule("String",        @"""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])'",                 TokenType.String,       7));
            AddRule(new SyntaxRule("BuildTag",      @"^//\s*go:build[^\n]*|^//\s*\+build[^\n]*",             TokenType.Preprocessor, 6));
            AddRule(new SyntaxRule("Keywords",      @"\b(" + string.Join("|", Keywords) + @")\b",             TokenType.Keyword,      4));
            AddRule(new SyntaxRule("Types",         @"\b(" + string.Join("|", Types)    + @")\b",             TokenType.Type,         3));
            AddRule(new SyntaxRule("Number",        @"\b(?:0x[0-9a-fA-F_]+|0b[01_]+|0o[0-7_]+|\d[\d_]*\.?[\d_]*(?:[eE][+-]?[\d_]+)?(?:i)?)\b",
                                                                                                              TokenType.Number,       2));
            AddRule(new SyntaxRule("Operator",      @"[+\-*/%&|^~!=<>:]+|&\^|<<=|>>=|\.\.\.",               TokenType.Operator,     1));
        }

        public override bool ShouldAutoIndent(string lineText)
        {
            return lineText.TrimEnd().EndsWith("{");
        }
    }
}

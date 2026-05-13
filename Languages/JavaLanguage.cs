// =============================================================================
//  Java Language Definition for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides Java language configuration used by the editor
//  syntax highlighting and parsing engine.
//
//  This file is responsible for:
//  - Defining Java language metadata
//  - Registering Java source file extensions
//  - Configuring syntax highlighting rules
//  - Detecting comments, strings, and annotations
//  - Detecting keywords, types, numbers, and operators
//  - Supporting automatic indentation behavior
//
// -----------------------------------------------------------------------------
//
//  Supported File Extensions:
//  - .java
//
// -----------------------------------------------------------------------------
//
//  Syntax Highlighting Features:
//  - Single-line comments      -> // comment
//  - Multi-line comments       -> /* comment */
//  - Standard string literals  -> "text"
//  - Character literals        -> 'A'
//  - Java annotations          -> @Override
//  - Java keywords             -> class, interface, public
//  - Type/class names
//  - Numeric literals
//  - Operators and expressions
//
// -----------------------------------------------------------------------------
//
//  Keyword Support:
//  Includes standard and modern Java keywords such as:
//  - class
//  - interface
//  - synchronized
//  - enum
//  - record
//  - sealed
//  - permits
//  - var
//
//  Compatible with multiple Java language versions.
//
// -----------------------------------------------------------------------------
//
//  Type Recognition:
//  Detects class and type identifiers using
//  uppercase naming conventions.
//
//  Examples:
//  - String
//  - ArrayList
//  - Scanner
//  - CustomClass
//
// -----------------------------------------------------------------------------
//
//  Numeric Literal Support:
//  Detects common Java number formats:
//
//  Examples:
//  - Integer       -> 123
//  - Floating      -> 3.14f
//  - Long          -> 999L
//  - Hexadecimal   -> 0xFF
//
// -----------------------------------------------------------------------------
//
//  Auto Indentation:
//  Automatically increases indentation after lines
//  ending with opening braces '{'.
//
//  Example:
//      public class App
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
//  - Strings are parsed correctly
//  - Annotations are detected separately
//  - Accurate token classification
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - Java source code editing
//  - JVM application development
//  - Android development
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
    public class JavaLanguage : LanguageDefinition
    {
        private static readonly string[] Keywords = {
            "abstract","assert","boolean","break","byte","case","catch","char","class","const",
            "continue","default","do","double","else","enum","extends","final","finally","float",
            "for","goto","if","implements","import","instanceof","int","interface","long","native",
            "new","package","private","protected","public","return","short","static","strictfp",
            "super","switch","synchronized","this","throw","throws","transient","try","void",
            "volatile","while","true","false","null","var","record","sealed","permits"
        };

        public JavaLanguage()
        {
            Name = "Java";
            CommentSingleLine = "//";
            CommentMultiLineStart = "/*";
            CommentMultiLineEnd = "*/";
            IndentChars = "    ";
            AddExtension(".java");

            AddRule(new SyntaxRule("BlockComment", @"/\*[\s\S]*?\*/", TokenType.Comment, 10));
            AddRule(new SyntaxRule("LineComment",  @"//[^\n]*",        TokenType.Comment, 9));
            AddRule(new SyntaxRule("String",       @"""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])'", TokenType.String, 8));
            AddRule(new SyntaxRule("Annotation",   @"@\w+",            TokenType.Attribute, 7));
            string kwPat = @"\b(" + string.Join("|", Keywords) + @")\b";
            AddRule(new SyntaxRule("Keywords", kwPat, TokenType.Keyword, 6));
            AddRule(new SyntaxRule("Type",   @"\b[A-Z][A-Za-z0-9_]*\b", TokenType.Type, 4));
            AddRule(new SyntaxRule("Number", @"\b\d+\.?\d*[fFdDlL]?\b|0x[0-9a-fA-F]+[lL]?\b", TokenType.Number, 3));
            AddRule(new SyntaxRule("Operator", @"[+\-*/%&|^~!=<>?:]+|>>>|<<|>>", TokenType.Operator, 1));
        }

        public override bool ShouldAutoIndent(string lineText) { return lineText.EndsWith("{"); }
    }
}

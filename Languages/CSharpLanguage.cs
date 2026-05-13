// =============================================================================
//  C# Language Definition for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides full C# language configuration used by the
//  editor syntax highlighting and parsing engine.
//
//  This file is responsible for:
//  - Defining C# language metadata
//  - Registering supported C# file extensions
//  - Configuring syntax highlighting rules
//  - Detecting comments, strings, and attributes
//  - Detecting keywords, types, numbers, and operators
//  - Supporting automatic indentation behavior
//
// -----------------------------------------------------------------------------
//
//  Supported File Extensions:
//  - .cs
//
// -----------------------------------------------------------------------------
//
//  Syntax Highlighting Features:
//  - Single-line comments      -> // comment
//  - Multi-line comments       -> /* comment */
//  - Standard string literals  -> "text"
//  - Verbatim strings          -> @"text"
//  - Character literals        -> 'A'
//  - Preprocessor directives   -> #region
//  - Attributes                -> [Serializable]
//  - C# keywords               -> class, async, await
//  - Common .NET types
//  - Numeric literals
//  - Operators and expressions
//
// -----------------------------------------------------------------------------
//
//  Keyword Support:
//  Includes modern C# keywords such as:
//  - async
//  - await
//  - nameof
//  - dynamic
//  - partial
//  - yield
//  - var
//
//  Compatible with multiple C# language versions.
//
// -----------------------------------------------------------------------------
//
//  Built-in Type Recognition:
//  Includes common .NET framework classes and types:
//
//  Examples:
//  - String
//  - DateTime
//  - List
//  - Dictionary
//  - Task
//  - HttpClient
//  - JsonSerializer
//
// -----------------------------------------------------------------------------
//
//  Auto Indentation:
//  Automatically increases indentation after:
//
//  - Opening braces '{'
//  - Label or case lines ending with ':'
//
//  Example:
//      switch(value)
//      {
//          case 1:
//              |
//      }
//
// -----------------------------------------------------------------------------
//
//  Internal Parsing System:
//  Uses regex-based syntax rules with priority ordering.
//
//  Higher priority rules ensure:
//  - Comments override keywords
//  - Strings are parsed correctly
//  - Attributes are detected properly
//  - Accurate syntax rendering
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - C# source code editing
//  - .NET application development
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
    public class CSharpLanguage : LanguageDefinition
    {
        private static readonly string[] Keywords = {
            "abstract","as","base","bool","break","byte","case","catch","char",
            "checked","class","const","continue","decimal","default","delegate",
            "do","double","else","enum","event","explicit","extern","false",
            "finally","fixed","float","for","foreach","goto","if","implicit",
            "in","int","interface","internal","is","lock","long","namespace",
            "new","null","object","operator","out","override","params","private",
            "protected","public","readonly","ref","return","sbyte","sealed",
            "short","sizeof","stackalloc","static","string","struct","switch",
            "this","throw","true","try","typeof","uint","ulong","unchecked",
            "unsafe","ushort","using","virtual","void","volatile","while",
            "get","set","value","add","remove","partial","where","yield",
            "async","await","var","dynamic","nameof","when"
        };

        private static readonly string[] Types = {
            "String","Int32","Int64","Boolean","Double","Single","Decimal",
            "Byte","Char","DateTime","TimeSpan","Guid","Exception","Object",
            "List","Dictionary","Array","StringBuilder","Stream","Console",
            "Math","Environment","Thread","Task","Action","Func","Predicate",
            "IEnumerable","ICollection","IList","IDictionary","IDisposable",
            "HashSet","Queue","Stack","Tuple","Nullable","EventArgs",
            "CancellationToken","HttpClient","JsonSerializer"
        };

        public CSharpLanguage()
        {
            Name = "C#";
            CommentSingleLine     = "//";
            CommentMultiLineStart = "/*";
            CommentMultiLineEnd   = "*/";
            IndentChars = "    ";
            AddExtension(".cs");

            AddRule(new SyntaxRule("BlockComment",  @"/\*[\s\S]*?\*/",                                        TokenType.Comment,     10));
            AddRule(new SyntaxRule("LineComment",   @"//[^\n]*",                                               TokenType.Comment,      9));
            AddRule(new SyntaxRule("VerbatimString",@"@""(?:[^""]|"""")*""",                                   TokenType.String,       8));
            AddRule(new SyntaxRule("String",        @"""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])'",                  TokenType.String,       7));
            AddRule(new SyntaxRule("Preprocessor",  @"^\s*#\w+[^\n]*",                                        TokenType.Preprocessor, 6));
            AddRule(new SyntaxRule("Attribute",     @"\[\s*\w[\w\.]*(?:\s*\(.*?\))?\s*\]",                    TokenType.Attribute,    5));
            AddRule(new SyntaxRule("Keywords",      @"\b(" + string.Join("|", Keywords) + @")\b",             TokenType.Keyword,      4));
            AddRule(new SyntaxRule("Types",         @"\b(" + string.Join("|", Types)    + @")\b",             TokenType.Type,         3));
            AddRule(new SyntaxRule("Number",        @"\b(?:0x[0-9a-fA-F]+|\d+\.?\d*(?:[eE][+-]?\d+)?[fFdDmMlLuU]*)\b", TokenType.Number, 2));
            AddRule(new SyntaxRule("Operator",      @"[+\-*/%&|^~!=<>?:]+|=>|\?\?|\.\.",                      TokenType.Operator,     1));
        }

        public override bool ShouldAutoIndent(string lineText)
        {
            return lineText.TrimEnd().EndsWith("{") || lineText.TrimEnd().EndsWith(":");
        }
    }
}

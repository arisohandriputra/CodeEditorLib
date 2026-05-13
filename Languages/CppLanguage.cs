// =============================================================================
//  C++ Language Definition for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides full C and C++ language configuration used by
//  the editor syntax highlighting and parsing engine.
//
//  This file is responsible for:
//  - Defining C/C++ language metadata
//  - Registering supported file extensions
//  - Configuring syntax highlighting rules
//  - Detecting comments and strings
//  - Detecting keywords, types, numbers, and operators
//  - Supporting automatic indentation behavior
//  - Supporting bracket pair matching
//
// -----------------------------------------------------------------------------
//
//  Supported File Extensions:
//  - .cpp
//  - .cc
//  - .cxx
//  - .c
//  - .h
//  - .hpp
//  - .hxx
//
// -----------------------------------------------------------------------------
//
//  Syntax Highlighting Features:
//  - Single-line comments      -> // comment
//  - Multi-line comments       -> /* comment */
//  - Raw string literals       -> R"(text)"
//  - Standard string literals  -> "text"
//  - Character literals        -> 'A'
//  - Preprocessor directives   -> #include
//  - C++ keywords              -> class, template, constexpr
//  - Types and identifiers
//  - Numeric literals
//  - Operators and symbols
//
// -----------------------------------------------------------------------------
//
//  Keyword Support:
//  Includes modern C++ keywords such as:
//  - constexpr
//  - consteval
//  - co_await
//  - requires
//  - concept
//  - override
//  - final
//
//  Compatible with modern C++ standards including:
//  - C++11
//  - C++14
//  - C++17
//  - C++20
//
// -----------------------------------------------------------------------------
//
//  Auto Indentation:
//  Automatically increases indentation level after lines
//  ending with opening braces '{'.
//
//  Example:
//      if (value)
//      {
//          |
//      }
//
// -----------------------------------------------------------------------------
//
//  Bracket Pair Support:
//  Automatically recognizes matching bracket pairs:
//
//      ( )
//      [ ]
//      { }
//      < >
//
//  Useful for:
//  - Auto-closing brackets
//  - Bracket highlighting
//  - Code navigation
//
// -----------------------------------------------------------------------------
//
//  Internal Parsing System:
//  Uses regex-based syntax rules with priorities.
//
//  Higher priority rules are processed first to ensure:
//  - Correct token classification
//  - Proper comment/string detection
//  - Accurate syntax rendering
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - C/C++ source editing
//  - Header file editing
//  - Syntax highlighting
//  - Code parsing systems
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
    public class CppLanguage : LanguageDefinition
    {
        private static readonly string[] Keywords = {
            "alignas","alignof","and","and_eq","asm","auto","bitand","bitor","bool","break",
            "case","catch","char","char8_t","char16_t","char32_t","class","compl","concept",
            "const","consteval","constexpr","constinit","const_cast","continue","co_await",
            "co_return","co_yield","decltype","default","delete","do","double","dynamic_cast",
            "else","enum","explicit","export","extern","false","float","for","friend","goto",
            "if","inline","int","long","mutable","namespace","new","noexcept","not","not_eq",
            "nullptr","operator","or","or_eq","private","protected","public","register",
            "reinterpret_cast","requires","return","short","signed","sizeof","static",
            "static_assert","static_cast","struct","switch","template","this","thread_local",
            "throw","true","try","typedef","typeid","typename","union","unsigned","using",
            "virtual","void","volatile","wchar_t","while","xor","xor_eq","override","final"
        };

        public CppLanguage()
        {
            Name = "C++";
            CommentSingleLine = "//";
            CommentMultiLineStart = "/*";
            CommentMultiLineEnd = "*/";
            IndentChars = "    ";
            AddExtension(".cpp");
            AddExtension(".cc");
            AddExtension(".cxx");
            AddExtension(".c");
            AddExtension(".h");
            AddExtension(".hpp");
            AddExtension(".hxx");

            AddRule(new SyntaxRule("BlockComment",  @"/\*[\s\S]*?\*/",  TokenType.Comment, 10));
            AddRule(new SyntaxRule("LineComment",   @"//[^\n]*",        TokenType.Comment, 9));
            AddRule(new SyntaxRule("RawString",     @"R""[^\(]*\([\s\S]*?\)[^\)]*""", TokenType.String, 9));
            AddRule(new SyntaxRule("String",        @"""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])'", TokenType.String, 8));
            AddRule(new SyntaxRule("Preprocessor",  @"^\s*#\w+[^\n]*",  TokenType.Preprocessor, 7));
            string kwPat = @"\b(" + string.Join("|", Keywords) + @")\b";
            AddRule(new SyntaxRule("Keywords",  kwPat, TokenType.Keyword, 6));
            AddRule(new SyntaxRule("Type",      @"\b(?:std::\w+|[A-Z][A-Za-z0-9_]*)\b", TokenType.Type, 5));
            AddRule(new SyntaxRule("Number",    @"\b(?:0x[0-9a-fA-F]+|0b[01]+|\d+\.?\d*(?:[eE][+-]?\d+)?[uUlLfF]*)\b", TokenType.Number, 4));
            AddRule(new SyntaxRule("Operator",  @"[+\-*/%&|^~!=<>?:]+|->|\.\.\.|::", TokenType.Operator, 1));
        }

        public override bool ShouldAutoIndent(string lineText) { return lineText.TrimEnd().EndsWith("{"); }

        public override Dictionary<char, char> GetBracketPairs()
        {
            Dictionary<char, char> pairs = base.GetBracketPairs();
            pairs.Add('<', '>');
            return pairs;
        }
    }
}

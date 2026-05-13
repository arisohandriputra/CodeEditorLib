// =============================================================================
//  PHP Language Definition for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides PHP language configuration used by the
//  editor syntax highlighting and parsing engine.
//
//  This file is responsible for:
//  - Defining PHP language metadata
//  - Registering PHP source file extensions
//  - Configuring syntax highlighting rules
//  - Detecting comments, variables, and strings
//  - Detecting PHP keywords and operators
//  - Supporting automatic indentation behavior
//
// -----------------------------------------------------------------------------
//
//  Supported File Extensions:
//  - .php
//  - .phtml
//  - .php3
//  - .php4
//  - .php5
//
// -----------------------------------------------------------------------------
//
//  Syntax Highlighting Features:
//  - Single-line comments      -> // comment
//  - Hash comments             -> # comment
//  - Multi-line comments       -> /* comment */
//  - Standard string literals  -> "text"
//  - Single-quoted strings     -> 'text'
//  - HereDoc blocks
//  - PHP variables             -> $variable
//  - PHP keywords              -> function, class, namespace
//  - Numeric literals
//  - Operators and expressions
//
// -----------------------------------------------------------------------------
//
//  Keyword Support:
//  Includes standard and modern PHP keywords such as:
//  - function
//  - trait
//  - namespace
//  - readonly
//  - match
//  - yield
//  - fn
//
//  Compatible with multiple PHP language versions.
//
// -----------------------------------------------------------------------------
//
//  Variable Recognition:
//  Detects PHP variable identifiers using
//  dollar-sign notation.
//
//  Examples:
//  - $name
//  - $userData
//  - $config
//
// -----------------------------------------------------------------------------
//
//  PHP Tag Detection:
//  Detects PHP opening and closing tags.
//
//  Examples:
//  - <?php
//  - <?=
//  - ?>
//
// -----------------------------------------------------------------------------
//
//  String Handling:
//  Supports multiple PHP string styles:
//
//  Examples:
//  - Standard strings
//  - Single-quoted strings
//  - HereDoc syntax
//
// -----------------------------------------------------------------------------
//
//  Auto Indentation:
//  Automatically increases indentation after lines
//  ending with opening braces '{'.
//
//  Example:
//      if ($ready)
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
//  - Variables are highlighted separately
//  - Accurate token classification
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - PHP source code editing
//  - Web backend development
//  - Server-side scripting
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
    public class PhpLanguage : LanguageDefinition
    {
        private static readonly string[] Keywords = {
            "abstract","and","array","as","break","callable","case","catch","class","clone",
            "const","continue","declare","default","die","do","echo","else","elseif","empty",
            "enddeclare","endfor","endforeach","endif","endswitch","endwhile","eval","exit",
            "extends","final","finally","fn","for","foreach","function","global","goto","if",
            "implements","include","include_once","instanceof","insteadof","interface","isset",
            "list","match","namespace","new","or","print","private","protected","public",
            "readonly","require","require_once","return","static","switch","throw","trait",
            "try","unset","use","var","while","xor","yield","true","false","null","NULL","TRUE","FALSE"
        };

        public PhpLanguage()
        {
            Name = "PHP";
            CommentSingleLine = "//";
            CommentMultiLineStart = "/*";
            CommentMultiLineEnd = "*/";
            IndentChars = "    ";
            AddExtension(".php");
            AddExtension(".phtml");
            AddExtension(".php3");
            AddExtension(".php4");
            AddExtension(".php5");

            AddRule(new SyntaxRule("BlockComment",  @"/\*[\s\S]*?\*/",     TokenType.Comment, 10));
            AddRule(new SyntaxRule("LineComment",   @"(?://|#)[^\n]*",     TokenType.Comment, 9));
            AddRule(new SyntaxRule("HereDoc",       @"<<<\w+[\s\S]*?\n\w+;", TokenType.String, 8));
            AddRule(new SyntaxRule("String",        @"""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])*'", TokenType.String, 7));
            AddRule(new SyntaxRule("Variable",      @"\$\w+",              TokenType.Identifier, 7));
            string kwPat = @"\b(" + string.Join("|", Keywords) + @")\b";
            AddRule(new SyntaxRule("Keywords",  kwPat, TokenType.Keyword, 6));
            AddRule(new SyntaxRule("OpenTag",   @"<\?php|<\?=|\?>",        TokenType.Preprocessor, 6));
            AddRule(new SyntaxRule("Type",      @"\b[A-Z][A-Za-z0-9_]*\b",TokenType.Type, 5));
            AddRule(new SyntaxRule("Number",    @"\b\d+\.?\d*\b|0x[0-9a-fA-F]+\b", TokenType.Number, 4));
            AddRule(new SyntaxRule("Operator",  @"[+\-*/%&|^~!=<>?:]+|\.\.",TokenType.Operator, 1));
        }

        public override bool ShouldAutoIndent(string lineText) { return lineText.TrimEnd().EndsWith("{"); }
    }
}

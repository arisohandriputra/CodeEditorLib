// =============================================================================
//  Rust Language Definition for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides Rust language configuration used by the
//  editor syntax highlighting and parsing engine.
//
//  This file is responsible for:
//  - Defining Rust language metadata
//  - Registering Rust source file extensions
//  - Configuring syntax highlighting rules
//  - Detecting comments, attributes, and macros
//  - Detecting Rust keywords and standard types
//  - Supporting automatic indentation behavior
//
// -----------------------------------------------------------------------------
//
//  Supported File Extensions:
//  - .rs
//
// -----------------------------------------------------------------------------
//
//  Syntax Highlighting Features:
//  - Single-line comments      -> // comment
//  - Documentation comments    -> /// docs
//  - Multi-line comments       -> /* comment */
//  - Standard string literals  -> "text"
//  - Raw string literals
//  - Attributes                -> #[derive(Debug)]
//  - Lifetimes                 -> 'a
//  - Macros                    -> println!
//  - Rust keywords             -> fn, impl, trait
//  - Standard library types
//  - Numeric literals
//  - Operators and expressions
//
// -----------------------------------------------------------------------------
//
//  Keyword Support:
//  Includes modern Rust keywords such as:
//  - async
//  - await
//  - trait
//  - impl
//  - unsafe
//  - crate
//  - match
//  - move
//
//  Compatible with modern Rust syntax.
//
// -----------------------------------------------------------------------------
//
//  Type Recognition:
//  Detects common Rust primitive types,
//  collections, traits, and standard library items.
//
//  Examples:
//  - String
//  - Vec
//  - HashMap
//  - Result
//  - Option
//  - Mutex
//
// -----------------------------------------------------------------------------
//
//  Macro Detection:
//  Detects Rust macros using exclamation-mark syntax.
//
//  Examples:
//  - println!
//  - vec!
//  - format!
//  - dbg!
//
// -----------------------------------------------------------------------------
//
//  Attribute Support:
//  Detects Rust attributes and compiler directives.
//
//  Examples:
//  - #[derive(Debug)]
//  - #![allow(dead_code)]
//
// -----------------------------------------------------------------------------
//
//  Lifetime Recognition:
//  Detects Rust lifetime annotations.
//
//  Examples:
//  - 'a
//  - 'static
//
// -----------------------------------------------------------------------------
//
//  Numeric Literal Support:
//  Detects multiple Rust number formats.
//
//  Examples:
//  - Decimal       -> 123
//  - Hexadecimal   -> 0xFF
//  - Binary        -> 0b1010
//  - Float         -> 3.14f64
//
// -----------------------------------------------------------------------------
//
//  Auto Indentation:
//  Automatically increases indentation after lines
//  ending with opening braces '{'.
//
//  Example:
//      fn main() {
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
//  - Macros are highlighted separately
//  - Attributes and lifetimes are parsed correctly
//  - Accurate token classification
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - Rust source code editing
//  - Systems programming
//  - High-performance application development
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
    public class RustLanguage : LanguageDefinition
    {
        private static readonly string[] Keywords = {
            "as","async","await","break","const","continue","crate","dyn","else",
            "enum","extern","false","fn","for","if","impl","in","let","loop",
            "match","mod","move","mut","pub","ref","return","self","Self","static",
            "struct","super","trait","true","type","union","unsafe","use","where",
            "while","abstract","become","box","do","final","macro","override",
            "priv","try","typeof","unsized","virtual","yield"
        };

        private static readonly string[] Types = {
            "bool","char","f32","f64","i8","i16","i32","i64","i128","isize",
            "str","u8","u16","u32","u64","u128","usize","String","Vec","HashMap",
            "HashSet","BTreeMap","BTreeSet","Option","Result","Box","Rc","Arc",
            "Cell","RefCell","Mutex","RwLock","Ok","Err","Some","None","panic",
            "println","print","eprintln","eprint","format","vec","assert",
            "assert_eq","assert_ne","todo","unimplemented","unreachable","dbg",
            "Clone","Copy","Debug","Default","Display","Drop","Eq","From","Into",
            "Iterator","PartialEq","PartialOrd","Ord","Hash","Send","Sync",
            "Sized","Fn","FnMut","FnOnce","Future","Stream","Read","Write",
            "BufRead","Seek","Error","Path","PathBuf","File","BufReader","BufWriter"
        };

        public RustLanguage()
        {
            Name = "Rust";
            CommentSingleLine     = "//";
            CommentMultiLineStart = "/*";
            CommentMultiLineEnd   = "*/";
            IndentChars = "    ";
            AddExtension(".rs");

            AddRule(new SyntaxRule("BlockComment",  @"/\*[\s\S]*?\*/",                                          TokenType.Comment,     10));
            AddRule(new SyntaxRule("DocComment",    @"/{2,3}[^\n]*",                                            TokenType.Comment,      9));
            AddRule(new SyntaxRule("RawString",     @"r#+""[\s\S]*?""+#|r""[^""]*""",                           TokenType.String,       8));
            AddRule(new SyntaxRule("String",        @"""(?:\\[\s\S]|[^""\\])*""|'(?:\\[\s\S]|[^'\\])'",         TokenType.String,       7));
            AddRule(new SyntaxRule("Attribute",     @"#!?\[[\s\S]*?\]",                                         TokenType.Attribute,    6));
            AddRule(new SyntaxRule("Lifetime",      @"'[a-z_]\w*\b",                                            TokenType.Attribute,    5));
            AddRule(new SyntaxRule("Macro",         @"\b\w+!(?=[\s\[({{])",                                     TokenType.Preprocessor, 5));
            AddRule(new SyntaxRule("Keywords",      @"\b(" + string.Join("|", Keywords) + @")\b",               TokenType.Keyword,      4));
            AddRule(new SyntaxRule("Types",         @"\b(" + string.Join("|", Types)    + @")\b",               TokenType.Type,         3));
            AddRule(new SyntaxRule("Number",        @"\b(?:0x[0-9a-fA-F_]+|0b[01_]+|0o[0-7_]+|\d[\d_]*\.?[\d_]*(?:[eE][+-]?[\d_]+)?(?:_?[iu]\d+|_?f(?:32|64))?)\b",
                                                                                                                 TokenType.Number,       2));
            AddRule(new SyntaxRule("Operator",      @"[+\-*/%&|^~!=<>?:]+|=>|->|\.\.|\.\.=",                   TokenType.Operator,     1));
        }

        public override bool ShouldAutoIndent(string lineText)
        {
            return lineText.TrimEnd().EndsWith("{");
        }
    }
}

// =============================================================================
//  Syntax Rule for CodeEditorLib
// =============================================================================
//
//  Description :
//  Defines a syntax highlighting rule used by the editor
//  for detecting and classifying source code tokens.
//
//  Features:
//  - Regex-based pattern matching
//  - Token type assignment
//  - Rule priority support
//  - Custom regex option handling
//  - Named syntax rule identification
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
using System.Text.RegularExpressions;

namespace CodeEditorLib.Core
{
    public class SyntaxRule
    {
        private Regex _regex;
        private TokenType _tokenType;
        private string _name;
        private int _priority;

        public SyntaxRule(string name, string pattern, TokenType tokenType)
            : this(name, pattern, tokenType, 0)
        {
        }

        public SyntaxRule(string name, string pattern, TokenType tokenType, int priority)
        {
            _name = name;
            _tokenType = tokenType;
            _priority = priority;
            _regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.Multiline);
        }

        public SyntaxRule(string name, string pattern, TokenType tokenType, RegexOptions options)
        {
            _name = name;
            _tokenType = tokenType;
            _priority = 0;
            _regex = new Regex(pattern, options | RegexOptions.Compiled | RegexOptions.Multiline);
        }

        public string Name { get { return _name; } }

        public TokenType TokenType { get { return _tokenType; } }

        public Regex Regex { get { return _regex; } }

        public int Priority { get { return _priority; } }
    }
}

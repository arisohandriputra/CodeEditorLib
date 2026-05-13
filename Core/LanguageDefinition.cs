// =============================================================================
//  Language Definition for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides the base structure for defining programming language
//  configurations used by the editor.
//
//  Features:
//  - Syntax highlighting rule registration
//  - File extension association
//  - Single-line and multi-line comment definitions
//  - Automatic indentation support
//  - Bracket pair configuration
//  - Custom language behavior support
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
using System.Text.RegularExpressions;

namespace CodeEditorLib.Core
{
    public abstract class LanguageDefinition
    {
        private List<SyntaxRule> _rules;
        private List<string> _fileExtensions;
        private string _commentSingleLine;
        private string _commentMultiLineStart;
        private string _commentMultiLineEnd;
        private string _name;
        private string _indentChars;

        protected LanguageDefinition()
        {
            _rules          = new List<SyntaxRule>();
            _fileExtensions = new List<string>();
            _indentChars    = "    ";
        }

        public string Name                { get { return _name; }               protected set { _name = value; } }
        public IList<string> FileExtensions { get { return _fileExtensions; } }
        public IList<SyntaxRule> Rules    { get { return _rules; } }
        public string CommentSingleLine   { get { return _commentSingleLine; }  protected set { _commentSingleLine = value; } }
        public string CommentMultiLineStart { get { return _commentMultiLineStart; } protected set { _commentMultiLineStart = value; } }
        public string CommentMultiLineEnd { get { return _commentMultiLineEnd; } protected set { _commentMultiLineEnd = value; } }
        public string IndentChars         { get { return _indentChars; }        protected set { _indentChars = value; } }

        public virtual Dictionary<char, char> GetBracketPairs()
        {
            Dictionary<char, char> pairs = new Dictionary<char, char>();
            pairs.Add('(', ')');
            pairs.Add('{', '}');
            pairs.Add('[', ']');
            return pairs;
        }

        protected void AddRule(SyntaxRule rule)      { _rules.Add(rule); }
        protected void AddExtension(string ext)
        {
            if (!ext.StartsWith(".")) ext = "." + ext;
            _fileExtensions.Add(ext.ToLower());
        }

        public virtual bool ShouldAutoIndent(string lineText) { return false; }
    }
}

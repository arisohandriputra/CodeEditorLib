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

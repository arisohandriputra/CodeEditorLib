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

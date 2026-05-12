using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CodeEditorLib.Core
{
    public class SyntaxHighlighter
    {
        private LanguageDefinition _language;

        public SyntaxHighlighter() { }

        public SyntaxHighlighter(LanguageDefinition language)
        {
            _language = language;
        }

        public LanguageDefinition Language
        {
            get { return _language; }
            set { _language = value; }
        }

        public List<SyntaxToken> Tokenize(string text)
        {
            if (_language == null || string.IsNullOrEmpty(text))
                return new List<SyntaxToken>();

            bool[] claimed = new bool[text.Length];
            List<SyntaxToken> tokens = new List<SyntaxToken>();

            List<SyntaxRule> sortedRules = new List<SyntaxRule>(_language.Rules);
            sortedRules.Sort(delegate(SyntaxRule a, SyntaxRule b) {
                return b.Priority.CompareTo(a.Priority);
            });

            foreach (SyntaxRule rule in sortedRules)
            {
                MatchCollection matches = rule.Regex.Matches(text);
                foreach (Match match in matches)
                {
                    if (match.Length == 0) continue;

                    bool overlaps = false;
                    for (int i = match.Index; i < match.Index + match.Length && i < claimed.Length; i++)
                    {
                        if (claimed[i]) { overlaps = true; break; }
                    }
                    if (overlaps) continue;

                    for (int i = match.Index; i < match.Index + match.Length && i < claimed.Length; i++)
                        claimed[i] = true;

                    tokens.Add(new SyntaxToken(match.Index, match.Length, rule.TokenType, match.Value));
                }
            }

            tokens.Sort(delegate(SyntaxToken a, SyntaxToken b) {
                return a.StartIndex.CompareTo(b.StartIndex);
            });

            return tokens;
        }

        public List<SyntaxToken> TokenizeLine(string lineText, int lineStartOffset)
        {
            List<SyntaxToken> all = Tokenize(lineText);
            List<SyntaxToken> adjusted = new List<SyntaxToken>();
            foreach (SyntaxToken t in all)
                adjusted.Add(new SyntaxToken(t.StartIndex + lineStartOffset, t.Length, t.TokenType, t.Text));
            return adjusted;
        }
    }
}

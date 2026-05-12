using System;
using System.Collections.Generic;
using CodeEditorLib.Core;

namespace CodeEditorLib.Highlighting
{
    public class HighlightScheme
    {
        private Dictionary<TokenType, HighlightStyle> _styles;
        private string _name;

        public HighlightScheme(string name)
        {
            _name = name;
            _styles = new Dictionary<TokenType, HighlightStyle>();
        }

        public string Name { get { return _name; } }

        public void SetStyle(TokenType tokenType, HighlightStyle style)
        {
            _styles[tokenType] = style;
        }

        public HighlightStyle GetStyle(TokenType tokenType)
        {
            HighlightStyle style;
            if (_styles.TryGetValue(tokenType, out style))
                return style;
            return HighlightStyle.Default;
        }

        public bool HasStyle(TokenType tokenType)
        {
            return _styles.ContainsKey(tokenType);
        }
    }
}

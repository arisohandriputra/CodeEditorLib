using System;

namespace CodeEditorLib.Core
{
    public enum TokenType
    {
        Default,
        Keyword,
        String,
        Comment,
        Number,
        Operator,
        Preprocessor,
        Type,
        Identifier,
        Whitespace,
        Delimiter,
        Attribute,
        XmlTag,
        XmlAttribute,
        XmlValue,
        HtmlTag,
        HtmlAttribute,
        HtmlValue,
        CssProperty,
        CssValue,
        CssSelector,
        SqlKeyword,
        SqlFunction
    }

    public class SyntaxToken
    {
        private int _startIndex;
        private int _length;
        private TokenType _tokenType;
        private string _text;

        public SyntaxToken(int startIndex, int length, TokenType tokenType, string text)
        {
            _startIndex = startIndex;
            _length = length;
            _tokenType = tokenType;
            _text = text;
        }

        public int StartIndex { get { return _startIndex; } }

        public int Length { get { return _length; } }

        public int EndIndex { get { return _startIndex + _length; } }

        public TokenType TokenType { get { return _tokenType; } }

        public string Text { get { return _text; } }

        public override string ToString()
        {
            return string.Format("[{0}] '{1}' @ {2}+{3}", _tokenType, _text, _startIndex, _length);
        }
    }
}

using System.Drawing;
using CodeEditorLib.Core;
using CodeEditorLib.Highlighting;

namespace CodeEditorLib.Themes
{
    /// <summary>
    /// Dark theme inspired by Monokai / VS Code Dark+.
    /// </summary>
    public class DarkTheme : EditorTheme
    {
        public DarkTheme() : base("Dark") { }

        public override Color BackgroundColor      { get { return Color.FromArgb(30, 30, 30); } }
        public override Color DefaultTextColor     { get { return Color.FromArgb(212, 212, 212); } }
        public override Color SelectionColor       { get { return Color.FromArgb(38, 79, 120); } }
        public override Color CurrentLineColor     { get { return Color.FromArgb(40, 40, 40); } }
        public override Color LineNumberBackColor  { get { return Color.FromArgb(24, 24, 24); } }
        public override Color LineNumberForeColor  { get { return Color.FromArgb(133, 133, 133); } }
        public override Color LineNumberBorderColor{ get { return Color.FromArgb(50, 50, 50); } }
        public override Color CaretColor           { get { return Color.White; } }
        public override Color BracketMatchColor    { get { return Color.FromArgb(80, 80, 0); } }
        public override Color AutoCompleteBackColor{ get { return Color.FromArgb(37, 37, 38); } }
        public override Color AutoCompleteForeColor{ get { return Color.FromArgb(212, 212, 212); } }
        public override Color AutoCompleteSelectionColor { get { return Color.FromArgb(0, 120, 215); } }

        protected override void BuildScheme(HighlightScheme scheme)
        {
            // Keywords - blue
            scheme.SetStyle(TokenType.Keyword,      new HighlightStyle(Color.FromArgb(86, 156, 214), true));
            // Strings - orange/brown
            scheme.SetStyle(TokenType.String,       new HighlightStyle(Color.FromArgb(206, 145, 120)));
            // Comments - green, italic
            scheme.SetStyle(TokenType.Comment,      new HighlightStyle(Color.FromArgb(106, 153, 85), false));
            // Numbers - light green
            scheme.SetStyle(TokenType.Number,       new HighlightStyle(Color.FromArgb(181, 206, 168)));
            // Types/classes - teal
            scheme.SetStyle(TokenType.Type,         new HighlightStyle(Color.FromArgb(78, 201, 176)));
            // Operators
            scheme.SetStyle(TokenType.Operator,     new HighlightStyle(Color.FromArgb(212, 212, 212)));
            // Preprocessor - gray-purple
            scheme.SetStyle(TokenType.Preprocessor, new HighlightStyle(Color.FromArgb(155, 155, 155)));
            // Identifiers
            scheme.SetStyle(TokenType.Identifier,   new HighlightStyle(Color.FromArgb(156, 220, 254)));
            // Attribute
            scheme.SetStyle(TokenType.Attribute,    new HighlightStyle(Color.FromArgb(78, 201, 176)));
            // XML/HTML
            scheme.SetStyle(TokenType.XmlTag,       new HighlightStyle(Color.FromArgb(86, 156, 214)));
            scheme.SetStyle(TokenType.XmlAttribute, new HighlightStyle(Color.FromArgb(156, 220, 254)));
            scheme.SetStyle(TokenType.XmlValue,     new HighlightStyle(Color.FromArgb(206, 145, 120)));
            scheme.SetStyle(TokenType.HtmlTag,      new HighlightStyle(Color.FromArgb(86, 156, 214)));
            scheme.SetStyle(TokenType.HtmlAttribute,new HighlightStyle(Color.FromArgb(156, 220, 254)));
            scheme.SetStyle(TokenType.HtmlValue,    new HighlightStyle(Color.FromArgb(206, 145, 120)));
            // CSS
            scheme.SetStyle(TokenType.CssSelector,  new HighlightStyle(Color.FromArgb(215, 186, 125)));
            scheme.SetStyle(TokenType.CssProperty,  new HighlightStyle(Color.FromArgb(156, 220, 254)));
            scheme.SetStyle(TokenType.CssValue,     new HighlightStyle(Color.FromArgb(206, 145, 120)));
            // SQL
            scheme.SetStyle(TokenType.SqlKeyword,   new HighlightStyle(Color.FromArgb(86, 156, 214), true));
            scheme.SetStyle(TokenType.SqlFunction,  new HighlightStyle(Color.FromArgb(220, 220, 170)));
        }
    }
}

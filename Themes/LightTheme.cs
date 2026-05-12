using System.Drawing;
using CodeEditorLib.Core;
using CodeEditorLib.Highlighting;

namespace CodeEditorLib.Themes
{
    /// <summary>
    /// Light theme inspired by Visual Studio / IntelliJ Light.
    /// </summary>
    public class LightTheme : EditorTheme
    {
        public LightTheme() : base("Light") { }

        public override Color BackgroundColor      { get { return Color.White; } }
        public override Color DefaultTextColor     { get { return Color.Black; } }
        public override Color SelectionColor       { get { return Color.FromArgb(173, 214, 255); } }
        public override Color CurrentLineColor     { get { return Color.FromArgb(240, 248, 255); } }
        public override Color LineNumberBackColor  { get { return Color.FromArgb(238, 238, 238); } }
        public override Color LineNumberForeColor  { get { return Color.FromArgb(120, 120, 120); } }
        public override Color LineNumberBorderColor{ get { return Color.FromArgb(180, 180, 180); } }
        public override Color CaretColor           { get { return Color.Black; } }
        public override Color BracketMatchColor    { get { return Color.FromArgb(230, 230, 150); } }
        public override Color AutoCompleteBackColor{ get { return Color.White; } }
        public override Color AutoCompleteForeColor{ get { return Color.Black; } }
        public override Color AutoCompleteSelectionColor { get { return Color.FromArgb(0, 120, 215); } }

        protected override void BuildScheme(HighlightScheme scheme)
        {
            scheme.SetStyle(TokenType.Keyword,      new HighlightStyle(Color.FromArgb(0, 0, 255), true));
            scheme.SetStyle(TokenType.String,       new HighlightStyle(Color.FromArgb(163, 21, 21)));
            scheme.SetStyle(TokenType.Comment,      new HighlightStyle(Color.FromArgb(0, 128, 0)));
            scheme.SetStyle(TokenType.Number,       new HighlightStyle(Color.FromArgb(9, 134, 88)));
            scheme.SetStyle(TokenType.Type,         new HighlightStyle(Color.FromArgb(43, 145, 175)));
            scheme.SetStyle(TokenType.Operator,     new HighlightStyle(Color.Black));
            scheme.SetStyle(TokenType.Preprocessor, new HighlightStyle(Color.FromArgb(128, 128, 128)));
            scheme.SetStyle(TokenType.Identifier,   new HighlightStyle(Color.Black));
            scheme.SetStyle(TokenType.Attribute,    new HighlightStyle(Color.FromArgb(43, 145, 175)));
            scheme.SetStyle(TokenType.XmlTag,       new HighlightStyle(Color.FromArgb(128, 0, 0)));
            scheme.SetStyle(TokenType.XmlAttribute, new HighlightStyle(Color.FromArgb(255, 0, 0)));
            scheme.SetStyle(TokenType.XmlValue,     new HighlightStyle(Color.FromArgb(0, 0, 255)));
            scheme.SetStyle(TokenType.HtmlTag,      new HighlightStyle(Color.FromArgb(128, 0, 0)));
            scheme.SetStyle(TokenType.HtmlAttribute,new HighlightStyle(Color.FromArgb(255, 0, 0)));
            scheme.SetStyle(TokenType.HtmlValue,    new HighlightStyle(Color.FromArgb(0, 0, 255)));
            scheme.SetStyle(TokenType.CssSelector,  new HighlightStyle(Color.FromArgb(128, 0, 0)));
            scheme.SetStyle(TokenType.CssProperty,  new HighlightStyle(Color.FromArgb(255, 0, 0)));
            scheme.SetStyle(TokenType.CssValue,     new HighlightStyle(Color.FromArgb(0, 0, 255)));
            scheme.SetStyle(TokenType.SqlKeyword,   new HighlightStyle(Color.FromArgb(0, 0, 255), true));
            scheme.SetStyle(TokenType.SqlFunction,  new HighlightStyle(Color.FromArgb(128, 0, 128)));
        }
    }
}

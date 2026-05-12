using System;
using CodeEditorLib.Core;

namespace CodeEditorLib.Languages
{
    public class CssLanguage : LanguageDefinition
    {
        public CssLanguage()
        {
            Name = "CSS";
            CommentMultiLineStart = "/*";
            CommentMultiLineEnd = "*/";
            IndentChars = "  ";
            AddExtension(".css");
            AddExtension(".scss");
            AddExtension(".less");
            AddExtension(".sass");

            AddRule(new SyntaxRule("Comment",    @"/\*[\s\S]*?\*/",                    TokenType.Comment, 10));
            AddRule(new SyntaxRule("AtRule",     @"@[\w\-]+",                           TokenType.Preprocessor, 9));
            AddRule(new SyntaxRule("Selector",   @"(?:^|\})\s*([^{]+)(?=\s*\{)",       TokenType.CssSelector, 8));
            AddRule(new SyntaxRule("CssValue",   @"(?<=:)\s*[^;{}]+",                  TokenType.CssValue, 7));
            AddRule(new SyntaxRule("CssProp",    @"[\w\-]+(?=\s*:)",                   TokenType.CssProperty, 6));
            AddRule(new SyntaxRule("Color",      @"#(?:[0-9a-fA-F]{3}|[0-9a-fA-F]{6})\b", TokenType.Number, 5));
            AddRule(new SyntaxRule("Number",     @"\b\d+\.?\d*(?:px|em|rem|%|vh|vw|pt|cm|mm|s|ms)?\b", TokenType.Number, 4));
            AddRule(new SyntaxRule("String",     @"""[^""]*""|'[^']*'",                 TokenType.String, 3));
        }

        public override bool ShouldAutoIndent(string lineText) { return lineText.TrimEnd().EndsWith("{"); }
    }
}

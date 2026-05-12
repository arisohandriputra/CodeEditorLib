using System;
using CodeEditorLib.Core;

namespace CodeEditorLib.Languages
{
    public class HtmlLanguage : LanguageDefinition
    {
        public HtmlLanguage()
        {
            Name = "HTML";
            CommentMultiLineStart = "<!--";
            CommentMultiLineEnd = "-->";
            IndentChars = "  ";
            AddExtension(".html");
            AddExtension(".htm");
            AddExtension(".xhtml");

            AddRule(new SyntaxRule("Comment",      @"<!--[\s\S]*?-->",  TokenType.Comment, 10));
            AddRule(new SyntaxRule("Doctype",      @"<!DOCTYPE[^>]*>",  TokenType.Preprocessor, 9));
            AddRule(new SyntaxRule("ScriptTag",    @"<script[\s\S]*?</script>", TokenType.Delimiter, 8));
            AddRule(new SyntaxRule("AttrValue",    @"(?<==)(?:""[^""]*""|'[^']*')", TokenType.HtmlValue, 7));
            AddRule(new SyntaxRule("AttrName",     @"\b\w[\w\-]*(?=\s*=)", TokenType.HtmlAttribute, 6));
            AddRule(new SyntaxRule("Tags",         @"</?[A-Za-z][\w\-]*|/?>", TokenType.HtmlTag, 5));
            AddRule(new SyntaxRule("Entity",       @"&[A-Za-z#\d]+;",   TokenType.Number, 4));
        }

        public override bool ShouldAutoIndent(string lineText)
        {
            string t = lineText.TrimEnd().ToLower();
            return t.EndsWith(">") && !t.Contains("</") && !t.EndsWith("/>") &&
                   (t.Contains("<div") || t.Contains("<section") || t.Contains("<ul") ||
                    t.Contains("<ol") || t.Contains("<table") || t.Contains("<tbody") ||
                    t.Contains("<tr") || t.Contains("<head") || t.Contains("<body") ||
                    t.Contains("<html") || t.Contains("<script") || t.Contains("<style"));
        }
    }
}

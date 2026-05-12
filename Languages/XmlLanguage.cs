using System;
using CodeEditorLib.Core;

namespace CodeEditorLib.Languages
{
    public class XmlLanguage : LanguageDefinition
    {
        public XmlLanguage()
        {
            Name = "XML";
            CommentMultiLineStart = "<!--";
            CommentMultiLineEnd = "-->";
            IndentChars = "  ";
            AddExtension(".xml");
            AddExtension(".xaml");
            AddExtension(".config");
            AddExtension(".resx");
            AddExtension(".csproj");
            AddExtension(".sln");
            AddExtension(".xslt");

            AddRule(new SyntaxRule("Comment",       @"<!--[\s\S]*?-->",            TokenType.Comment, 10));
            AddRule(new SyntaxRule("CData",         @"<!\[CDATA\[[\s\S]*?\]\]>",   TokenType.String, 9));
            AddRule(new SyntaxRule("XmlDecl",       @"<\?xml[\s\S]*?\?>",          TokenType.Preprocessor, 8));
            AddRule(new SyntaxRule("AttrValue",     @"""[^""]*""|'[^']*'",         TokenType.XmlValue, 7));
            AddRule(new SyntaxRule("AttrName",      @"\b[\w:\-\.]+(?=\s*=)",       TokenType.XmlAttribute, 6));
            AddRule(new SyntaxRule("Tags",          @"</?[\w:\-\.]+|/?>",          TokenType.XmlTag, 5));
            AddRule(new SyntaxRule("Entity",        @"&[A-Za-z#\d]+;",             TokenType.Number, 4));
        }
    }
}

using System;
using System.Collections.Generic;
using CodeEditorLib.Core;

namespace CodeEditorLib.Languages
{
    public class JavaLanguage : LanguageDefinition
    {
        private static readonly string[] Keywords = {
            "abstract","assert","boolean","break","byte","case","catch","char","class","const",
            "continue","default","do","double","else","enum","extends","final","finally","float",
            "for","goto","if","implements","import","instanceof","int","interface","long","native",
            "new","package","private","protected","public","return","short","static","strictfp",
            "super","switch","synchronized","this","throw","throws","transient","try","void",
            "volatile","while","true","false","null","var","record","sealed","permits"
        };

        public JavaLanguage()
        {
            Name = "Java";
            CommentSingleLine = "//";
            CommentMultiLineStart = "/*";
            CommentMultiLineEnd = "*/";
            IndentChars = "    ";
            AddExtension(".java");

            AddRule(new SyntaxRule("BlockComment", @"/\*[\s\S]*?\*/", TokenType.Comment, 10));
            AddRule(new SyntaxRule("LineComment",  @"//[^\n]*",        TokenType.Comment, 9));
            AddRule(new SyntaxRule("String",       @"""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])'", TokenType.String, 8));
            AddRule(new SyntaxRule("Annotation",   @"@\w+",            TokenType.Attribute, 7));
            string kwPat = @"\b(" + string.Join("|", Keywords) + @")\b";
            AddRule(new SyntaxRule("Keywords", kwPat, TokenType.Keyword, 6));
            AddRule(new SyntaxRule("Type",   @"\b[A-Z][A-Za-z0-9_]*\b", TokenType.Type, 4));
            AddRule(new SyntaxRule("Number", @"\b\d+\.?\d*[fFdDlL]?\b|0x[0-9a-fA-F]+[lL]?\b", TokenType.Number, 3));
            AddRule(new SyntaxRule("Operator", @"[+\-*/%&|^~!=<>?:]+|>>>|<<|>>", TokenType.Operator, 1));
        }

        public override bool ShouldAutoIndent(string lineText) { return lineText.EndsWith("{"); }
    }
}

using System;
using System.Collections.Generic;
using CodeEditorLib.Core;

namespace CodeEditorLib.Languages
{
    public class JavaScriptLanguage : LanguageDefinition
    {
        private static readonly string[] Keywords = {
            "break","case","catch","class","const","continue","debugger",
            "default","delete","do","else","export","extends","false",
            "finally","for","function","if","import","in","instanceof",
            "let","new","null","of","return","static","super","switch",
            "this","throw","true","try","typeof","undefined","var","void",
            "while","with","yield","async","await","from","get","set"
        };

        private static readonly string[] Builtins = {
            "console","document","window","Array","Object","String",
            "Number","Boolean","Math","Date","JSON","Promise","Error",
            "Map","Set","RegExp","Symbol","parseInt","parseFloat",
            "isNaN","isFinite","setTimeout","setInterval","fetch"
        };

        public JavaScriptLanguage()
        {
            Name = "JavaScript";
            CommentSingleLine     = "//";
            CommentMultiLineStart = "/*";
            CommentMultiLineEnd   = "*/";
            IndentChars = "  ";
            AddExtension(".js");
            AddExtension(".jsx");
            AddExtension(".mjs");

            AddRule(new SyntaxRule("BlockComment", @"/\*[\s\S]*?\*/",                          TokenType.Comment,  10));
            AddRule(new SyntaxRule("LineComment",  @"//[^\n]*",                                 TokenType.Comment,   9));
            AddRule(new SyntaxRule("TemplateLit",  @"`(?:\\.|[^`\\])*`",                       TokenType.String,    8));
            AddRule(new SyntaxRule("String",       @"""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])*'",  TokenType.String,    7));
            AddRule(new SyntaxRule("Keywords",     @"\b(" + string.Join("|", Keywords) + @")\b", TokenType.Keyword, 5));
            AddRule(new SyntaxRule("Builtins",     @"\b(" + string.Join("|", Builtins) + @")\b", TokenType.Type,   4));
            AddRule(new SyntaxRule("Number",       @"\b\d+\.?\d*(?:[eE][+-]?\d+)?\b",          TokenType.Number,    3));
            AddRule(new SyntaxRule("Operator",     @"[+\-*/%&|^~!=<>?:]+|=>|\?\.",             TokenType.Operator,  1));
        }

        public override bool ShouldAutoIndent(string lineText)
        {
            return lineText.TrimEnd().EndsWith("{");
        }
    }
}

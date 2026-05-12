using System;
using System.Collections.Generic;
using CodeEditorLib.Core;

namespace CodeEditorLib.Languages
{
    public class PythonLanguage : LanguageDefinition
    {
        private static readonly string[] Keywords = {
            "False","None","True","and","as","assert","async","await",
            "break","class","continue","def","del","elif","else","except",
            "finally","for","from","global","if","import","in","is",
            "lambda","nonlocal","not","or","pass","raise","return",
            "try","while","with","yield"
        };

        private static readonly string[] Builtins = {
            "print","len","range","type","str","int","float","bool",
            "list","dict","set","tuple","input","open","enumerate",
            "zip","map","filter","sorted","reversed","sum","min","max",
            "abs","round","isinstance","issubclass","hasattr","getattr",
            "setattr","dir","vars","repr","format","super","property",
            "staticmethod","classmethod","Exception","ValueError","TypeError"
        };

        public PythonLanguage()
        {
            Name = "Python";
            CommentSingleLine = "#";
            IndentChars = "    ";
            AddExtension(".py");
            AddExtension(".pyw");

            AddRule(new SyntaxRule("Comment",      @"#[^\n]*",                                           TokenType.Comment,   9));
            AddRule(new SyntaxRule("TripleDouble", "\"\"\"[\\s\\S]*?\"\"\"",                             TokenType.Comment,   8));
            AddRule(new SyntaxRule("TripleSingle", @"'''[\s\S]*?'''",                                    TokenType.Comment,   8));
            AddRule(new SyntaxRule("String",       @"""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])*'",            TokenType.String,    7));
            AddRule(new SyntaxRule("Keywords",     @"\b(" + string.Join("|", Keywords) + @")\b",         TokenType.Keyword,   5));
            AddRule(new SyntaxRule("Builtins",     @"\b(" + string.Join("|", Builtins) + @")\b",         TokenType.Type,      4));
            AddRule(new SyntaxRule("Decorator",    @"@\w+",                                               TokenType.Attribute, 3));
            AddRule(new SyntaxRule("Number",       @"\b\d+\.?\d*(?:[eE][+-]?\d+)?\b",                   TokenType.Number,    2));
            AddRule(new SyntaxRule("Operator",     @"[+\-*/%&|^~!=<>]+|//|\*\*",                        TokenType.Operator,  1));
        }

        public override bool ShouldAutoIndent(string lineText)
        {
            return lineText.TrimEnd().EndsWith(":");
        }
    }
}

using System;
using System.Collections.Generic;
using CodeEditorLib.Core;

namespace CodeEditorLib.Languages
{
    public class CSharpLanguage : LanguageDefinition
    {
        private static readonly string[] Keywords = {
            "abstract","as","base","bool","break","byte","case","catch","char",
            "checked","class","const","continue","decimal","default","delegate",
            "do","double","else","enum","event","explicit","extern","false",
            "finally","fixed","float","for","foreach","goto","if","implicit",
            "in","int","interface","internal","is","lock","long","namespace",
            "new","null","object","operator","out","override","params","private",
            "protected","public","readonly","ref","return","sbyte","sealed",
            "short","sizeof","stackalloc","static","string","struct","switch",
            "this","throw","true","try","typeof","uint","ulong","unchecked",
            "unsafe","ushort","using","virtual","void","volatile","while",
            "get","set","value","add","remove","partial","where","yield",
            "async","await","var","dynamic","nameof","when"
        };

        private static readonly string[] Types = {
            "String","Int32","Int64","Boolean","Double","Single","Decimal",
            "Byte","Char","DateTime","TimeSpan","Guid","Exception","Object",
            "List","Dictionary","Array","StringBuilder","Stream","Console",
            "Math","Environment","Thread","Task","Action","Func","Predicate",
            "IEnumerable","ICollection","IList","IDictionary","IDisposable",
            "HashSet","Queue","Stack","Tuple","Nullable","EventArgs",
            "CancellationToken","HttpClient","JsonSerializer"
        };

        public CSharpLanguage()
        {
            Name = "C#";
            CommentSingleLine     = "//";
            CommentMultiLineStart = "/*";
            CommentMultiLineEnd   = "*/";
            IndentChars = "    ";
            AddExtension(".cs");

            AddRule(new SyntaxRule("BlockComment",  @"/\*[\s\S]*?\*/",                                        TokenType.Comment,     10));
            AddRule(new SyntaxRule("LineComment",   @"//[^\n]*",                                               TokenType.Comment,      9));
            AddRule(new SyntaxRule("VerbatimString",@"@""(?:[^""]|"""")*""",                                   TokenType.String,       8));
            AddRule(new SyntaxRule("String",        @"""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])'",                  TokenType.String,       7));
            AddRule(new SyntaxRule("Preprocessor",  @"^\s*#\w+[^\n]*",                                        TokenType.Preprocessor, 6));
            AddRule(new SyntaxRule("Attribute",     @"\[\s*\w[\w\.]*(?:\s*\(.*?\))?\s*\]",                    TokenType.Attribute,    5));
            AddRule(new SyntaxRule("Keywords",      @"\b(" + string.Join("|", Keywords) + @")\b",             TokenType.Keyword,      4));
            AddRule(new SyntaxRule("Types",         @"\b(" + string.Join("|", Types)    + @")\b",             TokenType.Type,         3));
            AddRule(new SyntaxRule("Number",        @"\b(?:0x[0-9a-fA-F]+|\d+\.?\d*(?:[eE][+-]?\d+)?[fFdDmMlLuU]*)\b", TokenType.Number, 2));
            AddRule(new SyntaxRule("Operator",      @"[+\-*/%&|^~!=<>?:]+|=>|\?\?|\.\.",                      TokenType.Operator,     1));
        }

        public override bool ShouldAutoIndent(string lineText)
        {
            return lineText.TrimEnd().EndsWith("{") || lineText.TrimEnd().EndsWith(":");
        }
    }
}

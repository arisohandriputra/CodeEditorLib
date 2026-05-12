using System;
using System.Collections.Generic;
using CodeEditorLib.Core;

namespace CodeEditorLib.Languages
{
    public class GoLanguage : LanguageDefinition
    {
        private static readonly string[] Keywords = {
            "break","case","chan","const","continue","default","defer","else",
            "fallthrough","for","func","go","goto","if","import","interface",
            "map","package","range","return","select","struct","switch","type",
            "var"
        };

        private static readonly string[] Types = {
            "bool","byte","complex64","complex128","error","float32","float64",
            "int","int8","int16","int32","int64","rune","string","uint","uint8",
            "uint16","uint32","uint64","uintptr","any","comparable",
            "append","cap","clear","close","complex","copy","delete","imag",
            "len","make","max","min","new","panic","print","println","real","recover",
            "Context","WaitGroup","Mutex","RWMutex","Once","Reader","Writer",
            "ReadWriter","Closer","Handler","Request","Response","Server","Client",
            "Buffer","Scanner","Map","Slice","Chan","Func"
        };

        public GoLanguage()
        {
            Name = "Go";
            CommentSingleLine     = "//";
            CommentMultiLineStart = "/*";
            CommentMultiLineEnd   = "*/";
            IndentChars = "\t";
            AddExtension(".go");

            AddRule(new SyntaxRule("BlockComment",  @"/\*[\s\S]*?\*/",                                        TokenType.Comment,     10));
            AddRule(new SyntaxRule("LineComment",   @"//[^\n]*",                                              TokenType.Comment,      9));
            AddRule(new SyntaxRule("RawString",     @"`[^`]*`",                                               TokenType.String,       8));
            AddRule(new SyntaxRule("String",        @"""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])'",                 TokenType.String,       7));
            AddRule(new SyntaxRule("BuildTag",      @"^//\s*go:build[^\n]*|^//\s*\+build[^\n]*",             TokenType.Preprocessor, 6));
            AddRule(new SyntaxRule("Keywords",      @"\b(" + string.Join("|", Keywords) + @")\b",             TokenType.Keyword,      4));
            AddRule(new SyntaxRule("Types",         @"\b(" + string.Join("|", Types)    + @")\b",             TokenType.Type,         3));
            AddRule(new SyntaxRule("Number",        @"\b(?:0x[0-9a-fA-F_]+|0b[01_]+|0o[0-7_]+|\d[\d_]*\.?[\d_]*(?:[eE][+-]?[\d_]+)?(?:i)?)\b",
                                                                                                              TokenType.Number,       2));
            AddRule(new SyntaxRule("Operator",      @"[+\-*/%&|^~!=<>:]+|&\^|<<=|>>=|\.\.\.",               TokenType.Operator,     1));
        }

        public override bool ShouldAutoIndent(string lineText)
        {
            return lineText.TrimEnd().EndsWith("{");
        }
    }
}

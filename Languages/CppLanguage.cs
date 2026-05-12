using System;
using System.Collections.Generic;
using CodeEditorLib.Core;

namespace CodeEditorLib.Languages
{
    public class CppLanguage : LanguageDefinition
    {
        private static readonly string[] Keywords = {
            "alignas","alignof","and","and_eq","asm","auto","bitand","bitor","bool","break",
            "case","catch","char","char8_t","char16_t","char32_t","class","compl","concept",
            "const","consteval","constexpr","constinit","const_cast","continue","co_await",
            "co_return","co_yield","decltype","default","delete","do","double","dynamic_cast",
            "else","enum","explicit","export","extern","false","float","for","friend","goto",
            "if","inline","int","long","mutable","namespace","new","noexcept","not","not_eq",
            "nullptr","operator","or","or_eq","private","protected","public","register",
            "reinterpret_cast","requires","return","short","signed","sizeof","static",
            "static_assert","static_cast","struct","switch","template","this","thread_local",
            "throw","true","try","typedef","typeid","typename","union","unsigned","using",
            "virtual","void","volatile","wchar_t","while","xor","xor_eq","override","final"
        };

        public CppLanguage()
        {
            Name = "C++";
            CommentSingleLine = "//";
            CommentMultiLineStart = "/*";
            CommentMultiLineEnd = "*/";
            IndentChars = "    ";
            AddExtension(".cpp");
            AddExtension(".cc");
            AddExtension(".cxx");
            AddExtension(".c");
            AddExtension(".h");
            AddExtension(".hpp");
            AddExtension(".hxx");

            AddRule(new SyntaxRule("BlockComment",  @"/\*[\s\S]*?\*/",  TokenType.Comment, 10));
            AddRule(new SyntaxRule("LineComment",   @"//[^\n]*",        TokenType.Comment, 9));
            AddRule(new SyntaxRule("RawString",     @"R""[^\(]*\([\s\S]*?\)[^\)]*""", TokenType.String, 9));
            AddRule(new SyntaxRule("String",        @"""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])'", TokenType.String, 8));
            AddRule(new SyntaxRule("Preprocessor",  @"^\s*#\w+[^\n]*",  TokenType.Preprocessor, 7));
            string kwPat = @"\b(" + string.Join("|", Keywords) + @")\b";
            AddRule(new SyntaxRule("Keywords",  kwPat, TokenType.Keyword, 6));
            AddRule(new SyntaxRule("Type",      @"\b(?:std::\w+|[A-Z][A-Za-z0-9_]*)\b", TokenType.Type, 5));
            AddRule(new SyntaxRule("Number",    @"\b(?:0x[0-9a-fA-F]+|0b[01]+|\d+\.?\d*(?:[eE][+-]?\d+)?[uUlLfF]*)\b", TokenType.Number, 4));
            AddRule(new SyntaxRule("Operator",  @"[+\-*/%&|^~!=<>?:]+|->|\.\.\.|::", TokenType.Operator, 1));
        }

        public override bool ShouldAutoIndent(string lineText) { return lineText.TrimEnd().EndsWith("{"); }

        public override Dictionary<char, char> GetBracketPairs()
        {
            Dictionary<char, char> pairs = base.GetBracketPairs();
            pairs.Add('<', '>');
            return pairs;
        }
    }
}

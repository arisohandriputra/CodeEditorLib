using System;
using System.Collections.Generic;
using CodeEditorLib.Core;

namespace CodeEditorLib.Languages
{
    public class PhpLanguage : LanguageDefinition
    {
        private static readonly string[] Keywords = {
            "abstract","and","array","as","break","callable","case","catch","class","clone",
            "const","continue","declare","default","die","do","echo","else","elseif","empty",
            "enddeclare","endfor","endforeach","endif","endswitch","endwhile","eval","exit",
            "extends","final","finally","fn","for","foreach","function","global","goto","if",
            "implements","include","include_once","instanceof","insteadof","interface","isset",
            "list","match","namespace","new","or","print","private","protected","public",
            "readonly","require","require_once","return","static","switch","throw","trait",
            "try","unset","use","var","while","xor","yield","true","false","null","NULL","TRUE","FALSE"
        };

        public PhpLanguage()
        {
            Name = "PHP";
            CommentSingleLine = "//";
            CommentMultiLineStart = "/*";
            CommentMultiLineEnd = "*/";
            IndentChars = "    ";
            AddExtension(".php");
            AddExtension(".phtml");
            AddExtension(".php3");
            AddExtension(".php4");
            AddExtension(".php5");

            AddRule(new SyntaxRule("BlockComment",  @"/\*[\s\S]*?\*/",     TokenType.Comment, 10));
            AddRule(new SyntaxRule("LineComment",   @"(?://|#)[^\n]*",     TokenType.Comment, 9));
            AddRule(new SyntaxRule("HereDoc",       @"<<<\w+[\s\S]*?\n\w+;", TokenType.String, 8));
            AddRule(new SyntaxRule("String",        @"""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])*'", TokenType.String, 7));
            AddRule(new SyntaxRule("Variable",      @"\$\w+",              TokenType.Identifier, 7));
            string kwPat = @"\b(" + string.Join("|", Keywords) + @")\b";
            AddRule(new SyntaxRule("Keywords",  kwPat, TokenType.Keyword, 6));
            AddRule(new SyntaxRule("OpenTag",   @"<\?php|<\?=|\?>",        TokenType.Preprocessor, 6));
            AddRule(new SyntaxRule("Type",      @"\b[A-Z][A-Za-z0-9_]*\b",TokenType.Type, 5));
            AddRule(new SyntaxRule("Number",    @"\b\d+\.?\d*\b|0x[0-9a-fA-F]+\b", TokenType.Number, 4));
            AddRule(new SyntaxRule("Operator",  @"[+\-*/%&|^~!=<>?:]+|\.\.",TokenType.Operator, 1));
        }

        public override bool ShouldAutoIndent(string lineText) { return lineText.TrimEnd().EndsWith("{"); }
    }
}

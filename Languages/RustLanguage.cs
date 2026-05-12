using System;
using System.Collections.Generic;
using CodeEditorLib.Core;

namespace CodeEditorLib.Languages
{
    public class RustLanguage : LanguageDefinition
    {
        private static readonly string[] Keywords = {
            "as","async","await","break","const","continue","crate","dyn","else",
            "enum","extern","false","fn","for","if","impl","in","let","loop",
            "match","mod","move","mut","pub","ref","return","self","Self","static",
            "struct","super","trait","true","type","union","unsafe","use","where",
            "while","abstract","become","box","do","final","macro","override",
            "priv","try","typeof","unsized","virtual","yield"
        };

        private static readonly string[] Types = {
            "bool","char","f32","f64","i8","i16","i32","i64","i128","isize",
            "str","u8","u16","u32","u64","u128","usize","String","Vec","HashMap",
            "HashSet","BTreeMap","BTreeSet","Option","Result","Box","Rc","Arc",
            "Cell","RefCell","Mutex","RwLock","Ok","Err","Some","None","panic",
            "println","print","eprintln","eprint","format","vec","assert",
            "assert_eq","assert_ne","todo","unimplemented","unreachable","dbg",
            "Clone","Copy","Debug","Default","Display","Drop","Eq","From","Into",
            "Iterator","PartialEq","PartialOrd","Ord","Hash","Send","Sync",
            "Sized","Fn","FnMut","FnOnce","Future","Stream","Read","Write",
            "BufRead","Seek","Error","Path","PathBuf","File","BufReader","BufWriter"
        };

        public RustLanguage()
        {
            Name = "Rust";
            CommentSingleLine     = "//";
            CommentMultiLineStart = "/*";
            CommentMultiLineEnd   = "*/";
            IndentChars = "    ";
            AddExtension(".rs");

            AddRule(new SyntaxRule("BlockComment",  @"/\*[\s\S]*?\*/",                                          TokenType.Comment,     10));
            AddRule(new SyntaxRule("DocComment",    @"/{2,3}[^\n]*",                                            TokenType.Comment,      9));
            AddRule(new SyntaxRule("RawString",     @"r#+""[\s\S]*?""+#|r""[^""]*""",                           TokenType.String,       8));
            AddRule(new SyntaxRule("String",        @"""(?:\\[\s\S]|[^""\\])*""|'(?:\\[\s\S]|[^'\\])'",         TokenType.String,       7));
            AddRule(new SyntaxRule("Attribute",     @"#!?\[[\s\S]*?\]",                                         TokenType.Attribute,    6));
            AddRule(new SyntaxRule("Lifetime",      @"'[a-z_]\w*\b",                                            TokenType.Attribute,    5));
            AddRule(new SyntaxRule("Macro",         @"\b\w+!(?=[\s\[({{])",                                     TokenType.Preprocessor, 5));
            AddRule(new SyntaxRule("Keywords",      @"\b(" + string.Join("|", Keywords) + @")\b",               TokenType.Keyword,      4));
            AddRule(new SyntaxRule("Types",         @"\b(" + string.Join("|", Types)    + @")\b",               TokenType.Type,         3));
            AddRule(new SyntaxRule("Number",        @"\b(?:0x[0-9a-fA-F_]+|0b[01_]+|0o[0-7_]+|\d[\d_]*\.?[\d_]*(?:[eE][+-]?[\d_]+)?(?:_?[iu]\d+|_?f(?:32|64))?)\b",
                                                                                                                 TokenType.Number,       2));
            AddRule(new SyntaxRule("Operator",      @"[+\-*/%&|^~!=<>?:]+|=>|->|\.\.|\.\.=",                   TokenType.Operator,     1));
        }

        public override bool ShouldAutoIndent(string lineText)
        {
            return lineText.TrimEnd().EndsWith("{");
        }
    }
}

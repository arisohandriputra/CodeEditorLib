using System;
using System.Collections.Generic;
using CodeEditorLib.Core;

namespace CodeEditorLib.Languages
{
    public class VbLanguage : LanguageDefinition
    {
        private static readonly string[] Keywords = {
            "AddHandler","AddressOf","Alias","And","AndAlso","As","Boolean","ByRef",
            "Byte","ByVal","Call","Case","Catch","CBool","CByte","CChar","CDate",
            "CDbl","CDec","Char","CInt","Class","CLng","CObj","Const","Continue",
            "CSByte","CShort","CSng","CStr","CType","CUInt","CULng","CUShort",
            "Date","Decimal","Declare","Default","Delegate","Dim","DirectCast",
            "Do","Double","Each","Else","ElseIf","End","EndIf","Enum","Erase",
            "Error","Event","Exit","False","Finally","For","Friend","Function",
            "Get","GetType","GetXMLNamespace","Global","GoSub","GoTo","Handles",
            "If","Implements","Imports","In","Inherits","Integer","Interface",
            "Is","IsNot","Let","Lib","Like","Long","Loop","Me","Mod","Module",
            "MustInherit","MustOverride","MyBase","MyClass","Namespace","Narrowing",
            "New","Next","Not","Nothing","NotInheritable","NotOverridable","Object",
            "Of","On","Operator","Option","Optional","Or","OrElse","Out",
            "Overloads","Overridable","Overrides","ParamArray","Partial","Private",
            "Property","Protected","Public","RaiseEvent","ReadOnly","ReDim","REM",
            "RemoveHandler","Resume","Return","SByte","Select","Set","Shadows",
            "Shared","Short","Single","Static","Step","Stop","String","Structure",
            "Sub","SyncLock","Then","Throw","To","True","Try","TryCast","TypeOf",
            "UInteger","ULong","UShort","Using","Variant","Wend","When","While",
            "Widening","With","WithEvents","WriteOnly","Xor","Async","Await",
            "Iterator","Yield"
        };

        private static readonly string[] Types = {
            "Console","Math","String","Integer","Boolean","Double","Single",
            "Long","Short","Byte","Char","Date","Decimal","Object","Exception",
            "List","Dictionary","Array","StringBuilder","Stream","Thread","Task",
            "Action","Func","IEnumerable","ICollection","IList","IDictionary",
            "HashSet","Queue","Stack","Tuple","EventArgs","CancellationToken",
            "HttpClient","JsonSerializer"
        };

        public VbLanguage()
        {
            Name = "VB";
            CommentSingleLine     = "'";
            CommentMultiLineStart = null;
            CommentMultiLineEnd   = null;
            IndentChars = "    ";
            AddExtension(".vb");
            AddExtension(".vbs");

            AddRule(new SyntaxRule("LineComment",  @"(?:'|REM\b)[^\n]*",                                       TokenType.Comment,     9));
            AddRule(new SyntaxRule("String",       @"""(?:[^""\n]|"""")*""",                                    TokenType.String,      8));
            AddRule(new SyntaxRule("Preprocessor", @"^\s*#(?:If|Else|ElseIf|End\s+If|Const|Region|End\s+Region)[^\n]*",
                                                                                                               TokenType.Preprocessor, 6));
            AddRule(new SyntaxRule("Keywords",     @"\b(" + string.Join("|", Keywords) + @")\b",               TokenType.Keyword,      4));
            AddRule(new SyntaxRule("Types",        @"\b(" + string.Join("|", Types)    + @")\b",               TokenType.Type,         3));
            AddRule(new SyntaxRule("Number",       @"\b(?:&H[0-9a-fA-F]+|&O[0-7]+|\d+\.?\d*(?:[eE][+-]?\d+)?[FRDLISUlisur]?)\b",
                                                                                                               TokenType.Number,       2));
            AddRule(new SyntaxRule("Operator",     @"[+\-*\/\\^&|~!=<>?:]+|<<=|>>=",                          TokenType.Operator,     1));
        }

        public override bool ShouldAutoIndent(string lineText)
        {
            string trimmed = lineText.Trim().ToUpper();
            return trimmed.StartsWith("SUB ") || trimmed.StartsWith("FUNCTION ") ||
                   trimmed.StartsWith("CLASS ") || trimmed.StartsWith("MODULE ") ||
                   trimmed.StartsWith("IF ") || trimmed.StartsWith("FOR ") ||
                   trimmed.StartsWith("DO") || trimmed.StartsWith("WHILE ") ||
                   trimmed.StartsWith("WITH ") || trimmed.StartsWith("SELECT ") ||
                   trimmed.StartsWith("TRY") || trimmed.StartsWith("NAMESPACE ");
        }
    }
}

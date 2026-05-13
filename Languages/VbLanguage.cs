// =============================================================================
//  VB Language Definition for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides Visual Basic language configuration used by the
//  editor syntax highlighting and parsing engine.
//
//  This file is responsible for:
//  - Defining VB language metadata
//  - Registering VB source file extensions
//  - Configuring syntax highlighting rules
//  - Detecting comments, strings, and compiler directives
//  - Detecting VB keywords and standard types
//  - Supporting VB-style indentation rules
//
// -----------------------------------------------------------------------------
//
//  Supported File Extensions:
//  - .vb
//  - .vbs
//
// -----------------------------------------------------------------------------
//
//  Syntax Highlighting Features:
//  - Single-line comments      -> ' comment
//  - REM comments              -> REM comment
//  - String literals           -> "text"
//  - Preprocessor directives   -> #If, #Region
//  - VB keywords               -> Sub, Function, Class
//  - VB types                  -> Integer, String
//  - Numeric literals
//  - Operators and expressions
//
// -----------------------------------------------------------------------------
//
//  Keyword Support:
//  Includes full Visual Basic language keywords such as:
//  - Sub
//  - Function
//  - Class
//  - Module
//  - If / ElseIf / Else
//  - Try / Catch / Finally
//  - Select Case
//  - Async / Await
//  - Yield
//
//  Compatible with VB.NET and legacy VBScript-style syntax.
//
// -----------------------------------------------------------------------------
//
//  Type Recognition:
//  Detects common VB runtime and .NET types.
//
//  Examples:
//  - String
//  - Integer
//  - Boolean
//  - List
//  - Dictionary
//  - Task
//
// -----------------------------------------------------------------------------
//
//  Numeric Literal Support:
//  Supports VB numeric formats.
//
//  Examples:
//  - Decimal      -> 123
//  - Hexadecimal   -> &HFF
//  - Octal         -> &O77
//  - Suffix types  -> 123D, 123F
//
// -----------------------------------------------------------------------------
//
//  Preprocessor Support:
//  Detects VB compiler directives.
//
//  Examples:
//  - #If
//  - #Else
//  - #Region
//  - #Const
//
// -----------------------------------------------------------------------------
//
//  Auto Indentation:
//  Automatically increases indentation for constructs like:
//
//  Examples:
//      Sub Test()
//          |
//      End Sub
//
//      If condition Then
//          |
//      End If
//
// -----------------------------------------------------------------------------
//
//  Internal Parsing System:
//  Uses regex-based syntax rules with priority ordering.
//
//  Priority handling ensures:
//  - Comments override keywords
//  - Strings are parsed correctly
//  - Directives are highlighted separately
//  - Accurate token classification
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - VB.NET development
//  - Legacy VB scripting
//  - Windows Forms applications
//  - Syntax highlighting
//  - IDE/editor integration
//
// -----------------------------------------------------------------------------
//
//  Author      : Ari Sohandri Putra
//  Repository  : https://github.com/arisohandriputra/CodeEditorLib
//  License     : MIT License
//  Copyright   : Copyright (c) 2026 Ari Sohandri Putra
//
// -----------------------------------------------------------------------------
//
//  Permission is hereby granted, free of charge, to any person obtaining a
//  copy of this software and associated documentation files (the "Software"),
//  to deal in the Software without restriction, including without limitation
//  the rights to use, copy, modify, merge, publish, distribute, sublicense,
//  and/or sell copies of the Software, and to permit persons to whom the
//  Software is furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//
//  IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
//  CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
//  TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
//  SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// =============================================================================

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

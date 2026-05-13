// =============================================================================
//  SQL Language Definition for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides SQL language configuration used by the
//  editor syntax highlighting and parsing engine.
//
//  This file is responsible for:
//  - Defining SQL language metadata
//  - Registering SQL file extensions
//  - Configuring syntax highlighting rules
//  - Detecting SQL comments and string literals
//  - Detecting SQL keywords and built-in functions
//  - Supporting SQL query syntax parsing
//
// -----------------------------------------------------------------------------
//
//  Supported File Extensions:
//  - .sql
//
// -----------------------------------------------------------------------------
//
//  Syntax Highlighting Features:
//  - Single-line comments      -> -- comment
//  - Multi-line comments       -> /* comment */
//  - SQL string literals       -> 'text'
//  - Unicode strings           -> N'text'
//  - SQL keywords              -> SELECT, INSERT, UPDATE
//  - SQL functions             -> COUNT, SUM, AVG
//  - Numeric literals
//
// -----------------------------------------------------------------------------
//
//  Keyword Support:
//  Includes common SQL commands and clauses such as:
//  - SELECT
//  - INSERT
//  - UPDATE
//  - DELETE
//  - CREATE
//  - ALTER
//  - JOIN
//  - GROUP BY
//  - ORDER BY
//  - TRANSACTION
//
//  Compatible with common SQL dialects.
//
// -----------------------------------------------------------------------------
//
//  SQL Function Recognition:
//  Detects common SQL built-in functions.
//
//  Examples:
//  - COUNT()
//  - SUM()
//  - AVG()
//  - GETDATE()
//  - DATEADD()
//  - ROW_NUMBER()
//
// -----------------------------------------------------------------------------
//
//  String Handling:
//  Supports SQL string literal formats.
//
//  Examples:
//  - 'Hello'
//  - N'Unicode Text'
//  - Escaped quotes -> 'It''s SQL'
//
// -----------------------------------------------------------------------------
//
//  Case-Insensitive Matching:
//  SQL keyword and function matching uses
//  case-insensitive regular expressions.
//
//  Examples:
//  - SELECT
//  - select
//  - Select
//
//  All are recognized correctly.
//
// -----------------------------------------------------------------------------
//
//  Internal Parsing System:
//  Uses regex-based syntax rules with priority ordering.
//
//  Priority handling ensures:
//  - Comments override keywords
//  - Functions are highlighted separately
//  - Strings are parsed correctly
//  - Accurate token classification
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - SQL query editing
//  - Database scripting
//  - Stored procedure development
//  - Syntax highlighting
//  - IDE/editor database integration
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
//  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
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
    public class SqlLanguage : LanguageDefinition
    {
        private static readonly string[] Keywords = {
            "SELECT","FROM","WHERE","INSERT","INTO","VALUES","UPDATE","SET","DELETE",
            "CREATE","TABLE","ALTER","DROP","INDEX","VIEW","PROCEDURE","FUNCTION","TRIGGER",
            "DATABASE","SCHEMA","GRANT","REVOKE","BEGIN","COMMIT","ROLLBACK","TRANSACTION",
            "JOIN","INNER","LEFT","RIGHT","FULL","OUTER","CROSS","ON","AS","DISTINCT",
            "ORDER","BY","GROUP","HAVING","UNION","ALL","INTERSECT","EXCEPT","LIMIT",
            "OFFSET","TOP","ROWNUM","EXISTS","NOT","AND","OR","IN","BETWEEN","LIKE",
            "IS","NULL","CASE","WHEN","THEN","ELSE","END","CAST","CONVERT","COALESCE",
            "IF","WHILE","DECLARE","EXEC","EXECUTE","RETURN","PRIMARY","KEY","FOREIGN",
            "REFERENCES","UNIQUE","CHECK","DEFAULT","CONSTRAINT","WITH","CTE","NOLOCK"
        };

        private static readonly string[] Functions = {
            "COUNT","SUM","AVG","MIN","MAX","ROUND","ABS","CEILING","FLOOR","POWER",
            "SQRT","LEN","LENGTH","SUBSTRING","UPPER","LOWER","TRIM","LTRIM","RTRIM",
            "REPLACE","CHARINDEX","PATINDEX","ISNULL","NULLIF","GETDATE","GETUTCDATE",
            "DATEADD","DATEDIFF","DATENAME","DATEPART","CONVERT","CAST","FORMAT",
            "ROW_NUMBER","RANK","DENSE_RANK","NTILE","LAG","LEAD","FIRST_VALUE","LAST_VALUE"
        };

        public SqlLanguage()
        {
            Name = "SQL";
            CommentSingleLine = "--";
            CommentMultiLineStart = "/*";
            CommentMultiLineEnd = "*/";
            IndentChars = "    ";
            AddExtension(".sql");

            AddRule(new SyntaxRule("BlockComment", @"/\*[\s\S]*?\*/",  TokenType.Comment, 10));
            AddRule(new SyntaxRule("LineComment",  @"--[^\n]*",        TokenType.Comment, 9));
            AddRule(new SyntaxRule("String",       @"N?'(?:''|[^'])*'",TokenType.String, 8));
            string fnPat = @"\b(?i)(" + string.Join("|", Functions) + @")\b";
            AddRule(new SyntaxRule("Functions", fnPat, TokenType.SqlFunction, 7));
            string kwPat = @"\b(?i)(" + string.Join("|", Keywords) + @")\b";
            AddRule(new SyntaxRule("Keywords",  kwPat, TokenType.SqlKeyword, 6));
            AddRule(new SyntaxRule("Number",    @"\b\d+\.?\d*\b",      TokenType.Number, 4));
        }
    }
}

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

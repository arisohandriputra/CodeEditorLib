using System;
using System.Text;

namespace CodeEditorLib.Core
{
    public class CodeFormatter
    {
        private LanguageDefinition _language;

        public CodeFormatter(LanguageDefinition language)
        {
            _language = language;
        }

        public LanguageDefinition Language
        {
            get { return _language; }
            set { _language = value; }
        }

        public string GetIndentForNextLine(string currentLine)
        {
            if (_language == null) return string.Empty;

            string indent = GetLeadingWhitespace(currentLine);
            string trimmed = currentLine.TrimEnd();

            if (_language.ShouldAutoIndent(trimmed))
                indent += _language.IndentChars;

            return indent;
        }

        public static string GetLeadingWhitespace(string line)
        {
            if (string.IsNullOrEmpty(line)) return string.Empty;
            int i = 0;
            while (i < line.Length && (line[i] == ' ' || line[i] == '\t'))
                i++;
            return line.Substring(0, i);
        }

        public string ToggleLineComment(string[] lines)
        {
            if (_language == null || string.IsNullOrEmpty(_language.CommentSingleLine))
                return string.Join(Environment.NewLine, lines);

            string prefix = _language.CommentSingleLine;

            bool allCommented = true;
            foreach (string line in lines)
            {
                string trimmed = line.TrimStart();
                if (!trimmed.StartsWith(prefix)) { allCommented = false; break; }
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (allCommented)
                {
                    int idx = line.IndexOf(prefix);
                    if (idx >= 0)
                    {
                        string afterPrefix = line.Substring(idx + prefix.Length);
                        if (afterPrefix.StartsWith(" ")) afterPrefix = afterPrefix.Substring(1);
                        line = line.Substring(0, idx) + afterPrefix;
                    }
                }
                else
                {
                    string ws = GetLeadingWhitespace(line);
                    string rest = line.Substring(ws.Length);
                    line = ws + prefix + " " + rest;
                }

                if (i > 0) sb.Append(Environment.NewLine);
                sb.Append(line);
            }
            return sb.ToString();
        }

        public string[] IndentLines(string[] lines)
        {
            if (_language == null) return lines;
            string[] result = new string[lines.Length];
            for (int i = 0; i < lines.Length; i++)
                result[i] = _language.IndentChars + lines[i];
            return result;
        }

        public string[] OutdentLines(string[] lines)
        {
            if (_language == null) return lines;
            string indent = _language.IndentChars;
            string[] result = new string[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith(indent))
                    result[i] = lines[i].Substring(indent.Length);
                else if (lines[i].Length > 0 && (lines[i][0] == ' ' || lines[i][0] == '\t'))
                    result[i] = lines[i].Substring(1); 
                else
                    result[i] = lines[i];
            }
            return result;
        }

        public static string TabsToSpaces(string text, int tabSize)
        {
            return text.Replace("\t", new string(' ', tabSize));
        }

        public static string SpacesToTabs(string text, int tabSize)
        {
            string spaces = new string(' ', tabSize);
            StringBuilder sb = new StringBuilder();
            foreach (string line in text.Split('\n'))
            {
                string converted = line;
                while (converted.StartsWith(spaces))
                    converted = "\t" + converted.Substring(spaces.Length);
                sb.AppendLine(converted);
            }
            return sb.ToString().TrimEnd('\r', '\n', ' ');
        }
    }
}

// =============================================================================
//  HTML Language Definition for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides HTML language configuration used by the editor
//  syntax highlighting and parsing engine.
//
//  This file is responsible for:
//  - Defining HTML language metadata
//  - Registering supported HTML file extensions
//  - Configuring syntax highlighting rules
//  - Detecting tags, attributes, and values
//  - Detecting comments and document declarations
//  - Supporting automatic indentation behavior
//
// -----------------------------------------------------------------------------
//
//  Supported File Extensions:
//  - .html
//  - .htm
//  - .xhtml
//
// -----------------------------------------------------------------------------
//
//  Syntax Highlighting Features:
//  - HTML comments             -> <!-- comment -->
//  - DOCTYPE declarations      -> <!DOCTYPE html>
//  - HTML tags                 -> <div>
//  - Closing tags              -> </div>
//  - Self-closing tags         -> <br />
//  - Attribute names           -> class, id
//  - Attribute values          -> "container"
//  - HTML entities             -> &amp;
//  - Script blocks             -> <script>...</script>
//
// -----------------------------------------------------------------------------
//
//  HTML Structure Recognition:
//  Detects common HTML structures such as:
//
//      <div class="container">
//          Content
//      </div>
//
// -----------------------------------------------------------------------------
//
//  Auto Indentation:
//  Automatically increases indentation after opening
//  container-style HTML tags.
//
//  Supported examples:
//  - <div>
//  - <section>
//  - <table>
//  - <body>
//  - <script>
//  - <style>
//
//  Prevents indentation for:
//  - Closing tags
//  - Self-closing tags
//
// -----------------------------------------------------------------------------
//
//  Internal Parsing System:
//  Uses regex-based syntax rules with priority ordering.
//
//  Priority handling ensures:
//  - Comments override tag parsing
//  - Attribute values are detected correctly
//  - Script blocks are isolated properly
//  - Accurate HTML token classification
//
// -----------------------------------------------------------------------------
//
//  Rendering Support:
//  Provides token classification for:
//  - HTML tags
//  - HTML attributes
//  - HTML attribute values
//  - Special entities
//
//  Used by the syntax highlighter to apply custom styling.
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - HTML document editing
//  - Web development
//  - Frontend coding
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
//  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//
//  IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
//  CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
//  TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
//  SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// =============================================================================

using System;
using CodeEditorLib.Core;

namespace CodeEditorLib.Languages
{
    public class HtmlLanguage : LanguageDefinition
    {
        public HtmlLanguage()
        {
            Name = "HTML";
            CommentMultiLineStart = "<!--";
            CommentMultiLineEnd = "-->";
            IndentChars = "  ";
            AddExtension(".html");
            AddExtension(".htm");
            AddExtension(".xhtml");

            AddRule(new SyntaxRule("Comment",      @"<!--[\s\S]*?-->",  TokenType.Comment, 10));
            AddRule(new SyntaxRule("Doctype",      @"<!DOCTYPE[^>]*>",  TokenType.Preprocessor, 9));
            AddRule(new SyntaxRule("ScriptTag",    @"<script[\s\S]*?</script>", TokenType.Delimiter, 8));
            AddRule(new SyntaxRule("AttrValue",    @"(?<==)(?:""[^""]*""|'[^']*')", TokenType.HtmlValue, 7));
            AddRule(new SyntaxRule("AttrName",     @"\b\w[\w\-]*(?=\s*=)", TokenType.HtmlAttribute, 6));
            AddRule(new SyntaxRule("Tags",         @"</?[A-Za-z][\w\-]*|/?>", TokenType.HtmlTag, 5));
            AddRule(new SyntaxRule("Entity",       @"&[A-Za-z#\d]+;",   TokenType.Number, 4));
        }

        public override bool ShouldAutoIndent(string lineText)
        {
            string t = lineText.TrimEnd().ToLower();
            return t.EndsWith(">") && !t.Contains("</") && !t.EndsWith("/>") &&
                   (t.Contains("<div") || t.Contains("<section") || t.Contains("<ul") ||
                    t.Contains("<ol") || t.Contains("<table") || t.Contains("<tbody") ||
                    t.Contains("<tr") || t.Contains("<head") || t.Contains("<body") ||
                    t.Contains("<html") || t.Contains("<script") || t.Contains("<style"));
        }
    }
}

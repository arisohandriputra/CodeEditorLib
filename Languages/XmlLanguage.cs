// =============================================================================
//  XML Language Definition for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides XML language configuration used by the editor syntax highlighting
//  and parsing engine.
//
//  This file is responsible for:
//  - Defining XML language metadata
//  - Registering XML-related file extensions
//  - Configuring syntax highlighting rules
//  - Detecting XML tags, attributes, and values
//  - Supporting XML declarations and CDATA blocks
//  - Handling indentation rules for structured markup
//
// -----------------------------------------------------------------------------
//
//  Supported File Extensions:
//  - .xml
//  - .xaml
//  - .config
//  - .resx
//  - .csproj
//  - .sln
//  - .xslt
//
// -----------------------------------------------------------------------------
//
//  Syntax Highlighting Features:
//  - XML comments          -> <!-- comment -->
//  - CDATA sections        -> <![CDATA[ ... ]]>
//  - XML declaration       -> <?xml version="1.0"?>
//  - Attribute values      -> "value" or 'value'
//  - Attribute names       -> name="value"
//  - XML tags              -> <tag>, </tag>
//  - XML entities          -> &amp;, &lt;, &#123;
//
// -----------------------------------------------------------------------------
//
//  XML Structure Support:
//  Handles hierarchical markup structure including:
//
//  - Opening tags
//  - Closing tags
//  - Self-closing tags
//  - Nested elements
//
// -----------------------------------------------------------------------------
//
//  Auto Indentation:
//  Improves readability by automatically indenting nested XML blocks.
//
//  Example:
//
//      <root>
//          <child>
//              |
//          </child>
//      </root>
//
// -----------------------------------------------------------------------------
//
//  Parsing System:
//  Uses regex-based syntax rules with priority ordering:
//
//  Priority ensures:
//  - Comments override all tokens
//  - CDATA blocks are preserved
//  - Attributes are correctly separated from tags
//  - Tags are parsed before entities
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - UI markup files (XAML)
//  - Configuration files (.config)
//  - Project files (.csproj, .sln)
//  - Data serialization formats
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
//  and/or sell copies of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//
//  IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
//  CLAIM, DAMAGES OR OTHER LIABILITY,
//  WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// =============================================================================

using System;
using CodeEditorLib.Core;

namespace CodeEditorLib.Languages
{
    public class XmlLanguage : LanguageDefinition
    {
        public XmlLanguage()
        {
            Name = "XML";
            CommentMultiLineStart = "<!--";
            CommentMultiLineEnd = "-->";
            IndentChars = "  ";
            AddExtension(".xml");
            AddExtension(".xaml");
            AddExtension(".config");
            AddExtension(".resx");
            AddExtension(".csproj");
            AddExtension(".sln");
            AddExtension(".xslt");

            AddRule(new SyntaxRule("Comment",       @"<!--[\s\S]*?-->",            TokenType.Comment, 10));
            AddRule(new SyntaxRule("CData",         @"<!\[CDATA\[[\s\S]*?\]\]>",   TokenType.String, 9));
            AddRule(new SyntaxRule("XmlDecl",       @"<\?xml[\s\S]*?\?>",          TokenType.Preprocessor, 8));
            AddRule(new SyntaxRule("AttrValue",     @"""[^""]*""|'[^']*'",         TokenType.XmlValue, 7));
            AddRule(new SyntaxRule("AttrName",      @"\b[\w:\-\.]+(?=\s*=)",       TokenType.XmlAttribute, 6));
            AddRule(new SyntaxRule("Tags",          @"</?[\w:\-\.]+|/?>",          TokenType.XmlTag, 5));
            AddRule(new SyntaxRule("Entity",        @"&[A-Za-z#\d]+;",             TokenType.Number, 4));
        }
    }
}

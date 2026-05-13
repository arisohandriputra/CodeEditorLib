// =============================================================================
//  Language Registry System for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides centralized language registration and lookup
//  management for the editor syntax highlighting system.
//
//  This file is responsible for:
//  - Registering supported programming languages
//  - Managing language definitions by name
//  - Managing language definitions by file extension
//  - Detecting languages from file paths
//  - Providing global access to language configurations
//
// -----------------------------------------------------------------------------
//
//  LanguageRegistry Class :
//  Acts as the central language manager used by the
//  editor for handling syntax language definitions.
//
//  Uses a singleton architecture to ensure a single
//  global registry instance throughout the application.
//
// -----------------------------------------------------------------------------
//
//  Built-in Registered Languages:
//  - C#
//  - Java
//  - JavaScript
//  - Python
//  - HTML
//  - CSS
//  - SQL
//  - XML
//  - PHP
//  - C++
//  - VB.NET
//  - Go
//  - Rust
//
// -----------------------------------------------------------------------------
//
//  Main Features:
//  - Language registration
//  - Extension-to-language mapping
//  - Name-to-language lookup
//  - File path language detection
//  - Dynamic custom language support
//
// -----------------------------------------------------------------------------
//
//  File Extension Detection:
//  Automatically detects language types from
//  file extensions.
//
//  Examples:
//      .cs    -> C#
//      .js    -> JavaScript
//      .html  -> HTML
//      .cpp   -> C++
//
// -----------------------------------------------------------------------------
//
//  Internal Architecture:
//  Uses dictionary-based lookup tables for:
//
//  - Fast language name searching
//  - Fast extension matching
//  - Efficient runtime access
//
//  Case-insensitive comparisons are used to ensure
//  consistent language detection.
//
// -----------------------------------------------------------------------------
//
//  Singleton Pattern:
//  The registry uses a singleton instance to provide:
//
//  - Centralized language management
//  - Shared application-wide access
//  - Reduced memory usage
//  - Consistent language configuration
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - Syntax highlighting engine
//  - Automatic language detection
//  - File opening systems
//  - IDE/editor integration
//  - Custom language registration
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
using System.IO;
using CodeEditorLib.Core;

namespace CodeEditorLib.Languages
{
    public class LanguageRegistry
    {
        private static LanguageRegistry _instance;
        private Dictionary<string, LanguageDefinition> _byName;
        private Dictionary<string, LanguageDefinition> _byExtension;

        private LanguageRegistry()
        {
            _byName = new Dictionary<string, LanguageDefinition>(StringComparer.OrdinalIgnoreCase);
            _byExtension = new Dictionary<string, LanguageDefinition>(StringComparer.OrdinalIgnoreCase);
        }

        public static LanguageRegistry Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LanguageRegistry();
                    _instance.RegisterDefaults();
                }
                return _instance;
            }
        }

        private void RegisterDefaults()
        {
            Register(new CSharpLanguage());
            Register(new JavaLanguage());
            Register(new JavaScriptLanguage());
            Register(new PythonLanguage());
            Register(new HtmlLanguage());
            Register(new CssLanguage());
            Register(new SqlLanguage());
            Register(new XmlLanguage());
            Register(new PhpLanguage());
            Register(new CppLanguage());
            Register(new VbLanguage());
            Register(new GoLanguage());
            Register(new RustLanguage());
        }

        public void Register(LanguageDefinition language)
        {
            if (language == null) throw new ArgumentNullException("language");
            _byName[language.Name] = language;
            foreach (string ext in language.FileExtensions)
                _byExtension[ext] = language;
        }

        public LanguageDefinition GetByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            LanguageDefinition lang;
            _byName.TryGetValue(name, out lang);
            return lang;
        }

        public LanguageDefinition GetByExtension(string extension)
        {
            if (string.IsNullOrEmpty(extension)) return null;
            if (!extension.StartsWith(".")) extension = "." + extension;
            LanguageDefinition lang;
            _byExtension.TryGetValue(extension.ToLower(), out lang);
            return lang;
        }

        public LanguageDefinition GetByFilePath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return null;
            string ext = Path.GetExtension(filePath);
            return GetByExtension(ext);
        }

        public ICollection<string> GetLanguageNames()
        {
            return _byName.Keys;
        }

        public ICollection<LanguageDefinition> GetAllLanguages()
        {
            return _byName.Values;
        }

        public bool Contains(string name)
        {
            return _byName.ContainsKey(name);
        }
    }
}

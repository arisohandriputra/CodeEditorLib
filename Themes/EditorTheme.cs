// =============================================================================
//  EditorTheme Base Class for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides an abstract base class for all editor themes used in CodeEditorLib.
//
//  This class is responsible for:
//  - Defining the structure of a visual theme system
//  - Storing theme metadata such as theme name
//  - Initializing and managing syntax highlighting schemes
//  - Providing abstract color definitions for editor UI components
//  - Forcing derived themes to implement consistent styling rules
//
// -----------------------------------------------------------------------------
//
//  Core Responsibilities:
//  - Acts as the foundation for all theme implementations (e.g., Dark, Dracula)
//  - Initializes HighlightScheme for syntax highlighting
//  - Links UI color configuration with token-based highlighting system
//  - Ensures consistent theme behavior across the editor engine
//
// -----------------------------------------------------------------------------
//
//  Theme System Design:
//  Each theme derived from EditorTheme must define:
//
//  - Background color            -> Editor canvas background
//  - Default text color          -> Base text rendering color
//  - Selection color             -> Selected text highlight color
//  - Current line color          -> Active line highlight
//  - Line number styling         -> Background, foreground, border colors
//  - Caret color                 -> Text cursor color
//  - Bracket match color         -> Matching bracket highlight
//  - Autocomplete UI colors      -> Popup background, text, selection
//
// -----------------------------------------------------------------------------
//
//  Syntax Highlighting System:
//  Uses HighlightScheme to map TokenType values to HighlightStyle rules.
//
//  The BuildScheme() method must be implemented by derived themes to:
//  - Assign colors to language tokens
//  - Define font styles (bold, italic, etc.)
//  - Control how syntax elements are visually represented
//
//  This ensures consistent token rendering across languages.
//
// -----------------------------------------------------------------------------
//
//  Rendering Architecture:
//  Theme → HighlightScheme → TokenType → HighlightStyle → UI Rendering
//
//  This pipeline ensures:
//  - Separation of logic and presentation
//  - Easy customization of editor appearance
//  - Extensible multi-language support
//
// -----------------------------------------------------------------------------
//
//  Usage Pattern:
//  1. Create a custom theme by inheriting EditorTheme
//  2. Override all abstract color properties
//  3. Implement BuildScheme() to define syntax colors
//  4. Assign theme to editor control
//
// -----------------------------------------------------------------------------
//
//  Example Derived Themes:
//  - DarkTheme
//  - DraculaTheme
//  - LightTheme (future extension)
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
//  EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//
//  IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
//  CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
//  TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// =============================================================================

using System;
using System.Drawing;
using CodeEditorLib.Core;
using CodeEditorLib.Highlighting;

namespace CodeEditorLib.Themes
{
  
    public abstract class EditorTheme
    {
        private string _name;
        private HighlightScheme _scheme;

        protected EditorTheme(string name)
        {
            _name = name;
            _scheme = new HighlightScheme(name);
            BuildScheme(_scheme);
        }

        public string Name { get { return _name; } }

        public HighlightScheme Scheme { get { return _scheme; } }


        public abstract Color BackgroundColor { get; }

        public abstract Color DefaultTextColor { get; }

        public abstract Color SelectionColor { get; }

        public abstract Color CurrentLineColor { get; }

        public abstract Color LineNumberBackColor { get; }

        public abstract Color LineNumberForeColor { get; }

        public abstract Color LineNumberBorderColor { get; }

        public abstract Color CaretColor { get; }

        public abstract Color BracketMatchColor { get; }

        public abstract Color AutoCompleteBackColor { get; }

        public abstract Color AutoCompleteForeColor { get; }

        public abstract Color AutoCompleteSelectionColor { get; }

        protected abstract void BuildScheme(HighlightScheme scheme);
    }
}

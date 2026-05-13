// =============================================================================
//  Editor Event Argument System for CodeEditorLib
// =============================================================================
//
//  Description :
//  Defines custom EventArgs classes used by the editor for
//  handling internal editor events and user interactions.
//
//  This file is responsible for:
//  - Passing text change information
//  - Reporting language mode changes
//  - Tracking caret position updates
//  - Handling find and replace operations
//  - Providing structured event data to subscribers
//
// -----------------------------------------------------------------------------
//
//  TextChangedEventArgs :
//  Provides information when editor text content changes.
//
//  Contains:
//  - Previous text value
//  - New updated text value
//
//  Used for:
//  - Change tracking
//  - Auto-save systems
//  - Undo/redo integration
//  - External editor notifications
//
// -----------------------------------------------------------------------------
//
//  LanguageChangedEventArgs :
//  Provides information when syntax language mode changes.
//
//  Example:
//      C#  -> XML
//      XML -> SQL
//
//  Used for:
//  - Syntax highlighting updates
//  - Parser switching
//  - Language service initialization
//
// -----------------------------------------------------------------------------
//
//  CaretPositionEventArgs :
//  Provides caret position information inside the editor.
//
//  Contains:
//  - Current line number
//  - Current column number
//  - Absolute character index
//
//  Used for:
//  - Status bar updates
//  - Navigation systems
//  - Code tools and diagnostics
//
// -----------------------------------------------------------------------------
//
//  FindReplaceEventArgs :
//  Provides information for find and replace operations.
//
//  Contains:
//  - Search text
//  - Replacement text
//  - Match case option
//  - Whole word option
//  - Total match count
//
//  Used for:
//  - Search dialogs
//  - Replace operations
//  - Match result reporting
//
// -----------------------------------------------------------------------------
//
//  Event System Features:
//  - Lightweight event communication
//  - Strongly typed editor events
//  - Centralized event argument definitions
//  - Easy integration with WinForms event model
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

namespace CodeEditorLib.Events
{
    public class TextChangedEventArgs : EventArgs
    {
        private string _oldText;
        private string _newText;

        public TextChangedEventArgs(string oldText, string newText)
        {
            _oldText = oldText;
            _newText = newText;
        }

        public string OldText { get { return _oldText; } }
        public string NewText { get { return _newText; } }
    }

    public class LanguageChangedEventArgs : EventArgs
    {
        private string _languageName;

        public LanguageChangedEventArgs(string languageName)
        {
            _languageName = languageName;
        }

        public string LanguageName { get { return _languageName; } }
    }

    public class CaretPositionEventArgs : EventArgs
    {
        private int _line;
        private int _column;
        private int _absolutePosition;

        public CaretPositionEventArgs(int line, int column, int absolutePosition)
        {
            _line = line;
            _column = column;
            _absolutePosition = absolutePosition;
        }

        public int Line { get { return _line; } }

        public int Column { get { return _column; } }

        public int AbsolutePosition { get { return _absolutePosition; } }
    }

    public class FindReplaceEventArgs : EventArgs
    {
        private string _searchText;
        private string _replaceText;
        private bool _matchCase;
        private bool _wholeWord;
        private int _matchCount;

        public FindReplaceEventArgs(string searchText, string replaceText, bool matchCase, bool wholeWord, int matchCount)
        {
            _searchText = searchText;
            _replaceText = replaceText;
            _matchCase = matchCase;
            _wholeWord = wholeWord;
            _matchCount = matchCount;
        }

        public string SearchText { get { return _searchText; } }
        public string ReplaceText { get { return _replaceText; } }
        public bool MatchCase { get { return _matchCase; } }
        public bool WholeWord { get { return _wholeWord; } }
        public int MatchCount { get { return _matchCount; } }
    }
}

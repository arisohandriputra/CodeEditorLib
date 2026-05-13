// =============================================================================
//  Text Selection System for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides text selection management functionality used by
//  the editor for handling selected text ranges.
//
//  This file is responsible for:
//  - Storing selection start and end positions
//  - Calculating selection length
//  - Detecting empty selections
//  - Checking whether an index exists inside selection range
//  - Normalizing reversed selections automatically
//
// -----------------------------------------------------------------------------
//
//  TextSelection Class :
//  Represents a text selection range inside the editor.
//
//  The selection system is used for:
//  - Text highlighting
//  - Copy/Cut/Delete operations
//  - Keyboard text selection
//  - Mouse drag selection
//  - Multi-line editing features
//
//  Example:
//      Selected Text:
//          Hello [World]
//
//      Start Index : 6
//      End Index   : 11
//
// -----------------------------------------------------------------------------
//
//  Features:
//  - Automatic start/end normalization
//  - Selection length calculation
//  - Empty selection detection
//  - Range validation support
//  - Human-readable debug output
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

namespace CodeEditorLib.Core
{
    public class TextSelection
    {
        private int _start;
        private int _end;

        public TextSelection() : this(0, 0) { }

        public TextSelection(int start, int end)
        {
            _start = Math.Min(start, end);
            _end = Math.Max(start, end);
        }

        public int Start { get { return _start; } set { _start = value; } }

        public int End { get { return _end; } set { _end = value; } }

        public int Length { get { return _end - _start; } }

        public bool IsEmpty { get { return _start == _end; } }

        public bool Contains(int index)
        {
            return index >= _start && index < _end;
        }

        public void Normalize()
        {
            if (_start > _end) { int tmp = _start; _start = _end; _end = tmp; }
        }

        public override string ToString()
        {
            return string.Format("Selection[{0}, {1}] len={2}", _start, _end, Length);
        }
    }
}

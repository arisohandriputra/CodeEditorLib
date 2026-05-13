// =============================================================================
//  Undo / Redo Management System for CodeEditorLib
// =============================================================================
//
//  Description :
//  Provides undo and redo functionality for the editor by
//  storing text modification history as edit actions.
//
//  This file is responsible for:
//  - Recording text editing operations
//  - Managing undo history stacks
//  - Managing redo history stacks
//  - Restoring previous editor states
//  - Tracking caret positions after edits
//
// -----------------------------------------------------------------------------
//
//  EditAction Class :
//  Represents a single text modification operation.
//
//  Each edit action stores:
//  - Text insertion position
//  - Inserted text content
//  - Removed text content
//  - Caret position after editing
//
//  Example:
//      Original Text : Hello
//      Inserted Text : " World"
//      Result        : Hello World
//
// -----------------------------------------------------------------------------
//
//  UndoRedoManager Class :
//  Handles editor history management using stack-based storage.
//
//  Features:
//  - Unlimited sequential undo operations
//  - Unlimited sequential redo operations
//  - Configurable maximum history size
//  - Automatic redo clearing after new edits
//  - Fast stack-based history access
//
// -----------------------------------------------------------------------------
//
//  Internal Logic :
//  - New edits are pushed into the Undo stack
//  - Undo operations move actions into the Redo stack
//  - Redo operations move actions back into the Undo stack
//  - Old history entries are trimmed automatically
//
// -----------------------------------------------------------------------------
//
//  Typical Usage:
//  - Ctrl + Z -> Undo last edit
//  - Ctrl + Y -> Redo reverted edit
//  - Text insertion tracking
//  - Text deletion tracking
//  - Caret restoration after undo/redo
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

namespace CodeEditorLib.Core
{
    public class EditAction
    {
        public int Position;
        public string InsertedText;
        public string RemovedText;
        public int CaretAfter;

        public EditAction(int position, string inserted, string removed, int caretAfter)
        {
            Position = position;
            InsertedText = inserted;
            RemovedText = removed;
            CaretAfter = caretAfter;
        }
    }

    public class UndoRedoManager
    {
        private Stack<EditAction> _undoStack;
        private Stack<EditAction> _redoStack;
        private int _maxHistory;

        public UndoRedoManager() : this(500) { }

        public UndoRedoManager(int maxHistory)
        {
            _maxHistory = maxHistory;
            _undoStack = new Stack<EditAction>();
            _redoStack = new Stack<EditAction>();
        }

        public bool CanUndo { get { return _undoStack.Count > 0; } }

        public bool CanRedo { get { return _redoStack.Count > 0; } }

        public int UndoCount { get { return _undoStack.Count; } }

        public int RedoCount { get { return _redoStack.Count; } }

        public void RecordAction(EditAction action)
        {
            _undoStack.Push(action);
            _redoStack.Clear();

            if (_undoStack.Count > _maxHistory)
            {
                Stack<EditAction> tmp = new Stack<EditAction>();
                int count = 0;
                foreach (EditAction a in _undoStack)
                {
                    if (count++ < _maxHistory) tmp.Push(a);
                }
                _undoStack.Clear();
                Stack<EditAction> ordered = new Stack<EditAction>();
                foreach (EditAction a in tmp) ordered.Push(a);
                foreach (EditAction a in ordered) _undoStack.Push(a);
            }
        }

        public EditAction Undo()
        {
            if (_undoStack.Count == 0) return null;
            EditAction action = _undoStack.Pop();
            _redoStack.Push(action);
            return action;
        }

        public EditAction Redo()
        {
            if (_redoStack.Count == 0) return null;
            EditAction action = _redoStack.Pop();
            _undoStack.Push(action);
            return action;
        }

        public void Clear()
        {
            _undoStack.Clear();
            _redoStack.Clear();
        }
    }
}

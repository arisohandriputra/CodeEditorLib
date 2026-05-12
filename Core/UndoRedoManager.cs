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

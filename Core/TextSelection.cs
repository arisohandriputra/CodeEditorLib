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

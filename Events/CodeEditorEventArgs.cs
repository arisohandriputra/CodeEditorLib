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

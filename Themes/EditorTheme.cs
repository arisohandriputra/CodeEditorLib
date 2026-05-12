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

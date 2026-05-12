using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CodeEditorLib.Core;
using CodeEditorLib.Events;
using CodeEditorLib.Highlighting;
using CodeEditorLib.Languages;
using CodeEditorLib.Themes;

namespace CodeEditorLib.Controls
{
   
    public class CodeEditorControl : UserControl
    {
        private RichTextBox _textBox;
        private LineNumberPanel _linePanel;
        private Panel _lineContainer;

        private SyntaxHighlighter _highlighter;
        private UndoRedoManager _undoRedo;
        private CodeFormatter _formatter;
        private LanguageDefinition _language;
        private EditorTheme _theme;

        private bool _highlighting;
        private bool _suppressEvents;
        private int _tabSize;
        private bool _showLineNumbers;
        private bool _highlightCurrentLine;
        private bool _autoBrackets;
        private bool _autoIndent;
        private bool _wordWrap;
        private string _lastText;
        private string _lastHighlightedText;
        private Timer _highlightTimer;

        public event EventHandler<TextChangedEventArgs> CodeTextChanged;
        public event EventHandler<LanguageChangedEventArgs> LanguageChanged;
        public event EventHandler<CaretPositionEventArgs> CaretPositionChanged;
        public event EventHandler<FindReplaceEventArgs> FindReplaceCompleted;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref POINT lParam);

        private const int WM_SETREDRAW    = 0x000B;
        private const int EM_GETSCROLLPOS = 0x04DD;
        private const int EM_SETSCROLLPOS = 0x04DE;

        [System.Runtime.InteropServices.StructLayout(
            System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct POINT { public int X; public int Y; }

        private delegate int ColorSlotFunc(Color c);
        private delegate string[] LineTransformer(string[] lines);

        private void SetRedraw(bool on)
        {
            SendMessage(_textBox.Handle, WM_SETREDRAW, on ? (IntPtr)1 : IntPtr.Zero, IntPtr.Zero);
        }

        private POINT GetScrollPos()
        {
            POINT p = new POINT();
            SendMessage(_textBox.Handle, EM_GETSCROLLPOS, IntPtr.Zero, ref p);
            return p;
        }

        private void SetScrollPos(POINT p)
        {
            SendMessage(_textBox.Handle, EM_SETSCROLLPOS, IntPtr.Zero, ref p);
        }

        public CodeEditorControl()
        {
            _tabSize              = 4;
            _showLineNumbers      = true;
            _highlightCurrentLine = true;
            _autoBrackets         = true;
            _autoIndent           = true;
            _wordWrap             = false;
            _highlighter          = new SyntaxHighlighter();
            _undoRedo             = new UndoRedoManager();
            _highlighting         = false;
            _suppressEvents       = false;
            _lastHighlightedText  = null;

            _highlightTimer = new Timer();
            _highlightTimer.Interval = 300;
            _highlightTimer.Tick += delegate(object s, EventArgs ev)
            {
                _highlightTimer.Stop();
                ApplyHighlighting();
            };

            BuildLayout();
            ApplyTheme(new Themes.DarkTheme());
        }

        private void BuildLayout()
        {
            SuspendLayout();

            _lineContainer = new Panel { Dock = DockStyle.Fill };

            _linePanel = new LineNumberPanel { Dock = DockStyle.Left, Width = 45 };

            _textBox = new RichTextBox
            {
                Dock        = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                AcceptsTab  = true,
                Multiline   = true,
                ScrollBars  = RichTextBoxScrollBars.Both,
                WordWrap    = _wordWrap,
                DetectUrls  = false,
                Font        = new Font("Consolas", 10f, FontStyle.Regular)
            };

            _textBox.TextChanged      += OnTextChanged;
            _textBox.SelectionChanged += OnSelectionChanged;
            _textBox.KeyDown          += OnKeyDown;
            _textBox.KeyPress         += OnKeyPress;

            _textBox.VScroll += delegate(object s, EventArgs e) { _linePanel.Invalidate(); };
            _textBox.HScroll += delegate(object s, EventArgs e) { _linePanel.Invalidate(); };

            _linePanel.Editor = _textBox;

            _lineContainer.Controls.Add(_textBox);
            _lineContainer.Controls.Add(_linePanel);
            Controls.Add(_lineContainer);
            ResumeLayout(false);
        }


        public string Code
        {
            get { return _textBox.Text; }
            set
            {
                _suppressEvents = true;
                _textBox.Text   = value;
                _lastText       = value;
                _lastHighlightedText = null;
                _undoRedo.Clear();
                _suppressEvents = false;
                ApplyHighlighting();
            }
        }

        public LanguageDefinition Language
        {
            get { return _language; }
            set
            {
                _language             = value;
                _highlighter.Language = value;
                _formatter            = _language != null ? new CodeFormatter(_language) : null;
                _lastHighlightedText  = null;
                ApplyHighlighting();
                if (LanguageChanged != null && _language != null)
                    LanguageChanged(this, new LanguageChangedEventArgs(_language.Name));
            }
        }

        public string LanguageName
        {
            get { return _language != null ? _language.Name : string.Empty; }
            set { Language = LanguageRegistry.Instance.GetByName(value); }
        }

        public EditorTheme Theme
        {
            get { return _theme; }
            set { ApplyTheme(value); }
        }

        public override Font Font
        {
            get { return _textBox.Font; }
            set
            {
                _textBox.Font       = value;
                _linePanel.LineFont = value;
                _linePanel.Width    = _linePanel.ComputeWidth(_textBox.Lines.Length);
                _linePanel.Invalidate();
            }
        }

        public bool ShowLineNumbers
        {
            get { return _showLineNumbers; }
            set
            {
                _showLineNumbers           = value;
                _linePanel.ShowLineNumbers = value;
                _linePanel.Width = value ? _linePanel.ComputeWidth(_textBox.Lines.Length) : 0;
                _linePanel.Invalidate();
            }
        }

        public bool HighlightCurrentLine
        {
            get { return _highlightCurrentLine; }
            set { _highlightCurrentLine = value; _linePanel.Invalidate(); }
        }

        public bool AutoBrackets
        {
            get { return _autoBrackets; }
            set { _autoBrackets = value; }
        }

        public bool AutoIndent
        {
            get { return _autoIndent; }
            set { _autoIndent = value; }
        }

        public bool WordWrap
        {
            get { return _wordWrap; }
            set { _wordWrap = value; _textBox.WordWrap = value; }
        }

        public int TabSize
        {
            get { return _tabSize; }
            set { _tabSize = Math.Max(1, value); }
        }

        public bool ReadOnly
        {
            get { return _textBox.ReadOnly; }
            set { _textBox.ReadOnly = value; }
        }

        public int CurrentLine
        {
            get { return _textBox.GetLineFromCharIndex(_textBox.SelectionStart) + 1; }
        }

        public int CurrentColumn
        {
            get
            {
                int li = _textBox.GetLineFromCharIndex(_textBox.SelectionStart);
                int ls = _textBox.GetFirstCharIndexFromLine(li);
                return _textBox.SelectionStart - ls + 1;
            }
        }

        public int LineCount  { get { return _textBox.Lines.Length; } }
        public bool CanUndo   { get { return _undoRedo.CanUndo; } }
        public bool CanRedo   { get { return _undoRedo.CanRedo; } }

        private void ApplyTheme(EditorTheme theme)
        {
            if (theme == null) return;
            _theme = theme;
            _textBox.BackColor = theme.BackgroundColor;
            _textBox.ForeColor = theme.DefaultTextColor;
            _linePanel.Theme   = theme;
            _linePanel.Invalidate();
            _lastHighlightedText = null;
            ApplyHighlighting();
        }

        private void ScheduleHighlight()
        {
            _highlightTimer.Stop();
            _highlightTimer.Interval = 300;
            _highlightTimer.Start();
        }

        private void ApplyHighlighting()
        {
            if (_highlighting) return;
            if (_language == null || _theme == null) return;

            string text = _textBox.Text;
            if (string.IsNullOrEmpty(text)) { _lastHighlightedText = text; return; }
            if (text == _lastHighlightedText) return;

            _highlighting   = true;
            _suppressEvents = true;
            SetRedraw(false);

            try
            {
                int   savedStart = _textBox.SelectionStart;
                int   savedLen   = _textBox.SelectionLength;
                POINT scroll     = GetScrollPos();

                List<SyntaxToken> tokens = _highlighter.Tokenize(text);
                string rtf = BuildRtf(text, tokens);

                _textBox.Rtf = rtf;

                _lastHighlightedText = _textBox.Text;

                SetScrollPos(scroll);
                int safeStart = Math.Min(savedStart, _textBox.TextLength);
                _textBox.Select(safeStart, Math.Min(savedLen, _textBox.TextLength - safeStart));
            }
            finally
            {
                SetRedraw(true);
                _textBox.Refresh();     
                _highlighting   = false;
                _suppressEvents = false;
            }
        }

        private string BuildRtf(string text, List<SyntaxToken> tokens)
        {
            HighlightScheme scheme    = _theme.Scheme;
            Color           defaultFg = _theme.DefaultTextColor;
            Color           defaultBg = _theme.BackgroundColor;
            int             fontSize  = (int)(_textBox.Font.Size * 2);

            List<Color>          table   = new List<Color>();
            Dictionary<int, int> slotOf  = new Dictionary<int, int>();

            table.Add(Color.Black);                
            table.Add(defaultFg);                  
            table.Add(defaultBg);                 
            slotOf[defaultFg.ToArgb()] = 1;
            slotOf[defaultBg.ToArgb()] = 2;

            ColorSlotFunc slot = delegate(Color c)
            {
                int key = c.ToArgb();
                if (slotOf.ContainsKey(key)) return slotOf[key];
                table.Add(c);
                slotOf[key] = table.Count - 1;
                return slotOf[key];
            };

            foreach (SyntaxToken tok in tokens)
            {
                HighlightStyle st = scheme.GetStyle(tok.TokenType);
                slot(st.ForeColor);
                if (st.BackColor != Color.Empty) slot(st.BackColor);
            }

            string fontName = _textBox.Font.FontFamily.Name
                .Replace(@"\", @"\\").Replace("{", @"\{").Replace("}", @"\}");

            StringBuilder sb = new StringBuilder(text.Length * 6);

            sb.Append(@"{\rtf1\ansi\ansicpg1252\deff0");
            sb.Append(@"{\fonttbl{\f0\fmodern\fcharset0 ").Append(fontName).Append(@";}}");

            sb.Append(@"{\colortbl;");  
            for (int i = 1; i < table.Count; i++)
                sb.AppendFormat(@"\red{0}\green{1}\blue{2};", table[i].R, table[i].G, table[i].B);
            sb.Append("}");

            sb.AppendFormat(@"\f0\fs{0}\cf1\highlight2 ", fontSize);

            int len  = text.Length;
            int tIdx = 0;

            int   curFg = 1, curBg = 2;
            bool  curBold = false, curItalic = false, curUnder = false;
            bool  groupOpen = false;

            for (int pos = 0; pos < len; pos++)
            {
                while (tIdx < tokens.Count &&
                       tokens[tIdx].StartIndex + tokens[tIdx].Length <= pos)
                    tIdx++;

                int  newFg = 1, newBg = 2;
                bool newBold = false, newItalic = false, newUnder = false;
                bool inTok = tIdx < tokens.Count && tokens[tIdx].StartIndex <= pos;
                if (inTok)
                {
                    HighlightStyle st = scheme.GetStyle(tokens[tIdx].TokenType);
                    if (!scheme.HasStyle(tokens[tIdx].TokenType))
                        newFg = 1;
                    else
                        newFg = slot(st.ForeColor);
                    newBg    = st.BackColor != Color.Empty ? slot(st.BackColor) : 2;
                    newBold  = st.Bold; newItalic = st.Italic; newUnder = st.Underline;
                }

                char c = text[pos];

                if (c == '\r') continue;

                if (c == '\n')
                {
                    if (groupOpen) { sb.Append('}'); groupOpen = false; }
                    sb.Append(@"\par").Append("\r\n");
                    curFg = -1; 
                    continue;
                }

                if (newFg != curFg || newBg != curBg ||
                    newBold != curBold || newItalic != curItalic || newUnder != curUnder)
                {
                    if (groupOpen) { sb.Append('}'); groupOpen = false; }
                    curFg = newFg; curBg = newBg;
                    curBold = newBold; curItalic = newItalic; curUnder = newUnder;
                }

                if (!groupOpen)
                {
                    sb.Append('{');
                    sb.AppendFormat(@"\cf{0}\highlight{1}", curFg, curBg);
                    if (curBold)   sb.Append(@"\b");
                    if (curItalic) sb.Append(@"\i");
                    if (curUnder)  sb.Append(@"\ul");
                    sb.Append(' ');
                    groupOpen = true;
                }

                if      (c == '\\') sb.Append(@"\\");
                else if (c == '{' )   sb.Append(@"\{");
                else if (c == '}' )   sb.Append(@"\}");
                else if (c > 127)     sb.AppendFormat(@"\u{0}?", (int)c);
                else                  sb.Append(c);
            }

            if (groupOpen) { sb.Append('}'); groupOpen = false; }

            sb.Append('}');
            return sb.ToString();
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents || _highlighting) return;

            string newText = _textBox.Text;

            if (_lastText != null && _lastText != newText)
                _undoRedo.RecordAction(new EditAction(
                    0, newText, _lastText, _textBox.SelectionStart));
            _lastText = newText;

            _linePanel.Width = _showLineNumbers
                ? _linePanel.ComputeWidth(_textBox.Lines.Length) : 0;
            _linePanel.Invalidate();

            ScheduleHighlight();

            if (CodeTextChanged != null)
                CodeTextChanged(this, new TextChangedEventArgs(
                    _lastText ?? string.Empty, newText));
        }

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            int li = _textBox.GetLineFromCharIndex(_textBox.SelectionStart);
            _linePanel.CurrentLine = li + 1;
            _linePanel.Invalidate();

            if (CaretPositionChanged != null)
                CaretPositionChanged(this, new CaretPositionEventArgs(
                    li + 1, CurrentColumn, _textBox.SelectionStart));
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.Z:           Undo();          e.Handled = true; return;
                    case Keys.Y:           Redo();          e.Handled = true; return;
                    case Keys.OemQuestion:
                    case Keys.Divide:      ToggleComment();    e.Handled = true; return;
                    case Keys.F:           ShowFindDialog();   e.Handled = true; return;
                    case Keys.H:           ShowReplaceDialog();e.Handled = true; return;
                    case Keys.G:           ShowGotoDialog();   e.Handled = true; return;
                }
            }

            if (e.KeyCode == Keys.Tab)
            {
                if (_textBox.SelectionLength > 0)
                {
                    if (e.Shift) OutdentSelection(); else IndentSelection();
                    e.Handled = true;
                    return;
                }
                if (!e.Shift) { InsertTabAtCaret(); e.Handled = true; return; }
            }

            if (e.KeyCode == Keys.Return && _autoIndent && _language != null)
            {
                HandleEnterKey(); e.Handled = true;
            }
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (_autoBrackets && _language != null)
            {
                Dictionary<char, char> pairs = _language.GetBracketPairs();
                if (pairs.ContainsKey(e.KeyChar))
                {
                    int caret = _textBox.SelectionStart;
                    _textBox.SelectionLength = 0;
                    _textBox.SelectedText    = e.KeyChar.ToString() + pairs[e.KeyChar];
                    _textBox.SelectionStart  = caret + 1;
                    e.Handled = true;
                }
            }
        }

        public void Undo()
        {
            EditAction action = _undoRedo.Undo();
            if (action == null) return;
            _suppressEvents = true;
            _textBox.Text   = action.RemovedText;
            _textBox.SelectionStart = Math.Min(action.CaretAfter, _textBox.TextLength);
            _suppressEvents = false;
            _lastText = _textBox.Text;
            _lastHighlightedText = null;
            ApplyHighlighting();
        }

        public void Redo()
        {
            EditAction action = _undoRedo.Redo();
            if (action == null) return;
            _suppressEvents = true;
            _textBox.Text   = action.InsertedText;
            _textBox.SelectionStart = Math.Min(action.CaretAfter, _textBox.TextLength);
            _suppressEvents = false;
            _lastText = _textBox.Text;
            _lastHighlightedText = null;
            ApplyHighlighting();
        }

        public void ToggleComment()
        {
            if (_formatter == null) return;
            int start = _textBox.SelectionStart, len = _textBox.SelectionLength;
            int fl = _textBox.GetLineFromCharIndex(start);
            int ll = _textBox.GetLineFromCharIndex(start + len);
            string[] all = _textBox.Lines, sel = new string[ll - fl + 1];
            for (int i = 0; i < sel.Length; i++)
                sel[i] = (fl + i < all.Length) ? all[fl + i] : string.Empty;
            string toggled = _formatter.ToggleLineComment(sel);
            int rs = _textBox.GetFirstCharIndexFromLine(fl);
            int re = (ll + 1 < all.Length)
                     ? _textBox.GetFirstCharIndexFromLine(ll + 1) - 1
                     : _textBox.TextLength;
            _textBox.Select(rs, re - rs);
            _textBox.SelectedText = toggled;
        }

        public void IndentSelection()
        {
            if (_formatter == null) return;
            TransformSelectedLines(delegate(string[] ls) { return _formatter.IndentLines(ls); });
        }

        public void OutdentSelection()
        {
            if (_formatter == null) return;
            TransformSelectedLines(delegate(string[] ls) { return _formatter.OutdentLines(ls); });
        }

        private void TransformSelectedLines(LineTransformer transform)
        {
            int start = _textBox.SelectionStart;
            int fl = _textBox.GetLineFromCharIndex(start);
            int ll = _textBox.GetLineFromCharIndex(start + _textBox.SelectionLength);
            string[] all = _textBox.Lines, sel = new string[ll - fl + 1];
            for (int i = 0; i < sel.Length; i++)
                sel[i] = (fl + i < all.Length) ? all[fl + i] : string.Empty;
            string[] result = transform(sel);
            int rs = _textBox.GetFirstCharIndexFromLine(fl);
            int re = (ll + 1 < all.Length)
                     ? _textBox.GetFirstCharIndexFromLine(ll + 1) - 1
                     : _textBox.TextLength;
            _textBox.Select(rs, re - rs);
            _textBox.SelectedText = string.Join(Environment.NewLine, result);
        }

        private void InsertTabAtCaret()
        {
            string indent = _language != null ? _language.IndentChars : new string(' ', _tabSize);
            int caret = _textBox.SelectionStart;
            _textBox.SelectionLength = 0;
            _textBox.SelectedText    = indent;
            _textBox.SelectionStart  = caret + indent.Length;
        }

        private void HandleEnterKey()
        {
            int caret = _textBox.SelectionStart;
            int li    = _textBox.GetLineFromCharIndex(caret);
            string line = li < _textBox.Lines.Length ? _textBox.Lines[li] : string.Empty;
            string indent = _formatter != null
                ? _formatter.GetIndentForNextLine(line)
                : CodeFormatter.GetLeadingWhitespace(line);
            _textBox.SelectionLength = 0;
            _textBox.SelectedText    = Environment.NewLine + indent;
        }

        public bool FindNext(string searchText, bool matchCase, bool wholeWord)
        {
            if (string.IsNullOrEmpty(searchText)) return false;
            string text   = _textBox.Text;
            string search = matchCase ? searchText : searchText.ToLower();
            string source = matchCase ? text       : text.ToLower();
            int start = _textBox.SelectionStart + _textBox.SelectionLength;
            if (start >= text.Length) start = 0;

            int found = -1, foundLen = search.Length;
            if (wholeWord)
            {
                RegexOptions o = matchCase ? RegexOptions.None : RegexOptions.IgnoreCase;
                Regex rx = new Regex(@"\b" + Regex.Escape(search) + @"\b", o);
                Match m = rx.Match(source, start);
                if (!m.Success) m = rx.Match(source, 0);
                if (m.Success) { found = m.Index; foundLen = m.Length; }
            }
            else
            {
                found = source.IndexOf(search, start, StringComparison.Ordinal);
                if (found < 0) found = source.IndexOf(search, 0, StringComparison.Ordinal);
            }
            if (found >= 0) { _textBox.Select(found, foundLen); _textBox.ScrollToCaret(); return true; }
            return false;
        }

        public int ReplaceAll(string find, string replace, bool matchCase, bool wholeWord)
        {
            if (string.IsNullOrEmpty(find)) return 0;
            RegexOptions o = matchCase ? RegexOptions.None : RegexOptions.IgnoreCase;
            string pat = wholeWord ? @"\b" + Regex.Escape(find) + @"\b" : Regex.Escape(find);
            Regex rx = new Regex(pat, o);
            string orig = _textBox.Text;
            int count = rx.Matches(orig).Count;
            if (count > 0) Code = rx.Replace(orig, replace ?? string.Empty);
            if (FindReplaceCompleted != null)
                FindReplaceCompleted(this, new FindReplaceEventArgs(find, replace, matchCase, wholeWord, count));
            return count;
        }

        public void ShowFindDialog()    { using (Form d = BuildFRDialog(false)) d.ShowDialog(this); }
        public void ShowReplaceDialog() { using (Form d = BuildFRDialog(true))  d.ShowDialog(this); }

        public void ShowGotoDialog()
        {
            using (Form dlg = new Form { Text="Go To Line", Size=new Size(260,110),
                FormBorderStyle=FormBorderStyle.FixedDialog,
                MaximizeBox=false, MinimizeBox=false,
                StartPosition=FormStartPosition.CenterParent })
            {
                Label lbl = new Label { Text="Line number:", Left=10, Top=15, AutoSize=true };
                NumericUpDown num = new NumericUpDown
                    { Left=100, Top=12, Width=80, Minimum=1,
                      Maximum=_textBox.Lines.Length, Value=CurrentLine };
                Button ok  = new Button { Text="Go",     Left=70,  Top=45, DialogResult=DialogResult.OK };
                Button can = new Button { Text="Cancel",  Left=155, Top=45, DialogResult=DialogResult.Cancel };
                dlg.AcceptButton = ok; dlg.CancelButton = can;
                dlg.Controls.AddRange(new Control[] { lbl, num, ok, can });
                if (dlg.ShowDialog(this) == DialogResult.OK) GotoLine((int)num.Value);
            }
        }

        public void GotoLine(int lineNumber)
        {
            lineNumber = Math.Max(1, Math.Min(lineNumber, _textBox.Lines.Length));
            int ci = _textBox.GetFirstCharIndexFromLine(lineNumber - 1);
            _textBox.SelectionStart = ci; _textBox.SelectionLength = 0;
            _textBox.ScrollToCaret(); _textBox.Focus();
        }

        public void OpenFile(string path)
        {
            Code = System.IO.File.ReadAllText(path);
            LanguageDefinition lang = LanguageRegistry.Instance.GetByFilePath(path);
            if (lang != null) Language = lang;
        }

        public void SaveFile(string path)
        {
            System.IO.File.WriteAllText(path, _textBox.Text);
        }

        public void Cut()       { _textBox.Cut(); }
        public void Copy()      { _textBox.Copy(); }
        public void Paste()     { _textBox.Paste(); }
        public void SelectAll() { _textBox.SelectAll(); }

        private Form BuildFRDialog(bool showReplace)
        {
            Form dlg = new Form { Text = showReplace ? "Find & Replace" : "Find",
                Size = new Size(370, showReplace ? 190 : 145),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox=false, MinimizeBox=false,
                StartPosition = FormStartPosition.CenterParent };

            int y = 10;
            Label lf = new Label { Text="Find:", Left=10, Top=y+3, AutoSize=true };
            TextBox tf = new TextBox { Left=80, Top=y, Width=260 };
            y += 30;

            TextBox tr = null;
            if (showReplace)
            {
                Label lr = new Label { Text="Replace:", Left=10, Top=y+3, AutoSize=true };
                tr = new TextBox { Left=80, Top=y, Width=260 };
                dlg.Controls.AddRange(new Control[] { lr, tr });
                y += 30;
            }

            CheckBox cc = new CheckBox { Text="Match case", Left=10,  Top=y, AutoSize=true };
            CheckBox cw = new CheckBox { Text="Whole word", Left=120, Top=y, AutoSize=true };
            y += 28;

            Button bf = new Button { Text="Find Next",  Left=10,  Top=y, Width=90 };
            Button bc = new Button { Text="Close", Left=260, Top=y, Width=80,
                                     DialogResult=DialogResult.Cancel };
            dlg.CancelButton = bc;
            bf.Click += delegate { FindNext(tf.Text, cc.Checked, cw.Checked); };
            dlg.Controls.AddRange(new Control[] { lf, tf, cc, cw, bf, bc });

            if (showReplace)
            {
                Button br = new Button { Text="Replace All", Left=110, Top=y, Width=90 };
                br.Click += delegate
                {
                    int n = ReplaceAll(tf.Text, tr != null ? tr.Text : "", cc.Checked, cw.Checked);
                    MessageBox.Show(n + " replacement(s) made.", "Replace All",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                };
                dlg.Controls.Add(br);
            }
            return dlg;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "CodeEditorControl";
            this.Load += new System.EventHandler(this.CodeEditorControl_Load);
            this.ResumeLayout(false);

        }

        private void CodeEditorControl_Load(object sender, EventArgs e)
        {

        }
    }
}

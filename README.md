# CodeEditorLib

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![.NET Framework](https://img.shields.io/badge/.NET%20Framework-2.0+-purple.svg)](https://dotnet.microsoft.com/)
[![Platform](https://img.shields.io/badge/Platform-Windows-0078D4.svg)](https://www.microsoft.com/windows)

---

<div align="center">

## 💖 Support This Project

If CodeEditorLib saved you time or you're using it in something real, consider sponsoring. It helps me keep maintaining this and building more open source tools.

### [☕ Sponsor on GitHub → github.com/sponsors/arisohandriputra](https://github.com/sponsors/arisohandriputra)

[![Sponsor](https://img.shields.io/badge/Sponsor-%E2%9D%A4-ea4aaa?style=for-the-badge&logo=github-sponsors&logoColor=white)](https://github.com/sponsors/arisohandriputra)

</div>

---

<img width="941" height="668" alt="image" src="https://github.com/user-attachments/assets/f2a9edbc-ebdf-4992-b013-0e9a09428478" />

A Windows Forms code editor control for .NET. Drop it into any WinForms project and you get a working code editor — syntax highlighting, line numbers, themes, minimap, undo/redo, auto-indent, find & replace, the whole thing. No external dependencies.

I built this because I kept rewriting the same editor boilerplate across different projects. Now I just reference the DLL and move on.

---

## What's included

Everything works out of the box without any extra setup:

- Syntax highlighting for 13 languages
- Line number gutter that scrolls with the content
- **Minimap panel** — color-coded code overview with viewport indicator and click/drag navigation
- 7 built-in themes (Dark, Light, Monokai, Solarized Dark, Solarized Light, Nord, Dracula), easy to make your own
- Undo/Redo up to 500 steps back
- Auto-indent on Enter
- Auto-close for `(`, `{`, `[`
- Find & Replace with case-sensitive and whole-word options
- Comment/uncomment selected lines in one shot
- Indent and outdent multiple lines at once
- Current line highlight
- Go To Line dialog with live preview and slider
- Word wrap toggle
- Configurable tab size
- Open and save files directly from the control
- Right-click context menu with all common actions and keyboard shortcut hints

---

## Supported languages

C#, C++, Java, JavaScript, Python, PHP, Go, Rust, Visual Basic, HTML, CSS, SQL, XML.

Need something else? You can add your own language by subclassing `LanguageDefinition`. There's an example near the bottom of this file.

---

## Getting started

### Build it

```
msbuild CodeEditorLib.sln /p:Configuration=Release
```

Grab `bin\Release\CodeEditorLib.dll` and add it as a reference in your project. Or just add `CodeEditorLib.csproj` directly to your solution as a project reference — whichever you prefer.

### Add it to a form

```csharp
using CodeEditorLib.Controls;
using CodeEditorLib.Themes;

public partial class MainForm : Form
{
    private CodeEditorControl _editor;

    public MainForm()
    {
        InitializeComponent();

        _editor = new CodeEditorControl { Dock = DockStyle.Fill };
        _editor.LanguageName = "CSharp";
        _editor.Theme = new DarkTheme();
        _editor.Code = "// start typing";

        Controls.Add(_editor);
    }
}
```

That's really all you need to get a fully working editor on screen.

---

## Configuration

All of these can be changed at any point, not just at startup:

```csharp
_editor.TabSize              = 4;
_editor.ShowLineNumbers      = true;
_editor.HighlightCurrentLine = true;
_editor.AutoBrackets         = true;
_editor.AutoIndent           = true;
_editor.WordWrap             = false;
_editor.ShowMinimap          = true;
_editor.ReadOnly             = false;
```

### Switching themes

```csharp
// set directly by instance
_editor.Theme = new DarkTheme();
_editor.Theme = new LightTheme();
_editor.Theme = new MonokaiTheme();
_editor.Theme = new SolarizedDarkTheme();
_editor.Theme = new SolarizedLightTheme();
_editor.Theme = new NordTheme();
_editor.Theme = new DraculaTheme();

// or by name via the registry
_editor.ThemeName = "Dracula";
```

You can also browse all available themes at runtime:

```csharp
foreach (string name in ThemeRegistry.Instance.GetSortedThemeNames())
{
    comboTheme.Items.Add(name);
}
```

### Setting the language

```csharp
// by name
_editor.LanguageName = "Python";

// by file extension
_editor.Language = LanguageRegistry.Instance.GetByExtension(".rs");

// auto-detect from a file path — handy when the user opens a file
_editor.Language = LanguageRegistry.Instance.GetByFilePath(@"C:\project\main.go");
```

### Opening and saving files

```csharp
_editor.OpenFile(@"C:\project\main.cs");   // loads content and auto-detects language
_editor.SaveFile(@"C:\project\main.cs");   // writes current content to disk
```

---

## Minimap

The minimap is shown on the right side of the editor by default. It renders a compact color-coded overview of the entire file — different line types (keywords, imports, comments, strings, control flow) are drawn in distinct colors so you can visually orient yourself in a large file at a glance.

Click anywhere on the minimap to jump to that position. Click and drag to scroll continuously. The viewport indicator shows exactly which portion of the file is currently visible in the editor.

```csharp
_editor.ShowMinimap = true;   // show/hide
_editor.ShowMinimap = false;
```

The minimap can also be toggled at runtime from the editor's right-click context menu.

---

## Keyboard shortcuts

| Shortcut | Action |
|---|---|
| `Ctrl+Z` | Undo |
| `Ctrl+Y` | Redo |
| `Ctrl+/` | Toggle comment |
| `Ctrl+F` | Find |
| `Ctrl+H` | Replace |
| `Ctrl+G` | Go To Line |
| `Tab` | Indent selection (when text is selected) |
| `Shift+Tab` | Outdent selection |

---

## Events

Hook into these if you need to react to what the user is doing:

```csharp
// update a status bar with line/column info
_editor.CaretPositionChanged += (s, e) =>
{
    statusLabel.Text = $"Ln {e.Line}  Col {e.Column}";
};

// track content changes
_editor.CodeTextChanged += (s, e) =>
{
    titleBar.Text = "MyEditor *";
};

// know when find/replace finishes
_editor.FindReplaceCompleted += (s, e) =>
{
    statusLabel.Text = $"{e.MatchCount} replacement(s) made";
};

// react to language switches
_editor.LanguageChanged += (s, e) =>
{
    langLabel.Text = e.LanguageName;
};
```

---

## Calling editor actions from your UI

If you want toolbar buttons or menu items to drive the editor:

```csharp
undoButton.Click    += (s, e) => _editor.Undo();
redoButton.Click    += (s, e) => _editor.Redo();
commentButton.Click += (s, e) => _editor.ToggleComment();
indentButton.Click  += (s, e) => _editor.IndentSelection();
outdentButton.Click += (s, e) => _editor.OutdentSelection();
findButton.Click    += (s, e) => _editor.ShowFindDialog();
replaceButton.Click += (s, e) => _editor.ShowReplaceDialog();
gotoButton.Click    += (s, e) => _editor.ShowGotoDialog();
openButton.Click    += (s, e) => _editor.OpenFile(filePath);
saveButton.Click    += (s, e) => _editor.SaveFile(filePath);

// keep undo/redo buttons in sync
undoButton.Enabled = _editor.CanUndo;
redoButton.Enabled = _editor.CanRedo;
```

---

## Project structure

```
CodeEditorLib/
├── Controls/
│   ├── CodeEditorControl.cs      # the main UserControl
│   ├── LineNumberPanel.cs        # the line number gutter
│   ├── MinimapPanel.cs           # color-coded minimap with viewport indicator
│   └── GoToLineDialog.cs         # go to line dialog with slider and live preview
├── Core/
│   ├── CodeFormatter.cs          # indent, outdent, comment helpers
│   ├── LanguageDefinition.cs     # base class for all languages
│   ├── SyntaxHighlighter.cs      # regex-based tokenizer
│   ├── SyntaxRule.cs             # a single highlighting rule
│   ├── SyntaxToken.cs            # a matched token with position and type
│   ├── TextSelection.cs          # selection range
│   └── UndoRedoManager.cs        # stack-based undo/redo
├── Events/
│   └── CodeEditorEventArgs.cs    # event arg classes
├── Highlighting/
│   ├── HighlightScheme.cs        # collection of styles per token type
│   └── HighlightStyle.cs         # foreground color + bold/italic
├── Languages/
│   ├── LanguageRegistry.cs       # singleton, lookup by name or extension
│   └── [13 language files]
├── Themes/
│   ├── EditorTheme.cs            # abstract base
│   ├── ThemeRegistry.cs          # singleton, lookup by name
│   ├── DarkTheme.cs
│   ├── LightTheme.cs
│   ├── MonokaiTheme.cs
│   ├── SolarizedDarkTheme.cs
│   ├── SolarizedLightTheme.cs
│   ├── NordTheme.cs
│   └── DraculaTheme.cs
└── Properties/
    └── AssemblyInfo.cs
```

---

## Adding a custom language

Subclass `LanguageDefinition`, define your regex rules, and register it once at startup. Here's Lua as an example:

```csharp
using System.Text.RegularExpressions;
using CodeEditorLib.Core;

public class LuaLanguage : LanguageDefinition
{
    public LuaLanguage()
    {
        Name = "Lua";
        AddExtension(".lua");

        CommentSingleLine     = "--";
        CommentMultiLineStart = "--[[";
        CommentMultiLineEnd   = "]]";

        AddRule(new SyntaxRule(
            new Regex(@"\b(and|break|do|else|elseif|end|false|for|function|if|in|local|nil|not|or|repeat|return|then|true|until|while)\b"),
            TokenType.Keyword, priority: 10));

        AddRule(new SyntaxRule(
            new Regex(@"--[^\n]*"),
            TokenType.Comment, priority: 20));

        AddRule(new SyntaxRule(
            new Regex(@"""[^""\\]*(?:\\.[^""\\]*)*""|'[^'\\]*(?:\\.[^'\\]*)*'"),
            TokenType.String, priority: 15));

        AddRule(new SyntaxRule(
            new Regex(@"\b\d+(\.\d+)?\b"),
            TokenType.Number, priority: 5));
    }
}
```

Register it once when your app starts:

```csharp
LanguageRegistry.Instance.Register(new LuaLanguage());
```

After that it works exactly like any built-in language — by name, by extension, by file path.

---

## Making a custom theme

Subclass `EditorTheme`, fill in the colors, and build the highlight scheme. You can also register it in `ThemeRegistry` so it's accessible by name:

```csharp
using System.Drawing;
using CodeEditorLib.Highlighting;
using CodeEditorLib.Themes;

public class SolarizedDarkTheme : EditorTheme
{
    public SolarizedDarkTheme() : base("Solarized Dark") { }

    public override Color BackgroundColor            => Color.FromArgb(0, 43, 54);
    public override Color DefaultTextColor           => Color.FromArgb(131, 148, 150);
    public override Color SelectionColor             => Color.FromArgb(7, 54, 66);
    public override Color CurrentLineColor           => Color.FromArgb(7, 54, 66);
    public override Color LineNumberBackColor        => Color.FromArgb(0, 43, 54);
    public override Color LineNumberForeColor        => Color.FromArgb(88, 110, 117);
    public override Color LineNumberBorderColor      => Color.FromArgb(7, 54, 66);
    public override Color CaretColor                 => Color.White;
    public override Color BracketMatchColor          => Color.FromArgb(88, 110, 117);
    public override Color AutoCompleteBackColor      => Color.FromArgb(7, 54, 66);
    public override Color AutoCompleteForeColor      => Color.FromArgb(131, 148, 150);
    public override Color AutoCompleteSelectionColor => Color.FromArgb(38, 139, 210);

    protected override void BuildScheme(HighlightScheme scheme)
    {
        scheme.Set(TokenType.Keyword,  new HighlightStyle(Color.FromArgb(133, 153, 0)));
        scheme.Set(TokenType.String,   new HighlightStyle(Color.FromArgb(42, 161, 152)));
        scheme.Set(TokenType.Comment,  new HighlightStyle(Color.FromArgb(88, 110, 117), italic: true));
        scheme.Set(TokenType.Number,   new HighlightStyle(Color.FromArgb(211, 54, 130)));
        scheme.Set(TokenType.Type,     new HighlightStyle(Color.FromArgb(181, 137, 0)));
        scheme.Set(TokenType.Operator, new HighlightStyle(Color.FromArgb(131, 148, 150)));
    }
}
```

Register it so it's accessible by name:

```csharp
ThemeRegistry.Instance.Register(new SolarizedDarkTheme());
_editor.ThemeName = "Solarized Dark";
```

Or just set it directly:

```csharp
_editor.Theme = new SolarizedDarkTheme();
```

---

## Requirements

- Windows (WinForms dependency)
- .NET Framework 2.0 or higher
- Visual Studio 2005+ or any MSBuild-compatible toolchain

---

## Contributing

Pull requests are welcome. If you're adding a language, subclass `LanguageDefinition` and register it in `LanguageRegistry.RegisterDefaults()`. For a new theme, subclass `EditorTheme` and register it in `ThemeRegistry.RegisterDefaults()`. Keep the PR focused on one thing and make sure existing behavior still works.

For bugs, a small reproducible snippet is worth more than a paragraph of description.

---

## License

MIT. See [LICENSE](LICENSE) for the full text.

Copyright (c) 2026 Ari Sohandri Putra

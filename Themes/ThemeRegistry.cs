// =============================================================================
// Theme Registry for CodeEditorLib
// =============================================================================
//
// Description :
// Manages registration, storage, and retrieval of all editor themes
// used by the CodeEditorLib syntax highlighting system.
//
// This class is responsible for:
// - Maintaining a centralized theme registry (singleton instance)
// - Registering built-in and custom themes
// - Providing theme lookup by name
// - Returning available theme collections
// - Sorting theme names for UI display
//
// -----------------------------------------------------------------------------
//
// Core Features:
// - Singleton pattern for global theme access
// - Case-insensitive theme name storage
// - Built-in theme auto-registration
// - Support for custom theme extensions
//
// -----------------------------------------------------------------------------
//
// Registered Default Themes:
// - DarkTheme
// - LightTheme
// - MonokaiTheme
// - SolarizedDarkTheme
// - SolarizedLightTheme
// - NordTheme
// - DraculaTheme
//
// -----------------------------------------------------------------------------
//
// Theme Lookup System:
// Themes are retrieved by name using a dictionary lookup.
// Case-insensitive matching ensures flexible access.
//
// Example:
// ThemeRegistry.Instance.GetByName("dark")
//
// -----------------------------------------------------------------------------
//
// Thread / Lifecycle Notes:
// This registry is lazily initialized via a singleton instance.
// First access triggers creation and default theme registration.
//
// -----------------------------------------------------------------------------
//
// Usage Scenarios:
// - Theme switching in code editor UI
// - Settings / preferences panel
// - Plugin-based theme injection
// - Runtime theme customization
//
// -----------------------------------------------------------------------------
//
// Author : Ari Sohandri Putra
// Repository : https://github.com/arisohandriputra/CodeEditorLib

// License : MIT License
// Copyright : Copyright (c) 2026 Ari Sohandri Putra
//
// -----------------------------------------------------------------------------
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
// CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// =============================================================================

using System;
using System.Collections.Generic;

namespace CodeEditorLib.Themes
{
    public class ThemeRegistry
    {
        private static ThemeRegistry _instance;
        private Dictionary<string, EditorTheme> _themes;

        private ThemeRegistry()
        {
            _themes = new Dictionary<string, EditorTheme>(StringComparer.OrdinalIgnoreCase);
            RegisterDefaults();
        }

        public static ThemeRegistry Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ThemeRegistry();
                return _instance;
            }
        }

        private void RegisterDefaults()
        {
            Register(new DarkTheme());
            Register(new LightTheme());
            Register(new MonokaiTheme());
            Register(new SolarizedDarkTheme());
            Register(new SolarizedLightTheme());
            Register(new NordTheme());
            Register(new DraculaTheme());
        }

        public void Register(EditorTheme theme)
        {
            if (theme == null) throw new ArgumentNullException("theme");
            _themes[theme.Name] = theme;
        }

        public EditorTheme GetByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            EditorTheme t;
            _themes.TryGetValue(name, out t);
            return t;
        }

        public ICollection<string> GetThemeNames()
        {
            return _themes.Keys;
        }

        public ICollection<EditorTheme> GetAllThemes()
        {
            return _themes.Values;
        }

        public string[] GetSortedThemeNames()
        {
            string[] names = new string[_themes.Count];
            _themes.Keys.CopyTo(names, 0);
            Array.Sort(names, StringComparer.OrdinalIgnoreCase);
            return names;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using CodeEditorLib.Core;

namespace CodeEditorLib.Languages
{
    public class LanguageRegistry
    {
        private static LanguageRegistry _instance;
        private Dictionary<string, LanguageDefinition> _byName;
        private Dictionary<string, LanguageDefinition> _byExtension;

        private LanguageRegistry()
        {
            _byName = new Dictionary<string, LanguageDefinition>(StringComparer.OrdinalIgnoreCase);
            _byExtension = new Dictionary<string, LanguageDefinition>(StringComparer.OrdinalIgnoreCase);
        }

        public static LanguageRegistry Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LanguageRegistry();
                    _instance.RegisterDefaults();
                }
                return _instance;
            }
        }

        private void RegisterDefaults()
        {
            Register(new CSharpLanguage());
            Register(new JavaLanguage());
            Register(new JavaScriptLanguage());
            Register(new PythonLanguage());
            Register(new HtmlLanguage());
            Register(new CssLanguage());
            Register(new SqlLanguage());
            Register(new XmlLanguage());
            Register(new PhpLanguage());
            Register(new CppLanguage());
            Register(new VbLanguage());
            Register(new GoLanguage());
            Register(new RustLanguage());
        }

        public void Register(LanguageDefinition language)
        {
            if (language == null) throw new ArgumentNullException("language");
            _byName[language.Name] = language;
            foreach (string ext in language.FileExtensions)
                _byExtension[ext] = language;
        }

        public LanguageDefinition GetByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            LanguageDefinition lang;
            _byName.TryGetValue(name, out lang);
            return lang;
        }

        public LanguageDefinition GetByExtension(string extension)
        {
            if (string.IsNullOrEmpty(extension)) return null;
            if (!extension.StartsWith(".")) extension = "." + extension;
            LanguageDefinition lang;
            _byExtension.TryGetValue(extension.ToLower(), out lang);
            return lang;
        }

        public LanguageDefinition GetByFilePath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return null;
            string ext = Path.GetExtension(filePath);
            return GetByExtension(ext);
        }

        public ICollection<string> GetLanguageNames()
        {
            return _byName.Keys;
        }

        public ICollection<LanguageDefinition> GetAllLanguages()
        {
            return _byName.Values;
        }

        public bool Contains(string name)
        {
            return _byName.ContainsKey(name);
        }
    }
}

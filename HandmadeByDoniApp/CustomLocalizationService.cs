using HandmadeByDoniApp.Services.Data.Interfaces;
using Microsoft.Extensions.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HandmadeByDoniApp.Services.Data
{
    public class CustomLocalizationService : IStringLocalizer
    {
        //ResourceManager? _resourceManager;

        //public LocalizedString this[string name] => throw new NotImplementedException();

        //public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();

        //ResourceManager? ResourceManager
        //{
        //    get
        //    {
        //        if (_resourceManager == null)
        //            _resourceManager = new ResourceManager("HandmadeByDoniApp.Web.Resources.App", typeof(CustomLocalizationService).Assembly);
        //        return _resourceManager;
        //    }
        //}

        //public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        //{
        //    throw new NotImplementedException();
        //}

        //public string GetString(string key)
        //{
        //    var culture = CultureInfo.CurrentUICulture.Name;
        //    string value = null;
        //    switch (culture)
        //    {
        //        case "en-US":
        //            value = ResourceManager?.GetString(key);
        //            break;
        //        case "bg-BG":
        //            value = ResourceManager?.GetString(key);
        //            break;
        //    }
        //    return value;
        //}

        private readonly ResourceManager _resourceManager;
        private readonly CultureInfo _currentCulture;

        public CustomLocalizationService(string baseName, string resourceDir)
        {
            // Инициализиране на ResourceManager за работа с .resx файловете
            _resourceManager = new ResourceManager(baseName, typeof(CustomLocalizationService).Assembly);
            _currentCulture = CultureInfo.CurrentUICulture;
        }

        public LocalizedString this[string name]
        {
            get
            {
                var value = _resourceManager.GetString(name, _currentCulture);
                return new LocalizedString(name, value ?? $"[{name}]");
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = _resourceManager.GetString(name, _currentCulture);
                var value = string.Format(format ?? $"[{name}]", arguments);
                return new LocalizedString(name, value);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            // Връщане на всички низове от текущата култура
            ResourceSet resourceSet = _resourceManager.GetResourceSet(_currentCulture, true, includeParentCultures);
            foreach (DictionaryEntry entry in resourceSet)
            {
                yield return new LocalizedString(entry.Key.ToString(), entry.Value.ToString());
            }
        }

        // Можете да добавите и логика за задаване на друга култура, ако е нужно
    }
}

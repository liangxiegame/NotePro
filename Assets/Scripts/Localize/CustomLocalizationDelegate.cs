using System.Collections.Generic;
using RSG;
using Unity.UIWidgets.material;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace NotePro
{
    public class CustomLocalizationDelegate : LocalizationsDelegate<MaterialLocalizations>
    {
        public static CustomLocalizationDelegate Del = new CustomLocalizationDelegate();

        public static List<Locale> SupportedLocales = new List<Locale>()
        {
            new Locale("zn", "CN"),
            new Locale("en", "US")
        };

        public override bool isSupported(Locale locale)
        {
            return SupportedLocales.Contains(locale);
        }

        public override IPromise<object> load(Locale locale)
        {
            var lang = "en_US";

            if (Application.systemLanguage == SystemLanguage.Chinese ||
                Application.systemLanguage == SystemLanguage.ChineseSimplified)
            {
                lang = "zh_CN";
            }

            switch (lang)
            {
                case "en_US":
                    return new Promise<object>((resolveAction, expceptionAction) => resolveAction(new en_US()));
                case "zh_CN":
                    return new Promise<object>((resolveAction, expceptionAction) => resolveAction(new zh_CN()));
            }

            return new Promise<object>((resolveAction, expceptionAction) => resolveAction(new en_US()));
        }

        public override bool shouldReload(LocalizationsDelegate old)
        {
            return false;
        }
    }
}
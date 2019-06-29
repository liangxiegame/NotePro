using System.Collections.Generic;
using QFramework.UIWidgets.ReduxPersist;
using Unity.UIWidgets;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace NotePro
{
    public class App : UIWidgetsPanel
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            FontManager.instance.addFont(Resources.Load<Font>("MaterialIcons-Regular"), "Material Icons");
            FontManager.instance.addFont(Resources.Load<Font>("fonts/ProductSans-Bold"), "Sans Bold");
            FontManager.instance.addFont(Resources.Load<Font>("fonts/ProductSans-Regular"), "Sans Regular");
        }

        protected override Widget createWidget()
        {
            var store = new Store<AppState>(AppReducer.Reduce, AppState.Load(),
                ReduxPersistMiddleware.create<AppState>());

            return new StoreProvider<AppState>(
                store: store,
                child: new MaterialApp(
                    localizationsDelegates: new List<LocalizationsDelegate<MaterialLocalizations>>()
                    {
                        CustomLocalizationDelegate.Del,
                        DefaultMaterialLocalizations.del
                    },
                    supportedLocales: CustomLocalizationDelegate.SupportedLocales,

                    theme: new ThemeData(
                        primarySwatch: Colors.deepPurple,
                        textTheme: new TextTheme(
                            headline: new TextStyle(
                                fontFamily: "Sans Bold",
                                fontWeight: FontWeight.bold,
                                fontSize: 24,
                                color: Colors.black
                            ),
                            title: new TextStyle(
                                fontFamily: "Sans Bold",
                                fontWeight: FontWeight.bold,
                                fontSize: 20,
                                color: Colors.black
                            ),
                            subhead: new TextStyle(
                                fontFamily: "Sans Regular",
                                fontWeight: FontWeight.normal,
                                fontSize: 18,
                                color: Colors.black
                            ),
                            body1:new TextStyle(
                                fontFamily: "Sans Regular",
                                fontWeight: FontWeight.normal,
                                fontSize: 16,
                                color: Colors.black
                            ),
                            body2:new TextStyle(
                                fontFamily: "Sans Regular",
                                fontWeight: FontWeight.normal,
                                fontSize: 16,
                                color: Colors.black
                            ),
                            subtitle: new TextStyle(
                                fontFamily: "Sans Regular",
                                fontWeight: FontWeight.normal,
                                fontSize: 14,
                                color: Colors.black
                            )
                        )
                    ),
                    home: new NoteList()
                )
            );
        }
    }
}
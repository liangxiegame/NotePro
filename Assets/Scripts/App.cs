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
        }

        protected override Widget createWidget()
        {
            var store = new Store<AppState>(AppReducer.Reduce, new AppState());

            return new StoreProvider<AppState>(
                store: store,
                child: new MaterialApp(
                    home: new Scaffold(
                        floatingActionButton: new FloatingActionButton(
                            child: new Icon(Icons.add, color: Colors.black),
                            onPressed: () => { Debug.Log("on pressed"); },
                            backgroundColor: Colors.white,
                            shape: new CircleBorder(
                                side: new BorderSide(
                                    color: Colors.black,
                                    width: 2f
                                )
                            )
                        )
                    )
                )
            );
        }
    }
}
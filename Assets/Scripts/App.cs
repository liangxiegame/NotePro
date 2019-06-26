using Unity.UIWidgets.engine;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace NotePro
{
    public class App : UIWidgetsPanel
    {
        protected override Widget createWidget()
        {
            return new Text("Hello World",
                style: new TextStyle(
                    color: Colors.white,
                    fontWeight: FontWeight.bold
                )
            );
        }
    }
}
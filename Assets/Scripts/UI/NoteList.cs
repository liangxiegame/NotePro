using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;

namespace NotePro
{
    public class NoteList : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                floatingActionButton: new FloatingActionButton(
                    child: new Icon(Icons.add, color: Colors.black),
                    onPressed: () =>
                    {
                        Navigator.of(context).push(new MaterialPageRoute(
                                buildContext => new NoteEditor()
                            )
                        );
                    },
                    backgroundColor: Colors.white,
                    shape: new CircleBorder(
                        side: new BorderSide(
                            color: Colors.black,
                            width: 2f
                        )
                    )
                ),
                body: new StoreConnector<AppState, int>(
                    converter: state => state.Notes.Count,
                    builder: (buildContext, model, dispatcher) => new Text(model.ToString()))
            );
        }
    }
}
using System.Collections.Generic;
using System.Linq;
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
            return new StoreConnector<AppState, AppState>(
                converter: state => state,
                builder: (buildCtx, model, dispatcher) =>
                {
                    var filter = model.Filter;

                    var list = model.Notes
                        .Where(note =>
                        {
                            if (filter.Type == FilterType.ByInbox)
                            {
                                return note.Priority == 0 && note.ColorIndex == 0;
                            }

                            if (filter.Type == FilterType.ByAll)
                            {
                                return true;
                            }

                            return false;
                        })
                        .ToList();

                    return new Scaffold(
                        appBar: new AppBar(
                            title: new Text(filter.Title,
                                style: Theme.of(context).textTheme.headline
                            ),
                            centerTitle: true,
                            backgroundColor: Colors.white,
                            elevation: 0,
                            iconTheme: Theme.of(context).iconTheme
                        ),
                        drawer: new SideDrawer(),
                        floatingActionButton: new FloatingActionButton(
                            child: new Icon(Icons.add, color: Colors.black),
                            onPressed: () =>
                            {
                                Navigator.of(context).push(new MaterialPageRoute(
                                        buildContext => new NoteEditor(NoteEditorMode.CREATION, new Note())
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
                        backgroundColor: Colors.white,
                        body: ListView.builder(
                            itemCount: list.Count,
                            itemBuilder: (context1, index) =>
                            {
                                var note = list[index];

                                return new NoteWidget(note, () =>
                                {
                                    Navigator.of(context)
                                        .push(new MaterialPageRoute(ctx =>
                                            new NoteEditor(NoteEditorMode.MODIFICATION, note)));
                                });
                            }
                        )
                    );
                }
            );
        }
    }
}
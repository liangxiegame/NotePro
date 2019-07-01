using System.Collections.Generic;
using System.Linq;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
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
                    Widget text = new Text(filter.Title,
                        style: Theme.of(context).textTheme.headline
                    );


                    var list = model.Notes
                        .Where(note =>
                        {
                            switch (filter.Type)
                            {
                                case FilterType.ByInbox:
                                    return note.Priority == 0 && note.ColorIndex == 0 &&
                                           string.IsNullOrWhiteSpace(note.NotebookId);
                                case FilterType.ByAll:
                                    return true;
                                case FilterType.ByColor:
                                    text = new ColorCircle(filter.ColorIndex, size: 35);
                                    return note.ColorIndex == filter.ColorIndex;
                                case FilterType.ByPriority:
                                    text = new Text(Utils.PriorityText(filter.PriorityIndex),
                                        style: Theme.of(context).textTheme.headline
                                            .copyWith(color: Utils.PriorityColor(filter.PriorityIndex)));
                                    return note.Priority == filter.PriorityIndex;
                                case FilterType.ByNotebook:
                                    return note.NotebookId == filter.NotebookId;
                                default:
                                    return false;
                            }
                        })
                        .ToList();

                    return new Scaffold(
                        appBar: new AppBar(
                            title: text,
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
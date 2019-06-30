using System.Collections.Generic;
using System.Linq;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;
using UnityEngine;

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

                    if (filter == null)
                    {
                        filter = Filter.ByInbox(context);
                    }

                    var noteList = model.Notes.Where(note =>
                    {
                        if (filter.FilterType == FilterType.ByInbox)
                        {
                            return note.ColorIndex == 0 && note.Priority == 0;
                        }
                        else if (filter.FilterType == FilterType.ByColor)
                        {
                            return note.ColorIndex == filter.ColorIndex;
                        }
                        else if (filter.FilterType == FilterType.ByPriority)
                        {
                            return note.Priority == filter.PriorityIndex;
                        }

                        return false;
                    }).ToList();

                    return new Scaffold(
                        appBar: new AppBar(
                            title: new Text(filter.Title,
                                style: Theme.of(context).textTheme.headline
                            ),
                            centerTitle: true,
                            backgroundColor: Colors.white,
                            elevation: 0,
                            iconTheme: new IconThemeData(
                                color: Colors.black
                            )
                        ),
                        floatingActionButton:
                        new FloatingActionButton(
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
                        drawer: new SideDrawer(),
                        body: ListView.builder(
                            itemCount: noteList.Count,
                            itemBuilder: (context1, index) =>
                            {
                                var note = noteList[index];

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
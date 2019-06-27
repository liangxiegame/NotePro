using System.Collections.Generic;
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
            return new Scaffold(
                appBar: new AppBar(
                    title: new Text("Notes",
                        style: Theme.of(context).textTheme.headline
                    ),
                    centerTitle: true,
                    backgroundColor: Colors.white,
                    elevation: 0
                ),
                floatingActionButton:
                new FloatingActionButton(
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
                body: new StoreConnector<AppState, List<Note>>(
                    converter: state => state.Notes,
                    builder: (buildContext, model, dispatcher) =>
                    {
                        return ListView.builder(
                            itemCount: model.Count,
                            itemBuilder: (context1, index) =>
                            {
                                var note = model[index];

                                return new NoteWidget(note, () =>
                                {
                                    Debug.Log("开始编辑");
                                });
                            }
                        );
                    }
                )
            );
        }
    }
}
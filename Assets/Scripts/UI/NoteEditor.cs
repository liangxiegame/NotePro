using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.service;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace NotePro
{
    public class NoteEditor : StatefulWidget
    {
        public override State createState()
        {
            return new NoteEditorState();
        }
    }


    public class NoteEditorState : State<NoteEditor>
    {
        public string mTitle       = string.Empty;
        public string mDescription = string.Empty;

        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState, object>(
                converter: state => null,
                builder: (buildContext, model, dispatcher) =>
                {
                    return new Scaffold(
                        appBar: new AppBar(
                            elevation: 0,
                            title: new Text("Add Note",
                                style: Theme.of(context).textTheme.headline),
                            backgroundColor: Colors.white,
                            leading: new IconButton(
                                icon: new Icon(Icons.arrow_back_ios, color: Colors.black),
                                onPressed: () => { Navigator.of(context).pop(); }
                            ),
                            actions: new List<Widget>()
                            {
                                string.IsNullOrEmpty(mTitle)
                                    ? new Container() as Widget
                                    : new IconButton(
                                        icon: new Icon(Icons.save, color: Colors.black),
                                        onPressed: () =>
                                        {
                                            dispatcher.dispatch(new AddNoteAction(new Note()
                                            {
                                                Title = mTitle,
                                                Description = mDescription,
                                            }));

                                            Navigator.pop(context);
                                        }
                                    ),
                                new IconButton(
                                    icon: new Icon(Icons.delete, color: Colors.black),
                                    onPressed: () => { }
                                )
                            }
                        ),
                        body: new Container(
                            color: Colors.white,
                            child: new Column(
                                children: new List<Widget>()
                                {
                                    new Padding(
                                        padding: EdgeInsets.all(16),
                                        child: new TextField(
                                            onChanged: value => { this.setState(() => { mTitle = value; }); },
                                            style: Theme.of(context).textTheme.body1,
                                            decoration: new InputDecoration(
                                                hintText: "Title"
                                            )
                                        )
                                    ),
                                    new Expanded(
                                        child: new Padding(
                                            padding: EdgeInsets.all(16),
                                            child: new TextField(
                                                onChanged: value => { mDescription = value; },
                                                style: Theme.of(context).textTheme.body2,
                                                keyboardType: TextInputType.multiline,
                                                maxLength: 255,
                                                maxLines: 10,
                                                decoration: new InputDecoration(
                                                    hintText: "Description"
                                                )
                                            )
                                        )
                                    )
                                }
                            )
                        )
                    );
                }
            );
        }
    }
}
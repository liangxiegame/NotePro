using System;
using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace NotePro
{
    public class NoteWidget : StatelessWidget
    {
        private Note mNote;

        private Action mOnClick;

        public NoteWidget(Note note, Action onClick)
        {
            mNote = note;
            mOnClick = onClick;
        }

        public override Widget build(BuildContext context)
        {
            return new GestureDetector(
                onTap: () => { mOnClick(); },
                child: new Padding(
                    padding: EdgeInsets.all(8),
                    child: new Container(
                        padding: EdgeInsets.all(8),
                        child: new Container(
                            padding: EdgeInsets.all(8),
                            decoration: new BoxDecoration(
                                color: Colors.white,
                                border: Border.all(width: 2, color: Colors.black),
                                borderRadius: BorderRadius.circular(8.0f)
                            ),
                            child: new Column(
                                children: new List<Widget>()
                                {
                                    new Row(
                                        children: new List<Widget>()
                                        {
                                            new Expanded(
                                                child: new Padding(
                                                    padding: EdgeInsets.all(8.0f),
                                                    child: new Text(mNote.Title,
                                                        style: Theme.of(context).textTheme.body1
                                                    )
                                                )
                                            ),
                                            new Text("!!!", style: new TextStyle(color: Colors.red))
                                        }
                                    ),
                                    new Padding(
                                        padding: EdgeInsets.all(8.0f),
                                        child: new Row(
                                            mainAxisAlignment: MainAxisAlignment.start,
                                            children: new List<Widget>()
                                            {
                                                new Expanded(
                                                    child: new Text(
                                                        mNote.Description ?? "",
                                                        style: Theme.of(context).textTheme.body2
                                                    )
                                                )
                                            }
                                        )
                                    ),
                                    new Row(
                                        mainAxisAlignment: MainAxisAlignment.end,
                                        children: new List<Widget>()
                                        {
                                            new Text(mNote.Date,
                                                style: Theme.of(context).textTheme.subtitle
                                            )
                                        }
                                    )
                                }
                            )
                        )
                    )
                )
            );
        }
    }
}
using System;
using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.service;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using DialogUtils = Unity.UIWidgets.material.DialogUtils;

namespace NotePro
{
    public enum NoteEditorMode
    {
        /// <summary>
        /// 创建模式
        /// </summary>
        CREATION,

        /// <summary>
        /// 编辑模式
        /// </summary>
        MODIFICATION
    }

    public class NoteEditor : StatefulWidget
    {
        public NoteEditorMode Mode { get; }
        public Note           Note { get; }

        public NoteEditor(NoteEditorMode mode, Note note)
        {
            this.Mode = mode;
            this.Note = note;
        }

        public override State createState()
        {
            return new NoteEditorState();
        }
    }


    public class NoteEditorState : State<NoteEditor>
    {
        public override void initState()
        {
            base.initState();

            mTitleController = new TextEditingController(widget.Note.Title);
            mDescriptionController = new TextEditingController(widget.Note.Description);
        }


        public TextEditingController mTitleController;
        public TextEditingController mDescriptionController;

        public bool SaveBtnVisible
        {
            get
            {
                if (widget.Mode == NoteEditorMode.CREATION)
                {
                    return !string.IsNullOrWhiteSpace(mTitleController.text);
                }
                else
                {
                    return !string.IsNullOrWhiteSpace(mTitleController.text) &&
                           (widget.Note.Title != mTitleController.text ||
                            widget.Note.Description != mDescriptionController.text);
                }
            }
        }

        void ShowDiscardDialog(BuildContext context)
        {
            DialogUtils.showDialog(
                context,
                builder: buildContext => new AlertDialog(
                    shape: new RoundedRectangleBorder(
                        borderRadius: BorderRadius.all(Radius.circular(10.0f))
                    ),
                    title: new Text(
                        "Discard Changes?",
                        style: Theme.of(context).textTheme.body1
                    ),
                    content: new Text(
                        "Are you sure you want to discard changes?",
                        style: Theme.of(context).textTheme.body2
                    ),
                    actions: new List<Widget>()
                    {
                        new FlatButton(
                            child: new Text(
                                "Yes",
                                style: Theme.of(context)
                                    .textTheme
                                    .body1
                                    .copyWith(color: Colors.purple)
                            ),
                            onPressed: () =>
                            {
                                Navigator.pop(buildContext);
                                Navigator.pop(buildContext);
                            }
                        ),
                        new FlatButton(
                            child: new Text(
                                "No",
                                style: Theme.of(context)
                                    .textTheme
                                    .body1
                                    .copyWith(color: Colors.purple)
                            ),
                            onPressed: () =>
                            {
                                Navigator.pop(buildContext);
                            }
                        )
                    }
                )
            );
        }

        void ShowDeleteDialog(BuildContext context, Action onDelete)
        {
            DialogUtils.showDialog(
                context,
                builder: buildContext => new AlertDialog(
                    shape: new RoundedRectangleBorder(
                        borderRadius: BorderRadius.all(Radius.circular(10.0f))
                    ),
                    title: new Text(
                        "Delete Note?",
                        style: Theme.of(context).textTheme.body1
                    ),
                    content: new Text(
                        "Are you sure you want to delete this note?",
                        style: Theme.of(context).textTheme.body2
                    ),
                    actions: new List<Widget>()
                    {
                        new FlatButton(
                            child: new Text(
                                "Yes",
                                style: Theme.of(context)
                                    .textTheme
                                    .body1
                                    .copyWith(color: Colors.purple)
                            ),
                            onPressed: () =>
                            {
                                Navigator.pop(buildContext);
                                onDelete.Invoke();
                            }
                        ),
                        new FlatButton(
                            child: new Text(
                                "No",
                                style: Theme.of(context)
                                    .textTheme
                                    .body1
                                    .copyWith(color: Colors.purple)
                            ),
                            onPressed: () => { Navigator.pop(buildContext); }
                        )
                    }
                )
            );
        }

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
                                onPressed: () =>
                                {
                                    if (widget.Mode == NoteEditorMode.MODIFICATION && SaveBtnVisible)
                                    {
                                        ShowDiscardDialog(context);
                                    }
                                    else
                                    {
                                        Navigator.of(context).pop();
                                    }

                                }
                            ),
                            actions: new List<Widget>()
                            {
                                SaveBtnVisible
                                    ? new IconButton(
                                        icon: new Icon(Icons.save, color: Colors.black),
                                        onPressed: () =>
                                        {
                                            widget.Note.Title = mTitleController.text;
                                            widget.Note.Description = mDescriptionController.text;


                                            if (widget.Mode == NoteEditorMode.CREATION)
                                            {
                                                dispatcher.dispatch(new AddNoteAction(widget.Note));
                                            }
                                            else
                                            {
                                                dispatcher.dispatch(new UpdateNoteAction(widget.Note));
                                            }

                                            Navigator.pop(context);
                                        }
                                    )
                                    : new Container() as Widget,

                                widget.Mode == NoteEditorMode.MODIFICATION
                                    ? new IconButton(
                                        icon: new Icon(Icons.delete, color: Colors.black),
                                        onPressed: () =>
                                        {
                                            ShowDeleteDialog(context, () =>
                                            {
                                                dispatcher.dispatch(new DeleteNoteAction(widget.Note));
                                                Navigator.of(context).pop();
                                            });
                                        }
                                    ) as Widget
                                    : new Container()
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
                                            controller: mTitleController,
                                            onChanged: value => { this.setState(() => { }); },
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
                                                controller: mDescriptionController,
                                                onChanged: value => { this.setState(() => { }); },
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
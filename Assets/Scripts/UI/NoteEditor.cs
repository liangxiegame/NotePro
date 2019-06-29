using System;
using System.Collections.Generic;
using markdown;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.service;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using DialogUtils = Unity.UIWidgets.material.DialogUtils;
using Text = Unity.UIWidgets.widgets.Text;

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
            mPriorityIndex = widget.Note.Priority;
            mColorIndex = widget.Note.ColorIndex;
        }


        private TextEditingController mTitleController;
        private TextEditingController mDescriptionController;
        private int                   mPriorityIndex = 0;
        private int                   mColorIndex    = 0;

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
                            widget.Note.Description != mDescriptionController.text ||
                            widget.Note.Priority != mPriorityIndex ||
                            widget.Note.ColorIndex != mColorIndex);
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
                        L.of(context).DiscardChanges,
                        style: Theme.of(context).textTheme.title
                    ),
                    content: new Text(
                        L.of(context).DiscardChangesContent,
                        style: Theme.of(context).textTheme.body2
                    ),
                    actions: new List<Widget>()
                    {
                        new FlatButton(
                            child: new Text(
                                L.of(context).Yes,
                                style: Theme.of(context)
                                    .textTheme
                                    .title
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
                                L.of(context).No,
                                style: Theme.of(context)
                                    .textTheme
                                    .title
                                    .copyWith(color: Colors.purple)
                            ),
                            onPressed: () => { Navigator.pop(buildContext); }
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
                        L.of(context).DeleteNote,
                        style: Theme.of(context).textTheme.title
                    ),
                    content: new Text(
                        L.of(context).DeleteNoteContent,
                        style: Theme.of(context).textTheme.body2
                    ),
                    actions: new List<Widget>()
                    {
                        new FlatButton(
                            child: new Text(
                                L.of(context).Yes,
                                style: Theme.of(context)
                                    .textTheme
                                    .title
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
                                L.of(context).No,
                                style: Theme.of(context)
                                    .textTheme
                                    .title
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
                            title: new Text(
                                widget.Mode == NoteEditorMode.CREATION ? L.of(context).AddNote : L.of(context).EditNote,
                                style: Theme.of(context).textTheme.headline),
                            backgroundColor: AppConst.Colors[mColorIndex],
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
                                            widget.Note.Priority = mPriorityIndex;
                                            widget.Note.ColorIndex = mColorIndex;


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
                            color: AppConst.Colors[mColorIndex],
                            child: new Column(
                                children: new List<Widget>()
                                {
                                    new PriorityPicker(
                                        mPriorityIndex,
                                        priorityIndex => { setState(() => { mPriorityIndex = priorityIndex; }); }
                                    ),
                                    new ColorPicker(mColorIndex,
                                        colorIndex => { setState(() => { mColorIndex = colorIndex; }); }),
                                    new Padding(
                                        padding: EdgeInsets.all(16),
                                        child: new TextField(
                                            controller: mTitleController,
                                            onChanged: value => { this.setState(() => { }); },
                                            style: Theme.of(context).textTheme.title,
                                            decoration: new InputDecoration(
                                                hintText: L.of(context).Title
                                            )
                                        )
                                    ),
                                    new Expanded(
                                        child: new Padding(
                                            padding: EdgeInsets.all(16),
                                            child: new Column(
                                                children: new List<Widget>()
                                                {
                                                    new TextField(
                                                        controller: mDescriptionController,
                                                        onChanged: value => { this.setState(() => { }); },
                                                        style: Theme.of(context).textTheme.subhead,
                                                        keyboardType: TextInputType.multiline,
                                                        maxLength: 255,
                                                        maxLines: 10,
                                                        decoration: new InputDecoration(
                                                            hintText: L.of(context).Description
                                                        )
                                                    ),
                                                    new Row(
                                                        mainAxisAlignment: MainAxisAlignment.end,
                                                        children: new List<Widget>
                                                        {
                                                            new OutlineButton(
                                                                child: new Text("Markdown 预览",
                                                                    style: Theme.of(context).textTheme.subhead),
                                                                onPressed: () =>
                                                                {
                                                                    Navigator.of(context)
                                                                        .push(new MaterialPageRoute(
                                                                            builder: context1 =>
                                                                                new MarkdownPreviewer(mColorIndex,
                                                                                    mTitleController.text,
                                                                                    mDescriptionController.text)));
                                                                }
                                                            )
                                                        }
                                                    ),
                                                }
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
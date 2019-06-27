using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.service;
using Unity.UIWidgets.widgets;
using UnityEngine;

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
                                        onPressed: () => { }
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
                                                onChanged: value => { this.setState(() => {  }); },
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
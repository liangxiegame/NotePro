using System.Collections.Generic;
using Unity.UIWidgets;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.widgets;

namespace NotePro
{
    public enum NotebookEditorMode
    {
        CREATION,
        MODIFICATION
    }

    public class NotebookEditor : StatefulWidget
    {
        public Dispatcher Dispatcher { get; }

        public NotebookEditorMode Mode { get; }

        public Notebook Notebook { get; }

        public NotebookEditor(Dispatcher dispatcher, NotebookEditorMode mode = NotebookEditorMode.CREATION,
            Notebook notebook = null)
        {
            Dispatcher = dispatcher;
            Notebook = notebook ?? new Notebook();
            Mode = mode;
        }

        public override State createState()
        {
            return new NotebookEditorState();
        }
    }

    public class NotebookEditorState : State<NotebookEditor>
    {
        private TextEditingController mNameController;

        bool mBtnSaveVisible
        {
            get
            {
                if (widget.Mode == NotebookEditorMode.CREATION)
                {
                    return !string.IsNullOrWhiteSpace(mNameController.text);
                }
                else
                {
                    return !string.IsNullOrWhiteSpace(mNameController.text) &&
                           mNameController.text != widget.Notebook.Name;
                }
            }
        }

        public override void initState()
        {
            base.initState();
            mNameController = new TextEditingController(widget.Notebook.Name);
        }

        public override Widget build(BuildContext context)
        {
            return new AlertDialog(
                shape: new RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(10)
                ),
                title: new Text(widget.Mode == NotebookEditorMode.CREATION
                        ? L.of(context).CreateNotebook
                        : L.of(context).EditNotebook,
                    style: Theme.of(context).textTheme.title),
                content: new TextField(
                    onChanged: _ => { setState(() => { }); },
                    controller: mNameController,
                    style: Theme.of(context).textTheme.body2,
                    decoration: new InputDecoration(
                        hintText: L.of(context).NameOfNotebook
                    )
                ),
                actions: new List<Widget>()
                {
                    new FlatButton(
                        child: new Text(L.of(context).Delete,
                            style: Theme.of(context)
                                .textTheme
                                .title
                                .copyWith(color: Colors.red)
                        ),
                        onPressed: () =>
                        {
                            widget.Dispatcher.dispatch(new DeleteNotebookAction(widget.Notebook));
                            Navigator.of(context).pop();
                        }
                    ),
                    !mBtnSaveVisible
                        ? new Container() as Widget
                        : new FlatButton(
                            child: new Text(
                                widget.Mode == NotebookEditorMode.CREATION
                                    ? L.of(context).Create
                                    : L.of(context).Save,
                                style: Theme.of(context)
                                    .textTheme
                                    .title
                                    .copyWith(color: Colors.purple)
                            ),
                            onPressed: () =>
                            {
                                widget.Notebook.Name = mNameController.text;


                                if (widget.Mode == NotebookEditorMode.CREATION)
                                {
                                    widget.Dispatcher.dispatch(new AddNotebookAction(widget.Notebook));
                                }
                                else
                                {
                                    widget.Dispatcher.dispatch(new UpdateNotebookAction(widget.Notebook));
                                }


                                Navigator.pop(context);
                            }
                        ),
                    new FlatButton(
                        child: new Text(
                            L.of(context).Cancel,
                            style: Theme.of(context)
                                .textTheme
                                .title
                                .copyWith(color: Colors.purple)
                        ),
                        onPressed: () => { Navigator.pop(context); }
                    )
                }
            );
        }
    }
}
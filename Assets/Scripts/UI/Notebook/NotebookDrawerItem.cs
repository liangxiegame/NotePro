using System.Collections.Generic;
using System.Linq;
using Unity.UIWidgets;
using Unity.UIWidgets.material;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using DialogUtils = Unity.UIWidgets.material.DialogUtils;

namespace NotePro
{
    public class NotebookDrawerItem : StatelessWidget
    {
        List<Widget> BuildNotebooks(List<Notebook> notebooks, Dispatcher dispatcher, BuildContext context)
        {
            var retList = new List<Widget>()
            {
                new ListTile(
                    leading: new Icon(Icons.add, color: Colors.black),
                    title: new Text(L.of(context).CreateNotebook,
                        style: Theme.of(context).textTheme.subhead
                            .copyWith(fontWeight: FontWeight.bold)),
                    onTap: () =>
                    {
                        DialogUtils.showDialog(context,
                            builder: context1 => new NotebookEditor(dispatcher));
                    }
                )
            };

            retList.AddRange(notebooks.Select(notebook => new ListTile(
                    leading: new Icon(Icons.bookmark, color: Colors.black),
                    title: new Text(notebook.Name,
                        style: Theme.of(context).textTheme.subhead.copyWith(fontWeight: FontWeight.bold)),
                    trailing: new IconButton(
                        icon: new Icon(Icons.edit, color: Colors.black),
                        onPressed: () =>
                        {
                            DialogUtils.showDialog(context,
                                builder: buildContext =>
                                    new NotebookEditor(dispatcher, NotebookEditorMode.MODIFICATION, notebook));
                        }
                    ),
                    onTap: () =>
                    {
                        dispatcher.dispatch(
                            new ApplyFilterAction(Filter.ByNotebook(context, notebook.Id, notebook.Name)));
                        Navigator.of(context).pop();
                    }
                )
            ));

            return retList;
        }

        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState, AppState>(
                converter: state => state,
                builder: (buildContext, model, dispatcher) =>
                {
                    return new ExpansionTile(
                        leading: new Icon(Icons.book, color: Colors.black),
                        title: new Text(L.of(context).Notebook, style: Theme.of(context).textTheme.title),
                        children: BuildNotebooks(model.Notebooks, dispatcher, context)
                    );
                }
            );
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.UIWidgets;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;

namespace NotePro
{
    public class NotebookPicker : StatelessWidget
    {
        private string mSelectedId;

        private Action<string> mOnSelect;


        List<Widget> BuildNotebooks(List<Notebook> notebooks, BuildContext context)
        {
            var retList = new List<Widget>()
            {
                new ListTile(
                    leading: new Icon(Icons.book),
                    title: new Text(L.of(context).NoneSelect, style: Theme.of(context).textTheme.title),
                    trailing: string.IsNullOrWhiteSpace(mSelectedId)
                        ? new Icon(Icons.done)
                        : null,
                    onTap: () =>
                    {
                        mSelectedId = null;
                        mOnSelect(mSelectedId);
                        Navigator.of(context).pop();
                    }
                )
            };

            retList.AddRange(notebooks.Select(notebook => new ListTile(
                leading: new Icon(Icons.book),
                title: new Text(notebook.Name, style: Theme.of(context).textTheme.title),
                trailing: mSelectedId == notebook.Id
                    ? new Icon(Icons.done)
                    : null,
                onTap: () =>
                {
                    mSelectedId = notebook.Id;
                    mOnSelect(mSelectedId);
                    Navigator.of(context).pop();
                }
            )));


            return retList;
        }


        public NotebookPicker(string selectedId, Action<string> onSelect)
        {
            mSelectedId = selectedId;
            mOnSelect = onSelect;
        }

        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState, List<Notebook>>(
                converter: state => state.Notebooks,
                builder: (buildContext, model, dispatcher) =>
                {
                    return new AlertDialog(
                        shape: new RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(10)
                        ),
                        title: new Text(L.of(context).SelectNotebook, style: Theme.of(context).textTheme.headline),
                        content: new ListView(
                            children: BuildNotebooks(model, context)
                        )
                    );
                }
            );
        }
    }
}
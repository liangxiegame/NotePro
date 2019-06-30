using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;

namespace NotePro
{
    public class SideDrawer : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState, object>(
                converter: state => null,
                builder: (buildContext, model, dispatcher) =>
                {
                    return new Drawer(
                        child: new ListView(
                            children: new List<Widget>()
                            {
                                new ListTile(
                                    leading: new Icon(Icons.inbox, color: Colors.black),
                                    title: new Text(L.of(context).Inbox, style: Theme.of(context).textTheme.title),
                                    onTap: () =>
                                    {
                                        dispatcher.dispatch(new ApplyFilterAction(Filter.ByInbox()));
                                        Navigator.of(context).pop();
                                    }
                                ),
                                new ListTile(
                                    leading: new Icon(Icons.book, color: Colors.black),
                                    title: new Text(L.of(context).Notebook, style: Theme.of(context).textTheme.title)
                                ),
                                new ListTile(
                                    leading: new Icon(Icons.priority_high, color: Colors.black),
                                    title: new Text(L.of(context).Priority, style: Theme.of(context).textTheme.title)
                                ),
                                new ListTile(
                                    leading: new Icon(Icons.color_lens, color: Colors.black),
                                    title: new Text(L.of(context).Color, style: Theme.of(context).textTheme.title)
                                ),
                                new ListTile(
                                    leading: new Icon(Icons.all_out, color: Colors.black),
                                    title: new Text(L.of(context).All, style: Theme.of(context).textTheme.title),
                                    onTap: () =>
                                    {
                                        dispatcher.dispatch(new ApplyFilterAction(Filter.ByAll()));
                                        Navigator.of(context).pop();
                                    }
                                )
                            }
                        )
                    );
                }
            );
        }
    }
}
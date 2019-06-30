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
                                    leading: new Icon(Icons.inbox),
                                    title: new Text(L.of(context).Inbox, style: Theme.of(context).textTheme.title),
                                    onTap: () =>
                                    {
                                        dispatcher.dispatch(new ApplyFilterAction(Filter.ByInbox(context)));
                                        Navigator.pop(context);
                                    }
                                ),
                                new PriorityDrawerItem(),
                                new ColorDrawerItem()
                            }
                        )
                    );
                }
            );
        }
    }
}
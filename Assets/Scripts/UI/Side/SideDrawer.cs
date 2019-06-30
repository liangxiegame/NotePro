using System;
using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;
using UnityEngine.AI;

namespace NotePro
{
    public class SideDrawer : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState, Filter>(
                converter: state => state.Filter,
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
                                        dispatcher.dispatch(new ApplyFilterAction(Filter.ByInbox(context)));
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
                                new ExpansionTile(
                                    leading: new Icon(Icons.color_lens, color: Colors.black),
                                    title: new Text(L.of(context).Color, style: Theme.of(context).textTheme.title),
                                    children: new List<Widget>()
                                    {
                                        new ColorPicker(model.Type != FilterType.ByColor ? -1 : model.ColorIndex,
                                            colorIndex =>
                                            {
                                                dispatcher.dispatch(
                                                    new ApplyFilterAction(Filter.ByColor(context, colorIndex)));
                                                Navigator.of(context).pop();
                                            }),
                                        new SizedBox(height: 20)
                                    }
                                ),
                                new ListTile(
                                    leading: new Icon(Icons.all_out, color: Colors.black),
                                    title: new Text(L.of(context).All, style: Theme.of(context).textTheme.title),
                                    onTap: () =>
                                    {
                                        dispatcher.dispatch(new ApplyFilterAction(Filter.ByAll(context)));
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
using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine.Assertions.Must;
using DialogUtils = Unity.UIWidgets.material.DialogUtils;

namespace NotePro
{
    public class SideDrawer : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState, AppState>(
                converter: state => state,
                builder: (buildContext, model, dispatcher) =>
                {
                    return new Drawer(
                        child: new ListView(
                            children: new List<Widget>
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
                                new NotebookDrawerItem(),
                                new ExpansionTile(
                                    leading: new Icon(Icons.priority_high, color: Colors.black),
                                    title: new Text(L.of(context).Priority, style: Theme.of(context).textTheme.title),
                                    children: new List<Widget>()
                                    {
                                        new PriorityPicker(
                                            model.Filter.Type == FilterType.ByPriority
                                                ? model.Filter.PriorityIndex
                                                : -1,
                                            priorityIndex =>
                                            {
                                                dispatcher.dispatch(
                                                    new ApplyFilterAction(Filter.ByPriority(context, priorityIndex)));

                                                Navigator.of(context).pop();
                                            }, 300),
                                        new SizedBox(height: 20)
                                    }
                                ),
                                new ExpansionTile(
                                    leading: new Icon(Icons.color_lens, color: Colors.black),
                                    title: new Text(L.of(context).Color, style: Theme.of(context).textTheme.title),
                                    children: new List<Widget>()
                                    {
                                        new ColorPicker(
                                            model.Filter.Type != FilterType.ByColor ? -1 : model.Filter.ColorIndex,
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
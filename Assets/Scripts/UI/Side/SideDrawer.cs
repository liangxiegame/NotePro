using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace NotePro
{
    public class SideDrawer : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new Drawer(
                child: new ListView(
                    children: new List<Widget>()
                    {
                        new ListTile(
                            leading: new Icon(Icons.all_out, color: Colors.black),
                            title: new Text("全部笔记", style: Theme.of(context).textTheme.title)
                        ),
                        new ListTile(
                            leading: new Icon(Icons.inbox, color: Colors.black),
                            title: new Text("收件箱", style: Theme.of(context).textTheme.title)
                        ),
                        new ListTile(
                            leading: new Icon(Icons.book, color: Colors.black),
                            title: new Text("笔记本", style: Theme.of(context).textTheme.title)
                        ),
                        new ListTile(
                            leading: new Icon(Icons.priority_high, color: Colors.black),
                            title: new Text("优先级", style: Theme.of(context).textTheme.title)
                        ),
                        new ListTile(
                            leading: new Icon(Icons.color_lens, color: Colors.black),
                            title: new Text("颜色", style: Theme.of(context).textTheme.title)
                        )
                    }
                )
            );
        }
    }
}
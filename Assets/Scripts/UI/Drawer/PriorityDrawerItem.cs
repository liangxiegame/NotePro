using System;
using System.Collections.Generic;
using System.Linq;
using Unity.UIWidgets;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace NotePro
{
    public class PriorityDrawerItem : StatelessWidget
    {
        List<Widget> BuildColors(Dispatcher dispatcher, BuildContext context)
        {
            return Enumerable.Range(0, 3)
                .Select(t => new PriorityRow(t,
                    () =>
                    {
                        dispatcher.dispatch(new ApplyFilterAction(Filter.ByPriority(context, t)));
                        Navigator.of(context).pop();
                    }))
                .Cast<Widget>()
                .ToList();
        }

        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState, object>(
                converter: state => null,
                builder: (buildContext, model, dispatcher) => new ExpansionTile(
                    leading: new Text(L.of(context).Priority, style: Theme.of(context).textTheme.title),
                    children: BuildColors(dispatcher, context)
                )
            );
        }
    }

    class PriorityRow : StatelessWidget
    {
        Color mPriorityColor
        {
            get
            {
                switch (mPriorityIndex)
                {
                    case 0:
                        return Colors.green;
                    case 1:
                        return Colors.yellow;
                    case 2:
                        return Colors.red;
                }

                return Colors.yellow;
            }
        }

        string mPriorityText
        {
            get
            {
                switch (mPriorityIndex)
                {
                    case 0:
                        return "!";
                    case 1:
                        return "!!";
                    case 2:
                        return "!!!";
                }

                return "!!";
            }
        }

        private Action mOnClick;

        public PriorityRow(int priorityIndex, Action onClick)
        {
            mPriorityIndex = priorityIndex;
            mOnClick = onClick;
        }


        private int mPriorityIndex { get; }

        public override Widget build(BuildContext context)
        {
            return new ListTile(
                title: new Text(mPriorityText, style: new TextStyle(
                    color: mPriorityColor
                ))
                ,
                onTap: () => { mOnClick.Invoke(); }
            );
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.UIWidgets;
using Unity.UIWidgets.material;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace NotePro
{
    public class ColorDrawerItem : StatelessWidget
    {
        List<Widget> BuildColors(Dispatcher dispatcher, BuildContext context)
        {
            return AppConst.Colors
                .Select((t, i) => new ColorRow(i, t,
                    () =>
                    {
                        dispatcher.dispatch(new ApplyFilterAction(Filter.ByColor(context, i)));
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
                    leading: new Text(L.of(context).Color, style: Theme.of(context).textTheme.title),
                    children: BuildColors(dispatcher, context)
                )
            );
        }
    }

    class ColorRow : StatelessWidget
    {
        private Action mOnClick;

        public ColorRow(int colorIndex, Color color, Action onClick)
        {
            mColorIndex = colorIndex;
            mColor = color;
            mOnClick = onClick;
        }

        private Color mColor { get; }

        private int mColorIndex { get; }

        public override Widget build(BuildContext context)
        {
            return new InkWell(
                child: new Container(
                    height: 25,
                    color: mColor
                ),
                onTap: () =>
                {
                    mOnClick();
                    Navigator.of(context).pop();
                }
            );
        }
    }
}
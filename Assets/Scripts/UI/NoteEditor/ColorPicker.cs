using System;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.widgets;

namespace NotePro
{
    public class ColorPicker : StatefulWidget
    {
        public int         Index { get; }
        public Action<int> OnTap { get; }

        public ColorPicker(int index, Action<int> onTap)
        {
            this.Index = index;
            this.OnTap = onTap;
        }

        public override State createState()
        {
            return new ColorPickerState();
        }
    }

    public class ColorPickerState : State<ColorPicker>
    {
        private int mSelectedIndex = 0;

        public override void initState()
        {
            base.initState();
            mSelectedIndex = widget.Index;
        }

        public override Widget build(BuildContext context)
        {
            return new SizedBox(
                height: 50,
                child: ListView.builder(
                    scrollDirection: Axis.horizontal,
                    itemCount: AppConst.Colors.Length,
                    itemBuilder: (buildContext, index) => new Container(
                        padding: EdgeInsets.all(8),
                        width: 50,
                        height: 50,
                        child: new InkWell(
                            onTap: () =>
                            {
                                setState(() => { mSelectedIndex = index; });

                                widget.OnTap(index);
                            },
                            child: new ColorCircle(
                                index: index,
                                selected: index == mSelectedIndex
                            )
                        )
                    )
                )
            );
        }
    }

    public class ColorCircle : StatelessWidget
    {
        private int mIndex;

        private bool mSelected = false;

        private float? mSize = null;

        public ColorCircle(int index, bool selected = false, float? size = null)
        {
            mIndex = index;
            mSelected = selected;
            mSize = size;
        }

        public override Widget build(BuildContext context)
        {
            return new Container(
                width: mSize,
                height: mSize,
                child: mSelected
                    ? new Center(
                        child: new Icon(Icons.done)
                    ) as Widget
                    : new Container(),
                decoration: new BoxDecoration(
                    color: AppConst.Colors[mIndex],
                    shape: BoxShape.circle,
                    border: Border.all(width: 2, color: Colors.black)
                )
            );
        }
    }
}
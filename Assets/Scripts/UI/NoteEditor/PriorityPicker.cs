using System;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace NotePro
{
    public class PriorityPicker : StatefulWidget
    {
        public int Index;

        public Action<int> OnTap;

        public PriorityPicker(int index, Action<int> onTap)
        {
            Index = index;
            OnTap = onTap;
        }


        public override State createState()
        {
            return new PriorityState();
        }
    }

    public class PriorityState : State<PriorityPicker>
    {
        private int mSelectedIndex = 0;

        private Color[] mPriorityColors =
        {
            Colors.green,
            Colors.lightGreen,
            Colors.red
        };

        private string[] mPriorityTexts =
        {
            "Low",
            "High",
            "Very High"
        };

        public override void initState()
        {
            base.initState();
            mSelectedIndex = widget.Index;
        }

        public override Widget build(BuildContext context)
        {
            var width = MediaQuery.of(context).size.width;

            return new SizedBox(
                height: 60,
                child: ListView.builder(
                    scrollDirection: Axis.horizontal,
                    itemCount: 3,
                    itemBuilder: (buildContext, index) =>
                        new InkWell(
                            onTap: () =>
                            {
                                setState(() => { mSelectedIndex = index; });

                                widget.OnTap(index);
                            },
                            child: new Container(
                                padding: EdgeInsets.all(8),
                                width: width / 3,
                                child: new Container(
                                    child: new Center(
                                        child: new Text(
                                            mPriorityTexts[index],
                                            style: new TextStyle(
                                                color: mSelectedIndex == index
                                                    ? Colors.white
                                                    : Colors.black,
                                                fontWeight: FontWeight.bold
                                            )
                                        )
                                    ),
                                    decoration: new BoxDecoration(
                                        border: mSelectedIndex == index
                                            ? Border.all(width: 2, color: Colors.black)
                                            : Border.all(width: 0, color: Colors.transparent),
                                        borderRadius: BorderRadius.circular(8),
                                        color: mSelectedIndex == index
                                            ? mPriorityColors[index]
                                            : Colors.transparent
                                    )
                                )
                            )
                        )
                )
            );
        }
    }
}
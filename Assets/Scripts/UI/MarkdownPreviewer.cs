using markdown;
using Unity.UIWidgets.material;
using Unity.UIWidgets.widgets;
using UnityEngine;
using Text = Unity.UIWidgets.widgets.Text;

namespace NotePro
{
    public class MarkdownPreviewer : StatelessWidget
    {
        private int mColorIndex;

        private string mTitle;

        private string mDescription;

        public MarkdownPreviewer(int colorIndex, string title, string description)
        {
            mColorIndex = colorIndex;
            mTitle = title;
            mDescription = description;
        }


        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                appBar: new AppBar(
                    title: new Text(mTitle, style: Theme.of(context).textTheme.headline),
                    backgroundColor: AppConst.Colors[mColorIndex],
                    leading: new IconButton(
                        icon: new Icon(Icons.arrow_back_ios, color: Colors.black),
                        onPressed: () => { Navigator.of(context).pop(); }
                    ),
                    elevation: 0
                ),
                backgroundColor: AppConst.Colors[mColorIndex],
                body: new Markdown(null, mDescription,
                    styleSheet: MarkdownStyleSheet.fromTheme(Theme.of(context)),
                    syntaxHighlighter: new DartSyntaxHighlighter(SyntaxHighlighterStyle.lightThemeStyle()),
                    onTapLink:
                    url => { Application.OpenURL(url); })
            );
        }
    }
}
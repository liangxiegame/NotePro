using Unity.UIWidgets.widgets;

namespace NotePro
{
    public enum FilterType
    {
        ByInbox,
        ByColor,
        ByPriority,
        ByNoteBook
    }

    /// <summary>
    /// 过滤对象
    /// </summary>
    public class Filter
    {
        public static Filter ByInbox(BuildContext context)
        {
            return new Filter()
            {
                Title = L.of(context).Inbox,
                FilterType = FilterType.ByInbox
            };
        }

        public static Filter ByColor(BuildContext context, int colorIndex)
        {
            return new Filter()
            {
                Title = L.of(context).Color,
                ColorIndex = colorIndex,
                FilterType = FilterType.ByColor
            };
        }

        public static Filter ByPriority(BuildContext context, int priority)
        {
            return new Filter
            {
                Title = L.of(context).Priority,
                PriorityIndex = priority,
                FilterType = FilterType.ByPriority
            };
        }

        public string Title;

        public FilterType FilterType;

        public int ColorIndex;

        public int PriorityIndex;
    }
}
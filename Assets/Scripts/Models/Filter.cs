using Unity.UIWidgets.widgets;

namespace NotePro
{
    public enum FilterType
    {
        ByInbox,
        ByAll,
        ByPriority,
        ByColor,
        ByNotebook
    }


    public class Filter
    {
        public static Filter ByInbox(BuildContext context)
        {
            return new Filter()
            {
                Title = L.of(context).Inbox,
                Type = FilterType.ByInbox
            };
        }

        public static Filter ByAll(BuildContext context)
        {
            return new Filter()
            {
                Title = L.of(context).All,
                Type = FilterType.ByAll
            };
        }

        public static Filter ByColor(BuildContext context, int colorIndex)
        {
            return new Filter()
            {
                Title = L.of(context).Color,
                Type = FilterType.ByColor,
                ColorIndex = colorIndex
            };
        }

        public static Filter ByPriority(BuildContext context, int priorityIndex)
        {
            return new Filter()
            {
                Title = L.of(context).Priority,
                Type = FilterType.ByPriority,
                PriorityIndex = priorityIndex
            };
        }

        public string Title;

        public FilterType Type;

        public int ColorIndex;

        public int PriorityIndex;
    }
}
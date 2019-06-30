using System.Security.Cryptography.X509Certificates;
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
        public static Filter ByInbox()
        {
            return new Filter()
            {
                Title = "收件箱",
                Type = FilterType.ByInbox
            };
        }

        public static Filter ByAll()
        {
            return new Filter()
            {
                Title = "全部笔记",
                Type = FilterType.ByAll
            };
        }

        public string Title;

        public FilterType Type;
    }
}
using System.Collections.Generic;
using QFramework.UIWidgets.ReduxPersist;

namespace NotePro
{
    public class AppState : AbstractPersistState<AppState>
    {
        public List<Note> Notes = new List<Note>();

        public Filter Filter = new Filter()
        {
            Title = L.IsChinese ? "收件箱" : "Inbox",
            Type = FilterType.ByInbox
        };
    }
}
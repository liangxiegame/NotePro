using System.Collections.Generic;
using QFramework.UIWidgets.ReduxPersist;

namespace NotePro
{
    public class AppState : AbstractPersistState<AppState>
    {
        public List<Note> Notes = new List<Note>();

        public Filter Filter = null;
    }
}
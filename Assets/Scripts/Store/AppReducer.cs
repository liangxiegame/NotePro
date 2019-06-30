using System;
using Unity.UIWidgets.material;

namespace NotePro
{
    public class AppReducer
    {
        public static AppState Reduce(AppState previoustate, object action)
        {
            switch (action)
            {
                case AddNoteAction addNoteAction:
                    addNoteAction.Note.Date = DateTime.Now.ToString("MMM-dd yyyy");
                    previoustate.Notes.Add(addNoteAction.Note);
                    return previoustate;
                case UpdateNoteAction updateNoteAction:
                    updateNoteAction.Note.Date = DateTime.Now.ToString("MMM-dd yyyy");
                    return previoustate;
                case DeleteNoteAction deleteNoteAction:
                    previoustate.Notes.Remove(deleteNoteAction.Note);
                    return previoustate;
                case ApplyFilterAction applyFilterAction:
                    previoustate.Filter = applyFilterAction.Filter;
                    return previoustate;
            }

            return previoustate;
        }
    }
}
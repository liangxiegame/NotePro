using System;
using Unity.UIWidgets.material;

namespace NotePro
{
    public class AppReducer
    {
        public static AppState Reduce(AppState previousState, object action)
        {
            switch (action)
            {
                case AddNoteAction addNoteAction:
                    addNoteAction.Note.Date = DateTime.Now.ToString("MMM-dd yyyy");
                    previousState.Notes.Add(addNoteAction.Note);
                    return previousState;
                case UpdateNoteAction updateNoteAction:
                    updateNoteAction.Note.Date = DateTime.Now.ToString("MMM-dd yyyy");
                    return previousState;
                case DeleteNoteAction deleteNoteAction:
                    previousState.Notes.Remove(deleteNoteAction.Note);
                    return previousState;
                case ApplyFilterAction applyFilterAction:
                    previousState.Filter = applyFilterAction.Filter;
                    return previousState;
            }

            return previousState;
        }
    }
}
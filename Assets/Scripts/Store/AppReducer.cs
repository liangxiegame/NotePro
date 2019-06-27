using System;
using Unity.UIWidgets.material;

namespace NotePro
{
    public class AppReducer
    {
        public static AppState Reduce(AppState previousstate, object action)
        {
            switch (action)
            {
                case AddNoteAction addNoteAction:
                    addNoteAction.Note.Date = DateTime.Now.ToString("yyyy/MMMM/dd/HH:mm:ss");
                    previousstate.Notes.Add(addNoteAction.Note);
                    return previousstate;
                case UpdateNoteAction updateNoteAction:
                    updateNoteAction.Note.Date = DateTime.Now.ToString("yyyy/MMMM/dd/HH:mm:ss");
                    return previousstate;
                case DeleteNoteAction deleteNoteAction:
                    previousstate.Notes.Remove(deleteNoteAction.Note);
                    return previousstate;
            }

            return previousstate;
        }
    }
}
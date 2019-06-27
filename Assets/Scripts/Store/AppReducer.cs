namespace NotePro
{
    public class AppReducer
    {
        public static AppState Reduce(AppState previousstate, object action)
        {
            switch (action)
            {
                case AddNoteAction addNoteAction:
                    previousstate.Notes.Add(addNoteAction.Note);
                    return previousstate;
                case UpdateNoteAction _:
                    return previousstate;
            }

            return previousstate;
        }
    }
}
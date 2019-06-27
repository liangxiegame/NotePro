namespace NotePro
{
    public class AddNoteAction
    {
        public AddNoteAction(Note note)
        {
            Note = note;
        }

        public Note Note { get; }
    }
}
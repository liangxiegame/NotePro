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


    public class UpdateNoteAction
    {
        public UpdateNoteAction(Note note)
        {
            Note = note;
        }
        
        public Note Note { get; }
    }

    public class DeleteNoteAction
    {
        public DeleteNoteAction(Note note)
        {
            Note = note;
        }
        
        public Note Note { get; }
    }

    public class ApplyFilterAction
    {
        public ApplyFilterAction(Filter filter)
        {
            Filter = filter;
        }

        public Filter Filter { get; }
    }
    
}
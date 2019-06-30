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
        public Filter Filter { get; }

        public ApplyFilterAction(Filter filter)
        {
            this.Filter = filter;
        }
    }
    
}
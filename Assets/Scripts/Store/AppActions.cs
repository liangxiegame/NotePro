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

    public class AddNotebookAction
    {
        public AddNotebookAction(Notebook notebook)
        {
            Notebook = notebook;
        }

        public Notebook Notebook { get; }
    }

    public class UpdateNotebookAction
    {
        public UpdateNotebookAction(Notebook notebook)
        {
            Notebook = notebook;
        }

        public Notebook Notebook { get; }
    }

    public class DeleteNotebookAction
    {
        public Notebook Notebook { get; }
        public DeleteNotebookAction(Notebook notebook)
        {
            Notebook = notebook;
        }
    }
}
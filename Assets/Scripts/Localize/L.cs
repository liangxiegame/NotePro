using Unity.UIWidgets.material;
using Unity.UIWidgets.widgets;

namespace NotePro
{
    public abstract class L : DefaultMaterialLocalizations
    {
        public static L of(BuildContext context)
        {
            return Localizations.of<L>(context, typeof(MaterialLocalizations));
        }


        public virtual string Notes => "Notes";
        public virtual string EditNote => "Edit Note";
        public virtual string AddNote => "Add Note";
        public virtual string Low => "Low";
        public virtual string High => "High";
        public virtual string VeryHigh => "Very High";

        public virtual string DiscardChanges => "Discard Changes ?";
        public virtual string DiscardChangesContent => "Are you sure you want to discard changes ?";
        public virtual string Yes => "Yes";
        public virtual string No => "No";

        public virtual string DeleteNote => "Delete Note ?";
        public virtual string DeleteNoteContent => "Are you sure you want to delete this note ?";
        
        public virtual string Title       => "Title";
        public virtual string Description => "Description";


        #region 过滤

        public virtual string Inbox => "Inbox";
        public virtual string All => "All";

        public virtual string Priority => "Priority";

        public virtual string Color => "Color";

        public virtual string Notebook => "Notebook";

        #endregion

    }
}
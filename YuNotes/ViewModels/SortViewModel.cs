namespace YuNotes.ViewModels
{
    public class SortViewModel
    {
        public SortState TitleSort { get; set; }
        public SortState EditTimeSort { get; set; }
        public SortState CreateTimeSort { get; set; }

        public SortViewModel(SortState sortOrder)
        {

            TitleSort = SortState.TitleAsc;
            EditTimeSort = SortState.EditDesc;
            CreateTimeSort = SortState.CreateDesc;


            switch (sortOrder)
            {
                case SortState.TitleDesc:
                    TitleSort = SortState.TitleAsc;
                    break;
                case SortState.TitleAsc:
                    TitleSort = SortState.TitleDesc;
                    break;
                case SortState.EditDesc:
                    EditTimeSort = SortState.EditAsc;
                    break;
                case SortState.CreateAsc:
                    CreateTimeSort = SortState.CreateDesc;
                    break;
                case SortState.CreateDesc:
                    CreateTimeSort = SortState.CreateAsc;
                    break;
                default:
                    EditTimeSort = SortState.EditDesc;
                    break;
            }
        }
    }
}

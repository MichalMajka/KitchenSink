using Starcounter;

namespace KitchenSink
{
    [Database]
    public class Book
    {
        public string Author;
        public string Title;
        public long Position;
    }

    partial class PaginationPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            this.TotalEntries = Db.SQL<long>("SELECT COUNT(e) FROM KitchenSink.Book e").First;
            InitPage();
        }

        // Changes the number of entries per page depending on input and sets the page to 1
        void Handle(Input.EntriesPerPage action)
        {
            this.CurrentOffset = 0;
            this.EntriesPerPage = action.Value;
            SetTotalPages();
            GetNewPage();
        }

        // Sets the current page when the user clicks on one of the numbered buttons
        void Handle(Input.ChangePage action)
        {
            this.CurrentOffset = this.EntriesPerPage * (action.Value - 1);
            GetNewPage();
        }

        // Goes to the next page on click
        void Handle(Input.NextPageTrigger action)
        {
            if (this.CurrentOffset + this.EntriesPerPage < this.TotalEntries)
            {
                this.CurrentOffset = this.CurrentOffset + this.EntriesPerPage;
            }
            GetNewPage();
        }

        // Goes to the previous page on click
        void Handle(Input.PreviousPageTrigger action)
        {
            this.CurrentOffset = this.CurrentOffset - this.EntriesPerPage >= 0 ? this.CurrentOffset - this.EntriesPerPage : 0;
            GetNewPage();
        }

        // Goes to the last page on click
        void Handle(Input.LastPageTrigger action)
        {
            var remainder = this.TotalEntries % this.EntriesPerPage;
            this.CurrentOffset = remainder != 0 ? this.TotalEntries - (remainder) : this.TotalEntries - this.EntriesPerPage;
            GetNewPage();
        }

        // Goes to the first page on click
        void Handle(Input.FirstPageTrigger action)
        {
            this.CurrentOffset = 0;
            GetNewPage();
        }

        // Initializes the page by calling all the neccesary methods to set the page to 1
        private void InitPage()
        {
            GetNewPage();
            SetTotalPages();
            CreateNavButtons(this.EntriesPerPage);
            SetPageEntries();
        }

        // Creates the choices for page entries.
        // Modify the array entryChoices to modify how many entries per page the user can choose between.
        private void SetPageEntries()
        {
            int[] entryChoices = new int[] { 5, 15, 30 };
            foreach (int entry in entryChoices)
            {
                var page = this.PageEntries.Add();
                page.Amount = entry;
            }
        }

        // Sets the JSON value for total pages    
        private void SetTotalPages()
        {
            var totalPagesRounded = this.TotalEntries / this.EntriesPerPage;
            this.TotalPages = this.TotalEntries % this.EntriesPerPage == 0 ? totalPagesRounded : totalPagesRounded + 1;
        }

        // Establishes the navigation buttons.
        // Displays all the page buttons if there are less then 10,
        // Otherwise it shows the current page, the two before and after.
        private void CreateNavButtons(long entriesPerPage)
        {
            this.Pages.Clear();
            this.CurrentPage = this.CurrentOffset / entriesPerPage + 1;

            if (this.TotalPages < 10)
            {
                for (long i = 1; i < this.TotalPages + 1; i++)
                {
                    CreateButton(i);
                }
            }

            else if (this.TotalPages >= 10)
            {
                long pagesBefore = -2;
                long pagesAfter = 3;

                if (this.CurrentPage + pagesAfter > this.TotalPages)
                {
                    pagesBefore -= (this.CurrentPage + pagesAfter - 1) % this.TotalPages;
                }

                if (this.CurrentPage + pagesBefore <= 0)
                {
                    pagesAfter -= (this.CurrentPage + pagesBefore - 1);
                }

                for (long i = this.CurrentPage + pagesBefore; i < this.CurrentPage + pagesAfter; i++)
                {
                    if (i > 0 && i < this.TotalPages + 1)
                    {
                        CreateButton(i);
                    }
                }
            }
            this.DisableLast = this.CurrentPage == this.TotalPages;
            this.DisableFirst = this.CurrentPage == 1;
        }

        // Creates a nav button by setting the JSON to the current page number and if it's active.
        private void CreateButton(long pageNumber)
        {
            var page = this.Pages.Add();
            page.PageNumber = pageNumber;
            page.Active = (this.CurrentPage == pageNumber);
        }

        // Switch to the desired page depeding on the current offset.
        private void GetNewPage()
        {
            this.Library.Data = Db.SQL<Book>("SELECT b FROM KitchenSink.Book b ORDER BY b.Position FETCH ? OFFSET ?", this.EntriesPerPage, this.CurrentOffset);
            CreateNavButtons(this.EntriesPerPage);
        }
    }
}
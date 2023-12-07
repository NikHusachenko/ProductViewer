namespace ProductViewer.Client.Models
{
    public class PaginationManager
    {
        private int _currentPage;
        private int _pageSize;

        public int PreviousPageNumber
        {
            get
            {
                if (_currentPage == 1)
                {
                    return -1;
                }
                else
                {
                    return _currentPage - 1;
                }
            }
        }

        public int NextPage
        {
            get
            {
                if (_currentPage == _pageSize)
                {
                    return -1;
                }
                else
                {
                    return _currentPage + 1;
                }
            }
        }

        public PaginationManager() : this(1, 1) { }

        public PaginationManager(int currentPage, int pageSize)
        {
            if (currentPage <= 0 || pageSize <= 0)
            {
                // throw new ArgumentException("Current Page or Page Size less than or equal to zero. Current Page must be greater or equal to one");
                
                _currentPage = 1;
                _pageSize = 1;

                return;
            }

            if (currentPage < _pageSize)
            {
                throw new ArgumentException("Current Page must be less than or equal to Page Size");
            }

            _currentPage = currentPage;
            _pageSize = pageSize;
        }
    }
}
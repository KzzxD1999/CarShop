using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.BL.ViewModel
{
    public class PageViewModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; set; }
        
        public PageViewModel(int count, int pageNumger, int pageSize)
        {
            PageNumber = pageNumger;
            TotalPages = (int)Math.Ceiling(count / (double) pageSize);
        }

        public bool PreviousPage
        {
            get{
                return (PageNumber > 1);
            }
        }
        public bool NextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}

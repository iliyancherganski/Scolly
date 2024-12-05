using Microsoft.AspNetCore.Mvc;

namespace Scolly.Controllers
{
    public class BaseController : Controller
    {
        public List<T> Pagination<T>(int? page, List<T> list, int elementsOnPage)
        {
            if (page == null || elementsOnPage < page * elementsOnPage - list.Count())
            {
                page = 1;
            }
            int pagecount = list.Count() / elementsOnPage;
            if (list.Count() % elementsOnPage > 0)
            {
                pagecount++;
            }

            var tempList = new List<T>();
            int itemsOnPage;
            if (page < pagecount)
            {
                itemsOnPage = elementsOnPage;
            }
            else
            {
                itemsOnPage = list.Count() - (elementsOnPage * ((int)page - 1));
            }

            for (int i = ((int)page - 1) * elementsOnPage; i < ((int)page - 1) * elementsOnPage + itemsOnPage; i++)
            {
                tempList.Add(list[i]);
            }

            ViewBag.Page = (int)page;
            ViewBag.PageCount = pagecount;

            return tempList;
        }
    }
}

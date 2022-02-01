using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    class ListViewController
    {
        ViewMode Mode { get; set; }
        public enum ViewMode
        {
            PageView,
            ScrollView,
            MixedView
        }

        public ListViewController(ViewMode mode = ViewMode.ScrollView)
        {
            Mode = mode;
        }

        public void ExecuteView<T>(IList<T> items)
        {
            switch (Mode)
            {
                case ViewMode.ScrollView:
                    DoScroll(items);
                    break;
                case ViewMode.PageView:
                    DoPages(items);
                    break;
                case ViewMode.MixedView:
                    MixView(items);
                    break;
            }
        }

        private void DoPages<T>(IList<T> items)
        {
            var pageSize = 3;
            var pageCounter = 0;

            while (true)
            {
                Console.WriteLine($"<{pageCounter + 1}>");

                var offset = pageSize * pageCounter;
                var offseted = items.Skip(offset);
                var page = offseted.Take(pageSize);

                Print(page);

            read:
                var keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (pageCounter < items.Count / pageSize)
                    {
                        Console.Clear();
                        pageCounter++;
                    }
                    else goto read;
                }
                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (pageCounter > 0)
                    {
                        Console.Clear();
                        pageCounter--;
                    }
                    else goto read;
                }
            }
        }

        private void DoScroll<T>(IList<T> items)
        {
            var scrollSkip = 0;
            while (true)
            {
                var scrollView = items.Skip(scrollSkip).Take(3);
                Print(scrollView);

            read:
                var keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (scrollSkip < items.Count)
                    {
                        Console.Clear();
                        scrollSkip += 1;
                    }
                    else goto read;
                }
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (scrollSkip > 0)
                    {
                        Console.Clear();
                        scrollSkip -= 1;
                    }
                    else goto read;
                }
            }
        }

        private void MixView<T>(IList<T> items)
        {
            dynamic picture = 0;

            var scrollSkip = 0;
            var pageSize = 3;
            var pageCounter = 0;
            var pageCount = items.Count / pageSize;

            bool scroll = false;
            bool pages = false;

            IList<T> scrollView = new List<T>();

            while (true)
            {
                if (scroll)
                {
                    picture = "scrolling";

                    scrollView = items.Skip(scrollSkip).Take(3).ToList();

                    Console.Clear();
                    Console.WriteLine($"< {picture} >");
                    Print(scrollView);
                }
                if (pages)
                {
                    picture = pageCounter;

                    var offset = pageSize * pageCounter;
                    var offseted = items.Skip(offset).ToList();
                    var page = offseted.Take(pageSize).ToList();

                    Console.Clear();
                    Console.WriteLine($"< {picture + 1} >");
                    Print(page);
                }

            read:
                var keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (scrollSkip > 0)
                    {
                        if (pages)
                        {
                            var offseted = pageCounter * pageSize;
                            scrollSkip = offseted;
                        }
                        scrollSkip -= 1;
                        scroll = true;
                        pages = false;
                    }
                    else goto read;
                }
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (scrollSkip < items.Count)
                    {
                        if (pages)
                        {
                            var offseted = pageCounter * pageSize;
                            scrollSkip = offseted;
                        }
                        scrollSkip += 1;
                        scroll = true;
                        pages = false;
                    }
                    else goto read;
                }
                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (scroll)
                    {
                        var skippedPages = (scrollSkip + 2) / pageSize;
                        pageCounter = skippedPages;
                    }
                    if (pageCounter > 0)
                    {
                        pageCounter--;
                        pages = true;
                        scroll = false;
                    }
                    else goto read;
                }
                if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (pageCounter < pageCount)
                    {
                        if (scroll)
                        {
                            var skippedPages = (scrollSkip + 2) / pageSize;
                            pageCounter = skippedPages;
                        }
                        else pageCounter++;

                        pages = true;
                        scroll = false;
                    }
                    else goto read;
                }
            }
        }

        private void Print<T>(IEnumerable<T> enumerable, string prefix = null)
        {
            if (prefix != null)
                Console.Write(prefix + " ");
            foreach (var item in enumerable)
                Console.WriteLine(item);
        }
    }
}

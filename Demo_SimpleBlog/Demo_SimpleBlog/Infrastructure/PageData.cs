﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Demo_SimpleBlog.Infrastructure
{
    public class PageData<T>: IEnumerable<T>
    {
        private IEnumerable<T> _currentItems;
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalPages { get; set; }

        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }


        public int NextPage
        {
            get
            {
                if (!HasNextPage)
                {
                    throw new InvalidOperationException();
                }

                return Page + 1;
            }
        }

        public int PreviousPage
        {
            get
            {
                if (!HasPreviousPage)
                {
                    throw new InvalidOperationException();
                }

                return Page - 1;
            }
        }

        public PageData(IEnumerable<T> currentItems, int totalCount, int page, int perPage)
        {
            _currentItems = currentItems;
            TotalCount = totalCount;
            Page = page;
            PerPage = perPage;

            TotalPages = (int)Math.Ceiling((float)TotalCount / PerPage);

            HasNextPage = Page < TotalPages;
            HasPreviousPage = Page > 1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _currentItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
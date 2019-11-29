using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsApp.Services
{
    public enum PostStatus : short
    {
        /// <summary>
        /// Post waiting for an approval
        /// </summary>
        Pending = 1,
        /// <summary>
        /// Post has been approved and it is published.
        /// </summary>
        Published = 2,
        /// <summary>
        /// Post has been rejected by an Editor
        /// </summary>
        Rejected = 3,
        /// <summary>
        /// Post has been created by a Writer, but it is not ready to be published
        /// </summary>
        Created = 4
    }
}

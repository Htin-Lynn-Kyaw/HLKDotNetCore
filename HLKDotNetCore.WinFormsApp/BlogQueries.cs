using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLKDotNetCore.WinFormsApp
{
    public class BlogQueries
    {
        public static string BlogCreate { get; } = @"INSERT INTO [dbo].[tbl_Blog]
               ([BlogTitle]
               ,[BlogAuthor]
               ,[BlogContent])
                VALUES
               (@BlogTitle,
               @BlogAuthor,
               @BlogContent)";

        public static string BlogSelectAll { get; } = @"select * from tbl_blog";

        public static string BlogDelete { get; } = @"Delete from [dbo].[tbl_Blog]
                                                WHERE BlogID = @BlogID";

        public static string BlogSelect { get; } = @"select * from tbl_blog where blogID = @BlogID";

        public static string BlogUpdate { get; } = @"UPDATE [dbo].[tbl_Blog]
                                                    SET [BlogTitle] = @BlogTitle,
                                                    [BlogAuthor] = @BlogAuthor,
                                                    [BlogContent] = @BlogContent
                                                    WHERE BlogID = @BlogID";
    }
}

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
    }
}

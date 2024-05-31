using Dapper;
using HLKDotNetCore.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HLKDotNetCore.WinFormsApp
{
    public partial class FrmBlogList : Form
    {
        private readonly DapperService _dapperService;

        public FrmBlogList()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }

        private void FrmBlogList_Load(object sender, EventArgs e)
        {
            SelectAllBlog();
        }

        private void blogGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var ID = Convert.ToInt32(blogGridView.Rows[e.RowIndex].Cells["colID"].Value);

            if (e.ColumnIndex is (int)Actions.Edit)
            {
                FrmBlog frm = new FrmBlog(ID);
                frm.ShowDialog();

                SelectAllBlog();
            }
            else if (e.ColumnIndex is (int)Actions.Delete) 
            {
                var dialogResult = MessageBox.Show("Are you sure?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult is not DialogResult.Yes) return;

                DeleteBlog(ID);
            }
        }
        
        private void DeleteBlog(int id)
        {
            int result = _dapperService.Execute(BlogQueries.BlogDelete, new { BlogID = 1 });
            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            MessageBox.Show(message);
            SelectAllBlog();
        }

        private void SelectAllBlog()
        {
            List<BlogModel> lst = _dapperService.Query<BlogModel>(BlogQueries.BlogSelectAll);
            blogGridView.DataSource = lst;
        }
    }
}

using Dapper;
using HLKDotNetCore.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HLKDotNetCore.WinFormsApp
{
    public partial class FrmBlog : Form
    {
        private readonly DapperService _dapperService;
        private readonly int _blogID;
        public FrmBlog()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }

        public FrmBlog(int blogID)
        {
            InitializeComponent();
            _blogID = blogID;
            _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

            var model = _dapperService.QueryFirstOrDefault<BlogModel>(BlogQueries.BlogSelect, new { BlogID = _blogID });

            if (model is null) return;

            txtTitle.Text = model.BlogTitle;
            txtAuthor.Text = model.BlogAuthor;
            txtContent.Text = model.BlogContent;

            btnSave.Visible = false;
            btnUpdate.Visible = true;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void ClearControls()
        {
            txtAuthor.Clear();
            txtContent.Clear();
            txtTitle.Clear();

            txtTitle.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BlogModel blog = new BlogModel()
                {
                    BlogAuthor = txtAuthor.Text.Trim(),
                    BlogContent = txtContent.Text.Trim(),
                    BlogTitle = txtTitle.Text.Trim(),
                };
                int result = _dapperService.Execute(BlogQueries.BlogCreate, blog);
                string message = result > 0 ? "Saving successful." : "Saving failed.";
                MessageBox.Show(message, "Blog", MessageBoxButtons.OK, result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                if (result > 0)
                    ClearControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                BlogModel item = new BlogModel()
                {
                    BlogID = _blogID,
                    BlogAuthor = txtAuthor.Text.Trim(),
                    BlogContent = txtContent.Text.Trim(),
                    BlogTitle = txtTitle.Text.Trim(),
                };

                int result = _dapperService.Execute(BlogQueries.BlogUpdate, item);

                string message = result > 0 ? "Update Successful" : "Update Failed";

                MessageBox.Show(message);

                this.Close();   

            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

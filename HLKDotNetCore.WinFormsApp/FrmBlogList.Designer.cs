namespace HLKDotNetCore.WinFormsApp
{
    partial class FrmBlogList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            blogGridView = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colTitle = new DataGridViewTextBoxColumn();
            colAuthor = new DataGridViewTextBoxColumn();
            colContent = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)blogGridView).BeginInit();
            SuspendLayout();
            // 
            // blogGridView
            // 
            blogGridView.AllowUserToAddRows = false;
            blogGridView.AllowUserToDeleteRows = false;
            blogGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            blogGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            blogGridView.Columns.AddRange(new DataGridViewColumn[] { colID, colTitle, colAuthor, colContent });
            blogGridView.Dock = DockStyle.Fill;
            blogGridView.Location = new Point(0, 0);
            blogGridView.Name = "blogGridView";
            blogGridView.ReadOnly = true;
            blogGridView.RowHeadersWidth = 51;
            blogGridView.RowTemplate.Height = 29;
            blogGridView.Size = new Size(800, 450);
            blogGridView.TabIndex = 0;
            // 
            // colID
            // 
            colID.DataPropertyName = "BlogID";
            colID.HeaderText = "ID";
            colID.MinimumWidth = 6;
            colID.Name = "colID";
            colID.ReadOnly = true;
            colID.Visible = false;
            // 
            // colTitle
            // 
            colTitle.DataPropertyName = "BlogTitle";
            colTitle.HeaderText = "Title";
            colTitle.MinimumWidth = 6;
            colTitle.Name = "colTitle";
            colTitle.ReadOnly = true;
            // 
            // colAuthor
            // 
            colAuthor.DataPropertyName = "BlogAuthor";
            colAuthor.HeaderText = "Author";
            colAuthor.MinimumWidth = 6;
            colAuthor.Name = "colAuthor";
            colAuthor.ReadOnly = true;
            // 
            // colContent
            // 
            colContent.DataPropertyName = "BlogContent";
            colContent.HeaderText = "Content";
            colContent.MinimumWidth = 6;
            colContent.Name = "colContent";
            colContent.ReadOnly = true;
            // 
            // FrmBlogList
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(blogGridView);
            Name = "FrmBlogList";
            Text = "FrmBlogList";
            Load += FrmBlogList_Load;
            ((System.ComponentModel.ISupportInitialize)blogGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView blogGridView;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colTitle;
        private DataGridViewTextBoxColumn colAuthor;
        private DataGridViewTextBoxColumn colContent;
    }
}
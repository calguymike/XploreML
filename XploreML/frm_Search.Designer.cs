namespace XploreML
{
    partial class frm_search
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_search));
            this.txtbx_Search = new System.Windows.Forms.TextBox();
            this.btn_Search = new System.Windows.Forms.Button();
            this.listView_searchResults = new System.Windows.Forms.ListView();
            this.headerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.headerType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.headerDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.headerValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chk_Group = new System.Windows.Forms.CheckBox();
            this.chk_Calibrations = new System.Windows.Forms.CheckBox();
            this.chk_Curves = new System.Windows.Forms.CheckBox();
            this.chk_Variables = new System.Windows.Forms.CheckBox();
            this.chk_Maps = new System.Windows.Forms.CheckBox();
            this.btn_D2H = new System.Windows.Forms.Button();
            this.txtbox_noPerLine = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtbx_Search
            // 
            this.txtbx_Search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbx_Search.Location = new System.Drawing.Point(2, 1);
            this.txtbx_Search.Name = "txtbx_Search";
            this.txtbx_Search.Size = new System.Drawing.Size(776, 20);
            this.txtbx_Search.TabIndex = 0;
            // 
            // btn_Search
            // 
            this.btn_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Search.Location = new System.Drawing.Point(784, 1);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(73, 20);
            this.btn_Search.TabIndex = 3;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.Btn_Search_Click);
            // 
            // listView_searchResults
            // 
            this.listView_searchResults.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listView_searchResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.headerName,
            this.headerType,
            this.groupDesc,
            this.headerDesc,
            this.headerValue});
            this.listView_searchResults.FullRowSelect = true;
            this.listView_searchResults.HideSelection = false;
            this.listView_searchResults.Location = new System.Drawing.Point(2, 52);
            this.listView_searchResults.Name = "listView_searchResults";
            this.listView_searchResults.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listView_searchResults.ShowItemToolTips = true;
            this.listView_searchResults.Size = new System.Drawing.Size(868, 477);
            this.listView_searchResults.TabIndex = 5;
            this.listView_searchResults.UseCompatibleStateImageBehavior = false;
            this.listView_searchResults.View = System.Windows.Forms.View.Details;
            this.listView_searchResults.DoubleClick += new System.EventHandler(this.ListView_searchResults_DoubleClick);
            // 
            // headerName
            // 
            this.headerName.Text = "Name";
            this.headerName.Width = 149;
            // 
            // headerType
            // 
            this.headerType.Text = "Type";
            this.headerType.Width = 108;
            // 
            // groupDesc
            // 
            this.groupDesc.Text = "Group";
            this.groupDesc.Width = 105;
            // 
            // headerDesc
            // 
            this.headerDesc.Text = "Description";
            this.headerDesc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.headerDesc.Width = 406;
            // 
            // headerValue
            // 
            this.headerValue.Text = "Value";
            this.headerValue.Width = 682;
            // 
            // chk_Group
            // 
            this.chk_Group.AutoSize = true;
            this.chk_Group.Location = new System.Drawing.Point(413, 29);
            this.chk_Group.Name = "chk_Group";
            this.chk_Group.Size = new System.Drawing.Size(60, 17);
            this.chk_Group.TabIndex = 6;
            this.chk_Group.Text = "Groups";
            this.chk_Group.UseVisualStyleBackColor = true;
            // 
            // chk_Calibrations
            // 
            this.chk_Calibrations.AutoSize = true;
            this.chk_Calibrations.Checked = true;
            this.chk_Calibrations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Calibrations.Location = new System.Drawing.Point(12, 29);
            this.chk_Calibrations.Name = "chk_Calibrations";
            this.chk_Calibrations.Size = new System.Drawing.Size(80, 17);
            this.chk_Calibrations.TabIndex = 7;
            this.chk_Calibrations.Text = "Calibrations";
            this.chk_Calibrations.UseVisualStyleBackColor = true;
            // 
            // chk_Curves
            // 
            this.chk_Curves.AutoSize = true;
            this.chk_Curves.Checked = true;
            this.chk_Curves.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Curves.Location = new System.Drawing.Point(114, 29);
            this.chk_Curves.Name = "chk_Curves";
            this.chk_Curves.Size = new System.Drawing.Size(59, 17);
            this.chk_Curves.TabIndex = 8;
            this.chk_Curves.Text = "Curves";
            this.chk_Curves.UseVisualStyleBackColor = true;
            // 
            // chk_Variables
            // 
            this.chk_Variables.AutoSize = true;
            this.chk_Variables.Checked = true;
            this.chk_Variables.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Variables.Location = new System.Drawing.Point(213, 29);
            this.chk_Variables.Name = "chk_Variables";
            this.chk_Variables.Size = new System.Drawing.Size(69, 17);
            this.chk_Variables.TabIndex = 9;
            this.chk_Variables.Text = "Variables";
            this.chk_Variables.UseVisualStyleBackColor = true;
            // 
            // chk_Maps
            // 
            this.chk_Maps.AutoSize = true;
            this.chk_Maps.Checked = true;
            this.chk_Maps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Maps.Location = new System.Drawing.Point(323, 29);
            this.chk_Maps.Name = "chk_Maps";
            this.chk_Maps.Size = new System.Drawing.Size(52, 17);
            this.chk_Maps.TabIndex = 10;
            this.chk_Maps.Text = "Maps";
            this.chk_Maps.UseVisualStyleBackColor = true;
            // 
            // btn_D2H
            // 
            this.btn_D2H.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_D2H.Location = new System.Drawing.Point(741, 26);
            this.btn_D2H.Name = "btn_D2H";
            this.btn_D2H.Size = new System.Drawing.Size(116, 20);
            this.btn_D2H.TabIndex = 11;
            this.btn_D2H.Text = "Convert CAL to HEX";
            this.btn_D2H.UseVisualStyleBackColor = true;
            this.btn_D2H.Click += new System.EventHandler(this.btn_D2H_Click);
            // 
            // txtbox_noPerLine
            // 
            this.txtbox_noPerLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbox_noPerLine.Location = new System.Drawing.Point(689, 26);
            this.txtbox_noPerLine.Name = "txtbox_noPerLine";
            this.txtbox_noPerLine.Size = new System.Drawing.Size(46, 20);
            this.txtbox_noPerLine.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(625, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "No per line";
            // 
            // frm_search
            // 
            this.AcceptButton = this.btn_Search;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 528);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtbox_noPerLine);
            this.Controls.Add(this.btn_D2H);
            this.Controls.Add(this.chk_Maps);
            this.Controls.Add(this.chk_Variables);
            this.Controls.Add(this.chk_Curves);
            this.Controls.Add(this.chk_Calibrations);
            this.Controls.Add(this.chk_Group);
            this.Controls.Add(this.listView_searchResults);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.txtbx_Search);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_search";
            this.Text = "XploreML";
            this.Load += new System.EventHandler(this.Frm_Search_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtbx_Search;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.ListView listView_searchResults;
        private System.Windows.Forms.ColumnHeader headerDesc;
        private System.Windows.Forms.ColumnHeader headerName;
        private System.Windows.Forms.ColumnHeader headerValue;
        private System.Windows.Forms.ColumnHeader headerType;
        private System.Windows.Forms.ColumnHeader groupDesc;
        private System.Windows.Forms.CheckBox chk_Group;
        private System.Windows.Forms.CheckBox chk_Calibrations;
        private System.Windows.Forms.CheckBox chk_Curves;
        private System.Windows.Forms.CheckBox chk_Variables;
        private System.Windows.Forms.CheckBox chk_Maps;
        private System.Windows.Forms.Button btn_D2H;
        private System.Windows.Forms.TextBox txtbox_noPerLine;
        private System.Windows.Forms.Label label1;
    }
}
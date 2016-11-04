namespace TaskManager
{
    partial class TaskManagerForm
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
            this.components = new System.ComponentModel.Container();
            this.tasksTreeView = new System.Windows.Forms.TreeView();
            this.taskPropertiesGroupBox = new System.Windows.Forms.GroupBox();
            this.registrationDateTextBox = new System.Windows.Forms.TextBox();
            this.taskStatusComboBox = new System.Windows.Forms.ComboBox();
            this.registrationDateLabel = new System.Windows.Forms.Label();
            this.statusDescriptionLabel = new System.Windows.Forms.Label();
            this.taskStatusLabel = new System.Windows.Forms.Label();
            this.tagLabel = new System.Windows.Forms.Label();
            this.taskNameLabel = new System.Windows.Forms.Label();
            this.statusDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.tagTextBox = new System.Windows.Forms.TextBox();
            this.taskNameTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doNotShowMonthlyReportedTasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortTasksByTagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subtaskContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addSubtaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskPropertiesGroupBox.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.subtaskContextMenuStrip.SuspendLayout();
            this.taskContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tasksTreeView
            // 
            this.tasksTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tasksTreeView.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tasksTreeView.Location = new System.Drawing.Point(24, 54);
            this.tasksTreeView.Name = "tasksTreeView";
            this.tasksTreeView.Size = new System.Drawing.Size(498, 379);
            this.tasksTreeView.TabIndex = 5;
            this.tasksTreeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tasksTreeView_BeforeSelect);
            this.tasksTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tasksTreeView_AfterSelect);
            this.tasksTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tasksTreeView_NodeMouseClick);
            this.tasksTreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tasksTreeView_KeyDown);
            // 
            // taskPropertiesGroupBox
            // 
            this.taskPropertiesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.taskPropertiesGroupBox.Controls.Add(this.registrationDateTextBox);
            this.taskPropertiesGroupBox.Controls.Add(this.taskStatusComboBox);
            this.taskPropertiesGroupBox.Controls.Add(this.registrationDateLabel);
            this.taskPropertiesGroupBox.Controls.Add(this.statusDescriptionLabel);
            this.taskPropertiesGroupBox.Controls.Add(this.taskStatusLabel);
            this.taskPropertiesGroupBox.Controls.Add(this.tagLabel);
            this.taskPropertiesGroupBox.Controls.Add(this.taskNameLabel);
            this.taskPropertiesGroupBox.Controls.Add(this.statusDescriptionTextBox);
            this.taskPropertiesGroupBox.Controls.Add(this.tagTextBox);
            this.taskPropertiesGroupBox.Controls.Add(this.taskNameTextBox);
            this.taskPropertiesGroupBox.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.taskPropertiesGroupBox.ForeColor = System.Drawing.Color.MidnightBlue;
            this.taskPropertiesGroupBox.Location = new System.Drawing.Point(552, 54);
            this.taskPropertiesGroupBox.Name = "taskPropertiesGroupBox";
            this.taskPropertiesGroupBox.Size = new System.Drawing.Size(391, 266);
            this.taskPropertiesGroupBox.TabIndex = 8;
            this.taskPropertiesGroupBox.TabStop = false;
            this.taskPropertiesGroupBox.Text = "Task properties";
            // 
            // registrationDateTextBox
            // 
            this.registrationDateTextBox.Enabled = false;
            this.registrationDateTextBox.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.registrationDateTextBox.Location = new System.Drawing.Point(110, 120);
            this.registrationDateTextBox.Name = "registrationDateTextBox";
            this.registrationDateTextBox.Size = new System.Drawing.Size(273, 24);
            this.registrationDateTextBox.TabIndex = 3;
            // 
            // taskStatusComboBox
            // 
            this.taskStatusComboBox.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.taskStatusComboBox.FormattingEnabled = true;
            this.taskStatusComboBox.Items.AddRange(new object[] {
            "New",
            "InProgress",
            "Done",
            "Weekly",
            "Monthly"});
            this.taskStatusComboBox.Location = new System.Drawing.Point(110, 90);
            this.taskStatusComboBox.Name = "taskStatusComboBox";
            this.taskStatusComboBox.Size = new System.Drawing.Size(273, 25);
            this.taskStatusComboBox.TabIndex = 2;
            this.taskStatusComboBox.SelectedIndexChanged += new System.EventHandler(this.taskStatusComboBox_SelectedIndexChanged);
            // 
            // registrationDateLabel
            // 
            this.registrationDateLabel.AutoSize = true;
            this.registrationDateLabel.Font = new System.Drawing.Font("Comic Sans MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.registrationDateLabel.Location = new System.Drawing.Point(6, 124);
            this.registrationDateLabel.Name = "registrationDateLabel";
            this.registrationDateLabel.Size = new System.Drawing.Size(98, 15);
            this.registrationDateLabel.TabIndex = 9;
            this.registrationDateLabel.Text = "Registration date:";
            // 
            // statusDescriptionLabel
            // 
            this.statusDescriptionLabel.AutoSize = true;
            this.statusDescriptionLabel.Font = new System.Drawing.Font("Comic Sans MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.statusDescriptionLabel.Location = new System.Drawing.Point(0, 153);
            this.statusDescriptionLabel.Name = "statusDescriptionLabel";
            this.statusDescriptionLabel.Size = new System.Drawing.Size(104, 15);
            this.statusDescriptionLabel.TabIndex = 8;
            this.statusDescriptionLabel.Text = "Status description:";
            // 
            // taskStatusLabel
            // 
            this.taskStatusLabel.AutoSize = true;
            this.taskStatusLabel.Font = new System.Drawing.Font("Comic Sans MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.taskStatusLabel.Location = new System.Drawing.Point(37, 94);
            this.taskStatusLabel.Name = "taskStatusLabel";
            this.taskStatusLabel.Size = new System.Drawing.Size(67, 15);
            this.taskStatusLabel.TabIndex = 7;
            this.taskStatusLabel.Text = "Task status:";
            // 
            // tagLabel
            // 
            this.tagLabel.AutoSize = true;
            this.tagLabel.Font = new System.Drawing.Font("Comic Sans MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tagLabel.Location = new System.Drawing.Point(75, 33);
            this.tagLabel.Name = "tagLabel";
            this.tagLabel.Size = new System.Drawing.Size(29, 15);
            this.tagLabel.TabIndex = 6;
            this.tagLabel.Text = "Tag:";
            // 
            // taskNameLabel
            // 
            this.taskNameLabel.AutoSize = true;
            this.taskNameLabel.Font = new System.Drawing.Font("Comic Sans MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.taskNameLabel.Location = new System.Drawing.Point(41, 64);
            this.taskNameLabel.Name = "taskNameLabel";
            this.taskNameLabel.Size = new System.Drawing.Size(63, 15);
            this.taskNameLabel.TabIndex = 5;
            this.taskNameLabel.Text = "Task name:";
            // 
            // statusDescriptionTextBox
            // 
            this.statusDescriptionTextBox.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.statusDescriptionTextBox.Location = new System.Drawing.Point(110, 149);
            this.statusDescriptionTextBox.Multiline = true;
            this.statusDescriptionTextBox.Name = "statusDescriptionTextBox";
            this.statusDescriptionTextBox.Size = new System.Drawing.Size(273, 111);
            this.statusDescriptionTextBox.TabIndex = 4;
            this.statusDescriptionTextBox.TextChanged += new System.EventHandler(this.statusDescriptionTextBox_TextChanged);
            // 
            // tagTextBox
            // 
            this.tagTextBox.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tagTextBox.Location = new System.Drawing.Point(110, 29);
            this.tagTextBox.Name = "tagTextBox";
            this.tagTextBox.Size = new System.Drawing.Size(273, 24);
            this.tagTextBox.TabIndex = 0;
            this.tagTextBox.TextChanged += new System.EventHandler(this.tagTextBox_TextChanged);
            // 
            // taskNameTextBox
            // 
            this.taskNameTextBox.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.taskNameTextBox.Location = new System.Drawing.Point(110, 60);
            this.taskNameTextBox.Name = "taskNameTextBox";
            this.taskNameTextBox.Size = new System.Drawing.Size(273, 24);
            this.taskNameTextBox.TabIndex = 1;
            this.taskNameTextBox.TextChanged += new System.EventHandler(this.taskNameTextBox_TextChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(960, 27);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.saveToolStripMenuItem1,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem1});
            this.openToolStripMenuItem.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.openToolStripMenuItem.ForeColor = System.Drawing.Color.MidnightBlue;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(46, 23);
            this.openToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(164, 24);
            this.openToolStripMenuItem1.Text = "&Open";
            this.openToolStripMenuItem1.Click += new System.EventHandler(this.openToolStripMenuItem1_Click);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(164, 24);
            this.saveToolStripMenuItem1.Text = "&Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(164, 24);
            this.exitToolStripMenuItem1.Text = "&Quit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.doNotShowMonthlyReportedTasksToolStripMenuItem,
            this.addNewTaskToolStripMenuItem,
            this.sortTasksByTagToolStripMenuItem});
            this.optionsToolStripMenuItem.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.optionsToolStripMenuItem.ForeColor = System.Drawing.Color.MidnightBlue;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(71, 23);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // doNotShowMonthlyReportedTasksToolStripMenuItem
            // 
            this.doNotShowMonthlyReportedTasksToolStripMenuItem.Checked = global::TaskManager.Properties.Settings.Default.DoNotShowMonthlyReportedTasks;
            this.doNotShowMonthlyReportedTasksToolStripMenuItem.ForeColor = System.Drawing.Color.MidnightBlue;
            this.doNotShowMonthlyReportedTasksToolStripMenuItem.Name = "doNotShowMonthlyReportedTasksToolStripMenuItem";
            this.doNotShowMonthlyReportedTasksToolStripMenuItem.Size = new System.Drawing.Size(312, 24);
            this.doNotShowMonthlyReportedTasksToolStripMenuItem.Text = "Do not show monthly reported tasks";
            this.doNotShowMonthlyReportedTasksToolStripMenuItem.Click += new System.EventHandler(this.doNotShowMonthlyReportedTasksToolStripMenuItem_Click);
            // 
            // addNewTaskToolStripMenuItem
            // 
            this.addNewTaskToolStripMenuItem.ForeColor = System.Drawing.Color.MidnightBlue;
            this.addNewTaskToolStripMenuItem.Name = "addNewTaskToolStripMenuItem";
            this.addNewTaskToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.addNewTaskToolStripMenuItem.Size = new System.Drawing.Size(312, 24);
            this.addNewTaskToolStripMenuItem.Text = "Add new task";
            this.addNewTaskToolStripMenuItem.Click += new System.EventHandler(this.addNewTaskToolStripMenuItem_Click);
            // 
            // sortTasksByTagToolStripMenuItem
            // 
            this.sortTasksByTagToolStripMenuItem.ForeColor = System.Drawing.Color.MidnightBlue;
            this.sortTasksByTagToolStripMenuItem.Name = "sortTasksByTagToolStripMenuItem";
            this.sortTasksByTagToolStripMenuItem.Size = new System.Drawing.Size(312, 24);
            this.sortTasksByTagToolStripMenuItem.Text = "Sort tasks by tag";
            this.sortTasksByTagToolStripMenuItem.Click += new System.EventHandler(this.sortTasksByTagToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.MidnightBlue;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(51, 23);
            this.helpToolStripMenuItem.Text = "&Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // subtaskContextMenuStrip
            // 
            this.subtaskContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSubtaskToolStripMenuItem});
            this.subtaskContextMenuStrip.Name = "contextMenuStrip1";
            this.subtaskContextMenuStrip.Size = new System.Drawing.Size(140, 26);
            // 
            // addSubtaskToolStripMenuItem
            // 
            this.addSubtaskToolStripMenuItem.Name = "addSubtaskToolStripMenuItem";
            this.addSubtaskToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.addSubtaskToolStripMenuItem.Text = "Add subtask";
            this.addSubtaskToolStripMenuItem.Click += new System.EventHandler(this.addSubtaskToolStripMenuItem_Click);
            // 
            // taskContextMenuStrip
            // 
            this.taskContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTaskToolStripMenuItem});
            this.taskContextMenuStrip.Name = "taskContextMenuStrip";
            this.taskContextMenuStrip.Size = new System.Drawing.Size(121, 26);
            // 
            // addTaskToolStripMenuItem
            // 
            this.addTaskToolStripMenuItem.Name = "addTaskToolStripMenuItem";
            this.addTaskToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.addTaskToolStripMenuItem.Text = "Add task";
            this.addTaskToolStripMenuItem.Click += new System.EventHandler(this.addTaskToolStripMenuItem_Click);
            // 
            // TaskManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 461);
            this.Controls.Add(this.taskPropertiesGroupBox);
            this.Controls.Add(this.tasksTreeView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TaskManagerForm";
            this.Text = "Task manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TaskManagerForm_FormClosing);
            this.taskPropertiesGroupBox.ResumeLayout(false);
            this.taskPropertiesGroupBox.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.subtaskContextMenuStrip.ResumeLayout(false);
            this.taskContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tasksTreeView;
        private System.Windows.Forms.GroupBox taskPropertiesGroupBox;
        private System.Windows.Forms.TextBox statusDescriptionTextBox;
        private System.Windows.Forms.TextBox tagTextBox;
        private System.Windows.Forms.TextBox taskNameTextBox;
        private System.Windows.Forms.Label registrationDateLabel;
        private System.Windows.Forms.Label statusDescriptionLabel;
        private System.Windows.Forms.Label taskStatusLabel;
        private System.Windows.Forms.Label tagLabel;
        private System.Windows.Forms.Label taskNameLabel;
        private System.Windows.Forms.ComboBox taskStatusComboBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip subtaskContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addSubtaskToolStripMenuItem;
        private System.Windows.Forms.TextBox registrationDateTextBox;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doNotShowMonthlyReportedTasksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewTaskToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip taskContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortTasksByTagToolStripMenuItem;
    }
}


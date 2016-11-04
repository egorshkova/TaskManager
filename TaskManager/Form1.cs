using System;
using System.Drawing;
using System.Windows.Forms;

namespace TaskManager
{
    public partial class TaskManagerForm : Form
    {        
        private Xml xml;
        public static string rootNodeName = "Task list";
        private Boolean isUpdatedAfterSelect = true;
        private TreeNode selectedNode;

        public TaskManagerForm()
        {
            InitializeComponent();
            if (this.tasksTreeView.SelectedNode != null)
            {
                selectedNode = this.tasksTreeView.SelectedNode;
            }
            taskStatusComboBox.SelectedIndex = 0;
            xml = new Xml();

            if (xml.IsLoaded)
            {
                xml.SaveAllTasksInCaseModified(this.tasksTreeView, this.Cursor, !this.doNotShowMonthlyReportedTasksToolStripMenuItem.Checked,
                    this.taskContextMenuStrip, this.subtaskContextMenuStrip);
            }
            xml.Load();
            xml.PopulateTreeViewWithTasks2(this.tasksTreeView, this.Cursor, !this.doNotShowMonthlyReportedTasksToolStripMenuItem.Checked,
                this.taskContextMenuStrip, this.subtaskContextMenuStrip);
        }

       
        #region ButtonEventHandling

        private void addTaskButton_Click(object sender, EventArgs e)
        {
            //need to perform check if task or subtask to be created 
            //depend on current selected node (root or existed task)
            if (tasksTreeView.SelectedNode != null)
            {
                if (String.Compare(tasksTreeView.SelectedNode.Text, rootNodeName) == 0)
                {
                    //root element-> add new task                    
                    TreeNode newNode = xml.AddNewTask();
                    this.tasksTreeView.Nodes[0].Nodes.Add(newNode);
                    xml.AtLeastOneTaskChanged();
                }
                else if (this.tasksTreeView.SelectedNode.Level == 1) //extra check for case when trying to create new subtask for subtask
                {
                    //task element-> add subtask
                    Task selectedTask = xml.TaskDictionary[this.tasksTreeView.SelectedNode];
                    TreeNode subTaskNode = selectedTask.AddSubtask();
                    this.tasksTreeView.SelectedNode.Nodes.Add(subTaskNode);
                    this.tasksTreeView.ExpandAll();
                    xml.AtLeastOneTaskChanged();
                }
            }
        }

        #endregion ButtonEventHandling

        #region ToolStripMenuEventHandling

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //open xml file, load it and save info to taskListArray
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Title = "Select XML Document";
                dialog.Filter = "XML Files(*.xml)|*.xml|All files (*.*)|*.*";
                dialog.FilterIndex = 2;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.XmlTaskList = dialog.FileName;
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (xml.IsLoaded)
            {
                xml.SaveAllTasksInCaseModified(this.tasksTreeView, this.Cursor, !this.doNotShowMonthlyReportedTasksToolStripMenuItem.Checked,
                    this.taskContextMenuStrip, this.subtaskContextMenuStrip);
            }
            xml = new Xml();

            xml.Load();
            xml.PopulateTreeViewWithTasks2(this.tasksTreeView, this.Cursor, !this.doNotShowMonthlyReportedTasksToolStripMenuItem.Checked,
                this.taskContextMenuStrip, this.subtaskContextMenuStrip);
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            xml.SaveAllTasksInCaseModified(this.tasksTreeView, this.Cursor, !this.doNotShowMonthlyReportedTasksToolStripMenuItem.Checked,
                this.taskContextMenuStrip, this.subtaskContextMenuStrip);
            //19-08-2015
            this.tasksTreeView.SelectedNode = selectedNode;
        }

        #endregion ToolStripMenuEventHandling

        #region TreeViewEventHandling

        private void tasksTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selectedNode = this.tasksTreeView.SelectedNode;

            isUpdatedAfterSelect = false;

            //add new values for text field and duplicate the same info in tag field
            taskNameTextBox.Text = "";
            tagTextBox.Text = "";
            statusDescriptionTextBox.Text = "";
            registrationDateTextBox.Text = "";
            taskStatusComboBox.SelectedIndex = 0;

            if (this.tasksTreeView.SelectedNode.Level == 0 && String.Compare(this.tasksTreeView.SelectedNode.Text, rootNodeName) == 0)
            {
                this.taskNameTextBox.Enabled = false;
                this.tagTextBox.Enabled = false;
                this.taskStatusComboBox.Enabled = false;
                this.statusDescriptionTextBox.Enabled = false;
                this.registrationDateTextBox.Enabled = false;
            }
            else
            {
                this.taskNameTextBox.Enabled = true;
                this.tagTextBox.Enabled = true;
                this.taskStatusComboBox.Enabled = true;
                this.statusDescriptionTextBox.Enabled = true;
                this.registrationDateTextBox.Enabled = true;
            }

            Task currentTaskOrSubTask = xml.GetCurrentTaskOrSubtask(tasksTreeView);

            if (currentTaskOrSubTask != null)
            {
                //TODO tag property to be removed?????? to think
                taskNameTextBox.Text = currentTaskOrSubTask.Description;
                taskNameTextBox.Tag = currentTaskOrSubTask.Description;                          //duplicate value in tag property
                tagTextBox.Text = currentTaskOrSubTask.Tag;
                tagTextBox.Tag = currentTaskOrSubTask.Tag;                                       //duplicate value in tag property
                taskStatusComboBox.SelectedIndex = Convert.ToInt32(currentTaskOrSubTask.Status);
                taskStatusComboBox.Tag = Convert.ToInt32(currentTaskOrSubTask.Status);           //duplicate value in tag property
                statusDescriptionTextBox.Text = currentTaskOrSubTask.StatusDescription;
                statusDescriptionTextBox.Tag = currentTaskOrSubTask.StatusDescription;            //duplicate value in tag property
                registrationDateTextBox.Text = currentTaskOrSubTask.RegistrationDate.ToString();
                registrationDateTextBox.Tag = currentTaskOrSubTask.RegistrationDate.ToString();   //duplicate value in tag property
            }            

            isUpdatedAfterSelect = true;

            switch (this.tasksTreeView.SelectedNode.Level)
            {
                case 0: 
                    addNewTaskToolStripMenuItem.Text = "Add new task";
                    break;
                case 1:
                    addNewTaskToolStripMenuItem.Text = "Add new subtask";
                    break;
                default:
                    addNewTaskToolStripMenuItem.Text = "Add new task";
                    break;
            }            

            //TODO could it be the possible place where it's enough to call initializeComboBoxItems?????
        }

        private void tasksTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            //checkIfTaskWasChanged();
        }

        private void tasksTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (tasksTreeView.SelectedNode != null)
                {
                    string msg="";
                    string heading = "";
                    switch (this.tasksTreeView.SelectedNode.Level)
                    {
                        case 0: //root
                            msg = "Would you really like to delete all registered tasks?";
                            heading = "Delete all tasks";
                            break;
                        case 1: //task
                            msg = "Would you really like to delete selected task : " + this.tasksTreeView.SelectedNode.Text + " ? (All its subtasks will be removed too)";
                            heading = "Delete selected task";
                            break;
                        case 2: //subtask
                            msg = "Would you really like to delete selected subtask : " + this.tasksTreeView.SelectedNode.Text + " ?";
                            heading = "Delete selected subtask";
                            break;
                        default:
                            break;
                    }

                    if (MessageBox.Show(msg, heading, MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        xml.RemoveTaskFromDictionary(this.tasksTreeView.SelectedNode);
                        if (this.tasksTreeView.SelectedNode.Level == 0)
                        {
                            foreach (TreeNode child in this.tasksTreeView.Nodes)
                            {
                                child.Nodes.Clear();
                            }
                        }
                        else
                        {
                            this.tasksTreeView.SelectedNode.Remove();
                        }
                    }                    
                }
            }
        }


        #endregion TreeViewEventHandling
        
        #region EventHandling

        private void TaskManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //checkIfTaskWasChanged();
            xml.SaveAllTasksInCaseModified(this.tasksTreeView, this.Cursor, !this.doNotShowMonthlyReportedTasksToolStripMenuItem.Checked, this.taskContextMenuStrip, this.subtaskContextMenuStrip);
        }

        #endregion EventHandling
       

        private void contextMenuToEnable()
        {
            /*it's possible to add as much contextMenuStrip-s as needed and use it for different nodes in tree view*/
            tasksTreeView.ContextMenuStrip = subtaskContextMenuStrip;
            foreach (TreeNode rootNode in tasksTreeView.Nodes)
            {
                rootNode.ContextMenuStrip = subtaskContextMenuStrip;
                foreach (TreeNode childNode in rootNode.Nodes)
                {
                    childNode.ContextMenuStrip = subtaskContextMenuStrip;
                }
            }          
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Task state <-> Color:\n" +
                            "New <-> White \n" +
                            "In progress <-> Yellow \n" +
                            "Done <-> Light green \n" +
                            "Weekly reported <-> Fuchsia \n" +
                            "Monthly reported <-> Cyan \n", "Color scheme", MessageBoxButtons.OK);
        }
        
        private void doNotShowMonthlyReportedTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            doNotShowMonthlyReportedTasksToolStripMenuItem.Checked = (doNotShowMonthlyReportedTasksToolStripMenuItem.Checked) ? false : true;
            Properties.Settings.Default.DoNotShowMonthlyReportedTasks = doNotShowMonthlyReportedTasksToolStripMenuItem.Checked;
            Properties.Settings.Default.Save(); 
            xml.PopulateTreeViewWithTasks2(this.tasksTreeView, this.Cursor, !doNotShowMonthlyReportedTasksToolStripMenuItem.Checked, 
                this.taskContextMenuStrip, this.subtaskContextMenuStrip);
            if (this.tasksTreeView.SelectedNode == null)
            {
                if (selectedNode.BackColor == Color.Cyan)
                {
                    //this.tasksTreeView.SelectedNode = null;
                    registrationDateTextBox.Text = "";
                    taskNameTextBox.Text = "";
                    tagTextBox.Text = "";
                    statusDescriptionTextBox.Text = "";
                    taskStatusComboBox.SelectedIndex = 0;
                }
                else
                {
                    this.tasksTreeView.SelectedNode = selectedNode;
                }
            }
            else
            {
                //this case is for monthly reported tasks which are hidden 
                //just clearing out task's textboxes  + adding null check in update current task function in xml.cs
                
            }
        }

        private void taskNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (isUpdatedAfterSelect & (xml != null))
            {
                xml.UpdateCurrentTask(taskNameTextBox.Name, taskNameTextBox.Text, this.tasksTreeView);
                xml.CurrentTaskWasUpdated();
                xml.AtLeastOneTaskChanged();
            }
        }

        private void tagTextBox_TextChanged(object sender, EventArgs e)
        {
            if (isUpdatedAfterSelect & (xml != null))
            {
                xml.UpdateCurrentTask(tagTextBox.Name, tagTextBox.Text, this.tasksTreeView);
                xml.CurrentTaskWasUpdated();
                xml.AtLeastOneTaskChanged();
                //InitializeComboBoxItems();
            }
        }

        private void taskStatusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdatedAfterSelect & (xml != null))
            {
                xml.UpdateCurrentTask(taskStatusComboBox.Name, taskStatusComboBox.SelectedIndex.ToString(), this.tasksTreeView);
                xml.CurrentTaskWasUpdated();
                xml.AtLeastOneTaskChanged();
            }
        }

        private void statusDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            if (isUpdatedAfterSelect & (xml != null))
            {
                xml.UpdateCurrentTask(statusDescriptionTextBox.Name, statusDescriptionTextBox.Text, this.tasksTreeView);
                xml.CurrentTaskWasUpdated();
                xml.AtLeastOneTaskChanged();
            }
        }

        private void addNewTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //need to perform check if task or subtask to be created 
            //depend on current selected node (root or existed task)
            if (tasksTreeView.SelectedNode != null)
            {
                if (String.Compare(tasksTreeView.SelectedNode.Text, rootNodeName) == 0)
                {
                    //root element-> add new task                    
                    TreeNode newNode = xml.AddNewTask();
                    newNode.ContextMenuStrip = this.subtaskContextMenuStrip;
                    this.tasksTreeView.Nodes[0].Nodes.Add(newNode);
                    
                }
                else if (this.tasksTreeView.SelectedNode.Level == 1) //extra check for case when trying to create new subtask for subtask
                {
                    //task element-> add subtask
                    Task selectedTask = xml.TaskDictionary[this.tasksTreeView.SelectedNode];
                    TreeNode subTaskNode = selectedTask.AddSubtask();
                    this.tasksTreeView.SelectedNode.Nodes.Add(subTaskNode);                    
                }
                this.tasksTreeView.ExpandAll();
                xml.AtLeastOneTaskChanged();
            }
        }

        private void addTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode newNode = xml.AddNewTask();
            newNode.ContextMenuStrip = this.subtaskContextMenuStrip;
            this.tasksTreeView.Nodes[0].Nodes.Add(newNode);
            this.tasksTreeView.ExpandAll();
            xml.AtLeastOneTaskChanged();
        }

        private void addSubtaskToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            Task selectedTask = xml.TaskDictionary[this.tasksTreeView.SelectedNode];
            TreeNode subTaskNode = selectedTask.AddSubtask();
            this.tasksTreeView.SelectedNode.Nodes.Add(subTaskNode);
            this.tasksTreeView.ExpandAll();
            xml.AtLeastOneTaskChanged();
        }

        //14-08-2015 added method
        private void tasksTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.tasksTreeView.SelectedNode = e.Node;
                selectedNode = this.tasksTreeView.SelectedNode;
            }
        }

        private void sortTasksByTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tasksTreeView.Sort();
        }


        //private void addSubtaskToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("add task");
        //}



        #region toberemoved

        /*
          private void restoreXmlTreeViewFromXmlDocument()
        {
            try
            {
                //Just a good practice -- change the cursor to a 
                //wait cursor while the nodes populate
                this.Cursor = Cursors.WaitCursor;

                //First, we'll load the Xml document
                xml.XmlDocument.Load(xml.PathToFile);

                //Now, clear out the treeview, 
                //and add the first (root) node
                tasksTreeView.Nodes.Clear();
                tasksTreeView.Nodes.Add(new TreeNode(xml.XmlDocument.DocumentElement.Name));//taskList

                TreeNode tnode = new TreeNode();
                tnode = (TreeNode)tasksTreeView.Nodes[0];//taskList

                //We make a call to addTreeNode, 
                //where we'll add all of our nodes
                addTreeNode(xml.XmlDocument.DocumentElement, tnode);

                //Expand the treeview to show all nodes
                tasksTreeView.ExpandAll();

                contextMenuToEnable();
            }
            catch (XmlException xmlex) //Exception is thrown is there is an error in the Xml
            {
                MessageBox.Show(xmlex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }           
        }       
         */

        /*
         //This function is called recursively until all nodes are loaded
        private void addTreeNode1(XmlNode xmlNode, TreeNode treeNode)
        {
            XmlNode xNode;
            TreeNode tNode;
            XmlNodeList xNodeList;            

            if (xmlNode.HasChildNodes)
            {
                xNodeList = xmlNode.ChildNodes;
                for (int x = 0; x <= xNodeList.Count - 1; x++)////Loop through the child nodes
                {
                    xNode = xmlNode.ChildNodes[x];

                    if ((xNode.Attributes != null) & (xNode.Attributes["Description"] !=null))                 
                    {
                        treeNode.Nodes.Add(new TreeNode(xNode.Attributes["Description"].Value));
                    }
                    else treeNode.Nodes.Add(new TreeNode(xNode.Name));                    
                    //treeNode.Nodes.Add(new TreeNode(xNode.Name));
                    tNode = treeNode.Nodes[x];
                    addTreeNode1(xNode, tNode);
                }
            }
            else //No children, so add the outer xml (trimming off whitespace)
            {
                treeNode.Text = xmlNode.OuterXml.Trim();
            }            
        }

         */

        #endregion toberemoved

        
    }


    //private void checkIfTaskWasChanged()
    //{
    //    //to check if text property was changed or modified property
    //    //and save changes if there are any
    //    if (this.tasksTreeView.SelectedNode != null)
    //    {
    //        if (String.Compare(this.tasksTreeView.SelectedNode.Text, rootNodeName) != 0)
    //        {
    //            Task currentTaskOrSubTask = new Task();
    //            if (this.tasksTreeView.SelectedNode.Level == 1)
    //            {
    //                //task
    //                currentTaskOrSubTask = xml.TaskDictionary[tasksTreeView.SelectedNode];
    //            }
    //            else if (this.tasksTreeView.SelectedNode.Level == 2)
    //            {
    //                //subtask
    //                currentTaskOrSubTask = xml.TaskDictionary[this.tasksTreeView.SelectedNode.Parent].SubTaskList[this.tasksTreeView.SelectedNode];
    //            }
    //            foreach (Control control in this.taskPropertiesGroupBox.Controls)
    //            {
    //                if ((control is TextBox))
    //                {
    //                    if (String.Compare(control.Text, (String)control.Tag) != 0)
    //                    {
    //                        //field was modified -> need to be saved
    //                        switch (control.Name)
    //                        {
    //                            case "registrationDateTextBox":
    //                                currentTaskOrSubTask.RegistrationDate = Convert.ToDateTime(control.Text);
    //                                break;
    //                            case "taskNameTextBox":
    //                                currentTaskOrSubTask.Description = control.Text;
    //                                this.tasksTreeView.SelectedNode.Text = currentTaskOrSubTask.Description;
    //                                break;
    //                            case "tagTextBox":
    //                                currentTaskOrSubTask.Tag = control.Text;
    //                                break;
    //                            case "statusDescriptionTextBox":
    //                                currentTaskOrSubTask.StatusDescription = control.Text;
    //                                break;
    //                            default:
    //                                break;
    //                        }
    //                        xml.AtLeastOneTaskChanged();
    //                    }
    //                }
    //                else if (control is ComboBox)
    //                {
    //                    if (((ComboBox)control).SelectedIndex != Convert.ToInt32(control.Tag))
    //                    {
    //                        if (control.Name == "taskStatusComboBox")
    //                        {
    //                            currentTaskOrSubTask.Status = (CommonTaskStatus)((ComboBox)control).SelectedIndex;
    //                            switch (currentTaskOrSubTask.Status)
    //                            {
    //                                case CommonTaskStatus.New:
    //                                    this.tasksTreeView.SelectedNode.BackColor = Color.White;
    //                                    break;
    //                                case CommonTaskStatus.InProgress:
    //                                    this.tasksTreeView.SelectedNode.BackColor = Color.Yellow;
    //                                    break;
    //                                case CommonTaskStatus.Done:
    //                                    this.tasksTreeView.SelectedNode.BackColor = Color.LightGreen;
    //                                    break;
    //                                case CommonTaskStatus.Weekly:
    //                                    this.tasksTreeView.SelectedNode.BackColor = Color.Fuchsia;
    //                                    break;
    //                                case CommonTaskStatus.Monthly:
    //                                    this.tasksTreeView.SelectedNode.BackColor = Color.Cyan;
    //                                    break;
    //                                default:
    //                                    this.tasksTreeView.SelectedNode.BackColor = Color.White;
    //                                    break;
    //                            }
    //                        }
    //                        xml.AtLeastOneTaskChanged();
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}      



    //private void taskNameTextBox_MouseClick(object sender, MouseEventArgs e)
    //{
    //    //if taskNameTextBox.ta
    //    //check if parent "Task list" was selected -> then add new task
    //    //if old task was modifid -> modify
    //    if ((tasksTreeView.SelectedNode.Parent == null) & (String.Compare(tasksTreeView.SelectedNode.Text, "Task list") == 0))
    //    {
    //        //"Task list" node was selected
    //        Task newTask = new Task(taskNameTextBox.Text);
    //    }
    //    else
    //    {

    //        //old task to be modified
    //    }
    //}


    /*
     * 
     *  TreeNode node = new TreeNode(newTaskTextBox2.Text);
            //node.BackColor = Color.Yellow; -> in case task is in progress
            // node.BackColor = Color.LightGreen;-> in case task is done
           // node.ForeColor = Color.Red; in case monthly reported
            //node.ForeColor = Color.Blue; in case weekly reported
            
     * 
     * 
            tasksTreeView.Nodes.Add(node);
            // Suppress repainting the TreeView until all the objects have been created.
            tasksTreeView.BeginUpdate();
            // Clear the TreeView each time the method is called.
            tasksTreeView.Nodes.Clear();

            tasksTreeView.Nodes.Add(new TreeNode("task1"));
            tasksTreeView.Nodes[0].Nodes.Add(new TreeNode("task1 description"));
            tasksTreeView.Nodes[0].Nodes.Add(new TreeNode("task1 status"));
            tasksTreeView.Nodes[0].Nodes.Add(new TreeNode("task1 subtasks"));
            tasksTreeView.Nodes[0].Nodes[2].Nodes.Add(new TreeNode("subtask1"));
            tasksTreeView.Nodes.Add(new TreeNode("task2"));
            tasksTreeView.Nodes[1].Nodes.Add(new TreeNode("task2 description"));
            tasksTreeView.Nodes[1].Nodes.Add(new TreeNode("task2 status"));

            // Begin repainting the TreeView.
            tasksTreeView.EndUpdate();    
     * 
     * 
     * 
            xmlfile.checkIfTaskExists("t1");
             */


}

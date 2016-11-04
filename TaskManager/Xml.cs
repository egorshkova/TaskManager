using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Drawing;

namespace TaskManager
{
    class Xml
    {
        private string _pathToFile;
        private Boolean _isLoaded;
        private XmlDocument _xmlDocument;
        private Dictionary<TreeNode, Task> _taskDictionary;
        private Boolean _isAtLeastOneTaskChanged;
        private Task _currentTaskOrSubtask;
        private Boolean _isCurrentTaskModified;
        public ArrayList tags;

        #region Properties

        public Dictionary<TreeNode, Task> TaskDictionary
        {
            get { return this._taskDictionary; }
        }

        public string PathToFile
        {
            set { _pathToFile = value; }
            get { return _pathToFile; }
        }

        public Boolean IsLoaded
        {
            get { return _isLoaded; }
        }

        public XmlDocument XmlDocument
        {
            get { return _xmlDocument; }
            set { _xmlDocument = value; }
        }

        public ArrayList Tags
        {
            get { return tags; }
        }
        #endregion Properties

        public Xml()
        {
            _isLoaded = false;
            _xmlDocument = new XmlDocument();
            tags = new ArrayList();
            //_pathToFile = "";
            if (Path.IsPathRooted(Properties.Settings.Default.XmlTaskList))
            {
                _pathToFile = Properties.Settings.Default.XmlTaskList;
            }
            else
            {
                _pathToFile = Directory.GetCurrentDirectory() + "\\" + Properties.Settings.Default.XmlTaskList;
            }
            _taskDictionary = new Dictionary<TreeNode, Task>();
            _isAtLeastOneTaskChanged = false;
            _currentTaskOrSubtask = new Task();
            _isCurrentTaskModified = false;
        }

        public void AtLeastOneTaskChanged()
        {
            this._isAtLeastOneTaskChanged = true;
        }

        public XmlElement getRoot()
        {
            if (this._isLoaded)
            {
                return this._xmlDocument.DocumentElement;
            }
            else return null;
        }

        public void Load()
        {
            try
            {
                if (!File.Exists(this._pathToFile))
                {
                    using (FileStream fs = File.Create(this._pathToFile))
                    {
                        fs.Close();
                    }

                }

                if (new FileInfo(this._pathToFile).Length == 0)
                {
                    using (XmlTextWriter textWritter = new XmlTextWriter(this._pathToFile, Encoding.UTF8))
                    {
                        textWritter.WriteStartDocument();//Создаём в файле заголовок XML-документа:
                        textWritter.WriteWhitespace("\n");
                        textWritter.WriteStartElement("TaskList");//Создём голову (head):    
                        textWritter.WriteWhitespace("\n");
                        textWritter.WriteFullEndElement();
                        textWritter.Close();//закрываем наш XmlTextWriter:     
                    }
                }

                this._xmlDocument.Load(this._pathToFile);
                this._isLoaded = true;
                restoreTaskListDictionaryFromXmlDocument();
                this._isAtLeastOneTaskChanged = false;
            }
            catch (XmlException xmlex)
            {
                MessageBox.Show(xmlex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public TreeNode AddNewTask()
        {
            Task newTask = new Task("new task");
            TreeNode newTreeNode = new TreeNode();
            //newTreeNode.Text = newTask.Description; //TODO to check how it works with tostring overwritten
            newTreeNode.Text = newTask.ToString();
            newTreeNode.BackColor = Color.White;
            this._taskDictionary.Add(newTreeNode, newTask);
            if (!tags.Contains(newTask.Tag)) tags.Add(newTask.Tag);
            return newTreeNode;
        }

        public void RemoveTaskFromDictionary(TreeNode selectedTreeNode)
        {
            //TODO to think how to remove tags from tags array list
            switch (selectedTreeNode.Level)
            {
                case 0: //remove all tasks
                    this._taskDictionary.Clear();
                    break;
                case 1: //remove task with subtasks if any
                    this._taskDictionary.Remove(selectedTreeNode);
                    break;
                case 2: //remove subtask
                    this._taskDictionary[selectedTreeNode.Parent].SubTaskList.Remove(selectedTreeNode);
                    break;
                default:
                    break;
            }
            this._isAtLeastOneTaskChanged = true;
        }

        private void restoreTaskListDictionaryFromXmlDocument() 
        {
            if (this._isLoaded)
            {
                try
                {
                    this._taskDictionary.Clear();
                    if (this._xmlDocument.DocumentElement.HasChildNodes)
                    {
                        //go through all child nodes (=tasks) for taskList
                        foreach (XmlNode taskNode in this._xmlDocument.DocumentElement.ChildNodes)
                        {
                            readOutTaskOrSubTaskInfoFromXmlNodes(false, taskNode, null);
                        }
                    }
                }
                catch (XmlException xmlex) 
                {
                    MessageBox.Show(xmlex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void readOutTaskOrSubTaskInfoFromXmlNodes(Boolean isSubtask, XmlNode taskOrSubtaskNode, TreeNode parentNodeKey)
        {
            Task taskOrSubTask = new Task();
            TreeNode taskOrSubtaskNodeKey = new TreeNode();
            Boolean subtaskExists = false;

            if ((taskOrSubtaskNode.Attributes != null) & (taskOrSubtaskNode.Attributes["Description"] != null))
            {
                taskOrSubTask.Description = taskOrSubtaskNode.Attributes["Description"].Value;
                //taskOrSubtaskNodeKey.Text = taskOrSubTask.Description; //TODO to check 
                
            }
            foreach (XmlNode taskOrSubTaskProperiesNode in taskOrSubtaskNode)
            {
                if (taskOrSubTaskProperiesNode.FirstChild != null)
                {
                    switch (taskOrSubTaskProperiesNode.Name)
                    {
                        case "Status":
                            taskOrSubTask.Status = (CommonTaskStatus)Convert.ToInt32(taskOrSubTaskProperiesNode.FirstChild.Value);//to check here
                            switch (taskOrSubTask.Status)
                            {
                                case CommonTaskStatus.New:
                                    taskOrSubtaskNodeKey.BackColor = Color.White;
                                    break;
                                case CommonTaskStatus.InProgress:
                                    taskOrSubtaskNodeKey.BackColor = Color.Yellow;
                                    break;
                                case CommonTaskStatus.Done:
                                    taskOrSubtaskNodeKey.BackColor = Color.LightGreen;
                                    break;
                                case CommonTaskStatus.Weekly:
                                    taskOrSubtaskNodeKey.BackColor = Color.Fuchsia;
                                    break;
                                case CommonTaskStatus.Monthly:
                                    taskOrSubtaskNodeKey.BackColor = Color.Cyan;
                                    break;
                                default:
                                    taskOrSubtaskNodeKey.BackColor = Color.White;
                                    break;
                            }
                            break;
                        case "StatusDescription":
                            taskOrSubTask.StatusDescription = taskOrSubTaskProperiesNode.FirstChild.Value;
                            break;
                        case "RegistrationDate":
                            taskOrSubTask.RegistrationDate = Convert.ToDateTime(taskOrSubTaskProperiesNode.FirstChild.Value);
                            break;
                        case "Tag":
                            taskOrSubTask.Tag = taskOrSubTaskProperiesNode.FirstChild.Value;
                            break;
                        case "SubTaskList":
                            //WorkAround to eliminate issue with subtask adding to dictiionary while task isn't added to dictionary
                            if (!isSubtask)
                            {
                                subtaskExists = true;
                                taskOrSubtaskNodeKey.Text = taskOrSubTask.ToString();//TODO new added to check
                                this._taskDictionary.Add(taskOrSubtaskNodeKey, taskOrSubTask);
                                if (!tags.Contains(taskOrSubTask.Tag)) tags.Add(taskOrSubTask.Tag);
                            }
                            if (taskOrSubTaskProperiesNode.HasChildNodes)
                            {
                                foreach (XmlNode subtaskNode in taskOrSubTaskProperiesNode.ChildNodes)
                                {
                                    readOutTaskOrSubTaskInfoFromXmlNodes(true, subtaskNode, taskOrSubtaskNodeKey);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            if (isSubtask)
            {//solved with WA issue при добавлении сабтаски в дикшенари парент (таска  ) еще не добавлена в свой дикшенари -Ю нельзя найти парента и добавить сабтаску
                if (parentNodeKey != null)
                {
                    taskOrSubtaskNodeKey.Text = taskOrSubTask.ToString();//TODO new added to check
                    this._taskDictionary[parentNodeKey].SubTaskList.Add(taskOrSubtaskNodeKey, taskOrSubTask);
                    if (!tags.Contains(taskOrSubTask.Tag)) tags.Add(taskOrSubTask.Tag);
                }
            }
            else if (!subtaskExists)
            {
                taskOrSubtaskNodeKey.Text = taskOrSubTask.ToString();//TODO new added to check
                this._taskDictionary.Add(taskOrSubtaskNodeKey, taskOrSubTask);
                if (!tags.Contains(taskOrSubTask.Tag)) tags.Add(taskOrSubTask.Tag);
            }
        }
        
        private void addNode(XmlDocument doc, XmlNode parent, string elementName, string innerText)
        {
            XmlNode node = doc.CreateElement(elementName);
            node.InnerText = innerText;
            parent.AppendChild(node);
        }

        private void saveTaskToXml(XmlDocument doc, XmlNode parent, Task tsk)
        {
            if (tsk.Status != null) addNode(doc, parent, "Status", Convert.ToInt32(tsk.Status).ToString());
            if (tsk.StatusDescription != null) addNode(doc, parent, "StatusDescription", tsk.StatusDescription.ToString());
            if (tsk.RegistrationDate != null) addNode(doc, parent, "RegistrationDate", tsk.RegistrationDate.ToString());
            if (tsk.Tag != null) addNode(doc, parent, "Tag", tsk.Tag.ToString());
        }

        public void SaveAllTasksInCaseModified(TreeView treeView, Cursor cursor, Boolean showMonthlyReported, ContextMenuStrip taskCMS, ContextMenuStrip subtaskCMS)
        {
            //to check if at least one task was modified - all xml document must be re-written
            //need to add a bool field to class (mordified or not)

            if (this._isAtLeastOneTaskChanged & this._isLoaded & File.Exists(this._pathToFile))
            {
                //remove all childs of documentElement
                //then add all tasks from tasksdictionary
                XmlElement root = _xmlDocument.DocumentElement;
                if (root.HasChildNodes)
                {
                    root.RemoveAll();
                    
                }
                foreach (Task task in this._taskDictionary.Values)
                {
                    //add all tasks to xml file
                    SaveTask(task);
                }

                _xmlDocument.Save(this._pathToFile);

                PopulateTreeViewWithTasks2(treeView, cursor, showMonthlyReported, taskCMS, subtaskCMS);                
                this._isAtLeastOneTaskChanged = false;
            }
        }

        public void SaveTask(Task task)
        {   
            //Создаём XML-запись:
            XmlNode taskNode = _xmlDocument.CreateElement("Task");
            _xmlDocument.DocumentElement.AppendChild(taskNode);
            XmlAttribute xmlAttribute = _xmlDocument.CreateAttribute("Description");
            xmlAttribute.Value = task.Description;
            taskNode.Attributes.Append(xmlAttribute);

            //Добавляем в запись данные:
            saveTaskToXml(_xmlDocument, taskNode, task);
                       
            if (task.SubTaskList != null)
            {
                if (task.SubTaskList.Count != 0)
                {
                    XmlNode subTaskListNode = _xmlDocument.CreateElement("SubTaskList");
                    taskNode.AppendChild(subTaskListNode);
                    foreach (Task subTask in task.SubTaskList.Values)
                    {
                        XmlNode subTaskNode = _xmlDocument.CreateElement("SubTask");
                        XmlAttribute xmlAttributeN = _xmlDocument.CreateAttribute("Description");
                        xmlAttributeN.Value = subTask.Description;
                        subTaskNode.Attributes.Append(xmlAttributeN);
                        subTaskListNode.AppendChild(subTaskNode);

                        saveTaskToXml(_xmlDocument, subTaskNode, subTask);
                    }
                }
            }
        }

        public void PopulateTreeViewWithTasks2(TreeView treeView, Cursor cursor, Boolean showMonthlyReported, 
                                               ContextMenuStrip taskContextMenuStrip, ContextMenuStrip subtaskContextMenuStrip)//using dictionary
        {
            try
            {
                //Just a good practice -- change the cursor to a 
                //wait cursor while the nodes populate
                cursor = Cursors.WaitCursor;

                //Now, clear out the treeview, 
                //and add the first (root) node
                if (treeView.Nodes.Count != 0 && treeView.Nodes[0].Nodes.Count != 0)
                {
                    for (int j = 0; j < treeView.Nodes[0].Nodes.Count; j++)
                    {
                        treeView.Nodes[0].Nodes[j].Nodes.Clear();
                    }
                    treeView.Nodes[0].Nodes.Clear();
                }
                treeView.Nodes.Clear();
                

                treeView.Nodes.Add(new TreeNode(TaskManagerForm.rootNodeName));

                if ((this._taskDictionary != null) & (this._taskDictionary.Count != 0))
                {
                    int i = 0;
                    foreach (KeyValuePair<TreeNode, Task> pair in this._taskDictionary)
                    {
                        if (!showMonthlyReported)
                        {
                            //check task state
                            if (pair.Value.Status != CommonTaskStatus.Monthly)
                            {
                                treeView.Nodes[0].Nodes.Add(pair.Key);
                                if (pair.Value.SubTaskList != null)
                                {
                                    if (pair.Value.SubTaskList.Count != 0)
                                    {
                                        foreach (KeyValuePair<TreeNode, Task> subTaskPair in pair.Value.SubTaskList)
                                        {
                                            if (subTaskPair.Value.Status != CommonTaskStatus.Monthly)
                                            {
                                                treeView.Nodes[0].Nodes[i].Nodes.Add(subTaskPair.Key);
                                            }
                                        }
                                    }
                                }
                                i++;
                            }
                        }
                        else
                        {
                            //check task state
                            treeView.Nodes[0].Nodes.Add(pair.Key);
                            if (pair.Value.SubTaskList != null)
                            {
                                if (pair.Value.SubTaskList.Count != 0)
                                {
                                    foreach (KeyValuePair<TreeNode, Task> subTaskPair in pair.Value.SubTaskList)
                                    {
                                        treeView.Nodes[0].Nodes[i].Nodes.Add(subTaskPair.Key);
                                    }
                                }
                            }
                            i++;
                        }
                    }
                }
                //treeView.Sort();//to do !!!!!!!!!
                //Expand the treeview to show all nodes
                treeView.ExpandAll();

                foreach (TreeNode root in treeView.Nodes)
                {
                    root.ContextMenuStrip = taskContextMenuStrip;
                    foreach (TreeNode taskNode in root.Nodes)
                    {
                        taskNode.ContextMenuStrip = subtaskContextMenuStrip;
                    }
                }

            }
            catch (XmlException xmlex)
            {
                MessageBox.Show(xmlex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cursor = Cursors.Default;
            }
        }

        public Boolean checkIfTaskExists(string attributeValue)
        {
            XmlDocument xmlDocument = new XmlDocument();
           
            xmlDocument.Load(_pathToFile);

            XmlNodeList xmlNodeList = xmlDocument.DocumentElement.ChildNodes; //return all Task nodes
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                if (xmlNode.Attributes["Description"].Value == attributeValue)
                    return true;
            }
            return false;
        }

        public Task GetCurrentTaskOrSubtask(TreeView treeView)
        {
            if (treeView.SelectedNode != null)
            {
                switch (treeView.SelectedNode.Level)
                {
                    case 1:     //task
                        _currentTaskOrSubtask = _taskDictionary[treeView.SelectedNode];
                        _isCurrentTaskModified = false;
                        break;
                    case 2:     //subtask
                        _currentTaskOrSubtask = _taskDictionary[treeView.SelectedNode.Parent].SubTaskList[treeView.SelectedNode];
                        _isCurrentTaskModified = false;
                        break;
                    default:
                        return null;
                }
            }
            return _currentTaskOrSubtask;
        }

        public void UpdateCurrentTask(string controlName, string newValue, TreeView treeView)
        {
            if (treeView.SelectedNode != null)
            {
                switch (controlName)
                {
                    case "registrationDateTextBox":
                        _currentTaskOrSubtask.RegistrationDate = Convert.ToDateTime(newValue);//Convert.ToDateTime(control.Text);
                        break;
                    case "taskNameTextBox":
                        _currentTaskOrSubtask.Description = newValue;                           //control.Text;
                                                                                                //treeView.SelectedNode.Text = _currentTaskOrSubtask.Description; //TODO to check!!!
                        treeView.SelectedNode.Text = _currentTaskOrSubtask.ToString();
                        break;
                    case "tagTextBox":
                        _currentTaskOrSubtask.Tag = newValue;
                        treeView.SelectedNode.Text = _currentTaskOrSubtask.ToString();
                        //if (!tags.Contains(_currentTaskOrSubtask.Tag)) tags.Add(_currentTaskOrSubtask.Tag);
                        break;
                    case "statusDescriptionTextBox":
                        _currentTaskOrSubtask.StatusDescription = newValue;
                        treeView.SelectedNode.Text = _currentTaskOrSubtask.ToString();
                        break;
                    case "taskStatusComboBox":
                        _currentTaskOrSubtask.Status = (CommonTaskStatus)Convert.ToInt32(newValue);     //(CommonTaskStatus)((ComboBox)control).SelectedIndex
                        switch (_currentTaskOrSubtask.Status)
                        {
                            case CommonTaskStatus.New:
                                treeView.SelectedNode.BackColor = Color.White;
                                break;
                            case CommonTaskStatus.InProgress:
                                treeView.SelectedNode.BackColor = Color.Yellow;
                                break;
                            case CommonTaskStatus.Done:
                                treeView.SelectedNode.BackColor = Color.LightGreen;
                                break;
                            case CommonTaskStatus.Weekly:
                                treeView.SelectedNode.BackColor = Color.Fuchsia;
                                break;
                            case CommonTaskStatus.Monthly:
                                treeView.SelectedNode.BackColor = Color.Cyan;
                                break;
                            default:
                                treeView.SelectedNode.BackColor = Color.White;
                                break;
                        }
                        //18-08-2015 
                        if (_currentTaskOrSubtask.SubTaskList != null)
                        {
                            foreach (KeyValuePair<TreeNode, Task> subTaskPair in _currentTaskOrSubtask.SubTaskList)
                            {
                                if (subTaskPair.Value.Status < _currentTaskOrSubtask.Status)
                                {
                                    subTaskPair.Value.Status = _currentTaskOrSubtask.Status;
                                    switch (subTaskPair.Value.Status)
                                    {
                                        case CommonTaskStatus.New:
                                            subTaskPair.Key.BackColor= Color.White;
                                            break;
                                        case CommonTaskStatus.InProgress:
                                            subTaskPair.Key.BackColor = Color.Yellow;
                                            break;
                                        case CommonTaskStatus.Done:
                                            subTaskPair.Key.BackColor = Color.LightGreen;
                                            break;
                                        case CommonTaskStatus.Weekly:
                                            subTaskPair.Key.BackColor = Color.Fuchsia;
                                            break;
                                        case CommonTaskStatus.Monthly:
                                            subTaskPair.Key.BackColor = Color.Cyan;
                                            break;
                                        default:
                                            subTaskPair.Key.BackColor = Color.White;
                                            break;
                                    }                                   
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }            
        }

        public void CurrentTaskWasUpdated()
        {
            _isCurrentTaskModified = true;
        }


        //public void PopulateTreeViewWithTasksWithSpecifiedTag(TreeView treeView, Cursor cursor, Boolean showMonthlyReported,
        //                                                         ContextMenuStrip taskContextMenuStrip, ContextMenuStrip subtaskContextMenuStrip,
        //                                                         string selectedTag)//using dictionary
        //{
        //    try
        //    {
        //        //Just a good practice -- change the cursor to a 
        //        //wait cursor while the nodes populate
        //        cursor = Cursors.WaitCursor;

        //        //Now, clear out the treeview, 
        //        //and add the first (root) node
        //        if (treeView.Nodes.Count != 0 && treeView.Nodes[0].Nodes.Count != 0)
        //        {
        //            for (int j = 0; j < treeView.Nodes[0].Nodes.Count; j++)
        //            {
        //                treeView.Nodes[0].Nodes[j].Nodes.Clear();
        //            }
        //            treeView.Nodes[0].Nodes.Clear();
        //        }
        //        treeView.Nodes.Clear();
                
        //        treeView.Nodes.Add(new TreeNode(TaskManagerForm.rootNodeName));

        //        if ((this._taskDictionary != null) & (this._taskDictionary.Count != 0))
        //        {
        //            int i = 0;
        //            foreach (KeyValuePair<TreeNode, Task> pair in this._taskDictionary)
        //            {
        //                if (String.Compare(selectedTag, "All tags") != 0)
        //                {
        //                    if (String.Compare(selectedTag, pair.Value.Tag) != 0)
        //                    {
        //                        continue;
        //                    }
        //                }
        //                monthlyReportedTasks(showMonthlyReported, pair, treeView, ref i);  
        //            }
        //        }               

        //        //Expand the treeview to show all nodes
        //        treeView.ExpandAll();

        //        foreach (TreeNode root in treeView.Nodes)
        //        {
        //            root.ContextMenuStrip = taskContextMenuStrip;
        //            foreach (TreeNode taskNode in root.Nodes)
        //            {
        //                taskNode.ContextMenuStrip = subtaskContextMenuStrip;
        //            }
        //        }

        //    }
        //    catch (XmlException xmlex)
        //    {
        //        MessageBox.Show(xmlex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        cursor = Cursors.Default;
        //    }
        //}


        private void monthlyReportedTasks(Boolean showMonthlyReported, KeyValuePair<TreeNode, Task> pair, TreeView treeView, ref int iter)
        {
            if (!showMonthlyReported)
            {
                //check task state
                if (pair.Value.Status != CommonTaskStatus.Monthly)
                {
                    treeView.Nodes[0].Nodes.Add(pair.Key);
                    if (pair.Value.SubTaskList != null)
                    {
                        if (pair.Value.SubTaskList.Count != 0)
                        {
                            foreach (KeyValuePair<TreeNode, Task> subTaskPair in pair.Value.SubTaskList)
                            {
                                if (subTaskPair.Value.Status != CommonTaskStatus.Monthly)
                                {
                                    treeView.Nodes[0].Nodes[iter].Nodes.Add(subTaskPair.Key);
                                }
                            }
                        }
                    }
                    iter++;
                }
            }
            else
            {
                //check task state
                treeView.Nodes[0].Nodes.Add(pair.Key);
                if (pair.Value.SubTaskList != null)
                {
                    if (pair.Value.SubTaskList.Count != 0)
                    {
                        foreach (KeyValuePair<TreeNode, Task> subTaskPair in pair.Value.SubTaskList)
                        {
                            treeView.Nodes[0].Nodes[iter].Nodes.Add(subTaskPair.Key);
                        }
                    }
                }
                iter++;
            }
        }

        #region toberemoved


        //            Для получения дат всех заказов можно получить все теги "Заказ", и получить значение их атрибута "Дата".
        //XmlNodeList orders = doc.GetElementsByTagName("Заказ");
        //foreach (XmlNode node in orders)
        //    Console.WriteLine(elemList[i].Attributes["Дата"]); }         

        //public Task getTaskByTaskName(string taskName)
        //{
        //    Task t = new Task("", CommonTaskStatus.New, DateTime.Now);
        //    foreach (Task tsk in this._taskList)
        //    {
        //        //int res = String.Compare(tsk.Description, taskName, false);
        //        if (String.Compare(tsk.Description, taskName, false) == 0)
        //        {
        //            return tsk;
        //        }
        //        else return t;
        //    }
        //    return t;
        //}
        
        #endregion toberemoved
    }
   
}

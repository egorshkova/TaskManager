using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

namespace TaskManager
{

    enum CommonTaskStatus
    {
        New,        //no color
        InProgress, //yellow
        Done,       //green
        Weekly,     //orange
        Monthly     //blue 
    }

    class Task
    {
        private string tag;
        private string description;
        private CommonTaskStatus status;
        private string statusDescription;
        private DateTime registrationDate;
        private Dictionary<TreeNode,Task> subTaskList;    

     
        #region Properties

        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public CommonTaskStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        public string StatusDescription
        {
            get { return statusDescription; }
            set { statusDescription = value; }
        }

        public DateTime RegistrationDate
        {
            get { return registrationDate; }
            set { registrationDate = value; }
        }

        public Dictionary<TreeNode, Task> SubTaskList
        {
            get { return subTaskList; }
        }

        #endregion Properties
       
        public Task()
        {
            this.tag = "";
            this.description = "";
            this.status = 0;
            this.statusDescription = "";
            this.registrationDate = DateTime.Now;
            subTaskList = new Dictionary<TreeNode, Task>();
        }

        public Task(string _taskName)
        {
            this.description = _taskName;
            this.tag = "";
            this.status = 0;
            this.statusDescription = "";
            this.registrationDate = DateTime.Now;
            this.subTaskList = new Dictionary<TreeNode, Task>();
        }

        public TreeNode AddSubtask()
        {
            Task newSubTask = new Task("new subtask");
            newSubTask.status = this.status; //18-08-2015 the same status for new subtask as status of parent node
            TreeNode subTaskNode = new TreeNode();
            subTaskNode.Text = newSubTask.description;
            
            subTaskNode.BackColor = Color.White;////18-08-2015 make correct color of the node
            switch (newSubTask.status)
            {
                case CommonTaskStatus.New:
                    subTaskNode.BackColor = Color.White;
                    break;
                case CommonTaskStatus.InProgress:
                    subTaskNode.BackColor = Color.Yellow;
                    break;
                case CommonTaskStatus.Done:
                    subTaskNode.BackColor = Color.LightGreen;
                    break;
                case CommonTaskStatus.Weekly:
                    subTaskNode.BackColor = Color.Fuchsia;
                    break;
                case CommonTaskStatus.Monthly:
                    subTaskNode.BackColor = Color.Cyan;
                    break;
                default:
                    subTaskNode.BackColor = Color.White;
                    break;
            }            
            this.subTaskList.Add(subTaskNode, newSubTask);
            return subTaskNode;
        }

        public override string ToString()
        {
            string str = "";
            if (String.Compare(tag, "") != 0)
            { 
                str += tag + ": "; 
            }
            str += description;
            //if (String.Compare(statusDescription, "") != 0)//was " "
            //{
            //    str += " (" + statusDescription + ")";
            //}
            return str;
        }
    }
}


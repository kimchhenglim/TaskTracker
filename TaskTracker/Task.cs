using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker
{
    public class Task
    {
        private int id;
        private string description;
        private string status;

        public Task(int id, string description, string status)
        {
            this.id = id;
            this.description = description;
            this.status = status;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}

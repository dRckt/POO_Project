using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Node
    {
        protected List<Line> InputLineList;
        protected List<Line> OutputLineList;
        public Node()
        {
            Console.WriteLine("");
            this.InputLineList = new List<Line> { };
            this.OutputLineList = new List<Line> { };
        }

        public List<Line> GetInputLineList { get { return this.InputLineList; } }
        public List<Line> GetOutputLineList { get { return this.OutputLineList; } }

        public void AddInputLineToList(Line newInputLine)
        {
            this.InputLineList.Add(newInputLine);
        }
        public void AddOutputLineToList(Line newOutputLine)
        {
            this.OutputLineList.Add(newOutputLine);
        }
    }
}

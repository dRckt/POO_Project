using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Node
    {
        protected List<Line> InputLineList;
        protected List<Line> OutputLineList;
        private string Name;
        public Node(string name)
        {
            Name = name;
            Console.WriteLine("");
            InputLineList = new List<Line> { };
            OutputLineList = new List<Line> { };
        }

        public List<Line> GetInputLineList { get { return InputLineList; } }
        public List<Line> GetOutputLineList { get { return OutputLineList; } }
        public string GetName { get { return Name; } }
        public void AddInputLineToList(Line newInputLine)
        {
            InputLineList.Add(newInputLine);
        }
        public void AddOutputLineToList(Line newOutputLine)
        {
            OutputLineList.Add(newOutputLine);
        }

        public void ResetInputLineList(Line newInputLine)
        {
            InputLineList = new List<Line> { newInputLine };
        }
        public void ResetOutputLineList(Line newOutputLine)
        {
            OutputLineList = new List<Line> { newOutputLine };
        }

    }
}

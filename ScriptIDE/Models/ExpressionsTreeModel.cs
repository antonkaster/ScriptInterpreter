using InterpreterLib.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LangGUI.Models
{
    public class ExpressionsTreeModel
    {
        public string Text { get; set; } = "";

        public bool IsNodeExpanded { get; set; } = true;

        public List<ExpressionsTreeModel> Tree { get; set; } = new List<ExpressionsTreeModel>();

        public ExpressionsTreeModel(Expression expression)
        {
            Text = expression.ToString();

            foreach(var item in expression.SubExpressions)
            {
                Tree.Add(new ExpressionsTreeModel(item));
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Windows;

namespace LTHWindow.Windows.Main
{
    public partial class LoadingDialog : Window
    {
        private List<Action> Actions;
        private int _actualAction = 0;
        public LoadingDialog()
        {
            InitializeComponent();
            
            Actions = new List<Action>();
        }

        public void AddAction(Action action)
        {
            Actions.Add(action);
        }

        public void AddActions(IEnumerable<Action> actions)
        {
            foreach (var action in actions)
            {
                Actions.Add(action);
            }
        }

        public void Init()
        {
            // Init label
            ActionText.Text = Actions[0].Name;
            
            // Init progress bar
            ProgressBar.Minimum = 0;
            ProgressBar.Maximum = Actions.Count;

            ProgressBar.Value = 0;
        }

        public void FinishAction()
        {
            if (_actualAction < Actions.Count)
            {
                ActionText.Text = Actions[_actualAction].Name;
                _actualAction++;
                ProgressBar.Value = _actualAction;
            }
            else
            {
                ActionText.Text = "Done";
            }
        }
    }

    public class Action
    {
        public string Name { get; }

        public Action(string name)
        {
            Name = name;
        }
    }
}
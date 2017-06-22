using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QL.UI
{
    public partial class QuestionaireContainer : Form
    {
        public QuestionaireContainer(string formName, IList<UIBinding> bindings, Action evaluatorCallback)
        {
            InitializeComponent();

            EvaluatorCallback = evaluatorCallback;
            Bindings = bindings;          

            titleLabel.Text = $"QL form for {formName}";
            InitializeBindings();
            EvaluatorCallback();
            EvaluatorCallback();
        }

        protected readonly IList<UIBinding> Bindings;
        protected readonly Action EvaluatorCallback;

        private void InitializeBindings()
        {
            tableLayout.RowStyles.Clear();
            for (int i=0; i<Bindings.Count; i++)
            {
                var binding = Bindings[i];
                binding.Control.UpdateCallback = EvaluatorCallback;
                tableLayout.Controls.Add(binding.Label, 0, i);
                tableLayout.Controls.Add((Control)binding.Control, 1, i);
            }
        }
        
        private void doneButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void QuestionaireContainer_DoubleClick(object sender, EventArgs e)
        {
            EvaluatorCallback();
            foreach(var b in Bindings)
            {
                b.Visible = true;
            }
        }
    }
}
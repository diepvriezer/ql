using QL.UI.Widgets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL.UI
{
    public partial class QuestionaireContainer : Form
    {
        public QuestionaireContainer(string formName, IList<UIBinding> bindings)
        {
            InitializeComponent();

            titleLabel.Text = $"QL form for {formName}";
            Bindings = bindings;
            InitializeBindings();
        }

        protected readonly IList<UIBinding> Bindings;

        private void InitializeBindings()
        {
            tableLayout.RowStyles.Clear();
            for (int i=0; i<Bindings.Count; i++)
            {
                var binding = Bindings[i];
                tableLayout.Controls.Add(binding.Label, 0, i);
                tableLayout.Controls.Add((Control)binding.Control, 1, i);
            }
        }

        private void QuestionaireContainer_Load(object sender, EventArgs e)
        {
        }
        
        private void doneButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

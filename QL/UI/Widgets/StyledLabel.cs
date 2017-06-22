using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL.UI.Widgets
{
    public class StyledLabel : Label
    {
        public StyledLabel(string label)
        {
            Label = label;
            Initialize();
        }

        public string Label { get; }

        private void Initialize()
        {
            this.AutoSize = true;
            this.Location = new System.Drawing.Point(4, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Padding = new System.Windows.Forms.Padding(12);
            this.Text = Label;
            this.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        }
    }
}

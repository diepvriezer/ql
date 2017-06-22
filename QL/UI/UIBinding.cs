using QL.UI.Widgets;
using System.Windows.Forms;

namespace QL.UI
{
    public class UIBinding
    {
        public string QuestionId { get; set; }
        public Label Label { get; set; }
        public IWidget Control { get; set; }

        public bool Visible
        {
            get { return Label.Visible || Control.Visible; }
            set
            {
                Control.Visible = value;
                Label.Visible = value;
            }
        }
    }
}

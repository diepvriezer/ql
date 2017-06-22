using QL.Languages.QLang;
using QL.Runtime;
using QL.Traversals;
using QL.UI;
using QL.UI.Widgets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();

            var path = "Tests/valid.ql";
            if (args.Length > 0)
            {
                path = args[0];
            }

            if (!File.Exists(path))
            {
                Console.WriteLine($"ABRT\tCannot open {path}");
                return;
            }

            Console.WriteLine($"INFO\tPath: {path}");

            // Parse form.
            var tree = new AstBuilder().BuildFromPath(path);

            // Print code.
            var source = new PrintSource().Visit(tree);
            Console.WriteLine("INFO\tCode listing");
            Console.WriteLine("---------------------------------");
            Console.WriteLine(source);
            Console.WriteLine("---------------------------------");

            // Validate and check.
            var questionTrav = new QuestionInventory();
            var questionResults = questionTrav.Visit(tree);
            if (!questionTrav.Continue())
                return;

            // Perform type checking, use previously determined questions to type mapping.
            var typeCheck = new TypeChecker(questionResults.QuestionsWithTypes);
            typeCheck.Visit(tree);
            if (!typeCheck.Continue())
                return;

            // Build evaluator with list of tree to UI bindings. Use default factory.
            var widgetFactory = new WidgetFactory();
            var bindings = new CreateBindings(widgetFactory).Visit(tree);
            var evaluator = new Evaluator(bindings, questionResults.ComputedQuestions.ToList());
            Action refresh = () => evaluator.Visit(tree);

            // Open window and run!
            var window = new QuestionaireContainer(tree.Name, bindings, refresh);
            Application.Run(window);
        }
    }
}

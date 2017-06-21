using QL.Languages.QLang;
using QL.Traversals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL
{
    class Program
    {
        static void Main(string[] args)
        {
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
            var questionTravResult = questionTrav.Visit(tree);
            if (!questionTrav.Continue())
                return;

            // Perform type checking, use previously determined questions to type mapping.
            var typeCheck = new TypeChecker(questionTravResult.QuestionsWithTypes);
            typeCheck.Visit(tree);
            if (!typeCheck.Continue())
                return;
        }
    }
}

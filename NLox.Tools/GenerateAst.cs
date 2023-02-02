using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;



namespace NLox.Tools
{
    public class GenerateAst
    {
        public readonly List<string> ExpressionGrammers = new List<string>()
        {
            "Binary : Expr Left, Token Operator, Expr Right",
            "Grouping : Expr expression",
            "Literal : Object Value",
            "Unary : Token Operator, Expr Right"
        };
        public void GenerateClasses(string directory)
        {
            foreach(var g in ExpressionGrammers)
            {
                var classDefinitionSplit =  g.Split(" : ");
                var className = classDefinitionSplit[0].Trim();
                var fieldDefinition = classDefinitionSplit[1].Trim();

                var fileName = className + ".cs";

                
                StringBuilder classContent = new StringBuilder();
                int indent = 0;
                AppendLine(classContent,"using System;");
                AppendLine(classContent,"namespace NLox.Grammer;");

                AppendLine(classContent,"public class " + className + " : Expr",indent);
                AppendLine(classContent,"{",indent); // class start
                

                var fields = fieldDefinition.Split(",");

                indent++;
                AppendLine(classContent, $"public {className}({fieldDefinition.Trim()})", indent);
                AppendLine(classContent, "{", indent); // constructor start
                
                indent++;
                foreach(var field in fields)
                {
                    var fieldParts = field.Trim().Split(" ");
                    var fieldName = fieldParts[1].Trim();
                    AppendLine(classContent,$"this.{fieldName} = {fieldName};",indent);
                }
                indent--;
                AppendLine(classContent, "}", indent); // constructor end

                foreach(var field in fields)
                {
                    var fieldParts = field.Trim().Split(" ");
                    var fieldType = fieldParts[0].Trim();
                    var fieldName = fieldParts[1].Trim();
                    AppendLine(classContent, $"public {fieldType} {fieldName} {{get; set;}}",indent);
                }

                indent--;
                classContent.AppendLine("}"); // class end
                

                string outputPath = Path.Combine(directory, fileName);
                File.WriteAllText(outputPath, classContent.ToString());
            }

        }

        private void AppendLine(StringBuilder stringBuilder, string line, int indentCount=0)
        {
            for(int i = 0; i < indentCount; i++)
            {
                stringBuilder.Append("\t");
            }
            stringBuilder.AppendLine(line);
        }
    }
}

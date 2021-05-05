using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIS.MvcFramework.ViewEngine
{
    public class SisViewEngine : IViewEngine
    {
        public string GetHtml(string templateCode, object viewModel)
        {
            string cshapCode = GenerateCsharpfromTemplatr(templateCode);
            IView executableObject = GenerateExecutableCode(cshapCode, viewModel);
             string html= executableObject.ExecuteTemplate(viewModel);
            return html;
        }

        private string GenerateCsharpfromTemplatr(string templateCode)
        {
            var methodBody = GetMethodBody(templateCode);
            string csharpCode = @"
             using System;
             using System.Text;
            using System.Linq;
            using System.Collections.Generic;
            
            namespace Viewnamespace
            {
                public class ViewClass : IView
                    {
                        public string ExecuteTemplate(object viewModel)
                            {   var html= new StringBuilder();

                                " + methodBody + @"
                                return html.ToString();
                             }

        private string GetMethodBody(string templateCode)
        {
            throw new NotImplementedException();
        }
    }
            }

";
            return csharpCode;
        }

        private string GetMethodBody(string templateCode)
        {
            throw new NotImplementedException();
        }

        private IView GenerateExecutableCode(string csharpCode, object viewModel)
        {
            var compileResult = CSharpCompilation.Create("ViewAssembly")
                 .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                 .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                 .AddReferences(MetadataReference.CreateFromFile(typeof(IView).Assembly.Location));
            if (viewModel != null)
            {
                if (viewModel.GetType().IsGenericType)
                {
                    var genericArguments = viewModel.GetType().GenericTypeArguments;
                    foreach (var genericArgument in genericArguments)
                    {
                        compileResult = compileResult
                            .AddReferences(MetadataReference.CreateFromFile(genericArgument.Assembly.Location));
                    }
                }

                compileResult = compileResult
                    .AddReferences(MetadataReference.CreateFromFile(viewModel.GetType().Assembly.Location));
            }

            var libraries = Assembly.Load(
                new AssemblyName("netstandard")).GetReferencedAssemblies();
            foreach (var library in libraries)
            {
                compileResult = compileResult
                    .AddReferences(MetadataReference.CreateFromFile(
                        Assembly.Load(library).Location));
            }

            compileResult = compileResult.AddSyntaxTrees(SyntaxFactory.ParseSyntaxTree(csharpCode));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                EmitResult result = compileResult.Emit(memoryStream);
                if (!result.Success)
                {
                    return new ErrorView(result.Diagnostics
                        .Where(x => x.Severity == DiagnosticSeverity.Error)
                        .Select(x => x.GetMessage()), csharpCode);
                }

                try
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var byteAssembly = memoryStream.ToArray();
                    var assembly = Assembly.Load(byteAssembly);
                    var viewType = assembly.GetType("ViewNamespace.ViewClass");
                    var instance = Activator.CreateInstance(viewType);
                    return (instance as IView)
                        ?? new ErrorView(new List<string> { "Instance is null!" }, csharpCode);
                }
                catch (Exception ex)
                {
                    return new ErrorView(new List<string> { ex.ToString() }, csharpCode);
                }
            }
        }


    }
}

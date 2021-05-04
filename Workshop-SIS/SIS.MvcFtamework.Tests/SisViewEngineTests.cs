using SIS.MvcFramework.ViewEngine;
using System;
using System.IO;
using Xunit;

namespace SIS.MvcFtamework.Tests
{
    public class SisViewEngineTests
    {
        [Theory]
        [InlineData("CleanHtml")]
        [InlineData("Foreach")]
        [InlineData("IfElseFor")]
        [InlineData("ViewModel")]
        public void TestGetHtml(string fileName)
        {
            var viewModel = new TestViewModel
            {
                DateOfBirth = new DateTime(2020, 7, 15),
                Name = "Dark",
                Price = 555.55M
            };

            IViewEngine viewEngine = new SisViewEngine();
            var view = File.ReadAllText($"ViewTests/{fileName}.html");
            var result = viewEngine.GetHtml(view, viewModel);
            var expectedResult = File.ReadAllText($"ViewTests/{fileName}.Result.html");
            Assert.Equal(expectedResult, result);

        }

       
        public class TestViewModel
        {
            public decimal Price { get; set; }
            public string Name { get; set; }
            public DateTime DateOfBirth { get; set; }
        }
    }
}

   

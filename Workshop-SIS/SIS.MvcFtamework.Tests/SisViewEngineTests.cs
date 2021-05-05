using SIS.MvcFramework.ViewEngine;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace SIS.MvcFtamework.Tests
{
    public class SISViewEngineTests
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
                    DateOfBirth = new DateTime(2020,7, 15),
                    Name = "Dark",
                    Price = 555.55M,
                };
                 IViewEngine viewEngine = new SISViewEngine();
                var view = File.ReadAllText($"ViewTests/{fileName}.html");
                var result = viewEngine.GetHtml(view, viewModel, null);
                var expectedResult = File.ReadAllText($"ViewTests/{fileName}.Result.html");
                Assert.Equal(expectedResult, result);
            }
        
        [Fact]
        public void TestTemplateViewMode()
        {
            IViewEngine viewEngine = new SISViewEngine();
            var actualResult = viewEngine.GetHtml(@"@foreach(var num in Model)
{
<span>@num</span>
}", new List<int> { 1, 2, 3 }, null);
            var expectedResult = @"<span>1</span>
<span>2</span>
<span>3</span>
";
            Assert.Equal(expectedResult, actualResult);
        }
    }
}


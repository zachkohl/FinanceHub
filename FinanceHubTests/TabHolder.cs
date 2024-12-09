using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.Utilities.Helpers.Interfaces;
using Moq;
using Xunit;
using FinanceHub.Controllers;
using Castle.DynamicProxy;
namespace FinanceHub.Tests
{


    public class TabHolder_tests
    {
        private string _defaultSettings = $"{{\"CurrentTab\": 0 }}";

        [Fact]
        public void TabHolder_getStartingTab_checksIfaConfigFileExistsAndIfNotCreatesIt()
        {
            //Arrange

            var internalFile = Mock.Of<IFile>();
            Mock.Get(internalFile).Setup(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));
            Mock.Get(internalFile).Setup(f => f.Exists(It.IsAny<string>())).Returns(false);
            Mock.Get(internalFile).Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(_defaultSettings);

            var fileSystem = Mock.Of<IFileSystem>();
            Mock.Get(fileSystem).Setup(static f => f.File).Returns(internalFile);

            var file = Mock.Of<IFile>();




            var TablHolder = new TabHolder(fileSystem);

            //Act

            TablHolder.GetStartingTab();

            //Assert

            Mock.Get(internalFile).Verify(f => f.Exists(It.IsAny<string>()), Times.Once);


        }


        [Theory]
        [InlineData((int)TabHolder.TabOptions.Input)]
        [InlineData((int)TabHolder.TabOptions.Data)]
        public void TabHolder_getStartingTab_returnsCorrectTabFromFile(int tab)
        {
            //Arrange
            IFileSystem fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
    {
        { "settings.json", new MockFileData(   $"{{\"CurrentTab\": {tab} }}") }
    });
               var file = Mock.Of<IFile>();
            Mock.Get(file).Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
            //Act
            TabHolder MyTablHolder = new TabHolder(fileSystem);

            FinanceHubSettings settings = MyTablHolder.GetStartingTab();

            //Assert
            Assert.Equal(settings.CurrentTab, tab);

        }

        [Fact]
        public void TabHolder_saveChanges_savesTheTabOptionToAFile()
        {
            //arrange
            var internalFile = Mock.Of<IFile>();
            Mock.Get(internalFile).Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(_defaultSettings);
            Mock.Get(internalFile).Setup(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));

            var fileSystem = SetupMultiLevelFileMock(internalFile);

            var MyTablHolder = new TabHolder(fileSystem);

            //Act
            MyTablHolder.SaveTab(0);

            //Assert
            Mock.Get(internalFile).Verify(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()),Times.Once);
        }

        private IFileSystem SetupMultiLevelFileMock(IFile f)
        {
            var fileSystem = Mock.Of<IFileSystem>();
            Mock.Get(fileSystem).Setup(static f => f.File).Returns(f);
            return fileSystem;
        } 

    }
}

using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceHub.Controllers;
using FinanceHub.DataBase;
using Moq;
using System.Text.Json;
namespace FinanceHub.Tests
{
    public class Users_Tests : IDisposable
    {
        public string _name = "testUser";
        private string _defaultSettings;

        IDB _db;
        public Users_Tests()
        {
            FinanceHubSettings settings = new FinanceHubSettings { CurrentTab = 0,CurrentUser="testUserY" };
            _defaultSettings = JsonSerializer.Serialize(settings);

            _db = new DBWrapper();
            _db.deleteDBForUser(_name);
        }
        public void Dispose()
        {
            _db.deleteDBForUser(_name);
        }
        public string prepSettings(FinanceHubSettings settings)
        {
            return JsonSerializer.Serialize(settings);
        }


        [Fact]
        public void Users_getCurrentUser_returns_nullWhenNoCurrentUserExist()
        {
            var internalFile = Mock.Of<IFile>();
            Mock.Get(internalFile).Setup(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));
            Mock.Get(internalFile).Setup(f => f.Exists(It.IsAny<string>())).Returns(true);           
            Mock.Get(internalFile).Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(prepSettings(new FinanceHubSettings { CurrentTab = 0, CurrentUser = "testUserY" }));
            var fileSystem = Mock.Of<IFileSystem>();
            Mock.Get(fileSystem).Setup(static f => f.File).Returns(internalFile);
            Users MyUsers = new Users(fileSystem,_db);
            var MyUser = MyUsers.GetCurrentUser();

            Assert.NotNull(MyUser);
        }

        [Fact]
        public void Users_CreateUser_AddsDatabaseAndPersistsUserName()
        {
            //Arrange
            var internalFile = Mock.Of<IFile>();
            Mock.Get(internalFile).Setup(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));
            Mock.Get(internalFile).Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
            Mock.Get(internalFile).Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(prepSettings(new FinanceHubSettings { CurrentTab = 0, CurrentUser = "testUserY" }));
            var fileSystem = Mock.Of<IFileSystem>();
            Mock.Get(fileSystem).Setup(static f => f.File).Returns(internalFile);


            //Act
            Users MyUsers = new Users(fileSystem,_db);
            MyUsers.CreateUser(_name);

            ////Assert
            _db.connectForUser(_name);
            Mock.Get(internalFile).Verify(f => f.WriteAllText(It.IsAny<string>(),It.IsAny<string>()), Times.Once);

        }

        [Fact]
        public void Users_SwitchUsers_ChangesDefaultInSettingsJson()
        {
            //Arrange
            string newName = "newUserX";
            var internalFile = Mock.Of<IFile>();
            Mock.Get(internalFile).Setup(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));
            Mock.Get(internalFile).Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
            string[] users = ["testUserYVERYNEW", newName];

            var newSettings = new FinanceHubSettings { CurrentTab = 0, CurrentUser = "testUserYVERYNEW", Users = users };

            Mock.Get(internalFile).Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(prepSettings(newSettings));
            var fileSystem = Mock.Of<IFileSystem>();
            Mock.Get(fileSystem).Setup(static f => f.File).Returns(internalFile);
            var mockDb = Mock.Of<IDB>();
            Mock.Get(mockDb).Setup(db => db.connectForUser(It.IsAny<string>()));



            //Act
            Users MyUsers = new Users(fileSystem, mockDb);
            MyUsers.SwitchUser(newName);


            //Assert
            Mock.Get(internalFile).Verify(f => f.WriteAllText(It.IsAny<string>(), It.Is<string>(s => s.Contains($"CurrentUser\":\"{newName}"))), Times.Once);
            Mock.Get(mockDb).Verify(db => db.connectForUser(It.IsAny<string>()));
        }

        [Fact]
        public void Users_DeleteUser_RemovesUserFromSettingsJson()
        {
            //Arrange
            string removeName = "removeMe";
            string defaultUser = "defaultUser";
            var internalFile = Mock.Of<IFile>();
            Mock.Get(internalFile).Setup(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));
            Mock.Get(internalFile).Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
            string[] users = [defaultUser, removeName];

            var newSettings = new FinanceHubSettings { CurrentTab = 0, CurrentUser = defaultUser, Users = users };

            Mock.Get(internalFile).Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(prepSettings(newSettings));
            var fileSystem = Mock.Of<IFileSystem>();
            Mock.Get(fileSystem).Setup(static f => f.File).Returns(internalFile);
            var mockDb = Mock.Of<IDB>();
            Mock.Get(mockDb).Setup(db => db.deleteDBForUser(It.IsAny<string>()));


            //Act
            Users MyUsers = new Users(fileSystem, mockDb);
            MyUsers.DeleteUser(removeName);

            //Assert
            Mock.Get(internalFile).Verify(f => f.WriteAllText(It.IsAny<string>(), It.Is<string>(s => !s.Contains(removeName))), Times.Once);
            Mock.Get(mockDb).Verify(db => db.deleteDBForUser(It.IsAny<string>()));


        }


    }
}

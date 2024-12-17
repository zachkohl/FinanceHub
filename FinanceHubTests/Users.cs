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
using FinanceHub.Models;

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


        [Theory]
        [InlineData("testExoticUser")]
        [InlineData(null)]
        public void Users_getCurrentUser_returns_UserOrNull(string? name)
        {
            var internalFile = Mock.Of<IFile>();
            Mock.Get(internalFile).Setup(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));
            Mock.Get(internalFile).Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
            Moq.Language.Flow.IReturnsResult<IFile> returnsResult = Mock.Get(internalFile).Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(prepSettings(settings: new FinanceHubSettings { CurrentTab = 0, CurrentUser = name, Users = name == null ? [] : [name] }));
            var fileSystem = Mock.Of<IFileSystem>();
            Mock.Get(fileSystem).Setup(static f => f.File).Returns(internalFile);
            Users MyUsers = new Users(fileSystem,_db);
            var MyUser = MyUsers.GetCurrentUser();
            if (name == null)
            {
                Assert.Null(MyUser);
            }
            else
            {
                Assert.Equal(name, MyUser?.Name);
            }
           
        }

        [Fact]
        public void Users_CreateUser_AddsDatabaseAndPersistsUserName()
        {
            //Arrange
            var internalFile = Mock.Of<IFile>();
            Mock.Get(internalFile).Setup(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));
            Mock.Get(internalFile).Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
            Mock.Get(internalFile).SetupSequence(f => f.ReadAllText(It.IsAny<string>()))
                .Returns(prepSettings(new FinanceHubSettings { CurrentTab = 0, CurrentUser = "testUserY" }))
                .Returns(prepSettings(new FinanceHubSettings { CurrentTab = 0, CurrentUser = "testUserY", Users = ["testUserY",_name] }));

            var fileSystem = Mock.Of<IFileSystem>();
            Mock.Get(fileSystem).Setup(static f => f.File).Returns(internalFile);


            //Act
            Users MyUsers = new Users(fileSystem,_db);
            MyUsers.CreateUser(_name);

            ////Assert
            _db.connectForUser(_name);
            Mock.Get(internalFile).Verify(f => f.WriteAllText(It.IsAny<string>(),It.IsAny<string>()), Times.Exactly(2));

        }

        [Fact]
        public void Users_CreateUser_RejectsBadName()
        {
            //Arrange
            string name = "this is not a valid name";
            var internalFile = Mock.Of<IFile>();
            Mock.Get(internalFile).Setup(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));
            Mock.Get(internalFile).Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
            string[] users = ["Y", "X"];
            var newSettings = new FinanceHubSettings { CurrentTab = 0, CurrentUser = "Y", Users = users };
            Mock.Get(internalFile).Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(prepSettings(newSettings));
            var fileSystem = Mock.Of<IFileSystem>();
            Mock.Get(fileSystem).Setup(static f => f.File).Returns(internalFile);
            var mockDb = Mock.Of<IDB>();
            Mock.Get(mockDb).Setup(db => db.deleteDBForUser(It.IsAny<string>()));


            //Act
            Users MyUsers = new Users(fileSystem, _db);
           bool success= MyUsers.CreateUser(name);

            //Assert
            Assert.False(success);
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

        [Fact]
public void Users_GetUsersNotCurrentUser_ReturnsAListOfUsersButNotTheCurrentUser()
        {
            //Arrange
            string otherUser = "removeMe";
            string defaultUser = "defaultUser";
            var internalFile = Mock.Of<IFile>();
            string[] users = [defaultUser, otherUser];

            var newSettings = new FinanceHubSettings { CurrentTab = 0, CurrentUser = defaultUser, Users = users };

            Mock.Get(internalFile).Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(prepSettings(newSettings));
            var fileSystem = Mock.Of<IFileSystem>();
            Mock.Get(fileSystem).Setup(static f => f.File).Returns(internalFile);
            var mockDb = Mock.Of<IDB>();


            //Act
            Users MyUsers = new Users(fileSystem, mockDb);
            List<string> list = MyUsers.GetUsersNotCurrentUser();


            //Assert
            Assert.Single(list);
            Assert.Equal(otherUser, list.First());


        }
        [Fact]

public void Users_processFileForActiveUser_processesCSVFile()
        {
            //Arrange
            string csvData = @"Date,Description,Original Description,Category,Amount,Status
12/12/2024,SQ *TERRE COFFEE & B             121224,SQ *TERRE COFFEE & B             121224,Category Pending,-3.5,Pending
";

            Transaction myTransaction = new Transaction { Date=new DateOnly(2024,12,12),Amount = -3.5m,Description= "SQ *TERRE COFFEE & B             121224",OriginalDescription= "SQ *TERRE COFFEE & B             121224",BankCatagory= "Category Pending",BankStatus="Pending" };
            List<Transaction> mockList = new List<Transaction>() { myTransaction};
           
            
            string defaultUser = "defaultUser";
            var internalFile = Mock.Of<IFile>();
            string[] users = [defaultUser];
            Mock.Get(internalFile).Setup(f => f.Exists(It.IsAny<string>())).Returns(true);

            var newSettings = new FinanceHubSettings { CurrentTab = 0, CurrentUser = defaultUser, Users = users };

            Mock.Get(internalFile).SetupSequence(f => f.ReadAllText(It.IsAny<string>()))
                .Returns(csvData);
            var fileSystem = Mock.Of<IFileSystem>();
            Mock.Get(fileSystem).Setup(static f => f.File).Returns(internalFile);
            var mockDb = Mock.Of<IDB>();
            Mock.Get(mockDb).Setup(static db => db.saveTransactions(It.IsAny<List<Transaction>>()));
            Users myUsers = new Users(fileSystem, mockDb);


            //Act
            myUsers.processFileForActiveUser("testFilePath");

            //Assert
              Mock.Get(mockDb).Verify(db => db.saveTransactions(It.IsAny<List<Transaction>>()));


        }

    }
}

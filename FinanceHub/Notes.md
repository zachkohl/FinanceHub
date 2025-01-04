# notes

great article on Entity Core: https://learn.microsoft.com/en-us/ef/core/


https://wpftutorial.net/DataGrid.html

https://learn.microsoft.com/en-us/ef/core/querying/


new name for Users. TransactionsService

new service Users. All this does is handle user stuff (CRUD). UsersService

Both define their own interface. Both recieve the SQLWrapper as an injection.

the SQLWrapper implements both interfaces. 

https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.itemscontrol?view=windowsdesktop-9.0

Have to use code behind if you want parameters in the ViewModel: https://stackoverflow.com/questions/33337649/wpf-the-viewmodel-type-does-not-contain-any-accessible-constructors;

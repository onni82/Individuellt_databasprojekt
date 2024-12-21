namespace Individuellt_databasprojekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu.Run(); // Runs a menu based on Entity Framework (ORM)
			//SQLMenu.Run(); // Runs a menu based on ADO.NET instead of Entity Framework (ORM)
		}
    }
}
// Scaffold-DbContext "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=School;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -ContextDir Data -Context SchoolContext
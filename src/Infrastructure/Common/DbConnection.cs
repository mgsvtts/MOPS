using Infrastructure.Models;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.PostgreSQL;
using Type = Infrastructure.Models.Type;

namespace Infrastructure.Common;

public sealed class DbConnection : DataConnection
{
    public ITable<Image> Images => this.GetTable<Image>();
    public ITable<Type> Types => this.GetTable<Type>();
    public ITable<MerchItem> MerchItems => this.GetTable<MerchItem>();
    public ITable<Order> Orders => this.GetTable<Order>();
    public ITable<OrderItem> OrderItems => this.GetTable<OrderItem>();

    public DbConnection() : base("Default")
    { }

    public static void Bind(string connectionString)
    {
        AddConfiguration("Default", connectionString, PostgreSQLTools.GetDataProvider(connectionString: connectionString));

#if DEBUG
        TurnTraceSwitchOn(System.Diagnostics.TraceLevel.Info);
        WriteTraceLine = (s, s1, l) => System.Diagnostics.Debug.WriteLine(s, s1);
#endif
    }
}
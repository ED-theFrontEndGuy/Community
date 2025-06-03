namespace App.DAL.EF.DataSeeding;

public class InitialData
{
    public static readonly (string roleName, Guid? id)[]
        Roles =
        [
            ("admin", null),
            ("user", null),
        ];

    public static readonly (string name, string firstName, string lastName, string password, Guid? id, string[] roles)[]
        Users =
        [
            ("admin@community.com", "admin", "community", "Admin 123", null, ["admin"]),
            ("user@community.com", "user", "community", "User 123", null, ["user"]),
        ];
}
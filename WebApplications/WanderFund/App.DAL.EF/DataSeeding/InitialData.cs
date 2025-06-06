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
            ("admin@wanderfund.com", "admin", "wanderfund", "Admin 123", null, ["admin"]),
            ("user@wanderfund.com", "user", "wanderfund", "User 123", null, ["user"]),
        ];
}
namespace Medical.System.Core.Enums;

public static class UserRoleConstants
{
    public const string Admin = "Admin";
    public const string Doctor = "Doctor";
    public const string Nurse = "Nurse";
    public const string Patient = "Patient";
    public const string Receptionist = "Receptionist";

    public static List<string> GetRoles()
    {
        return new List<string>
            {
                Admin,
                Doctor,
                Nurse,
                Patient,
                Receptionist
            };
    }
}

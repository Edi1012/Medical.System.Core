namespace Medical.System.Core.Models.Entities
{
    public class EmergencyContact
    {
        public string Name { get; set; } // Full name of the emergency contact
        public string Relationship { get; set; } // Relationship to the patient (e.g., spouse, parent, friend)
        public string PhoneNumber { get; set; } // Phone number to reach the emergency contact
        public string Email { get; set; } // Optional email address
        public Address Address { get; set; } // Optional address information, could be the same Address class used elsewhere

    }
}

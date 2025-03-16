namespace apief;

    public class User
    {
        public Guid id {get; set;}
        public string email {get; set;}="";
        public string? passwordHash {get; set;}
        
    }
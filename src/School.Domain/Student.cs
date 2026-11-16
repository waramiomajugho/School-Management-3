namespace School.Domain;



public class Student

{
 public Guid studentid {get; set;} = Guid.NewGuid();
 public string Name {get; set;} = string.Empty;
 public string Email {get; set;} = string.Empty;


}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test_background_api.dbContext.Entities;

[Table("message", Schema = "public")]
public class Message
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("date")]
    public DateTime Date { get; set; }

    [Column("lead_id")]
    public string LeadId { get; set; }

    [Column("first_name")]
    public string FirstName { get; set; }

    [Column("last_name")]
    public string LastName { get; set; }

    [Column("street_address")]
    public string StreetAddress { get; set; }

    [Column("city")]
    public string City { get; set; }

    [Column("state")]
    public string State { get; set; }

    [Column("phone1")]
    public string Phone1 { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("lead_source")]
    public string LeadSource { get; set; }

    [Column("service")]
    public string Service { get; set; }

    [Column("questions")]
    public string Questions { get; set; }

    [Column("zip")]
    public string Zip { get; set; }  
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EWYRYV_HFT_202223.Models
{
    [Table("manager")]
    public class Manager
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ManagerId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string? Nationality { get; set; }

        [ForeignKey(nameof(Team))]
        public int? TeamId { get; set; }

        [NotMapped]
        public virtual Team Team { get; set; }

        public Manager()
        {

        }

        public Manager(string line)
        {
            string[] split = line.Split("#");
            ManagerId = int.Parse(split[0]);
            Name = split[1];
            Nationality = split[2];
            TeamId = int.Parse(split[3]);
        }

    }
}

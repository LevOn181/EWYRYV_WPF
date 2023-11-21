using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWYRYV_HFT_202223.Models
{
    [Table("team")]
    public class Team
    {

        [Key]
        //[Range(1,20)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamId { get; set; }
        
        [Required]
        [StringLength(240)]
        public string Name { get; set; }

        [Range(1800, 2022)]
        public int? FoundationYear { get; set; }
        
        [NotMapped]
        public virtual ICollection<Player> Players { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Manager Manager { get; set; }
        public Team()
        {

        }
        public Team(string name, int? foundation)
        {
            this.Name = name;
            this.FoundationYear = foundation;
        }

        public Team(string line)
        {
            string[] split = line.Split("#");
            TeamId = int.Parse(split[0]);
            Name = split[1];
            FoundationYear = int.Parse(split[2]);
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loremipsum.Gym.Entities
{
    [Serializable]
    public class LogIn : IComparable<LogIn>
    {
        public LogIn(string logInName, string logInPassword, int rank)
        {
            LogInName = logInName;
            LogInPassword = logInPassword;
            Rank = rank;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string LogInName { get; set; }

        [Required] public string LogInPassword { get; set;}

        [Required] public int Rank { get; set; }

        public int CompareTo(LogIn other)
        {
            return LogInName.CompareTo(other.LogInName);
        }

    }
}

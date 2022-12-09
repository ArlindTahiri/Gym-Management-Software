﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace loremipsum.Entities
{
    [Serializable]
    public class Member: IComparable<Member>
    {
        public Member(string forename,string surname,Address address,string eMail, int iban, DateTime birthday)
        {
            MemberID = MemberID++;
            Forename = forename;
            Surname = surname;
            Address = address;
            EMail = eMail;
            Iban = iban;
            Birthday = birthday;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MemberID { get; }

        [Required] public string Forename { get; set; }

        [Required] public string Surname { get; set;}

        [Required] public Address Address { get; set; }

        [Required] public string EMail { get; set; }

        [Required] public int Iban { get; set; }

        [Required] public DateTime Birthday { get; set;}

        public int CompareTo(Member other)
        {
            return MemberID.CompareTo(other.MemberID);
        }

        public override string ToString() 
        {
            return Forename+" "+Surname+" "+Address+" "+EMail+" "+Iban+" "+ Birthday;
        }
    }
}

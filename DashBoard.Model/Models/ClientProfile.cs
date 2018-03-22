using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DashBoard.Model.Models
{ 
    /// <summary>
    /// Клас таблиці інформації о користувачі
    /// </summary>
public class ClientProfile
    {
        [Key]
        [ForeignKey("DutUser")]
        public string Id { get; set; }
        public bool Enable { get; set; }

        public string FullName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastVisitDate { get; set; }

        public virtual DutUser DutUser { get; set; }

        public ClientProfile() => CreateDate = DateTime.Now;
    }
}

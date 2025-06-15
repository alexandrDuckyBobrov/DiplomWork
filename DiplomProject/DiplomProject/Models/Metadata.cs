using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiplomProject.Models
{
    public class catalogueMetadata
    {
        [Display(Name = "ID")]
        public int catalogid { get; set; }
        public string catalogname { get; set; }
        public string catalogimagepath { get; set; }
        public string catalogdesc { get; set; }
    }
    public class contactsMetadata
    {
        [Display(Name = "ID")]
        public int id { get; set; }
        [Display(Name = "Пользователь")]
        public int users { get; set; }
        [Display(Name = "Телефон")]
        public string phone { get; set; }
        [Display(Name = "Электронная почта")]
        public string email { get; set; }
        public virtual users users1 { get; set; }
    }
    public class requestsMetadata
    {
        [Display(Name = "ID")]
        [Required]
        public int requestid { get; set; }
        [Display(Name = "Статус")]
        [Required]
        public int status { get; set; }
        [Display(Name = "Описание")]
        [Required]
        public string requestdesc { get; set; }
        [Display(Name = "Прикреплённый файл")]
        public string requestfiles { get; set; }
        [Display(Name = "Пользователь")]
        public int users { get; set; }
    }
    public class requeststatusMetadata
    {
        [Display(Name = "ID")]
        public int statusid { get; set; }
        [Display(Name = "Статус")]
        public string statusname { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<requests> requests { get; set; }
    }
    public class rolesMetadata
    {
        [Display(Name = "ID")]
        public int roleid { get; set; }
        [Display(Name = "Роль")]
        public string rolename { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<roleusers> roleusers { get; set; }
    }
    public class roleusersMetadata
    {
        [Display(Name = "ID")]
        public int users { get; set; }
        public Nullable<int> roles { get; set; }
        public virtual roles roles1 { get; set; }
        public virtual users users1 { get; set; }
    }
    public partial class usersMetadata
    {
        [Display(Name = "ID")]
        public int userid { get; set; }

        [Display(Name = "Имя пользователя")]
        public string login { get; set; }
        [Display(Name = "Пароль")]
        public string password { get; set; }
        public virtual roleusers roleusers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<contacts> contacts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<requests> requests { get; set; }
    }
}

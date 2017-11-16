using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework_04.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string StudyGroupId { get; set; }
        public string StudyGroupName { get; set; }
    }
}
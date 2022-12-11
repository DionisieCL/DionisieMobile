using System;
using System.ComponentModel.DataAnnotations;

namespace Schoolager.Web.Data.Entities
{
    public class LessonResource : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Guid FileId { get; set; }
        public string FileFullPath => $"https://schoolmanagesysstorage.blob.core.windows.net/teachers/{FileId}";

        public LessonData LessonData { get; set; }

        public int LessonDataId { get; set; }
    }
}

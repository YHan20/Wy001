using System;

namespace WebApi.Api.Entity
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int DisplayOrder { get; set; }
        public string Remarks { get; set; }
    }
}
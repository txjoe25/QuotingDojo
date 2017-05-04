using System;
using System.ComponentModel.DataAnnotations;

namespace QuotingDojo.Models
{
    public class Quote : BaseEntity
    {
        [Key]
        public int QuoteId { get; set; }

        [Required]
        public string QuoterName { get; set; }

        [Required]
        public string QuoteContent { get; set; }

        public int Likes { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Quote()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Likes = 0;
        }
    }
}
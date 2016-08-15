using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace instinct360.Models
{
 
    public class ReviewAnswer
    {
        public int Id { get; set; }
        public float Number { get; set; }
        public string Question { get; set; }
        public string Information { get; set; }
        public bool MultipleChoice { get; set; }
        public string ChoiceText { get; set; }
        public int ChoiceId { get; set; }
        public string FreeText { get; set; }
    }

    public class ReviewSection
    {
        public int Id { get; set; }
        public float Number { get; set; }
        public string Name { get; set; }
        public List<ReviewAnswer> Answers { get; set; }
    }

    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ReviewTemplateName { get; set; }
        public List<ReviewSection> Sections { get; set; }
    }

}
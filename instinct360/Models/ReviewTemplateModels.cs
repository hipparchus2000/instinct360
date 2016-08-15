using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace instinct360.Models
{
    public class ChoiceAttributeWeighting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Value { get; set; }
    }

    public class ReviewQuestionChoice
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<ChoiceAttributeWeighting> AttributeValues { get; set; }
    }

    public class ReviewQuestion
    {
        public int Id { get; set; }
        public float Number { get; set; }
        public string Question { get; set; }
        public string Information { get; set; }
        public bool MultipleChoice { get; set; }
        public List<ReviewQuestionChoice> Choices { get; set; }
    }

    public class ReviewTemplateSection
    {
        public int Id { get; set; }
        public float Number { get; set; }
        public string Name { get; set; }
        public List<string> attributeList { get; set; }
        public List<ReviewQuestion> Questions { get; set; }
    }

    public class ReviewTemplate
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ReviewTemplateName { get; set; }
        public List<ReviewTemplateSection> Sections { get; set; }
    }

}
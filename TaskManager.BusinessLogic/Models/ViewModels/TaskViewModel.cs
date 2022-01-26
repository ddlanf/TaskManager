using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.BusinessLogic.Models.ViewModels
{
    public class TaskViewModel
    {
        public List<ProjectModel> Projects { get; set; } = new();

        [Required]
        public string Description { get; set; } = "";

        [Required]
        [ValidDateString]
        [DateLaterThan("DueDate", ErrorMessage = "Start Date cannot be later than due date")]
        public string StartDate { get; set; } = "";
        
        [Required]
        [ValidDateString]
        public string DueDate { get; set; } = "";

        [Required]
        public int AssignedProjectId { get; set; }
        public List<EmployeeModel> Employees { get; set; } = new();

        [Required]
        public List<int> AssignedEmployeeIds { get; set; } = new();

        public List<ProjectTaskModel> ProjectTasks { get; set; } = new();

        public string? ServerErrorMessage { get; set; }
    }
}

public class ValidDateStringAttribute : ValidationAttribute
{
    public string GetErrorMessage() => $"Enter value should be in the format of dd-mm-yyyy";

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var dateString = value.ToString();
        DateTimeOffset date;
        var validString = DateTimeOffset.TryParse(dateString, out date);
        if (string.IsNullOrEmpty(dateString) || !validString)
        {  
            return new ValidationResult(GetErrorMessage());
        }
        return ValidationResult.Success;
    }
}

public class DateLaterThanAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public DateLaterThanAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        ErrorMessage = ErrorMessageString;
        var currentValue = value.ToString();
        DateTimeOffset startDate;
        var validStartDates = DateTimeOffset.TryParse(currentValue, out startDate);

        var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

        if (property == null)
            throw new ArgumentException("Property with this name not found");

        var comparisonValue = property.GetValue(validationContext.ObjectInstance)!.ToString();
        DateTimeOffset dueDate;
        var validDueDates = DateTimeOffset.TryParse(comparisonValue, out dueDate);

        if (DateTimeOffset.Compare(startDate, dueDate) > 0 && (validStartDates && validDueDates))
            return new ValidationResult(ErrorMessage);

        return ValidationResult.Success;
    }
}
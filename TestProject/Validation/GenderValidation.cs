using System.ComponentModel.DataAnnotations;

namespace TestProject.Validation
{
    public class GenderValidation : ValidationAttribute
    {
        public GenderValidation() { }

        public override bool IsValid(object value)
        {

            if (value is (object)"Male" or (object)"Female") 
            {
                return true;
            }

            return false;
        }
    }
}

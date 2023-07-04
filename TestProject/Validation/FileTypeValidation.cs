using System.ComponentModel.DataAnnotations;

namespace WebAppCleanBlog.Validations
{
    public class FileTypeValidation : ValidationAttribute
    {
        private readonly string _filetypes;

        public FileTypeValidation(string FileTypes)
        {
            _filetypes = FileTypes;
        }

        public override bool IsValid(object value)
        {
            bool result = false;

            // Add validation logic here.
            if (value != null)
            {
                var file = value as IFormFile;

                string[] types = _filetypes.Split(',');

                foreach(string type in types)
                {
                    if (Path.GetExtension(file.FileName).ToLower() == type)
                        result = true;
                }
            }

            if(value == null)
            {
                result = true;
            }

            return result;
        }
    }
}

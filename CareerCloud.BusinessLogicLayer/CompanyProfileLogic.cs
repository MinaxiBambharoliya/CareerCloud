using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
        {
                
        }

        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            string sPattern = "^\\d{3}-\\d{3}-\\d{4}$";
            foreach (CompanyProfilePoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.CompanyWebsite) || !(poco.CompanyWebsite.EndsWith(".ca") || poco.CompanyWebsite.EndsWith(".com") || poco.CompanyWebsite.EndsWith(".biz")))
                    exceptions.Add(new ValidationException(600, $"{poco.Id}'s must end with extensions ('.ca','.com','.biz')"));

                if (string.IsNullOrEmpty(poco.ContactPhone) || Regex.IsMatch(poco.ContactPhone, sPattern) || poco.ContactPhone.Length != 12)
                    exceptions.Add(new ValidationException(601, $"{poco.Id}'s phone number must correspond to 123-456-7890 pattern!!"));
            }

            if (exceptions.Count > 0)
                throw new AggregateException(exceptions);
        }
    }
}

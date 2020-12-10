using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
    {
        public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repository) : base (repository)
        {

        }

        public override void Add(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(ApplicantSkillPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach(ApplicantSkillPoco poco in pocos)
            {
                if (poco.StartMonth > 12)
                    exceptions.Add(new ValidationException(101, $"{poco.Id} 's start month cannot be greater than 12!!"));

                if (poco.EndMonth > 12)
                    exceptions.Add(new ValidationException(102, $"{poco.Id} 's End month cannot be greater than 12!!"));

                if (poco.StartYear < 1900)
                    exceptions.Add(new ValidationException(103, $"{poco.Id} 's Start year cannot be less than 1900!!"));

                if (poco.EndYear < poco.StartYear)
                    exceptions.Add(new ValidationException(104, $"{poco.Id} 's End year cannot be less than Start year!!"));
            }

            if (exceptions.Count > 0)
                throw new AggregateException(exceptions);
        }
    }
}

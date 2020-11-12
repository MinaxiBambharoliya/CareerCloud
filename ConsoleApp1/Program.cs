using CareerCloud.ADODataAccessLayer;
using CareerCloud.Pocos;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicantEducationRepository repo = new ApplicantEducationRepository();

            ApplicantEducationPoco poco = new ApplicantEducationPoco();
            poco.Id = Guid.NewGuid();
            //poco.Applicant = Guid.NewGuid();
            poco.Major = "IT";
            poco.CertificateDiploma = "dotnet";
            poco.StartDate = new DateTime(2020, 10, 5);
            poco.CompletionDate = new DateTime(2021,03,21);
            poco.CompletionPercent = 80;

            repo.Add(new ApplicantEducationPoco[] {poco});


        }
    }
}

using Azure;
using Humanizer;

namespace NexusAPI.Models
{
    public class Nexus
    {
        public Guid NexusId { get; set; } = Guid.NewGuid();
        
        
        // JD
        public string?  Title { get; set; }
        
        public string? Description { get; set; }
        public TypeOfPosition TypeOfPosition { get; set; }
        public string? PrimaryRole { get; set; }
        public int WorkExperience { get; set; }
        public string? Skills { get; set; }

        //location

        public string? Location { get; set; }
        public bool AcceptApplicantsWhoNeedToRelocate { get; set; }
        public bool RelocationAssistance { get; set; }

        // Remote Work Details

        public RemotePolicy RemotePolicy { get; set; }

        //salary and equity

        public Currency Currency { get; set; }
        public decimal? AnnualSalaryMin { get; set; }
        public decimal? AnnualSalaryMax { get; set; }
        public bool Equity { get; set; }
        public decimal? EquityMin { get; set; }
        public decimal? EquityMax { get; set; }
        // dev
        public Guid? Creator { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;



        public ICollection<CodingAssessment>? CodingAssessments { get; set; }
    }

    public enum TypeOfPosition
    {
        FullTimeEmployee = 0,
        Contractor = 1,
        Cofounder = 2,
        Intern = 3
    }

    public enum RemotePolicy
    {
        InOfficeWfhFlexible = 0,
        InOfficeNotWfhFlexible = 1,
        OnsiteOrRemote = 2,
        RemoteOnly = 3
    }

    public enum Currency
    {
        USD, // United States Dollar
        EUR, // Euro
        GBP, // British Pound Sterling
        JPY, // Japanese Yen
        AUD, // Australian Dollar
        CAD, // Canadian Dollar
        CHF, // Swiss Franc
        CNY, // Chinese Yuan
        INR, // Indian Rupee
        RUB, // Russian Ruble
        BRL, // Brazilian Real
        ZAR, // South African Rand
        SEK, // Swedish Krona
        NZD, // New Zealand Dollar
        MXN, // Mexican Peso
        SGD, // Singapore Dollar
        HKD, // Hong Kong Dollar
        NOK, // Norwegian Krone
        KRW, // South Korean Won
        TRY, // Turkish Lira
        SAR, // Saudi Riyal
        AED, // United Arab Emirates Dirham
        ARS, // Argentine Peso
        COP, // Colombian Peso
        IDR, // Indonesian Rupiah
        ILS, // Israeli Shekel
        PLN, // Polish Zloty
        THB, // Thai Baht
        VND  // Vietnamese Dong
    }

}

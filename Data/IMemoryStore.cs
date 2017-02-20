using System.Collections.Generic;

using FisherInsuranceApi.Models;


namespace FisherInsuranceApi.Data

{

    public interface IMemoryStore

    {

        Quote CreateQuote(Quote quote);

        IEnumerable<Quote> RetrieveAllQuotes {get; }

        Quote RetrieveQuote(int id);

        Quote UpdateQuote(Quote quote);


        void DeleteQuote(int id);


    }
    
}
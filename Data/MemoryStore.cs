using System;

using System.Collections.Generic;

using FisherInsuranceApi.Models;


namespace FisherInsuranceApi.Data

{
    public class MemoryStore : IMemoryStore

    {

        private Dictionary<int,Quote> quotes;

        public MemoryStore()
        {
            quotes = new Dictionary<int, Quote>();

            new List<Quote>{
                new Quote { Product="Auto", ExpireDate=DateTime.Now.AddDays(45), Price=350.00M },
                new Quote { Product="Auto", ExpireDate=DateTime.Now.AddDays(45), Price=300.00M },
                new Quote { Product="Auto", ExpireDate=DateTime.Now.AddDays(45), Price=450.00M },
                new Quote { Product="Auto", ExpireDate=DateTime.Now.AddDays(45), Price=250.00M },
            }.ForEach(quote => this.CreateQuote(quote));     
            
        }
   
        public Quote CreateQuote(Quote quote)

        {
            if (quote.Id == 0)

            {
                int key = quotes.Count;

                while (quotes.ContainsKey(key))
                {
                    key++;
                };
                
                quote.Id=key;

            }
            quotes[quote.Id]=quote;

            return quote;
        }

    
        public IEnumerable<Quote> RetrieveAllQuotes => quotes.Values;

        public Quote RetrieveQuote(int id) => quotes.ContainsKey(id) ? quotes[id] : null;

        public Quote UpdateQuote(Quote quote)
        {
            quotes[quote.Id]=quote;

            return quote;
        }
        public void DeleteQuote(int id)
        {
            quotes.Remove(id);
        }
    
    }
}
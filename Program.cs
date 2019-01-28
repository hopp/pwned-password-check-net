using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using CsvHelper;
using SharpPwned.NET;
using PwnedPasswordCheckNet.Layouts;
using CommandLine;

namespace PwnedPasswordCheckNet
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = CommandLine.Parser.Default.ParseArguments<Options>(args);

            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o => Do(o))
                   .WithNotParsed<Options>((errs) => HandleParseError(errs));
        }

        static void HandleParseError(IEnumerable<Error> errors)
        {
            foreach(var error in errors)
            {
                Console.Out.WriteLine($"{error.Tag}, {error.ToString()}");
            }
        }

        static void Do(Options options)
        {
            var csvFile = options.Path;

            var records = new List<KeepassLayout>();
            using (var reader = new StreamReader(csvFile))
            {
                using (var csv = new CsvReader(reader))
                {
                    records = csv.GetRecords<KeepassLayout>().ToList();
                }
            }            

            var grouped = records.GroupBy(r => r.LoginName).Select(s=> new {Login = s.Key, Records = s.ToList()}).ToList().Where(l => l.Login.Contains("@"));

            var client = new HaveIBeenPwnedRestClient();

            foreach(var login in grouped)
            {
                var breaches = client.GetAccountBreaches(login.Login).Result;
                if(breaches.Any())
                {
                    Console.WriteLine($"Login: {login.Login}, Breaches: {string.Join(", ",breaches.Select(b => b.Title))}");
                    foreach(var record in login.Records)
                    {
                        var pwnedPass = client.IsPasswordPwned(record.Password).Result;
                        if(pwnedPass)
                        {
                            Console.WriteLine($"Possible pwned password - Account: {record.Account}, Password: {record.Password}");
                        }
                    }
                }
            }
        }
    }
}

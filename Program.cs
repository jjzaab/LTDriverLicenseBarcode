using System;
using System.Collections.Generic;
using Leadtools;
using Leadtools.Codecs;
using Leadtools.Barcode;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            // provide drivers license file with PDF417 barcode to recognize
            string inputFilePath = @"PATH TO IMAGE";

            RasterSupport.SetLicense(@"C:\LEADTOOLS 20\Common\License\LEADTOOLS.LIC", System.IO.File.ReadAllText(@"C:\LEADTOOLS 20\Common\License\LEADTOOLS.LIC.KEY"));

            using (RasterCodecs codecs = new RasterCodecs())
            using (RasterImage inputImage = codecs.Load(inputFilePath))
            {
                BarcodeEngine engine = new BarcodeEngine();
                BarcodeData data = engine.Reader.ReadBarcode(inputImage, LeadRect.Empty, BarcodeSymbology.PDF417);
                if (data != null)
                {
                    using (AAMVAID id = BarcodeData.ParseAAMVAData(data.GetData(), false))
                    {
                        if (id != null)
                        {
                            Console.WriteLine("Issuer Identification Number: " + id.IssuerIdentificationNumber);
                            Console.WriteLine("Jurisdiction: " + id.Jurisdiction.ToString());
                            Console.WriteLine("AAMVA CDS Version: " + id.Version.ToString());
                            Console.WriteLine("Jurisdiction Version: " + id.JurisdictionVersion);
                            Console.WriteLine("Number of Entries: " + id.NumberOfEntries.ToString());

                            AAMVANameResult firstNameResult = id.FirstName;
                            if (firstNameResult != null)
                            {
                                Console.WriteLine("First Name: " + firstNameResult.Value + ", Inferred?: " + firstNameResult.InferredFromFullName);
                            }

                            AAMVANameResult lastNameResult = id.LastName;
                            if (lastNameResult != null)
                            {
                                Console.WriteLine("Last Name: " + lastNameResult.Value + ", Inferred?: " + lastNameResult.InferredFromFullName);
                            }

                            string addressStreet1 = id.AddressStreet1;
                            if (addressStreet1 != null)
                                Console.WriteLine("Address Street 1: " + addressStreet1);

                            string addressStreet2 = id.AddressStreet2;
                            if (addressStreet2 != null)
                                Console.WriteLine("Address Street 2: " + addressStreet2);

                            string addressStateAbbreviation = id.AddressStateAbbreviation;
                            if (addressStateAbbreviation != null)
                                Console.WriteLine("Address State Abbreviation: " + addressStateAbbreviation);

                            string addressCity = id.AddressCity;
                            if (addressCity != null)
                                Console.WriteLine("Address City: " + addressCity);

                            string addressPostalCode = id.AddressPostalCode;
                            if (addressPostalCode != null)
                                Console.WriteLine("Address Postal Code: " + addressPostalCode);

                            AAMVARegion addressRegion = id.AddressRegion;
                            Console.WriteLine("Address Region: " + addressRegion.ToString());

                            string dateOfBirth = id.DateOfBirth;
                            if (dateOfBirth != null)
                                Console.WriteLine("Date of Birth: " + dateOfBirth);

                            if (id.Over18Available)
                            {
                                Console.WriteLine("Over 18?: " + id.Over18);
                            }

                            if (id.Over19Available)
                            {
                                Console.WriteLine("Over 19?: " + id.Over19);
                            }

                            if (id.Over21Available)
                            {
                                Console.WriteLine("Over 21?: " + id.Over21);
                            }

                            if (id.ExpirationAvailable)
                            {
                                Console.WriteLine("Expired?: " + id.Expired);
                            }

                            string expirationDate = id.ExpirationDate;
                            if (expirationDate != null)
                                Console.WriteLine("Expiration Date: " + expirationDate);

                            string issueDate = id.IssueDate;
                            if (issueDate != null)
                                Console.WriteLine("Issue Date: " + issueDate);

                            string idNumber = id.Number;
                            if (idNumber != null)
                                Console.WriteLine("ID Number: " + idNumber);

                            AAMVAEyeColor eyeColor = id.EyeColor;
                            Console.WriteLine("Eye Color: " + eyeColor.ToString());

                            AAMVAHairColor hairColor = id.HairColor;
                            Console.WriteLine("Hair Color: " + hairColor.ToString());

                            AAMVASex sex = id.Sex;
                            Console.WriteLine("Sex: " + sex.ToString());
                            Console.ReadLine();
                        }
                    }
                }
            }

        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMModels;
using System.Data;

namespace JMSeedData
{

    public class SeedData
    {

        public List<Customer> Customers { get; set; }
        public List<PaymentType> PaymentTypes { get; set; }

        public List<Customer> LoadDataSet(out TransactionalInformation transaction)
        {

            transaction = new TransactionalInformation();

            Customers = new List<Customer>();
            PaymentTypes = new List<PaymentType>();

            try
            {

                PaymentType paymentType1 = new PaymentType();
                paymentType1.PaymentTypeID = new Guid("dd000829-46dd-41a1-9d8d-5f55c3b844a1");
                paymentType1.Description = "Check";
                paymentType1.RequiresCreditCard = 0;
                PaymentTypes.Add(paymentType1);

                PaymentType paymentType2 = new PaymentType();
                paymentType2.PaymentTypeID = new Guid("73db4cab-1ddb-466d-930f-97699373b333");
                paymentType2.Description = "Visa";
                paymentType2.RequiresCreditCard = 1;
                PaymentTypes.Add(paymentType2);

                PaymentType paymentType3 = new PaymentType();
                paymentType3.PaymentTypeID = new Guid("3e0dd131-a8d3-4bfb-bded-9f7209b6965b");
                paymentType3.Description = "American Express";
                paymentType3.RequiresCreditCard = 1;
                PaymentTypes.Add(paymentType3);

                PaymentType paymentType4 = new PaymentType();
                paymentType4.PaymentTypeID = new Guid("afb8207c-1f28-41e1-8834-e9a0041806a0");
                paymentType4.Description = "Bill-Me Later";
                paymentType4.RequiresCreditCard = 0;
                PaymentTypes.Add(paymentType4);

                PaymentType paymentType5 = new PaymentType();
                paymentType5.PaymentTypeID = new Guid("1bcf2c92-028f-4664-9f5e-f39545691ce7");
                paymentType5.Description = "Discover";
                paymentType5.RequiresCreditCard = 1;
                PaymentTypes.Add(paymentType5);

                PaymentType paymentType6 = new PaymentType();
                paymentType6.PaymentTypeID = new Guid("18851120-73c5-40f7-b498-f82aa7b28d24");
                paymentType6.Description = "MasterCard";
                paymentType6.RequiresCreditCard = 1;
                PaymentTypes.Add(paymentType6);

                DataSet dataSet = new DataSet();

                dataSet.ReadXml(@"c:\myfiles\CodeProjectMVC5\TestData.xml");

                int count = dataSet.Tables.Count;
                int rows = dataSet.Tables[0].Rows.Count;
                int paymentType = 0;
                int counter = 0;

                //DataSet cloneSet = dataSet.Clone();
                //int colCount = dataSet.Tables[0].Columns.Count;

                for (int i = 0; i < dataSet.Tables[0].Rows.Count - 1; i++)
                {
                    counter++;

                    Customer customer = new Customer();

                    DataRow datarow = dataSet.Tables[0].Rows[i];

                    customer.CustomerID = Guid.NewGuid();

                    customer.FirstName = datarow["FirstName"].ToString();
                    customer.LastName = datarow["LastName"].ToString();
                    customer.PhoneNumber = datarow["PhoneNumber"].ToString();
                    //customer.TelePhone = datarow["TelePhone"].ToString();
                    customer.Address = datarow["AddressLine1"].ToString();

                    if (datarow["AddressLine1"].ToString().Length > 0)
                        customer.Address = customer.Address + ", " + datarow["AddressLine2"].ToString();

                    customer.City = datarow["City"].ToString();
                    customer.Region = datarow["State"].ToString();
                    customer.PostalCode = datarow["ZipCode"].ToString();
                    customer.EmailAddress = datarow["EmailAddress"].ToString();

                    if (customer.EmailAddress.Trim().Length == 0)
                    {
                        customer.EmailAddress = customer.LastName + customer.FirstName + "@hotmail.com";
                    }

                    customer.EmailAddress = customer.EmailAddress.Replace(" ", "");

                    customer.CreditLimit = 0;
                    customer.CreditCardNumber = "";
                    customer.CreditCardSecurityCode = "";
                    customer.DateCreated = DateTime.Now;
                    customer.DateUpdated = DateTime.Now;

                    DateTime birthDate;
                    DateTime testDate;
                    DateTime creditCardExpirationDate;

                    string dob = Convert.ToString(datarow["DateOfBirth"]);
                    if (DateTime.TryParse(dob, out testDate))
                    {
                        birthDate = testDate;
                        string year = birthDate.Year.ToString();
                        string creditCardDate = birthDate.Month + "/" + birthDate.Day + "/" + year;
                        creditCardExpirationDate = Convert.ToDateTime(creditCardDate);
                    }
                    else
                    {
                        creditCardExpirationDate = Convert.ToDateTime(DateTime.Now.AddYears(1));
                    }

                    paymentType = paymentType + 1;
                    int requiresCreditCard = 0;

                    if (paymentType == 1)
                    {
                        customer.PaymentTypeID = paymentType1.PaymentTypeID;
                        requiresCreditCard = paymentType1.RequiresCreditCard;
                    }
                    else if (paymentType == 2)
                    {
                        customer.PaymentTypeID = paymentType2.PaymentTypeID;
                        requiresCreditCard = paymentType2.RequiresCreditCard;
                    }
                    else if (paymentType == 3)
                    {
                        customer.PaymentTypeID = paymentType3.PaymentTypeID;
                        requiresCreditCard = paymentType3.RequiresCreditCard;
                    }
                    else if (paymentType == 4)
                    {
                        customer.PaymentTypeID = paymentType4.PaymentTypeID;
                        requiresCreditCard = paymentType4.RequiresCreditCard;
                    }
                    else if (paymentType == 5)
                    {
                        customer.PaymentTypeID = paymentType5.PaymentTypeID;
                        requiresCreditCard = paymentType5.RequiresCreditCard;
                    }
                    else if (paymentType == 6)
                    {
                        customer.PaymentTypeID = paymentType6.PaymentTypeID;
                        requiresCreditCard = paymentType6.RequiresCreditCard;
                        paymentType = 0;
                    }

                    if (requiresCreditCard == 0)
                    {
                        Random random = new Random();
                        int creditLimit = random.Next(1000, 10000);
                        customer.CreditLimit = (Decimal)creditLimit;
                    }
                    else
                    {
                        Random random = new Random();
                        int creditCardNumber = random.Next(1000000, 14000000);
                        int creditCardSecurityCode = random.Next(100, 999);
                        customer.CreditCardNumber = creditCardNumber.ToString();
                        customer.CreditCardSecurityCode = creditCardSecurityCode.ToString();
                        customer.CreditCardExpirationDate = creditCardExpirationDate;

                    }

                    //if (counter == 50)
                    //{
                    //DataRow workRow = cloneSet.Tables[0].NewRow();

                    //for (int x = 0; x < colCount; x++)
                    //{
                    //    workRow[x] = datarow[x];
                    //}

                    //cloneSet.Tables[0].Rows.Add(workRow);

                    //Customers.Add(customer);
                    //counter = 0;
                    //}

                    Customers.Add(customer);

                }

                transaction.ReturnMessage = new List<string>();
                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Records Loaded = " + rows.ToString());

                //int clonedrows = cloneSet.Tables[0].Rows.Count;

                //cloneSet.WriteXml(@"c:\myfiles\CodeProjectMVC5\TestData.xml");


                return Customers;
            }
            catch (Exception ex)
            {
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);

                return null;
            }


        }

    }
}

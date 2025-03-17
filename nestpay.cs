using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Nestpay
{
    /**
     * Nestpay veya Payten kullanan tüm sistemler için bu alan kullanılabilir.
     */
    public enum MODE
    {
        Test,
        Prod
    }
    public enum BANK
    {
        Asseco,
        Anadolu,
        Akbank,
        Isbank,
        Ziraat,
        Halkbank,
        Finansbank,
        Teb
    }
    public class Nestpay
    {
        public string Mode { get; set; }
        public string Endpoint { get; set; }
        private string ClientId { get; set; }
        private string Username { get; set; }
        private string Password { get; set; }
        private string StoreKey { get; set; }
        public void SetClientId(string clientid)
        {
            ClientId = clientid;
        }
        public void SetUsername(string username)
        {
            Username = username;
        }
        public void SetPassword(string password)
        {
            Password = password;
        }
        public void SetStoreKey(string storekey)
        {
            StoreKey = storekey;
        }
        public Nestpay(MODE mode, BANK bank)
        {
            // Switch expression yerine if-else veya switch-case kullanılmalı
            switch (mode)
            {
                case MODE.Test:
                    Mode = "T";
                    break;
                case MODE.Prod:
                    Mode = "P";
                    break;
                default:
                    Mode = null;
                    break;
            }

            switch (bank)
            {
                case BANK.Asseco:
                    Endpoint = "https://entegrasyon.asseco-see.com.tr";
                    break;
                case BANK.Akbank:
                    Endpoint = "https://www.sanalakpos.com";
                    break;
                case BANK.Anadolu:
                    Endpoint = "https://anadolusanalpos.est.com.tr";
                    break;
                case BANK.Isbank:
                    Endpoint = "https://spos.isbank.com.tr";
                    break;
                case BANK.Ziraat:
                    Endpoint = "https://sanalpos2.ziraatbank.com.tr";
                    break;
                case BANK.Halkbank:
                    Endpoint = "https://sanalpos.halkbank.com.tr";
                    break;
                case BANK.Finansbank:
                    Endpoint = "https://www.fbwebpos.com";
                    break;
                case BANK.Teb:
                    Endpoint = "https://sanalpos.teb.com.tr";
                    break;
                default:
                    Endpoint = null;
                    break;
            }
        }
        [XmlRoot("CC5Request")]
        public class CC5Request
        {
            [XmlElement("Mode", IsNullable = false)]
            public string Mode { get; set; }
            [FormElement("TranType")]
            [XmlElement("Type", IsNullable = false)]
            public string TransactionType { get; set; }
            [FormElement("storetype")]
            [XmlIgnore]
            public string StoreType { get; set; }
            [XmlElement("Name", IsNullable = false)]
            public string Username { get; set; }
            [XmlElement("Password", IsNullable = false)]
            public string Password { get; set; }
            [FormElement("clientid")]
            [XmlElement("ClientId", IsNullable = false)]
            public string ClientId { get; set; }
            [FormElement("oid")]
            [XmlElement("OrderId", IsNullable = false)]
            public string OrderId { get; set; }
            [XmlElement("GroupId", IsNullable = false)]
            public string GroupId { get; set; }
            [XmlElement("TransId", IsNullable = false)]
            public string TransId { get; set; }
            [XmlElement("UserId", IsNullable = false)]
            public string UserId { get; set; }
            [FormElement("clientip")]
            [XmlElement("IPAddress", IsNullable = false)]
            public string IPAddress { get; set; }
            [XmlElement("Email", IsNullable = false)]
            public string Email { get; set; }
            [FormElement("pan")]
            [XmlElement("Number", IsNullable = false)]
            public string CardNumber { get; set; }
            [XmlElement("Expires", IsNullable = false)]
            public string CardExpiry { get; set; }
            [FormElement("Ecom_Payment_Card_ExpDate_Month")]
            [XmlIgnore]
            public string CardMonth { get; set; }
            [FormElement("Ecom_Payment_Card_ExpDate_Year")]
            [XmlIgnore]
            public string CardYear { get; set; }
            [FormElement("cv2")]
            [XmlElement("Cvv2Val", IsNullable = false)]
            public string CardCode { get; set; }
            [FormElement("amount")]
            [XmlElement("Total", IsNullable = false)]
            public string Amount { get; set; }
            [FormElement("currency")]
            [XmlElement("Currency", IsNullable = false)]
            public string Currency { get; set; }
            [FormElement("Instalment")]
            [XmlElement("Instalment", IsNullable = false)]
            public string Installment { get; set; }
            [XmlElement("BillTo", IsNullable = false)]
            public To BillTo { get; set; }
            [XmlElement("ShipTo", IsNullable = false)]
            public To ShipTo { get; set; }
            [XmlElement("PayerTxnId", IsNullable = false)]
            public string PayerTxnId { get; set; }
            [XmlElement("PayerSecurityLevel", IsNullable = false)]
            public string PayerSecurityLevel { get; set; }
            [XmlElement("PayerAuthenticationCode", IsNullable = false)]
            public string PayerAuthenticationCode { get; set; }
            [XmlElement("CardholderPresentCode", IsNullable = false)]
            public string CardholderPresentCode { get; set; }
            [FormElement("rnd")]
            [XmlIgnore]
            public string Random { get; set; }
            [FormElement("hashAlgorithm")]
            [XmlIgnore]
            public string HashAlgorithm { get; set; }
            [FormElement("okUrl")]
            [XmlIgnore]
            public string OkUrl { get; set; }
            [FormElement("failUrl")]
            [XmlIgnore]
            public string FailUrl { get; set; }
            [FormElement("lang")]
            [XmlIgnore]
            public string Lang { get; set; }
            public void SetIPv4(string ipv4)
            {
                IPAddress = ipv4;
            }
            public void SetOrderId(string orderid)
            {
                OrderId = orderid;
            }
            public void SetAmount(string amount, string currency)
            {
                Amount = amount;
                // Switch expression yerine if-else veya switch-case kullanılmalı
                switch (currency)
                {
                    case "TRY":
                    case "YTL":
                    case "TRL":
                    case "TL":
                        Currency = "949";
                        break;
                    case "USD":
                        Currency = "840";
                        break;
                    case "EUR":
                        Currency = "978";
                        break;
                    case "GBP":
                        Currency = "826";
                        break;
                    case "JPY":
                        Currency = "392";
                        break;
                    default:
                        Currency = currency;
                        break;
                }
            }
            public void SetInstallment(string installment)
            {
                Installment = installment;
            }
            public void SetCardNumber(string cardnumber)
            {
                CardNumber = cardnumber;
            }
            public void SetCardExpiry(string cardmonth, string cardyear)
            {
                CardExpiry = cardmonth + "/" + cardyear;
                CardMonth = cardmonth;
                CardYear = cardyear;
            }
            public void SetCardCode(string cardcode)
            {
                CardCode = cardcode;
            }
            public void SetUsername(string username)
            {
                Username = username;
            }
            public void SetEmail(string email)
            {
                Email = email;
            }
            public void SetOkUrl(string okUrl)
            {
                OkUrl = okUrl;
            }
            public void SetFailUrl(string failUrl)
            {
                FailUrl = failUrl;
            }
        }
        public class To
        {
            [FormElement("cardholder")]
            [XmlElement("Name", IsNullable = false)]
            public string Name { get; set; }
            [XmlElement("Company", IsNullable = false)]
            public string Company { get; set; }
            [XmlElement("Street1", IsNullable = false)]
            public string Street1 { get; set; }
            [XmlElement("Street2", IsNullable = false)]
            public string Street2 { get; set; }
            [XmlElement("Street3", IsNullable = false)]
            public string Street3 { get; set; }
            [XmlElement("City", IsNullable = false)]
            public string City { get; set; }
            [XmlElement("StateProv", IsNullable = false)]
            public string StateProv { get; set; }
            [XmlElement("PostalCode", IsNullable = false)]
            public string PostalCode { get; set; }
            [XmlElement("Country", IsNullable = false)]
            public string Country { get; set; }
            [FormElement("phone")]
            [XmlElement("TelVoice", IsNullable = false)]
            public string TelVoice { get; set; }
        }
        [XmlRoot("CC5Response")]
        public class CC5Response
        {
            [XmlElement("OrderId")]
            public string OrderId { get; set; }
            [XmlElement("GroupId")]
            public string GroupId { get; set; }
            [XmlElement("TransId")]
            public string TransId { get; set; }
            [XmlElement("Response")]
            public string Response { get; set; }
            [XmlElement("AuthCode")]
            public string AuthCode { get; set; }
            [XmlElement("HostRefNum")]
            public string HostRefNum { get; set; }
            [XmlElement("ProcReturnCode")]
            public string ProcReturnCode { get; set; }
            [XmlElement("ErrMsg")]
            public string ErrMsg { get; set; }
        }
        public class FormElementAttribute : Attribute
        {
            public string Key { get; }
            public FormElementAttribute(string key)
            {
                Key = key;
            }
        }
        public class Writer : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }
        public static string Json<T>(T data) where T : class
        {
            // JSON işlemi .NET 4.6.1 için Newtonsoft.Json kullanılarak yapılmalı
            return Newtonsoft.Json.JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented,
                new Newtonsoft.Json.JsonSerializerSettings
                {
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
                });
        }
        public static byte[] Byte(string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }
        public static string Hash(Dictionary<string, string> form, string secret)
        {
            var data = new List<string>();
            foreach (var item in form.OrderBy(key => key.Key, StringComparer.InvariantCultureIgnoreCase))
            {
                if (item.Key.ToLower() != "hash" && item.Key.ToLower() != "encoding")
                {
                    data.Add(item.Value);
                }
            }
            data.Add(secret);
            return Convert.ToBase64String(SHA512.Create().ComputeHash(Byte(string.Join("|", data))));
        }
        public CC5Response PreAuth(CC5Request data)
        {
            data.Mode = Mode;
            data.ClientId = ClientId;
            data.Username = Username;
            data.Password = Password;
            data.TransactionType = "PreAuth";
            return _Transaction(data);
        }
        public CC5Response PostAuth(CC5Request data)
        {
            data.Mode = Mode;
            data.ClientId = ClientId;
            data.Username = Username;
            data.Password = Password;
            data.TransactionType = "PostAuth";
            return _Transaction(data);
        }
        public CC5Response Auth(CC5Request data)
        {
            data.Mode = Mode;
            data.ClientId = ClientId;
            data.Username = Username;
            data.Password = Password;
            data.TransactionType = "Auth";
            return _Transaction(data);
        }
        public CC5Response Refund(CC5Request data)
        {
            data.Mode = Mode;
            data.ClientId = ClientId;
            data.Username = Username;
            data.Password = Password;
            data.TransactionType = "Credit";
            return _Transaction(data);
        }
        public CC5Response Cancel(CC5Request data)
        {
            data.Mode = Mode;
            data.ClientId = ClientId;
            data.Username = Username;
            data.Password = Password;
            data.TransactionType = "Void";
            return _Transaction(data);
        }
        public CC5Response PreAuth3d(CC5Request data)
        {
            data.Mode = Mode;
            data.ClientId = ClientId;
            data.Username = Username;
            data.Password = Password;
            data.TransactionType = "PreAuth";
            return _Transaction(data);
        }
        public CC5Response Auth3d(CC5Request data)
        {
            data.Mode = Mode;
            data.ClientId = ClientId;
            data.Username = Username;
            data.Password = Password;
            data.TransactionType = "Auth";
            return _Transaction(data);
        }
        public Dictionary<string, string> PreAuth3dForm(CC5Request data)
        {
            data.ClientId = ClientId;
            data.TransactionType = "PreAuth";
            data.HashAlgorithm = "ver3";
            data.StoreType = "3D";
            data.Random = new Random().Next(100000, 999999).ToString();
            var form = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            var elements = data.GetType().GetProperties().Where(x => x.GetCustomAttribute<FormElementAttribute>() != null);
            foreach (var element in elements)
            {
                var key = element.GetCustomAttribute<FormElementAttribute>().Key;
                var value = element.GetValue(data)?.ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    form.Add(key, value);
                }
            }
            if (data.BillTo != null)
            {
                var billto_elements = data.BillTo.GetType().GetProperties().Where(x => x.GetCustomAttribute<FormElementAttribute>() != null);
                foreach (var element in billto_elements)
                {
                    var key = element.GetCustomAttribute<FormElementAttribute>().Key;
                    var value = element.GetValue(data.BillTo)?.ToString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        form.Add(key, value);
                    }
                }
            }
            if (data.ShipTo != null)
            {
                var shipto_elements = data.ShipTo.GetType().GetProperties().Where(x => x.GetCustomAttribute<FormElementAttribute>() != null);
                foreach (var element in shipto_elements)
                {
                    var key = element.GetCustomAttribute<FormElementAttribute>().Key;
                    var value = element.GetValue(data.ShipTo)?.ToString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        form.Add(key, value);
                    }
                }
            }
            form.Add("hash", Hash(form, StoreKey));
            return form.OrderBy(key => key.Key, StringComparer.InvariantCultureIgnoreCase).ToDictionary(x => x.Key, x => x.Value);
        }
        public Dictionary<string, string> Auth3dForm(CC5Request data)
        {
            data.ClientId = ClientId;
            data.TransactionType = "Auth";
            data.HashAlgorithm = "ver3";
            data.StoreType = "3D";
            data.Random = new Random().Next(100000, 999999).ToString();
            var form = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            var elements = data.GetType().GetProperties().Where(x => x.GetCustomAttribute<FormElementAttribute>() != null);
            foreach (var element in elements)
            {
                var key = element.GetCustomAttribute<FormElementAttribute>().Key;
                var value = element.GetValue(data)?.ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    form.Add(key, value);
                }
            }
            if (data.BillTo != null)
            {
                var billto_elements = data.BillTo.GetType().GetProperties().Where(x => x.GetCustomAttribute<FormElementAttribute>() != null);
                foreach (var element in billto_elements)
                {
                    var key = element.GetCustomAttribute<FormElementAttribute>().Key;
                    var value = element.GetValue(data.BillTo)?.ToString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        form.Add(key, value);
                    }
                }
            }
            if (data.ShipTo != null)
            {
                var shipto_elements = data.ShipTo.GetType().GetProperties().Where(x => x.GetCustomAttribute<FormElementAttribute>() != null);
                foreach (var element in shipto_elements)
                {
                    var key = element.GetCustomAttribute<FormElementAttribute>().Key;
                    var value = element.GetValue(data.ShipTo)?.ToString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        form.Add(key, value);
                    }
                }
            }
            form.Add("hash", Hash(form, StoreKey));
            return form.OrderBy(key => key.Key, StringComparer.InvariantCultureIgnoreCase).ToDictionary(x => x.Key, x => x.Value);
        }
        private CC5Response _Transaction(CC5Request data)
        {
            var cc5request = new XmlSerializer(typeof(CC5Request));
            var cc5response = new XmlSerializer(typeof(CC5Response));

            // using var yapısı .NET 4.6.1 için uygun değil
            Writer writer = new Writer();
            try
            {
                var ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);
                cc5request.Serialize(writer, data, ns);

                // HttpClient kullanımı .NET 4.6.1 için farklı olmalı
                using (HttpClient http = new HttpClient())
                {
                    // Async olmayan yaklaşım kullanılmalı
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Endpoint + "/fim/api");
                    request.Content = new StringContent(writer.ToString(), Encoding.UTF8, "text/xml");

                    // SendAsync ve Wait kullanılmalı
                    HttpResponseMessage response = http.SendAsync(request).Result;

                    // ReadAsStream yerine ReadAsStreamAsync().Result kullanılmalı
                    // veya daha basit olarak ReadAsStream şimdi yok, farklı yöntem kullanılmalı
                    Stream stream = response.Content.ReadAsStreamAsync().Result;
                    var result = (CC5Response)cc5response.Deserialize(stream);
                    return result;
                }
            }
            catch (Exception err)
            {
                if (err.InnerException != null)
                {
                    Console.WriteLine(err.InnerException.Message);
                }
                else
                {
                    Console.WriteLine(err.Message);
                }
            }
            finally
            {
                // Kaynakları serbest bırak
                if (writer != null)
                {
                    writer.Dispose();
                }
            }
            return null;
        }
    }
}
